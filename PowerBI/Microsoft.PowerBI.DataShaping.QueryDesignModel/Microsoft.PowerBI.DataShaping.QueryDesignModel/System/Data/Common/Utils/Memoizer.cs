using System;
using System.Collections.Generic;
using Microsoft.Threading;

namespace System.Data.Common.Utils
{
	// Token: 0x0200005C RID: 92
	internal sealed class Memoizer<TArg, TResult>
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x00013B49 File Offset: 0x00011D49
		internal Memoizer(Func<TArg, TResult> function, IEqualityComparer<TArg> argComparer)
		{
			EntityUtil.CheckArgumentNull<Func<TArg, TResult>>(function, "function");
			this._function = function;
			this._resultCache = new Dictionary<TArg, Memoizer<TArg, TResult>.Result>(argComparer);
			this._lock = new ReaderWriterLockSlim();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00013B7C File Offset: 0x00011D7C
		internal TResult Evaluate(TArg arg)
		{
			this._lock.EnterReadLock();
			Memoizer<TArg, TResult>.Result result;
			bool flag;
			try
			{
				flag = this._resultCache.TryGetValue(arg, out result);
			}
			finally
			{
				this._lock.ExitReadLock();
			}
			if (!flag)
			{
				this._lock.EnterWriteLock();
				try
				{
					if (!this._resultCache.TryGetValue(arg, out result))
					{
						result = new Memoizer<TArg, TResult>.Result(() => this._function(arg));
						this._resultCache.Add(arg, result);
					}
				}
				finally
				{
					this._lock.ExitWriteLock();
				}
			}
			return result.GetValue();
		}

		// Token: 0x040006E7 RID: 1767
		private readonly Func<TArg, TResult> _function;

		// Token: 0x040006E8 RID: 1768
		private readonly Dictionary<TArg, Memoizer<TArg, TResult>.Result> _resultCache;

		// Token: 0x040006E9 RID: 1769
		private readonly ReaderWriterLockSlim _lock;

		// Token: 0x020002AB RID: 683
		private class Result
		{
			// Token: 0x06001C35 RID: 7221 RVA: 0x0004E7C7 File Offset: 0x0004C9C7
			internal Result(Func<TResult> createValueDelegate)
			{
				this._delegate = createValueDelegate;
			}

			// Token: 0x06001C36 RID: 7222 RVA: 0x0004E7D8 File Offset: 0x0004C9D8
			internal TResult GetValue()
			{
				if (this._delegate == null)
				{
					return this._value;
				}
				TResult tresult;
				lock (this)
				{
					if (this._delegate == null)
					{
						tresult = this._value;
					}
					else
					{
						this._value = this._delegate();
						this._delegate = null;
						tresult = this._value;
					}
				}
				return tresult;
			}

			// Token: 0x04000F8C RID: 3980
			private TResult _value;

			// Token: 0x04000F8D RID: 3981
			private Func<TResult> _delegate;
		}
	}
}
