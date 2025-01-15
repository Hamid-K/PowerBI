using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000062 RID: 98
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	internal sealed class UnconditionalSuppressMessageAttribute : Attribute
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x0000B2C9 File Offset: 0x000094C9
		public UnconditionalSuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000B2DF File Offset: 0x000094DF
		public string Category { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B2E7 File Offset: 0x000094E7
		public string CheckId { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000B2EF File Offset: 0x000094EF
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000B2F7 File Offset: 0x000094F7
		public string Scope { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000B300 File Offset: 0x00009500
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000B308 File Offset: 0x00009508
		public string Target { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000B311 File Offset: 0x00009511
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000B319 File Offset: 0x00009519
		public string MessageId { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000B322 File Offset: 0x00009522
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000B32A File Offset: 0x0000952A
		public string Justification { get; set; }
	}
}
