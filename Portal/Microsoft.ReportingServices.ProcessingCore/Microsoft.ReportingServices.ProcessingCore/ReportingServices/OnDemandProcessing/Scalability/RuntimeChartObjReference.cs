using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000871 RID: 2161
	internal class RuntimeChartObjReference : RuntimeChartCriObjReference, IReference<RuntimeChartObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600776D RID: 30573 RVA: 0x001ED9B5 File Offset: 0x001EBBB5
		internal RuntimeChartObjReference()
		{
		}

		// Token: 0x0600776E RID: 30574 RVA: 0x001ED9BD File Offset: 0x001EBBBD
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartObjReference;
		}

		// Token: 0x0600776F RID: 30575 RVA: 0x001ED9C1 File Offset: 0x001EBBC1
		[DebuggerStepThrough]
		public new RuntimeChartObj Value()
		{
			return (RuntimeChartObj)base.InternalValue();
		}
	}
}
