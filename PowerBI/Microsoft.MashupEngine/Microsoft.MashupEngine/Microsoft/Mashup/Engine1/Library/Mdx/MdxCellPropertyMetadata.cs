using System;
using System.Data.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000989 RID: 2441
	internal sealed class MdxCellPropertyMetadata
	{
		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x000EB17A File Offset: 0x000E937A
		// (set) Token: 0x06004609 RID: 17929 RVA: 0x000EB182 File Offset: 0x000E9382
		public string Name { get; set; }

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x0600460A RID: 17930 RVA: 0x000EB18B File Offset: 0x000E938B
		// (set) Token: 0x0600460B RID: 17931 RVA: 0x000EB193 File Offset: 0x000E9393
		public string Caption { get; set; }

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x0600460C RID: 17932 RVA: 0x000EB19C File Offset: 0x000E939C
		// (set) Token: 0x0600460D RID: 17933 RVA: 0x000EB1A4 File Offset: 0x000E93A4
		public OleDbType DataType { get; set; }
	}
}
