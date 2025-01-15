using System;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.DataShapeQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.SemanticQueryTranslation.TranslateQuery
{
	// Token: 0x0200001C RID: 28
	internal static class TranslateSemanticQueryConfigHandler
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x000053EC File Offset: 0x000035EC
		internal static DaxTableGenerationOptions GetGenerationOptions(TranslateSemanticQueryConfigKind configKind)
		{
			if (configKind == TranslateSemanticQueryConfigKind.Default)
			{
				return TranslateSemanticQueryConfigHandler.DefaultTableGenOptions;
			}
			if (configKind != TranslateSemanticQueryConfigKind.ForDataView)
			{
				Contract.RetailFail("Unknown TranslateSemanticQueryConfigKind: " + configKind.ToString());
				return null;
			}
			return TranslateSemanticQueryConfigHandler.DataViewTableGenOptions;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005420 File Offset: 0x00003620
		// Note: this type is marked as 'beforefieldinit'.
		static TranslateSemanticQueryConfigHandler()
		{
			bool? flag = new bool?(true);
			TranslateSemanticQueryConfigHandler.DefaultDsqtOptions = new DataShapeQueryTranslationOptions(null, flag, false, false, false, false);
			TranslateSemanticQueryConfigHandler.DefaultTableGenOptions = new DaxTableGenerationOptions(TranslateSemanticQueryConfigHandler.DefaultDsqGenOptions, TranslateSemanticQueryConfigHandler.DefaultDsqtOptions);
			TranslateSemanticQueryConfigHandler.DataViewDsqGenOptions = new DataShapeGenerationOptions(null, false, false, true, true, false, AllowedExpressionContent.TopLevelQuerySelect);
			flag = new bool?(false);
			TranslateSemanticQueryConfigHandler.DataViewDsqtOptions = new DataShapeQueryTranslationOptions(null, flag, false, false, false, true);
			TranslateSemanticQueryConfigHandler.DataViewTableGenOptions = new DaxTableGenerationOptions(TranslateSemanticQueryConfigHandler.DataViewDsqGenOptions, TranslateSemanticQueryConfigHandler.DataViewDsqtOptions);
		}

		// Token: 0x04000057 RID: 87
		private static readonly DataShapeGenerationOptions DefaultDsqGenOptions = new DataShapeGenerationOptions(null, false, false, false, false, true, AllowedExpressionContent.TopLevelQuerySelect);

		// Token: 0x04000058 RID: 88
		private static readonly DataShapeQueryTranslationOptions DefaultDsqtOptions;

		// Token: 0x04000059 RID: 89
		private static readonly DaxTableGenerationOptions DefaultTableGenOptions;

		// Token: 0x0400005A RID: 90
		private static readonly DataShapeGenerationOptions DataViewDsqGenOptions;

		// Token: 0x0400005B RID: 91
		private static readonly DataShapeQueryTranslationOptions DataViewDsqtOptions;

		// Token: 0x0400005C RID: 92
		private static readonly DaxTableGenerationOptions DataViewTableGenOptions;
	}
}
