using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C04 RID: 7172
	public class ObjectComparer<T> : EqualityComparer<T>
	{
		// Token: 0x0600B308 RID: 45832 RVA: 0x00246F44 File Offset: 0x00245144
		public override bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x0600B309 RID: 45833 RVA: 0x00246F54 File Offset: 0x00245154
		public override int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x04005B62 RID: 23394
		public static readonly IEqualityComparer<T> Instance = new ObjectComparer<T>();
	}
}
