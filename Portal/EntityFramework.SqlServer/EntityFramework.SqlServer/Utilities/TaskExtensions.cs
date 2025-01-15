using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000023 RID: 35
	public static class TaskExtensions
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000F380 File Offset: 0x0000D580
		public static TaskExtensions.CultureAwaiter<T> WithCurrentCulture<T>(this Task<T> task)
		{
			return new TaskExtensions.CultureAwaiter<T>(task);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000F388 File Offset: 0x0000D588
		public static TaskExtensions.CultureAwaiter WithCurrentCulture(this Task task)
		{
			return new TaskExtensions.CultureAwaiter(task);
		}

		// Token: 0x0200006C RID: 108
		public struct CultureAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600067E RID: 1662 RVA: 0x0001B5BF File Offset: 0x000197BF
			public CultureAwaiter(Task<T> task)
			{
				this._task = task;
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x0001B5C8 File Offset: 0x000197C8
			public TaskExtensions.CultureAwaiter<T> GetAwaiter()
			{
				return this;
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001B5D0 File Offset: 0x000197D0
			public bool IsCompleted
			{
				get
				{
					return this._task.IsCompleted;
				}
			}

			// Token: 0x06000681 RID: 1665 RVA: 0x0001B5E0 File Offset: 0x000197E0
			public T GetResult()
			{
				return this._task.GetAwaiter().GetResult();
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x0001B600 File Offset: 0x00019800
			public void OnCompleted(Action continuation)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000683 RID: 1667 RVA: 0x0001B608 File Offset: 0x00019808
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

			// Token: 0x040001CC RID: 460
			private readonly Task<T> _task;
		}

		// Token: 0x0200006D RID: 109
		public struct CultureAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000684 RID: 1668 RVA: 0x0001B66A File Offset: 0x0001986A
			public CultureAwaiter(Task task)
			{
				this._task = task;
			}

			// Token: 0x06000685 RID: 1669 RVA: 0x0001B673 File Offset: 0x00019873
			public TaskExtensions.CultureAwaiter GetAwaiter()
			{
				return this;
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001B67B File Offset: 0x0001987B
			public bool IsCompleted
			{
				get
				{
					return this._task.IsCompleted;
				}
			}

			// Token: 0x06000687 RID: 1671 RVA: 0x0001B688 File Offset: 0x00019888
			public void GetResult()
			{
				this._task.GetAwaiter().GetResult();
			}

			// Token: 0x06000688 RID: 1672 RVA: 0x0001B6A8 File Offset: 0x000198A8
			public void OnCompleted(Action continuation)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000689 RID: 1673 RVA: 0x0001B6B0 File Offset: 0x000198B0
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

			// Token: 0x040001CD RID: 461
			private readonly Task _task;
		}
	}
}
