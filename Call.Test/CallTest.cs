using System;
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
            #region arrange
            CallItem expected = new CallItem(1234, 0, "myUrl", "myPayload", "myResponse", "myInternalParameters");
            Mock<ICall> mock_Call = new Moq.Mock<ICall>();
            mock_Call.Setup(x => x.doCall(It.IsAny<object>(), It.IsAny<CallMethod>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expected);
            #endregion

            #region act
            CallItem actual = mock_Call.Object.doCall(1234, CallMethod.GET, "myUrl", "myPayload");
            #endregion

            #region assert
            Assert.AreEqual(expected.code, actual.code);
            Assert.AreEqual(expected.endPoint, actual.endPoint);
            Assert.AreEqual(expected.id, actual.id);
            Assert.AreEqual(expected.internal_parameters, actual.internal_parameters);
            Assert.AreEqual(expected.payload, actual.payload);
            Assert.AreEqual(expected.response, actual.response);
            #endregion
        }

        [TestMethod]
        public void RealGetTest()
        {
            #region arrange
            string endPoint = @"https://www.google.com";
            object id = "1234";
            CallMethod callMethod = CallMethod.GET;
            CallItem expected = new CallItem(id, 0, endPoint, null, "", null);
            #endregion

            #region act
            ICall call = new HttpCall();
            CallItem actual=call.doCall(id, callMethod, endPoint);
            #endregion

            #region assert
            Assert.AreEqual(expected.code, actual.code);
            Assert.AreEqual(expected.endPoint, actual.endPoint);
            Assert.AreEqual(expected.id, actual.id);
            Assert.AreEqual(expected.internal_parameters, actual.internal_parameters);
            Assert.AreEqual(expected.payload, actual.payload);
            Assert.IsNotNull(actual.response);
            #endregion
        }
    }
}
