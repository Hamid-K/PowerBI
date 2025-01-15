using System;
using System.ServiceModel;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x0200065C RID: 1628
	[ServiceContract]
	public interface ILiveTracing
	{
		// Token: 0x0600363F RID: 13887
		[OperationContract]
		int ContactLiveTracing();

		// Token: 0x06003640 RID: 13888
		[OperationContract]
		int SendTraceLines(int writerNumber, string[] traceLines);
	}
}
