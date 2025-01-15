using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200011D RID: 285
	internal static class SelectExpandSyntacticParser
	{
		// Token: 0x06000D3E RID: 3390 RVA: 0x00026908 File Offset: 0x00024B08
		public static void Parse(string selectClause, string expandClause, IEdmStructuredType parentEntityType, ODataUriParserConfiguration configuration, out ExpandToken expandTree, out SelectToken selectTree)
		{
			SelectExpandParser selectExpandParser = new SelectExpandParser(selectClause, configuration.Settings.SelectExpandLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier, false)
			{
				MaxPathDepth = configuration.Settings.PathLimit
			};
			selectTree = selectExpandParser.ParseSelect();
			SelectExpandParser selectExpandParser2 = new SelectExpandParser(configuration.Resolver, expandClause, parentEntityType, configuration.Settings.SelectExpandLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier, configuration.EnableNoDollarQueryOptions)
			{
				MaxPathDepth = configuration.Settings.PathLimit,
				MaxFilterDepth = configuration.Settings.FilterLimit,
				MaxOrderByDepth = configuration.Settings.OrderByLimit,
				MaxSearchDepth = configuration.Settings.SearchLimit
			};
			expandTree = selectExpandParser2.ParseExpand();
		}
	}
}
