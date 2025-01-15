using System;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006A5 RID: 1701
	public class Usage
	{
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x00066CED File Offset: 0x00064EED
		// (set) Token: 0x060024C0 RID: 9408 RVA: 0x00066CF5 File Offset: 0x00064EF5
		[JsonProperty("prompt_tokens")]
		public int PromptTokens { get; set; }

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x00066CFE File Offset: 0x00064EFE
		// (set) Token: 0x060024C2 RID: 9410 RVA: 0x00066D06 File Offset: 0x00064F06
		[JsonProperty("completion_tokens")]
		public int CompletionTokens { get; set; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x00066D0F File Offset: 0x00064F0F
		// (set) Token: 0x060024C4 RID: 9412 RVA: 0x00066D17 File Offset: 0x00064F17
		[JsonProperty("total_tokens")]
		public int TotalTokens { get; set; }
	}
}
