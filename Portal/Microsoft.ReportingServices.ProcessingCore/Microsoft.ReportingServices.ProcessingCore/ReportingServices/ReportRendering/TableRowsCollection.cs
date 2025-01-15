using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000045 RID: 69
	internal sealed class TableRowsCollection
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x00012FBC File Offset: 0x000111BC
		internal TableRowsCollection(Table owner, TableDetail detailDef, TableDetailInstanceList detailInstances)
		{
			this.m_owner = owner;
			this.m_detailInstances = detailInstances;
			this.m_detailDef = detailDef;
		}

		// Token: 0x1700046C RID: 1132
		public TableDetailRowCollection this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				TableDetailRowCollection tableDetailRowCollection = null;
				if (index == 0)
				{
					tableDetailRowCollection = this.m_firstRows;
				}
				else if (this.m_rows != null)
				{
					tableDetailRowCollection = this.m_rows[index - 1];
				}
				if (tableDetailRowCollection == null)
				{
					TableRowInstance[] array = null;
					TableDetailInstance tableDetailInstance = null;
					if (this.m_detailInstances != null && index < this.m_detailInstances.Count)
					{
						tableDetailInstance = this.m_detailInstances[index];
						array = tableDetailInstance.DetailRowInstances;
					}
					else
					{
						Global.Tracer.Assert(this.m_detailInstances == null || this.m_detailInstances.Count == 0);
					}
					tableDetailRowCollection = new TableDetailRowCollection(this.m_owner, this.m_detailDef.DetailRows, array, tableDetailInstance);
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (index == 0)
						{
							this.m_firstRows = tableDetailRowCollection;
						}
						else
						{
							if (this.m_rows == null)
							{
								this.m_rows = new TableDetailRowCollection[this.m_detailInstances.Count - 1];
							}
							this.m_rows[index - 1] = tableDetailRowCollection;
						}
					}
				}
				return tableDetailRowCollection;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00013104 File Offset: 0x00011304
		public int Count
		{
			get
			{
				if (this.m_detailInstances == null || this.m_detailInstances.Count == 0)
				{
					return 1;
				}
				return this.m_detailInstances.Count;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00013128 File Offset: 0x00011328
		internal TableDetail DetailDefinition
		{
			get
			{
				return this.m_detailDef;
			}
		}

		// Token: 0x04000150 RID: 336
		private Table m_owner;

		// Token: 0x04000151 RID: 337
		private TableDetail m_detailDef;

		// Token: 0x04000152 RID: 338
		private TableDetailInstanceList m_detailInstances;

		// Token: 0x04000153 RID: 339
		private TableDetailRowCollection[] m_rows;

		// Token: 0x04000154 RID: 340
		private TableDetailRowCollection m_firstRows;
	}
}
