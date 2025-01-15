using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000161 RID: 353
	internal static class SelectExpandSyntacticParser
	{
		// Token: 0x06001207 RID: 4615 RVA: 0x000358A8 File Offset: 0x00033AA8
		public static void Parse(string selectClause, string expandClause, IEdmStructuredType parentStructuredType, ODataUriParserConfiguration configuration, out ExpandToken expandTree, out SelectToken selectTree)
		{
			SelectExpandParser selectExpandParser = new SelectExpandParser(selectClause, configuration.Settings.SelectExpandLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier, false)
			{
				MaxPathDepth = configuration.Settings.PathLimit
			};
			selectTree = selectExpandParser.ParseSelect();
			SelectExpandParser selectExpandParser2 = new SelectExpandParser(configuration.Resolver, expandClause, parentStructuredType, configuration.Settings.SelectExpandLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier, configuration.EnableNoDollarQueryOptions)
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
