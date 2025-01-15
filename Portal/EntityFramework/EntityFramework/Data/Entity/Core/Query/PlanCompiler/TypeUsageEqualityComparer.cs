using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000374 RID: 884
	internal sealed class TypeUsageEqualityComparer : IEqualityComparer<TypeUsage>
	{
		// Token: 0x06002AD4 RID: 10964 RVA: 0x0008CC74 File Offset: 0x0008AE74
		private TypeUsageEqualityComparer()
		{
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0008CC7C File Offset: 0x0008AE7C
		public bool Equals(TypeUsage x, TypeUsage y)
		{
			return x != null && y != null && TypeUsageEqualityComparer.Equals(x.EdmType, y.EdmType);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x0008CC97 File Offset: 0x0008AE97
		public int GetHashCode(TypeUsage obj)
		{
			return obj.EdmType.Identity.GetHashCode();
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x0008CCA9 File Offset: 0x0008AEA9
		internal static bool Equals(EdmType x, EdmType y)
		{
			return x.Identity.Equals(y.Identity);
		}

		// Token: 0x04000ECD RID: 3789
		internal static readonly TypeUsageEqualityComparer Instance = new TypeUsageEqualityComparer();
	}
}
