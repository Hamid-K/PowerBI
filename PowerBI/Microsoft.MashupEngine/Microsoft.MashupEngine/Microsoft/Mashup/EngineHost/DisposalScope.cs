using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001960 RID: 6496
	internal class DisposalScope : IDisposable
	{
		// Token: 0x0600A4AF RID: 42159 RVA: 0x00221B40 File Offset: 0x0021FD40
		public DisposalScope()
		{
			this.disposables = new List<IDisposable>();
		}

		// Token: 0x0600A4B0 RID: 42160 RVA: 0x00221B53 File Offset: 0x0021FD53
		public void ClearAndDispose<T>(ref T disposable) where T : IDisposable
		{
			this.disposables.Add(disposable);
			disposable = default(T);
		}

		// Token: 0x0600A4B1 RID: 42161 RVA: 0x00221B74 File Offset: 0x0021FD74
		public void Dispose()
		{
			foreach (IDisposable disposable in this.disposables)
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x040055C8 RID: 21960
		private readonly List<IDisposable> disposables;
	}
}
