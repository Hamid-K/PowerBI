using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000052 RID: 82
	public abstract class ReportExprHost : ReportItemExprHost
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00002E22 File Offset: 0x00001022
		protected ReportExprHost(object reportObjectModel)
		{
			base.SetReportObjectModel((OnDemandObjectModel)reportObjectModel);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00002E36 File Offset: 0x00001036
		internal void CustomCodeOnInit()
		{
			if (this.m_codeProxyBase != null)
			{
				this.m_codeProxyBase.CallOnInit();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00002E4B File Offset: 0x0000104B
		public virtual object ReportLanguageExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00002E4E File Offset: 0x0000104E
		public virtual object AutoRefreshExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00002E51 File Offset: 0x00001051
		public virtual object InitialPageNameExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00002E54 File Offset: 0x00001054
		internal IList<AggregateParamExprHost> AggregateParamHostsRemotable
		{
			get
			{
				return this.m_aggregateParamHostsRemotable;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00002E5C File Offset: 0x0000105C
		[CLSCompliant(false)]
		public IList<LookupExprHost> LookupExprHostsRemotable
		{
			get
			{
				return this.m_lookupExprHostsRemotable;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00002E64 File Offset: 0x00001064
		[CLSCompliant(false)]
		public IList<LookupDestExprHost> LookupDestExprHostsRemotable
		{
			get
			{
				return this.m_lookupDestExprHostsRemotable;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00002E6C File Offset: 0x0000106C
		internal IList<ReportParamExprHost> ReportParameterHostsRemotable
		{
			get
			{
				return this.m_reportParameterHostsRemotable;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00002E74 File Offset: 0x00001074
		internal IList<DataSourceExprHost> DataSourceHostsRemotable
		{
			get
			{
				return this.m_dataSourceHostsRemotable;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00002E7C File Offset: 0x0000107C
		internal IList<DataSetExprHost> DataSetHostsRemotable
		{
			get
			{
				return this.m_dataSetHostsRemotable;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00002E84 File Offset: 0x00001084
		internal IList<StyleExprHost> PageSectionHostsRemotable
		{
			get
			{
				return this.m_pageSectionHostsRemotable;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00002E8C File Offset: 0x0000108C
		internal virtual StyleExprHost PageHost
		{
			get
			{
				return this.m_pageHost;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00002E94 File Offset: 0x00001094
		[CLSCompliant(false)]
		public IList<StyleExprHost> PageHostsRemotable
		{
			get
			{
				return this.m_pageHostsRemotable;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00002E9C File Offset: 0x0000109C
		[CLSCompliant(false)]
		public IList<ReportSectionExprHost> ReportSectionHostsRemotable
		{
			get
			{
				return this.m_reportSectionHostsRemotable;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00002EA4 File Offset: 0x000010A4
		internal IList<ReportItemExprHost> LineHostsRemotable
		{
			get
			{
				return this.m_lineHostsRemotable;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00002EAC File Offset: 0x000010AC
		internal IList<ReportItemExprHost> RectangleHostsRemotable
		{
			get
			{
				return this.m_rectangleHostsRemotable;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00002EB4 File Offset: 0x000010B4
		internal IList<TextBoxExprHost> TextBoxHostsRemotable
		{
			get
			{
				return this.m_textBoxHostsRemotable;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00002EBC File Offset: 0x000010BC
		internal IList<ImageExprHost> ImageHostsRemotable
		{
			get
			{
				return this.m_imageHostsRemotable;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00002EC4 File Offset: 0x000010C4
		internal IList<SubreportExprHost> SubreportHostsRemotable
		{
			get
			{
				return this.m_subreportHostsRemotable;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00002ECC File Offset: 0x000010CC
		internal IList<TablixExprHost> TablixHostsRemotable
		{
			get
			{
				return this.m_tablixHostsRemotable;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00002ED4 File Offset: 0x000010D4
		internal IList<ChartExprHost> ChartHostsRemotable
		{
			get
			{
				return this.m_chartHostsRemotable;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00002EDC File Offset: 0x000010DC
		internal IList<GaugePanelExprHost> GaugePanelHostsRemotable
		{
			get
			{
				return this.m_gaugePanelHostsRemotable;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00002EE4 File Offset: 0x000010E4
		internal IList<MapExprHost> MapHostsRemotable
		{
			get
			{
				return this.m_mapHostsRemotable;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00002EEC File Offset: 0x000010EC
		internal IList<MapDataRegionExprHost> MapDataRegionHostsRemotable
		{
			get
			{
				return this.m_mapDataRegionHostsRemotable;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00002EF4 File Offset: 0x000010F4
		internal IList<CustomReportItemExprHost> CustomReportItemHostsRemotable
		{
			get
			{
				return this.m_customReportItemHostsRemotable;
			}
		}

		// Token: 0x0400007E RID: 126
		protected CustomCodeProxyBase m_codeProxyBase;

		// Token: 0x0400007F RID: 127
		public IndexedExprHost VariableValueHosts;

		// Token: 0x04000080 RID: 128
		[CLSCompliant(false)]
		protected IList<AggregateParamExprHost> m_aggregateParamHostsRemotable;

		// Token: 0x04000081 RID: 129
		[CLSCompliant(false)]
		protected IList<LookupExprHost> m_lookupExprHostsRemotable;

		// Token: 0x04000082 RID: 130
		[CLSCompliant(false)]
		protected IList<LookupDestExprHost> m_lookupDestExprHostsRemotable;

		// Token: 0x04000083 RID: 131
		[CLSCompliant(false)]
		protected IList<ReportParamExprHost> m_reportParameterHostsRemotable;

		// Token: 0x04000084 RID: 132
		[CLSCompliant(false)]
		protected IList<DataSourceExprHost> m_dataSourceHostsRemotable;

		// Token: 0x04000085 RID: 133
		[CLSCompliant(false)]
		protected IList<DataSetExprHost> m_dataSetHostsRemotable;

		// Token: 0x04000086 RID: 134
		[CLSCompliant(false)]
		protected IList<StyleExprHost> m_pageSectionHostsRemotable;

		// Token: 0x04000087 RID: 135
		protected StyleExprHost m_pageHost;

		// Token: 0x04000088 RID: 136
		[CLSCompliant(false)]
		protected IList<StyleExprHost> m_pageHostsRemotable;

		// Token: 0x04000089 RID: 137
		[CLSCompliant(false)]
		protected IList<ReportSectionExprHost> m_reportSectionHostsRemotable;

		// Token: 0x0400008A RID: 138
		[CLSCompliant(false)]
		protected IList<ReportItemExprHost> m_lineHostsRemotable;

		// Token: 0x0400008B RID: 139
		[CLSCompliant(false)]
		protected IList<ReportItemExprHost> m_rectangleHostsRemotable;

		// Token: 0x0400008C RID: 140
		[CLSCompliant(false)]
		protected IList<TextBoxExprHost> m_textBoxHostsRemotable;

		// Token: 0x0400008D RID: 141
		[CLSCompliant(false)]
		protected IList<ImageExprHost> m_imageHostsRemotable;

		// Token: 0x0400008E RID: 142
		[CLSCompliant(false)]
		protected IList<SubreportExprHost> m_subreportHostsRemotable;

		// Token: 0x0400008F RID: 143
		[CLSCompliant(false)]
		protected IList<TablixExprHost> m_tablixHostsRemotable;

		// Token: 0x04000090 RID: 144
		[CLSCompliant(false)]
		protected IList<ChartExprHost> m_chartHostsRemotable;

		// Token: 0x04000091 RID: 145
		[CLSCompliant(false)]
		protected IList<GaugePanelExprHost> m_gaugePanelHostsRemotable;

		// Token: 0x04000092 RID: 146
		[CLSCompliant(false)]
		protected IList<MapExprHost> m_mapHostsRemotable;

		// Token: 0x04000093 RID: 147
		[CLSCompliant(false)]
		protected IList<MapDataRegionExprHost> m_mapDataRegionHostsRemotable;

		// Token: 0x04000094 RID: 148
		[CLSCompliant(false)]
		protected IList<CustomReportItemExprHost> m_customReportItemHostsRemotable;
	}
}
