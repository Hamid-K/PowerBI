using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.Http
{
	// Token: 0x02000013 RID: 19
	internal class EmptyReadOnlyDictionary<TKey, TValue>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public static IDictionary<TKey, TValue> Value
		{
			get
			{
				return EmptyReadOnlyDictionary<TKey, TValue>._value;
			}
		}

		// Token: 0x04000013 RID: 19
		private static readonly ReadOnlyDictionary<TKey, TValue> _value = new ReadOnlyDictionary<TKey, TValue>(new Dictionary<TKey, TValue>());
	}
}
