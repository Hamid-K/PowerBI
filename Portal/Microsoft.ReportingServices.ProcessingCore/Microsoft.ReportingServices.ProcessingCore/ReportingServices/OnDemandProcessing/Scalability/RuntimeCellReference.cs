using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000868 RID: 2152
	internal class RuntimeCellReference : IScopeReference, IReference<RuntimeCell>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<IOnDemandScopeInstance>
	{
		// Token: 0x0600774F RID: 30543 RVA: 0x001ED89E File Offset: 0x001EBA9E
		internal RuntimeCellReference()
		{
		}

		// Token: 0x06007750 RID: 30544 RVA: 0x001ED8A6 File Offset: 0x001EBAA6
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCellReference;
		}

		// Token: 0x06007751 RID: 30545 RVA: 0x001ED8AD File Offset: 0x001EBAAD
		[DebuggerStepThrough]
		public new RuntimeCell Value()
		{
			return (RuntimeCell)base.InternalValue();
		}

		// Token: 0x06007752 RID: 30546 RVA: 0x001ED8BA File Offset: 0x001EBABA
		[DebuggerStepThrough]
		IOnDemandScopeInstance IReference<IOnDemandScopeInstance>.Value()
		{
			return (IOnDemandScopeInstance)base.InternalValue();
		}
	}
}
