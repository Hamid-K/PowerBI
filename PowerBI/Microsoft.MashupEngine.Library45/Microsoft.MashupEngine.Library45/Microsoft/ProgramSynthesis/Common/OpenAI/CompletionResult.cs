using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Common.OpenAI
{
	// Token: 0x020006A8 RID: 1704
	[Serializable]
	public class CompletionResult
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x00066FB4 File Offset: 0x000651B4
		// (set) Token: 0x060024F0 RID: 9456 RVA: 0x00066FBC File Offset: 0x000651BC
		[JsonIgnore]
		public string Id { get; set; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x00066FC5 File Offset: 0x000651C5
		// (set) Token: 0x060024F2 RID: 9458 RVA: 0x00066FCD File Offset: 0x000651CD
		[JsonIgnore]
		public long CreatedUnixTime { get; set; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x00066FD8 File Offset: 0x000651D8
		[JsonIgnore]
		public DateTime Created
		{
			get
			{
				return default(DateTimeOffset).DateTime;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x00066FF3 File Offset: 0x000651F3
		// (set) Token: 0x060024F5 RID: 9461 RVA: 0x00066FFB File Offset: 0x000651FB
		[JsonProperty("choices")]
		public List<Choice> Completions { get; set; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x00067004 File Offset: 0x00065204
		// (set) Token: 0x060024F7 RID: 9463 RVA: 0x0006700C File Offset: 0x0006520C
		[JsonIgnore]
		public string RequestId { get; set; }

		// Token: 0x060024F8 RID: 9464 RVA: 0x00067015 File Offset: 0x00065215
		public override string ToString()
		{
			if (this.Completions != null && this.Completions.Count > 0)
			{
				return this.Completions[0].ToString();
			}
			return "CompletionResult " + this.Id + " has no valid output";
		}
	}
}
