using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000048 RID: 72
	internal sealed class TableDetailRowCollection : TableRowCollection
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x0001340F File Offset: 0x0001160F
		internal TableDetailRowCollection(Table owner, TableRowList rowDefs, TableRowInstance[] rowInstances, TableDetailInstance detailInstance)
			: base(owner, rowDefs, rowInstances)
		{
			this.m_detailInstance = detailInstance;
		}

		// Token: 0x1700047C RID: 1148
		public override TableRow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_rowDefs.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, base.Count });
				}
				TableDetailRow tableDetailRow = null;
				if (this.m_rows != null)
				{
					tableDetailRow = (TableDetailRow)this.m_rows[index];
				}
				if (tableDetailRow == null)
				{
					TableRowInstance tableRowInstance = null;
					if (this.m_rowInstances != null && index < this.m_rowInstances.Length)
					{
						tableRowInstance = this.m_rowInstances[index];
					}
					else
					{
						Global.Tracer.Assert(this.m_rowInstances == null);
					}
					tableDetailRow = new TableDetailRow(this.m_owner, this.m_rowDefs[index], tableRowInstance, this);
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (this.m_rows == null)
						{
							TableRow[] array = new TableDetailRow[this.m_rowDefs.Count];
							this.m_rows = array;
						}
						this.m_rows[index] = tableDetailRow;
					}
				}
				return tableDetailRow;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001351B File Offset: 0x0001171B
		internal TableDetailInstance DetailInstance
		{
			get
			{
				return this.m_detailInstance;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00013523 File Offset: 0x00011723
		internal TableDetailInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_detailInstance == null)
				{
					return null;
				}
				if (this.m_detailInstanceInfo == null)
				{
					this.m_detailInstanceInfo = this.m_detailInstance.GetInstanceInfo(this.m_owner.RenderingContext.ChunkManager);
				}
				return this.m_detailInstanceInfo;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00013560 File Offset: 0x00011760
		internal bool Hidden
		{
			get
			{
				bool flag = false;
				TableDetail tableDetail = ((Table)this.m_owner.ReportItemDef).TableDetail;
				if (this.DetailInstance == null)
				{
					flag = RenderingContext.GetDefinitionHidden(tableDetail.Visibility);
				}
				else if (tableDetail.Visibility != null)
				{
					if (tableDetail.Visibility.Toggle != null)
					{
						flag = this.m_owner.RenderingContext.IsItemHidden(this.DetailInstance.UniqueName, false);
					}
					else
					{
						flag = this.InstanceInfo.StartHidden;
					}
				}
				return flag;
			}
		}

		// Token: 0x0400015B RID: 347
		private TableDetailInstance m_detailInstance;

		// Token: 0x0400015C RID: 348
		private TableDetailInstanceInfo m_detailInstanceInfo;
	}
}
