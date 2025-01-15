using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000D9 RID: 217
	internal enum JsonTokenType
	{
		// Token: 0x04000322 RID: 802
		String,
		// Token: 0x04000323 RID: 803
		Number,
		// Token: 0x04000324 RID: 804
		Boolean,
		// Token: 0x04000325 RID: 805
		Null,
		// Token: 0x04000326 RID: 806
		ObjectOpen,
		// Token: 0x04000327 RID: 807
		ObjectClose,
		// Token: 0x04000328 RID: 808
		ArrayOpen,
		// Token: 0x04000329 RID: 809
		ArrayClose,
		// Token: 0x0400032A RID: 810
		Colon,
		// Token: 0x0400032B RID: 811
		Comma,
		// Token: 0x0400032C RID: 812
		EndOfFile
	}
}
