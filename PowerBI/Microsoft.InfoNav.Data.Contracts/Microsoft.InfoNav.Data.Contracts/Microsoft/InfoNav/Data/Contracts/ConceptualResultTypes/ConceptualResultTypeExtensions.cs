using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes
{
	// Token: 0x02000144 RID: 324
	public static class ConceptualResultTypeExtensions
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x000113B4 File Offset: 0x0000F5B4
		public static bool IsNumeric(this ConceptualResultType resultType)
		{
			ConceptualPrimitiveType? primitiveTypeKind = resultType.GetPrimitiveTypeKind();
			return primitiveTypeKind != null && primitiveTypeKind.GetValueOrDefault().IsNumeric();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000113E0 File Offset: 0x0000F5E0
		public static bool IsDateTime(this ConceptualResultType resultType)
		{
			ConceptualPrimitiveType? primitiveTypeKind = resultType.GetPrimitiveTypeKind();
			return primitiveTypeKind != null && primitiveTypeKind.GetValueOrDefault().IsDateTime();
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001140C File Offset: 0x0000F60C
		public static bool IsText(this ConceptualResultType resultType)
		{
			ConceptualPrimitiveType? primitiveTypeKind = resultType.GetPrimitiveTypeKind();
			return primitiveTypeKind != null && primitiveTypeKind.GetValueOrDefault().IsText();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00011438 File Offset: 0x0000F638
		public static bool IsDouble(this ConceptualResultType resultType)
		{
			ConceptualPrimitiveType? primitiveTypeKind = resultType.GetPrimitiveTypeKind();
			return primitiveTypeKind != null && primitiveTypeKind.GetValueOrDefault().IsDouble();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00011464 File Offset: 0x0000F664
		public static ConceptualRowType GetRowType(this ConceptualResultType resultType)
		{
			ConceptualRowType conceptualRowType = resultType as ConceptualRowType;
			if (conceptualRowType != null)
			{
				return conceptualRowType;
			}
			ConceptualTableType conceptualTableType = resultType as ConceptualTableType;
			if (conceptualTableType != null)
			{
				return conceptualTableType.RowType;
			}
			return null;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00011490 File Offset: 0x0000F690
		public static ConceptualPrimitiveType? GetPrimitiveTypeKind(this ConceptualResultType resultType)
		{
			ConceptualPrimitiveResultType conceptualPrimitiveResultType = resultType as ConceptualPrimitiveResultType;
			if (conceptualPrimitiveResultType != null)
			{
				return new ConceptualPrimitiveType?(conceptualPrimitiveResultType.ConceptualDataType);
			}
			return null;
		}
	}
}
