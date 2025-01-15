using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200001C RID: 28
	internal sealed class AutoScope : IDisposable
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000041C4 File Offset: 0x000023C4
		public AutoScope(Action start, Action end)
		{
			this.m_end = end;
			start();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000041D9 File Offset: 0x000023D9
		public void Dispose()
		{
			this.m_end();
		}

		// Token: 0x040000AF RID: 175
		private Action m_end;
	}
}
