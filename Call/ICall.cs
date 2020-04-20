using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call
{
    public interface ICall
    {
        Dictionary<string,object> doCall(
            object id, 
            CallMethod _CallMethod, 
            CallContentType _CallContentType, 
            string endPoint, 
            IResponseParser responseParser,
            string payload = null);

        Task<Dictionary<string, object>> doCallAsync(
            object id, 
            CallMethod _CallMethod, 
            CallContentType _CallContentType, 
            string endPoint,
            IResponseParser responseParser,
            string payload = null);
    }
}
