using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000014 RID: 20
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	internal sealed class UnconditionalSuppressMessageAttribute : Attribute
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002189 File Offset: 0x00000389
		[NullableContext(1)]
		public UnconditionalSuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000219F File Offset: 0x0000039F
		[Nullable(1)]
		public string Category
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021A7 File Offset: 0x000003A7
		[Nullable(1)]
		public string CheckId
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000021AF File Offset: 0x000003AF
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000021B7 File Offset: 0x000003B7
		public string Scope { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000021C0 File Offset: 0x000003C0
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000021C8 File Offset: 0x000003C8
		public string Target { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000021D1 File Offset: 0x000003D1
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000021D9 File Offset: 0x000003D9
		public string MessageId { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000021E2 File Offset: 0x000003E2
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000021EA File Offset: 0x000003EA
		public string Justification { get; set; }
	}
}
