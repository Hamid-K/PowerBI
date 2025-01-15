using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200086E RID: 2158
	internal class RuntimeTablixObjReference : RuntimeDataTablixObjReference, IReference<RuntimeTablixObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007764 RID: 30564 RVA: 0x001ED964 File Offset: 0x001EBB64
		internal RuntimeTablixObjReference()
		{
		}

		// Token: 0x06007765 RID: 30565 RVA: 0x001ED96C File Offset: 0x001EBB6C
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixObjReference;
		}

		// Token: 0x06007766 RID: 30566 RVA: 0x001ED970 File Offset: 0x001EBB70
		[DebuggerStepThrough]
		public new RuntimeTablixObj Value()
		{
			return (RuntimeTablixObj)base.InternalValue();
		}
	}
}
