using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Data.Edm.Internal
{
	// Token: 0x020000D3 RID: 211
	internal sealed class Memoizer<TArg, TResult>
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0000BBC9 File Offset: 0x00009DC9
		internal Memoizer(Func<TArg, TResult> function, IEqualityComparer<TArg> argComparer)
		{
			this.function = function;
			this.resultCache = new Dictionary<TArg, Memoizer<TArg, TResult>.Result>(argComparer);
			this.slimLock = new ReaderWriterLockSlim();
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000BC10 File Offset: 0x00009E10
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

		// Token: 0x0400019C RID: 412
		private readonly Func<TArg, TResult> function;

		// Token: 0x0400019D RID: 413
		private readonly Dictionary<TArg, Memoizer<TArg, TResult>.Result> resultCache;

		// Token: 0x0400019E RID: 414
		private readonly ReaderWriterLockSlim slimLock;

		// Token: 0x020000D4 RID: 212
		private class Result
		{
			// Token: 0x06000440 RID: 1088 RVA: 0x0000BCDC File Offset: 0x00009EDC
			internal Result(Func<TResult> createValueDelegate)
			{
				this.createValueDelegate = createValueDelegate;
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x0000BCEC File Offset: 0x00009EEC
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

			// Token: 0x0400019F RID: 415
			private TResult value;

			// Token: 0x040001A0 RID: 416
			private Func<TResult> createValueDelegate;
		}
	}
}
