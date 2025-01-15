using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000375 RID: 885
	internal static class TypeUtils
	{
		// Token: 0x06002AD9 RID: 10969 RVA: 0x0008CCC8 File Offset: 0x0008AEC8
		internal static bool IsStructuredType(TypeUsage type)
		{
			return TypeSemantics.IsReferenceType(type) || TypeSemantics.IsRowType(type) || TypeSemantics.IsEntityType(type) || TypeSemantics.IsRelationshipType(type) || TypeSemantics.IsComplexType(type);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x0008CCF2 File Offset: 0x0008AEF2
		internal static bool IsCollectionType(TypeUsage type)
		{
			return TypeSemantics.IsCollectionType(type);
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x0008CCFA File Offset: 0x0008AEFA
		internal static bool IsEnumerationType(TypeUsage type)
		{
			return TypeSemantics.IsEnumerationType(type);
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x0008CD02 File Offset: 0x0008AF02
		internal static TypeUsage CreateCollectionType(TypeUsage elementType)
		{
			return TypeHelpers.CreateCollectionTypeUsage(elementType);
		}
	}
}
