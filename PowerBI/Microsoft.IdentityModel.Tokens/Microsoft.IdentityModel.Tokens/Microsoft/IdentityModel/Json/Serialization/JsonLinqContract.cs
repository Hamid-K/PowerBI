using System;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x02000091 RID: 145
	internal class JsonLinqContract : JsonContract
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x0001D516 File Offset: 0x0001B716
		public JsonLinqContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Linq;
		}
	}
}
