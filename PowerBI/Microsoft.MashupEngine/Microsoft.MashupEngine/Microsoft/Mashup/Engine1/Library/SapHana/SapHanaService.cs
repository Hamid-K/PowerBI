using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200047A RID: 1146
	internal sealed class SapHanaService
	{
		// Token: 0x0600261C RID: 9756 RVA: 0x0006E4EA File Offset: 0x0006C6EA
		public SapHanaService(IResource resource, SapHanaOdbcDataSource dataSource, string cubesTableName, Version sapHanaVersion, bool version2)
		{
			this.catalogs = new SapHanaCatalogCollection(resource, dataSource, cubesTableName, sapHanaVersion, version2);
			this.version2 = version2;
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x0006E50C File Offset: 0x0006C70C
		public SapHanaCatalogCollection Catalogs
		{
			get
			{
				return this.catalogs;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x0006E514 File Offset: 0x0006C714
		public bool UseSemanticSet
		{
			get
			{
				return this.version2;
			}
		}

		// Token: 0x04000FED RID: 4077
		private readonly SapHanaCatalogCollection catalogs;

		// Token: 0x04000FEE RID: 4078
		private readonly bool version2;
	}
}
