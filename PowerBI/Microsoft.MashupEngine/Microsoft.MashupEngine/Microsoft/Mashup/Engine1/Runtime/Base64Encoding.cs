using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001261 RID: 4705
	internal class Base64Encoding
	{
		// Token: 0x06007BF6 RID: 31734 RVA: 0x001AAE54 File Offset: 0x001A9054
		public static string GetString(byte[] bytes)
		{
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x06007BF7 RID: 31735 RVA: 0x001AAE5C File Offset: 0x001A905C
		public static bool TryFromBase64String(string s, out byte[] bytes)
		{
			bytes = new byte[0];
			bool flag;
			try
			{
				bytes = Convert.FromBase64String(s);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			return flag;
		}
	}
}
