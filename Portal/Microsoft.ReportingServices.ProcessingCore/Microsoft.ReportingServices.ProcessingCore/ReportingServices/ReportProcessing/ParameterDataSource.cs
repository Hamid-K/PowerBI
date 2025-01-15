using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000617 RID: 1559
	[Serializable]
	internal class ParameterDataSource : IParameterDataSource
	{
		// Token: 0x0600557A RID: 21882 RVA: 0x0016884A File Offset: 0x00166A4A
		internal ParameterDataSource()
		{
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x0016886E File Offset: 0x00166A6E
		internal ParameterDataSource(int dataSourceIndex, int dataSetIndex)
		{
			this.m_dataSourceIndex = dataSourceIndex;
			this.m_dataSetIndex = dataSetIndex;
		}

		// Token: 0x17001F44 RID: 8004
		// (get) Token: 0x0600557C RID: 21884 RVA: 0x001688A0 File Offset: 0x00166AA0
		// (set) Token: 0x0600557D RID: 21885 RVA: 0x001688A8 File Offset: 0x00166AA8
		public int DataSourceIndex
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

		// Token: 0x17001F45 RID: 8005
		// (get) Token: 0x0600557E RID: 21886 RVA: 0x001688B1 File Offset: 0x00166AB1
		// (set) Token: 0x0600557F RID: 21887 RVA: 0x001688B9 File Offset: 0x00166AB9
		public int DataSetIndex
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

		// Token: 0x17001F46 RID: 8006
		// (get) Token: 0x06005580 RID: 21888 RVA: 0x001688C2 File Offset: 0x00166AC2
		// (set) Token: 0x06005581 RID: 21889 RVA: 0x001688CA File Offset: 0x00166ACA
		public int ValueFieldIndex
		{
			get
			{
				return this.m_valueFieldIndex;
			}
			set
			{
				this.m_valueFieldIndex = value;
			}
		}

		// Token: 0x17001F47 RID: 8007
		// (get) Token: 0x06005582 RID: 21890 RVA: 0x001688D3 File Offset: 0x00166AD3
		// (set) Token: 0x06005583 RID: 21891 RVA: 0x001688DB File Offset: 0x00166ADB
		public int LabelFieldIndex
		{
			get
			{
				return this.m_labelFieldIndex;
			}
			set
			{
				this.m_labelFieldIndex = value;
			}
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x001688E4 File Offset: 0x00166AE4
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.DataSourceIndex, Token.Int32),
				new MemberInfo(MemberName.DataSetIndex, Token.Int32),
				new MemberInfo(MemberName.ValueFieldIndex, Token.Int32),
				new MemberInfo(MemberName.LabelFieldIndex, Token.Int32)
			});
		}

		// Token: 0x04002D65 RID: 11621
		private int m_dataSourceIndex = -1;

		// Token: 0x04002D66 RID: 11622
		private int m_dataSetIndex = -1;

		// Token: 0x04002D67 RID: 11623
		private int m_valueFieldIndex = -1;

		// Token: 0x04002D68 RID: 11624
		private int m_labelFieldIndex = -1;
	}
}
