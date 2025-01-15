using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001DF RID: 479
	public sealed class CollectionDisposer<T> : IDisposable where T : class, IDisposable
	{
		// Token: 0x06000C82 RID: 3202 RVA: 0x0002BCCD File Offset: 0x00029ECD
		public CollectionDisposer(IEnumerable<T> disposables)
		{
			this.m_disposables = disposables;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002BCDC File Offset: 0x00029EDC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002BCE5 File Offset: 0x00029EE5
		public IEnumerable<T> Elements
		{
			get
			{
				return this.m_disposables;
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002BCF0 File Offset: 0x00029EF0
		private void Dispose(bool disposing)
		{
			if (disposing && this.m_disposables != null)
			{
				foreach (T t in this.m_disposables)
				{
					t.Dispose();
				}
			}
			this.m_disposables = null;
		}

		// Token: 0x040004CD RID: 1229
		private IEnumerable<T> m_disposables;
	}
}
