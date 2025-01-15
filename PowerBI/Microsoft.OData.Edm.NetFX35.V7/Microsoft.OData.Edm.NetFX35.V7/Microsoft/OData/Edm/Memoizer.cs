using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200000A RID: 10
	internal sealed class Memoizer<TArg, TResult>
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002A90 File Offset: 0x00000C90
		internal Memoizer(Func<TArg, TResult> function, IEqualityComparer<TArg> argComparer)
		{
			this.function = function;
			this.resultCache = new Dictionary<TArg, Memoizer<TArg, TResult>.Result>(argComparer);
			this.slimLock = new ReaderWriterLockSlim();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002AB8 File Offset: 0x00000CB8
		internal TResult Evaluate(TArg arg)
		{
			this.slimLock.EnterReadLock();
			Memoizer<TArg, TResult>.Result result;
			bool flag;
			try
			{
				flag = this.resultCache.TryGetValue(arg, ref result);
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
					if (!this.resultCache.TryGetValue(arg, ref result))
					{
						result = new Memoizer<TArg, TResult>.Result(() => this.function.Invoke(arg));
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

		// Token: 0x0400000A RID: 10
		private readonly Func<TArg, TResult> function;

		// Token: 0x0400000B RID: 11
		private readonly Dictionary<TArg, Memoizer<TArg, TResult>.Result> resultCache;

		// Token: 0x0400000C RID: 12
		private readonly ReaderWriterLockSlim slimLock;

		// Token: 0x02000201 RID: 513
		private class Result
		{
			// Token: 0x06000D56 RID: 3414 RVA: 0x0002455C File Offset: 0x0002275C
			internal Result(Func<TResult> createValueDelegate)
			{
				this.createValueDelegate = createValueDelegate;
			}

			// Token: 0x06000D57 RID: 3415 RVA: 0x0002456C File Offset: 0x0002276C
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
						this.value = this.createValueDelegate.Invoke();
						this.createValueDelegate = null;
						tresult = this.value;
					}
				}
				return tresult;
			}

			// Token: 0x04000746 RID: 1862
			private TResult value;

			// Token: 0x04000747 RID: 1863
			private Func<TResult> createValueDelegate;
		}
	}
}
