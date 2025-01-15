using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ModelParameters
{
	// Token: 0x020000EB RID: 235
	internal sealed class ParameterMappingComparer : IEqualityComparer<ParameterMapping>
	{
		// Token: 0x06000635 RID: 1589 RVA: 0x0000CF5D File Offset: 0x0000B15D
		private ParameterMappingComparer()
		{
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000CF68 File Offset: 0x0000B168
		public bool Equals(ParameterMapping m1, ParameterMapping m2)
		{
			bool? flag = Util.AreEqual<ParameterMapping>(m1, m2);
			if (flag != null)
			{
				return flag.Value;
			}
			return ParameterMappings.ParameterNameComparer.Equals(m1.ParameterName, m2.ParameterName) && m1.IsListType == m2.IsListType && m1.IsSelectAllFilter == m2.IsSelectAllFilter && m1.Values.SetEquals(m2.Values);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000CFD8 File Offset: 0x0000B1D8
		public int GetHashCode(ParameterMapping obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			int hashCode = Hashing.GetHashCode<string>(obj.ParameterName, null);
			ISet<ResolvedQueryLiteralExpression> values = obj.Values;
			return Hashing.CombineHash(hashCode, Hashing.GetHashCode<int?>((values != null) ? new int?(values.Count) : null, null), Hashing.GetHashCode<bool>(obj.IsListType, null), Hashing.GetHashCode<bool>(obj.IsSelectAllFilter, null));
		}

		// Token: 0x040002A6 RID: 678
		public static ParameterMappingComparer Instance = new ParameterMappingComparer();
	}
}
