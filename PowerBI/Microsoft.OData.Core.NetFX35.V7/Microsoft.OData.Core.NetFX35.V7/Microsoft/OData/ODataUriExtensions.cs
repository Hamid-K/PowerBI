using System;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000D4 RID: 212
	public static class ODataUriExtensions
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x000174B8 File Offset: 0x000156B8
		public static Uri BuildUri(this ODataUri odataUri, ODataUrlKeyDelimiter urlKeyDelimiter)
		{
			NodeToStringBuilder nodeToStringBuilder = new NodeToStringBuilder();
			SelectExpandClauseToStringBuilder selectExpandClauseToStringBuilder = new SelectExpandClauseToStringBuilder();
			string text = string.Empty;
			bool flag = true;
			if (odataUri.Filter != null)
			{
				text = ODataUriExtensions.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$filter" + "=" + Uri.EscapeDataString(nodeToStringBuilder.TranslateFilterClause(odataUri.Filter));
			}
			if (odataUri.SelectAndExpand != null)
			{
				string text2 = selectExpandClauseToStringBuilder.TranslateSelectExpandClause(odataUri.SelectAndExpand, true);
				if (!string.IsNullOrEmpty(text2))
				{
					text = ODataUriExtensions.WriteQueryPrefixOrSeparator(flag, text);
					flag = false;
					text += text2;
				}
			}
			if (odataUri.OrderBy != null)
			{
				text = ODataUriExtensions.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$orderby" + "=" + Uri.EscapeDataString(nodeToStringBuilder.TranslateOrderByClause(odataUri.OrderBy));
			}
			if (odataUri.Top != null)
			{
				text = ODataUriExtensions.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$top" + "=" + Uri.EscapeDataString(odataUri.Top.ToString());
			}
			if (odataUri.Skip != null)
			{
				text = ODataUriExtensions.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$skip" + "=" + Uri.EscapeDataString(odataUri.Skip.ToString());
			}
			if (odataUri.QueryCount != null)
			{
				text = ODataUriExtensions.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$count" + "=" + Uri.EscapeDataString((odataUri.QueryCount == true) ? "true" : "false");
			}
			if (odataUri.Search != null)
			{
				text = ODataUriExtensions.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$search" + "=" + Uri.EscapeDataString(nodeToStringBuilder.TranslateSearchClause(odataUri.Search));
			}
			if (odataUri.SkipToken != null)
			{
				text = ODataUriExtensions.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$skiptoken" + "=" + Uri.EscapeDataString(odataUri.SkipToken);
			}
			if (odataUri.DeltaToken != null)
			{
				text = ODataUriExtensions.WriteQueryPrefixOrSeparator(flag, text);
				flag = false;
				text = text + "$deltatoken" + "=" + Uri.EscapeDataString(odataUri.DeltaToken);
			}
			if (odataUri.ParameterAliasNodes != null && odataUri.ParameterAliasNodes.Count > 0)
			{
				string text3 = nodeToStringBuilder.TranslateParameterAliasNodes(odataUri.ParameterAliasNodes);
				text = (string.IsNullOrEmpty(text3) ? text : (ODataUriExtensions.WriteQueryPrefixOrSeparator(flag, text) + text3));
			}
			string text4 = odataUri.Path.ToResourcePathString(urlKeyDelimiter) + text;
			if (!(odataUri.ServiceRoot == null))
			{
				return new Uri(odataUri.ServiceRoot, new Uri(text4, 2));
			}
			return new Uri(text4, 2);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001776E File Offset: 0x0001596E
		private static string WriteQueryPrefixOrSeparator(bool writeQueryPrefix, string queryOptions)
		{
			if (writeQueryPrefix)
			{
				return queryOptions + "?";
			}
			return queryOptions + "&";
		}
	}
}
