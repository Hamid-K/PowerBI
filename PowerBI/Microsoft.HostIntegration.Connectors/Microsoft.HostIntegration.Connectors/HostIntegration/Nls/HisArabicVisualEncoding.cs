using System;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000614 RID: 1556
	internal class HisArabicVisualEncoding : HisEncoding
	{
		// Token: 0x06003497 RID: 13463 RVA: 0x000AF31B File Offset: 0x000AD51B
		public HisArabicVisualEncoding(HisEncoding.HostCodePages hostCP)
			: base(hostCP)
		{
			this.converter = new HisArabicVisualConverter(this);
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsSingleByte
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x00028FA6 File Offset: 0x000271A6
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000AF330 File Offset: 0x000AD530
		public override int GetMaxByteCount(int charCount)
		{
			return charCount * 2;
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000AF335 File Offset: 0x000AD535
		public override string EncodingName
		{
			get
			{
				return "IBM Ebcdic - Arabic  -- CP420";
			}
		}
	}
}
