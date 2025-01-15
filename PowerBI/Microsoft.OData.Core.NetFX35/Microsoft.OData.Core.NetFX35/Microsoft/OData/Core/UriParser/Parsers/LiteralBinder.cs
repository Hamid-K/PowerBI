using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001CE RID: 462
	internal sealed class LiteralBinder
	{
		// Token: 0x06001133 RID: 4403 RVA: 0x0003CCA8 File Offset: 0x0003AEA8
		internal static ConstantNode BindLiteral(LiteralToken literalToken)
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
	}
}
