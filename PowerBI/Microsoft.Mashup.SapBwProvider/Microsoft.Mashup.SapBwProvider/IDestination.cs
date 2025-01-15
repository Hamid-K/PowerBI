using System;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200000F RID: 15
	public interface IDestination
	{
		// Token: 0x060000BC RID: 188
		void Ping();

		// Token: 0x060000BD RID: 189
		IRfcFunction CreateFunction(string functionName);

		// Token: 0x060000BE RID: 190
		void InvokeFunction(IRfcFunction function, string traceKey);

		// Token: 0x060000BF RID: 191
		void BeginContext();

		// Token: 0x060000C0 RID: 192
		void EndContext();
	}
}
