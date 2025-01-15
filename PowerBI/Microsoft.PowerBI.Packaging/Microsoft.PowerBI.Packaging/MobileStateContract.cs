using System;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200000F RID: 15
	public class MobileStateContract
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002854 File Offset: 0x00000A54
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000285C File Offset: 0x00000A5C
		[JsonProperty("pages", Required = Required.Always)]
		public MobileStatePages Pages { get; set; }
	}
}
