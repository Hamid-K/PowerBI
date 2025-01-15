using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001991 RID: 6545
	public sealed class CacheSet : ICacheSet, IDisposable
	{
		// Token: 0x17002A63 RID: 10851
		// (get) Token: 0x0600A613 RID: 42515 RVA: 0x00225AC5 File Offset: 0x00223CC5
		// (set) Token: 0x0600A614 RID: 42516 RVA: 0x00225ACD File Offset: 0x00223CCD
		public IObjectCache ObjectCache { get; set; }

		// Token: 0x17002A64 RID: 10852
		// (get) Token: 0x0600A615 RID: 42517 RVA: 0x00225AD6 File Offset: 0x00223CD6
		// (set) Token: 0x0600A616 RID: 42518 RVA: 0x00225ADE File Offset: 0x00223CDE
		public IPersistentCache PersistentCache { get; set; }

		// Token: 0x17002A65 RID: 10853
		// (get) Token: 0x0600A617 RID: 42519 RVA: 0x00225AE7 File Offset: 0x00223CE7
		// (set) Token: 0x0600A618 RID: 42520 RVA: 0x00225AEF File Offset: 0x00223CEF
		public IPersistentObjectCache PersistentObjectCache { get; set; }

		// Token: 0x0600A619 RID: 42521 RVA: 0x00225AF8 File Offset: 0x00223CF8
		public void Dispose()
		{
			this.PersistentObjectCache.Dispose();
			this.PersistentCache.Dispose();
			this.ObjectCache.Dispose();
		}
	}
}
