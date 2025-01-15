using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000041 RID: 65
	internal sealed class Memoizer<TArg, TResult>
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00004648 File Offset: 0x00002848
		internal Memoizer(Func<TArg, TResult> function, IEqualityComparer<TArg> argComparer)
		{
			this.function = function;
			this.resultCache = new Dictionary<TArg, Memoizer<TArg, TResult>.Result>(argComparer);
			this.slimLock = new Memoizer<TArg, TResult>.ReaderWriterLockSlim();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004670 File Offset: 0x00002870
		internal TResult Evaluate(TArg arg)
		{
			this.slimLock.EnterReadLock();
			Memoizer<TArg, TResult>.Result result;
			bool flag;
			try
			{
				flag = this.resultCache.TryGetValue(arg, out result);
			}
			finally
			{
				this.slimLock.ExitReadLock();
			}
			if (!flag)
			{
				this.slimLock.EnterWriteLock();
				try
				{
					if (!this.resultCache.TryGetValue(arg, out result))
					{
						result = new Memoizer<TArg, TResult>.Result(() => this.function(arg));
						this.resultCache.Add(arg, result);
					}
				}
				finally
				{
					this.slimLock.ExitWriteLock();
				}
			}
			return result.GetValue();
		}

		// Token: 0x04000079 RID: 121
		private readonly Func<TArg, TResult> function;

		// Token: 0x0400007A RID: 122
		private readonly Dictionary<TArg, Memoizer<TArg, TResult>.Result> resultCache;

		// Token: 0x0400007B RID: 123
		private readonly Memoizer<TArg, TResult>.ReaderWriterLockSlim slimLock;

		// Token: 0x0200020F RID: 527
		private class Result
		{
			// Token: 0x06000E0E RID: 3598 RVA: 0x000266EF File Offset: 0x000248EF
			internal Result(Func<TResult> createValueDelegate)
			{
				this.createValueDelegate = createValueDelegate;
			}

			// Token: 0x06000E0F RID: 3599 RVA: 0x00026700 File Offset: 0x00024900
			internal TResult GetValue()
			{
				if (this.createValueDelegate == null)
				{
					return this.value;
				}
				TResult tresult;
				lock (this)
				{
					if (this.createValueDelegate == null)
					{
						tresult = this.value;
					}
					else
					{
						this.value = this.createValueDelegate();
						this.createValueDelegate = null;
						tresult = this.value;
					}
				}
				return tresult;
			}

			// Token: 0x040007C5 RID: 1989
			private TResult value;

			// Token: 0x040007C6 RID: 1990
			private Func<TResult> createValueDelegate;
		}

		// Token: 0x02000210 RID: 528
		private sealed class ReaderWriterLockSlim
		{
			// Token: 0x06000E10 RID: 3600 RVA: 0x00026778 File Offset: 0x00024978
			internal void EnterReadLock()
			{
				Monitor.Enter(this.readerWriterLock);
			}

			// Token: 0x06000E11 RID: 3601 RVA: 0x00026778 File Offset: 0x00024978
			internal void EnterWriteLock()
			{
				Monitor.Enter(this.readerWriterLock);
			}

			// Token: 0x06000E12 RID: 3602 RVA: 0x00026785 File Offset: 0x00024985
			internal void ExitReadLock()
			{
				Monitor.Exit(this.readerWriterLock);
			}

			// Token: 0x06000E13 RID: 3603 RVA: 0x00026785 File Offset: 0x00024985
			internal void ExitWriteLock()
			{
				Monitor.Exit(this.readerWriterLock);
			}

			// Token: 0x040007C7 RID: 1991
			private object readerWriterLock = new object();
		}
	}
}
