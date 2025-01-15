using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000A RID: 10
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	internal sealed class UnconditionalSuppressMessageAttribute : Attribute
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000020FD File Offset: 0x000002FD
		[NullableContext(1)]
		public UnconditionalSuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002113 File Offset: 0x00000313
		[Nullable(1)]
		public string Category
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000211B File Offset: 0x0000031B
		[Nullable(1)]
		public string CheckId
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002123 File Offset: 0x00000323
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000212B File Offset: 0x0000032B
		public string Scope { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002134 File Offset: 0x00000334
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000213C File Offset: 0x0000033C
		public string Target { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002145 File Offset: 0x00000345
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000214D File Offset: 0x0000034D
		public string MessageId { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002156 File Offset: 0x00000356
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000215E File Offset: 0x0000035E
		public string Justification { get; set; }
	}
}
