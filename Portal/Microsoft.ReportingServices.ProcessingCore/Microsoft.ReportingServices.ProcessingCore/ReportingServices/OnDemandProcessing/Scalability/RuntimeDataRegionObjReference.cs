using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200085B RID: 2139
	public class RuntimeDataRegionObjReference : IScopeReference, IReference<RuntimeDataRegionObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007719 RID: 30489 RVA: 0x001ED67B File Offset: 0x001EB87B
		internal RuntimeDataRegionObjReference()
		{
		}

		// Token: 0x0600771A RID: 30490 RVA: 0x001ED683 File Offset: 0x001EB883
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObjReference;
		}

		// Token: 0x0600771B RID: 30491 RVA: 0x001ED68A File Offset: 0x001EB88A
		[DebuggerStepThrough]
		public new RuntimeDataRegionObj Value()
		{
			return (RuntimeDataRegionObj)base.InternalValue();
		}
	}
}
