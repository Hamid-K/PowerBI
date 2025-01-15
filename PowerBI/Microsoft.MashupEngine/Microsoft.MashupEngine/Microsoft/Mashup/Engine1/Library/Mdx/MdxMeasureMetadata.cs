using System;
using System.Data.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000983 RID: 2435
	internal sealed class MdxMeasureMetadata
	{
		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x060045BC RID: 17852 RVA: 0x000EAF27 File Offset: 0x000E9127
		// (set) Token: 0x060045BD RID: 17853 RVA: 0x000EAF2F File Offset: 0x000E912F
		public string UniqueName { get; set; }

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x000EAF38 File Offset: 0x000E9138
		// (set) Token: 0x060045BF RID: 17855 RVA: 0x000EAF40 File Offset: 0x000E9140
		public string Caption { get; set; }

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x000EAF49 File Offset: 0x000E9149
		// (set) Token: 0x060045C1 RID: 17857 RVA: 0x000EAF51 File Offset: 0x000E9151
		public OleDbType DataType { get; set; }

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x000EAF5A File Offset: 0x000E915A
		// (set) Token: 0x060045C3 RID: 17859 RVA: 0x000EAF62 File Offset: 0x000E9162
		public string MeasureGroupName { get; set; }

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x000EAF6B File Offset: 0x000E916B
		// (set) Token: 0x060045C5 RID: 17861 RVA: 0x000EAF73 File Offset: 0x000E9173
		public string DisplayFolder { get; set; }
	}
}
