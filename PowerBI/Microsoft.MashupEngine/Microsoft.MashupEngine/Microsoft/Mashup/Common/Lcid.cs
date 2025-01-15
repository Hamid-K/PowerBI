using System;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BFD RID: 7165
	public static class Lcid
	{
		// Token: 0x0600B2E7 RID: 45799 RVA: 0x002469CC File Offset: 0x00244BCC
		public static bool IsTransient(int lcid)
		{
			if (lcid <= 12288)
			{
				if (lcid <= 8192)
				{
					if (lcid != 2048 && lcid != 4096 && lcid != 8192)
					{
						return false;
					}
				}
				else if (lcid <= 10240)
				{
					if (lcid != 9216 && lcid != 10240)
					{
						return false;
					}
				}
				else if (lcid != 11264 && lcid != 12288)
				{
					return false;
				}
			}
			else if (lcid <= 15360)
			{
				if (lcid != 13312 && lcid != 14336 && lcid != 15360)
				{
					return false;
				}
			}
			else if (lcid <= 17408)
			{
				if (lcid != 16384 && lcid != 17408)
				{
					return false;
				}
			}
			else if (lcid != 18432 && lcid != 19456)
			{
				return false;
			}
			return true;
		}
	}
}
