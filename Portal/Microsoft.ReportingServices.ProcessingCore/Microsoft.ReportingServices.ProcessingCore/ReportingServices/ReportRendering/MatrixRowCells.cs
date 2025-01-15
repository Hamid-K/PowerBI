using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000038 RID: 56
	internal sealed class MatrixRowCells
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0000FD36 File Offset: 0x0000DF36
		internal MatrixRowCells(int count)
		{
			this.m_count = count;
		}

		// Token: 0x170003FF RID: 1023
		internal MatrixCell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.m_count });
				}
				if (this.m_matrixRowCells != null)
				{
					return this.m_matrixRowCells[index];
				}
				return null;
			}
			set
			{
				if (index < 0 || index >= this.m_count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.m_count });
				}
				if (this.m_matrixRowCells == null)
				{
					this.m_matrixRowCells = new MatrixCell[this.m_count];
				}
				this.m_matrixRowCells[index] = value;
			}
		}

		// Token: 0x04000105 RID: 261
		private int m_count;

		// Token: 0x04000106 RID: 262
		private MatrixCell[] m_matrixRowCells;
	}
}
