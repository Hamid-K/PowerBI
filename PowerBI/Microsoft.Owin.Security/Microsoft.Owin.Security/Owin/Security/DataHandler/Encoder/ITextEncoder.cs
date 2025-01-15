using System;

namespace Microsoft.Owin.Security.DataHandler.Encoder
{
	// Token: 0x02000033 RID: 51
	public interface ITextEncoder
	{
		// Token: 0x060000DE RID: 222
		string Encode(byte[] data);

		// Token: 0x060000DF RID: 223
		byte[] Decode(string text);
	}
}
