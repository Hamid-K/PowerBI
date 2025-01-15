using System;
using System.Net;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001EB RID: 491
	public class IpV6AddressToken : IpAddressToken
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00020393 File Offset: 0x0001E593
		public IpV6AddressToken(string source, int start, int end, IPAddress address)
			: base(source, start, end, address)
		{
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00020467 File Offset: 0x0001E667
		public override string EntityName
		{
			get
			{
				return "IPV6 Address";
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x000203A7 File Offset: 0x0001E5A7
		public override double ScoreMultiplier
		{
			get
			{
				return 5.0;
			}
		}
	}
}
