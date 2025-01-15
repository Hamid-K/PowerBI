using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004B1 RID: 1201
	internal sealed class ParentDataSetContainer
	{
		// Token: 0x06003C09 RID: 15369 RVA: 0x00103897 File Offset: 0x00101A97
		public ParentDataSetContainer(DataSet parentDataSet)
		{
			this.m_rowParentDataSet = parentDataSet;
			this.m_columnParentDataSet = null;
			this.m_count = 1;
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x001038B4 File Offset: 0x00101AB4
		public ParentDataSetContainer(DataSet rowParentDataSet, DataSet columnParentDataSet)
		{
			this.m_rowParentDataSet = rowParentDataSet;
			this.m_columnParentDataSet = columnParentDataSet;
			this.m_count = 2;
		}

		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x06003C0B RID: 15371 RVA: 0x001038D1 File Offset: 0x00101AD1
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x170019C0 RID: 6592
		// (get) Token: 0x06003C0C RID: 15372 RVA: 0x001038D9 File Offset: 0x00101AD9
		public DataSet ParentDataSet
		{
			get
			{
				return this.m_rowParentDataSet;
			}
		}

		// Token: 0x170019C1 RID: 6593
		// (get) Token: 0x06003C0D RID: 15373 RVA: 0x001038E1 File Offset: 0x00101AE1
		public DataSet RowParentDataSet
		{
			get
			{
				return this.m_rowParentDataSet;
			}
		}

		// Token: 0x170019C2 RID: 6594
		// (get) Token: 0x06003C0E RID: 15374 RVA: 0x001038E9 File Offset: 0x00101AE9
		public DataSet ColumnParentDataSet
		{
			get
			{
				return this.m_columnParentDataSet;
			}
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x001038F1 File Offset: 0x00101AF1
		public bool AreAllSameDataSet()
		{
			return this.Count == 1 || DataSet.AreEqualById(this.m_rowParentDataSet, this.m_columnParentDataSet);
		}

		// Token: 0x04001C4D RID: 7245
		private readonly DataSet m_rowParentDataSet;

		// Token: 0x04001C4E RID: 7246
		private readonly DataSet m_columnParentDataSet;

		// Token: 0x04001C4F RID: 7247
		private readonly int m_count;
	}
}
