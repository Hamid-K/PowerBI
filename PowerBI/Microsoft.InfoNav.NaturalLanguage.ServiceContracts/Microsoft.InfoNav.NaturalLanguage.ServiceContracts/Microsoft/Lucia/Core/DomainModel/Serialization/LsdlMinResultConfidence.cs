using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001A6 RID: 422
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum LsdlMinResultConfidence
	{
		// Token: 0x04000744 RID: 1860
		Default,
		// Token: 0x04000745 RID: 1861
		VeryHigh,
		// Token: 0x04000746 RID: 1862
		High,
		// Token: 0x04000747 RID: 1863
		Medium,
		// Token: 0x04000748 RID: 1864
		Low
	}
}
