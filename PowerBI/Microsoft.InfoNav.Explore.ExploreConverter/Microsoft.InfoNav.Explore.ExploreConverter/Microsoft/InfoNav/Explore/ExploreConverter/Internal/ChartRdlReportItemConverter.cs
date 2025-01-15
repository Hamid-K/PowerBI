using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200002E RID: 46
	internal class ChartRdlReportItemConverter : BaseRdlReportItemConverter
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00006D94 File Offset: 0x00004F94
		public override bool SupportsTargetedScopeFilters(PVVisual visual)
		{
			return visual.DataContext.Buckets.Where((Bucket bucket) => bucket.Name == "Category" || bucket.Name == "Series").Any((Bucket bucket) => bucket.BucketItems.Count > 0) || (visual.ParentIsDataContainer && visual.ParentVisual.Type == "SmallMultiple");
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006E18 File Offset: 0x00005018
		public override void Load(IReportDeserializationContext ctx, ReportItem reportItem, PVVisual visual)
		{
			Chart chart = reportItem as Chart;
			Contract.Check(chart != null, "Expect chart to not be null");
			LayoutContext layoutContext = this.CreateLayoutContext(chart);
			visual.LayoutContext = layoutContext;
			this.LoadDataContext(ctx, chart, visual.DataContext);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006E58 File Offset: 0x00005058
		public void AddChartMemberToBucket(IReportDeserializationContext ctx, ChartMember member, Bucket bucket)
		{
			if (member != null)
			{
				Group group = member.Group;
				if (group != null)
				{
					Expression expression = null;
					Label label = member.Label;
					if (label != null)
					{
						expression = label.Expression;
					}
					else
					{
						List<GroupExpression> groupExpressions = group.GroupExpressions;
						if (groupExpressions.Count > 0)
						{
							Contract.Check((long)groupExpressions.Count == 1L, "Composite groups are not supported yet");
							expression = groupExpressions[0].Expression;
						}
					}
					Contract.Check(expression != null, "Invalid ChartMember.  Neither a Label or GroupExpression on a group");
					base.AddToBucketIfNotNull(bucket, ctx.GetCurrentDataSet(), expression);
				}
				List<ChartMember> chartMembers = member.ChartMembers;
				if (chartMembers != null && chartMembers.Count > 0)
				{
					this.AddChartMemberToBucket(ctx, chartMembers[0], bucket);
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006F00 File Offset: 0x00005100
		public virtual LayoutContext CreateLayoutContext(Chart chart)
		{
			ChartLayoutType chartLayoutType = ChartRdlReportItemConverter.ConvertChartType(chart.Type, chart.Subtype);
			return new LayoutContext
			{
				ChartLayoutType = chartLayoutType.ToString(),
				LegendPosition = chart.LegendPosition.ToString(),
				IsLegendHidden = new bool?(chart.LegendHidden),
				AreLabelsVisible = new bool?(chart.IsLabelsVisible),
				LabelsPosition = chart.LabelsPositions.ToString(),
				IsChartTitleHidden = new bool?(chart.ChartTitleHidden)
			};
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006FA0 File Offset: 0x000051A0
		public static ChartLayoutType ConvertChartType(string chartType, string chartSubtype)
		{
			ChartLayoutType chartLayoutType;
			if (chartType == "Bar")
			{
				if (chartSubtype == "Stacked")
				{
					chartLayoutType = ChartLayoutType.BarStacked;
				}
				else if (chartSubtype == "PercentStacked")
				{
					chartLayoutType = ChartLayoutType.BarPercentStacked;
				}
				else
				{
					chartLayoutType = ChartLayoutType.Bar;
				}
			}
			else if (chartType == "Funnel")
			{
				chartLayoutType = ChartLayoutType.Funnel;
			}
			else if (chartType == "Line")
			{
				chartLayoutType = ChartLayoutType.Line;
			}
			else if (chartType == "Scatter")
			{
				if (chartSubtype == "Map")
				{
					chartLayoutType = ChartLayoutType.Map;
				}
				else
				{
					chartLayoutType = ChartLayoutType.Scatter;
				}
			}
			else if (chartType == "Shape")
			{
				if (chartSubtype == "DecisionTree")
				{
					chartLayoutType = ChartLayoutType.DecisionTree;
				}
				else if (chartSubtype == "TreeMap")
				{
					chartLayoutType = ChartLayoutType.TreeMap;
				}
				else if (chartSubtype == "Sunburst")
				{
					chartLayoutType = ChartLayoutType.Sunburst;
				}
				else
				{
					chartLayoutType = ChartLayoutType.Pie;
				}
			}
			else if (chartSubtype == "Stacked")
			{
				chartLayoutType = ChartLayoutType.ColumnStacked;
			}
			else if (chartSubtype == "PercentStacked")
			{
				chartLayoutType = ChartLayoutType.ColumnPercentStacked;
			}
			else
			{
				chartLayoutType = ChartLayoutType.Column;
			}
			return chartLayoutType;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000070A3 File Offset: 0x000052A3
		public virtual void LoadDataContext(IReportDeserializationContext ctx, Chart chart, DataContext dataContext)
		{
		}
	}
}
