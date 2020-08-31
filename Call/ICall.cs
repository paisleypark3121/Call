using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call
{
    public interface ICall
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_CallMethod"></param>
        /// <param name="_CallContentType"></param>
        /// <param name="endPoint"></param>
        /// <param name="responseParser"></param>
        /// <param name="payload"></param>
        /// <param name="parameters">Key: ByPassCert - value: it is not significant</param>
        /// <returns></returns>
        Dictionary<string,object> doCall(
            object id, 
            CallMethod _CallMethod, 
            CallContentType _CallContentType, 
            string endPoint, 
            IResponseParser responseParser,            
            string payload = null,
            Dictionary<string, object> parameters = null
            );

        Task<Dictionary<string, object>> doCallAsync(
            object id, 
            CallMethod _CallMethod, 
            CallContentType _CallContentType, 
            string endPoint,
            IResponseParser responseParser,
            string payload = null,
            Dictionary<string, object> parameters=null
            );
    }
}
