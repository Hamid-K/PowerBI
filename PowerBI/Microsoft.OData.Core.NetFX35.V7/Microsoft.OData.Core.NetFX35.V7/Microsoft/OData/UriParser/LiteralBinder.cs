using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000EC RID: 236
	internal sealed class LiteralBinder
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x0001DD2C File Offset: 0x0001BF2C
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
