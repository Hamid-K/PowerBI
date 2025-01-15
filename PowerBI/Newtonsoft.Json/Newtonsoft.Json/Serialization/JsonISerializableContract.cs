using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008F RID: 143
	public class JsonISerializableContract : JsonContainerContract
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001D4AD File Offset: 0x0001B6AD
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001D4B5 File Offset: 0x0001B6B5
		[Nullable(new byte[] { 2, 1 })]
		public ObjectConstructor<object> ISerializableCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001D4BE File Offset: 0x0001B6BE
		[NullableContext(1)]
		public JsonISerializableContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Serializable;
		}
	}
}
