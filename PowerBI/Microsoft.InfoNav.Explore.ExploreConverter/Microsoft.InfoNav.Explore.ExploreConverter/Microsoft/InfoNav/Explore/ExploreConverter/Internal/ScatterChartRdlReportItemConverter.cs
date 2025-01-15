using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200003D RID: 61
	internal class ScatterChartRdlReportItemConverter : ChartRdlReportItemConverter
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000897A File Offset: 0x00006B7A
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00008982 File Offset: 0x00006B82
		public int SeriesCount
		{
			get
			{
				return this._seriesCount;
			}
			set
			{
				this._seriesCount = value;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000898C File Offset: 0x00006B8C
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
					BucketItems = new List<BucketItem>(),
					Properties = new List<BucketProperty>()
				};
				dataContext.Buckets.Add(bucket3);
				Bucket bucket4 = new Bucket
				{
					Name = "X",
					BucketItems = new List<BucketItem>(),
					Properties = new List<BucketProperty>()
				};
				dataContext.Buckets.Add(bucket4);
				Bucket bucket5 = new Bucket
				{
					Name = "Size",
					BucketItems = new List<BucketItem>(),
					Properties = new List<BucketProperty>()
				};
				dataContext.Buckets.Add(bucket5);
				ChartData chartData = chart.ChartData;
				if (chartData != null)
				{
					foreach (ChartSeries chartSeries in chartData.ChartSeries)
					{
						List<ChartDataPoint> dataPoints = chartSeries.DataPoints;
						Contract.Check(dataPoints.Count == 1, "Expect only one datapoint for Scatter charts");
						ChartDataPoint chartDataPoint = dataPoints[0];
						base.AddToBucketIfNotNull(bucket4, currentDataSet, chartDataPoint.X);
						base.AddToBucketIfNotNull(bucket3, currentDataSet, chartDataPoint.Y);
						base.AddToBucketIfNotNull(bucket5, currentDataSet, chartDataPoint.Size);
					}
				}
				if (chart.ScalarXAxis)
				{
					bucket4.Properties.Add(new BucketProperty
					{
						Name = "ScalarAxis",
						Value = true
					});
				}
			}
		}

		// Token: 0x040000C3 RID: 195
		private int _seriesCount;
	}
}
