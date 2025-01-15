using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x02000082 RID: 130
	[NullableContext(1)]
	internal interface ISerializationBinder
	{
		// Token: 0x0600068A RID: 1674
		Type BindToType([Nullable(2)] string assemblyName, string typeName);

		// Token: 0x0600068B RID: 1675
		[NullableContext(2)]
		void BindToName([Nullable(1)] Type serializedType, out string assemblyName, out string typeName);
	}
}
