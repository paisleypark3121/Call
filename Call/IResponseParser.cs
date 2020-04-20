using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call
{
    public interface IResponseParser
    {
        Dictionary<string,object> parse(string response);
    }
}
