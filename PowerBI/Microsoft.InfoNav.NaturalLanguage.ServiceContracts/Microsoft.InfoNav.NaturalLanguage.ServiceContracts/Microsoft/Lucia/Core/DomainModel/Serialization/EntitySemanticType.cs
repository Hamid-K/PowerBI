using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001B0 RID: 432
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum EntitySemanticType
	{
		// Token: 0x0400076E RID: 1902
		Person,
		// Token: 0x0400076F RID: 1903
		Animate,
		// Token: 0x04000770 RID: 1904
		Inanimate,
		// Token: 0x04000771 RID: 1905
		Location,
		// Token: 0x04000772 RID: 1906
		Time,
		// Token: 0x04000773 RID: 1907
		Duration
	}
}
