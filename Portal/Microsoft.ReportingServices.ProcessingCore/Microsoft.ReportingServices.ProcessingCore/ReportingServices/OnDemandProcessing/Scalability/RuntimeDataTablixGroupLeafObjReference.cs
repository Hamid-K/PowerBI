using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200085F RID: 2143
	internal class RuntimeDataTablixGroupLeafObjReference : RuntimeGroupLeafObjReference, IReference<RuntimeDataTablixGroupLeafObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IOnDemandMemberInstanceReference, IOnDemandMemberOwnerInstanceReference, IReference<IOnDemandScopeInstance>, IReference<IOnDemandMemberOwnerInstance>, IReference<IOnDemandMemberInstance>
	{
		// Token: 0x06007727 RID: 30503 RVA: 0x001ED702 File Offset: 0x001EB902
		internal RuntimeDataTablixGroupLeafObjReference()
		{
		}

		// Token: 0x06007728 RID: 30504 RVA: 0x001ED70A File Offset: 0x001EB90A
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupLeafObjReference;
		}

		// Token: 0x06007729 RID: 30505 RVA: 0x001ED711 File Offset: 0x001EB911
		[DebuggerStepThrough]
		public new RuntimeDataTablixGroupLeafObj Value()
		{
			return (RuntimeDataTablixGroupLeafObj)base.InternalValue();
		}

		// Token: 0x0600772A RID: 30506 RVA: 0x001ED71E File Offset: 0x001EB91E
		[DebuggerStepThrough]
		IOnDemandScopeInstance IReference<IOnDemandScopeInstance>.Value()
		{
			return (IOnDemandScopeInstance)base.InternalValue();
		}

		// Token: 0x0600772B RID: 30507 RVA: 0x001ED72B File Offset: 0x001EB92B
		[DebuggerStepThrough]
		IOnDemandMemberOwnerInstance IReference<IOnDemandMemberOwnerInstance>.Value()
		{
			return (IOnDemandMemberOwnerInstance)base.InternalValue();
		}

		// Token: 0x0600772C RID: 30508 RVA: 0x001ED738 File Offset: 0x001EB938
		[DebuggerStepThrough]
		IOnDemandMemberInstance IReference<IOnDemandMemberInstance>.Value()
		{
			return (IOnDemandMemberInstance)base.InternalValue();
		}
	}
}
