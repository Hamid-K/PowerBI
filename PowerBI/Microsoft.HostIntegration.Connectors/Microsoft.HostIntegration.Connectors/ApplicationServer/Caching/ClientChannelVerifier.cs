using System;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002A2 RID: 674
	internal class ClientChannelVerifier
	{
		// Token: 0x060018DB RID: 6363 RVA: 0x0004ACD0 File Offset: 0x00048ED0
		internal ClientChannelVerifier(object verifyObject, VerifyResponseCallback verifyCallback)
		{
			this._verifyObject = verifyObject;
			this._verifyCallback = verifyCallback;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0004ACF4 File Offset: 0x00048EF4
		internal OperationResult Verify(IChannelContainer channelContainer, TimeSpan sendTimeout, TimeSpan receiveTimeout, out ServerInformation serverInfo)
		{
			serverInfo = null;
			using (Message message = Utility.CreateMessage("http://schemas.microsoft.com/velocity/msgs/VerificationAction", this._verifyObject))
			{
				channelContainer.Channel.Send(message, sendTimeout);
			}
			Message message2 = channelContainer.Channel.Receive(receiveTimeout);
			if (message2 != null)
			{
				OperationResult operationResult;
				if (string.Equals(message2.Headers.Action, "http://schemas.microsoft.com/velocity/msgs/ErrorNotificationAction"))
				{
					ResponseBody response = ResponseBody.GetResponse(message2);
					DataCacheException ex = Utility.CreateClientException(response.ResponseCode, this._logSource);
					Utility.AddInformationToException(ex, response.ResponseCode, response.Value);
					operationResult = new OperationResult(OperationStatus.AsyncFailureReceived, ex);
				}
				else
				{
					ClientVersionInfo body = message2.GetBody<ClientVersionInfo>();
					channelContainer.RemoteVersionInfo = body;
					operationResult = ((this._verifyCallback == null || this._verifyCallback(body, out serverInfo)) ? OperationResult.Success : OperationResult.VerificationFailed);
				}
				message2.Close();
				return operationResult;
			}
			ReleaseAssert.IsTrue(channelContainer.Channel.State == CommunicationState.Closing || channelContainer.Channel.State == CommunicationState.Closed || channelContainer.Channel.State == CommunicationState.Faulted);
			throw new CommunicationObjectFaultedException(string.Format(CultureInfo.InvariantCulture, "Channel already in {0} state.", new object[] { channelContainer.Channel.State }));
		}

		// Token: 0x04000D84 RID: 3460
		private string _logSource = "DistributedCache.ClientChannelVerifier";

		// Token: 0x04000D85 RID: 3461
		private object _verifyObject;

		// Token: 0x04000D86 RID: 3462
		private VerifyResponseCallback _verifyCallback;
	}
}
