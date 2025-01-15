using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000100 RID: 256
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum AdjectiveForm
	{
		// Token: 0x04000578 RID: 1400
		Simple,
		// Token: 0x04000579 RID: 1401
		Comparative,
		// Token: 0x0400057A RID: 1402
		Superlative
	}
}
