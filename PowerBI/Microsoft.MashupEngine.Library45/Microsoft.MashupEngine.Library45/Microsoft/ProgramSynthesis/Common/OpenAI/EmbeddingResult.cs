using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006AD RID: 1709
	[Serializable]
	public class EmbeddingResult
	{
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x0006719A File Offset: 0x0006539A
		// (set) Token: 0x0600251A RID: 9498 RVA: 0x000671A2 File Offset: 0x000653A2
		[JsonProperty("model")]
		public string Model { get; set; }

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x000671AB File Offset: 0x000653AB
		// (set) Token: 0x0600251C RID: 9500 RVA: 0x000671B3 File Offset: 0x000653B3
		[JsonProperty("usage")]
		public EmbeddingUsage Usage { get; set; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x000671BC File Offset: 0x000653BC
		// (set) Token: 0x0600251E RID: 9502 RVA: 0x000671C4 File Offset: 0x000653C4
		[JsonProperty("data")]
		public List<EmbeddingData> Data { get; set; }
	}
}
