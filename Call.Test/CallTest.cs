using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Call.Test
{
    [TestClass]
    public class CallTest
    {
        [TestMethod]
        public void MockCallTest()
        {
            //#region arrange
            //Dictionary<string, object> expected = new Dictionary<string, object>
            //{
            //    {"Response","Hello World!" }
            //};
            //Mock<ICall> mock_Call = new Moq.Mock<ICall>();
            //IResponseParser parser = new SimpleParser();
            //mock_Call.Setup(x => x.doCall(
            //    It.IsAny<object>(), 
            //    It.IsAny<CallMethod>(), 
            //    It.IsAny<CallContentType>(), 
            //    It.IsAny<string>(), 
            //    It.IsAny<IResponseParser>(),
            //    It.IsAny<string>())).Returns(expected);
            //#endregion

            //#region act
            //Dictionary<string, object> actual = mock_Call.Object.doCall(1234, CallMethod.GET, CallContentType.plain, "myUrl", parser, "myPayload");
            //#endregion

            //#region assert
            //Assert.IsNotNull(actual);
            //Assert.IsTrue(actual.Count == 1);
            //Assert.IsTrue(actual.ContainsKey("Response"));
            //Assert.AreEqual(expected["Response"],actual["Response"]);
            //#endregion
        }

        [TestMethod]
        public void RealGetTest()
        {
            //#region arrange
            //string endPoint = @"https://www.google.com";
            //object id = "1234";
            //CallMethod callMethod = CallMethod.GET;
            //CallContentType callContentType = CallContentType.plain;
            //#endregion

            //#region act
            //ICall call = new HttpCall();
            //IResponseParser parser = new SimpleParser();
            //Dictionary<string, object> actual = call.doCall(id, callMethod, callContentType, endPoint,parser);
            //#endregion

            //#region assert
            //Assert.IsNotNull(actual);
            //Assert.IsTrue(actual.Count == 1);
            //Assert.IsTrue(actual.ContainsKey("Response"));
            //#endregion
        }

        [TestMethod]
        public void JsonPostTest()
        {
            #region arrange
            DateTime dateTime = new DateTime(2020, 8, 31, 13, 23, 10);
            long timestamp = 1598873142327;
            DateTime dateTime_ts = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime.ToLocalTime();

            string alpha = "12,34";
            string beta = "56,78";
            string body = "[{\"datetime\":\"" + dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "\"," +
                              "\"datetime_ts\":\"" + dateTime_ts.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "\"," +
                              "\"alpha\":" + alpha.Replace(",", ".") + "," +
                              "\"beta\":" + beta.Replace(",", ".")+ "}]";

            string endPoint = @"https://postman-echo.com/post";
            object id = "1234";
            CallMethod callMethod = CallMethod.POST;
            CallContentType callContentType = CallContentType.json;
            #endregion

            #region act
            ICall call = new HttpCall();
            IResponseParser parser = new SimpleParser();
            Dictionary<string, object> actual = call.doCall(id, callMethod, callContentType, endPoint, parser,body);
            #endregion

            #region assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.ContainsKey("Response"));
            #endregion
        }
    }
}
