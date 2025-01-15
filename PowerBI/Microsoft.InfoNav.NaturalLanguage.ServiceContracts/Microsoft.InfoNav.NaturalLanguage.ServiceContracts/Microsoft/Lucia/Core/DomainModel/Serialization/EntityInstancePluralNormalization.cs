using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001B2 RID: 434
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum EntityInstancePluralNormalization
	{
		// Token: 0x04000779 RID: 1913
		Default,
		// Token: 0x0400077A RID: 1914
		Normalized,
		// Token: 0x0400077B RID: 1915
		None
	}
}
