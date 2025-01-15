using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200003E RID: 62
	internal sealed class TableColumnCollection
	{
		// Token: 0x0600055F RID: 1375 RVA: 0x00012167 File Offset: 0x00010367
		internal TableColumnCollection(Table owner, TableColumnList columnDefs)
		{
			this.m_owner = owner;
			this.m_columnDefs = columnDefs;
		}

		// Token: 0x17000441 RID: 1089
		public TableColumn this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_columnDefs.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				TableColumn tableColumn;
				if (this.m_columns == null || this.m_columns[index] == null)
				{
					tableColumn = new TableColumn(this.m_owner, this.m_columnDefs[index], index);
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (this.m_columns == null)
						{
							this.m_columns = new TableColumn[this.m_columnDefs.Count];
						}
						this.m_columns[index] = tableColumn;
					}
				}
				else
				{
					tableColumn = this.m_columns[index];
				}
				return tableColumn;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00012242 File Offset: 0x00010442
		public int Count
		{
			get
			{
				return this.m_columnDefs.Count;
			}
		}

		// Token: 0x0400012F RID: 303
		private Table m_owner;

		// Token: 0x04000130 RID: 304
		private TableColumnList m_columnDefs;

		// Token: 0x04000131 RID: 305
		private TableColumn[] m_columns;
	}
}
