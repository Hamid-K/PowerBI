using System;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200197F RID: 6527
	public sealed class NotifyingPersistentCache : CacheDelegatingPersistentCache
	{
		// Token: 0x0600A59C RID: 42396 RVA: 0x002243B5 File Offset: 0x002225B5
		public NotifyingPersistentCache(PersistentCache cache, Action callback)
			: base(cache)
		{
			this.callback = callback;
		}

		// Token: 0x0600A59D RID: 42397 RVA: 0x002243C8 File Offset: 0x002225C8
		public override void Dispose()
		{
			try
			{
				if (this.callback != null)
				{
					Action action = this.callback;
					this.callback = null;
					action();
				}
			}
			finally
			{
				base.Dispose();
			}
		}

		// Token: 0x04005633 RID: 22067
		private Action callback;
	}
}
