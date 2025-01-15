using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000455 RID: 1109
	internal interface IDatagramTransportFactory
	{
		// Token: 0x060026C0 RID: 9920
		DatagramConnection CreateDatagramConnection(Uri remoteAddress);

		// Token: 0x060026C1 RID: 9921
		IMessageListener CreateMessageListener(Uri listenerAddress);
	}
}
