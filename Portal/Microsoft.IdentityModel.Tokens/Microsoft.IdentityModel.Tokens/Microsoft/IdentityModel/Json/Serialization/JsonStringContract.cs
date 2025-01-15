using System;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200009A RID: 154
	internal class JsonStringContract : JsonPrimitiveContract
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x00023915 File Offset: 0x00021B15
		public JsonStringContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.String;
		}
	}
}
