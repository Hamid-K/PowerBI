using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000081 RID: 129
	internal interface ISerializationBinder
	{
		// Token: 0x06000680 RID: 1664
		Type BindToType([Nullable(2)] string assemblyName, string typeName);

		// Token: 0x06000681 RID: 1665
		[NullableContext(2)]
		void BindToName([Nullable(0)] Type serializedType, out string assemblyName, out string typeName);
	}
}
