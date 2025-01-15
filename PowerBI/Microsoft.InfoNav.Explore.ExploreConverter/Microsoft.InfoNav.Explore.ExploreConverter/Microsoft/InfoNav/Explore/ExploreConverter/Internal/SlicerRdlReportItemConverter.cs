using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200003E RID: 62
	internal sealed class SlicerRdlReportItemConverter : TableRdlReportItemConverter
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00008B98 File Offset: 0x00006D98
		public LayoutContext CreateLayoutContext(Tablix tablix)
		{
			return new LayoutContext
			{
				Columns = base.GetColumnsInfo(tablix)
			};
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008BAC File Offset: 0x00006DAC
		public override void SetOutputValues(IReportDeserializationContext ctx, PVVisual visual, ReportItem reportItem)
		{
			Tablix tablix = reportItem as Tablix;
			Contract.Check(tablix != null, "Expect reportItem to be Tablix");
			IFilterCondition<IRdmQueryExpression> slicerFilterCondition = tablix.SlicerFilterCondition;
			if (slicerFilterCondition != null)
			{
				RdmToDocumentConverter.SetFilterOutputProperty(visual, slicerFilterCondition);
			}
		}
	}
}
