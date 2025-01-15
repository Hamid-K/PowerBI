using System;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000010 RID: 16
	public class ImpersonatedDestination : IDestination
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x000041DA File Offset: 0x000023DA
		public ImpersonatedDestination(IDestination destination, Func<string, IDisposable> impersonate)
		{
			this.destination = destination;
			this.impersonate = impersonate;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000041F0 File Offset: 0x000023F0
		public void Ping()
		{
			using (this.impersonate("Impersonate/Ping"))
			{
				this.destination.Ping();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004238 File Offset: 0x00002438
		public void BeginContext()
		{
			using (this.impersonate("Impersonate/BeginContext"))
			{
				this.destination.BeginContext();
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004280 File Offset: 0x00002480
		public IRfcFunction CreateFunction(string functionName)
		{
			IRfcFunction rfcFunction;
			using (this.impersonate("Impersonate/CreateFunction/" + functionName))
			{
				rfcFunction = this.destination.CreateFunction(functionName);
			}
			return rfcFunction;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000042D0 File Offset: 0x000024D0
		public void EndContext()
		{
			using (this.impersonate("Impersonate/EndContext"))
			{
				this.destination.EndContext();
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004318 File Offset: 0x00002518
		public void InvokeFunction(IRfcFunction function, string traceKey)
		{
			using (this.impersonate("Impersonate/InvokeFunction/" + function.Metadata.Name))
			{
				this.destination.InvokeFunction(function, traceKey);
			}
		}

		// Token: 0x0400002A RID: 42
		private readonly IDestination destination;

		// Token: 0x0400002B RID: 43
		private readonly Func<string, IDisposable> impersonate;
	}
}
