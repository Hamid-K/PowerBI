using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200086D RID: 2157
	internal class RuntimeOnDemandDataSetObjReference : IScopeReference, IReference<IHierarchyObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<RuntimeOnDemandDataSetObj>, IReference<ReportProcessing.IFilterOwner>
	{
		// Token: 0x0600775F RID: 30559 RVA: 0x001ED92E File Offset: 0x001EBB2E
		internal RuntimeOnDemandDataSetObjReference()
		{
		}

		// Token: 0x06007760 RID: 30560 RVA: 0x001ED936 File Offset: 0x001EBB36
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeOnDemandDataSetObjReference;
		}

		// Token: 0x06007761 RID: 30561 RVA: 0x001ED93D File Offset: 0x001EBB3D
		[DebuggerStepThrough]
		IHierarchyObj IReference<IHierarchyObj>.Value()
		{
			return (IHierarchyObj)base.InternalValue();
		}

		// Token: 0x06007762 RID: 30562 RVA: 0x001ED94A File Offset: 0x001EBB4A
		[DebuggerStepThrough]
		public new RuntimeOnDemandDataSetObj Value()
		{
			return (RuntimeOnDemandDataSetObj)base.InternalValue();
		}

		// Token: 0x06007763 RID: 30563 RVA: 0x001ED957 File Offset: 0x001EBB57
		[DebuggerStepThrough]
		ReportProcessing.IFilterOwner IReference<ReportProcessing.IFilterOwner>.Value()
		{
			return (ReportProcessing.IFilterOwner)base.InternalValue();
		}
	}
}
