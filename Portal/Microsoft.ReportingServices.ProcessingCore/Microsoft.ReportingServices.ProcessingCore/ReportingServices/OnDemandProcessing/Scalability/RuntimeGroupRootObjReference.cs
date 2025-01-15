using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000862 RID: 2146
	public class RuntimeGroupRootObjReference : RuntimeGroupObjReference, IReference<RuntimeGroupRootObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<IDataCorrelation>
	{
		// Token: 0x06007735 RID: 30517 RVA: 0x001ED794 File Offset: 0x001EB994
		internal RuntimeGroupRootObjReference()
		{
		}

		// Token: 0x06007736 RID: 30518 RVA: 0x001ED79C File Offset: 0x001EB99C
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupRootObjReference;
		}

		// Token: 0x06007737 RID: 30519 RVA: 0x001ED7A3 File Offset: 0x001EB9A3
		[DebuggerStepThrough]
		public new RuntimeGroupRootObj Value()
		{
			return (RuntimeGroupRootObj)base.InternalValue();
		}

		// Token: 0x06007738 RID: 30520 RVA: 0x001ED7B0 File Offset: 0x001EB9B0
		[DebuggerStepThrough]
		IDataCorrelation IReference<IDataCorrelation>.Value()
		{
			return (IDataCorrelation)base.InternalValue();
		}
	}
}
