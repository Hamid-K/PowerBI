using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000154 RID: 340
	internal interface IFunctionCallParser
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001180 RID: 4480
		ExpressionLexer Lexer { get; }

		// Token: 0x06001181 RID: 4481
		bool TryParseIdentifierAsFunction(QueryToken parent, out QueryToken result);
	}
}
