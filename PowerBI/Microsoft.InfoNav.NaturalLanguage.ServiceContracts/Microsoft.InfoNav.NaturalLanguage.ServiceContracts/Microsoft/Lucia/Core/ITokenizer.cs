using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200012A RID: 298
	public interface ITokenizer
	{
		// Token: 0x06000605 RID: 1541
		IEnumerable<IToken> Tokenize(TokenizerRuleContext context, string input);
	}
}
