using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x02000090 RID: 144
	internal class JsonISerializableContract : JsonContainerContract
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001D4F5 File Offset: 0x0001B6F5
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x0001D4FD File Offset: 0x0001B6FD
		[Nullable(new byte[] { 2, 1 })]
		public ObjectConstructor<object> ISerializableCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001D506 File Offset: 0x0001B706
		[NullableContext(1)]
		public JsonISerializableContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Serializable;
		}
	}
}
