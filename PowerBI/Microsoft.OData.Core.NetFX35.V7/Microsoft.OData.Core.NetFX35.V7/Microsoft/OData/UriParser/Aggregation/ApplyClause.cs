using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001C6 RID: 454
	public sealed class ApplyClause
	{
		// Token: 0x060011D0 RID: 4560 RVA: 0x00031DA8 File Offset: 0x0002FFA8
		public ApplyClause(IList<TransformationNode> transformations)
		{
			ExceptionUtils.CheckArgumentNotNull<IList<TransformationNode>>(transformations, "transformations");
			this.transformations = transformations;
			int i = transformations.Count - 1;
			while (i >= 0)
			{
				if (transformations[i].Kind == TransformationNodeKind.Aggregate)
				{
					this.lastAggregateExpressions = (transformations[i] as AggregateTransformationNode).Expressions;
					return;
				}
				if (transformations[i].Kind == TransformationNodeKind.GroupBy)
				{
					GroupByTransformationNode groupByTransformationNode = transformations[i] as GroupByTransformationNode;
					this.lastGroupByPropertyNodes = groupByTransformationNode.GroupingProperties;
					TransformationNode childTransformations = groupByTransformationNode.ChildTransformations;
					if (childTransformations != null && childTransformations.Kind == TransformationNodeKind.Aggregate)
					{
						this.lastAggregateExpressions = (childTransformations as AggregateTransformationNode).Expressions;
						return;
					}
					break;
				}
				else
				{
					i--;
				}
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00031E53 File Offset: 0x00030053
		public IEnumerable<TransformationNode> Transformations
		{
			get
			{
				return this.transformations;
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00031E5B File Offset: 0x0003005B
		internal string GetContextUri()
		{
			return this.CreatePropertiesUriSegment(this.lastGroupByPropertyNodes, this.lastAggregateExpressions);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00031E6F File Offset: 0x0003006F
		internal List<string> GetLastAggregatedPropertyNames()
		{
			if (this.lastAggregateExpressions != null)
			{
				return Enumerable.ToList<string>(Enumerable.Select<AggregateExpression, string>(this.lastAggregateExpressions, (AggregateExpression statement) => statement.Alias));
			}
			return null;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00031EAC File Offset: 0x000300AC
		private string CreatePropertiesUriSegment(IEnumerable<GroupByPropertyNode> groupByPropertyNodes, IEnumerable<AggregateExpression> aggregateExpressions)
		{
			string text = string.Empty;
			if (groupByPropertyNodes != null)
			{
				string[] array = Enumerable.ToArray<string>(Enumerable.Select<GroupByPropertyNode, string>(groupByPropertyNodes, (GroupByPropertyNode prop) => prop.Name + this.CreatePropertiesUriSegment(prop.ChildTransformations, null)));
				text = string.Join(",", array);
				text = ((aggregateExpressions == null) ? text : string.Format(CultureInfo.InvariantCulture, "{0},{1}", new object[]
				{
					text,
					ApplyClause.CreateAggregatePropertiesUriSegment(aggregateExpressions)
				}));
			}
			else
			{
				text = ((aggregateExpressions == null) ? string.Empty : ApplyClause.CreateAggregatePropertiesUriSegment(aggregateExpressions));
			}
			if (!string.IsNullOrEmpty(text))
			{
				return "(" + text + ")";
			}
			return text;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00031F3C File Offset: 0x0003013C
		private static string CreateAggregatePropertiesUriSegment(IEnumerable<AggregateExpression> aggregateExpressions)
		{
			if (aggregateExpressions != null)
			{
				return string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<AggregateExpression, string>(aggregateExpressions, (AggregateExpression statement) => statement.Alias)));
			}
			return string.Empty;
		}

		// Token: 0x04000910 RID: 2320
		private readonly IEnumerable<TransformationNode> transformations;

		// Token: 0x04000911 RID: 2321
		private readonly IEnumerable<AggregateExpression> lastAggregateExpressions;

		// Token: 0x04000912 RID: 2322
		private readonly IEnumerable<GroupByPropertyNode> lastGroupByPropertyNodes;
	}
}
