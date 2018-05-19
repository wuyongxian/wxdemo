using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using wxPlatForm;

namespace wxweb
{
    public class eventFunc
    {
        public static void loadFunc(EventMessage em) {
          
            switch (em.EventType) {
                 
                case EventType.SUBSCRIBE:
                    subscribeEvent(em);
                    break;
                case EventType.CLICK:
                    DataTable dtwxMsg = DAL.wxDAL.wxMsgDAL.Get_wxmsmResponse_key(em.EventKey);
                    wxPlatForm.ReceiveMessage.WriteXML(em, dtwxMsg);
                    //eventSub.Create(em);
                    break;
            }
        }

        public static void subscribeEvent(EventMessage em) {
            eventSubscribe eventSub = new eventSubscribe();
            //保存关注者信息
            eventSub.Create(em);
            //发送关注消息
            DataTable dtwxMsg = DAL.wxDAL.wxMsgDAL.Get_wxmsmResponse_key("subscribe");
            wxPlatForm.ReceiveMessage.WriteXML(em, dtwxMsg);
        }
    }
}