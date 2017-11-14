using Store.BaseDTO;
using Store_FrameWork.BaseData;
using Strore_Data;
using SunShineStore.Customer.Interface;
using SunShineStore.CustomerDTO;
using SunShineStore.CustomerEntity;
using SunStore_Web.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SunShineStore.Customer.Impl
{
    public class UserServices: IUserServices
    {
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecuteResult UserInfoAdd(UserInfoDTO model)
        {
            var result = new ExecuteResult()
            {
                IsSuccess = false,
                FailType = EnumFailType.FailOnBizConstraint,
                ErrorMsg = "手机号格式不正确"
            };

            if (model == null || string.IsNullOrEmpty(model.tel))
            {
                result.FailType = EnumFailType.InvalidArgument;
                result.ErrorMsg = "参数校验失败";
                return result;
            }

            List<string> list = new List<string>();
            //中国移动
            list.Add(@"^(?:0?1)((?:3[56789]|5[0124789]|8[278])\d|34[0-8]|47\d)\d{7}$");
            //中国联通
            list.Add(@"^(?:0?1)(?:3[012]|4[5]|5[356]|8[356]\d|349)\d{7}$");
            //中国电信
            list.Add(@"^(?:0?1)(?:33|53|8[079])\d{8}$");
            //中国大陆
            list.Add(@"^(?:0?1)[34587]\d{9}$");
            //固话
            list.Add(@"^(([0\+]\d{2,3}-)?(0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$");

            var flag = false;

            for (int k = 0; k < list.Count; k++)
            {
                if (Regex.IsMatch(model.tel, list[k]))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                return result;


            var entity = DbUtility.GetSingle<UserInfoEntity>(r => r.tel == model.tel);

            if (entity != null)
            {
                result.FailType = EnumFailType.Exists;
                result.ErrorMsg = "当前记录值已存在";
                return result;
            }

            var i = DbUtility.Add(new UserInfoEntity()
            {
                tel = model.tel,
                name = model.name
            }, true);

            if (i > 0)
            {
                result.IsSuccess = true;
                result.FailType = EnumFailType.Success;
                result.ErrorMsg = "ok";
            }
            else
            {
                result.IsSuccess = false;
                result.FailType = EnumFailType.ExecuteException;
                result.ErrorMsg = "执行失败";
            }
            return result;
        }

        /// <summary>
        /// 查找用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecuteResult<List<UserInfoDTO>> GetList(UserSerachDTO model)
        {
            var result = new ExecuteResult<List<UserInfoDTO>>()
            {
                IsSuccess = false,
                FailType = EnumFailType.InvalidArgument,
                ErrorMsg = "参数错误",
                ReturnValue = new List<UserInfoDTO>()
            };

            var exp = DbUtility.GetGetSqlExpression<UserInfoEntity>();
            var userTable=TableManager.GetTableName(BaseTable.Users);
            int PageIndex = 0;
            int PageSize = 0;
            if (model != null)
            {
                if (string.IsNullOrEmpty(exp.WhereExpression))
                {
                    exp.WhereExpression += " WHERE 1=1 ";
                }

                if (!string.IsNullOrEmpty(model.Name))
                {
                    exp.WhereExpression +=string.Format(" AND {0}.name like '%{1}%'", userTable,model.Name);
                }
                if (!string.IsNullOrEmpty(model.Tel))
                {
                    exp.WhereExpression += string.Format(" AND {0}.tel like '%{1}%'", userTable, model.Tel);
                }

                if (model.PageIndx > 0 && model.PageSize > 0)
                {
                    PageIndex = model.PageIndx;
                    PageSize = model.PageSize;
                }
            }
            exp.OrderByDescending(r=>r.ID);

            var list = DbUtility.GetList<UserInfoDTO, UserInfoEntity>(exp, PageIndex, PageSize);
            result.IsSuccess = true;
            result.FailType = EnumFailType.Success;
            result.ErrorMsg = "ok";
            result.ReturnValue = list;
            return result;
        }
    }
}
