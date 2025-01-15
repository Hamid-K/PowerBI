using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000020 RID: 32
	internal static class ConceptualMeasureTemplateBuilder
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00004BAE File Offset: 0x00002DAE
		internal static ConceptualMeasureTemplate BuildMeasureTemplate(MeasureTemplate measureTemplate)
		{
			if (measureTemplate == null)
			{
				return null;
			}
			return new ConceptualMeasureTemplate(measureTemplate.DaxTemplateName);
		}

		// Token: 0x0400014D RID: 333
		internal const string MeasureTemplate = "MeasureTemplate";
	}
}
