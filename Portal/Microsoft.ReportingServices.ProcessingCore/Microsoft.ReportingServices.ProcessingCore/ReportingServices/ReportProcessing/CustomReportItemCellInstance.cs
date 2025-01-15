using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000769 RID: 1897
	[Serializable]
	internal sealed class CustomReportItemCellInstance
	{
		// Token: 0x06006947 RID: 26951 RVA: 0x00199B84 File Offset: 0x00197D84
		internal CustomReportItemCellInstance(int rowIndex, int colIndex, CustomReportItem definition, ReportProcessing.ProcessingContext pc)
		{
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = colIndex;
			Global.Tracer.Assert(definition != null && definition.DataRowCells != null && rowIndex < definition.DataRowCells.Count && colIndex < definition.DataRowCells[rowIndex].Count && 0 < definition.DataRowCells[rowIndex][colIndex].Count);
			DataValueCRIList dataValueCRIList = definition.DataRowCells[rowIndex][colIndex];
			Global.Tracer.Assert(dataValueCRIList != null);
			this.m_dataValueInstances = dataValueCRIList.EvaluateExpressions(definition.ObjectType, definition.Name, null, dataValueCRIList.RDLRowIndex, dataValueCRIList.RDLColumnIndex, pc);
			Global.Tracer.Assert(this.m_dataValueInstances != null);
		}

		// Token: 0x06006948 RID: 26952 RVA: 0x00199C54 File Offset: 0x00197E54
		internal CustomReportItemCellInstance()
		{
		}

		// Token: 0x17002530 RID: 9520
		// (get) Token: 0x06006949 RID: 26953 RVA: 0x00199C5C File Offset: 0x00197E5C
		// (set) Token: 0x0600694A RID: 26954 RVA: 0x00199C64 File Offset: 0x00197E64
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

		// Token: 0x17002531 RID: 9521
		// (get) Token: 0x0600694B RID: 26955 RVA: 0x00199C6D File Offset: 0x00197E6D
		// (set) Token: 0x0600694C RID: 26956 RVA: 0x00199C75 File Offset: 0x00197E75
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

		// Token: 0x17002532 RID: 9522
		// (get) Token: 0x0600694D RID: 26957 RVA: 0x00199C7E File Offset: 0x00197E7E
		// (set) Token: 0x0600694E RID: 26958 RVA: 0x00199C86 File Offset: 0x00197E86
		internal DataValueInstanceList DataValueInstances
		{
			get
			{
				return this.m_dataValueInstances;
			}
			set
			{
				this.m_dataValueInstances = value;
			}
		}

		// Token: 0x0600694F RID: 26959 RVA: 0x00199C90 File Offset: 0x00197E90
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.RowIndex, Token.Int32),
				new MemberInfo(MemberName.ColumnIndex, Token.Int32),
				new MemberInfo(MemberName.DataValueInstances, ObjectType.DataValueInstanceList)
			});
		}

		// Token: 0x040033DC RID: 13276
		private int m_rowIndex;

		// Token: 0x040033DD RID: 13277
		private int m_columnIndex;

		// Token: 0x040033DE RID: 13278
		private DataValueInstanceList m_dataValueInstances;
	}
}
