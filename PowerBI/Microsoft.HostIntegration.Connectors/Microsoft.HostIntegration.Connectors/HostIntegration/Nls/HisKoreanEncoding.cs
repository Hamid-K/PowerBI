using System;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000637 RID: 1591
	internal class HisKoreanEncoding : HisDBCSEncoding
	{
		// Token: 0x060035A0 RID: 13728 RVA: 0x000B4887 File Offset: 0x000B2A87
		public HisKoreanEncoding(HisEncoding.HostCodePages hostCP)
			: base(hostCP)
		{
			this.converter = new HisKoreanConverter(this);
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000B489C File Offset: 0x000B2A9C
		public override string EncodingName
		{
			get
			{
				return "IBM Ebcdic - Korea (Extended) -- CP933";
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsSingleByte
		{
			get
			{
				return false;
			}
		}
	}
}
