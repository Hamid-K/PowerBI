using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E0 RID: 224
	public sealed class OlapInfoAxis
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x0002EE2A File Offset: 0x0002D02A
		internal OlapInfoAxis(IDSFDataSet axisDataSet)
		{
			this.axisDataSet = axisDataSet;
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0002EE39 File Offset: 0x0002D039
		public string Name
		{
			get
			{
				return this.axisDataSet.DataSetName;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0002EE46 File Offset: 0x0002D046
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

		// Token: 0x04000804 RID: 2052
		private IDSFDataSet axisDataSet;

		// Token: 0x04000805 RID: 2053
		private OlapInfoHierarchyCollection hierarchies;
	}
}
