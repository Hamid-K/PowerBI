using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000454 RID: 1108
	internal interface ISessionTransportFactory
	{
		// Token: 0x060026BC RID: 9916
		IOutputSession CreateOutputSession(Uri remoteAddress);

		// Token: 0x060026BD RID: 9917
		ISessionListener CreateSessionListener(Uri listenerAddress);

		// Token: 0x060026BE RID: 9918
		IMessageListener CreateMessageListener(Uri listenerAddress);

		// Token: 0x060026BF RID: 9919
		bool IsRemoteDownException(Exception e);
	}
}
