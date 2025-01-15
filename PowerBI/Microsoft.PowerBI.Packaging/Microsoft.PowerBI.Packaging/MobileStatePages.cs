using System;
using Microsoft.PowerBI.Packaging.Project;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000010 RID: 16
	public class MobileStatePages
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000286D File Offset: 0x00000A6D
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002875 File Offset: 0x00000A75
		[JsonProperty("pages", Required = Required.Always)]
		public NonNulls<MobileStatePage> PagesList { get; set; }
	}
}
