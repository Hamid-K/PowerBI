using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200085D RID: 2141
	public class RuntimeGroupObjReference : RuntimeHierarchyObjReference, IReference<RuntimeGroupObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<ReportProcessing.IFilterOwner>
	{
		// Token: 0x06007720 RID: 30496 RVA: 0x001ED6BD File Offset: 0x001EB8BD
		internal RuntimeGroupObjReference()
		{
		}

		// Token: 0x06007721 RID: 30497 RVA: 0x001ED6C5 File Offset: 0x001EB8C5
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupObjReference;
		}

		// Token: 0x06007722 RID: 30498 RVA: 0x001ED6CC File Offset: 0x001EB8CC
		[DebuggerStepThrough]
		public new RuntimeGroupObj Value()
		{
			return (RuntimeGroupObj)base.InternalValue();
		}

		// Token: 0x06007723 RID: 30499 RVA: 0x001ED6D9 File Offset: 0x001EB8D9
		ReportProcessing.IFilterOwner IReference<ReportProcessing.IFilterOwner>.Value()
		{
			return (ReportProcessing.IFilterOwner)base.InternalValue();
		}
	}
}
