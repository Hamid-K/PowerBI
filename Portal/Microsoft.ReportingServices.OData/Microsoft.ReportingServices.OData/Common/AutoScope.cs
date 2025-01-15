using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000019 RID: 25
	internal sealed class AutoScope : IDisposable
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x000042B0 File Offset: 0x000024B0
		public AutoScope(Action start, Action end)
		{
			this.m_end = end;
			start();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000042C5 File Offset: 0x000024C5
		public void Dispose()
		{
			this.m_end();
		}

		// Token: 0x0400008C RID: 140
		private Action m_end;
	}
}
