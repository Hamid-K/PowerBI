using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200002D RID: 45
	internal class CategoryChartRdlReportItemConverter : ChartRdlReportItemConverter
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public override void SetOutputValues(IReportDeserializationContext ctx, PVVisual visual, ReportItem reportItem)
		{
			Chart chart = reportItem as Chart;
			using (new DataSetScope(ctx, chart))
			{
				DataSet currentDataSet = ctx.GetCurrentDataSet();
				ChartCategoryHierarchy categoryHierarchy = chart.CategoryHierarchy;
				Dictionary<Formula, SortDirection> dictionary = new Dictionary<Formula, SortDirection>();
				List<ChartMember> list = categoryHierarchy.ChartMembers;
				while (list != null && list.Count > 0)
				{
					ChartMember chartMember = list.First<ChartMember>();
					DataSet dataSet = currentDataSet;
					Group group = chartMember.Group;
					if (group != null && !string.IsNullOrEmpty(group.DataSetName))
					{
						using (new DataSetScope(ctx, group))
						{
							dataSet = ctx.GetCurrentDataSet();
						}
					}
					Dictionary<Formula, SortDirection> dictionary2 = this.ToSorts(dataSet, chartMember.SortExpressions);
					base.AppendUniqueSorts(dictionary, dictionary2);
					list = chartMember.ChartMembers;
				}
				if (dictionary.Count > 0)
				{
					List<PVProperty> properties = visual.Properties;
					PVProperty pvproperty = new PVProperty();
					pvproperty.Name = "SelectedSort";
					CustomPVProperties customPVProperties = new CustomPVProperties();
					customPVProperties.SortValue = dictionary.Select((KeyValuePair<Formula, SortDirection> sort) => new PVPropertyValue
					{
						Direction = sort.Value.ToString(),
						Formula = sort.Key
					}).ToList<PVPropertyValue>();
					pvproperty.Value = customPVProperties;
					properties.Add(pvproperty);
				}
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006BF0 File Offset: 0x00004DF0
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
					Name = "Y",
					Properties = new List<BucketProperty>(),
					BucketItems = new List<BucketItem>()
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
	}
}
