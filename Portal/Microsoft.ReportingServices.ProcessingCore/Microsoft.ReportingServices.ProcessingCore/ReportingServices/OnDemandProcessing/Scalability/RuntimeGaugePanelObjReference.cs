using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000872 RID: 2162
	internal class RuntimeGaugePanelObjReference : RuntimeChartCriObjReference, IReference<RuntimeGaugePanelObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007770 RID: 30576 RVA: 0x001ED9CE File Offset: 0x001EBBCE
		internal RuntimeGaugePanelObjReference()
		{
		}

		// Token: 0x06007771 RID: 30577 RVA: 0x001ED9D6 File Offset: 0x001EBBD6
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGaugePanelObjReference;
		}

		// Token: 0x06007772 RID: 30578 RVA: 0x001ED9DD File Offset: 0x001EBBDD
		[DebuggerStepThrough]
		public new RuntimeGaugePanelObj Value()
		{
			return (RuntimeGaugePanelObj)base.InternalValue();
		}
	}
}
