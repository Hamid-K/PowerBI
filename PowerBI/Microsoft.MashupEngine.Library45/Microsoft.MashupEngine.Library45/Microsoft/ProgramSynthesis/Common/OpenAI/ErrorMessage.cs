using System;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006A4 RID: 1700
	public class ErrorMessage
	{
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x00066CA9 File Offset: 0x00064EA9
		// (set) Token: 0x060024B7 RID: 9399 RVA: 0x00066CB1 File Offset: 0x00064EB1
		[JsonProperty("message")]
		public string Message { get; set; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x00066CBA File Offset: 0x00064EBA
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x00066CC2 File Offset: 0x00064EC2
		[JsonProperty("code")]
		public string Code { get; set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x00066CCB File Offset: 0x00064ECB
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x00066CD3 File Offset: 0x00064ED3
		[JsonProperty("type")]
		public string Type { get; set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x00066CDC File Offset: 0x00064EDC
		// (set) Token: 0x060024BD RID: 9405 RVA: 0x00066CE4 File Offset: 0x00064EE4
		[JsonProperty("param")]
		public string Param { get; set; }
	}
}
