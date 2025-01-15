using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200076F RID: 1903
	internal sealed class DynamicParameter
	{
		// Token: 0x06006963 RID: 26979 RVA: 0x00199E54 File Offset: 0x00198054
		internal DynamicParameter(DataSetReference validValueDataSet, DataSetReference defaultDataSet, int index, bool isComplex)
		{
			this.m_validValueDataSet = validValueDataSet;
			this.m_defaultDataSet = defaultDataSet;
			this.m_index = index;
			this.m_isComplex = isComplex;
		}

		// Token: 0x1700253B RID: 9531
		// (get) Token: 0x06006964 RID: 26980 RVA: 0x00199E79 File Offset: 0x00198079
		internal DataSetReference ValidValueDataSet
		{
			get
			{
				return this.m_validValueDataSet;
			}
		}

		// Token: 0x1700253C RID: 9532
		// (get) Token: 0x06006965 RID: 26981 RVA: 0x00199E81 File Offset: 0x00198081
		internal DataSetReference DefaultDataSet
		{
			get
			{
				return this.m_defaultDataSet;
			}
		}

		// Token: 0x1700253D RID: 9533
		// (get) Token: 0x06006966 RID: 26982 RVA: 0x00199E89 File Offset: 0x00198089
		internal int Index
		{
			get
			{
				return this.m_index;
			}
		}

		// Token: 0x1700253E RID: 9534
		// (get) Token: 0x06006967 RID: 26983 RVA: 0x00199E91 File Offset: 0x00198091
		internal bool IsComplex
		{
			get
			{
				return this.m_isComplex;
			}
		}

		// Token: 0x04003581 RID: 13697
		private DataSetReference m_validValueDataSet;

		// Token: 0x04003582 RID: 13698
		private DataSetReference m_defaultDataSet;

		// Token: 0x04003583 RID: 13699
		private int m_index;

		// Token: 0x04003584 RID: 13700
		private bool m_isComplex;
	}
}
