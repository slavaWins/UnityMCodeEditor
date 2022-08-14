using MCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCoder
{
    public class MC_DocHelpData{
        public string title;
        public string descr;
    }

    internal static class MC_DocHelp
    {

        public static MC_DocHelpData GetInfoByLinkType(MC_Value_LinkType linkType)
        {
            switch (linkType)
            {
                case MC_Value_LinkType._none:
                    return new MC_DocHelpData()
                    {
                        descr = "Переменная внутри функции",
                    };
                    break;
                case MC_Value_LinkType._input:

                    return new MC_DocHelpData()
                    {
                        descr = "Допустим мы подключили скрипт к какому-то объекту. В том месте где мы подключаем скрипт, появится эта переменная, и можно указать данные.",
                    };
                    break;

                case MC_Value_LinkType._event:

                    return new MC_DocHelpData()
                    {
                        descr = "Когда происходит событие, переменные этого типа создаются сами. Например если событие Клик - тогда предут данные о том кто кликнул",
                    };
                    break;

                case MC_Value_LinkType._custom:

                    return new MC_DocHelpData()
                    {
                        descr = "Мы можем завести свои собственные переменные, что бы хранить в них данные",
                    };
                    break;

                case MC_Value_LinkType._save:

                    return new MC_DocHelpData()
                    {
                        descr = "Сохраняемая переменная. Предмет или блок будут сохранять это между перезапусками серевера",
                    };
                    break;
                default: return new MC_DocHelpData()
                {
                    descr = "Не определенно"
                };
                         
            }


        }
    }
}
