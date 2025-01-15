using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000088 RID: 136
	internal abstract class ComparerBase<T, TProperty> : IEqualityComparer<T> where T : class where TProperty : class
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000E7E6 File Offset: 0x0000C9E6
		public bool Equals(T x, T y)
		{
			return (x == null && y == null) || (x != null && y != null && ComparerBase<T, TProperty>.Equals(this.GetProperty(x), this.GetProperty(y)));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000E81F File Offset: 0x0000CA1F
		private static bool Equals(TProperty px, TProperty py)
		{
			return (px == null && py == null) || (px != null && px.Equals(py));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000E84E File Offset: 0x0000CA4E
		public int GetHashCode(T obj)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<T>(obj, "obj");
			return this.GetProperty(obj).GetHashCode();
		}

		// Token: 0x060003F6 RID: 1014
		protected abstract TProperty GetProperty(T obj);
	}
}
