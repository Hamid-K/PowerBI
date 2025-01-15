using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000874 RID: 2164
	internal class RuntimeCriObjReference : RuntimeChartCriObjReference, IReference<RuntimeCriObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007776 RID: 30582 RVA: 0x001EDA06 File Offset: 0x001EBC06
		internal RuntimeCriObjReference()
		{
		}

		// Token: 0x06007777 RID: 30583 RVA: 0x001EDA0E File Offset: 0x001EBC0E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCriObjReference;
		}

		// Token: 0x06007778 RID: 30584 RVA: 0x001EDA12 File Offset: 0x001EBC12
		[DebuggerStepThrough]
		public new RuntimeCriObj Value()
		{
			return (RuntimeCriObj)base.InternalValue();
		}
	}
}
