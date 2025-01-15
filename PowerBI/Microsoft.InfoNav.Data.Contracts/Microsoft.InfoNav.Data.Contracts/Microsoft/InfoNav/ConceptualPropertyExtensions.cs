using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200002E RID: 46
	public static class ConceptualPropertyExtensions
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000028E0 File Offset: 0x00000AE0
		public static bool IsImageProperty(this IConceptualProperty property)
		{
			return property != null && (property.ConceptualDataCategory == ConceptualDataCategory.Image || property.ConceptualDataCategory == ConceptualDataCategory.ImageUrl || property.ConceptualDataType == ConceptualPrimitiveType.Binary);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002906 File Offset: 0x00000B06
		public static IConceptualColumn AsColumn(this IConceptualProperty property)
		{
			return property as IConceptualColumn;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000290E File Offset: 0x00000B0E
		public static IConceptualMeasure AsMeasure(this IConceptualProperty property)
		{
			return property as IConceptualMeasure;
		}
	}
}
