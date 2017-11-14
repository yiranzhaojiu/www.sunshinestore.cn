using ServiceStack.OrmLite;
using Store_FrameWork.Attributes;
using Store_FrameWork.BaseData;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Fasterflect;

namespace Strore_Data
{
    public partial class DbUtility
    {

        #region 属性
        /// <summary>
        /// 连接字符串字典
        /// </summary>
        private static ConcurrentDictionary<string, string> cahceConnStringDictionary =
            new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 业务类型字典
        /// </summary>
        private static ConcurrentDictionary<Type, BusinessRwType> cacheBusinessDictionary =
            new ConcurrentDictionary<Type, BusinessRwType>();

        #endregion

        #region 创建索

        private static object ConnectionFactoryLock = new object();

        #endregion

        #region 获取表达式树的对象
        
        /// <summary>
        /// 获取表达式树的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static SqlExpression<T> GetGetSqlExpression<T>()
        {
            return DbFactory.CurrentDialectProvider.SqlExpression<T>();
        }

        #endregion

        #region  Read DataBase

        /// <summary>
        /// 获取List<T>
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public static List<T> GetList<T>(SqlExpression<T> expression, int pageIndex = 0, int pageSize = 0)
        {
            if(expression==null)
                throw new ArgumentNullException("expression is null");

            //如果有分页则获取分页的数据
            if (pageIndex > 0 && pageSize > 1)
                expression.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return ExecRead<T>(r => r.Select(expression));
        }

        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public static T GetSingle<T>(Expression<Func<T, bool>> express)
        {
            if (express == null) throw new ArgumentNullException("Express IS NULL"); 
            return Exec(r=>r.Single(express));  
        }

        /// <summary>
        /// 读库 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param> 
        /// <returns>T</returns>
        internal static T Exec<T>(Func<IDbConnection, T> func)
        {
            var connection = DbReadConnection<T>();

            try
            {
                return func(connection);　
            }
            catch (Exception ex)
            {　
                throw;
            }
            finally
            {
                CloseConnection(connection);
            }
        }

        /// <summary>
        /// 读库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="precide"></param>
        /// <returns>return List<T> </returns>
        internal static List<T> ExecRead<T>(Func<IDbConnection, List<T>> precide)
        {
            IDbConnection dbConnection = DbReadConnection<T>(); 
            try
            {
                return precide(dbConnection);
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection(dbConnection);
            }  
        }

        /// <summary>
        /// 读库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="func"></param>
        /// <param name="connection"></param>
        /// <param name="isJoinBuilder"></param>
        /// <returns></returns>
        internal static List<T> ExecRead<T, T1>(Func<IDbConnection, List<T>> func, IDbConnection connection = null,
                                               bool isJoinBuilder = false)
        {
            var isRead = true;
            if (connection == null)
                connection = DbReadConnection<T1>();
            else isRead = false;

            try
            {
                var result = func(connection);
                return result;
            }
            catch (Exception ex)
            {
                //connection.GetLastSql()
                throw;
            }
            finally
            {
                CloseConnection(connection, isRead);
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="To">返回的类型（dto or entity）</typeparam>
        /// <typeparam name="TFrom">查询的类型(entity)</typeparam>
        /// <param name="expression">查询表达式</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public static List<To> GetList<To, TFrom>(SqlExpression<TFrom> expression, int pageIndex = 0, int pageSize = 0)
            where To : new()
        {
            if (expression == null) throw new ArgumentNullException("expression");
            
            if (pageIndex > 0 && pageSize > 1)
                expression.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var items = ExecRead<To, TFrom>(connection => connection.Select<To, TFrom>(expression));

            return items; 
        }


        /// <summary>
        /// 根据类型的特性读取不同的数据库,读的权限
        /// </summary>
        private static IDbConnection DbReadConnection<T>()
        {
            //根据类型T判断读取数据库的连接 
            var businessType = GetBusinessRwType<T>(); 
            return DbOpenConnection(businessType.BusinessRead); 
        }

        #endregion

        #region Write DataBase

        /// <summary>
        /// 添加对象(只运行添加单个对象，切记)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="selectIdentity"></param>
        /// <returns></returns>
        public static long Add<T>(T entity, bool selectIdentity = false)
        {
            if (entity == null) throw new ArgumentNullException("entity is null");

            var r= ExecWrite<long, T>(db => db.Insert(entity, true));

            if (!selectIdentity)
            {
                return 1;
            } 
            return r;
        }

        /// <summary>
        ///  批添加对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitis"></param>
        /// <returns></returns>
        public static bool AddAll<T>(IEnumerable<T> entitis)
        {
            if(entitis==null) throw new ArgumentNullException("IEnumerable entitis is null");

            try
            {
                ExecWrite<T>(db => db.InsertAll(entitis));
                return true;
            }
            catch
            {
                return false;   
            } 
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static long Update<T>(T entity,SqlExpression<T> expression)
        {
            if (entity == null) throw new ArgumentNullException("IEnumerable entitis is null");

            if(string.IsNullOrEmpty(expression.WhereExpression)|| WhereIsOneEqualOne(expression.WhereExpression))
                throw new ArgumentNullException("SqlExpression expression is error");

            return ExecWrite<long, T>(connection=>connection.UpdateOnly(entity, expression));
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static int Delte<T>(SqlExpression<T> expression)
        {
            if (string.IsNullOrEmpty(expression.WhereExpression) || WhereIsOneEqualOne(expression.WhereExpression))
                throw new ArgumentNullException("SqlExpression expression is error");

            return  DbWriteConnection<T>().Delete(expression);
        }

        /// <summary>
        /// 判断WHERE条件是否是1=1
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static bool WhereIsOneEqualOne(string where)
        {
            return where.Replace(" ", "") == "where1=1";
        }
         
        /// <summary>
        /// 执行数据的写
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private static T ExecWrite<T, T1>(Func<IDbConnection, T> func)
        {
            IDbConnection dbConnection = DbWriteConnection<T1>();

            try
            {
                return func(dbConnection);
            }
            catch (Exception ex)
            { 
                throw;
            }
            finally
            {
                CloseConnection(dbConnection); 
            }
        }

        /// <summary>
        /// 执行数据的写
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private static void ExecWrite<T>(Action<IDbConnection> func)
        {
            IDbConnection dbConnection = DbWriteConnection<T>();

            try
            {
                func(dbConnection);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection(dbConnection);
            }
        }

        /// <summary>
        /// 根据类型的特性读取不同的数据库，写的权限
        /// </summary>
        private static IDbConnection DbWriteConnection<T>()
        {
            //根据类型T判断读取数据库的连接 
            var businessType = GetBusinessRwType<T>();
            return DbOpenConnection(businessType.BusinessWrite);
        }

        #endregion
         
        #region internal 方法 

        /// <summary>
        /// 读取类型的特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal static BusinessRwType GetBusinessRwType<T>()
        {
            var type = typeof(T);
            if (!cacheBusinessDictionary.ContainsKey(type))
            {
                try
                {
                    var businessType = (BusinessRwTypeAttribute)(type.GetCustomAttributes(typeof(BusinessRwTypeAttribute), true)[0]);

                    cacheBusinessDictionary.TryAdd(type, new BusinessRwType() { BusinessRead=businessType.BusinessRead,BusinessWrite=businessType.BusinessWrite });
                }
                catch
                {
                    throw new Exception("类型没有设置BusinessType特性");
                } 
            }
            
            return cacheBusinessDictionary[type];
        }

        #endregion

        #region DataBase Common
     
        /// <summary>
        ///  打开数据库连接
        /// </summary>
        /// <param name="businessType"></param>
        private static IDbConnection DbOpenConnection(BusinessType businessType)
        {
            string businessCacheTypekey = businessType.ToString();

            string connecStr = cahceConnStringDictionary.GetOrAdd(businessCacheTypekey, key =>
            {
                return BaseClass.GetConnenctionStr(businessCacheTypekey); 
            });

            #region 获取ConnectionFactory对象

            OrmLiteConnectionFactory connectionFactory = null;
            if (OrmLiteConnectionFactory.NamedConnections.ContainsKey(businessCacheTypekey))
            {
                connectionFactory = OrmLiteConnectionFactory.NamedConnections[businessCacheTypekey];
            }
            else
            {
                lock (ConnectionFactoryLock)
                {
                    var dialect = DbFactory.CurrentDialectProvider;
                    dialect.NamingStrategy = new AosNamingStrategy();
                    connectionFactory = new OrmLiteConnectionFactory(connecStr, dialect);
                    OrmLiteConnectionFactory.NamedConnections.Add(businessCacheTypekey, connectionFactory);
                }
            }

            #endregion

            IDbConnection dbConnection = null;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(businessCacheTypekey))
                {
                    dbConnection = HttpContext.Current.Items[businessCacheTypekey] as IDbConnection;
                }
                else
                {
                    dbConnection = connectionFactory.OpenDbConnection();
                    HttpContext.Current.Items.Add(businessCacheTypekey, dbConnection);
                }
            }
            else //非web环境下
            {
                return connectionFactory.OpenDbConnection();
            }

            if (dbConnection != null && dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            return dbConnection;
        }
         
        #endregion

        #region 事务

        public static AosTransaction OpenTransaction<T>(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var businessType = typeof(T).Attribute<BusinessRwTypeAttribute>();

            if (businessType == null)
                throw new Exception("该类型没有添加BusinessRwTypeAttribute特性");

            var connection = DbWriteConnection<T>();

            var cacheKey = businessType.BusinessWrite;
            //WEB环境
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[cacheKey] = connection;
            }
             
            AosTransaction transaction = null;
            if (!DbTransactionContext.HasTransaction(connection.Database))
            {
                transaction = new AosTransaction(connection.BeginTransaction(isolationLevel));
                DbTransactionContext.AddTransaction(connection.Database, transaction);
            }
            else
            {
                transaction = DbTransactionContext.GetTransaction(connection.Database);
            }
            transaction.AddCount();
            return transaction;
        }

        #endregion

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="IsReadConnection">是否读链接 默认不是</param>
        public static void CloseConnection(IDbConnection connection, bool IsReadConnection = false)
        {
            if (!DbTransactionContext.HasTransaction(connection.Database))
            { 
                if (HttpContext.Current != null)
                { 
                    return;
                }
                connection.Close(); 
            }
        }

    } 

}
