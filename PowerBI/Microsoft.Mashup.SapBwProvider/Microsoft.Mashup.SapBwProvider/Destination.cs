using System;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200000E RID: 14
	internal class Destination : IDestination
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00004183 File Offset: 0x00002383
		public Destination(RfcDestination rfcDestination)
		{
			this.rfcDestination = rfcDestination;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004192 File Offset: 0x00002392
		public void Ping()
		{
			this.rfcDestination.Ping();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000419F File Offset: 0x0000239F
		public IRfcFunction CreateFunction(string functionName)
		{
			return this.rfcDestination.Repository.CreateFunction(functionName);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000041B2 File Offset: 0x000023B2
		public void InvokeFunction(IRfcFunction function, string traceKey)
		{
			function.Invoke(this.rfcDestination);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000041C0 File Offset: 0x000023C0
		public void BeginContext()
		{
			RfcSessionManager.BeginContext(this.rfcDestination);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000041CD File Offset: 0x000023CD
		public void EndContext()
		{
			RfcSessionManager.EndContext(this.rfcDestination);
		}

		// Token: 0x04000029 RID: 41
		private readonly RfcDestination rfcDestination;
	}
}
