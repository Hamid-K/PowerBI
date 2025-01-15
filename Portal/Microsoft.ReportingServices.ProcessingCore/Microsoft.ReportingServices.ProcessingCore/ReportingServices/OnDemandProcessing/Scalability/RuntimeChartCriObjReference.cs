using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000870 RID: 2160
	internal class RuntimeChartCriObjReference : RuntimeDataTablixObjReference, IReference<RuntimeChartCriObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600776A RID: 30570 RVA: 0x001ED999 File Offset: 0x001EBB99
		internal RuntimeChartCriObjReference()
		{
		}

		// Token: 0x0600776B RID: 30571 RVA: 0x001ED9A1 File Offset: 0x001EBBA1
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriObjReference;
		}

		// Token: 0x0600776C RID: 30572 RVA: 0x001ED9A8 File Offset: 0x001EBBA8
		[DebuggerStepThrough]
		public new RuntimeChartCriObj Value()
		{
			return (RuntimeChartCriObj)base.InternalValue();
		}
	}
}
