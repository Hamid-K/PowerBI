using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200036B RID: 875
	internal class StructuredTypeNullabilityAnalyzer : ColumnMapVisitor<HashSet<string>>
	{
		// Token: 0x06002A7B RID: 10875 RVA: 0x0008B6F1 File Offset: 0x000898F1
		internal override void Visit(VarRefColumnMap columnMap, HashSet<string> typesNeedingNullSentinel)
		{
			StructuredTypeNullabilityAnalyzer.AddTypeNeedingNullSentinel(typesNeedingNullSentinel, columnMap.Type);
			base.Visit(columnMap, typesNeedingNullSentinel);
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x0008B708 File Offset: 0x00089908
		private static void AddTypeNeedingNullSentinel(HashSet<string> typesNeedingNullSentinel, TypeUsage typeUsage)
		{
			if (TypeSemantics.IsCollectionType(typeUsage))
			{
				StructuredTypeNullabilityAnalyzer.AddTypeNeedingNullSentinel(typesNeedingNullSentinel, TypeHelpers.GetElementTypeUsage(typeUsage));
				return;
			}
			if (TypeSemantics.IsRowType(typeUsage) || TypeSemantics.IsComplexType(typeUsage))
			{
				StructuredTypeNullabilityAnalyzer.MarkAsNeedingNullSentinel(typesNeedingNullSentinel, typeUsage);
			}
			foreach (object obj in TypeHelpers.GetAllStructuralMembers(typeUsage))
			{
				EdmMember edmMember = (EdmMember)obj;
				StructuredTypeNullabilityAnalyzer.AddTypeNeedingNullSentinel(typesNeedingNullSentinel, edmMember.TypeUsage);
			}
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x0008B794 File Offset: 0x00089994
		internal static void MarkAsNeedingNullSentinel(HashSet<string> typesNeedingNullSentinel, TypeUsage typeUsage)
		{
			typesNeedingNullSentinel.Add(typeUsage.EdmType.Identity);
		}

		// Token: 0x04000EA3 RID: 3747
		internal static StructuredTypeNullabilityAnalyzer Instance = new StructuredTypeNullabilityAnalyzer();
	}
}
