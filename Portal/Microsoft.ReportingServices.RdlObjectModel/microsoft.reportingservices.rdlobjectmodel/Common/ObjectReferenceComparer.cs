using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000065 RID: 101
	internal sealed class ObjectReferenceComparer<T> : EqualityComparer<T>
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x00016317 File Offset: 0x00014517
		private ObjectReferenceComparer()
		{
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001631F File Offset: 0x0001451F
		public override bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001632F File Offset: 0x0001452F
		public override int GetHashCode(T x)
		{
			return x.GetHashCode();
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001633E File Offset: 0x0001453E
		public static ObjectReferenceComparer<T> Instance
		{
			get
			{
				return ObjectReferenceComparer<T>.m_instance;
			}
		}

		// Token: 0x04000102 RID: 258
		private static readonly ObjectReferenceComparer<T> m_instance = new ObjectReferenceComparer<T>();
	}
}
