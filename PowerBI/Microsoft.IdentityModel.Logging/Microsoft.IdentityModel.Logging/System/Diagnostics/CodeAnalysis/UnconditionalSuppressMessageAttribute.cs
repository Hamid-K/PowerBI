using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	internal sealed class UnconditionalSuppressMessageAttribute : Attribute
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002078 File Offset: 0x00000278
		public UnconditionalSuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000208E File Offset: 0x0000028E
		public string Category { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002096 File Offset: 0x00000296
		public string CheckId { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000209E File Offset: 0x0000029E
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020A6 File Offset: 0x000002A6
		public string Scope { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020AF File Offset: 0x000002AF
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020B7 File Offset: 0x000002B7
		public string Target { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020C0 File Offset: 0x000002C0
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020C8 File Offset: 0x000002C8
		public string MessageId { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020D1 File Offset: 0x000002D1
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020D9 File Offset: 0x000002D9
		public string Justification { get; set; }
	}
}
