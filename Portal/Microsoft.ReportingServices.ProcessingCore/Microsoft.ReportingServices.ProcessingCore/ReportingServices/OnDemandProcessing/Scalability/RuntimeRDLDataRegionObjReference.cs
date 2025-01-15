using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000866 RID: 2150
	internal class RuntimeRDLDataRegionObjReference : RuntimeDataRegionObjReference, IReference<IHierarchyObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<RuntimeRDLDataRegionObj>, IReference<IDataRowSortOwner>, IReference<ReportProcessing.IFilterOwner>, IReference<IDataCorrelation>
	{
		// Token: 0x06007743 RID: 30531 RVA: 0x001ED818 File Offset: 0x001EBA18
		internal RuntimeRDLDataRegionObjReference()
		{
		}

		// Token: 0x06007744 RID: 30532 RVA: 0x001ED820 File Offset: 0x001EBA20
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRDLDataRegionObjReference;
		}

		// Token: 0x06007745 RID: 30533 RVA: 0x001ED827 File Offset: 0x001EBA27
		[DebuggerStepThrough]
		IHierarchyObj IReference<IHierarchyObj>.Value()
		{
			return (IHierarchyObj)base.InternalValue();
		}

		// Token: 0x06007746 RID: 30534 RVA: 0x001ED834 File Offset: 0x001EBA34
		[DebuggerStepThrough]
		public new RuntimeRDLDataRegionObj Value()
		{
			return (RuntimeRDLDataRegionObj)base.InternalValue();
		}

		// Token: 0x06007747 RID: 30535 RVA: 0x001ED841 File Offset: 0x001EBA41
		[DebuggerStepThrough]
		IDataRowSortOwner IReference<IDataRowSortOwner>.Value()
		{
			return (IDataRowSortOwner)base.InternalValue();
		}

		// Token: 0x06007748 RID: 30536 RVA: 0x001ED84E File Offset: 0x001EBA4E
		[DebuggerStepThrough]
		ReportProcessing.IFilterOwner IReference<ReportProcessing.IFilterOwner>.Value()
		{
			return (ReportProcessing.IFilterOwner)base.InternalValue();
		}

		// Token: 0x06007749 RID: 30537 RVA: 0x001ED85B File Offset: 0x001EBA5B
		[DebuggerStepThrough]
		IDataCorrelation IReference<IDataCorrelation>.Value()
		{
			return (IDataCorrelation)base.InternalValue();
		}
	}
}
