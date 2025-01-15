using System;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000628 RID: 1576
	internal class HisDBCSEncoding : HisEncoding
	{
		// Token: 0x06003516 RID: 13590 RVA: 0x000B1787 File Offset: 0x000AF987
		public HisDBCSEncoding(HisEncoding.HostCodePages hostCP)
			: base(hostCP)
		{
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x00028FA6 File Offset: 0x000271A6
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000B1790 File Offset: 0x000AF990
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount % 2 == 0)
			{
				return 5 * charCount / 2;
			}
			return (5 * charCount + 3) / 2;
		}
	}
}
