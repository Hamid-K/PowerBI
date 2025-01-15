using System;

namespace antlr
{
	// Token: 0x0200000B RID: 11
	internal abstract class TokenCreator
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600008B RID: 139
		public abstract string TokenTypeName { get; }

		// Token: 0x0600008C RID: 140
		public abstract IToken Create();
	}
}
