using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A29 RID: 6697
	public sealed class NotifyingObjectCache : CacheDelegatingObjectCache
	{
		// Token: 0x0600A969 RID: 43369 RVA: 0x00230C1D File Offset: 0x0022EE1D
		public NotifyingObjectCache(IObjectCache cache, Action callback)
			: base(cache)
		{
			this.callback = callback;
		}

		// Token: 0x0600A96A RID: 43370 RVA: 0x00230C2D File Offset: 0x0022EE2D
		public override void Dispose()
		{
			if (this.callback != null)
			{
				Action action = this.callback;
				this.callback = null;
				action();
			}
			base.Dispose();
		}

		// Token: 0x04005828 RID: 22568
		private Action callback;
	}
}
