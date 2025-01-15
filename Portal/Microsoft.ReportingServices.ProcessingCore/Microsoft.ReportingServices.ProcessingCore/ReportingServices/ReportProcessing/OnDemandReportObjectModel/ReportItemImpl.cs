using System;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007B7 RID: 1975
	internal abstract class ReportItemImpl : Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.ReportItem
	{
		// Token: 0x06007027 RID: 28711 RVA: 0x001D33A0 File Offset: 0x001D15A0
		internal ReportItemImpl(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem itemDef, ReportRuntime reportRT, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(itemDef != null, "(null != itemDef)");
			Global.Tracer.Assert(reportRT != null, "(null != reportRT)");
			Global.Tracer.Assert(iErrorContext != null, "(null != iErrorContext)");
			this.m_item = itemDef;
			this.m_reportRT = reportRT;
			this.m_iErrorContext = iErrorContext;
		}

		// Token: 0x1700263A RID: 9786
		// (get) Token: 0x06007028 RID: 28712 RVA: 0x001D3401 File Offset: 0x001D1601
		internal string Name
		{
			get
			{
				return this.m_item.Name;
			}
		}

		// Token: 0x1700263B RID: 9787
		// (set) Token: 0x06007029 RID: 28713 RVA: 0x001D340E File Offset: 0x001D160E
		internal IScope Scope
		{
			set
			{
				this.m_scope = value;
			}
		}

		// Token: 0x0600702A RID: 28714
		internal abstract void Reset();

		// Token: 0x0600702B RID: 28715
		internal abstract void Reset(VariantResult aResult);

		// Token: 0x040039F1 RID: 14833
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem m_item;

		// Token: 0x040039F2 RID: 14834
		internal ReportRuntime m_reportRT;

		// Token: 0x040039F3 RID: 14835
		internal IErrorContext m_iErrorContext;

		// Token: 0x040039F4 RID: 14836
		internal IScope m_scope;
	}
}
