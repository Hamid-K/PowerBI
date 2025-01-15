using System;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x0200039A RID: 922
	internal sealed class DataSetReference
	{
		// Token: 0x0600259D RID: 9629 RVA: 0x000B44E9 File Offset: 0x000B26E9
		internal DataSetReference(string dataSet, string valueAlias, string labelAlias)
		{
			this.m_dataSet = dataSet;
			this.m_valueAlias = valueAlias;
			this.m_labelAlias = labelAlias;
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x000B4506 File Offset: 0x000B2706
		internal string DataSet
		{
			get
			{
				return this.m_dataSet;
			}
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x000B450E File Offset: 0x000B270E
		internal string ValueAlias
		{
			get
			{
				return this.m_valueAlias;
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x000B4516 File Offset: 0x000B2716
		internal string LabelAlias
		{
			get
			{
				return this.m_labelAlias;
			}
		}

		// Token: 0x040015F5 RID: 5621
		private string m_dataSet;

		// Token: 0x040015F6 RID: 5622
		private string m_valueAlias;

		// Token: 0x040015F7 RID: 5623
		private string m_labelAlias;
	}
}
