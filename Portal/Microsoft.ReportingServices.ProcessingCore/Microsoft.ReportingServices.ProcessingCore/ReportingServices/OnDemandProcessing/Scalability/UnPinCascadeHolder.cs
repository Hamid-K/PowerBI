using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200089B RID: 2203
	internal sealed class UnPinCascadeHolder : IDisposable
	{
		// Token: 0x060078D1 RID: 30929 RVA: 0x001F216B File Offset: 0x001F036B
		internal UnPinCascadeHolder()
		{
			this.m_cleanupRefs = new List<IDisposable>();
		}

		// Token: 0x060078D2 RID: 30930 RVA: 0x001F217E File Offset: 0x001F037E
		internal void AddCleanupRef(IDisposable cleanupRef)
		{
			this.m_cleanupRefs.Add(cleanupRef);
		}

		// Token: 0x060078D3 RID: 30931 RVA: 0x001F218C File Offset: 0x001F038C
		public void Dispose()
		{
			for (int i = 0; i < this.m_cleanupRefs.Count; i++)
			{
				this.m_cleanupRefs[i].Dispose();
			}
		}

		// Token: 0x04003CB1 RID: 15537
		private List<IDisposable> m_cleanupRefs;
	}
}
