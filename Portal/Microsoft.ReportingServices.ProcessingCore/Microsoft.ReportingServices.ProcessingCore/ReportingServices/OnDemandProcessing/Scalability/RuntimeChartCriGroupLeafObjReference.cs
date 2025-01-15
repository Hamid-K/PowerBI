using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000865 RID: 2149
	internal class RuntimeChartCriGroupLeafObjReference : RuntimeDataTablixGroupLeafObjReference, IReference<RuntimeChartCriGroupLeafObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<ISortDataHolder>
	{
		// Token: 0x0600773F RID: 30527 RVA: 0x001ED7F2 File Offset: 0x001EB9F2
		internal RuntimeChartCriGroupLeafObjReference()
		{
		}

		// Token: 0x06007740 RID: 30528 RVA: 0x001ED7FA File Offset: 0x001EB9FA
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriGroupLeafObjReference;
		}

		// Token: 0x06007741 RID: 30529 RVA: 0x001ED7FE File Offset: 0x001EB9FE
		[DebuggerStepThrough]
		public new RuntimeChartCriGroupLeafObj Value()
		{
			return (RuntimeChartCriGroupLeafObj)base.InternalValue();
		}

		// Token: 0x06007742 RID: 30530 RVA: 0x001ED80B File Offset: 0x001EBA0B
		[DebuggerStepThrough]
		ISortDataHolder IReference<ISortDataHolder>.Value()
		{
			return (ISortDataHolder)base.InternalValue();
		}
	}
}
