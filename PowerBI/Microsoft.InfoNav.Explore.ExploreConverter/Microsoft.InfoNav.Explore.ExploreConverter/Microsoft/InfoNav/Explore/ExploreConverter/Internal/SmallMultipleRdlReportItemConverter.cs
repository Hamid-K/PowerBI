using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000040 RID: 64
	internal sealed class SmallMultipleRdlReportItemConverter : ContainerRdlReportItemConverter
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00008C4C File Offset: 0x00006E4C
		public override bool SupportsTargetedScopeFilters(PVVisual visual)
		{
			return visual.DataContext.Buckets.Where((Bucket bucket) => bucket.Name == "RowHierarchy" || bucket.Name == "ColumnHierarchy").Any((Bucket bucket) => bucket.BucketItems.Count > 0);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008CAC File Offset: 0x00006EAC
		public override void LoadDataContext(IReportDeserializationContext ctx, Tablix tablix, PVVisual visual)
		{
			DataContext dataContext = visual.DataContext;
			using (new DataSetScope(ctx, tablix))
			{
				Bucket bucket = new Bucket
				{
					Name = "VerticalHierarchy",
					BucketItems = new List<BucketItem>(),
					Properties = new List<BucketProperty>()
				};
				dataContext.Buckets.Add(bucket);
				Bucket bucket2 = new Bucket
				{
					Name = "HorizontalHierarchy",
					BucketItems = new List<BucketItem>(),
					Properties = new List<BucketProperty>()
				};
				dataContext.Buckets.Add(bucket2);
				Bucket bucket3 = null;
				base.RowIndex = 0;
				base.ColumnIndex = 0;
				List<TablixMember> members = tablix.RowHierarchy.Members;
				Dictionary<ReportImageSource, int> dictionary = new Dictionary<ReportImageSource, int>();
				foreach (TablixMember tablixMember in members)
				{
					base.ConvertTablixRowHierarchy(ctx, tablix, visual, tablixMember, bucket3, bucket, bucket2, dictionary);
				}
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008DB4 File Offset: 0x00006FB4
		public override void ConvertTablixBodyCell(IReportDeserializationContext ctx, TablixCell cell, Tablix tablix, PVVisual visual, Bucket valuesBucket)
		{
			ReportItem reportItem = cell.ReportItem;
			Contract.Check(reportItem.RdlTagName == "Chart", "Small multiple must contain a chart.");
			PVVisual pvvisual = RdmToDocumentConverter.CreateVisual(ctx, reportItem, null, 0.0m);
			visual.AddVisual(pvvisual, true);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00008E00 File Offset: 0x00007000
		public override void SetOutputValues(IReportDeserializationContext ctx, PVVisual visual, ReportItem reportItem)
		{
			Tablix tablix = reportItem as Tablix;
			using (new DataSetScope(ctx, tablix))
			{
				DataSet currentDataSet = ctx.GetCurrentDataSet();
				TablixHierarchy rowHierarchy = tablix.RowHierarchy;
				Contract.Check(rowHierarchy != null, "RowHierarchy of Matrix must not be null");
				Dictionary<Formula, SortDirection> dictionary = new Dictionary<Formula, SortDirection>();
				base.LoadSortsFromMembers(rowHierarchy.Members, ctx, currentDataSet, dictionary);
				TablixHierarchy columnHierarchy = tablix.ColumnHierarchy;
				if (columnHierarchy != null)
				{
					base.LoadSortsFromMembers(columnHierarchy.Members, ctx, currentDataSet, dictionary);
				}
				if (dictionary.Count > 0)
				{
					List<PVProperty> properties = visual.Properties;
					PVProperty pvproperty = new PVProperty();
					pvproperty.Name = "SelectedSort";
					CustomPVProperties customPVProperties = new CustomPVProperties();
					customPVProperties.SortValue = (from kvp in dictionary.ToList<KeyValuePair<Formula, SortDirection>>()
						select new PVPropertyValue
						{
							Direction = kvp.Value.ToString(),
							Formula = kvp.Key
						}).ToList<PVPropertyValue>();
					pvproperty.Value = customPVProperties;
					properties.Add(pvproperty);
				}
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008EF0 File Offset: 0x000070F0
		protected override PVVisual ResolveVisualToSetDrillStateTo(PVVisual visualWithDrillStateSource)
		{
			IEnumerable<PVVisual> enumerable = visualWithDrillStateSource.Visuals.Where((PVVisual visual) => visual.ParentIsDataContainer);
			Contract.Check(enumerable.Count<PVVisual>() == 1, "Expected a nested data region and a filter container in a data container visual.");
			return enumerable.First<PVVisual>();
		}
	}
}
