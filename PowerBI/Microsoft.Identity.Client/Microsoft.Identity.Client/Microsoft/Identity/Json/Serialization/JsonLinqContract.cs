using System;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000090 RID: 144
	internal class JsonLinqContract : JsonContract
	{
		// Token: 0x060006FA RID: 1786 RVA: 0x0001CF42 File Offset: 0x0001B142
		public JsonLinqContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Linq;
		}
	}
}
