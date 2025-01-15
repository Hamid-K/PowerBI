using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000194 RID: 404
	internal sealed class OutputTableMapping
	{
		// Token: 0x06000DD2 RID: 3538 RVA: 0x00038808 File Offset: 0x00036A08
		internal OutputTableMapping()
		{
			this.m_outputTables = new List<PlanNamedTableContext>();
			this.m_outputTableMapping = new Dictionary<IDataBoundItem, int>();
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00038828 File Offset: 0x00036A28
		public void AddOutputTable(PlanNamedTableContext table, IDataBoundItem boundItem)
		{
			int count = this.m_outputTables.Count;
			this.m_outputTableMapping[boundItem] = count;
			this.m_outputTables.Add(table);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0003885A File Offset: 0x00036A5A
		public void AddOutputTable(PlanNamedTableContext table, IContextItem primaryItem, IDataBoundItem fallbackItem)
		{
			if (!this.m_outputTableMapping.ContainsKey(primaryItem))
			{
				this.AddOutputTable(table, primaryItem);
				return;
			}
			this.AddOutputTable(table, fallbackItem);
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0003887B File Offset: 0x00036A7B
		public IReadOnlyList<PlanNamedTableContext> OutputTables
		{
			get
			{
				return this.m_outputTables;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00038883 File Offset: 0x00036A83
		public IEnumerable<KeyValuePair<IDataBoundItem, int>> Mapping
		{
			get
			{
				return this.m_outputTableMapping;
			}
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003888C File Offset: 0x00036A8C
		public int IndexOf(string outputTableName)
		{
			return this.m_outputTables.FindIndex((PlanNamedTableContext p) => p.Name == outputTableName);
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x000388BD File Offset: 0x00036ABD
		public bool HasOutputTable
		{
			get
			{
				return this.m_outputTables.Count > 0;
			}
		}

		// Token: 0x040006C8 RID: 1736
		private readonly List<PlanNamedTableContext> m_outputTables;

		// Token: 0x040006C9 RID: 1737
		private readonly Dictionary<IDataBoundItem, int> m_outputTableMapping;
	}
}
