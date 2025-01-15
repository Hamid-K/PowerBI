using System;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000631 RID: 1585
	internal class HisHebrewVisualEncoding : HisEncoding
	{
		// Token: 0x0600358A RID: 13706 RVA: 0x000B3961 File Offset: 0x000B1B61
		public HisHebrewVisualEncoding(HisEncoding.HostCodePages hostCP)
			: base(hostCP)
		{
			this.converter = new HisHebrewVisualConverter(this);
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600358B RID: 13707 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsSingleByte
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x00028FA6 File Offset: 0x000271A6
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000AF330 File Offset: 0x000AD530
		public override int GetMaxByteCount(int charCount)
		{
			return charCount * 2;
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x000B3976 File Offset: 0x000B1B76
		public override string EncodingName
		{
			get
			{
				return "IBM Ebcdic - Hebrew  -- CP424";
			}
		}
	}
}
