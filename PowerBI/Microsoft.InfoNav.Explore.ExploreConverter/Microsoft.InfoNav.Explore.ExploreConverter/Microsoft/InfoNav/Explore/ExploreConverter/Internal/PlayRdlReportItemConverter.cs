using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200003B RID: 59
	internal sealed class PlayRdlReportItemConverter : ContainerRdlReportItemConverter
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000840C File Offset: 0x0000660C
		public override void LoadDataContext(IReportDeserializationContext ctx, Tablix tablix, PVVisual visual)
		{
			DataContext dataContext = visual.DataContext;
			using (new DataSetScope(ctx, tablix))
			{
				Bucket bucket = new Bucket
				{
					Name = "Play",
					BucketItems = new List<BucketItem>(),
					Properties = new List<BucketProperty>()
				};
				dataContext.Buckets.Add(bucket);
				Bucket bucket2 = null;
				Bucket bucket3 = null;
				Navigation navigation = tablix.BandLayoutOptions.Navigation;
				base.RowIndex = 0;
				base.ColumnIndex = 0;
				Contract.Check(tablix.RowHierarchy.Members.Count == 1, "the row size of a PlayAxis should be 1");
				string dataField = navigation.GetDataField();
				base.AddExtractedFieldToBucketIfNotNull(bucket, ctx.GetCurrentDataSet(), dataField);
				Dictionary<ReportImageSource, int> dictionary = new Dictionary<ReportImageSource, int>();
				foreach (TablixMember tablixMember in tablix.ColumnHierarchy.Members)
				{
					base.ConvertTablixColumnHierarchy(ctx, tablix, visual, tablixMember, bucket3, bucket2, dictionary);
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008520 File Offset: 0x00006720
		public override void ConvertTablixBodyCell(IReportDeserializationContext ctx, TablixCell cell, Tablix tablix, PVVisual visual, Bucket valuesBucket)
		{
			ReportItem reportItem = cell.ReportItem;
			Contract.Check(reportItem.RdlTagName == "Rectangle", "PlayAxis must contain a Rectangle.");
			Rectangle rectangle = reportItem as Rectangle;
			Contract.Check(rectangle != null, "Rectangle should not be null");
			List<ReportItem> reportItems = rectangle.ReportItems;
			Contract.Check(reportItems.Count == 1, "PlayAxis rectangle must have one reportItem");
			ReportItem reportItem2 = reportItems[0];
			Contract.Check(reportItem2.RdlTagName == "Chart", "PlayAxis rectangle must contain a chart.");
			PVVisual pvvisual = RdmToDocumentConverter.CreateVisual(ctx, reportItem2, base.GetReportItemState(ctx, reportItem2), 0.0m);
			visual.AddVisual(pvvisual, true);
		}
	}
}
