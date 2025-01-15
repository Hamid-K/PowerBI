using System;
using System.Threading;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x02000648 RID: 1608
	public static class Disposable
	{
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x00062C48 File Offset: 0x00060E48
		public static IDisposable Empty
		{
			get
			{
				return Disposable.EmptyDisposable.Instance;
			}
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x00062C4F File Offset: 0x00060E4F
		public static IDisposable Create(Action dispose)
		{
			return new Disposable.AnonymousDisposable(dispose);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x00062C57 File Offset: 0x00060E57
		public static ICancelable CreateCancelable()
		{
			return new Disposable.CancellationDisposable();
		}

		// Token: 0x02000649 RID: 1609
		private class EmptyDisposable : IDisposable
		{
			// Token: 0x060022FE RID: 8958 RVA: 0x0000CC37 File Offset: 0x0000AE37
			public void Dispose()
			{
			}

			// Token: 0x0400108C RID: 4236
			public static readonly Disposable.EmptyDisposable Instance = new Disposable.EmptyDisposable();
		}

		// Token: 0x0200064A RID: 1610
		private class AnonymousDisposable : IDisposable
		{
			// Token: 0x06002301 RID: 8961 RVA: 0x00062C6A File Offset: 0x00060E6A
			public AnonymousDisposable(Action dispose)
			{
				this._dispose = dispose;
			}

			// Token: 0x06002302 RID: 8962 RVA: 0x00062C7B File Offset: 0x00060E7B
			public void Dispose()
			{
				Action action = Interlocked.Exchange<Action>(ref this._dispose, null);
				if (action == null)
				{
					return;
				}
				action();
			}

			// Token: 0x0400108D RID: 4237
			private volatile Action _dispose;
		}

		// Token: 0x0200064B RID: 1611
		private class CancellationDisposable : ICancelable, IDisposable
		{
			// Token: 0x06002303 RID: 8963 RVA: 0x00062C93 File Offset: 0x00060E93
			public CancellationDisposable()
			{
				this._cancellationTokenSource = new CancellationTokenSource();
			}

			// Token: 0x06002304 RID: 8964 RVA: 0x00062CA6 File Offset: 0x00060EA6
			public void Dispose()
			{
				this._cancellationTokenSource.Cancel();
			}

			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x06002305 RID: 8965 RVA: 0x00062CB3 File Offset: 0x00060EB3
			public bool IsDisposed
			{
				get
				{
					return this._cancellationTokenSource.IsCancellationRequested;
				}
			}

			// Token: 0x0400108E RID: 4238
			private readonly CancellationTokenSource _cancellationTokenSource;
		}
	}
}
