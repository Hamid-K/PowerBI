using System;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.PowerBI.DataExtension.Msolap
{
	// Token: 0x0200000A RID: 10
	public sealed class ConnectionFactory : IConnectionFactory
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000025C6 File Offset: 0x000007C6
		public ConnectionFactory(ITracer tracer, IPrivateInformationService piiService)
			: this(tracer, piiService, false)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025D1 File Offset: 0x000007D1
		public ConnectionFactory(ITracer tracer, IPrivateInformationService piiService, bool enableMsolapTracing)
		{
			this._tracer = tracer;
			this._piiService = piiService;
			this._enableMsolapTracing = enableMsolapTracing;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025EE File Offset: 0x000007EE
		public IDbConnection CreateConnection(string dataExtension, string connectionString)
		{
			QueryContract.RetailAssert(dataExtension == "DAX", "Invalid data extension");
			return new Connection(connectionString, this._tracer, this._piiService, this._enableMsolapTracing);
		}

		// Token: 0x0400003F RID: 63
		private const string DataExtension = "DAX";

		// Token: 0x04000040 RID: 64
		private readonly ITracer _tracer;

		// Token: 0x04000041 RID: 65
		private readonly IPrivateInformationService _piiService;

		// Token: 0x04000042 RID: 66
		private readonly bool _enableMsolapTracing;
	}
}
