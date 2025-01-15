using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200086C RID: 2156
	internal class RuntimeSortHierarchyObjReference : Reference<IHierarchyObj>, IReference<RuntimeSortHierarchyObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600775C RID: 30556 RVA: 0x001ED915 File Offset: 0x001EBB15
		internal RuntimeSortHierarchyObjReference()
		{
		}

		// Token: 0x0600775D RID: 30557 RVA: 0x001ED91D File Offset: 0x001EBB1D
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortHierarchyObjReference;
		}

		// Token: 0x0600775E RID: 30558 RVA: 0x001ED921 File Offset: 0x001EBB21
		[DebuggerStepThrough]
		public RuntimeSortHierarchyObj Value()
		{
			return (RuntimeSortHierarchyObj)base.InternalValue();
		}
	}
}
