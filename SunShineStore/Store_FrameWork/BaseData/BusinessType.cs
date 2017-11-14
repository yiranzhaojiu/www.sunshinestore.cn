using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_FrameWork.BaseData
{
    public enum BusinessType
    {
        //杂志的读权限
        BusinessMgRead,
        //杂志的写权限
        BusinessMgWrite,
        //UserInfo读权限
        UserInfoRead,
        //UserInfo写权限
        UserInfoWrite,
    }
}
