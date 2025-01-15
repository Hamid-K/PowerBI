using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001A7 RID: 423
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum State
	{
		// Token: 0x0400074A RID: 1866
		Authored,
		// Token: 0x0400074B RID: 1867
		Generated,
		// Token: 0x0400074C RID: 1868
		Suggested,
		// Token: 0x0400074D RID: 1869
		Deleted,
		// Token: 0x0400074E RID: 1870
		Default
	}
}
