using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call
{
    public interface ICall
    {
        CallItem doCall(object id, CallMethod _CallMethod, string endPoint, string payload = null);

        Task<CallItem> doCallAsync(object id, CallMethod _CallMethod, string endPoint, string payload = null);
    }
}
