using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200086B RID: 2155
	internal class RuntimeChartCriCellReference : RuntimeCellReference, IReference<RuntimeChartCriCell>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007759 RID: 30553 RVA: 0x001ED8FC File Offset: 0x001EBAFC
		internal RuntimeChartCriCellReference()
		{
		}

		// Token: 0x0600775A RID: 30554 RVA: 0x001ED904 File Offset: 0x001EBB04
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriCellReference;
		}

		// Token: 0x0600775B RID: 30555 RVA: 0x001ED908 File Offset: 0x001EBB08
		[DebuggerStepThrough]
		public new RuntimeChartCriCell Value()
		{
			return (RuntimeChartCriCell)base.InternalValue();
		}
	}
}
