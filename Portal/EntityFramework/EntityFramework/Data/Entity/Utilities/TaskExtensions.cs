using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000081 RID: 129
	public static class TaskExtensions
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x000101BC File Offset: 0x0000E3BC
		public static TaskExtensions.CultureAwaiter<T> WithCurrentCulture<T>(this Task<T> task)
		{
			return new TaskExtensions.CultureAwaiter<T>(task);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000101C4 File Offset: 0x0000E3C4
		public static TaskExtensions.CultureAwaiter WithCurrentCulture(this Task task)
		{
			return new TaskExtensions.CultureAwaiter(task);
		}

		// Token: 0x02000712 RID: 1810
		public struct CultureAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x060054E6 RID: 21734 RVA: 0x00130F2D File Offset: 0x0012F12D
			public CultureAwaiter(Task<T> task)
			{
				this._task = task;
			}

			// Token: 0x060054E7 RID: 21735 RVA: 0x00130F36 File Offset: 0x0012F136
			public TaskExtensions.CultureAwaiter<T> GetAwaiter()
			{
				return this;
			}

			// Token: 0x1700100A RID: 4106
			// (get) Token: 0x060054E8 RID: 21736 RVA: 0x00130F3E File Offset: 0x0012F13E
			public bool IsCompleted
			{
				get
				{
					return this._task.IsCompleted;
				}
			}

			// Token: 0x060054E9 RID: 21737 RVA: 0x00130F4C File Offset: 0x0012F14C
			public T GetResult()
			{
				return this._task.GetAwaiter().GetResult();
			}

			// Token: 0x060054EA RID: 21738 RVA: 0x00130F6C File Offset: 0x0012F16C
			public void OnCompleted(Action continuation)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060054EB RID: 21739 RVA: 0x00130F74 File Offset: 0x0012F174
			public void UnsafeOnCompleted(Action continuation)
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				this._task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(delegate
				{
					CultureInfo currentCulture2 = Thread.CurrentThread.CurrentCulture;
					CultureInfo currentUICulture2 = Thread.CurrentThread.CurrentUICulture;
					Thread.CurrentThread.CurrentCulture = currentCulture;
					Thread.CurrentThread.CurrentUICulture = currentUICulture;
					try
					{
						continuation();
					}
					finally
					{
						Thread.CurrentThread.CurrentCulture = currentCulture2;
						Thread.CurrentThread.CurrentUICulture = currentUICulture2;
					}
				});
			}

			// Token: 0x04001E55 RID: 7765
			private readonly Task<T> _task;
		}

		// Token: 0x02000713 RID: 1811
		public struct CultureAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x060054EC RID: 21740 RVA: 0x00130FD6 File Offset: 0x0012F1D6
			public CultureAwaiter(Task task)
			{
				this._task = task;
			}

			// Token: 0x060054ED RID: 21741 RVA: 0x00130FDF File Offset: 0x0012F1DF
			public TaskExtensions.CultureAwaiter GetAwaiter()
			{
				return this;
			}

			// Token: 0x1700100B RID: 4107
			// (get) Token: 0x060054EE RID: 21742 RVA: 0x00130FE7 File Offset: 0x0012F1E7
			public bool IsCompleted
			{
				get
				{
					return this._task.IsCompleted;
				}
			}

			// Token: 0x060054EF RID: 21743 RVA: 0x00130FF4 File Offset: 0x0012F1F4
			public void GetResult()
			{
				this._task.GetAwaiter().GetResult();
			}

			// Token: 0x060054F0 RID: 21744 RVA: 0x00131014 File Offset: 0x0012F214
			public void OnCompleted(Action continuation)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060054F1 RID: 21745 RVA: 0x0013101C File Offset: 0x0012F21C
			public void UnsafeOnCompleted(Action continuation)
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				this._task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(delegate
				{
					CultureInfo currentCulture2 = Thread.CurrentThread.CurrentCulture;
					CultureInfo currentUICulture2 = Thread.CurrentThread.CurrentUICulture;
					Thread.CurrentThread.CurrentCulture = currentCulture;
					Thread.CurrentThread.CurrentUICulture = currentUICulture;
					try
					{
						continuation();
					}
					finally
					{
						Thread.CurrentThread.CurrentCulture = currentCulture2;
						Thread.CurrentThread.CurrentUICulture = currentUICulture2;
					}
				});
			}

			// Token: 0x04001E56 RID: 7766
			private readonly Task _task;
		}
	}
}
