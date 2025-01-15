using System;

namespace Microsoft.Owin.Security.DataHandler.Encoder
{
	// Token: 0x02000032 RID: 50
	public class Base64UrlTextEncoder : ITextEncoder
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00003B69 File Offset: 0x00001D69
		public string Encode(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return Convert.ToBase64String(data).TrimEnd(new char[] { '=' }).Replace('+', '-')
				.Replace('/', '_');
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003BA1 File Offset: 0x00001DA1
		public byte[] Decode(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return Convert.FromBase64String(Base64UrlTextEncoder.Pad(text.Replace('-', '+').Replace('_', '/')));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003BD0 File Offset: 0x00001DD0
		private static string Pad(string text)
		{
			int padding = 3 - (text.Length + 3) % 4;
			if (padding == 0)
			{
				return text;
			}
			return text + new string('=', padding);
		}
	}
}
