using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001B3 RID: 435
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum EntityVisibility
	{
		// Token: 0x0400077D RID: 1917
		Visible,
		// Token: 0x0400077E RID: 1918
		Hidden,
		// Token: 0x0400077F RID: 1919
		Children
	}
}
