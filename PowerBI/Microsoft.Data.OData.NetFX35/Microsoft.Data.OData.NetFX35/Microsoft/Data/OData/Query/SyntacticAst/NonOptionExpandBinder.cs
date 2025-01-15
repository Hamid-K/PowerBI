using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000040 RID: 64
	internal sealed class NonOptionExpandBinder : ExpandBinder
	{
		// Token: 0x06000199 RID: 409 RVA: 0x000071E1 File Offset: 0x000053E1
		public NonOptionExpandBinder(ODataUriParserConfiguration configuration, IEdmEntityType entityType, IEdmEntitySet entitySet)
			: base(configuration, entityType, entitySet)
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000071EC File Offset: 0x000053EC
		protected override SelectExpandClause GenerateSubExpand(IEdmNavigationProperty currentNavProp, ExpandTermToken tokenIn)
		{
			ExpandBinder expandBinder = new NonOptionExpandBinder(base.Configuration, currentNavProp.ToEntityType(), (base.EntitySet != null) ? base.EntitySet.FindNavigationTarget(currentNavProp) : null);
			return expandBinder.Bind(tokenIn.ExpandOption);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000722E File Offset: 0x0000542E
		protected override SelectExpandClause DecorateExpandWithSelect(SelectExpandClause subExpand, IEdmNavigationProperty currentNavProp, SelectToken select)
		{
			return subExpand;
		}
	}
}
