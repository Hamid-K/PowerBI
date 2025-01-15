using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003EC RID: 1004
	public class ListItem : RectangleItem
	{
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x0007EED1 File Offset: 0x0007D0D1
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x0007EED9 File Offset: 0x0007D0D9
		public Filters Filters
		{
			get
			{
				return this.m_filters;
			}
			set
			{
				this.m_filters = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x0007EEE2 File Offset: 0x0007D0E2
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x0007EEEA File Offset: 0x0007D0EA
		public Grouping Grouping
		{
			get
			{
				return this.m_grouping;
			}
			set
			{
				this.m_grouping = value;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x0007EEF3 File Offset: 0x0007D0F3
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x0007EEFB File Offset: 0x0007D0FB
		public Sorting Sorting
		{
			get
			{
				return this.m_sorting;
			}
			set
			{
				this.m_sorting = value;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x0007EF04 File Offset: 0x0007D104
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x0007EF0C File Offset: 0x0007D10C
		[DefaultValue(false)]
		public bool KeepTogether
		{
			get
			{
				return this.m_keepTogether;
			}
			set
			{
				this.m_keepTogether = value;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x0007EF15 File Offset: 0x0007D115
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x0007EF1D File Offset: 0x0007D11D
		[DefaultValue("")]
		public string NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x0007EF26 File Offset: 0x0007D126
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x0007EF2E File Offset: 0x0007D12E
		[DefaultValue(false)]
		public bool FillPage
		{
			get
			{
				return this.m_fillPage;
			}
			set
			{
				this.m_fillPage = value;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x0007EF37 File Offset: 0x0007D137
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x0007EF3F File Offset: 0x0007D13F
		[DefaultValue("")]
		public string DataSetName
		{
			get
			{
				return this.m_dataSetName;
			}
			set
			{
				this.m_dataSetName = value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x0007EF48 File Offset: 0x0007D148
		// (set) Token: 0x06001FEE RID: 8174 RVA: 0x0007EF50 File Offset: 0x0007D150
		[DefaultValue("")]
		public string DataInstanceName
		{
			get
			{
				return this.m_dataInstanceName;
			}
			set
			{
				this.m_dataInstanceName = value;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x0007EF59 File Offset: 0x0007D159
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x0007EF61 File Offset: 0x0007D161
		[DefaultValue(GroupingDataElementOutputs.Output)]
		public GroupingDataElementOutputs DataInstanceElementOutput
		{
			get
			{
				return this.m_dataInstanceOutput;
			}
			set
			{
				this.m_dataInstanceOutput = value;
			}
		}

		// Token: 0x04000DE7 RID: 3559
		private Grouping m_grouping;

		// Token: 0x04000DE8 RID: 3560
		private Sorting m_sorting;

		// Token: 0x04000DE9 RID: 3561
		private bool m_keepTogether;

		// Token: 0x04000DEA RID: 3562
		private string m_noRows;

		// Token: 0x04000DEB RID: 3563
		private bool m_fillPage;

		// Token: 0x04000DEC RID: 3564
		private string m_dataSetName;

		// Token: 0x04000DED RID: 3565
		private string m_dataInstanceName;

		// Token: 0x04000DEE RID: 3566
		private GroupingDataElementOutputs m_dataInstanceOutput;

		// Token: 0x04000DEF RID: 3567
		private Filters m_filters;
	}
}
