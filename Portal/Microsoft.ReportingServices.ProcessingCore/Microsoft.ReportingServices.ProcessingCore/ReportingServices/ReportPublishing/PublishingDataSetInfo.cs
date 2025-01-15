using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000398 RID: 920
	internal sealed class PublishingDataSetInfo
	{
		// Token: 0x0600258C RID: 9612 RVA: 0x000B43A8 File Offset: 0x000B25A8
		internal PublishingDataSetInfo(string dataSetName, int dataSetDefIndex, bool isComplex, Dictionary<string, bool> parameterNames)
		{
			this.m_dataSetName = dataSetName;
			this.m_dataSetDefIndex = dataSetDefIndex;
			this.m_isComplex = isComplex;
			this.m_parameterNames = parameterNames;
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x000B43E2 File Offset: 0x000B25E2
		// (set) Token: 0x0600258E RID: 9614 RVA: 0x000B43EA File Offset: 0x000B25EA
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

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x000B43F3 File Offset: 0x000B25F3
		// (set) Token: 0x06002590 RID: 9616 RVA: 0x000B43FB File Offset: 0x000B25FB
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

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x000B4404 File Offset: 0x000B2604
		internal string DataSetName
		{
			get
			{
				return this.m_dataSetName;
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x000B440C File Offset: 0x000B260C
		internal int DataSetDefIndex
		{
			get
			{
				return this.m_dataSetDefIndex;
			}
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x000B4414 File Offset: 0x000B2614
		internal bool IsComplex
		{
			get
			{
				return this.m_isComplex;
			}
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06002594 RID: 9620 RVA: 0x000B441C File Offset: 0x000B261C
		internal Dictionary<string, bool> ParameterNames
		{
			get
			{
				return this.m_parameterNames;
			}
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x000B4424 File Offset: 0x000B2624
		// (set) Token: 0x06002596 RID: 9622 RVA: 0x000B442C File Offset: 0x000B262C
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

		// Token: 0x06002597 RID: 9623 RVA: 0x000B4438 File Offset: 0x000B2638
		internal void MergeFlagsFromDataSource(bool isComplex, Dictionary<string, bool> datasourceParameterNames)
		{
			this.m_isComplex = this.m_isComplex || isComplex;
			if (datasourceParameterNames != null)
			{
				foreach (string text in datasourceParameterNames.Keys)
				{
					this.m_parameterNames[text] = true;
				}
			}
		}

		// Token: 0x040015EA RID: 5610
		private string m_dataSetName;

		// Token: 0x040015EB RID: 5611
		private Dictionary<string, bool> m_parameterNames;

		// Token: 0x040015EC RID: 5612
		private bool m_isComplex;

		// Token: 0x040015ED RID: 5613
		private int m_dataSetDefIndex = -1;

		// Token: 0x040015EE RID: 5614
		private int m_dataSourceIndex = -1;

		// Token: 0x040015EF RID: 5615
		private int m_dataSetIndex = -1;

		// Token: 0x040015F0 RID: 5616
		private int m_calculatedFieldIndex;
	}
}
