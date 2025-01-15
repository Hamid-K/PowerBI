using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200076E RID: 1902
	internal sealed class YukonDataSetInfo
	{
		// Token: 0x06006958 RID: 26968 RVA: 0x00199DA7 File Offset: 0x00197FA7
		internal YukonDataSetInfo(int index, bool isComplex, StringList parameterNames)
		{
			this.m_dataSetDefIndex = index;
			this.m_isComplex = isComplex;
			this.m_parameterNames = parameterNames;
		}

		// Token: 0x17002535 RID: 9525
		// (get) Token: 0x06006959 RID: 26969 RVA: 0x00199DD9 File Offset: 0x00197FD9
		// (set) Token: 0x0600695A RID: 26970 RVA: 0x00199DE1 File Offset: 0x00197FE1
		internal int DataSourceIndex
		{
			get
			{
				return this.m_dataSourceIndex;
			}
			set
			{
				this.m_dataSourceIndex = value;
			}
		}

		// Token: 0x17002536 RID: 9526
		// (get) Token: 0x0600695B RID: 26971 RVA: 0x00199DEA File Offset: 0x00197FEA
		// (set) Token: 0x0600695C RID: 26972 RVA: 0x00199DF2 File Offset: 0x00197FF2
		internal int DataSetIndex
		{
			get
			{
				return this.m_dataSetIndex;
			}
			set
			{
				this.m_dataSetIndex = value;
			}
		}

		// Token: 0x17002537 RID: 9527
		// (get) Token: 0x0600695D RID: 26973 RVA: 0x00199DFB File Offset: 0x00197FFB
		internal int DataSetDefIndex
		{
			get
			{
				return this.m_dataSetDefIndex;
			}
		}

		// Token: 0x17002538 RID: 9528
		// (get) Token: 0x0600695E RID: 26974 RVA: 0x00199E03 File Offset: 0x00198003
		internal bool IsComplex
		{
			get
			{
				return this.m_isComplex;
			}
		}

		// Token: 0x17002539 RID: 9529
		// (get) Token: 0x0600695F RID: 26975 RVA: 0x00199E0B File Offset: 0x0019800B
		internal StringList ParameterNames
		{
			get
			{
				return this.m_parameterNames;
			}
		}

		// Token: 0x1700253A RID: 9530
		// (get) Token: 0x06006960 RID: 26976 RVA: 0x00199E13 File Offset: 0x00198013
		// (set) Token: 0x06006961 RID: 26977 RVA: 0x00199E1B File Offset: 0x0019801B
		internal int CalculatedFieldIndex
		{
			get
			{
				return this.m_calculatedFieldIndex;
			}
			set
			{
				this.m_calculatedFieldIndex = value;
			}
		}

		// Token: 0x06006962 RID: 26978 RVA: 0x00199E24 File Offset: 0x00198024
		internal void MergeFlagsFromDataSource(bool isComplex, StringList datasourceParameterNames)
		{
			this.m_isComplex = this.m_isComplex || isComplex;
			if (this.m_parameterNames == null)
			{
				this.m_parameterNames = datasourceParameterNames;
				return;
			}
			if (datasourceParameterNames != null)
			{
				this.m_parameterNames.InsertRange(0, datasourceParameterNames);
			}
		}

		// Token: 0x0400357B RID: 13691
		private StringList m_parameterNames;

		// Token: 0x0400357C RID: 13692
		private bool m_isComplex;

		// Token: 0x0400357D RID: 13693
		private int m_dataSetDefIndex = -1;

		// Token: 0x0400357E RID: 13694
		private int m_dataSourceIndex = -1;

		// Token: 0x0400357F RID: 13695
		private int m_dataSetIndex = -1;

		// Token: 0x04003580 RID: 13696
		private int m_calculatedFieldIndex;
	}
}
