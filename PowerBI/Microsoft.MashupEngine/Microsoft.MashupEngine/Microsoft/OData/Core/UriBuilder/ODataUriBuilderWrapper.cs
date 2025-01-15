using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Aggregation;

namespace Microsoft.OData.Core.UriBuilder
{
	// Token: 0x02000175 RID: 373
	public sealed class ODataUriBuilderWrapper
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		public ODataUriBuilderWrapper(ODataUrlConventions urlConventions, ODataUri odataUri)
		{
			this.urlConventions = urlConventions;
			this.odataUri = odataUri;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000BF14 File Offset: 0x0000A114
		public Uri BuildUri()
		{
			Uri uri = new ODataUriBuilder(this.urlConventions, this.odataUri).BuildUri();
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

		// Token: 0x06000713 RID: 1811 RVA: 0x0000BFCC File Offset: 0x0000A1CC
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

		// Token: 0x06000714 RID: 1812 RVA: 0x0000C04C File Offset: 0x0000A24C
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

		// Token: 0x06000715 RID: 1813 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
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

		// Token: 0x06000716 RID: 1814 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
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

		// Token: 0x06000717 RID: 1815 RVA: 0x0000C2BC File Offset: 0x0000A4BC
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

		// Token: 0x06000718 RID: 1816 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
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

		// Token: 0x06000719 RID: 1817 RVA: 0x0000C390 File Offset: 0x0000A590
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

		// Token: 0x0400045D RID: 1117
		public const string QueryOptionApply = "$apply";

		// Token: 0x0400045E RID: 1118
		public const string KeywordAggregate = "aggregate";

		// Token: 0x0400045F RID: 1119
		private const string SymbolEqual = "=";

		// Token: 0x04000460 RID: 1120
		private const string SymbolQueryConcatenate = "&";

		// Token: 0x04000461 RID: 1121
		private const string SymbolQueryStart = "?";

		// Token: 0x04000462 RID: 1122
		private const string SymbolComma = ",";

		// Token: 0x04000463 RID: 1123
		private const string SymbolOpenParen = "(";

		// Token: 0x04000464 RID: 1124
		private const string SymbolClosedParen = ")";

		// Token: 0x04000465 RID: 1125
		private const string SymbolForwardSlash = "/";

		// Token: 0x04000466 RID: 1126
		private const string KeywordWith = "with";

		// Token: 0x04000467 RID: 1127
		private const string KeywordAs = "as";

		// Token: 0x04000468 RID: 1128
		private const string KeywordGroupBy = "groupby";

		// Token: 0x04000469 RID: 1129
		private const string KeywordFilter = "filter";

		// Token: 0x0400046A RID: 1130
		private const string SymbolEscapedSpace = "%20";

		// Token: 0x0400046B RID: 1131
		private readonly ODataUri odataUri;

		// Token: 0x0400046C RID: 1132
		private readonly ODataUrlConventions urlConventions;
	}
}
