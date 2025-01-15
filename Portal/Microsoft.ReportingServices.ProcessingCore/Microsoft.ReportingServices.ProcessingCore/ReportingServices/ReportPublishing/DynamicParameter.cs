using System;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000399 RID: 921
	internal sealed class DynamicParameter
	{
		// Token: 0x06002598 RID: 9624 RVA: 0x000B44A4 File Offset: 0x000B26A4
		internal DynamicParameter(DataSetReference validValueDataSet, DataSetReference defaultDataSet, int index, bool isComplex)
		{
			this.m_validValueDataSet = validValueDataSet;
			this.m_defaultDataSet = defaultDataSet;
			this.m_index = index;
			this.m_isComplex = isComplex;
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x000B44C9 File Offset: 0x000B26C9
		internal DataSetReference ValidValueDataSet
		{
			get
			{
				return this.m_validValueDataSet;
			}
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x0600259A RID: 9626 RVA: 0x000B44D1 File Offset: 0x000B26D1
		internal DataSetReference DefaultDataSet
		{
			get
			{
				return this.m_defaultDataSet;
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x000B44D9 File Offset: 0x000B26D9
		internal int Index
		{
			get
			{
				return this.m_index;
			}
		}

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x000B44E1 File Offset: 0x000B26E1
		internal bool IsComplex
		{
			get
			{
				return this.m_isComplex;
			}
		}

		// Token: 0x040015F1 RID: 5617
		private DataSetReference m_validValueDataSet;

		// Token: 0x040015F2 RID: 5618
		private DataSetReference m_defaultDataSet;

		// Token: 0x040015F3 RID: 5619
		private int m_index;

		// Token: 0x040015F4 RID: 5620
		private bool m_isComplex;
	}
}
