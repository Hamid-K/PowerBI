using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E4 RID: 228
	public sealed class OlapInfoHierarchy
	{
		// Token: 0x06000C85 RID: 3205 RVA: 0x0002EF90 File Offset: 0x0002D190
		internal OlapInfoHierarchy(DataTable hierarchyTable)
		{
			this.hierarchyTable = hierarchyTable;
			this.properties = null;
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002EFA6 File Offset: 0x0002D1A6
		public string Name
		{
			get
			{
				return this.hierarchyTable.TableName;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0002EFB3 File Offset: 0x0002D1B3
		public OlapInfoPropertyCollection HierarchyProperties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new OlapInfoPropertyCollection(this.hierarchyTable);
				}
				return this.properties;
			}
		}

		// Token: 0x040007FF RID: 2047
		private DataTable hierarchyTable;

		// Token: 0x04000800 RID: 2048
		private OlapInfoPropertyCollection properties;
	}
}
