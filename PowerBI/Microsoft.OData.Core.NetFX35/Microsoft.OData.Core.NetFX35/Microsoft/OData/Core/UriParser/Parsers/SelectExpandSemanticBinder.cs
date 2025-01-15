using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001DA RID: 474
	internal sealed class SelectExpandSemanticBinder
	{
		// Token: 0x06001173 RID: 4467 RVA: 0x0003E054 File Offset: 0x0003C254
		public SelectExpandClause Bind(IEdmStructuredType elementType, IEdmNavigationSource navigationSource, ExpandToken expandToken, SelectToken selectToken, ODataUriParserConfiguration configuration)
		{
			ExpandToken expandToken2 = SelectExpandSyntacticUnifier.Combine(expandToken, selectToken);
			ExpandTreeNormalizer expandTreeNormalizer = new ExpandTreeNormalizer();
			ExpandToken expandToken3 = expandTreeNormalizer.NormalizeExpandTree(expandToken2);
			SelectExpandBinder selectExpandBinder = new SelectExpandBinder(configuration, elementType, navigationSource);
			SelectExpandClause selectExpandClause = selectExpandBinder.Bind(expandToken3);
			SelectExpandClauseFinisher.AddExplicitNavPropLinksWhereNecessary(selectExpandClause);
			new ExpandDepthAndCountValidator(configuration.Settings.MaximumExpansionDepth, configuration.Settings.MaximumExpansionCount).Validate(selectExpandClause);
			return selectExpandClause;
		}
	}
}
