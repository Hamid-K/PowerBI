using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization.CommonSubqueryElimination
{
	// Token: 0x02000104 RID: 260
	internal readonly struct QueryShape : IEquatable<QueryShape>
	{
		// Token: 0x06000893 RID: 2195 RVA: 0x000221D4 File Offset: 0x000203D4
		public QueryShape(ResolvedQueryDefinition query)
		{
			this._value = Hashing.CombineHash(query.Let.Count, query.From.Count, query.Where.Count, query.Transform.Count, query.GroupBy.Count, query.Select.Count, query.OrderBy.Count, QueryShape.GetFirstSelectQueryShapePart(query));
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00022240 File Offset: 0x00020440
		private static int GetFirstSelectQueryShapePart(ResolvedQueryDefinition query)
		{
			if (query.Select.Count == 0)
			{
				return -48879;
			}
			ResolvedQueryExpression expression = query.Select[0].Expression;
			ResolvedQueryColumnExpression resolvedQueryColumnExpression = expression as ResolvedQueryColumnExpression;
			int num;
			if (resolvedQueryColumnExpression == null)
			{
				ResolvedQueryMeasureExpression resolvedQueryMeasureExpression = expression as ResolvedQueryMeasureExpression;
				if (resolvedQueryMeasureExpression == null)
				{
					ResolvedQueryAggregationExpression resolvedQueryAggregationExpression = expression as ResolvedQueryAggregationExpression;
					if (resolvedQueryAggregationExpression == null)
					{
						num = -48879;
					}
					else
					{
						ResolvedQueryColumnExpression resolvedQueryColumnExpression2 = resolvedQueryAggregationExpression.Expression as ResolvedQueryColumnExpression;
						int? num2;
						if (resolvedQueryColumnExpression2 == null)
						{
							num2 = null;
						}
						else
						{
							string name = resolvedQueryColumnExpression2.Column.Name;
							num2 = ((name != null) ? new int?(name.Length) : null);
						}
						num = num2 ?? (-48879);
					}
				}
				else
				{
					string name2 = resolvedQueryMeasureExpression.Measure.Name;
					num = ((name2 != null) ? name2.Length : (-48879));
				}
			}
			else
			{
				string name3 = resolvedQueryColumnExpression.Column.Name;
				num = ((name3 != null) ? name3.Length : (-48879));
			}
			return num;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00022337 File Offset: 0x00020537
		public override bool Equals(object obj)
		{
			return obj is QueryShape && this.Equals((QueryShape)obj);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0002234F File Offset: 0x0002054F
		public bool Equals(QueryShape other)
		{
			return this._value == other._value;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0002235F File Offset: 0x0002055F
		public override int GetHashCode()
		{
			return this._value;
		}

		// Token: 0x04000467 RID: 1127
		private readonly int _value;
	}
}
