using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007F RID: 127
	[NullableContext(1)]
	public interface IContractResolver
	{
		// Token: 0x06000684 RID: 1668
		JsonContract ResolveContract(Type type);
	}
}
