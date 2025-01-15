using System;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000875 RID: 2165
	internal class SortFilterExpressionScopeObjReference : Reference<IHierarchyObj>, IReference<RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007779 RID: 30585 RVA: 0x001EDA1F File Offset: 0x001EBC1F
		internal SortFilterExpressionScopeObjReference()
		{
		}

		// Token: 0x0600777A RID: 30586 RVA: 0x001EDA27 File Offset: 0x001EBC27
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterExpressionScopeObjReference;
		}

		// Token: 0x0600777B RID: 30587 RVA: 0x001EDA2B File Offset: 0x001EBC2B
		[DebuggerStepThrough]
		public RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj Value()
		{
			return (RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj)base.InternalValue();
		}
	}
}
