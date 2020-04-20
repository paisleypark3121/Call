using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call
{
    public enum CallContentType
    {
        plain,
        xml,
        json
    }

    public class CallContentTypeUtility
    {
        public static string getCallContentType(CallContentType type)
        {
            if (type == CallContentType.plain)
                return "text/plain";
            if (type == CallContentType.xml)
                return "text/xml";
            if (type == CallContentType.json)
                return "application/json";

            return null;
        }
    }

}
