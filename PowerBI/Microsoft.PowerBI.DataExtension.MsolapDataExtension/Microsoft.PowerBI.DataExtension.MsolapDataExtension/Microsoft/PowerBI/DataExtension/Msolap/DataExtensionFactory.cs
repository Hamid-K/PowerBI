using System;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.DataExtension.Msolap
{
	// Token: 0x0200000D RID: 13
	public static class DataExtensionFactory
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002811 File Offset: 0x00000A11
		public static IConnectionFactory CreateDefaultConnectionFactory(ITracer tracer, IPrivateInformationService piiService, bool enableMsolapTracing = false)
		{
			return new ConnectionFactory(tracer, piiService, enableMsolapTracing);
		}
	}
}
