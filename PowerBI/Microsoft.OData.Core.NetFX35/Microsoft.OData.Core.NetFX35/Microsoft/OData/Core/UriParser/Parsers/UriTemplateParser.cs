using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x02000218 RID: 536
	internal sealed class UriTemplateParser
	{
		// Token: 0x06001394 RID: 5012 RVA: 0x000482C4 File Offset: 0x000464C4
		internal static bool IsValidTemplateLiteral(string literalText)
		{
			return !string.IsNullOrEmpty(literalText) && literalText.StartsWith("{", 4) && literalText.EndsWith("}", 4);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000482EC File Offset: 0x000464EC
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
