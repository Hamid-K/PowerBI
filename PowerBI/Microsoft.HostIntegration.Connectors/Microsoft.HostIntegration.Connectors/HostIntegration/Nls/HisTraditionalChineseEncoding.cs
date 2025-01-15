using System;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x0200063E RID: 1598
	internal class HisTraditionalChineseEncoding : HisDBCSEncoding
	{
		// Token: 0x060035BB RID: 13755 RVA: 0x000B5C48 File Offset: 0x000B3E48
		public HisTraditionalChineseEncoding(HisEncoding.HostCodePages hostCP)
			: base(hostCP)
		{
			this.converter = new HisTraditionalChineseConverter(this);
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060035BC RID: 13756 RVA: 0x000B5C5D File Offset: 0x000B3E5D
		public override string EncodingName
		{
			get
			{
				return "IBM Ebcdic - Traditional Chinese (Extended) -- CP937";
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060035BD RID: 13757 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsSingleByte
		{
			get
			{
				return false;
			}
		}
	}
}
