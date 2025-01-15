using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200010A RID: 266
	internal sealed class Memoizer<TArg, TResult>
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x0000D732 File Offset: 0x0000B932
		internal Memoizer(Func<TArg, TResult> function, IEqualityComparer<TArg> argComparer)
		{
			this.function = function;
			this.resultCache = new Dictionary<TArg, Memoizer<TArg, TResult>.Result>(argComparer);
			this.slimLock = new ReaderWriterLockSlim();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000D778 File Offset: 0x0000B978
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

		// Token: 0x040001FC RID: 508
		private readonly Func<TArg, TResult> function;

		// Token: 0x040001FD RID: 509
		private readonly Dictionary<TArg, Memoizer<TArg, TResult>.Result> resultCache;

		// Token: 0x040001FE RID: 510
		private readonly ReaderWriterLockSlim slimLock;

		// Token: 0x0200010B RID: 267
		private class Result
		{
			// Token: 0x06000539 RID: 1337 RVA: 0x0000D844 File Offset: 0x0000BA44
			internal Result(Func<TResult> createValueDelegate)
			{
				this.createValueDelegate = createValueDelegate;
			}

			// Token: 0x0600053A RID: 1338 RVA: 0x0000D854 File Offset: 0x0000BA54
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

			// Token: 0x040001FF RID: 511
			private TResult value;

			// Token: 0x04000200 RID: 512
			private Func<TResult> createValueDelegate;
		}
	}
}
