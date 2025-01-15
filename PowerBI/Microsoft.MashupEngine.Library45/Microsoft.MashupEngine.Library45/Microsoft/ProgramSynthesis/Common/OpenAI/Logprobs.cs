using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006A9 RID: 1705
	[Serializable]
	public class Logprobs
	{
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x00067054 File Offset: 0x00065254
		// (set) Token: 0x060024FB RID: 9467 RVA: 0x0006705C File Offset: 0x0006525C
		[JsonIgnore]
		public List<string> Tokens { get; set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x00067065 File Offset: 0x00065265
		// (set) Token: 0x060024FD RID: 9469 RVA: 0x0006706D File Offset: 0x0006526D
		[JsonProperty("token_logprobs")]
		public List<double> TokenLogprobs { get; set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x00067076 File Offset: 0x00065276
		// (set) Token: 0x060024FF RID: 9471 RVA: 0x0006707E File Offset: 0x0006527E
		[JsonIgnore]
		public IList<IDictionary<string, double>> TopLogprobs { get; set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x00067087 File Offset: 0x00065287
		// (set) Token: 0x06002501 RID: 9473 RVA: 0x0006708F File Offset: 0x0006528F
		[JsonIgnore]
		public List<int> TextOffsets { get; set; }
	}
}
