using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	internal sealed class UnconditionalSuppressMessageAttribute : Attribute
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000236C File Offset: 0x0000056C
		public UnconditionalSuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002382 File Offset: 0x00000582
		public string Category { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000238A File Offset: 0x0000058A
		public string CheckId { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002392 File Offset: 0x00000592
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000239A File Offset: 0x0000059A
		public string Scope { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023A3 File Offset: 0x000005A3
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000023AB File Offset: 0x000005AB
		public string Target { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023B4 File Offset: 0x000005B4
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000023BC File Offset: 0x000005BC
		public string MessageId { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000023C5 File Offset: 0x000005C5
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000023CD File Offset: 0x000005CD
		public string Justification { get; set; }
	}
}
