using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001B1 RID: 433
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum EntityInstanceIndex
	{
		// Token: 0x04000775 RID: 1909
		Default,
		// Token: 0x04000776 RID: 1910
		All,
		// Token: 0x04000777 RID: 1911
		None
	}
}
