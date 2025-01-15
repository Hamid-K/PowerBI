using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000099 RID: 153
	public class JsonStringContract : JsonPrimitiveContract
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x000238F1 File Offset: 0x00021AF1
		[NullableContext(1)]
		public JsonStringContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.String;
		}
	}
}
