using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000F7 RID: 247
	internal sealed class SelectExpandSemanticBinder
	{
		// Token: 0x06000BE2 RID: 3042 RVA: 0x0001F274 File Offset: 0x0001D474
		public static SelectExpandClause Bind(ODataPathInfo odataPathInfo, ExpandToken expandToken, SelectToken selectToken, ODataUriParserConfiguration configuration)
		{
			ExpandToken expandToken2 = SelectExpandSyntacticUnifier.Combine(expandToken, selectToken);
			ExpandTreeNormalizer expandTreeNormalizer = new ExpandTreeNormalizer();
			ExpandToken expandToken3 = expandTreeNormalizer.NormalizeExpandTree(expandToken2);
			SelectExpandBinder selectExpandBinder = new SelectExpandBinder(configuration, odataPathInfo);
			SelectExpandClause selectExpandClause = selectExpandBinder.Bind(expandToken3);
			SelectExpandClauseFinisher.AddExplicitNavPropLinksWhereNecessary(selectExpandClause);
			new ExpandDepthAndCountValidator(configuration.Settings.MaximumExpansionDepth, configuration.Settings.MaximumExpansionCount).Validate(selectExpandClause);
			return selectExpandClause;
		}
	}
}
