using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200015A RID: 346
	internal sealed class GeneratedTableDeclaration
	{
		// Token: 0x06000CAA RID: 3242 RVA: 0x00034450 File Offset: 0x00032650
		internal GeneratedTableDeclaration(string planName, string queryName, GeneratedTable table, GeneratedTable originalTable)
		{
			this.m_planName = planName;
			this.m_queryName = queryName;
			this.m_table = table;
			this.m_originalTable = originalTable;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00034475 File Offset: 0x00032675
		public string PlanName
		{
			get
			{
				return this.m_planName;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0003447D File Offset: 0x0003267D
		public string QueryName
		{
			get
			{
				return this.m_queryName;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00034485 File Offset: 0x00032685
		public GeneratedTable Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0003448D File Offset: 0x0003268D
		public GeneratedTable OriginalTable
		{
			get
			{
				return this.m_originalTable;
			}
		}

		// Token: 0x0400064E RID: 1614
		private readonly string m_planName;

		// Token: 0x0400064F RID: 1615
		private readonly string m_queryName;

		// Token: 0x04000650 RID: 1616
		private readonly GeneratedTable m_table;

		// Token: 0x04000651 RID: 1617
		private readonly GeneratedTable m_originalTable;
	}
}
