using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001B4 RID: 436
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum EntityNameType
	{
		// Token: 0x04000781 RID: 1921
		None,
		// Token: 0x04000782 RID: 1922
		Name,
		// Token: 0x04000783 RID: 1923
		Identifier
	}
}
