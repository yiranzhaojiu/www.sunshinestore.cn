using Newtonsoft.Json;
using Store.BaseDTO.WXDTO;
using Store_Framework_Domain.Event;
using Strore_Common.Utility;
using Strore_Common.WxHelper;
using SunShineStore.WxCommonDTO;
using SunShineStore.WxCommonService.Impl.Event.EventData;
using SunShineStore.WxCommonService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SunShineStore.WxCommonService.Impl
{
    public class WxBottomMenuServices : IWxBottomMenuServices
    {
        private static string MenuPath = CommonHelper.GetConfigData("BottomMenuPath");

        /// <summary>
        /// 更新底部菜单
        /// </summary>
        public WxBaseDTO UpdateBottomMen()
        {
            string posturl = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", WxHelper.WxAccessToken);
            var MenuStr = GetBottomMenuStr();
            var str= HttpHelper.HttpPost(posturl, MenuStr);
            return JsonConvert.DeserializeObject<WxBaseDTO>(str); 
        }
         
        /// <summary>
        /// 删除底部菜单
        /// </summary>
        public WxBaseDTO DeleteBottomMenu()
        {
            string posturl = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}";
            posturl = string.Format(posturl, WxHelper.WxAccessToken);
            var str = HttpHelper.HttpGet(posturl);
            return JsonConvert.DeserializeObject<WxBaseDTO>(str); 
        }

        /// <summary>
        /// 获取Menu数据
        /// </summary>
        /// <returns></returns>
        public ReturnMenuDTO GetMenuData()
        {

            DomainEvent.Publish<OrderPayFinshNotify>(new OrderPayFinshNotify()
            {
                OrderID=11,
                OrderTime=DateTime.Now
            });

            var list = new List<WxBottomMenuDTO>(); 
            var xmltabs = XElement.Load(MenuPath);
            var Tablist = xmltabs.Elements("Tabs").FirstOrDefault().Elements("Tab"); 
            int i = 1;
            foreach (var xmltab in Tablist)
            {
                var model = new WxBottomMenuDTO() {
                    type = i,
                    Menutext = xmltab.Attribute("text").Value,
                    MenuUrl="", 
                    SortIndex=0, 
                }; 

                if (xmltab.Elements().Count()>0)
                {
                    int j = 1; 
                    foreach (var menu in xmltab.Elements("Menu"))
                    {
                        var childrenModel = new WxBottomMenuDTO()
                        { 
                            SortIndex=j,
                            type = i,
                            Menutext = menu.Attribute("text").Value,
                            MenuUrl = menu.Value
                        };
                        j++;
                        list.Add(childrenModel);
                    }
                }
                else
                {
                    model.MenuUrl = xmltab.Value;
                }
                list.Add(model);
                i++;
            }

            var retrun = new ReturnMenuDTO();
            retrun.FirstMenu = list.Where(r => r.type == 1).OrderBy(r => r.SortIndex).ToList();
            retrun.TwoMenu = list.Where(r => r.type == 2).OrderBy(r => r.SortIndex).ToList();
            retrun.ThreeMenu = list.Where(r => r.type == 3).OrderBy(r => r.SortIndex).ToList(); 
            return retrun;
        }
        /// <summary>
        /// 更新Menu数据
        /// </summary>
        /// <returns></returns>
        public WxBaseDTO UpdateMenuData(ReturnMenuDTO dto)
        { 
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(MenuPath);
                XmlNode xmldocRoot = xmlDoc.SelectSingleNode("root");
                xmldocRoot.RemoveAll();
                XmlElement Tabs = xmlDoc.CreateElement("Tabs"); //添加Tabs节点 

                List<WxBottomMenuDTO> list = new List<WxBottomMenuDTO>(); 
                if (dto.FirstMenu != null)
                {
                    list = list.Union(dto.FirstMenu).ToList();
                } 
                if (dto.TwoMenu != null)
                {
                    list = list.Union(dto.TwoMenu).ToList();
                } 
                if (dto.ThreeMenu != null)
                {
                    list = list.Union(dto.ThreeMenu).ToList();
                } 
                if (list.Count <= 0)
                {
                    return new WxBaseDTO()
                    {
                        errcode = "505",
                        errmsg = "数据提交异常"
                    }; 
                }

                list= list.Where(r => r.Menutext != null).OrderBy(r => r.type).ToList();

                var ListType = list.Select(r => r.type).GroupBy(r => r);

                foreach (var type in ListType)
                {
                    var tablist = list.Where(r => r.type == type.Key).OrderBy(r => r.SortIndex);
                    XmlElement Tab = xmlDoc.CreateElement("Tab"); //添加Tab节点 
                    Tab.SetAttribute("text", tablist.FirstOrDefault().Menutext);
                    if (tablist.Count() > 1)
                    {
                        foreach (var item in tablist.Where(r => r.SortIndex != 0))
                        {
                            XmlElement Menu = xmlDoc.CreateElement("Menu"); //添加Menu节点 
                            Menu.SetAttribute("text", item.Menutext);
                            Menu.InnerText = item.MenuUrl;
                            Tab.AppendChild(Menu);
                        }
                    }
                    else
                    {
                        Tab.InnerText = tablist.FirstOrDefault().MenuUrl;
                    }

                    Tabs.AppendChild(Tab);
                }
                xmldocRoot.AppendChild(Tabs);//添加到<root>节点中 
                xmlDoc.Save(MenuPath); 
                return  UpdateBottomMen();
            }
            catch(Exception ex)
            {
                return new WxBaseDTO()
                {
                    errcode = "505",
                    errmsg = ex.Message
                }; 
            }
        }
        
        /// <summary>
        /// 获取XML配置
        /// </summary>
        /// <returns></returns>
        private string GetBottomMenuStr()
        {
            StringBuilder str = new StringBuilder(500);
            str.Append("{\"button\":[");
            var xmltabs = XElement.Load(MenuPath); 
            var Tablist = xmltabs.Elements("Tabs").FirstOrDefault().Elements("Tab");
            foreach (var xmltab in Tablist)
            { 
                var name = xmltab.Attribute("text").Value;
                str.Append("{"); 
                str.AppendFormat("\"name\":\"{0}\",",name); 
                if (xmltab.Elements().Count()>0)
                {
                    str.Append("\"sub_button\":["); 
                    foreach (var menu in xmltab.Elements("Menu"))
                    { 
                        str.Append("{\"type\":\"view\",\"name\":\"" + menu.Attribute("text").Value + "\",\"url\":\"" + menu.Value + "\"}");
                        if (menu != xmltab.Elements("Menu").Last())
                        {
                            str.Append(",");
                        }
                    }
                    str.Append("]");
                }
                else
                {
                    str.Append("\"type\":\"view\",\"url\":\""+xmltab.Value+"\"");
                    
                }
                str.Append("}");
                if (xmltab != Tablist.Last())
                {
                    str.Append(",");
                }
            }
            str.Append("]}");
            return str.ToString();
        }
    }
}
