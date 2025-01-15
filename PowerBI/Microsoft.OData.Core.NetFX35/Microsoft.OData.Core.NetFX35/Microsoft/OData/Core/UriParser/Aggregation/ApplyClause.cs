using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.OData.Core.UriParser.Aggregation
{
	// Token: 0x020002AA RID: 682
	public sealed class ApplyClause
	{
		// Token: 0x0600178F RID: 6031 RVA: 0x00050ABC File Offset: 0x0004ECBC
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

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x00050B66 File Offset: 0x0004ED66
		public IEnumerable<TransformationNode> Transformations
		{
			get
			{
				return this.transformations;
			}
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00050B6E File Offset: 0x0004ED6E
		internal string GetContextUri()
		{
			return this.CreatePropertiesUriSegment(this.lastGroupByPropertyNodes, this.lastAggregateExpressions);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00050B8A File Offset: 0x0004ED8A
		internal List<string> GetLastAggregatedPropertyNames()
		{
			if (this.lastAggregateExpressions != null)
			{
				return Enumerable.ToList<string>(Enumerable.Select<AggregateExpression, string>(this.lastAggregateExpressions, (AggregateExpression statement) => statement.Alias));
			}
			return null;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00050BE0 File Offset: 0x0004EDE0
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
				return '(' + text + ')';
			}
			return text;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00050C85 File Offset: 0x0004EE85
		private static string CreateAggregatePropertiesUriSegment(IEnumerable<AggregateExpression> aggregateExpressions)
		{
			if (aggregateExpressions != null)
			{
				return string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<AggregateExpression, string>(aggregateExpressions, (AggregateExpression statement) => statement.Alias)));
			}
			return string.Empty;
		}

		// Token: 0x04000A24 RID: 2596
		private readonly IEnumerable<TransformationNode> transformations;

		// Token: 0x04000A25 RID: 2597
		private readonly IEnumerable<AggregateExpression> lastAggregateExpressions;

		// Token: 0x04000A26 RID: 2598
		private readonly IEnumerable<GroupByPropertyNode> lastGroupByPropertyNodes;
	}
}
