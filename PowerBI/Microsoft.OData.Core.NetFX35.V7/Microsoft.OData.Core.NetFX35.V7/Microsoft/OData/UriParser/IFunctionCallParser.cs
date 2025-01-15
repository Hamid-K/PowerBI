using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000111 RID: 273
	internal interface IFunctionCallParser
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000CD5 RID: 3285
		ExpressionLexer Lexer { get; }

		// Token: 0x06000CD6 RID: 3286
		QueryToken ParseIdentifierAsFunction(QueryToken parent);
	}
}
