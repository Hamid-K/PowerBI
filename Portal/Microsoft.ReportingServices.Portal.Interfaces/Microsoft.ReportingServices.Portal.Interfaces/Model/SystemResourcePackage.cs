using System;

namespace Model
{
	// Token: 0x0200002B RID: 43
	public class SystemResourcePackage : SystemResource
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00002955 File Offset: 0x00000B55
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000295D File Offset: 0x00000B5D
		public byte[] Content { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002966 File Offset: 0x00000B66
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000296E File Offset: 0x00000B6E
		public string PackageFileName { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00002977 File Offset: 0x00000B77
		public new CatalogItem PackageContent
		{
			get
			{
				return base.PackageContent;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000297F File Offset: 0x00000B7F
		public new SystemResourceItem[] Items
		{
			get
			{
				return base.Items;
			}
		}
	}
}
