using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x020000A3 RID: 163
	[NullableContext(1)]
	[Nullable(0)]
	internal class SerializationBinderAdapter : ISerializationBinder
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x000242DC File Offset: 0x000224DC
		public SerializationBinderAdapter(SerializationBinder serializationBinder)
		{
			this.SerializationBinder = serializationBinder;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000242EB File Offset: 0x000224EB
		public Type BindToType([Nullable(2)] string assemblyName, string typeName)
		{
			return this.SerializationBinder.BindToType(assemblyName, typeName);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000242FA File Offset: 0x000224FA
		[NullableContext(2)]
		public void BindToName([Nullable(1)] Type serializedType, out string assemblyName, out string typeName)
		{
			this.SerializationBinder.BindToName(serializedType, out assemblyName, out typeName);
		}

		// Token: 0x040002E6 RID: 742
		public readonly SerializationBinder SerializationBinder;
	}
}
