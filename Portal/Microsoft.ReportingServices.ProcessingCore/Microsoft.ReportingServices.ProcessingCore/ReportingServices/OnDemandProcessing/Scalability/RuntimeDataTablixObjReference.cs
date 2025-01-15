using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000867 RID: 2151
	internal class RuntimeDataTablixObjReference : RuntimeRDLDataRegionObjReference, IReference<RuntimeDataTablixObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IOnDemandMemberOwnerInstanceReference, IReference<IOnDemandScopeInstance>, IReference<IOnDemandMemberOwnerInstance>
	{
		// Token: 0x0600774A RID: 30538 RVA: 0x001ED868 File Offset: 0x001EBA68
		internal RuntimeDataTablixObjReference()
		{
		}

		// Token: 0x0600774B RID: 30539 RVA: 0x001ED870 File Offset: 0x001EBA70
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixObjReference;
		}

		// Token: 0x0600774C RID: 30540 RVA: 0x001ED877 File Offset: 0x001EBA77
		[DebuggerStepThrough]
		public new RuntimeDataTablixObj Value()
		{
			return (RuntimeDataTablixObj)base.InternalValue();
		}

		// Token: 0x0600774D RID: 30541 RVA: 0x001ED884 File Offset: 0x001EBA84
		[DebuggerStepThrough]
		IOnDemandScopeInstance IReference<IOnDemandScopeInstance>.Value()
		{
			return (IOnDemandScopeInstance)base.InternalValue();
		}

		// Token: 0x0600774E RID: 30542 RVA: 0x001ED891 File Offset: 0x001EBA91
		[DebuggerStepThrough]
		IOnDemandMemberOwnerInstance IReference<IOnDemandMemberOwnerInstance>.Value()
		{
			return (IOnDemandMemberOwnerInstance)base.InternalValue();
		}
	}
}
