using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000876 RID: 2166
	internal class SortExpressionScopeInstanceHolderReference : Reference<IHierarchyObj>, IReference<RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600777C RID: 30588 RVA: 0x001EDA38 File Offset: 0x001EBC38
		internal SortExpressionScopeInstanceHolderReference()
		{
		}

		// Token: 0x0600777D RID: 30589 RVA: 0x001EDA40 File Offset: 0x001EBC40
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortExpressionScopeInstanceHolderReference;
		}

		// Token: 0x0600777E RID: 30590 RVA: 0x001EDA44 File Offset: 0x001EBC44
		[DebuggerStepThrough]
		public RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder Value()
		{
			return (RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder)base.InternalValue();
		}
	}
}
