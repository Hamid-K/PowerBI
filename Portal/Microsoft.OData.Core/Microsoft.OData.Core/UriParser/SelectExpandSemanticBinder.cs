using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000134 RID: 308
	internal sealed class SelectExpandSemanticBinder
	{
		// Token: 0x0600104A RID: 4170 RVA: 0x0002B348 File Offset: 0x00029548
		public static SelectExpandClause Bind(ODataPathInfo odataPathInfo, ExpandToken expandToken, SelectToken selectToken, ODataUriParserConfiguration configuration, BindingState state)
		{
			ExpandToken expandToken2 = ExpandTreeNormalizer.NormalizeExpandTree(expandToken);
			SelectToken selectToken2 = SelectTreeNormalizer.NormalizeSelectTree(selectToken);
			SelectExpandBinder selectExpandBinder = new SelectExpandBinder(configuration, odataPathInfo, state);
			SelectExpandClause selectExpandClause = selectExpandBinder.Bind(expandToken2, selectToken2);
			SelectExpandClauseFinisher.AddExplicitNavPropLinksWhereNecessary(selectExpandClause);
			new ExpandDepthAndCountValidator(configuration.Settings.MaximumExpansionDepth, configuration.Settings.MaximumExpansionCount).Validate(selectExpandClause);
			return selectExpandClause;
		}
	}
}
