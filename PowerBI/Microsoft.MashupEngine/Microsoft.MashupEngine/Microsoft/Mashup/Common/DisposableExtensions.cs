using System;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BE9 RID: 7145
	public static class DisposableExtensions
	{
		// Token: 0x0600B282 RID: 45698 RVA: 0x002458CC File Offset: 0x00243ACC
		public static IDisposable AfterDispose(this IDisposable disposable, Action action)
		{
			return new DisposableExtensions.NotifyingDisposable(disposable, action);
		}

		// Token: 0x04005B38 RID: 23352
		public static readonly IDisposable Empty = new DisposableExtensions.EmptyDisposable();

		// Token: 0x02001BEA RID: 7146
		private sealed class NotifyingDisposable : IDisposable
		{
			// Token: 0x0600B284 RID: 45700 RVA: 0x002458E1 File Offset: 0x00243AE1
			public NotifyingDisposable(IDisposable disposable, Action callback)
			{
				this.disposable = disposable;
				this.callback = callback;
			}

			// Token: 0x0600B285 RID: 45701 RVA: 0x002458F8 File Offset: 0x00243AF8
			void IDisposable.Dispose()
			{
				if (this.disposable != null)
				{
					try
					{
						this.disposable.Dispose();
						this.callback();
					}
					finally
					{
						this.disposable = null;
					}
				}
			}

			// Token: 0x04005B39 RID: 23353
			private IDisposable disposable;

			// Token: 0x04005B3A RID: 23354
			private Action callback;
		}

		// Token: 0x02001BEB RID: 7147
		private sealed class EmptyDisposable : IDisposable
		{
			// Token: 0x0600B286 RID: 45702 RVA: 0x0000336E File Offset: 0x0000156E
			void IDisposable.Dispose()
			{
			}
		}
	}
}
