using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E4 RID: 228
	public sealed class OlapInfoHierarchy
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x0002F2C0 File Offset: 0x0002D4C0
		internal OlapInfoHierarchy(DataTable hierarchyTable)
		{
			this.hierarchyTable = hierarchyTable;
			this.properties = null;
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0002F2D6 File Offset: 0x0002D4D6
		public string Name
		{
			get
			{
				return this.hierarchyTable.TableName;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0002F2E3 File Offset: 0x0002D4E3
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

		// Token: 0x0400080C RID: 2060
		private DataTable hierarchyTable;

		// Token: 0x0400080D RID: 2061
		private OlapInfoPropertyCollection properties;
	}
}
