using LightInject;
using Strore_Common.Bootstrapper;
using SunShineStore.WxCommonService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store_Framework_Domain.Extensions;

namespace SunShineStore.WxCommonService.Impl
{
    public class StoreRegisterServiceTask : RegisterServiceBootstrapperTask
    {
        public StoreRegisterServiceTask(IServiceContainer container)
            : base(container)
        {

        }

        public override TaskContinuation Execute()
        {
            //注册依赖关系
            container.Register<IWxServerConfigServices, WxServerConfigServices>(new PerContainerLifetime());
            container.Register<IWxLoginServices, WxLoginServices>(new PerContainerLifetime());
            container.Register<IWxWeChatPublicPayServices, WxWeChatPublicPayServices>(new PerContainerLifetime());
            container.Register<IWxBottomMenuServices, WxBottomMenuServices>(new PerContainerLifetime()); 
            //注册领域事件-（事件总线，记录事件源与事件处理的映射）
            container.RegisterEventHandlers(this.GetType().Assembly);

            return TaskContinuation.Continue;
        }
    }
}
