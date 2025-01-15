using System;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000009 RID: 9
	public class ExplorationContractSplit
	{
		// Token: 0x0400000A RID: 10
		[JsonProperty("explorationContract", Required = Required.Always)]
		public ExplorationContract ExplorationContract;

		// Token: 0x0400000B RID: 11
		[JsonProperty("mobileState", Required = Required.AllowNull)]
		public MobileStateContract MobileState;
	}
}
