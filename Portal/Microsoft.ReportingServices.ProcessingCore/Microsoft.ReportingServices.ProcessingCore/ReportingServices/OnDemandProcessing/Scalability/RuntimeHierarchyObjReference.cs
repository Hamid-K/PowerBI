using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200085C RID: 2140
	public class RuntimeHierarchyObjReference : RuntimeDataRegionObjReference, IReference<IHierarchyObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<RuntimeHierarchyObj>
	{
		// Token: 0x0600771C RID: 30492 RVA: 0x001ED697 File Offset: 0x001EB897
		internal RuntimeHierarchyObjReference()
		{
		}

		// Token: 0x0600771D RID: 30493 RVA: 0x001ED69F File Offset: 0x001EB89F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObjReference;
		}

		// Token: 0x0600771E RID: 30494 RVA: 0x001ED6A3 File Offset: 0x001EB8A3
		[DebuggerStepThrough]
		IHierarchyObj IReference<IHierarchyObj>.Value()
		{
			return (IHierarchyObj)base.InternalValue();
		}

		// Token: 0x0600771F RID: 30495 RVA: 0x001ED6B0 File Offset: 0x001EB8B0
		[DebuggerStepThrough]
		public new RuntimeHierarchyObj Value()
		{
			return (RuntimeHierarchyObj)base.InternalValue();
		}
	}
}
