using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005F8 RID: 1528
	internal sealed class Memoizer<TArg, TResult>
	{
		// Token: 0x06004AA2 RID: 19106 RVA: 0x001087F4 File Offset: 0x001069F4
		internal Memoizer(Func<TArg, TResult> function, IEqualityComparer<TArg> argComparer)
		{
			this._function = function;
			this._resultCache = new Dictionary<TArg, Memoizer<TArg, TResult>.Result>(argComparer);
			this._lock = new ReaderWriterLockSlim();
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x0010881C File Offset: 0x00106A1C
		internal TResult Evaluate(TArg arg)
		{
			Memoizer<TArg, TResult>.Result result;
			if (!this.TryGetResult(arg, out result))
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

		// Token: 0x06004AA4 RID: 19108 RVA: 0x001088B8 File Offset: 0x00106AB8
		internal bool TryGetValue(TArg arg, out TResult value)
		{
			Memoizer<TArg, TResult>.Result result;
			if (this.TryGetResult(arg, out result))
			{
				value = result.GetValue();
				return true;
			}
			value = default(TResult);
			return false;
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x001088E8 File Offset: 0x00106AE8
		private bool TryGetResult(TArg arg, out Memoizer<TArg, TResult>.Result result)
		{
			this._lock.EnterReadLock();
			bool flag;
			try
			{
				flag = this._resultCache.TryGetValue(arg, out result);
			}
			finally
			{
				this._lock.ExitReadLock();
			}
			return flag;
		}

		// Token: 0x04001A3C RID: 6716
		private readonly Func<TArg, TResult> _function;

		// Token: 0x04001A3D RID: 6717
		private readonly Dictionary<TArg, Memoizer<TArg, TResult>.Result> _resultCache;

		// Token: 0x04001A3E RID: 6718
		private readonly ReaderWriterLockSlim _lock;

		// Token: 0x02000C3A RID: 3130
		private class Result
		{
			// Token: 0x06006A08 RID: 27144 RVA: 0x0016B047 File Offset: 0x00169247
			internal Result(Func<TResult> createValueDelegate)
			{
				this._delegate = createValueDelegate;
			}

			// Token: 0x06006A09 RID: 27145 RVA: 0x0016B058 File Offset: 0x00169258
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

			// Token: 0x0400307B RID: 12411
			private TResult _value;

			// Token: 0x0400307C RID: 12412
			private Func<TResult> _delegate;
		}
	}
}
