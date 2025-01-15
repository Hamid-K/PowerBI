using System;
using System.Diagnostics;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200012B RID: 299
	internal class RetryLazy<TInput, TResult> where TResult : class
	{
		// Token: 0x0600149B RID: 5275 RVA: 0x00035DF0 File Offset: 0x00033FF0
		public RetryLazy(Func<TInput, TResult> valueFactory)
		{
			this._valueFactory = valueFactory;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00035E0C File Offset: 0x0003400C
		[DebuggerStepThrough]
		public TResult GetValue(TInput input)
		{
			object @lock = this._lock;
			TResult value;
			lock (@lock)
			{
				if (this._value == null)
				{
					Func<TInput, TResult> valueFactory = this._valueFactory;
					try
					{
						this._valueFactory = null;
						this._value = valueFactory(input);
					}
					catch (Exception)
					{
						this._valueFactory = valueFactory;
						throw;
					}
				}
				value = this._value;
			}
			return value;
		}

		// Token: 0x040009AF RID: 2479
		private readonly object _lock = new object();

		// Token: 0x040009B0 RID: 2480
		private Func<TInput, TResult> _valueFactory;

		// Token: 0x040009B1 RID: 2481
		private TResult _value;
	}
}
