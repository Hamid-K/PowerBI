using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001A8 RID: 424
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum PropertyState
	{
		// Token: 0x04000750 RID: 1872
		Default,
		// Token: 0x04000751 RID: 1873
		Authored,
		// Token: 0x04000752 RID: 1874
		Generated,
		// Token: 0x04000753 RID: 1875
		Suggested
	}
}
