using System;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006AB RID: 1707
	[Serializable]
	public class EmbeddingUsage
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x00067156 File Offset: 0x00065356
		// (set) Token: 0x06002510 RID: 9488 RVA: 0x0006715E File Offset: 0x0006535E
		[JsonProperty("prompt_tokens")]
		public int PromptTokens { get; set; }

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x00067167 File Offset: 0x00065367
		// (set) Token: 0x06002512 RID: 9490 RVA: 0x0006716F File Offset: 0x0006536F
		[JsonProperty("total_tokens")]
		public string TotalTokens { get; set; }
	}
}
