using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	internal sealed class UnconditionalSuppressMessageAttribute : Attribute
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000030DC File Offset: 0x000012DC
		public UnconditionalSuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000030F2 File Offset: 0x000012F2
		public string Category { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000030FA File Offset: 0x000012FA
		public string CheckId { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00003102 File Offset: 0x00001302
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000310A File Offset: 0x0000130A
		public string Scope { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00003113 File Offset: 0x00001313
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000311B File Offset: 0x0000131B
		public string Target { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00003124 File Offset: 0x00001324
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000312C File Offset: 0x0000132C
		public string MessageId { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00003135 File Offset: 0x00001335
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000313D File Offset: 0x0000133D
		public string Justification { get; set; }
	}
}
