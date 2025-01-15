using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000039 RID: 57
	internal sealed class MatrixCell
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x0000FE18 File Offset: 0x0000E018
		internal MatrixCell(Matrix owner, int rowIndex, int columnIndex)
		{
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = columnIndex;
			if (!owner.NoRows)
			{
				MatrixCellInstancesList cells = ((MatrixInstance)owner.ReportItemInstance).Cells;
				this.m_matrixCellInstance = cells[rowIndex][columnIndex];
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000FE6C File Offset: 0x0000E06C
		public ReportItem ReportItem
		{
			get
			{
				ReportItem reportItem = this.m_cellReportItem;
				if (this.m_cellReportItem == null)
				{
					Matrix matrix = (Matrix)this.m_owner.ReportItemDef;
					ReportItemInstance reportItemInstance = null;
					NonComputedUniqueNames nonComputedUniqueNames = null;
					ReportItem reportItem2 = null;
					if (this.m_owner.NoRows)
					{
						reportItem2 = matrix.GetCellReportItem(this.m_rowIndex, this.m_columnIndex);
					}
					else
					{
						reportItem2 = matrix.GetCellReportItem(this.InstanceInfo.RowIndex, this.InstanceInfo.ColumnIndex);
						reportItemInstance = this.m_matrixCellInstance.Content;
						nonComputedUniqueNames = this.InstanceInfo.ContentUniqueNames;
					}
					if (reportItem2 != null)
					{
						try
						{
							MatrixSubtotalCellInstance matrixSubtotalCellInstance = this.m_matrixCellInstance as MatrixSubtotalCellInstance;
							if (matrixSubtotalCellInstance != null)
							{
								Global.Tracer.Assert(matrixSubtotalCellInstance.SubtotalHeadingInstance.MatrixHeadingDef.Subtotal.StyleClass != null);
								this.m_owner.RenderingContext.HeadingInstance = matrixSubtotalCellInstance.SubtotalHeadingInstance;
							}
						}
						catch (Exception ex)
						{
							Global.Tracer.Trace(TraceLevel.Error, "Could not restore matrix subtotal heading instance from intermediate format: {0}", new object[] { ex.StackTrace });
							this.m_owner.RenderingContext.HeadingInstance = null;
						}
						reportItem = ReportItem.CreateItem(0, reportItem2, reportItemInstance, this.m_owner.RenderingContext, nonComputedUniqueNames);
						this.m_owner.RenderingContext.HeadingInstance = null;
					}
					if (this.m_owner.RenderingContext.CacheState)
					{
						this.m_cellReportItem = reportItem;
					}
				}
				return reportItem;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		public string CellLabel
		{
			get
			{
				Matrix matrix = (Matrix)this.m_owner.ReportItemDef;
				if (matrix.OwcCellNames != null)
				{
					int num = this.IndexCellDefinition(matrix);
					return matrix.OwcCellNames[num];
				}
				return null;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00010014 File Offset: 0x0000E214
		public string ID
		{
			get
			{
				Matrix matrix = (Matrix)this.m_owner.ReportItemDef;
				int num = this.IndexCellDefinition(matrix);
				if (matrix.CellIDsForRendering == null)
				{
					matrix.CellIDsForRendering = new string[matrix.CellIDs.Count];
				}
				if (matrix.CellIDsForRendering[num] == null)
				{
					matrix.CellIDsForRendering[num] = matrix.CellIDs[num].ToString(CultureInfo.InvariantCulture);
				}
				return matrix.CellIDsForRendering[num];
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001008C File Offset: 0x0000E28C
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x000100E0 File Offset: 0x0000E2E0
		public object SharedRenderingInfo
		{
			get
			{
				Matrix matrix = (Matrix)this.m_owner.ReportItemDef;
				int num = this.IndexCellDefinition(matrix);
				int num2 = matrix.CellIDs[num];
				return this.m_owner.RenderingContext.RenderingInfoManager.SharedRenderingInfo[num2];
			}
			set
			{
				Matrix matrix = (Matrix)this.m_owner.ReportItemDef;
				int num = this.IndexCellDefinition(matrix);
				int num2 = matrix.CellIDs[num];
				this.m_owner.RenderingContext.RenderingInfoManager.SharedRenderingInfo[num2] = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00010134 File Offset: 0x0000E334
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return ((Matrix)this.m_owner.ReportItemDef).CellDataElementOutput;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0001014B File Offset: 0x0000E34B
		public string DataElementName
		{
			get
			{
				return ((Matrix)this.m_owner.ReportItemDef).CellDataElementName;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00010162 File Offset: 0x0000E362
		internal int ColumnIndex
		{
			get
			{
				if (this.m_matrixCellInstance == null)
				{
					return 0;
				}
				return this.InstanceInfo.ColumnIndex;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00010179 File Offset: 0x0000E379
		internal int RowIndex
		{
			get
			{
				if (this.m_matrixCellInstance == null)
				{
					return 0;
				}
				return this.InstanceInfo.RowIndex;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00010190 File Offset: 0x0000E390
		private MatrixCellInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_matrixCellInstance == null)
				{
					return null;
				}
				if (this.m_matrixCellInstanceInfo == null)
				{
					this.m_matrixCellInstanceInfo = this.m_matrixCellInstance.GetInstanceInfo(this.m_owner.RenderingContext.ChunkManager);
				}
				return this.m_matrixCellInstanceInfo;
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000101CC File Offset: 0x0000E3CC
		private int IndexCellDefinition(Matrix matrixDef)
		{
			int num;
			if (this.m_owner.NoRows)
			{
				num = this.m_rowIndex * matrixDef.MatrixColumns.Count + this.m_columnIndex;
			}
			else
			{
				num = this.InstanceInfo.RowIndex * matrixDef.MatrixColumns.Count + this.InstanceInfo.ColumnIndex;
			}
			return num;
		}

		// Token: 0x04000107 RID: 263
		private Matrix m_owner;

		// Token: 0x04000108 RID: 264
		private int m_rowIndex;

		// Token: 0x04000109 RID: 265
		private int m_columnIndex;

		// Token: 0x0400010A RID: 266
		private MatrixCellInstance m_matrixCellInstance;

		// Token: 0x0400010B RID: 267
		private ReportItem m_cellReportItem;

		// Token: 0x0400010C RID: 268
		private MatrixCellInstanceInfo m_matrixCellInstanceInfo;
	}
}
