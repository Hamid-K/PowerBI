using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000042 RID: 66
	internal sealed class TableGroupCollection
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x0001272E File Offset: 0x0001092E
		internal TableGroupCollection(Table owner, TableGroup parent, TableGroup groupDef, TableGroupInstanceList groupInstances)
		{
			this.m_owner = owner;
			this.m_parent = parent;
			this.m_groupInstances = groupInstances;
			this.m_groupDef = groupDef;
		}

		// Token: 0x17000458 RID: 1112
		public TableGroup this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				TableGroup tableGroup = null;
				if (index == 0)
				{
					tableGroup = this.m_firstGroup;
				}
				else if (this.m_groups != null)
				{
					tableGroup = this.m_groups[index - 1];
				}
				if (tableGroup == null)
				{
					TableGroupInstance tableGroupInstance = null;
					if (this.m_groupInstances != null && index < this.m_groupInstances.Count)
					{
						tableGroupInstance = this.m_groupInstances[index];
					}
					else
					{
						Global.Tracer.Assert(this.m_groupInstances == null || this.m_groupInstances.Count == 0);
					}
					tableGroup = new TableGroup(this.m_owner, this.m_parent, this.m_groupDef, tableGroupInstance);
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (index == 0)
						{
							this.m_firstGroup = tableGroup;
						}
						else
						{
							if (this.m_groups == null)
							{
								this.m_groups = new TableGroup[this.m_groupInstances.Count - 1];
							}
							this.m_groups[index - 1] = tableGroup;
						}
					}
				}
				return tableGroup;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00012873 File Offset: 0x00010A73
		public int Count
		{
			get
			{
				if (this.m_groupInstances == null || this.m_groupInstances.Count == 0)
				{
					return 1;
				}
				return this.m_groupInstances.Count;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00012897 File Offset: 0x00010A97
		internal TableGroup GroupDefinition
		{
			get
			{
				return this.m_groupDef;
			}
		}

		// Token: 0x0400013E RID: 318
		private Table m_owner;

		// Token: 0x0400013F RID: 319
		private TableGroup m_groupDef;

		// Token: 0x04000140 RID: 320
		private TableGroupInstanceList m_groupInstances;

		// Token: 0x04000141 RID: 321
		private TableGroup[] m_groups;

		// Token: 0x04000142 RID: 322
		private TableGroup m_firstGroup;

		// Token: 0x04000143 RID: 323
		private TableGroup m_parent;
	}
}
