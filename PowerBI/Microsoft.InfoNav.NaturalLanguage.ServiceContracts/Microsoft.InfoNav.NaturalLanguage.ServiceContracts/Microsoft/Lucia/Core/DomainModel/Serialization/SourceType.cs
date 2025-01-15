using System;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001CD RID: 461
	[JsonConverter(typeof(StrictStringEnumConverter))]
	public enum SourceType
	{
		// Token: 0x040007DD RID: 2013
		Default,
		// Token: 0x040007DE RID: 2014
		User,
		// Token: 0x040007DF RID: 2015
		Internal,
		// Token: 0x040007E0 RID: 2016
		External
	}
}
