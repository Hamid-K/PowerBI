using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000E6 RID: 230
	internal interface IQueryBuilder
	{
		// Token: 0x06000DE4 RID: 3556
		QueryParameterReferenceExpression DeclareQueryParameter(ConceptualResultType resultType, string parameterName);
	}
}
