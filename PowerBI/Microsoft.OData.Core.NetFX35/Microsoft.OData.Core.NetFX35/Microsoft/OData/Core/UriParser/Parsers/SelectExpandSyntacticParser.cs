using System;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x02000213 RID: 531
	internal static class SelectExpandSyntacticParser
	{
		// Token: 0x06001361 RID: 4961 RVA: 0x00046BEC File Offset: 0x00044DEC
		public static void Parse(string selectClause, string expandClause, IEdmStructuredType parentEntityType, ODataUriParserConfiguration configuration, out ExpandToken expandTree, out SelectToken selectTree)
		{
			SelectExpandParser selectExpandParser = new SelectExpandParser(selectClause, configuration.Settings.SelectExpandLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier)
			{
				MaxPathDepth = configuration.Settings.PathLimit
			};
			selectTree = selectExpandParser.ParseSelect();
			SelectExpandParser selectExpandParser2 = new SelectExpandParser(configuration.Resolver, expandClause, parentEntityType, configuration.Settings.SelectExpandLimit, configuration.EnableCaseInsensitiveUriFunctionIdentifier)
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
