using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001F8 RID: 504
	public sealed class ApplyClause
	{
		// Token: 0x06001674 RID: 5748 RVA: 0x0003EE64 File Offset: 0x0003D064
		public ApplyClause(IList<TransformationNode> transformations)
		{
			ExceptionUtils.CheckArgumentNotNull<IList<TransformationNode>>(transformations, "transformations");
			this.transformations = transformations;
			int i = transformations.Count - 1;
			while (i >= 0)
			{
				if (transformations[i].Kind == TransformationNodeKind.Aggregate)
				{
					this.lastAggregateExpressions = (transformations[i] as AggregateTransformationNode).AggregateExpressions;
					return;
				}
				if (transformations[i].Kind == TransformationNodeKind.GroupBy)
				{
					GroupByTransformationNode groupByTransformationNode = transformations[i] as GroupByTransformationNode;
					this.lastGroupByPropertyNodes = groupByTransformationNode.GroupingProperties;
					TransformationNode childTransformations = groupByTransformationNode.ChildTransformations;
					if (childTransformations != null && childTransformations.Kind == TransformationNodeKind.Aggregate)
					{
						this.lastAggregateExpressions = (childTransformations as AggregateTransformationNode).AggregateExpressions;
						return;
					}
					break;
				}
				else
				{
					if (transformations[i].Kind == TransformationNodeKind.Compute)
					{
						this.lastComputeExpressions = this.lastComputeExpressions ?? new List<ComputeExpression>();
						this.lastComputeExpressions.AddRange((transformations[i] as ComputeTransformationNode).Expressions);
					}
					i--;
				}
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x0003EF55 File Offset: 0x0003D155
		public IEnumerable<TransformationNode> Transformations
		{
			get
			{
				return this.transformations;
			}
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0003EF60 File Offset: 0x0003D160
		internal string GetContextUri()
		{
			IEnumerable<ComputeExpression> enumerable = this.transformations.OfType<ComputeTransformationNode>().SelectMany((ComputeTransformationNode n) => n.Expressions);
			return this.CreatePropertiesUriSegment(this.lastGroupByPropertyNodes, this.lastAggregateExpressions, enumerable);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0003EFB0 File Offset: 0x0003D1B0
		internal HashSet<EndPathToken> GetLastAggregatedPropertyNames()
		{
			if (this.lastAggregateExpressions == null && this.lastComputeExpressions == null && this.lastGroupByPropertyNodes == null)
			{
				return null;
			}
			HashSet<EndPathToken> hashSet = new HashSet<EndPathToken>();
			if (this.lastAggregateExpressions != null)
			{
				hashSet.UnionWith(this.lastAggregateExpressions.Select((AggregateExpressionBase statement) => new EndPathToken(statement.Alias, null)));
			}
			if (this.lastComputeExpressions != null)
			{
				hashSet.UnionWith(this.lastComputeExpressions.Select((ComputeExpression statement) => new EndPathToken(statement.Alias, null)));
			}
			if (this.lastGroupByPropertyNodes != null)
			{
				hashSet.UnionWith(this.GetGroupByPaths(this.lastGroupByPropertyNodes, null));
			}
			return hashSet;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0003F069 File Offset: 0x0003D269
		private IEnumerable<EndPathToken> GetGroupByPaths(IEnumerable<GroupByPropertyNode> nodes, EndPathToken token)
		{
			foreach (GroupByPropertyNode node in nodes)
			{
				EndPathToken nodeToken = new EndPathToken(node.Name, token);
				if (node.ChildTransformations == null || !node.ChildTransformations.Any<GroupByPropertyNode>())
				{
					yield return nodeToken;
				}
				else
				{
					foreach (EndPathToken endPathToken in this.GetGroupByPaths(node.ChildTransformations, nodeToken))
					{
						yield return endPathToken;
					}
					IEnumerator<EndPathToken> enumerator2 = null;
				}
				nodeToken = null;
				node = null;
			}
			IEnumerator<GroupByPropertyNode> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0003F088 File Offset: 0x0003D288
		private string CreatePropertiesUriSegment(IEnumerable<GroupByPropertyNode> groupByPropertyNodes, IEnumerable<AggregateExpressionBase> aggregateExpressions, IEnumerable<ComputeExpression> computeExpressions)
		{
			Func<GroupByPropertyNode, string> func = delegate(GroupByPropertyNode prop)
			{
				string text3 = this.CreatePropertiesUriSegment(prop.ChildTransformations, null, null);
				if (!string.IsNullOrEmpty(text3))
				{
					return prop.Name + "(" + text3 + ")";
				}
				return prop.Name;
			};
			string text = string.Empty;
			if (groupByPropertyNodes != null)
			{
				string[] array = groupByPropertyNodes.Select((GroupByPropertyNode prop) => func(prop)).ToArray<string>();
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
			if (computeExpressions != null && !string.IsNullOrEmpty(text))
			{
				string text2 = string.Join(",", computeExpressions.Select((ComputeExpression e) => e.Alias).ToArray<string>());
				if (!string.IsNullOrEmpty(text2))
				{
					text = string.Format(CultureInfo.InvariantCulture, "{0},{1}", new object[] { text, text2 });
				}
			}
			return text;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0003F17D File Offset: 0x0003D37D
		private static string CreateAggregatePropertiesUriSegment(IEnumerable<AggregateExpressionBase> aggregateExpressions)
		{
			if (aggregateExpressions != null)
			{
				return string.Join(",", aggregateExpressions.Select((AggregateExpressionBase statement) => statement.Alias).ToArray<string>());
			}
			return string.Empty;
		}

		// Token: 0x04000A20 RID: 2592
		private readonly IEnumerable<TransformationNode> transformations;

		// Token: 0x04000A21 RID: 2593
		private readonly IEnumerable<AggregateExpressionBase> lastAggregateExpressions;

		// Token: 0x04000A22 RID: 2594
		private readonly IEnumerable<GroupByPropertyNode> lastGroupByPropertyNodes;

		// Token: 0x04000A23 RID: 2595
		private readonly List<ComputeExpression> lastComputeExpressions;
	}
}
