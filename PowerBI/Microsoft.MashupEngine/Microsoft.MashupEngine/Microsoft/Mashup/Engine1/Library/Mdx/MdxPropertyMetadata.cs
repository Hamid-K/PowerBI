using System;
using System.Data.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000988 RID: 2440
	internal sealed class MdxPropertyMetadata
	{
		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x000EB0F2 File Offset: 0x000E92F2
		// (set) Token: 0x060045F8 RID: 17912 RVA: 0x000EB0FA File Offset: 0x000E92FA
		public string UniqueName { get; set; }

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x000EB103 File Offset: 0x000E9303
		// (set) Token: 0x060045FA RID: 17914 RVA: 0x000EB10B File Offset: 0x000E930B
		public string DimensionUniqueName { get; set; }

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x000EB114 File Offset: 0x000E9314
		// (set) Token: 0x060045FC RID: 17916 RVA: 0x000EB11C File Offset: 0x000E931C
		public string HierarchyUniqueName { get; set; }

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x060045FD RID: 17917 RVA: 0x000EB125 File Offset: 0x000E9325
		// (set) Token: 0x060045FE RID: 17918 RVA: 0x000EB12D File Offset: 0x000E932D
		public string LevelUniqueName { get; set; }

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x060045FF RID: 17919 RVA: 0x000EB136 File Offset: 0x000E9336
		// (set) Token: 0x06004600 RID: 17920 RVA: 0x000EB13E File Offset: 0x000E933E
		public string KeyUniqueName { get; set; }

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06004601 RID: 17921 RVA: 0x000EB147 File Offset: 0x000E9347
		// (set) Token: 0x06004602 RID: 17922 RVA: 0x000EB14F File Offset: 0x000E934F
		public string Caption { get; set; }

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06004603 RID: 17923 RVA: 0x000EB158 File Offset: 0x000E9358
		// (set) Token: 0x06004604 RID: 17924 RVA: 0x000EB160 File Offset: 0x000E9360
		public OleDbType DataType { get; set; }

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06004605 RID: 17925 RVA: 0x000EB169 File Offset: 0x000E9369
		// (set) Token: 0x06004606 RID: 17926 RVA: 0x000EB171 File Offset: 0x000E9371
		public MdxPropertyKind PropertyKind { get; set; }
	}
}
