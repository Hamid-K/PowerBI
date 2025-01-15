using System;
using System.Net;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001E9 RID: 489
	public class IpV4AddressToken : IpAddressToken
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x00020393 File Offset: 0x0001E593
		public IpV4AddressToken(string source, int start, int end, IPAddress address)
			: base(source, start, end, address)
		{
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x000203A0 File Offset: 0x0001E5A0
		public override string EntityName
		{
			get
			{
				return "IPV4 Address";
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x000203A7 File Offset: 0x0001E5A7
		public override double ScoreMultiplier
		{
			get
			{
				return 5.0;
			}
		}
	}
}
