using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000770 RID: 1904
	internal sealed class DataSetReference
	{
		// Token: 0x06006968 RID: 26984 RVA: 0x00199E99 File Offset: 0x00198099
		internal DataSetReference(string dataSet, string valueAlias, string labelAlias)
		{
			this.m_dataSet = dataSet;
			this.m_valueAlias = valueAlias;
			this.m_labelAlias = labelAlias;
		}

		// Token: 0x1700253F RID: 9535
		// (get) Token: 0x06006969 RID: 26985 RVA: 0x00199EB6 File Offset: 0x001980B6
		internal string DataSet
		{
			get
			{
				return this.m_dataSet;
			}
		}

		// Token: 0x17002540 RID: 9536
		// (get) Token: 0x0600696A RID: 26986 RVA: 0x00199EBE File Offset: 0x001980BE
		internal string ValueAlias
		{
			get
			{
				return this.m_valueAlias;
			}
		}

		// Token: 0x17002541 RID: 9537
		// (get) Token: 0x0600696B RID: 26987 RVA: 0x00199EC6 File Offset: 0x001980C6
		internal string LabelAlias
		{
			get
			{
				return this.m_labelAlias;
			}
		}

		// Token: 0x04003585 RID: 13701
		private string m_dataSet;

		// Token: 0x04003586 RID: 13702
		private string m_valueAlias;

		// Token: 0x04003587 RID: 13703
		private string m_labelAlias;
	}
}
