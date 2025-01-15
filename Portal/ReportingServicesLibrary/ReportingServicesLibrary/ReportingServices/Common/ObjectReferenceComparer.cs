using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200036B RID: 875
	internal sealed class ObjectReferenceComparer<T> : EqualityComparer<T>
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x00073F47 File Offset: 0x00072147
		private ObjectReferenceComparer()
		{
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00073F4F File Offset: 0x0007214F
		public override bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00073F5F File Offset: 0x0007215F
		public override int GetHashCode(T x)
		{
			return x.GetHashCode();
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00073F6E File Offset: 0x0007216E
		public static ObjectReferenceComparer<T> Instance
		{
			get
			{
				return ObjectReferenceComparer<T>.m_instance;
			}
		}

		// Token: 0x04000BD5 RID: 3029
		private static readonly ObjectReferenceComparer<T> m_instance = new ObjectReferenceComparer<T>();
	}
}
