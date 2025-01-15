using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000078 RID: 120
	public sealed class CellInfo
	{
		// Token: 0x06000796 RID: 1942 RVA: 0x0002538D File Offset: 0x0002358D
		internal CellInfo(MDDatasetFormatter formatter)
		{
			this.cellsTable = formatter.CellTable;
			this.properties = null;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x000253A8 File Offset: 0x000235A8
		public OlapInfoPropertyCollection CellProperties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new OlapInfoPropertyCollection(this.cellsTable);
				}
				return this.properties;
			}
		}

		// Token: 0x04000540 RID: 1344
		private DataTable cellsTable;

		// Token: 0x04000541 RID: 1345
		private OlapInfoPropertyCollection properties;
	}
}
