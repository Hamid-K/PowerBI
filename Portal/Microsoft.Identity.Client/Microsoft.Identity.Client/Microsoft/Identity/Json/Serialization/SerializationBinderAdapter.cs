using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x020000A2 RID: 162
	internal class SerializationBinderAdapter : ISerializationBinder
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x00023C78 File Offset: 0x00021E78
		public SerializationBinderAdapter(SerializationBinder serializationBinder)
		{
			this.SerializationBinder = serializationBinder;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00023C87 File Offset: 0x00021E87
		public Type BindToType([Nullable(2)] string assemblyName, string typeName)
		{
			return this.SerializationBinder.BindToType(assemblyName, typeName);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00023C96 File Offset: 0x00021E96
		[NullableContext(2)]
		public void BindToName([Nullable(0)] Type serializedType, out string assemblyName, out string typeName)
		{
			this.SerializationBinder.BindToName(serializedType, out assemblyName, out typeName);
		}

		// Token: 0x040002CB RID: 715
		public readonly SerializationBinder SerializationBinder;
	}
}
