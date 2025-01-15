using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.OData;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x0200077B RID: 1915
	public sealed class ODataUriBuilderWrapper
	{
		// Token: 0x06003860 RID: 14432 RVA: 0x000B4FDD File Offset: 0x000B31DD
		public ODataUriBuilderWrapper(ODataUrlKeyDelimiter urlKeyDelimiter, ODataUri odataUri)
		{
			this.urlKeyDelimiter = urlKeyDelimiter;
			this.odataUri = odataUri;
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000B4FF4 File Offset: 0x000B31F4
		public Uri BuildUri()
		{
			Uri uri = this.odataUri.BuildUri(this.urlKeyDelimiter);
			if (this.odataUri.Apply == null)
			{
				return uri;
			}
			ApplyClause apply = this.odataUri.Apply;
			string text;
			if (!this.TryConvertApplyToString(apply, out text))
			{
				throw new NotSupportedException();
			}
			string text2 = "$apply" + "=" + text;
			UriBuilder uriBuilder = new UriBuilder(uri);
			uriBuilder.Query = (string.IsNullOrEmpty(uri.Query) ? text2 : (uri.Query.Substring(1) + "&" + text2));
			if (uriBuilder.Uri.IsDefaultPort)
			{
				uriBuilder.Port = -1;
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000B50A8 File Offset: 0x000B32A8
		private bool TryConvertApplyToString(ApplyClause apply, out string result)
		{
			List<string> list = new List<string>();
			foreach (TransformationNode transformationNode in apply.Transformations)
			{
				string text;
				if (!this.TryTransformationToString(transformationNode, out text))
				{
					result = string.Empty;
					return false;
				}
				list.Add(text);
			}
			result = string.Join("/", list.ToArray());
			return true;
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000B5128 File Offset: 0x000B3328
		private bool TryTransformationToString(TransformationNode transformationNode, out string transform)
		{
			switch (transformationNode.Kind)
			{
			case TransformationNodeKind.Aggregate:
				return this.TryAggregateNodeToString(transformationNode as AggregateTransformationNode, out transform);
			case TransformationNodeKind.GroupBy:
				return this.TryGroupByNodeToString(transformationNode as GroupByTransformationNode, out transform);
			case TransformationNodeKind.Filter:
				return this.TryFilterNodeToString(transformationNode as FilterTransformationNode, out transform);
			default:
				transform = string.Empty;
				return false;
			}
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000B5184 File Offset: 0x000B3384
		private bool TryGroupByNodeToString(GroupByTransformationNode groupByTransformNode, out string result)
		{
			IEnumerable<GroupByPropertyNode> groupingProperties = groupByTransformNode.GroupingProperties;
			if (groupingProperties.Count<GroupByPropertyNode>() != 0)
			{
				List<string> list = new List<string>();
				foreach (GroupByPropertyNode groupByPropertyNode in groupingProperties)
				{
					string[] array;
					if (!this.TryGroupByPropertyNodeToString(groupByPropertyNode, out array))
					{
						result = string.Empty;
						return false;
					}
					list.AddRange(array);
				}
				string text = string.Join(",", list.ToArray());
				if (groupByTransformNode.ChildTransformations == null)
				{
					result = string.Concat(new string[] { "groupby", "(", "(", text, ")", ")" });
					return true;
				}
				string text2;
				if (!this.TryTransformationToString(groupByTransformNode.ChildTransformations, out text2))
				{
					result = string.Empty;
					return false;
				}
				result = string.Concat(new string[] { "groupby", "(", "(", text, ")", ",", text2, ")" });
				return true;
			}
			if (!this.TryTransformationToString(groupByTransformNode.ChildTransformations, out result))
			{
				result = string.Empty;
				return false;
			}
			return true;
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000B52D4 File Offset: 0x000B34D4
		private bool TryGroupByPropertyNodeToString(GroupByPropertyNode groupByPropertyNode, out string[] groupByPropertyResult)
		{
			if (groupByPropertyNode.Expression != null)
			{
				string text;
				if (!NodeToStringBuilderWrapper.TryTranslateNode(groupByPropertyNode.Expression, out text))
				{
					groupByPropertyResult = EmptyArray<string>.Instance;
					return false;
				}
				groupByPropertyResult = new string[] { text };
				return true;
			}
			else
			{
				if (groupByPropertyNode.ChildTransformations != null && groupByPropertyNode.ChildTransformations.Count > 0)
				{
					List<string> list = new List<string>();
					foreach (GroupByPropertyNode groupByPropertyNode2 in groupByPropertyNode.ChildTransformations)
					{
						string[] array;
						if (!this.TryGroupByPropertyNodeToString(groupByPropertyNode2, out array))
						{
							groupByPropertyResult = EmptyArray<string>.Instance;
							return false;
						}
						list.AddRange(array);
					}
					groupByPropertyResult = list.ToArray();
					return true;
				}
				groupByPropertyResult = EmptyArray<string>.Instance;
				return true;
			}
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000B5398 File Offset: 0x000B3598
		private bool TryFilterNodeToString(FilterTransformationNode filterTransformNode, out string filter)
		{
			if (NodeToStringBuilderWrapper.TryTranslateFilterClause(filterTransformNode.FilterClause, out filter))
			{
				filter = "filter" + ("(" + Uri.EscapeDataString(filter) + ")");
				return true;
			}
			filter = string.Empty;
			return false;
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000B53D4 File Offset: 0x000B35D4
		private bool TryAggregateNodeToString(AggregateTransformationNode aggregateTransformNode, out string aggregateString)
		{
			List<string> list = new List<string>();
			foreach (AggregateExpression aggregateExpression in aggregateTransformNode.Expressions)
			{
				string text;
				if (!this.TryAggregateExpressionToString(aggregateExpression, out text))
				{
					aggregateString = string.Empty;
					return false;
				}
				list.Add(text);
			}
			aggregateString = string.Join(",", list.ToArray());
			aggregateString = "aggregate" + "(" + aggregateString + ")";
			return true;
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000B546C File Offset: 0x000B366C
		private bool TryAggregateExpressionToString(AggregateExpression aggregateExpression, out string aggregateString)
		{
			string text;
			if (NodeToStringBuilderWrapper.TryTranslateNode(aggregateExpression.Expression, out text))
			{
				string text2 = aggregateExpression.Method.ToString().ToLowerInvariant();
				string alias = aggregateExpression.Alias;
				aggregateString = string.Concat(new string[] { text, "%20", "with", "%20", text2, "%20", "as", "%20", alias });
				return true;
			}
			aggregateString = string.Empty;
			return false;
		}

		// Token: 0x04001D1F RID: 7455
		public const string QueryOptionApply = "$apply";

		// Token: 0x04001D20 RID: 7456
		public const string KeywordAggregate = "aggregate";

		// Token: 0x04001D21 RID: 7457
		private const string SymbolEqual = "=";

		// Token: 0x04001D22 RID: 7458
		private const string SymbolQueryConcatenate = "&";

		// Token: 0x04001D23 RID: 7459
		private const string SymbolQueryStart = "?";

		// Token: 0x04001D24 RID: 7460
		private const string SymbolComma = ",";

		// Token: 0x04001D25 RID: 7461
		private const string SymbolOpenParen = "(";

		// Token: 0x04001D26 RID: 7462
		private const string SymbolClosedParen = ")";

		// Token: 0x04001D27 RID: 7463
		private const string SymbolForwardSlash = "/";

		// Token: 0x04001D28 RID: 7464
		private const string KeywordWith = "with";

		// Token: 0x04001D29 RID: 7465
		private const string KeywordAs = "as";

		// Token: 0x04001D2A RID: 7466
		private const string KeywordGroupBy = "groupby";

		// Token: 0x04001D2B RID: 7467
		private const string KeywordFilter = "filter";

		// Token: 0x04001D2C RID: 7468
		private const string SymbolEscapedSpace = "%20";

		// Token: 0x04001D2D RID: 7469
		private readonly ODataUri odataUri;

		// Token: 0x04001D2E RID: 7470
		private readonly ODataUrlKeyDelimiter urlKeyDelimiter;
	}
}
