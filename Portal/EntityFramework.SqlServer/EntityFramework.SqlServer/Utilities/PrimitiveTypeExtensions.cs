using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000020 RID: 32
	internal static class PrimitiveTypeExtensions
	{
		// Token: 0x06000375 RID: 885 RVA: 0x0000EB94 File Offset: 0x0000CD94
		internal static bool IsSpatialType(this PrimitiveType type)
		{
			PrimitiveTypeKind primitiveTypeKind = type.PrimitiveTypeKind;
			return primitiveTypeKind >= PrimitiveTypeKind.Geometry && primitiveTypeKind <= PrimitiveTypeKind.GeographyCollection;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000EBB7 File Offset: 0x0000CDB7
		internal static bool IsHierarchyIdType(this PrimitiveType type)
		{
			return type.PrimitiveTypeKind == PrimitiveTypeKind.HierarchyId;
		}
	}
}
