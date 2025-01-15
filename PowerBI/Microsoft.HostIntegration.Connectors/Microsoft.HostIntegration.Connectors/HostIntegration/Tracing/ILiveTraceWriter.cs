using System;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x0200065B RID: 1627
	public interface ILiveTraceWriter
	{
		// Token: 0x0600363D RID: 13885
		void WriteLine(string line);

		// Token: 0x0600363E RID: 13886
		void Close();
	}
}
