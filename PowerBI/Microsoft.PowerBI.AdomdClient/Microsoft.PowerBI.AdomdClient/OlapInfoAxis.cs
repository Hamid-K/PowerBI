using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E0 RID: 224
	public sealed class OlapInfoAxis
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x0002EAFA File Offset: 0x0002CCFA
		internal OlapInfoAxis(IDSFDataSet axisDataSet)
		{
			this.axisDataSet = axisDataSet;
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0002EB09 File Offset: 0x0002CD09
		public string Name
		{
			get
			{
				return this.axisDataSet.DataSetName;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0002EB16 File Offset: 0x0002CD16
		public OlapInfoHierarchyCollection Hierarchies
		{
			get
			{
				if (this.hierarchies == null)
				{
					this.hierarchies = new OlapInfoHierarchyCollection(this.axisDataSet);
				}
				return this.hierarchies;
			}
		}

		// Token: 0x040007F7 RID: 2039
		private IDSFDataSet axisDataSet;

		// Token: 0x040007F8 RID: 2040
		private OlapInfoHierarchyCollection hierarchies;
	}
}
