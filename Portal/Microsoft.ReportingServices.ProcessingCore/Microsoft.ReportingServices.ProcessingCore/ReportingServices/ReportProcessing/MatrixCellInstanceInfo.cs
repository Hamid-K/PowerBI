using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200073F RID: 1855
	[Serializable]
	internal sealed class MatrixCellInstanceInfo : InstanceInfo
	{
		// Token: 0x06006704 RID: 26372 RVA: 0x00192C54 File Offset: 0x00190E54
		internal MatrixCellInstanceInfo(int rowIndex, int colIndex, Matrix matrixDef, int cellDefIndex, ReportProcessing.ProcessingContext pc, MatrixCellInstance owner, out NonComputedUniqueNames nonComputedUniqueNames)
		{
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = colIndex;
			if (0 < matrixDef.CellReportItems.Count && !matrixDef.CellReportItems.IsReportItemComputed(cellDefIndex))
			{
				this.m_contentUniqueNames = NonComputedUniqueNames.CreateNonComputedUniqueNames(pc, matrixDef.CellReportItems[cellDefIndex]);
			}
			nonComputedUniqueNames = this.m_contentUniqueNames;
			Global.Tracer.Assert(cellDefIndex < matrixDef.FirstCellInstances.Count);
			if (matrixDef.FirstCellInstances[cellDefIndex])
			{
				pc.ChunkManager.AddInstanceToFirstPage(this, owner, pc.InPageSection);
				matrixDef.FirstCellInstances[cellDefIndex] = false;
				return;
			}
			pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x00192D18 File Offset: 0x00190F18
		internal MatrixCellInstanceInfo()
		{
		}

		// Token: 0x1700246A RID: 9322
		// (get) Token: 0x06006706 RID: 26374 RVA: 0x00192D20 File Offset: 0x00190F20
		// (set) Token: 0x06006707 RID: 26375 RVA: 0x00192D28 File Offset: 0x00190F28
		internal NonComputedUniqueNames ContentUniqueNames
		{
			get
			{
				return this.m_contentUniqueNames;
			}
			set
			{
				this.m_contentUniqueNames = value;
			}
		}

		// Token: 0x1700246B RID: 9323
		// (get) Token: 0x06006708 RID: 26376 RVA: 0x00192D31 File Offset: 0x00190F31
		// (set) Token: 0x06006709 RID: 26377 RVA: 0x00192D39 File Offset: 0x00190F39
		internal int RowIndex
		{
			get
			{
				return this.m_rowIndex;
			}
			set
			{
				this.m_rowIndex = value;
			}
		}

		// Token: 0x1700246C RID: 9324
		// (get) Token: 0x0600670A RID: 26378 RVA: 0x00192D42 File Offset: 0x00190F42
		// (set) Token: 0x0600670B RID: 26379 RVA: 0x00192D4A File Offset: 0x00190F4A
		internal int ColumnIndex
		{
			get
			{
				return this.m_columnIndex;
			}
			set
			{
				this.m_columnIndex = value;
			}
		}

		// Token: 0x0600670C RID: 26380 RVA: 0x00192D54 File Offset: 0x00190F54
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.ContentUniqueNames, ObjectType.NonComputedUniqueNames),
				new MemberInfo(MemberName.RowIndex, Token.Int32),
				new MemberInfo(MemberName.ColumnIndex, Token.Int32)
			});
		}

		// Token: 0x0400332B RID: 13099
		private NonComputedUniqueNames m_contentUniqueNames;

		// Token: 0x0400332C RID: 13100
		private int m_rowIndex;

		// Token: 0x0400332D RID: 13101
		private int m_columnIndex;
	}
}
