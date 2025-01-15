using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData
{
	// Token: 0x020000DB RID: 219
	internal sealed class ApplyClauseToStringBuilder
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x0001B38C File Offset: 0x0001958C
		public ApplyClauseToStringBuilder()
		{
			this.nodeToStringBuilder = new NodeToStringBuilder();
			this.query = new StringBuilder();
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001B3AC File Offset: 0x000195AC
		public string TranslateApplyClause(ApplyClause applyClause)
		{
			ExceptionUtils.CheckArgumentNotNull<ApplyClause>(applyClause, "applyClause");
			this.query.Append("$apply");
			this.query.Append("=");
			bool flag = false;
			foreach (TransformationNode transformationNode in applyClause.Transformations)
			{
				flag = this.AppendSlash(flag);
				this.Translate(transformationNode);
			}
			if (!flag)
			{
				return string.Empty;
			}
			return this.query.ToString();
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001B448 File Offset: 0x00019648
		private bool AppendComma(bool appendComma)
		{
			if (appendComma)
			{
				this.query.Append(",");
			}
			return true;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001B460 File Offset: 0x00019660
		private void AppendExpression(SingleValueNode expression)
		{
			string text = Uri.EscapeDataString(this.nodeToStringBuilder.TranslateNode(expression));
			this.query.Append(text);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001B48C File Offset: 0x0001968C
		private void AppendExpression(ODataExpandPath path)
		{
			string text = path.ToContextUrlPathString();
			this.query.Append(text);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001B4AD File Offset: 0x000196AD
		private bool AppendSlash(bool appendSlash)
		{
			if (appendSlash)
			{
				this.query.Append("/");
			}
			return true;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001B4C4 File Offset: 0x000196C4
		private void AppendWord(string word)
		{
			this.query.Append(word);
			this.query.Append("%20");
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001B4E4 File Offset: 0x000196E4
		private static string GetAggregationMethodName(AggregateExpression aggExpression)
		{
			switch (aggExpression.Method)
			{
			case AggregationMethod.Sum:
				return "sum";
			case AggregationMethod.Min:
				return "min";
			case AggregationMethod.Max:
				return "max";
			case AggregationMethod.Average:
				return "average";
			case AggregationMethod.CountDistinct:
				return "countdistinct";
			case AggregationMethod.VirtualPropertyCount:
				return "$count";
			case AggregationMethod.Custom:
				return aggExpression.MethodDefinition.MethodLabel;
			default:
				throw new ArgumentOutOfRangeException("aggExpression", "unknown AggregationMethod " + aggExpression.Method.ToString());
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001B574 File Offset: 0x00019774
		private void Translate(AggregateTransformationNode transformation)
		{
			this.Translate(transformation.AggregateExpressions);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001B584 File Offset: 0x00019784
		private void Translate(IEnumerable<AggregateExpressionBase> expressions)
		{
			bool flag = false;
			foreach (AggregateExpressionBase aggregateExpressionBase in expressions)
			{
				flag = this.AppendComma(flag);
				AggregateExpressionKind aggregateKind = aggregateExpressionBase.AggregateKind;
				if (aggregateKind != AggregateExpressionKind.PropertyAggregate)
				{
					if (aggregateKind == AggregateExpressionKind.EntitySetAggregate)
					{
						EntitySetAggregateExpression entitySetAggregateExpression = aggregateExpressionBase as EntitySetAggregateExpression;
						this.query.Append(entitySetAggregateExpression.Alias);
						this.query.Append("(");
						this.Translate(entitySetAggregateExpression.Children);
						this.query.Append(")");
					}
				}
				else
				{
					AggregateExpression aggregateExpression = aggregateExpressionBase as AggregateExpression;
					if (aggregateExpression.Method != AggregationMethod.VirtualPropertyCount)
					{
						this.AppendExpression(aggregateExpression.Expression);
						this.query.Append("%20");
						this.AppendWord("with");
					}
					this.AppendWord(ApplyClauseToStringBuilder.GetAggregationMethodName(aggregateExpression));
					this.AppendWord("as");
					this.query.Append(aggregateExpression.Alias);
				}
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001B69C File Offset: 0x0001989C
		private void Translate(ComputeTransformationNode transformation)
		{
			bool flag = false;
			foreach (ComputeExpression computeExpression in transformation.Expressions)
			{
				flag = this.AppendComma(flag);
				this.AppendExpression(computeExpression.Expression);
				this.query.Append("%20");
				this.AppendWord("as");
				this.query.Append(computeExpression.Alias);
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001B728 File Offset: 0x00019928
		private void Translate(ExpandTransformationNode transformation)
		{
			ExpandedNavigationSelectItem expandedNavigationSelectItem = transformation.ExpandClause.SelectedItems.Single<SelectItem>() as ExpandedNavigationSelectItem;
			this.AppendExpandExpression(expandedNavigationSelectItem);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001B754 File Offset: 0x00019954
		private void AppendExpandExpression(ExpandedNavigationSelectItem expandedNavigation)
		{
			this.AppendExpression(expandedNavigation.PathToNavigationProperty);
			if (expandedNavigation.FilterOption != null)
			{
				this.AppendComma(true);
				this.query.Append("%20");
				this.query.Append("filter");
				this.query.Append("(");
				this.AppendExpression(expandedNavigation.FilterOption.Expression);
				this.query.Append(")");
			}
			if (expandedNavigation.SelectAndExpand != null)
			{
				foreach (ExpandedNavigationSelectItem expandedNavigationSelectItem in expandedNavigation.SelectAndExpand.SelectedItems.OfType<ExpandedNavigationSelectItem>())
				{
					this.AppendComma(true);
					this.query.Append("%20");
					this.query.Append("expand");
					this.query.Append("(");
					this.AppendExpandExpression(expandedNavigationSelectItem);
					this.query.Append(")");
				}
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001B874 File Offset: 0x00019A74
		private void Translate(FilterTransformationNode transformation)
		{
			this.AppendExpression(transformation.FilterClause.Expression);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001B888 File Offset: 0x00019A88
		private void Translate(GroupByTransformationNode transformation)
		{
			bool flag = false;
			foreach (GroupByPropertyNode groupByPropertyNode in transformation.GroupingProperties)
			{
				if (flag)
				{
					this.AppendComma(flag);
				}
				else
				{
					flag = true;
					this.query.Append("(");
				}
				this.Translate(groupByPropertyNode);
			}
			if (flag)
			{
				this.query.Append(")");
			}
			if (transformation.ChildTransformations != null)
			{
				this.AppendComma(true);
				this.Translate(transformation.ChildTransformations);
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001B928 File Offset: 0x00019B28
		private void Translate(GroupByPropertyNode node)
		{
			if (node.Expression != null)
			{
				this.AppendExpression(node.Expression);
			}
			bool flag = false;
			foreach (GroupByPropertyNode groupByPropertyNode in node.ChildTransformations)
			{
				flag = this.AppendComma(flag);
				this.Translate(groupByPropertyNode);
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001B994 File Offset: 0x00019B94
		private void Translate(TransformationNode transformation)
		{
			switch (transformation.Kind)
			{
			case TransformationNodeKind.Aggregate:
				this.query.Append("aggregate");
				break;
			case TransformationNodeKind.GroupBy:
				this.query.Append("groupby");
				break;
			case TransformationNodeKind.Filter:
				this.query.Append("filter");
				break;
			case TransformationNodeKind.Compute:
				this.query.Append("compute");
				break;
			case TransformationNodeKind.Expand:
				this.query.Append("expand");
				break;
			default:
				throw new NotSupportedException("unknown TransformationNodeKind value " + transformation.Kind.ToString());
			}
			this.query.Append("(");
			GroupByTransformationNode groupByTransformationNode;
			AggregateTransformationNode aggregateTransformationNode;
			FilterTransformationNode filterTransformationNode;
			ComputeTransformationNode computeTransformationNode;
			if ((groupByTransformationNode = transformation as GroupByTransformationNode) != null)
			{
				this.Translate(groupByTransformationNode);
			}
			else if ((aggregateTransformationNode = transformation as AggregateTransformationNode) != null)
			{
				this.Translate(aggregateTransformationNode);
			}
			else if ((filterTransformationNode = transformation as FilterTransformationNode) != null)
			{
				this.Translate(filterTransformationNode);
			}
			else if ((computeTransformationNode = transformation as ComputeTransformationNode) != null)
			{
				this.Translate(computeTransformationNode);
			}
			else
			{
				ExpandTransformationNode expandTransformationNode;
				if ((expandTransformationNode = transformation as ExpandTransformationNode) == null)
				{
					throw new NotSupportedException("unknown TransformationNode type " + transformation.GetType().Name);
				}
				this.Translate(expandTransformationNode);
			}
			this.query.Append(")");
		}

		// Token: 0x040003C3 RID: 963
		private readonly NodeToStringBuilder nodeToStringBuilder;

		// Token: 0x040003C4 RID: 964
		private readonly StringBuilder query;
	}
}
