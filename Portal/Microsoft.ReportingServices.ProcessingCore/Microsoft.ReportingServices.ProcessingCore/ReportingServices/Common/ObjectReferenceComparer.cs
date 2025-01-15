using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005DA RID: 1498
	internal sealed class ObjectReferenceComparer<T> : EqualityComparer<T>
	{
		// Token: 0x060053E8 RID: 21480 RVA: 0x001616E7 File Offset: 0x0015F8E7
		private ObjectReferenceComparer()
		{
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x001616EF File Offset: 0x0015F8EF
		public override bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x060053EA RID: 21482 RVA: 0x001616FF File Offset: 0x0015F8FF
		public override int GetHashCode(T x)
		{
			return x.GetHashCode();
		}

		// Token: 0x17001EF8 RID: 7928
		// (get) Token: 0x060053EB RID: 21483 RVA: 0x0016170E File Offset: 0x0015F90E
		public static ObjectReferenceComparer<T> Instance
		{
			get
			{
				return ObjectReferenceComparer<T>.m_instance;
			}
		}

		// Token: 0x04002A37 RID: 10807
		private static readonly ObjectReferenceComparer<T> m_instance = new ObjectReferenceComparer<T>();
	}
}
