using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000831 RID: 2097
	[PersistedWithinRequestOnly]
	[SkipStaticValidation]
	internal class StreamingNoRowsMemberInstance : StreamingNoRowsScopeInstanceBase, IOnDemandMemberInstance, IOnDemandMemberOwnerInstance, IOnDemandScopeInstance, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007588 RID: 30088 RVA: 0x001E750B File Offset: 0x001E570B
		public StreamingNoRowsMemberInstance(OnDemandProcessingContext odpContext, IRIFReportDataScope member)
			: base(odpContext, member)
		{
		}

		// Token: 0x06007589 RID: 30089 RVA: 0x001E7515 File Offset: 0x001E5715
		public IOnDemandMemberInstanceReference GetNextMemberInstance()
		{
			return null;
		}

		// Token: 0x0600758A RID: 30090 RVA: 0x001E7518 File Offset: 0x001E5718
		public IOnDemandScopeInstance GetCellInstance(IOnDemandMemberInstanceReference outerGroupInstanceRef, out IReference<IOnDemandScopeInstance> cellRef)
		{
			cellRef = null;
			return null;
		}

		// Token: 0x1700279D RID: 10141
		// (get) Token: 0x0600758B RID: 30091 RVA: 0x001E751E File Offset: 0x001E571E
		public List<object> GroupExprValues
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600758C RID: 30092 RVA: 0x001E7521 File Offset: 0x001E5721
		public IOnDemandMemberInstanceReference GetFirstMemberInstance(ReportHierarchyNode rifMember)
		{
			return null;
		}

		// Token: 0x0600758D RID: 30093 RVA: 0x001E7524 File Offset: 0x001E5724
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StreamingNoRowsMemberInstance;
		}
	}
}
