using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000036 RID: 54
	internal sealed class SizeCollection
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		internal SizeCollection(Matrix owner, bool widthsCollection)
		{
			this.m_owner = owner;
			this.m_widthsCollection = widthsCollection;
		}

		// Token: 0x170003E5 RID: 997
		public ReportSize this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				ReportSize reportSize = null;
				if (this.m_reportSizeCollection != null && this.m_reportSizeCollection[index] != null)
				{
					reportSize = this.m_reportSizeCollection[index];
				}
				if (reportSize == null)
				{
					Matrix matrix = (Matrix)this.m_owner.ReportItemDef;
					MatrixInstance matrixInstance = (MatrixInstance)this.m_owner.ReportItemInstance;
					ReportSizeCollection reportSizeCollection;
					if (this.m_widthsCollection)
					{
						reportSizeCollection = matrix.CellWidthsForRendering;
					}
					else
					{
						reportSizeCollection = matrix.CellHeightsForRendering;
					}
					Global.Tracer.Assert(reportSizeCollection != null);
					if (this.m_owner.NoRows || matrixInstance == null || matrixInstance.Cells.Count == 0)
					{
						reportSize = reportSizeCollection[index];
					}
					else if ((this.m_widthsCollection && matrix.StaticColumns == null) || (!this.m_widthsCollection && matrix.StaticRows == null))
					{
						reportSize = reportSizeCollection[0];
					}
					else
					{
						bool cacheState = this.m_owner.RenderingContext.CacheState;
						this.m_owner.RenderingContext.CacheState = true;
						MatrixCellCollection cellCollection = this.m_owner.CellCollection;
						MatrixCell matrixCell;
						if (this.m_widthsCollection)
						{
							matrixCell = cellCollection[0, index];
						}
						else
						{
							matrixCell = cellCollection[index, 0];
						}
						reportSize = reportSizeCollection[matrixCell.ColumnIndex];
						this.m_owner.RenderingContext.CacheState = cacheState;
					}
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (this.m_reportSizeCollection == null)
						{
							this.m_reportSizeCollection = new ReportSizeCollection(this.Count);
						}
						this.m_reportSizeCollection[index] = reportSize;
					}
				}
				return reportSize;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		public int Count
		{
			get
			{
				MatrixInstance matrixInstance = (MatrixInstance)this.m_owner.ReportItemInstance;
				if (this.m_owner.NoRows || matrixInstance == null || matrixInstance.Cells.Count == 0)
				{
					Matrix matrix = (Matrix)this.m_owner.ReportItemDef;
					ReportSizeCollection reportSizeCollection;
					if (this.m_widthsCollection)
					{
						reportSizeCollection = matrix.CellWidthsForRendering;
					}
					else
					{
						reportSizeCollection = matrix.CellHeightsForRendering;
					}
					Global.Tracer.Assert(reportSizeCollection != null);
					return reportSizeCollection.Count;
				}
				if (this.m_widthsCollection)
				{
					return this.m_owner.CellColumns;
				}
				return this.m_owner.CellRows;
			}
		}

		// Token: 0x040000F7 RID: 247
		private Matrix m_owner;

		// Token: 0x040000F8 RID: 248
		private bool m_widthsCollection;

		// Token: 0x040000F9 RID: 249
		private ReportSizeCollection m_reportSizeCollection;
	}
}
