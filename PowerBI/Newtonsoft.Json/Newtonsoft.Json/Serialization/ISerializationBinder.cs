using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000081 RID: 129
	[NullableContext(1)]
	public interface ISerializationBinder
	{
		// Token: 0x06000689 RID: 1673
		Type BindToType([Nullable(2)] string assemblyName, string typeName);

		// Token: 0x0600068A RID: 1674
		[NullableContext(2)]
		void BindToName([Nullable(1)] Type serializedType, out string assemblyName, out string typeName);
	}
}
