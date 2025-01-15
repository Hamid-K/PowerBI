using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.AnalysisServices.Tabular.DDL;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.AdaptiveCaching
{
	// Token: 0x0200012C RID: 300
	internal static class AdaptiveCachingHelper
	{
		// Token: 0x06001484 RID: 5252 RVA: 0x0008B3F0 File Offset: 0x000895F0
		public static void ApplyAutomaticAggregations(Model model, AutomaticAggregationOptions options)
		{
			if (model.Server == null || !model.Server.Connected)
			{
				throw new InvalidOperationException(TomSR.Exception_ApplyAutomaticAggregationsInOfflineMode);
			}
			CompatibilityMode compatibilityMode = model.Server.CompatibilityMode;
			int compatibilityLevel = model.Database.GetCompatibilityLevel();
			if (!CompatibilityRestrictions.Feature_AdaptiveCaching.IsCompatible(compatibilityMode, compatibilityLevel))
			{
				throw new InvalidOperationException(TomSR.Exception_ApplyAutomaticAggregationsInvalidCompatLevel(compatibilityLevel.ToString(), CompatibilityRestrictions.Feature_AdaptiveCaching[compatibilityMode].ToString()));
			}
			DataSet adaptiveCachingRecommendations = DdlUtil.GetAdaptiveCachingRecommendations(model, options);
			Dictionary<QueryShape, Table> dictionary = new Dictionary<QueryShape, Table>();
			foreach (object obj in adaptiveCachingRecommendations.Tables["Table"].Rows)
			{
				string text = (string)((DataRow)obj)["[RecommendedAggregation]"];
				Table table = AdaptiveCachingHelper.ConstructSummaryTable(model, JsonConvert.DeserializeObject<QueryGraph>(text));
				dictionary.Add(AdaptiveCachingHelper.GetQueryShape(table), table);
			}
			AdaptiveCachingHelper.ApplyRecommendations(model, dictionary);
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0008B504 File Offset: 0x00089704
		internal static void ApplyRecommendations(Model model, IDictionary<QueryShape, Table> recommendations)
		{
			int num = model.Tables.Count;
			while (--num >= 0)
			{
				Table table = model.Tables[num];
				if (table.SystemManaged)
				{
					QueryShape queryShape = AdaptiveCachingHelper.GetQueryShape(table);
					Table table2;
					if (!recommendations.TryGetValue(queryShape, out table2))
					{
						model.Tables.Remove(table);
					}
					else
					{
						recommendations.Remove(queryShape);
						table.Annotations.Clear();
						foreach (Annotation annotation in table2.Annotations)
						{
							table.Annotations.Add(new Annotation
							{
								Name = annotation.Name,
								Value = annotation.Value
							});
						}
					}
				}
			}
			foreach (Table table3 in recommendations.Values)
			{
				model.Tables.Add(table3);
			}
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0008B62C File Offset: 0x0008982C
		internal static Table ConstructSummaryTable(Model model, QueryGraph queryGraph)
		{
			Table table = new Table
			{
				Name = Guid.NewGuid().ToString(),
				IsHidden = true,
				SystemManaged = true
			};
			foreach (QueryColumn queryColumn in queryGraph.QueryShape.Columns)
			{
				if (queryColumn.Aggregation == null)
				{
					Column column = model.Tables[queryColumn.Table].Columns[queryColumn.Column];
					table.Columns.Add(new DataColumn
					{
						ExplicitName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", column.Table.Name, column.Name),
						ExplicitDataType = column.DataType,
						IsAvailableInMDX = false,
						AlternateOf = new AlternateOf
						{
							Summarization = SummarizationType.GroupBy,
							BaseColumn = column
						}
					});
				}
				else
				{
					AggregationType? aggregationType = queryColumn.Aggregation;
					AggregationType aggregationType2 = AggregationType.CountRows;
					if ((aggregationType.GetValueOrDefault() == aggregationType2) & (aggregationType != null))
					{
						Table table2 = model.Tables[queryColumn.Table];
						table.Columns.Add(new DataColumn
						{
							ExplicitName = table.Columns.Count.ToString(CultureInfo.InvariantCulture),
							ExplicitDataType = DataType.Int64,
							IsAvailableInMDX = false,
							AlternateOf = new AlternateOf
							{
								Summarization = SummarizationType.Count,
								BaseTable = table2
							}
						});
					}
					else
					{
						Column column2 = model.Tables[queryColumn.Table].Columns[queryColumn.Column];
						aggregationType = queryColumn.Aggregation;
						aggregationType2 = AggregationType.Count;
						DataType dataType = (((aggregationType.GetValueOrDefault() == aggregationType2) & (aggregationType != null)) ? DataType.Int64 : column2.DataType);
						table.Columns.Add(new DataColumn
						{
							ExplicitName = table.Columns.Count.ToString(CultureInfo.InvariantCulture),
							ExplicitDataType = dataType,
							IsAvailableInMDX = false,
							AlternateOf = new AlternateOf
							{
								Summarization = AdaptiveCachingHelper.AggregationTypeToSummarizationType(queryColumn.Aggregation),
								BaseColumn = column2
							}
						});
					}
				}
			}
			table.Annotations.Add(new Annotation
			{
				Name = "activeFrequency",
				Value = XmlConvert.ToString(queryGraph.CoveredFrequency)
			});
			table.Annotations.Add(new Annotation
			{
				Name = "activeQueries",
				Value = XmlConvert.ToString(queryGraph.CoveredPatterns)
			});
			table.Annotations.Add(new Annotation
			{
				Name = "relativeGain(%)",
				Value = XmlConvert.ToString(queryGraph.RelativeGain)
			});
			table.Annotations.Add(new Annotation
			{
				Name = "relativeLift(%)",
				Value = XmlConvert.ToString(queryGraph.RelativeLift)
			});
			table.Annotations.Add(new Annotation
			{
				Name = "relativeSize(%)",
				Value = XmlConvert.ToString(queryGraph.RelativeSize)
			});
			table.Annotations.Add(new Annotation
			{
				Name = "estimatedCardinality(Krows)",
				Value = XmlConvert.ToString(queryGraph.RowCount)
			});
			table.Annotations.Add(new Annotation
			{
				Name = "avoidQueryHints",
				Value = XmlConvert.ToString(queryGraph.AvoidQueryHints)
			});
			table.Partitions.Add(new Partition
			{
				Source = new InferredPartitionSource(),
				Mode = ModeType.Import
			});
			return table;
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0008B9F4 File Offset: 0x00089BF4
		internal static QueryShape GetQueryShape(Table summaryTable)
		{
			QueryShape queryShape = new QueryShape
			{
				Columns = new List<QueryColumn>()
			};
			foreach (Column column in summaryTable.Columns.Where((Column c) => c.Type != ColumnType.RowNumber))
			{
				if (column.AlternateOf.Summarization == SummarizationType.GroupBy)
				{
					queryShape.Columns.Add(new QueryColumn
					{
						Table = column.AlternateOf.BaseColumn.Table.Name,
						Column = column.AlternateOf.BaseColumn.Name
					});
				}
				else if (column.AlternateOf.BaseColumn == null)
				{
					queryShape.Columns.Add(new QueryColumn
					{
						Aggregation = new AggregationType?(AggregationType.CountRows),
						Table = column.AlternateOf.BaseTable.Name
					});
				}
				else
				{
					queryShape.Columns.Add(new QueryColumn
					{
						Aggregation = new AggregationType?(AdaptiveCachingHelper.SummarizationTypeToAggregationType(column.AlternateOf.Summarization)),
						Table = column.AlternateOf.BaseColumn.Table.Name,
						Column = column.AlternateOf.BaseColumn.Name
					});
				}
			}
			return queryShape;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0008BB90 File Offset: 0x00089D90
		private static AggregationType SummarizationTypeToAggregationType(SummarizationType summarizationType)
		{
			switch (summarizationType)
			{
			case SummarizationType.Sum:
				return AggregationType.Sum;
			case SummarizationType.Count:
				return AggregationType.Count;
			case SummarizationType.Min:
				return AggregationType.Min;
			case SummarizationType.Max:
				return AggregationType.Max;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0008BBBC File Offset: 0x00089DBC
		private static SummarizationType AggregationTypeToSummarizationType(AggregationType? aggregationType)
		{
			if (aggregationType == null)
			{
				return SummarizationType.GroupBy;
			}
			switch (aggregationType.Value)
			{
			case AggregationType.Sum:
				return SummarizationType.Sum;
			case AggregationType.Count:
			case AggregationType.CountRows:
				return SummarizationType.Count;
			case AggregationType.Min:
				return SummarizationType.Min;
			case AggregationType.Max:
				return SummarizationType.Max;
			default:
				throw new NotImplementedException();
			}
		}
	}
}
