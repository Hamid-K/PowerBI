using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000121 RID: 289
	internal sealed class UriTemplateParser
	{
		// Token: 0x06000D6F RID: 3439 RVA: 0x0002802F File Offset: 0x0002622F
		internal static bool IsValidTemplateLiteral(string literalText)
		{
			return !string.IsNullOrEmpty(literalText) && literalText.StartsWith("{", 4) && literalText.EndsWith("}", 4);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00028055 File Offset: 0x00026255
		internal static bool TryParseLiteral(string literalText, IEdmTypeReference expectedType, out UriTemplateExpression expression)
		{
			if (UriTemplateParser.IsValidTemplateLiteral(literalText))
			{
				expression = new UriTemplateExpression
				{
					LiteralText = literalText,
					ExpectedType = expectedType
				};
				return true;
			}
			expression = null;
			return false;
		}
	}
}
