using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x02000017 RID: 23
	public static class ConceptualColumnExtensions
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002484 File Offset: 0x00000684
		public static bool HasMappedParameter(this IConceptualColumn column)
		{
			return !column.GetMappedParameter().IsNullOrEmpty<ConceptualMParameter>();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002494 File Offset: 0x00000694
		public static IReadOnlyList<ConceptualMParameter> GetMappedParameter(this IConceptualColumn column)
		{
			if (column.ParameterMetadata == null || column.ParameterMetadata.ParameterKind != ParameterKind.MParameters)
			{
				return null;
			}
			return column.ParameterMetadata.MappedMParameters;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000024B9 File Offset: 0x000006B9
		public static bool IsMappedParameterListType(this IConceptualColumn column)
		{
			return column.ParameterMetadata != null && column.ParameterMetadata.ParameterKind == ParameterKind.MParameters && column.ParameterMetadata.SupportsMultipleValues;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000024DE File Offset: 0x000006DE
		public static string MappedParameterSelectAllValue(this IConceptualColumn column)
		{
			if (column.ParameterMetadata == null || column.ParameterMetadata.ParameterKind != ParameterKind.MParameters)
			{
				return null;
			}
			return column.ParameterMetadata.SelectAllValue;
		}
	}
}
