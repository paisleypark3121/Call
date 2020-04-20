using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call
{
    public class SimpleParser : IResponseParser
    {
        Dictionary<string, object> IResponseParser.parse(string response)
        {
            if (string.IsNullOrEmpty(response))
                return null;

            return new Dictionary<string, object> {
                { "Response", response }
            };
        }
    }
}
