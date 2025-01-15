using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200086F RID: 2159
	internal class RuntimeDataShapeObjReference : RuntimeDataTablixObjReference, IReference<RuntimeDataShapeObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007767 RID: 30567 RVA: 0x001ED97D File Offset: 0x001EBB7D
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeObjReference;
		}

		// Token: 0x06007768 RID: 30568 RVA: 0x001ED984 File Offset: 0x001EBB84
		[DebuggerStepThrough]
		public new RuntimeDataShapeObj Value()
		{
			return (RuntimeDataShapeObj)base.InternalValue();
		}
	}
}
