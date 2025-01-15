using System;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000505 RID: 1285
	internal interface IDomainScopeMemberCreator
	{
		// Token: 0x0600433C RID: 17212
		void CreateDomainScopeMember(ReportHierarchyNode parentNode, Grouping grouping, AutomaticSubtotalContext context);
	}
}
