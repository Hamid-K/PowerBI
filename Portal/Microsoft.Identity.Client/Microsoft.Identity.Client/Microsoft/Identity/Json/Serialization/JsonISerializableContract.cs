using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x0200008F RID: 143
	internal class JsonISerializableContract : JsonContainerContract
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001CF21 File Offset: 0x0001B121
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0001CF29 File Offset: 0x0001B129
		[Nullable(new byte[] { 2, 0 })]
		public ObjectConstructor<object> ISerializableCreator
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0 })]
			set;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001CF32 File Offset: 0x0001B132
		public JsonISerializableContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Serializable;
		}
	}
}
