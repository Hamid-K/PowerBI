using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000795 RID: 1941
	internal abstract class ReportItemImpl : ReportItem
	{
		// Token: 0x06006C43 RID: 27715 RVA: 0x001B7160 File Offset: 0x001B5360
		internal ReportItemImpl(ReportItem itemDef, ReportRuntime reportRT, IErrorContext iErrorContext)
		{
			Global.Tracer.Assert(itemDef != null, "(null != itemDef)");
			Global.Tracer.Assert(reportRT != null, "(null != reportRT)");
			Global.Tracer.Assert(iErrorContext != null, "(null != iErrorContext)");
			this.m_item = itemDef;
			this.m_reportRT = reportRT;
			this.m_iErrorContext = iErrorContext;
		}

		// Token: 0x170025B0 RID: 9648
		// (get) Token: 0x06006C44 RID: 27716 RVA: 0x001B71C1 File Offset: 0x001B53C1
		internal string Name
		{
			get
			{
				return this.m_item.Name;
			}
		}

		// Token: 0x170025B1 RID: 9649
		// (set) Token: 0x06006C45 RID: 27717 RVA: 0x001B71CE File Offset: 0x001B53CE
		internal ReportProcessing.IScope Scope
		{
			set
			{
				this.m_scope = value;
			}
		}

		// Token: 0x04003664 RID: 13924
		internal ReportItem m_item;

		// Token: 0x04003665 RID: 13925
		internal ReportRuntime m_reportRT;

		// Token: 0x04003666 RID: 13926
		internal IErrorContext m_iErrorContext;

		// Token: 0x04003667 RID: 13927
		internal ReportProcessing.IScope m_scope;
	}
}
