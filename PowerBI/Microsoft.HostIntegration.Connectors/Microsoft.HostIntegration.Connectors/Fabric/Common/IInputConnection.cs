using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200044B RID: 1099
	internal interface IInputConnection : ITransportConnection, ITransportObject
	{
		// Token: 0x0600267D RID: 9853
		IInputContext Receive();

		// Token: 0x0600267E RID: 9854
		IInputContext Receive(TimeSpan timeout);

		// Token: 0x0600267F RID: 9855
		IAsyncResult BeginReceive(AsyncCallback callback, object state);

		// Token: 0x06002680 RID: 9856
		IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06002681 RID: 9857
		IInputContext EndReceive(IAsyncResult ar);
	}
}
