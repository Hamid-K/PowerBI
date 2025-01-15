using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000031 RID: 49
	internal sealed class DecisionTreeRdlReportItemConverter : ChartRdlReportItemConverter
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00007140 File Offset: 0x00005340
		public override void LoadDataContext(IReportDeserializationContext ctx, Chart chart, DataContext dataContext)
		{
			using (new DataSetScope(ctx, chart))
			{
				DataSet currentDataSet = ctx.GetCurrentDataSet();
				Bucket bucket = base.CreateAndAddBucket(dataContext, "Category");
				base.AddChartMemberToBucket(ctx, chart.CategoryHierarchy.ChartMembers[0], bucket);
				Bucket bucket2 = base.CreateAndAddBucket(dataContext, "Series");
				base.AddChartMemberToBucket(ctx, chart.SeriesHierarchy.ChartMembers[0], bucket2);
				Bucket bucket3 = new Bucket
				{
					BucketItems = new List<BucketItem>(),
					Properties = new List<BucketProperty>(),
					Name = "Y"
				};
				dataContext.Buckets.Add(bucket3);
				ChartData chartData = chart.ChartData;
				if (chartData != null)
				{
					foreach (ChartSeries chartSeries in chartData.ChartSeries)
					{
						foreach (ChartDataPoint chartDataPoint in chartSeries.DataPoints)
						{
							base.AddToBucketIfNotNull(bucket3, currentDataSet, chartDataPoint.Y);
						}
					}
				}
				if (chart.ScalarXAxis)
				{
					bucket.Properties.Add(new BucketProperty
					{
						Name = "ScalarAxis",
						Value = true
					});
				}
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000072DC File Offset: 0x000054DC
		public override void SetOutputValues(IReportDeserializationContext ctx, PVVisual visual, ReportItem reportItem)
		{
			Chart chart = reportItem as Chart;
			using (new DataSetScope(ctx, chart))
			{
				DataSet currentDataSet = ctx.GetCurrentDataSet();
				ChartMember chartMember = chart.CategoryHierarchy.ChartMembers[0];
				if (chartMember != null)
				{
					Dictionary<Formula, SortDirection> dictionary = this.ToSorts(currentDataSet, chartMember.SortExpressions);
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
		}
	}
}
