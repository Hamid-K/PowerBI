using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000448 RID: 1096
	internal interface IMessageListener : IListener, ITransportObject
	{
		// Token: 0x0600265C RID: 9820
		void RegisterFilter(string actionPrefix, ProcessReceivedMessage callback, object state);

		// Token: 0x0600265D RID: 9821
		bool UnregisterFilter(string actionPrefix);

		// Token: 0x0600265E RID: 9822
		void CloseInputConnection(IInputContext inputContext);
	}
}
