using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000091 RID: 145
	internal sealed class QueryLimitGenerator : LimitVisitor<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator>
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x00019FE2 File Offset: 0x000181E2
		private QueryLimitGenerator()
		{
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00019FEA File Offset: 0x000181EA
		public static Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator Generate(Microsoft.DataShaping.InternalContracts.DataShapeQuery.LimitOperator dsqLimitOperator)
		{
			return new QueryLimitGenerator().Visit(dsqLimitOperator);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00019FF7 File Offset: 0x000181F7
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.TopLimitOperator dsqLimitOperator)
		{
			return new Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.TopLimitOperator(dsqLimitOperator.GetPaddedCount(), dsqLimitOperator.Skip);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001A00A File Offset: 0x0001820A
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.SampleLimitOperator dsqLimitOperator)
		{
			return new Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.SampleLimitOperator(dsqLimitOperator.Count.Value + 2);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001A020 File Offset: 0x00018220
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator Visit(FirstLimitOperator dsqLimitOperator)
		{
			return new Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.TopLimitOperator(dsqLimitOperator.Count.Value, null);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001A046 File Offset: 0x00018246
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator Visit(LastLimitOperator dsqLimitOperator)
		{
			throw new InvalidOperationException("Last limit operators should have been normalized into First limit operators by now.");
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001A054 File Offset: 0x00018254
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator Visit(BottomLimitOperator dsqLimitOperator)
		{
			return new Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.TopLimitOperator(dsqLimitOperator.Count.Value + 2, null);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001A07C File Offset: 0x0001827C
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator Visit(BinnedLineSampleLimitOperator dsqLimitOperator)
		{
			throw new InvalidOperationException("BinnedLineSampleLimitOperator is not supported in the regular query pattern");
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001A088 File Offset: 0x00018288
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator Visit(OverlappingPointsSampleLimitOperator dsqLimitOperator)
		{
			throw new InvalidOperationException("OverlappingPointsSampleLimitOperator is not supported in the regular query pattern");
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001A094 File Offset: 0x00018294
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator Visit(TopNPerLevelLimitOperator dsqLimitOperator)
		{
			throw new InvalidOperationException("TopNPerLevelLimitOperator is not supported in the regular query pattern");
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001A0A0 File Offset: 0x000182A0
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.LimitOperator Visit(WindowLimitOperator dsqLimitOperator)
		{
			throw new InvalidOperationException("WindowLimitOperator is not supported in the regular query pattern");
		}
	}
}
