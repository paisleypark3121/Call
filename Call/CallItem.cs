using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call
{
    public class CallItem
    {
        public object id { get; set; }
        public int code { get; set; }
        public string endPoint { get; set; }
        public string payload { get; set; }
        public object response { get; set; }
        public string internal_parameters { get; set; }

        public CallItem(object _id, int _code, string _endPoint, string _payload, object _response, string _internal_parameters)
        {
            id = _id;
            code = _code;
            endPoint = _endPoint;
            payload = _payload;
            response = _response;
            internal_parameters = _internal_parameters;
        }

        public override string ToString()
        {
            string _response = "id: " + id.ToString();
            _response += " - code: " + code;
            if (!string.IsNullOrEmpty(endPoint)) _response += " - endPoint: " + endPoint;
            if (!string.IsNullOrEmpty(payload)) _response += " - payload: " + payload;
            if (response != null) _response += " - response: " + response.ToString();
            if (!string.IsNullOrEmpty(internal_parameters)) _response += " - internal_parameters: " + internal_parameters;

            return _response;
        }
    }
}
