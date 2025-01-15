using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200086A RID: 2154
	internal class RuntimeDataShapeIntersectionReference : RuntimeCellReference, IReference<RuntimeDataShapeIntersection>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007756 RID: 30550 RVA: 0x001ED8E0 File Offset: 0x001EBAE0
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeIntersectionReference;
		}

		// Token: 0x06007757 RID: 30551 RVA: 0x001ED8E7 File Offset: 0x001EBAE7
		[DebuggerStepThrough]
		public new RuntimeDataShapeIntersection Value()
		{
			return (RuntimeDataShapeIntersection)base.InternalValue();
		}
	}
}
