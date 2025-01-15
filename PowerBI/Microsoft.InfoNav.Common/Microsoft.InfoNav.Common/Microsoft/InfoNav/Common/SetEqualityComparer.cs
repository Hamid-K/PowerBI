using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200006F RID: 111
	[ImmutableObject(true)]
	internal sealed class SetEqualityComparer<T> : IEqualityComparer<ISet<T>>
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0000A8DC File Offset: 0x00008ADC
		internal SetEqualityComparer(IEqualityComparer<T> comparer)
		{
			this._comparer = comparer;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000A8EC File Offset: 0x00008AEC
		public bool Equals(ISet<T> x, ISet<T> y)
		{
			bool? flag = Util.AreEqual<ISet<T>>(x, y);
			if (flag != null)
			{
				return flag.Value;
			}
			return x.Count == y.Count && x.SetEquals(y);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000A929 File Offset: 0x00008B29
		public int GetHashCode(ISet<T> set)
		{
			return Hashing.CombineHash<T>(set, this._comparer);
		}

		// Token: 0x040000E1 RID: 225
		private readonly IEqualityComparer<T> _comparer;
	}
}
