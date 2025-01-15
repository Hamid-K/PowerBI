using System;
using System.Collections.Generic;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.OAuth2
{
	// Token: 0x02000201 RID: 513
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class DeviceAuthHeader
	{
		// Token: 0x060015B4 RID: 5556 RVA: 0x00047D8D File Offset: 0x00045F8D
		public DeviceAuthHeader(string base64EncodedCertificate)
		{
			this.Alg = "RS256";
			this.Type = "JWT";
			this.X5c = new List<string>();
			this.X5c.Add(base64EncodedCertificate);
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00047DC2 File Offset: 0x00045FC2
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x00047DCA File Offset: 0x00045FCA
		[JsonProperty("x5c")]
		public IList<string> X5c { get; set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00047DD3 File Offset: 0x00045FD3
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x00047DDB File Offset: 0x00045FDB
		[JsonProperty("typ")]
		public string Type { get; set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00047DE4 File Offset: 0x00045FE4
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x00047DEC File Offset: 0x00045FEC
		[JsonProperty("alg")]
		public string Alg { get; private set; }
	}
}
