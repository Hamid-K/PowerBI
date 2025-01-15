using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200041E RID: 1054
	internal sealed class SapHanaCatalog
	{
		// Token: 0x060023EB RID: 9195 RVA: 0x000653DE File Offset: 0x000635DE
		public SapHanaCatalog(IResource resource, SapHanaOdbcDataSource dataSource, string cubesTableName, string name, Version sapHanaVersion, bool useHierarchies)
		{
			this.resource = resource;
			this.name = name;
			this.cubes = SapHanaCubeCollectionBase.CreateCubeCollection(dataSource, cubesTableName, this, useHierarchies, sapHanaVersion);
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x00065407 File Offset: 0x00063607
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x0006540F File Offset: 0x0006360F
		public SapHanaCubeCollectionBase Cubes
		{
			get
			{
				return this.cubes;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x00065417 File Offset: 0x00063617
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x04000E75 RID: 3701
		private readonly IResource resource;

		// Token: 0x04000E76 RID: 3702
		private readonly string name;

		// Token: 0x04000E77 RID: 3703
		private readonly SapHanaCubeCollectionBase cubes;
	}
}
