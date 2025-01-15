using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.QueryDesignModel.Common;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200014A RID: 330
	internal sealed class DaxTopNPerLevelTranslator
	{
		// Token: 0x060011BA RID: 4538 RVA: 0x00031585 File Offset: 0x0002F785
		private DaxTopNPerLevelTranslator(QueryTopNPerLevelSampleExpression queryTopNPerLevelSampleExpression, DaxTransform daxTransform)
		{
			this._queryTopNPerLevelSampleExpression = queryTopNPerLevelSampleExpression;
			this._daxTransform = daxTransform;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0003159B File Offset: 0x0002F79B
		internal static DaxExpression Translate(QueryTopNPerLevelSampleExpression expression, DaxTransform daxTransform)
		{
			return new DaxTopNPerLevelTranslator(expression, daxTransform).Translate();
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000315AC File Offset: 0x0002F7AC
		private DaxExpression Translate()
		{
			DaxExpression inputDax = this._queryTopNPerLevelSampleExpression.Input.Expression.Accept<DaxExpression>(this._daxTransform);
			DaxExpression levelsDax = null;
			Tuple<DaxExpression, DaxExpression> pathAndWindowDax = null;
			inputDax = this._daxTransform.EvaluateInScope<DaxExpression>(this._queryTopNPerLevelSampleExpression.Input, delegate(DaxExpression table)
			{
				levelsDax = this.GenerateLevelsExpression(this._queryTopNPerLevelSampleExpression.Levels, inputDax);
				pathAndWindowDax = this.GeneratePathAndWindowsExpressions(this._queryTopNPerLevelSampleExpression.WindowExpansion);
				return table;
			}, false);
			DaxExpression daxExpression = this._queryTopNPerLevelSampleExpression.Count.Accept<DaxExpression>(this._daxTransform);
			DaxResultColumn daxResultColumn = new DaxResultColumn(this._queryTopNPerLevelSampleExpression.RestartIndicatorColumnName, DaxRef.Column(DaxUniqueNameGenerator.MakeUniqueColumnName(this._queryTopNPerLevelSampleExpression.RestartIndicatorColumnName, inputDax.ResultColumns)));
			return DaxFunctions.TopNPerLevel(daxExpression, inputDax, levelsDax, pathAndWindowDax.Item1, pathAndWindowDax.Item2, daxResultColumn);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00031690 File Offset: 0x0002F890
		private DaxExpression GenerateLevelsExpression(IReadOnlyList<TopNPerLevelLevelRow> levels, DaxExpression inputDax)
		{
			List<DaxResultColumn> value = DaxTopNPerLevelTranslator._levelsTableColumnType.Value;
			DaxDataTableStringBuilder daxDataTableStringBuilder = new DaxDataTableStringBuilder(value.Count);
			daxDataTableStringBuilder.Begin();
			foreach (TopNPerLevelLevelRow topNPerLevelLevelRow in levels)
			{
				DaxExpression daxExpression = topNPerLevelLevelRow.LevelId.Accept<DaxExpression>(this._daxTransform);
				DaxExpression daxExpression2 = DaxLiteral.FromString((topNPerLevelLevelRow.SubtotalName == null) ? string.Empty : topNPerLevelLevelRow.SubtotalName.Accept<DaxExpression>(this._daxTransform).Text);
				if (topNPerLevelLevelRow.SortByItems != null)
				{
					using (IEnumerator<QuerySortClause> enumerator2 = topNPerLevelLevelRow.SortByItems.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							QuerySortClause querySortClause = enumerator2.Current;
							daxDataTableStringBuilder.BeginRow();
							daxDataTableStringBuilder.AppendColumn(daxExpression.Text);
							daxDataTableStringBuilder.AppendColumn(daxExpression2.Text);
							string text;
							DaxExpression daxExpression3 = DaxLiteral.FromString(DaxFunctions.GetDaxSortArguments(inputDax, querySortClause, false, out text));
							daxDataTableStringBuilder.AppendColumn(daxExpression3.Text);
							daxDataTableStringBuilder.AppendColumn(text);
							daxDataTableStringBuilder.EndRow();
						}
						continue;
					}
				}
				daxDataTableStringBuilder.BeginRow();
				daxDataTableStringBuilder.AppendColumn(daxExpression.Text);
				daxDataTableStringBuilder.AppendColumn(daxExpression2.Text);
				daxDataTableStringBuilder.AppendColumn(DaxLiteral.FromString(string.Empty).Text);
				daxDataTableStringBuilder.AppendColumn(DaxFunctions.ToDaxAsNumericDirection(SortDirection.Ascending));
				daxDataTableStringBuilder.EndRow();
			}
			daxDataTableStringBuilder.End();
			return DaxExpression.Table(daxDataTableStringBuilder.ToDax(), value, false);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00031838 File Offset: 0x0002FA38
		private Tuple<DaxExpression, DaxExpression> GeneratePathAndWindowsExpressions(QueryTopNPerLevelWindowExpansion windowExpansionInfo)
		{
			int num = 0;
			int num2 = 0;
			this._columnNameCache = new Dictionary<QueryExpression, DaxExpression>();
			this._pathValueExpressions = new List<DaxExpression>();
			this._windowValueExpressions = new List<DaxExpression>();
			if (windowExpansionInfo != null)
			{
				this.Traverse(windowExpansionInfo, num, num2);
			}
			DaxExpression daxExpressionWithUnionVariant = this.GetDaxExpressionWithUnionVariant(this._pathValueExpressions, DaxTopNPerLevelTranslator._pathTableColumnType.Value);
			DaxExpression daxExpressionWithUnionVariant2 = this.GetDaxExpressionWithUnionVariant(this._windowValueExpressions, DaxTopNPerLevelTranslator._windowTableColumnType.Value);
			return new Tuple<DaxExpression, DaxExpression>(daxExpressionWithUnionVariant, daxExpressionWithUnionVariant2);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000318AC File Offset: 0x0002FAAC
		private DaxExpression GetDaxExpressionWithUnionVariant(List<DaxExpression> tableExpressions, List<DaxResultColumn> tableColumnType)
		{
			int count = tableExpressions.Count;
			DaxExpression daxExpression;
			if (count != 0)
			{
				if (count != 1)
				{
					daxExpression = DaxFunctions.UnionVariant(tableExpressions.ToArray());
				}
				else
				{
					daxExpression = tableExpressions.First<DaxExpression>();
				}
			}
			else
			{
				DaxDataTableStringBuilder daxDataTableStringBuilder = new DaxDataTableStringBuilder(tableColumnType.Count);
				daxDataTableStringBuilder.Begin();
				daxDataTableStringBuilder.End();
				daxExpression = DaxExpression.Table(daxDataTableStringBuilder.ToDax(), tableColumnType, false);
			}
			return daxExpression;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00031908 File Offset: 0x0002FB08
		private void Traverse(QueryTopNPerLevelWindowExpansion windowExpansionInfo, int levelId, int pathId)
		{
			this.BuildPathAndWindowTable(windowExpansionInfo.Values, windowExpansionInfo.WindowValues, levelId, pathId);
			if (!windowExpansionInfo.Children.IsNullOrEmpty<QueryTopNPerLevelWindowExpansion>())
			{
				foreach (QueryTopNPerLevelWindowExpansion queryTopNPerLevelWindowExpansion in windowExpansionInfo.Children)
				{
					this.Traverse(queryTopNPerLevelWindowExpansion, levelId + 1, pathId);
					pathId++;
				}
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00031980 File Offset: 0x0002FB80
		private void BuildPathAndWindowTable(IReadOnlyList<QueryExpression> values, IReadOnlyList<QueryTopNPerLevelWindowExpansionValue> windowValues, int levelId, int pathId)
		{
			if (!values.IsNullOrEmpty<QueryExpression>())
			{
				List<DaxResultColumn> value = DaxTopNPerLevelTranslator._pathTableColumnType.Value;
				for (int i = 0; i < values.Count; i++)
				{
					DaxDataTableStringBuilder daxDataTableStringBuilder = new DaxDataTableStringBuilder(value.Count);
					daxDataTableStringBuilder.Begin();
					daxDataTableStringBuilder.BeginRow();
					daxDataTableStringBuilder.AppendColumn(pathId.ToString());
					daxDataTableStringBuilder.AppendColumn(DaxLiteral.FromInt32(levelId).Text);
					QueryExpression queryExpression = this._queryTopNPerLevelSampleExpression.Levels[levelId].ValueColumns[i];
					daxDataTableStringBuilder.AppendColumn(this.GetDaxLiteralExpressionForColumn(queryExpression).Text);
					daxDataTableStringBuilder.AppendColumn(values[i].Accept<DaxExpression>(this._daxTransform).Text);
					daxDataTableStringBuilder.EndRow();
					daxDataTableStringBuilder.End();
					DaxExpression daxExpression = DaxExpression.Table(daxDataTableStringBuilder.ToDax(), value, false);
					this._pathValueExpressions.Add(daxExpression);
				}
			}
			if (!windowValues.IsNullOrEmpty<QueryTopNPerLevelWindowExpansionValue>())
			{
				List<DaxResultColumn> value2 = DaxTopNPerLevelTranslator._windowTableColumnType.Value;
				foreach (QueryTopNPerLevelWindowExpansionValue queryTopNPerLevelWindowExpansionValue in windowValues)
				{
					for (int j = 0; j < queryTopNPerLevelWindowExpansionValue.Values.Count; j++)
					{
						DaxDataTableStringBuilder daxDataTableStringBuilder2 = new DaxDataTableStringBuilder(value2.Count);
						daxDataTableStringBuilder2.Begin();
						daxDataTableStringBuilder2.BeginRow();
						daxDataTableStringBuilder2.AppendColumn(pathId.ToString());
						daxDataTableStringBuilder2.AppendColumn(DaxLiteral.FromInt32(levelId + 1).Text);
						QueryExpression queryExpression2 = this._queryTopNPerLevelSampleExpression.Levels[levelId + 1].WindowValueColumns[j];
						daxDataTableStringBuilder2.AppendColumn(this.GetDaxLiteralExpressionForColumn(queryExpression2).Text);
						daxDataTableStringBuilder2.AppendColumn(queryTopNPerLevelWindowExpansionValue.Values[j].Accept<DaxExpression>(this._daxTransform).Text);
						daxDataTableStringBuilder2.AppendColumn(((int)queryTopNPerLevelWindowExpansionValue.WindowKind).ToString());
						daxDataTableStringBuilder2.EndRow();
						daxDataTableStringBuilder2.End();
						DaxExpression daxExpression2 = DaxExpression.Table(daxDataTableStringBuilder2.ToDax(), value2, false);
						this._windowValueExpressions.Add(daxExpression2);
					}
				}
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00031BB4 File Offset: 0x0002FDB4
		private DaxExpression GetDaxLiteralExpressionForColumn(QueryExpression column)
		{
			DaxExpression daxExpression;
			if (this._columnNameCache.TryGetValue(column, out daxExpression))
			{
				return daxExpression;
			}
			DaxExpression daxExpression2 = DaxLiteral.FromString(column.Accept<DaxExpression>(this._daxTransform).Text);
			this._columnNameCache[column] = daxExpression2;
			return daxExpression2;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00031BF8 File Offset: 0x0002FDF8
		private static List<DaxResultColumn> GetLevelsTableColumnType()
		{
			string text = "LevelId";
			string text2 = "SubTotatalName";
			string text3 = "SortByKey";
			string text4 = "SortDirection";
			return new List<DaxResultColumn>
			{
				new DaxResultColumn(text, new DaxColumnRef(text, DaxTableRef.Empty)),
				new DaxResultColumn(text2, new DaxColumnRef(text2, DaxTableRef.Empty)),
				new DaxResultColumn(text3, new DaxColumnRef(text3, DaxTableRef.Empty)),
				new DaxResultColumn(text4, new DaxColumnRef(text4, DaxTableRef.Empty))
			};
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00031C80 File Offset: 0x0002FE80
		private static List<DaxResultColumn> GetPathTableColumnType()
		{
			string text = "PathId";
			string text2 = "LevelId";
			string text3 = "ColumnName";
			string text4 = "ColumnValue";
			return new List<DaxResultColumn>
			{
				new DaxResultColumn(text, new DaxColumnRef(text, DaxTableRef.Empty)),
				new DaxResultColumn(text2, new DaxColumnRef(text2, DaxTableRef.Empty)),
				new DaxResultColumn(text3, new DaxColumnRef(text3, DaxTableRef.Empty)),
				new DaxResultColumn(text4, new DaxColumnRef(text4, DaxTableRef.Empty))
			};
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00031D08 File Offset: 0x0002FF08
		private static List<DaxResultColumn> GetWindowTableColumnType()
		{
			string text = "PathId";
			string text2 = "LevelId";
			string text3 = "ColumnName";
			string text4 = "WindowValue";
			string text5 = "RestartKind";
			return new List<DaxResultColumn>
			{
				new DaxResultColumn(text, new DaxColumnRef(text, DaxTableRef.Empty)),
				new DaxResultColumn(text2, new DaxColumnRef(text2, DaxTableRef.Empty)),
				new DaxResultColumn(text3, new DaxColumnRef(text3, DaxTableRef.Empty)),
				new DaxResultColumn(text4, new DaxColumnRef(text4, DaxTableRef.Empty)),
				new DaxResultColumn(text5, new DaxColumnRef(text5, DaxTableRef.Empty))
			};
		}

		// Token: 0x04000AE1 RID: 2785
		private static Lazy<List<DaxResultColumn>> _levelsTableColumnType = new Lazy<List<DaxResultColumn>>(() => DaxTopNPerLevelTranslator.GetLevelsTableColumnType());

		// Token: 0x04000AE2 RID: 2786
		private static Lazy<List<DaxResultColumn>> _pathTableColumnType = new Lazy<List<DaxResultColumn>>(() => DaxTopNPerLevelTranslator.GetPathTableColumnType());

		// Token: 0x04000AE3 RID: 2787
		private static Lazy<List<DaxResultColumn>> _windowTableColumnType = new Lazy<List<DaxResultColumn>>(() => DaxTopNPerLevelTranslator.GetWindowTableColumnType());

		// Token: 0x04000AE4 RID: 2788
		private readonly QueryTopNPerLevelSampleExpression _queryTopNPerLevelSampleExpression;

		// Token: 0x04000AE5 RID: 2789
		private readonly DaxTransform _daxTransform;

		// Token: 0x04000AE6 RID: 2790
		private IDictionary<QueryExpression, DaxExpression> _columnNameCache;

		// Token: 0x04000AE7 RID: 2791
		private List<DaxExpression> _pathValueExpressions;

		// Token: 0x04000AE8 RID: 2792
		private List<DaxExpression> _windowValueExpressions;
	}
}
