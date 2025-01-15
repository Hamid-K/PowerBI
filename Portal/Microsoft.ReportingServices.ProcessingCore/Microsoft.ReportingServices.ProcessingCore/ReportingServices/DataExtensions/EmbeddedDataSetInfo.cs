using System;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005BA RID: 1466
	[Serializable]
	public sealed class EmbeddedDataSetInfo
	{
		// Token: 0x0600530A RID: 21258 RVA: 0x0015D667 File Offset: 0x0015B867
		public EmbeddedDataSetInfo(Guid id, string name, string datasourceName, string query)
		{
			this.m_id = id;
			this.m_dataSetName = name;
			this.m_dataSourceName = datasourceName;
			this.m_query = query;
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x0015D697 File Offset: 0x0015B897
		public EmbeddedDataSetInfo(string reportDataSetName, string datasourceName, string query)
		{
			this.m_id = Guid.NewGuid();
			this.m_dataSetName = reportDataSetName;
			this.m_dataSourceName = datasourceName;
			this.m_query = query;
		}

		// Token: 0x17001EDB RID: 7899
		// (get) Token: 0x0600530C RID: 21260 RVA: 0x0015D6CA File Offset: 0x0015B8CA
		// (set) Token: 0x0600530D RID: 21261 RVA: 0x0015D6D2 File Offset: 0x0015B8D2
		public Guid ID
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x17001EDC RID: 7900
		// (get) Token: 0x0600530E RID: 21262 RVA: 0x0015D6DB File Offset: 0x0015B8DB
		// (set) Token: 0x0600530F RID: 21263 RVA: 0x0015D6E3 File Offset: 0x0015B8E3
		public string DataSetName
		{
			get
			{
				return this.m_dataSetName;
			}
			set
			{
				this.m_dataSetName = value;
			}
		}

		// Token: 0x17001EDD RID: 7901
		// (get) Token: 0x06005310 RID: 21264 RVA: 0x0015D6EC File Offset: 0x0015B8EC
		// (set) Token: 0x06005311 RID: 21265 RVA: 0x0015D6F4 File Offset: 0x0015B8F4
		public string DataSourceName
		{
			get
			{
				return this.m_dataSourceName;
			}
			set
			{
				this.m_dataSourceName = value;
			}
		}

		// Token: 0x17001EDE RID: 7902
		// (get) Token: 0x06005312 RID: 21266 RVA: 0x0015D6FD File Offset: 0x0015B8FD
		// (set) Token: 0x06005313 RID: 21267 RVA: 0x0015D705 File Offset: 0x0015B905
		public string Query
		{
			get
			{
				return this.m_query;
			}
			set
			{
				this.m_query = value;
			}
		}

		// Token: 0x040029D9 RID: 10713
		private Guid m_id = Guid.Empty;

		// Token: 0x040029DA RID: 10714
		private string m_dataSetName;

		// Token: 0x040029DB RID: 10715
		private string m_dataSourceName;

		// Token: 0x040029DC RID: 10716
		private string m_query;
	}
}
