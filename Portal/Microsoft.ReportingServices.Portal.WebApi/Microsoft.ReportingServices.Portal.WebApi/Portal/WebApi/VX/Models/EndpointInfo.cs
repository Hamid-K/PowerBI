using System;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.WebApi.VX.Models
{
	// Token: 0x02000007 RID: 7
	public sealed class EndpointInfo
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002133 File Offset: 0x00000333
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000213B File Offset: 0x0000033B
		[JsonProperty(PropertyName = "supportedEndpoints")]
		public string[] SupportedEndpoints { get; set; }
	}
}
