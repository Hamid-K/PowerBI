using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriBuilder
{
	// Token: 0x020001BD RID: 445
	public sealed class ODataUriBuilder
	{
		// Token: 0x060010A9 RID: 4265 RVA: 0x0003A02B File Offset: 0x0003822B
		public ODataUriBuilder(ODataUrlConventions urlConventions, ODataUri odataUri)
		{
			this.urlConventions = urlConventions;
			this.odataUri = odataUri;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0003A044 File Offset: 0x00038244
		[SuppressMessage("DataWeb.Usage", "AC0018:SystemUriEscapeDataStringRule", Justification = "Values passed to this method are model elements like property names or keywords.")]
		public Uri BuildUri()
		{
			NodeToStringBuilder nodeToStringBuilder = new NodeToStringBuilder();
			SelectExpandClauseToStringBuilder selectExpandClauseToStringBuilder = new SelectExpandClauseToStringBuilder();
			string text = string.Empty;
			bool flag = true;
			if (this.odataUri.Filter != null)
			{
				text = ODataUriBuilder.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$filter" + "=" + Uri.EscapeDataString(nodeToStringBuilder.TranslateFilterClause(this.odataUri.Filter));
			}
			if (this.odataUri.SelectAndExpand != null)
			{
				string text2 = selectExpandClauseToStringBuilder.TranslateSelectExpandClause(this.odataUri.SelectAndExpand, true);
				if (!string.IsNullOrEmpty(text2))
				{
					text = ODataUriBuilder.WriteQueryPrefixOrSeparator(flag, text);
					flag = false;
					text += text2;
				}
			}
			if (this.odataUri.OrderBy != null)
			{
				text = ODataUriBuilder.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$orderby" + "=" + Uri.EscapeDataString(nodeToStringBuilder.TranslateOrderByClause(this.odataUri.OrderBy));
			}
			if (this.odataUri.Top != null)
			{
				text = ODataUriBuilder.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$top" + "=" + Uri.EscapeDataString(this.odataUri.Top.ToString());
			}
			if (this.odataUri.Skip != null)
			{
				text = ODataUriBuilder.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$skip" + "=" + Uri.EscapeDataString(this.odataUri.Skip.ToString());
			}
			if (this.odataUri.QueryCount != null)
			{
				text = ODataUriBuilder.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$count" + "=" + Uri.EscapeDataString((this.odataUri.QueryCount == true) ? "true" : "false");
			}
			if (this.odataUri.Search != null)
			{
				text = ODataUriBuilder.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$search" + "=" + Uri.EscapeDataString(nodeToStringBuilder.TranslateSearchClause(this.odataUri.Search));
			}
			if (this.odataUri.ParameterAliasNodes != null && this.odataUri.ParameterAliasNodes.Count > 0)
			{
				string text3 = nodeToStringBuilder.TranslateParameterAliasNodes(this.odataUri.ParameterAliasNodes);
				text = (string.IsNullOrEmpty(text3) ? text : (ODataUriBuilder.WriteQueryPrefixOrSeparator(flag, text) + text3));
			}
			string text4 = this.odataUri.Path.ToResourcePathString(this.urlConventions) + text;
			if (!(this.odataUri.ServiceRoot == null))
			{
				return new Uri(this.odataUri.ServiceRoot, new Uri(text4, 2));
			}
			return new Uri(text4, 2);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0003A302 File Offset: 0x00038502
		private static string WriteQueryPrefixOrSeparator(bool writeQueryPrefix, string queryOptions)
		{
			if (writeQueryPrefix)
			{
				return queryOptions + "?";
			}
			return queryOptions + "&";
		}

		// Token: 0x0400076C RID: 1900
		private readonly ODataUri odataUri;

		// Token: 0x0400076D RID: 1901
		private readonly ODataUrlConventions urlConventions;
	}
}
