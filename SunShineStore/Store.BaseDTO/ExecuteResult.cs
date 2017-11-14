using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Store.BaseDTO
{
    /// <summary>
    /// 方法执行的结果
    /// <para>方便在仅返回成功或失败执行，而不暴漏异常信息的方法上</para>
    /// </summary> 
    public class ExecuteResult: BaseDTO
    {
        /// <summary>
        /// 失败原因的分类
        /// </summary>　
        public EnumFailType FailType { get; set; }

    }

    /// <summary>
    /// 失败原因的分类
    /// </summary>　
    public enum EnumFailType
    {
        Success = 0, //枚举值为0的项要定义，否则wcf数据传输时会出错

        /// <summary>
        /// 参数校验失败
        /// </summary>
        //[Description("参数校验失败")]　
        InvalidArgument = 1,

        /// <summary>
        /// 执行过程出现异常
        /// </summary>
        //[Description("执行过程出现异常")]　 
        ExecuteException = 2,

        /// <summary>
        /// 执行超时
        /// </summary>
        //[Description("执行超时")]　 
        ExecuteTimeout = 3,

        /// <summary>
        /// 没有权限
        /// </summary>
        //[Description("没有权限")]
        NoAuth = 4,

        /// <summary>
        /// 业务规则校验失败
        /// </summary>
        //[Description("业务规则校验失败")]
        FailOnBizConstraint = 5,

        /// <summary>
        /// 批量操作时局部数据失败
        /// </summary>
        //[Description("批量操作时局部数据失败")]
        TopicalFail = 6,

        /// <summary>
        /// 被锁定 
        /// </summary>
        HasLocked = 7,

        /// <summary>
        /// 当前记录值已存在
        /// </summary>
        Exists = 8,
    }

    /// <summary>
    /// 方法执行的结果(带有实际返回结果)
    /// <para>方便在仅返回成功或失败执行，而不暴漏异常信息的方法上</para>
    /// </summary>
    /// <typeparam name="T">实际返回的类型（比如long、string[]、RoomDto等）</typeparam>
    [DataContract] 
    public class ExecuteResult<T> : ExecuteResult
    {
        /// <summary>
        /// 执行成功时，返回结果
        /// </summary>
        public T ReturnValue { get; set; }
    }
}
