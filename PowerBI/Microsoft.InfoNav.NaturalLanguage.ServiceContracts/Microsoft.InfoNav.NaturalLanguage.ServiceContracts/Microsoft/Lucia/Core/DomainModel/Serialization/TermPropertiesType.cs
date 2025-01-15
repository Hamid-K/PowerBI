using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001D0 RID: 464
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum TermPropertiesType
	{
		// Token: 0x040007E9 RID: 2025
		Noun,
		// Token: 0x040007EA RID: 2026
		Verb,
		// Token: 0x040007EB RID: 2027
		Adjective,
		// Token: 0x040007EC RID: 2028
		Preposition
	}
}
