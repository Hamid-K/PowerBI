using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200000E RID: 14
	internal sealed class ObjectReferenceComparer<T> : EqualityComparer<T>
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002CEF File Offset: 0x00000EEF
		private ObjectReferenceComparer()
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002CF7 File Offset: 0x00000EF7
		public override bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D07 File Offset: 0x00000F07
		public override int GetHashCode(T x)
		{
			return x.GetHashCode();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002D16 File Offset: 0x00000F16
		public static ObjectReferenceComparer<T> Instance
		{
			get
			{
				return ObjectReferenceComparer<T>.m_instance;
			}
		}

		// Token: 0x04000005 RID: 5
		private static readonly ObjectReferenceComparer<T> m_instance = new ObjectReferenceComparer<T>();
	}
}
