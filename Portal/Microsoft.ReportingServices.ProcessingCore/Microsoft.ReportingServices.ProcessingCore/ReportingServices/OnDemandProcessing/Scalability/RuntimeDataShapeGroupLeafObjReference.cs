using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000861 RID: 2145
	internal class RuntimeDataShapeGroupLeafObjReference : RuntimeDataTablixGroupLeafObjReference, IReference<RuntimeDataShapeGroupLeafObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<ISortDataHolder>
	{
		// Token: 0x06007731 RID: 30513 RVA: 0x001ED76B File Offset: 0x001EB96B
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeGroupLeafObjReference;
		}

		// Token: 0x06007732 RID: 30514 RVA: 0x001ED772 File Offset: 0x001EB972
		[DebuggerStepThrough]
		public new RuntimeDataShapeGroupLeafObj Value()
		{
			return (RuntimeDataShapeGroupLeafObj)base.InternalValue();
		}

		// Token: 0x06007733 RID: 30515 RVA: 0x001ED77F File Offset: 0x001EB97F
		[DebuggerStepThrough]
		ISortDataHolder IReference<ISortDataHolder>.Value()
		{
			return (ISortDataHolder)base.InternalValue();
		}
	}
}
