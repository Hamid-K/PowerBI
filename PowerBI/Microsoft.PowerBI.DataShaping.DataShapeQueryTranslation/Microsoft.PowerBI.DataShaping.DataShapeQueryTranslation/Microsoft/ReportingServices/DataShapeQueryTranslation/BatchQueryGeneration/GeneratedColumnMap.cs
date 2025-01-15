using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.Reporting.QueryDesign.BatchQueries;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000152 RID: 338
	internal class GeneratedColumnMap
	{
		// Token: 0x06000C72 RID: 3186 RVA: 0x00033970 File Offset: 0x00031B70
		internal GeneratedColumnMap()
		{
			this.m_expressionMap = new Dictionary<ExpressionId, QueryTableColumn>();
			this.m_planColumnNameMap = new Dictionary<string, QueryTableColumn>(StringComparer.Ordinal);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00033993 File Offset: 0x00031B93
		protected GeneratedColumnMap(Dictionary<ExpressionId, QueryTableColumn> expressionMap, Dictionary<string, QueryTableColumn> planColumnNameMap)
		{
			this.m_expressionMap = expressionMap;
			this.m_planColumnNameMap = planColumnNameMap;
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000339A9 File Offset: 0x00031BA9
		public IReadOnlyDictionary<ExpressionId, QueryTableColumn> ExpressionMap
		{
			get
			{
				return this.m_expressionMap;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000339B1 File Offset: 0x00031BB1
		public IReadOnlyDictionary<string, QueryTableColumn> PlanColumnNameMap
		{
			get
			{
				return this.m_planColumnNameMap;
			}
		}

		// Token: 0x170001E3 RID: 483
		public QueryTableColumn this[ExpressionId expressionId]
		{
			get
			{
				return this.GetColumn<ExpressionId>(this.m_expressionMap, expressionId);
			}
		}

		// Token: 0x170001E4 RID: 484
		public QueryTableColumn this[string planColumnName]
		{
			get
			{
				return this.GetColumn<string>(this.m_planColumnNameMap, planColumnName);
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x000339D8 File Offset: 0x00031BD8
		private QueryTableColumn GetColumn<TKey>(Dictionary<TKey, QueryTableColumn> map, TKey key)
		{
			QueryTableColumn queryTableColumn;
			if (!map.TryGetValue(key, out queryTableColumn))
			{
				Contract.RetailFail("Missing expected column {0}", key.ToString().MarkAsExpressionContent().ToCustomerContentString());
				throw new InvalidOperationException("Missing expected column");
			}
			return queryTableColumn;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00033A1D File Offset: 0x00031C1D
		public bool TryGetColumn(string planColumnName, out QueryTableColumn column)
		{
			return this.m_planColumnNameMap.TryGetValue(planColumnName, out column);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00033A2C File Offset: 0x00031C2C
		public bool TryGetColumn(ExpressionId expressionId, out QueryTableColumn column)
		{
			return this.m_expressionMap.TryGetValue(expressionId, out column);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00033A3C File Offset: 0x00031C3C
		public WritableGeneratedColumnMap UnionColumns(GeneratedColumnMap otherMap, IEnumerable<QueryTableColumn> allowedColumns)
		{
			HashSet<QueryTableColumn> hashSet = new HashSet<QueryTableColumn>(allowedColumns);
			Dictionary<ExpressionId, QueryTableColumn> dictionary = new Dictionary<ExpressionId, QueryTableColumn>(this.m_expressionMap.Count);
			GeneratedColumnMap.FilterMap<ExpressionId>(this.ExpressionMap.Concat(otherMap.ExpressionMap), dictionary, hashSet);
			Dictionary<string, QueryTableColumn> dictionary2 = new Dictionary<string, QueryTableColumn>(this.m_planColumnNameMap.Count, StringComparer.Ordinal);
			GeneratedColumnMap.FilterMap<string>(this.PlanColumnNameMap.Concat(otherMap.PlanColumnNameMap), dictionary2, hashSet);
			return new WritableGeneratedColumnMap(dictionary, dictionary2);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00033AB0 File Offset: 0x00031CB0
		protected static void AddExpressionMap(Dictionary<ExpressionId, QueryTableColumn> target, IEnumerable<KeyValuePair<ExpressionId, QueryTableColumn>> input, HashSet<QueryTableColumn> excludedColumns)
		{
			foreach (KeyValuePair<ExpressionId, QueryTableColumn> keyValuePair in input)
			{
				if (excludedColumns == null || !excludedColumns.Contains(keyValuePair.Value))
				{
					QueryTableColumn queryTableColumn;
					if (target.TryGetValue(keyValuePair.Key, out queryTableColumn))
					{
						if (!keyValuePair.Value.Equals(queryTableColumn))
						{
							Contract.RetailFail("Expected equal columns");
							throw new InvalidOperationException("Expected equal columns");
						}
					}
					else
					{
						target.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00033B4C File Offset: 0x00031D4C
		protected static void AddPlanColumnNameMap(Dictionary<string, QueryTableColumn> target, IEnumerable<KeyValuePair<string, QueryTableColumn>> input, HashSet<QueryTableColumn> excludedColumns)
		{
			foreach (KeyValuePair<string, QueryTableColumn> keyValuePair in input)
			{
				if (excludedColumns == null || !excludedColumns.Contains(keyValuePair.Value))
				{
					QueryTableColumn queryTableColumn;
					if (target.TryGetValue(keyValuePair.Key, out queryTableColumn))
					{
						if (!keyValuePair.Value.Equals(queryTableColumn))
						{
							Contract.RetailFail("Expected equal columns");
							throw new InvalidOperationException("Expected equal columns");
						}
					}
					else
					{
						target.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00033BE8 File Offset: 0x00031DE8
		public WritableGeneratedColumnMap FilterColumns(IEnumerable<QueryTableColumn> allowedColumns)
		{
			HashSet<QueryTableColumn> hashSet = new HashSet<QueryTableColumn>(allowedColumns);
			Dictionary<ExpressionId, QueryTableColumn> dictionary = new Dictionary<ExpressionId, QueryTableColumn>(this.m_expressionMap.Count);
			GeneratedColumnMap.FilterMap<ExpressionId>(this.ExpressionMap, dictionary, hashSet);
			Dictionary<string, QueryTableColumn> dictionary2 = new Dictionary<string, QueryTableColumn>(this.m_planColumnNameMap.Count, StringComparer.Ordinal);
			GeneratedColumnMap.FilterMap<string>(this.PlanColumnNameMap, dictionary2, hashSet);
			return new WritableGeneratedColumnMap(dictionary, dictionary2);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00033C44 File Offset: 0x00031E44
		public WritableGeneratedColumnMap FilterColumns(IReadOnlyDictionary<ExpressionId, QueryTableColumn> excludedExpressionIds, IReadOnlyDictionary<string, QueryTableColumn> excludedPlanColumnNames)
		{
			Dictionary<ExpressionId, QueryTableColumn> dictionary = new Dictionary<ExpressionId, QueryTableColumn>(this.m_expressionMap.Count);
			GeneratedColumnMap.FilterMap<ExpressionId>(this.ExpressionMap, dictionary, excludedExpressionIds);
			Dictionary<string, QueryTableColumn> dictionary2 = new Dictionary<string, QueryTableColumn>(this.m_planColumnNameMap.Count, StringComparer.Ordinal);
			GeneratedColumnMap.FilterMap<string>(this.PlanColumnNameMap, dictionary2, excludedPlanColumnNames);
			return new WritableGeneratedColumnMap(dictionary, dictionary2);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00033C9C File Offset: 0x00031E9C
		internal WritableGeneratedColumnMap GenerateWithReplacements(IReadOnlyDictionary<QueryTableColumn, QueryTableColumn> replacementColumns, bool allowReuseForUnmapped)
		{
			Dictionary<ExpressionId, QueryTableColumn> dictionary = new Dictionary<ExpressionId, QueryTableColumn>(this.m_expressionMap.Count);
			GeneratedColumnMap.ReplaceColumnsInMap<ExpressionId>(this.ExpressionMap, dictionary, replacementColumns, allowReuseForUnmapped);
			Dictionary<string, QueryTableColumn> dictionary2 = new Dictionary<string, QueryTableColumn>(this.m_planColumnNameMap.Count, StringComparer.Ordinal);
			GeneratedColumnMap.ReplaceColumnsInMap<string>(this.PlanColumnNameMap, dictionary2, replacementColumns, allowReuseForUnmapped);
			return new WritableGeneratedColumnMap(dictionary, dictionary2);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00033CF4 File Offset: 0x00031EF4
		private static void ReplaceColumnsInMap<TKey>(IEnumerable<KeyValuePair<TKey, QueryTableColumn>> oldMap, Dictionary<TKey, QueryTableColumn> newMap, IReadOnlyDictionary<QueryTableColumn, QueryTableColumn> replacementColumns, bool allowReuseForUnmapped)
		{
			foreach (KeyValuePair<TKey, QueryTableColumn> keyValuePair in oldMap)
			{
				QueryTableColumn value;
				if (!replacementColumns.TryGetValue(keyValuePair.Value, out value))
				{
					if (!allowReuseForUnmapped)
					{
						Contract.RetailFail("Expected all columns to be mapped.");
						throw new InvalidOperationException("Expected all columns to be mapped.");
					}
					value = keyValuePair.Value;
				}
				newMap.Add(keyValuePair.Key, value);
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00033D78 File Offset: 0x00031F78
		private static void FilterMap<TKey>(IEnumerable<KeyValuePair<TKey, QueryTableColumn>> oldMap, Dictionary<TKey, QueryTableColumn> newMap, HashSet<QueryTableColumn> allowedValueSet)
		{
			foreach (KeyValuePair<TKey, QueryTableColumn> keyValuePair in oldMap)
			{
				if (allowedValueSet.Contains(keyValuePair.Value))
				{
					QueryTableColumn queryTableColumn;
					if (newMap.TryGetValue(keyValuePair.Key, out queryTableColumn))
					{
						if (!keyValuePair.Value.Equals(queryTableColumn))
						{
							Contract.RetailFail("Expected equal values");
							throw new InvalidOperationException("Expected equal values");
						}
					}
					else
					{
						newMap.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00033E14 File Offset: 0x00032014
		private static void FilterMap<TKey>(IReadOnlyDictionary<TKey, QueryTableColumn> oldMap, Dictionary<TKey, QueryTableColumn> newMap, IReadOnlyDictionary<TKey, QueryTableColumn> excludedValues)
		{
			foreach (KeyValuePair<TKey, QueryTableColumn> keyValuePair in oldMap)
			{
				if (excludedValues == null || !excludedValues.ContainsKey(keyValuePair.Key))
				{
					QueryTableColumn queryTableColumn;
					if (newMap.TryGetValue(keyValuePair.Key, out queryTableColumn))
					{
						if (!keyValuePair.Value.Equals(queryTableColumn))
						{
							Contract.RetailFail("Expected equal values");
							throw new InvalidOperationException("Expected equal values");
						}
					}
					else
					{
						newMap.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00033EB0 File Offset: 0x000320B0
		internal IEnumerable<QueryTableColumn> GetAllColumns()
		{
			return this.ExpressionMap.Select((KeyValuePair<ExpressionId, QueryTableColumn> kvp) => kvp.Value).Concat(this.PlanColumnNameMap.Select((KeyValuePair<string, QueryTableColumn> kvp) => kvp.Value));
		}

		// Token: 0x0400063F RID: 1599
		protected readonly Dictionary<ExpressionId, QueryTableColumn> m_expressionMap;

		// Token: 0x04000640 RID: 1600
		protected readonly Dictionary<string, QueryTableColumn> m_planColumnNameMap;
	}
}
