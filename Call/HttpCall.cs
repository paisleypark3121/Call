using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Call
{
    public class HttpCall : ICall
    {
        public Dictionary<string,object> doCall(
            object id, 
            CallMethod _CallMethod, 
            CallContentType _CallContentType, 
            string url, 
            IResponseParser parser,
            string body = null,
            Dictionary<string, object> parameters = null)
        {
            #region variables
            string endPoint = url;
            string payload = body;
            #endregion
            #region try
            try
            {
                #region preconditions
                if (id == null)
                    id = DateTime.Now.Ticks + "_" + Random.generate(10000, 99999);
                if (string.IsNullOrEmpty(url))
                    throw new Exception("Missing mandatory parameters");
                if ((_CallMethod == CallMethod.POST) && (string.IsNullOrEmpty(body)))
                    throw new Exception("Missing mandatory parameters");
                #endregion

                #region endPoint / payload
                if ((_CallMethod == CallMethod.GET) && (url.Contains("?")))
                {
                    int index = url.IndexOf("?");
                    endPoint = url.Substring(0, index);
                    payload = url.Substring(index + 1);
                } 
                else if ((_CallMethod == CallMethod.GET) && (!string.IsNullOrEmpty(body)))
                {
                    url += "?" + body;
                    body = null;
                }
                #endregion

                #region parameters
                if (parameters != null)
                {
                    if (parameters.ContainsKey("ByPassCert"))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new
                        RemoteCertificateValidationCallback
                        (
                              delegate { return true; }
                        );
                    }
                }
                #endregion

                #region request
                HttpWebRequest _WebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                _WebRequest.ServicePoint.ConnectionLimit = 1000;
                _WebRequest.Timeout = 5000; _WebRequest.ReadWriteTimeout = 10000;
                _WebRequest.Method = _CallMethod.ToString();
                _WebRequest.ContentType = (!string.IsNullOrEmpty(CallContentTypeUtility.getCallContentType(_CallContentType))) ? 
                    CallContentTypeUtility.getCallContentType(_CallContentType) : 
                    CallContentTypeUtility.getCallContentType(CallContentType.plain);
                _WebRequest.AllowAutoRedirect = false;

                if (_CallMethod == CallMethod.POST)
                {
                    byte[] data = Encoding.ASCII.GetBytes(body);
                    _WebRequest.ContentLength = data.Length;
                    Stream requestStream = _WebRequest.GetRequestStream();
                    requestStream.Write(data, 0, data.Length);
                    requestStream.Close();
                }
                #endregion

                #region response
                string _response = null;
                HttpWebResponse response = (HttpWebResponse)_WebRequest.GetResponse();
                Stream responseStream = response.GetResponseStream();
                using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default))
                    _response = myStreamReader.ReadToEnd();
                responseStream.Close();
                response.Close();
                #endregion

                return parser.parse(_response);
            }
            #endregion
            #region catch
            catch (Exception ex)
            {
                string error = "Error in function " + this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name + " - " + ex.Message;
                System.Diagnostics.Trace.TraceError(error);
                return new Dictionary<string, object>()
                {
                    {"Error",error },
                };
            }
            #endregion
        }

        public async Task<Dictionary<string,object>> doCallAsync(
            object id, 
            CallMethod _CallMethod, 
            CallContentType _CallContentType, 
            string url,
            IResponseParser parser,
            string body = null,
            Dictionary<string, object> parameters = null)
        {
            #region variables
            string endPoint = url;
            string payload = body;
            #endregion
            #region try
            try
            {
                #region preconditions
                if (id == null)
                    id = DateTime.Now.Ticks + "_" + Random.generate(10000, 99999);
                if (string.IsNullOrEmpty(url))
                    throw new Exception("Missing mandatory parameters");
                if ((_CallMethod == CallMethod.POST) && (string.IsNullOrEmpty(body)))
                    throw new Exception("Missing mandatory parameters");
                #endregion

                #region endPoint / payload
                if ((_CallMethod == CallMethod.GET) && (url.Contains("?")))
                {
                    int index = url.IndexOf("?");
                    endPoint = url.Substring(0, index);
                    payload = url.Substring(index + 1);
                }
                else if ((_CallMethod == CallMethod.GET) && (!string.IsNullOrEmpty(body)))
                {
                    url += "?" + body;
                    body = null;
                }
                #endregion

                #region parameters
                if (parameters != null)
                {
                    if (parameters.ContainsKey("ByPassCert"))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new
                        RemoteCertificateValidationCallback
                        (
                              delegate { return true; }
                        );
                    }
                }
                #endregion

                #region request
                HttpWebRequest _WebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                _WebRequest.Method = _CallMethod.ToString();
                _WebRequest.ServicePoint.ConnectionLimit = 1000;
                _WebRequest.Timeout = 5000; _WebRequest.ReadWriteTimeout = 10000;
                _WebRequest.ContentType = (!string.IsNullOrEmpty(CallContentTypeUtility.getCallContentType(_CallContentType))) ?
                    CallContentTypeUtility.getCallContentType(_CallContentType) :
                    CallContentTypeUtility.getCallContentType(CallContentType.plain);
                _WebRequest.AllowAutoRedirect = false;

                if (_CallMethod == CallMethod.POST)
                {
                    byte[] data = Encoding.ASCII.GetBytes(body);
                    _WebRequest.ContentLength = data.Length;
                    Stream requestStream = _WebRequest.GetRequestStream();
                    requestStream.Write(data, 0, data.Length);
                    requestStream.Close();
                }
                #endregion

                #region response
                string _response = null;
                var response = await _WebRequest.GetResponseAsync();
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default))
                    {
                        _response = await myStreamReader.ReadToEndAsync();
                    }
                }
                #endregion

                return parser.parse(_response);
            }
            #endregion
            #region catch
            catch (Exception ex)
            {
                string error = "Error in function " + this.GetType().Name + "." + GetActualAsyncMethodName() + " - " + ex.Message;
                System.Diagnostics.Trace.TraceError(error);
                return null;
            }
            #endregion
        }

        public static string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;
    }
}
