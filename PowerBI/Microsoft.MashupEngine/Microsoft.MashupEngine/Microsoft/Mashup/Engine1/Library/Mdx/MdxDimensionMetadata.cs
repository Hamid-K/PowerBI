using System;
using System.Data.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000982 RID: 2434
	internal sealed class MdxDimensionMetadata
	{
		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x060045B3 RID: 17843 RVA: 0x000EAEE3 File Offset: 0x000E90E3
		// (set) Token: 0x060045B4 RID: 17844 RVA: 0x000EAEEB File Offset: 0x000E90EB
		public string UniqueName { get; set; }

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x060045B5 RID: 17845 RVA: 0x000EAEF4 File Offset: 0x000E90F4
		// (set) Token: 0x060045B6 RID: 17846 RVA: 0x000EAEFC File Offset: 0x000E90FC
		public string Caption { get; set; }

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x060045B7 RID: 17847 RVA: 0x000EAF05 File Offset: 0x000E9105
		// (set) Token: 0x060045B8 RID: 17848 RVA: 0x000EAF0D File Offset: 0x000E910D
		public MdxDimensionType DimensionType { get; set; }

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x060045B9 RID: 17849 RVA: 0x000EAF16 File Offset: 0x000E9116
		// (set) Token: 0x060045BA RID: 17850 RVA: 0x000EAF1E File Offset: 0x000E911E
		public OleDbType DataType { get; set; }
	}
}
