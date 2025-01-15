using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004BC RID: 1212
	public class ClientMessageInspector : IClientMessageInspector
	{
		// Token: 0x06002518 RID: 9496 RVA: 0x0008403C File Offset: 0x0008223C
		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			MessageHeaders headers = request.Headers;
			SerializedActivity serializedActivity = SerializedActivity.FromActivity(UtilsContext.Current.Activity);
			MessageHeader messageHeader = MessageHeader.CreateHeader(AddContextToHeaderEndpointBehavior.ACTIVITY_HEADER_NAME, AddContextToHeaderEndpointBehavior.ACTIVITY_HEADER_NAMESPACE, serializedActivity);
			if (UtilsContext.Current.TracingForced)
			{
				MessageHeader messageHeader2 = MessageHeader.CreateHeader(AddContextToHeaderEndpointBehavior.FORCE_TRACES_HEADER_NAME, AddContextToHeaderEndpointBehavior.FORCE_TRACES_HEADER_NAMESPACE, true);
				headers.Add(messageHeader2);
			}
			headers.Add(messageHeader);
			return null;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AfterReceiveReply(ref Message reply, object correlationState)
		{
		}
	}
}
