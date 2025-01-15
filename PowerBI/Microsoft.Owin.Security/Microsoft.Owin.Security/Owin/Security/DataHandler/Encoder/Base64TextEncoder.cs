using System;

namespace Microsoft.Owin.Security.DataHandler.Encoder
{
	// Token: 0x02000031 RID: 49
	public class Base64TextEncoder : ITextEncoder
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00003B51 File Offset: 0x00001D51
		public string Encode(byte[] data)
		{
			return Convert.ToBase64String(data);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003B59 File Offset: 0x00001D59
		public byte[] Decode(string text)
		{
			return Convert.FromBase64String(text);
		}
	}
}
