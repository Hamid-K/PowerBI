using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000090 RID: 144
	public class JsonLinqContract : JsonContract
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x0001D4CE File Offset: 0x0001B6CE
		[NullableContext(1)]
		public JsonLinqContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Linq;
		}
	}
}
