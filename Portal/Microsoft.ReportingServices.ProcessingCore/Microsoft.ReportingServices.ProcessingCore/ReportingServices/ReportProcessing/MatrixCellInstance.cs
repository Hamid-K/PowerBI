using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200073D RID: 1853
	[Serializable]
	internal class MatrixCellInstance : InstanceInfoOwner
	{
		// Token: 0x060066F9 RID: 26361 RVA: 0x00192AFA File Offset: 0x00190CFA
		internal MatrixCellInstance(int rowIndex, int colIndex, Matrix matrixDef, int cellDefIndex, ReportProcessing.ProcessingContext pc, out NonComputedUniqueNames nonComputedUniqueNames)
		{
			this.m_instanceInfo = new MatrixCellInstanceInfo(rowIndex, colIndex, matrixDef, cellDefIndex, pc, this, out nonComputedUniqueNames);
		}

		// Token: 0x060066FA RID: 26362 RVA: 0x00192B17 File Offset: 0x00190D17
		internal MatrixCellInstance()
		{
		}

		// Token: 0x17002468 RID: 9320
		// (get) Token: 0x060066FB RID: 26363 RVA: 0x00192B1F File Offset: 0x00190D1F
		// (set) Token: 0x060066FC RID: 26364 RVA: 0x00192B27 File Offset: 0x00190D27
		internal ReportItemInstance Content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x060066FD RID: 26365 RVA: 0x00192B30 File Offset: 0x00190D30
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfoOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.ReportItemDef, Token.Reference, ObjectType.ReportItem),
				new MemberInfo(MemberName.Content, ObjectType.ReportItemInstance)
			});
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x00192B71 File Offset: 0x00190D71
		internal MatrixCellInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_instanceInfo is OffsetInfo)
			{
				return chunkManager.GetReader(((OffsetInfo)this.m_instanceInfo).Offset).ReadMatrixCellInstanceInfo();
			}
			return (MatrixCellInstanceInfo)this.m_instanceInfo;
		}

		// Token: 0x04003329 RID: 13097
		private ReportItemInstance m_content;
	}
}
