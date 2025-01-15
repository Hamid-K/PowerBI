using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000077 RID: 119
	internal sealed class FormulaParserContext
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000BD08 File Offset: 0x00009F08
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000BD10 File Offset: 0x00009F10
		internal string EntityName { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000BD19 File Offset: 0x00009F19
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000BD21 File Offset: 0x00009F21
		internal string ContainerName { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000BD2A File Offset: 0x00009F2A
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000BD32 File Offset: 0x00009F32
		internal string PropertyName { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000BD3B File Offset: 0x00009F3B
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000BD43 File Offset: 0x00009F43
		internal FormulaEdmReferenceKind? EdmReferenceKind { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000BD4C File Offset: 0x00009F4C
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000BD54 File Offset: 0x00009F54
		internal string FunctionName { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000BD5D File Offset: 0x00009F5D
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000BD65 File Offset: 0x00009F65
		internal PrimitiveValue Literal { get; set; }
	}
}
