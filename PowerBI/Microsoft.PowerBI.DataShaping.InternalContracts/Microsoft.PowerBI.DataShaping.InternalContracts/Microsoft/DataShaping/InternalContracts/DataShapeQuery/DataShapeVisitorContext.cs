using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000078 RID: 120
	internal class DataShapeVisitorContext
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000682B File Offset: 0x00004A2B
		internal DataShapeVisitorContext(DataShape dataShape)
		{
			this.m_dataShape = dataShape;
			this.ResetPrimaryLeafIndex();
			this.ResetSecondaryLeafIndex();
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00006846 File Offset: 0x00004A46
		public DataShape DataShape
		{
			get
			{
				return this.m_dataShape;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000684E File Offset: 0x00004A4E
		public void ResetPrimaryLeafIndex()
		{
			this.m_primaryLeafIndex = -1;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00006857 File Offset: 0x00004A57
		public void ResetSecondaryLeafIndex()
		{
			this.m_secondaryLeafIndex = -1;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00006860 File Offset: 0x00004A60
		public void NextPrimaryLeaf()
		{
			this.m_primaryLeafIndex++;
			this.ResetSecondaryLeafIndex();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00006876 File Offset: 0x00004A76
		public void NextSecondaryLeaf()
		{
			this.m_secondaryLeafIndex++;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00006886 File Offset: 0x00004A86
		public DataIntersection GetActiveIntersection()
		{
			return this.m_dataShape.DataRows[this.m_primaryLeafIndex].Intersections[this.m_secondaryLeafIndex];
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000068B0 File Offset: 0x00004AB0
		public bool IsValidIntersection()
		{
			if (this.m_primaryLeafIndex > -1 && this.m_dataShape.DataRows != null && this.m_primaryLeafIndex < this.m_dataShape.DataRows.Count)
			{
				DataRow dataRow = this.m_dataShape.DataRows[this.m_primaryLeafIndex];
				if (this.m_secondaryLeafIndex > -1 && dataRow.Intersections != null && this.m_secondaryLeafIndex < dataRow.Intersections.Count)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00006929 File Offset: 0x00004B29
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00006931 File Offset: 0x00004B31
		public bool InPrimaryHierarchy
		{
			get
			{
				return this.m_isInPrimaryHierarchy;
			}
			set
			{
				this.m_isInPrimaryHierarchy = value;
			}
		}

		// Token: 0x0400014C RID: 332
		private readonly DataShape m_dataShape;

		// Token: 0x0400014D RID: 333
		private bool m_isInPrimaryHierarchy;

		// Token: 0x0400014E RID: 334
		private int m_primaryLeafIndex;

		// Token: 0x0400014F RID: 335
		private int m_secondaryLeafIndex;
	}
}
