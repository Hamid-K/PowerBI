using System;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006AC RID: 1708
	[Serializable]
	public class EmbeddingData
	{
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x00067178 File Offset: 0x00065378
		// (set) Token: 0x06002515 RID: 9493 RVA: 0x00067180 File Offset: 0x00065380
		[JsonProperty("embedding")]
		public double[] Embedding { get; set; }

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x00067189 File Offset: 0x00065389
		// (set) Token: 0x06002517 RID: 9495 RVA: 0x00067191 File Offset: 0x00065391
		[JsonProperty("index")]
		public int Index { get; set; }
	}
}
