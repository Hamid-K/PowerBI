using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001A5 RID: 421
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum LsdlDynamicImprovement
	{
		// Token: 0x0400073F RID: 1855
		Default,
		// Token: 0x04000740 RID: 1856
		Full,
		// Token: 0x04000741 RID: 1857
		HighConfidence,
		// Token: 0x04000742 RID: 1858
		None
	}
}
