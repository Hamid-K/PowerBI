using System;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001F8 RID: 504
	internal interface IFunctionCallParser
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001274 RID: 4724
		ExpressionLexer Lexer { get; }

		// Token: 0x06001275 RID: 4725
		QueryToken ParseIdentifierAsFunction(QueryToken parent);
	}
}
