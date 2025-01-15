using System;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000619 RID: 1561
	internal class HisSimplifiedChineseEncoding : HisDBCSEncoding
	{
		// Token: 0x060034C3 RID: 13507 RVA: 0x000B0B7A File Offset: 0x000AED7A
		public HisSimplifiedChineseEncoding(HisEncoding.HostCodePages hostCP)
			: base(hostCP)
		{
			this.converter = new HisSimplifiedChineseConverter(this);
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060034C4 RID: 13508 RVA: 0x000B0B8F File Offset: 0x000AED8F
		public override string EncodingName
		{
			get
			{
				return "IBM Ebcdic - Simplified Chinese (Extended) -- CP935";
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060034C5 RID: 13509 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsSingleByte
		{
			get
			{
				return false;
			}
		}
	}
}
