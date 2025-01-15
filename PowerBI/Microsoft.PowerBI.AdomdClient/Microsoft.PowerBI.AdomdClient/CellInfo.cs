using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000078 RID: 120
	public sealed class CellInfo
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x0002505D File Offset: 0x0002325D
		internal CellInfo(MDDatasetFormatter formatter)
		{
			this.cellsTable = formatter.CellTable;
			this.properties = null;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00025078 File Offset: 0x00023278
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

		// Token: 0x04000533 RID: 1331
		private DataTable cellsTable;

		// Token: 0x04000534 RID: 1332
		private OlapInfoPropertyCollection properties;
	}
}
