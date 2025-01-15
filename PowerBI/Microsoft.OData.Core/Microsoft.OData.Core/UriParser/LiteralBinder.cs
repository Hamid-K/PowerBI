using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012A RID: 298
	internal sealed class LiteralBinder
	{
		// Token: 0x06001005 RID: 4101 RVA: 0x00029608 File Offset: 0x00027808
		internal static QueryNode BindLiteral(LiteralToken literalToken)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralToken>(literalToken, "literalToken");
			if (string.IsNullOrEmpty(literalToken.OriginalText))
			{
				return new ConstantNode(literalToken.Value);
			}
			if (literalToken.ExpectedEdmTypeReference != null)
			{
				return new ConstantNode(literalToken.Value, literalToken.OriginalText, literalToken.ExpectedEdmTypeReference);
			}
			return new ConstantNode(literalToken.Value, literalToken.OriginalText);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0002966C File Offset: 0x0002786C
		internal static QueryNode BindInLiteral(LiteralToken literalToken)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralToken>(literalToken, "literalToken");
			if (string.IsNullOrEmpty(literalToken.OriginalText))
			{
				return new ConstantNode(literalToken.Value);
			}
			if (literalToken.ExpectedEdmTypeReference != null)
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = literalToken.ExpectedEdmTypeReference as IEdmCollectionTypeReference;
				if (edmCollectionTypeReference != null)
				{
					ODataCollectionValue odataCollectionValue = literalToken.Value as ODataCollectionValue;
					if (odataCollectionValue != null)
					{
						return new CollectionConstantNode(odataCollectionValue.Items, literalToken.OriginalText, edmCollectionTypeReference);
					}
				}
				return new ConstantNode(literalToken.Value, literalToken.OriginalText, literalToken.ExpectedEdmTypeReference);
			}
			return new ConstantNode(literalToken.Value, literalToken.OriginalText);
		}
	}
}
