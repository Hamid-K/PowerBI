using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000166 RID: 358
	internal sealed class UriTemplateParser
	{
		// Token: 0x06001247 RID: 4679 RVA: 0x00037963 File Offset: 0x00035B63
		internal static bool IsValidTemplateLiteral(string literalText)
		{
			return !string.IsNullOrEmpty(literalText) && literalText.StartsWith("{", StringComparison.Ordinal) && literalText.EndsWith("}", StringComparison.Ordinal);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00037989 File Offset: 0x00035B89
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
