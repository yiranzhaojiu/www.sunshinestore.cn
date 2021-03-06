﻿using System;
using System.Data;
using System.Linq.Expressions;

namespace ServiceStack.OrmLite
{
    public static class OrmLiteWriteExpressionsApi
    {
        /// <summary>
        /// Use an SqlExpression to select which fields to update and construct the where expression, E.g: 
        /// 
        ///   db.UpdateOnly(new Person { FirstName = "JJ" }, ev => ev.Update(p => p.FirstName).Where(x => x.FirstName == "Jimi"));
        ///   UPDATE "Person" SET "FirstName" = 'JJ' WHERE ("FirstName" = 'Jimi')
        /// 
        ///   What's not in the update expression doesn't get updated. No where expression updates all rows. E.g:
        /// 
        ///   db.UpdateOnly(new Person { FirstName = "JJ", LastName = "Hendo" }, ev => ev.Update(p => p.FirstName));
        ///   UPDATE "Person" SET "FirstName" = 'JJ'
        /// </summary>
        [Obsolete("Use db.UpdateOnly(model, db.From<T>())")]
        public static int UpdateOnly<T>(this IDbConnection dbConn, T model, Func<SqlExpression<T>, SqlExpression<T>> onlyFields)
        {
            return dbConn.Exec(dbCmd => dbCmd.UpdateOnly(model, onlyFields));
        }

        /// <summary>
        /// Use an SqlExpression to select which fields to update and construct the where expression, E.g: 
        /// 
        ///   var q = db.From&gt;Person&lt;());
        ///   db.UpdateOnly(new Person { FirstName = "JJ" }, q.Update(p => p.FirstName).Where(x => x.FirstName == "Jimi"));
        ///   UPDATE "Person" SET "FirstName" = 'JJ' WHERE ("FirstName" = 'Jimi')
        /// 
        ///   What's not in the update expression doesn't get updated. No where expression updates all rows. E.g:
        /// 
        ///   db.UpdateOnly(new Person { FirstName = "JJ", LastName = "Hendo" }, ev.Update(p => p.FirstName));
        ///   UPDATE "Person" SET "FirstName" = 'JJ'
        /// </summary>
        public static int UpdateOnly<T>(this IDbConnection dbConn, T model, SqlExpression<T> onlyFields)
        {
            return dbConn.Exec(dbCmd => dbCmd.UpdateOnly(model, onlyFields));
        }

        /// <summary>
        /// Update record, updating only fields specified in updateOnly that matches the where condition (if any), E.g:
        /// 
        ///   db.UpdateOnly(new Person { FirstName = "JJ" }, p => p.FirstName, p => p.LastName == "Hendrix");
        ///   UPDATE "Person" SET "FirstName" = 'JJ' WHERE ("LastName" = 'Hendrix')
        ///
        ///   db.UpdateOnly(new Person { FirstName = "JJ" }, p => p.FirstName);
        ///   UPDATE "Person" SET "FirstName" = 'JJ'
        /// </summary>
        public static int UpdateOnly<T, TKey>(this IDbConnection dbConn, T obj,
            Expression<Func<T, TKey>> onlyFields = null,
            Expression<Func<T, bool>> where = null)
        {
            return dbConn.Exec(dbCmd => dbCmd.UpdateOnly(obj, onlyFields, where));
        }

        /// <summary>
        /// Use an SqlExpression to select which fields to update and construct the where expression, E.g: 
        /// Numeric fields generates an increment sql which is usefull to increment counters, etc...
        /// avoiding concurrency conflicts
        /// 
        ///   var q = db.From&gt;Person&lt;());
        ///   db.UpdateIncrement(new Person { Age = 5 }, db.From<Person>().Update(p => p.Age).Where(x => x.FirstName == "Jimi"));
        ///   UPDATE "Person" SET "Age" = "Age" + 5 WHERE ("FirstName" = 'Jimi')
        /// 
        ///   What's not in the update expression doesn't get updated. No where expression updates all rows. E.g:
        /// 
        ///   db.UpdateIncrement(new Person { Age = 5, FirstName = "JJ", LastName = "Hendo" }, ev.Update(p => p.Age));
        ///   UPDATE "Person" SET "Age" = "Age" + 5
        /// </summary>
        public static int UpdateIncrement<T>(this IDbConnection dbConn, T model, SqlExpression<T> fields)
        {
            return dbConn.Exec(dbCmd => dbCmd.UpdateIncrement(model, fields));
        }

        /// <summary>
        /// Update record, updating only fields specified in updateOnly that matches the where condition (if any), E.g:
        /// Numeric fields generates an increment sql which is usefull to increment counters, etc...
        /// avoiding concurrency conflicts
        /// 
        ///   db.UpdateIncrement(new Person { Age = 5 }, p => p.Age, p => p.LastName == "Hendrix");
        ///   UPDATE "Person" SET "Age" = "Age" + 5 WHERE ("LastName" = 'Hendrix')
        ///
        ///   db.UpdateIncrement(new Person { Age = 5 }, p => p.FirstName);
        ///   UPDATE "Person" SET "Age" = "Age" + 5
        /// </summary>
        public static int UpdateIncrement<T, TKey>(this IDbConnection dbConn, T obj,
            Expression<Func<T, TKey>> fields = null,
            Expression<Func<T, bool>> where = null)
        {
            return dbConn.Exec(dbCmd => dbCmd.UpdateIncrement(obj, fields, where));
        }

        /// <summary>
        /// Updates all non-default values set on item matching the where condition (if any). E.g
        /// 
        ///   db.UpdateNonDefaults(new Person { FirstName = "JJ" }, p => p.FirstName == "Jimi");
        ///   UPDATE "Person" SET "FirstName" = 'JJ' WHERE ("FirstName" = 'Jimi')
        /// </summary>
        public static int UpdateNonDefaults<T>(this IDbConnection dbConn, T item, Expression<Func<T, bool>> obj)
        {
            return dbConn.Exec(dbCmd => dbCmd.UpdateNonDefaults(item, obj));
        }

        /// <summary>
        /// Updates all values set on item matching the where condition (if any). E.g
        /// 
        ///   db.Update(new Person { Id = 1, FirstName = "JJ" }, p => p.LastName == "Hendrix");
        ///   UPDATE "Person" SET "Id" = 1,"FirstName" = 'JJ',"LastName" = NULL,"Age" = 0 WHERE ("LastName" = 'Hendrix')
        /// </summary>
        public static int Update<T>(this IDbConnection dbConn, T item, Expression<Func<T, bool>> where)
        {
            return dbConn.Exec(dbCmd => dbCmd.Update(item, where));
        }

        /// <summary>
        /// Updates all matching fields populated on anonymousType that matches where condition (if any). E.g:
        /// 
        ///   db.Update&lt;Person&gt;(new { FirstName = "JJ" }, p => p.LastName == "Hendrix");
        ///   UPDATE "Person" SET "FirstName" = 'JJ' WHERE ("LastName" = 'Hendrix')
        /// </summary>
        public static int Update<T>(this IDbConnection dbConn, object updateOnly, Expression<Func<T, bool>> where = null)
        {
            return dbConn.Exec(dbCmd => dbCmd.Update(updateOnly, where));
        }

        /// <summary>
        /// Flexible Update method to succinctly execute a free-text update statement using optional params. E.g:
        /// 
        ///   db.Update&lt;Person&gt;(set:"FirstName = {0}".Params("JJ"), where:"LastName = {0}".Params("Hendrix"));
        ///   UPDATE "Person" SET FirstName = 'JJ' WHERE LastName = 'Hendrix'
        /// </summary>
        public static int UpdateFmt<T>(this IDbConnection dbConn, string set = null, string where = null)
        {
            return dbConn.Exec(dbCmd => dbCmd.UpdateFmt<T>(set, where));
        }

        /// <summary>
        /// Flexible Update method to succinctly execute a free-text update statement using optional params. E.g.
        /// 
        ///   db.Update(table:"Person", set: "FirstName = {0}".Params("JJ"), where: "LastName = {0}".Params("Hendrix"));
        ///   UPDATE "Person" SET FirstName = 'JJ' WHERE LastName = 'Hendrix'
        /// </summary>
        public static int UpdateFmt(this IDbConnection dbConn, string table = null, string set = null, string where = null)
        {
            return dbConn.Exec(dbCmd => dbCmd.UpdateFmt(table, set, where));
        }

        /// <summary>
        /// Insert only fields in POCO specified by the SqlExpression lambda. E.g:
        /// <para>db.InsertOnly(new Person { FirstName = "Amy", Age = 27 }, q =&gt; q.Insert(p =&gt; new { p.FirstName, p.Age }))</para>
        /// </summary>
        [Obsolete("Use db.InsertOnly(obj, db.From<T>())")]
        public static void InsertOnly<T>(this IDbConnection dbConn, T obj, Func<SqlExpression<T>, SqlExpression<T>> onlyFields)
        {
            dbConn.Exec(dbCmd => dbCmd.InsertOnly(obj, onlyFields));
        }

        /// <summary>
        /// Using an SqlExpression to only Insert the fields specified, e.g:
        /// 
        ///   db.InsertOnly(new Person { FirstName = "Amy" }, q => q.Insert(p => new { p.FirstName }));
        ///   INSERT INTO "Person" ("FirstName") VALUES ('Amy');
        /// </summary>
        public static void InsertOnly<T>(this IDbConnection dbConn, T obj, SqlExpression<T> onlyFields)
        {
            dbConn.Exec(dbCmd => dbCmd.InsertOnly(obj, onlyFields));
        }

        /// <summary>
        /// Delete the rows that matches the where expression, e.g:
        /// 
        ///   db.Delete&lt;Person&gt;(p => p.Age == 27);
        ///   DELETE FROM "Person" WHERE ("Age" = 27)
        /// </summary>
        public static int Delete<T>(this IDbConnection dbConn, Expression<Func<T, bool>> where)
        {
            return dbConn.Exec(dbCmd => dbCmd.Delete(where));
        }

        /// <summary>
        /// Delete the rows that matches the where expression, e.g:
        /// 
        ///   db.Delete&lt;Person&gt;(ev => ev.Where(p => p.Age == 27));
        ///   DELETE FROM "Person" WHERE ("Age" = 27)
        /// </summary>
        [Obsolete("Use db.Delete(db.From<T>())")]
        public static int Delete<T>(this IDbConnection dbConn, Func<SqlExpression<T>, SqlExpression<T>> where)
        {
            return dbConn.Exec(dbCmd => dbCmd.Delete(where));
        }

        /// <summary>
        /// Delete the rows that matches the where expression, e.g:
        /// 
        ///   var q = db.From&gt;Person&lt;());
        ///   db.Delete&lt;Person&gt;(q.Where(p => p.Age == 27));
        ///   DELETE FROM "Person" WHERE ("Age" = 27)
        /// </summary>
        public static int Delete<T>(this IDbConnection dbConn, SqlExpression<T> where)
        {
            return dbConn.Exec(dbCmd => dbCmd.Delete(where));
        }

        /// <summary>
        /// Flexible Delete method to succinctly execute a delete statement using free-text where expression. E.g.
        /// 
        ///   db.Delete&lt;Person&gt;(where:"Age = {0}".Params(27));
        ///   DELETE FROM "Person" WHERE Age = 27
        /// </summary>
        public static int DeleteFmt<T>(this IDbConnection dbConn, string where = null)
        {
            return dbConn.Exec(dbCmd => dbCmd.DeleteFmt<T>(where));
        }

        /// <summary>
        /// Flexible Delete method to succinctly execute a delete statement using free-text where expression. E.g.
        /// 
        ///   db.Delete(table:"Person", where: "Age = {0}".Params(27));
        ///   DELETE FROM "Person" WHERE Age = 27
        /// </summary>
        public static int DeleteFmt(this IDbConnection dbConn, string table = null, string where = null)
        {
            return dbConn.Exec(dbCmd => dbCmd.DeleteFmt(table, where));
        }
    }
}