using System;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000099 RID: 153
	internal class JsonStringContract : JsonPrimitiveContract
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x00023319 File Offset: 0x00021519
		public JsonStringContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.String;
		}
	}
}
