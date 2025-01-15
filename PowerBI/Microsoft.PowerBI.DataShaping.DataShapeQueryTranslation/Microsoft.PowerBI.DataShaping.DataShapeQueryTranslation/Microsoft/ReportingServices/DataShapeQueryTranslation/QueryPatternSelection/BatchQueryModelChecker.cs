using System;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryPatternSelection
{
	// Token: 0x0200006A RID: 106
	internal static class BatchQueryModelChecker
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x00013E70 File Offset: 0x00012070
		internal static bool CheckDaxQueryBatchingSupport(IFederatedConceptualSchema schema, QueryPatternReasonCollection reasons)
		{
			DaxCapabilitiesAnnotation defaultSchemaDaxCapabilitiesAnnotation = schema.GetDefaultSchemaDaxCapabilitiesAnnotation();
			return true & reasons.CheckBatchPrerequisite(defaultSchemaDaxCapabilitiesAnnotation.SupportsQueryBatching, QueryPatternReason.QueryBatchingFeatureNotSupported) & reasons.CheckBatchPrerequisite(defaultSchemaDaxCapabilitiesAnnotation.SupportsVariables, QueryPatternReason.TableVariablesNotSupported) & reasons.CheckBatchPrerequisite(defaultSchemaDaxCapabilitiesAnnotation.DaxFunctions.SupportsLeftOuterJoin, QueryPatternReason.LeftOuterJoinFunctionNotSupported) & reasons.CheckBatchPrerequisite(defaultSchemaDaxCapabilitiesAnnotation.DaxFunctions.SupportsSubstituteWithIndex, QueryPatternReason.SubstituteWithIndexFunctionNotSupported) & reasons.CheckBatchPrerequisite(defaultSchemaDaxCapabilitiesAnnotation.DaxFunctions.SupportsSummarizeColumns, QueryPatternReason.SummarizeColumnsFunctionNotSupported);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00013EE0 File Offset: 0x000120E0
		internal static bool CheckMultiDimensionalModel(IFederatedConceptualSchema schema, QueryPatternReasonCollection reasons)
		{
			DaxCapabilitiesAnnotation defaultSchemaDaxCapabilitiesAnnotation = schema.GetDefaultSchemaDaxCapabilitiesAnnotation();
			return reasons.CheckBatchPrerequisite(!defaultSchemaDaxCapabilitiesAnnotation.IsMultidimensional(), QueryPatternReason.MultiDimensionalModel);
		}
	}
}
