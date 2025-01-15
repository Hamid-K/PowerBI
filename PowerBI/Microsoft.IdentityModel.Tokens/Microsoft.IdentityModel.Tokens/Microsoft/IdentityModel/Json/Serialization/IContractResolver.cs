using System;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x02000080 RID: 128
	internal interface IContractResolver
	{
		// Token: 0x06000685 RID: 1669
		JsonContract ResolveContract(Type type);
	}
}
