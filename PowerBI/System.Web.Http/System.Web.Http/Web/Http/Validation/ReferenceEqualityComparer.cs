using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Web.Http.Validation
{
	// Token: 0x02000098 RID: 152
	internal class ReferenceEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000AD88 File Offset: 0x00008F88
		public static ReferenceEqualityComparer Instance
		{
			get
			{
				return ReferenceEqualityComparer._instance;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000AD8F File Offset: 0x00008F8F
		public bool Equals(object x, object y)
		{
			return x == y;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000AD95 File Offset: 0x00008F95
		public int GetHashCode(object obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x040000DC RID: 220
		private static readonly ReferenceEqualityComparer _instance = new ReferenceEqualityComparer();
	}
}
