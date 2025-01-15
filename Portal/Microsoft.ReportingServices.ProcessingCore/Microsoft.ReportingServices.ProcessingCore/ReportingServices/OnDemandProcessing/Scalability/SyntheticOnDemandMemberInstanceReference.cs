using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008AD RID: 2221
	internal class SyntheticOnDemandMemberInstanceReference : SyntheticOnDemandScopeInstanceReference, IOnDemandMemberInstanceReference, IOnDemandMemberOwnerInstanceReference, IReference<IOnDemandScopeInstance>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IReference<IOnDemandMemberOwnerInstance>, IReference<IOnDemandMemberInstance>
	{
		// Token: 0x06007951 RID: 31057 RVA: 0x001F37C8 File Offset: 0x001F19C8
		public SyntheticOnDemandMemberInstanceReference(IOnDemandMemberInstance member)
			: base(member)
		{
		}

		// Token: 0x06007952 RID: 31058 RVA: 0x001F37D1 File Offset: 0x001F19D1
		IOnDemandMemberOwnerInstance IReference<IOnDemandMemberOwnerInstance>.Value()
		{
			return (IOnDemandMemberOwnerInstance)this.Value();
		}

		// Token: 0x06007953 RID: 31059 RVA: 0x001F37DE File Offset: 0x001F19DE
		IOnDemandMemberInstance IReference<IOnDemandMemberInstance>.Value()
		{
			return (IOnDemandMemberInstance)this.Value();
		}
	}
}
