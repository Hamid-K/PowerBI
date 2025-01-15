using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000044 RID: 68
	internal abstract class TableRowCollection
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x00012E9A File Offset: 0x0001109A
		internal TableRowCollection(Table owner, TableRowList rowDefs, TableRowInstance[] rowInstances)
		{
			this.m_owner = owner;
			this.m_rowInstances = rowInstances;
			this.m_rowDefs = rowDefs;
		}

		// Token: 0x17000469 RID: 1129
		public virtual TableRow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_rowDefs.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				TableRow tableRow = null;
				if (this.m_rows != null)
				{
					tableRow = this.m_rows[index];
				}
				if (tableRow == null)
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
					tableRow = new TableRow(this.m_owner, this.m_rowDefs[index], tableRowInstance);
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (this.m_rows == null)
						{
							this.m_rows = new TableRow[this.m_rowDefs.Count];
						}
						this.m_rows[index] = tableRow;
					}
				}
				return tableRow;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00012FA7 File Offset: 0x000111A7
		public int Count
		{
			get
			{
				return this.m_rowDefs.Count;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00012FB4 File Offset: 0x000111B4
		internal TableRowList DetailRowDefinitions
		{
			get
			{
				return this.m_rowDefs;
			}
		}

		// Token: 0x0400014C RID: 332
		internal Table m_owner;

		// Token: 0x0400014D RID: 333
		internal TableRowList m_rowDefs;

		// Token: 0x0400014E RID: 334
		internal TableRowInstance[] m_rowInstances;

		// Token: 0x0400014F RID: 335
		internal TableRow[] m_rows;
	}
}
