using System;
using System.Collections.Generic;
using Microsoft.Internal;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010D8 RID: 4312
	internal struct LazyDictionary<TKey, TValue>
	{
		// Token: 0x060070E5 RID: 28901 RVA: 0x001838C4 File Offset: 0x00181AC4
		public LazyDictionary(Func<TKey, TValue> getValue)
		{
			this.getValue = getValue;
			this.dictionary = null;
		}

		// Token: 0x17001FBB RID: 8123
		public TValue this[TKey key]
		{
			get
			{
				this.EnsureIsInitialized();
				TValue tvalue;
				if (!this.dictionary.TryGetValue(key, out tvalue))
				{
					tvalue = this.getValue(key);
					this.dictionary[key] = tvalue;
				}
				return tvalue;
			}
		}

		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x060070E7 RID: 28903 RVA: 0x00183912 File Offset: 0x00181B12
		public IEnumerable<TValue> Values
		{
			get
			{
				if (this.dictionary == null)
				{
					return EmptyEnumerable<TValue>.Instance;
				}
				return this.dictionary.Values;
			}
		}

		// Token: 0x060070E8 RID: 28904 RVA: 0x0018392D File Offset: 0x00181B2D
		private void EnsureIsInitialized()
		{
			if (this.dictionary == null)
			{
				this.dictionary = new Dictionary<TKey, TValue>();
			}
		}

		// Token: 0x04003E2D RID: 15917
		private readonly Func<TKey, TValue> getValue;

		// Token: 0x04003E2E RID: 15918
		private Dictionary<TKey, TValue> dictionary;
	}
}
