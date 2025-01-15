using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000025 RID: 37
	public abstract class ReportExprHost : ReportItemExprHost
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x000023EB File Offset: 0x000005EB
		protected ReportExprHost()
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000023F3 File Offset: 0x000005F3
		protected ReportExprHost(object reportObjectModel)
		{
			base.SetReportObjectModel((ObjectModel)reportObjectModel);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002407 File Offset: 0x00000607
		internal void CustomCodeOnInit()
		{
			if (this.m_codeProxyBase != null)
			{
				this.m_codeProxyBase.CallOnInit();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000241C File Offset: 0x0000061C
		public virtual object ReportLanguageExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000241F File Offset: 0x0000061F
		internal IList<AggregateParamExprHost> AggregateParamHostsRemotable
		{
			get
			{
				if (this.m_aggregateParamHostsRemotable == null && this.AggregateParamHosts != null)
				{
					this.m_aggregateParamHostsRemotable = new RemoteArrayWrapper<AggregateParamExprHost>(this.AggregateParamHosts);
				}
				return this.m_aggregateParamHostsRemotable;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00002448 File Offset: 0x00000648
		internal IList<ReportParamExprHost> ReportParameterHostsRemotable
		{
			get
			{
				if (this.m_reportParameterHostsRemotable == null && this.ReportParameterHosts != null)
				{
					this.m_reportParameterHostsRemotable = new RemoteArrayWrapper<ReportParamExprHost>(this.ReportParameterHosts);
				}
				return this.m_reportParameterHostsRemotable;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002471 File Offset: 0x00000671
		internal IList<DataSourceExprHost> DataSourceHostsRemotable
		{
			get
			{
				if (this.m_dataSourceHostsRemotable == null && this.DataSourceHosts != null)
				{
					this.m_dataSourceHostsRemotable = new RemoteArrayWrapper<DataSourceExprHost>(this.DataSourceHosts);
				}
				return this.m_dataSourceHostsRemotable;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000249A File Offset: 0x0000069A
		internal IList<DataSetExprHost> DataSetHostsRemotable
		{
			get
			{
				if (this.m_dataSetHostsRemotable == null && this.DataSetHosts != null)
				{
					this.m_dataSetHostsRemotable = new RemoteArrayWrapper<DataSetExprHost>(this.DataSetHosts);
				}
				return this.m_dataSetHostsRemotable;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000024C3 File Offset: 0x000006C3
		internal IList<StyleExprHost> PageSectionHostsRemotable
		{
			get
			{
				if (this.m_pageSectionHostsRemotable == null && this.PageSectionHosts != null)
				{
					this.m_pageSectionHostsRemotable = new RemoteArrayWrapper<StyleExprHost>(this.PageSectionHosts);
				}
				return this.m_pageSectionHostsRemotable;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000024EC File Offset: 0x000006EC
		internal IList<ReportItemExprHost> LineHostsRemotable
		{
			get
			{
				if (this.m_lineHostsRemotable == null && this.LineHosts != null)
				{
					this.m_lineHostsRemotable = new RemoteArrayWrapper<ReportItemExprHost>(this.LineHosts);
				}
				return this.m_lineHostsRemotable;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002515 File Offset: 0x00000715
		internal IList<ReportItemExprHost> RectangleHostsRemotable
		{
			get
			{
				if (this.m_rectangleHostsRemotable == null && this.RectangleHosts != null)
				{
					this.m_rectangleHostsRemotable = new RemoteArrayWrapper<ReportItemExprHost>(this.RectangleHosts);
				}
				return this.m_rectangleHostsRemotable;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000253E File Offset: 0x0000073E
		internal IList<TextBoxExprHost> TextBoxHostsRemotable
		{
			get
			{
				if (this.m_textBoxHostsRemotable == null && this.TextBoxHosts != null)
				{
					this.m_textBoxHostsRemotable = new RemoteArrayWrapper<TextBoxExprHost>(this.TextBoxHosts);
				}
				return this.m_textBoxHostsRemotable;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002567 File Offset: 0x00000767
		internal IList<ImageExprHost> ImageHostsRemotable
		{
			get
			{
				if (this.m_imageHostsRemotable == null && this.ImageHosts != null)
				{
					this.m_imageHostsRemotable = new RemoteArrayWrapper<ImageExprHost>(this.ImageHosts);
				}
				return this.m_imageHostsRemotable;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002590 File Offset: 0x00000790
		internal IList<SubreportExprHost> SubreportHostsRemotable
		{
			get
			{
				if (this.m_subreportHostsRemotable == null && this.SubreportHosts != null)
				{
					this.m_subreportHostsRemotable = new RemoteArrayWrapper<SubreportExprHost>(this.SubreportHosts);
				}
				return this.m_subreportHostsRemotable;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000025B9 File Offset: 0x000007B9
		internal IList<ActiveXControlExprHost> ActiveXControlHostsRemotable
		{
			get
			{
				if (this.m_activeXControlHostsRemotable == null && this.ActiveXControlHosts != null)
				{
					this.m_activeXControlHostsRemotable = new RemoteArrayWrapper<ActiveXControlExprHost>(this.ActiveXControlHosts);
				}
				return this.m_activeXControlHostsRemotable;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000025E2 File Offset: 0x000007E2
		internal IList<ListExprHost> ListHostsRemotable
		{
			get
			{
				if (this.m_listHostsRemotable == null && this.ListHosts != null)
				{
					this.m_listHostsRemotable = new RemoteArrayWrapper<ListExprHost>(this.ListHosts);
				}
				return this.m_listHostsRemotable;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000260B File Offset: 0x0000080B
		internal IList<MatrixExprHost> MatrixHostsRemotable
		{
			get
			{
				if (this.m_matrixHostsRemotable == null && this.MatrixHosts != null)
				{
					this.m_matrixHostsRemotable = new RemoteArrayWrapper<MatrixExprHost>(this.MatrixHosts);
				}
				return this.m_matrixHostsRemotable;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002634 File Offset: 0x00000834
		internal IList<ChartExprHost> ChartHostsRemotable
		{
			get
			{
				if (this.m_chartHostsRemotable == null && this.ChartHosts != null)
				{
					this.m_chartHostsRemotable = new RemoteArrayWrapper<ChartExprHost>(this.ChartHosts);
				}
				return this.m_chartHostsRemotable;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000265D File Offset: 0x0000085D
		internal IList<TableExprHost> TableHostsRemotable
		{
			get
			{
				if (this.m_tableHostsRemotable == null && this.TableHosts != null)
				{
					this.m_tableHostsRemotable = new RemoteArrayWrapper<TableExprHost>(this.TableHosts);
				}
				return this.m_tableHostsRemotable;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002686 File Offset: 0x00000886
		internal IList<OWCChartExprHost> OWCChartHostsRemotable
		{
			get
			{
				if (this.m_OWCChartHostsRemotable == null && this.OWCChartHosts != null)
				{
					this.m_OWCChartHostsRemotable = new RemoteArrayWrapper<OWCChartExprHost>(this.OWCChartHosts);
				}
				return this.m_OWCChartHostsRemotable;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000026AF File Offset: 0x000008AF
		internal IList<CustomReportItemExprHost> CustomReportItemHostsRemotable
		{
			get
			{
				return this.m_customReportItemHostsRemotable;
			}
		}

		// Token: 0x04000006 RID: 6
		protected CustomCodeProxyBase m_codeProxyBase;

		// Token: 0x04000007 RID: 7
		protected AggregateParamExprHost[] AggregateParamHosts;

		// Token: 0x04000008 RID: 8
		[CLSCompliant(false)]
		protected IList<AggregateParamExprHost> m_aggregateParamHostsRemotable;

		// Token: 0x04000009 RID: 9
		protected ReportParamExprHost[] ReportParameterHosts;

		// Token: 0x0400000A RID: 10
		[CLSCompliant(false)]
		protected IList<ReportParamExprHost> m_reportParameterHostsRemotable;

		// Token: 0x0400000B RID: 11
		protected DataSourceExprHost[] DataSourceHosts;

		// Token: 0x0400000C RID: 12
		[CLSCompliant(false)]
		protected IList<DataSourceExprHost> m_dataSourceHostsRemotable;

		// Token: 0x0400000D RID: 13
		protected DataSetExprHost[] DataSetHosts;

		// Token: 0x0400000E RID: 14
		[CLSCompliant(false)]
		protected IList<DataSetExprHost> m_dataSetHostsRemotable;

		// Token: 0x0400000F RID: 15
		protected StyleExprHost[] PageSectionHosts;

		// Token: 0x04000010 RID: 16
		[CLSCompliant(false)]
		protected IList<StyleExprHost> m_pageSectionHostsRemotable;

		// Token: 0x04000011 RID: 17
		protected ReportItemExprHost[] LineHosts;

		// Token: 0x04000012 RID: 18
		[CLSCompliant(false)]
		protected IList<ReportItemExprHost> m_lineHostsRemotable;

		// Token: 0x04000013 RID: 19
		protected ReportItemExprHost[] RectangleHosts;

		// Token: 0x04000014 RID: 20
		[CLSCompliant(false)]
		protected IList<ReportItemExprHost> m_rectangleHostsRemotable;

		// Token: 0x04000015 RID: 21
		protected TextBoxExprHost[] TextBoxHosts;

		// Token: 0x04000016 RID: 22
		[CLSCompliant(false)]
		protected IList<TextBoxExprHost> m_textBoxHostsRemotable;

		// Token: 0x04000017 RID: 23
		protected ImageExprHost[] ImageHosts;

		// Token: 0x04000018 RID: 24
		[CLSCompliant(false)]
		protected IList<ImageExprHost> m_imageHostsRemotable;

		// Token: 0x04000019 RID: 25
		protected SubreportExprHost[] SubreportHosts;

		// Token: 0x0400001A RID: 26
		[CLSCompliant(false)]
		protected IList<SubreportExprHost> m_subreportHostsRemotable;

		// Token: 0x0400001B RID: 27
		protected ActiveXControlExprHost[] ActiveXControlHosts;

		// Token: 0x0400001C RID: 28
		[CLSCompliant(false)]
		protected IList<ActiveXControlExprHost> m_activeXControlHostsRemotable;

		// Token: 0x0400001D RID: 29
		protected ListExprHost[] ListHosts;

		// Token: 0x0400001E RID: 30
		[CLSCompliant(false)]
		protected IList<ListExprHost> m_listHostsRemotable;

		// Token: 0x0400001F RID: 31
		protected MatrixExprHost[] MatrixHosts;

		// Token: 0x04000020 RID: 32
		[CLSCompliant(false)]
		protected IList<MatrixExprHost> m_matrixHostsRemotable;

		// Token: 0x04000021 RID: 33
		protected ChartExprHost[] ChartHosts;

		// Token: 0x04000022 RID: 34
		[CLSCompliant(false)]
		protected IList<ChartExprHost> m_chartHostsRemotable;

		// Token: 0x04000023 RID: 35
		protected TableExprHost[] TableHosts;

		// Token: 0x04000024 RID: 36
		[CLSCompliant(false)]
		protected IList<TableExprHost> m_tableHostsRemotable;

		// Token: 0x04000025 RID: 37
		protected OWCChartExprHost[] OWCChartHosts;

		// Token: 0x04000026 RID: 38
		[CLSCompliant(false)]
		protected IList<OWCChartExprHost> m_OWCChartHostsRemotable;

		// Token: 0x04000027 RID: 39
		[CLSCompliant(false)]
		protected IList<CustomReportItemExprHost> m_customReportItemHostsRemotable;
	}
}
