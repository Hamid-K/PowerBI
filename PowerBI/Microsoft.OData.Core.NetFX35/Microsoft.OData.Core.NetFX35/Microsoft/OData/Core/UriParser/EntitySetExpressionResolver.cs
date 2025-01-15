using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x0200023A RID: 570
	internal static class EntitySetExpressionResolver
	{
		// Token: 0x06001471 RID: 5233 RVA: 0x000497F0 File Offset: 0x000479F0
		internal static IEdmEntitySet ResolveEntitySetFromExpression(IEdmExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			EdmExpressionKind expressionKind = expression.ExpressionKind;
			if (expressionKind == EdmExpressionKind.EntitySetReference)
			{
				return ((IEdmEntitySetReferenceExpression)expression).ReferencedEntitySet;
			}
			throw new NotSupportedException(Strings.Nodes_NonStaticEntitySetExpressionsAreNotSupportedInThisRelease);
		}
	}
}
