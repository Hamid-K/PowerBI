using System;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000830 RID: 2096
	[PersistedWithinRequestOnly]
	[SkipStaticValidation]
	internal class StreamingNoRowsDataRegionInstance : StreamingNoRowsScopeInstanceBase, IOnDemandMemberOwnerInstance, IOnDemandScopeInstance, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007585 RID: 30085 RVA: 0x001E74F7 File Offset: 0x001E56F7
		public StreamingNoRowsDataRegionInstance(OnDemandProcessingContext odpContext, IRIFReportDataScope dataRegion)
			: base(odpContext, dataRegion)
		{
		}

		// Token: 0x06007586 RID: 30086 RVA: 0x001E7501 File Offset: 0x001E5701
		public IOnDemandMemberInstanceReference GetFirstMemberInstance(ReportHierarchyNode rifMember)
		{
			return null;
		}

		// Token: 0x06007587 RID: 30087 RVA: 0x001E7504 File Offset: 0x001E5704
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StreamingNoRowsDataRegionInstance;
		}
	}
}
