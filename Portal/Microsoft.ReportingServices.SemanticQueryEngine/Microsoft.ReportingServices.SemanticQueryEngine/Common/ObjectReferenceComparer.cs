using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000076 RID: 118
	internal sealed class ObjectReferenceComparer<T> : EqualityComparer<T>
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x00015C33 File Offset: 0x00013E33
		private ObjectReferenceComparer()
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00015C3B File Offset: 0x00013E3B
		public override bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00015C4B File Offset: 0x00013E4B
		public override int GetHashCode(T x)
		{
			return x.GetHashCode();
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00015C5A File Offset: 0x00013E5A
		public static ObjectReferenceComparer<T> Instance
		{
			get
			{
				return ObjectReferenceComparer<T>.m_instance;
			}
		}

		// Token: 0x04000206 RID: 518
		private static readonly ObjectReferenceComparer<T> m_instance = new ObjectReferenceComparer<T>();
	}
}
