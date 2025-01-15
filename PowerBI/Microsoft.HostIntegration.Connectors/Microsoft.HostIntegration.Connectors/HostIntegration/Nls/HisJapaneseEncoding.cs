using System;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000632 RID: 1586
	internal class HisJapaneseEncoding : HisDBCSEncoding
	{
		// Token: 0x0600358F RID: 13711 RVA: 0x000B397D File Offset: 0x000B1B7D
		public HisJapaneseEncoding(HisEncoding.HostCodePages hostCP)
			: base(hostCP)
		{
			this.converter = new HisJapaneseConverter(this);
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x000B3992 File Offset: 0x000B1B92
		public override string EncodingName
		{
			get
			{
				return string.Format("IBM Ebcdic - Japanese - CP{0}", this.CodePage);
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003591 RID: 13713 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsSingleByte
		{
			get
			{
				return false;
			}
		}
	}
}
