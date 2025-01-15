using System;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006A7 RID: 1703
	[Serializable]
	public class Choice
	{
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x00066F68 File Offset: 0x00065168
		// (set) Token: 0x060024E6 RID: 9446 RVA: 0x00066F70 File Offset: 0x00065170
		[JsonProperty("text")]
		public string Text { get; set; }

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x00066F79 File Offset: 0x00065179
		// (set) Token: 0x060024E8 RID: 9448 RVA: 0x00066F81 File Offset: 0x00065181
		[JsonProperty("index")]
		public int Index { get; set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x00066F8A File Offset: 0x0006518A
		// (set) Token: 0x060024EA RID: 9450 RVA: 0x00066F92 File Offset: 0x00065192
		[JsonProperty("logprobs")]
		public Logprobs Logprobs { get; set; }

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x00066F9B File Offset: 0x0006519B
		// (set) Token: 0x060024EC RID: 9452 RVA: 0x00066FA3 File Offset: 0x000651A3
		[JsonIgnore]
		public string FinishReason { get; set; }

		// Token: 0x060024ED RID: 9453 RVA: 0x00066FAC File Offset: 0x000651AC
		public override string ToString()
		{
			return this.Text;
		}
	}
}
