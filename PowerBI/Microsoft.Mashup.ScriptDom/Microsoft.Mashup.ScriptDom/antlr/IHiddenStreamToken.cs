using System;

namespace antlr
{
	// Token: 0x02000012 RID: 18
	internal interface IHiddenStreamToken : IToken
	{
		// Token: 0x060000C3 RID: 195
		IHiddenStreamToken getHiddenAfter();

		// Token: 0x060000C4 RID: 196
		void setHiddenAfter(IHiddenStreamToken t);

		// Token: 0x060000C5 RID: 197
		IHiddenStreamToken getHiddenBefore();

		// Token: 0x060000C6 RID: 198
		void setHiddenBefore(IHiddenStreamToken t);
	}
}
