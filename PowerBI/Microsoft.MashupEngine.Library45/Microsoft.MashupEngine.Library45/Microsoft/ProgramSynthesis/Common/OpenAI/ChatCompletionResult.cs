using System;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006A2 RID: 1698
	public class ChatCompletionResult
	{
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x00066BFF File Offset: 0x00064DFF
		// (set) Token: 0x060024A1 RID: 9377 RVA: 0x00066C07 File Offset: 0x00064E07
		[JsonProperty("id")]
		public string Id { get; set; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x00066C10 File Offset: 0x00064E10
		// (set) Token: 0x060024A3 RID: 9379 RVA: 0x00066C18 File Offset: 0x00064E18
		[JsonProperty("model")]
		public string Model { get; set; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x00066C21 File Offset: 0x00064E21
		// (set) Token: 0x060024A5 RID: 9381 RVA: 0x00066C29 File Offset: 0x00064E29
		[JsonProperty("object")]
		public string Object { get; set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x00066C32 File Offset: 0x00064E32
		// (set) Token: 0x060024A7 RID: 9383 RVA: 0x00066C3A File Offset: 0x00064E3A
		[JsonProperty("created")]
		public long Created { get; set; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x00066C43 File Offset: 0x00064E43
		// (set) Token: 0x060024A9 RID: 9385 RVA: 0x00066C4B File Offset: 0x00064E4B
		[JsonProperty("choices")]
		public ChatCompletionResult.Choice[] Choices { get; set; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x00066C54 File Offset: 0x00064E54
		// (set) Token: 0x060024AB RID: 9387 RVA: 0x00066C5C File Offset: 0x00064E5C
		[JsonProperty("error")]
		public ErrorMessage Error { get; set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x00066C65 File Offset: 0x00064E65
		// (set) Token: 0x060024AD RID: 9389 RVA: 0x00066C6D File Offset: 0x00064E6D
		[JsonProperty("usage")]
		public Usage Usage { get; set; }

		// Token: 0x020006A3 RID: 1699
		public class Choice
		{
			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x060024AF RID: 9391 RVA: 0x00066C76 File Offset: 0x00064E76
			// (set) Token: 0x060024B0 RID: 9392 RVA: 0x00066C7E File Offset: 0x00064E7E
			[JsonProperty("message")]
			public ChatCompletionMessage Message { get; set; }

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x060024B1 RID: 9393 RVA: 0x00066C87 File Offset: 0x00064E87
			// (set) Token: 0x060024B2 RID: 9394 RVA: 0x00066C8F File Offset: 0x00064E8F
			[JsonProperty("index")]
			public int Index { get; set; }

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x060024B3 RID: 9395 RVA: 0x00066C98 File Offset: 0x00064E98
			// (set) Token: 0x060024B4 RID: 9396 RVA: 0x00066CA0 File Offset: 0x00064EA0
			[JsonProperty("finish_reason")]
			public string FinishReason { get; set; }
		}
	}
}
