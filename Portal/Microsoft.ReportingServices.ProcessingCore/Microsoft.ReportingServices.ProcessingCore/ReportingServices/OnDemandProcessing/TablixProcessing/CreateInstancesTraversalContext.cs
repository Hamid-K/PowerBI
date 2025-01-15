using System;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008D5 RID: 2261
	internal class CreateInstancesTraversalContext : ITraversalContext
	{
		// Token: 0x06007BAC RID: 31660 RVA: 0x001FC671 File Offset: 0x001FA871
		internal CreateInstancesTraversalContext(ScopeInstance parentInstance, IReference<RuntimeMemberObj>[] innerMembers, IReference<RuntimeDataTablixGroupLeafObj> innerGroupLeafRef)
		{
			this.m_parentInstance = parentInstance;
			this.m_innerMembers = innerMembers;
			this.m_innerGroupLeafRef = innerGroupLeafRef;
		}

		// Token: 0x1700288B RID: 10379
		// (get) Token: 0x06007BAD RID: 31661 RVA: 0x001FC68E File Offset: 0x001FA88E
		internal ScopeInstance ParentInstance
		{
			get
			{
				return this.m_parentInstance;
			}
		}

		// Token: 0x1700288C RID: 10380
		// (get) Token: 0x06007BAE RID: 31662 RVA: 0x001FC696 File Offset: 0x001FA896
		internal IReference<RuntimeMemberObj>[] InnerMembers
		{
			get
			{
				return this.m_innerMembers;
			}
		}

		// Token: 0x1700288D RID: 10381
		// (get) Token: 0x06007BAF RID: 31663 RVA: 0x001FC69E File Offset: 0x001FA89E
		internal IReference<RuntimeDataTablixGroupLeafObj> InnerGroupLeafRef
		{
			get
			{
				return this.m_innerGroupLeafRef;
			}
		}

		// Token: 0x04003D80 RID: 15744
		private ScopeInstance m_parentInstance;

		// Token: 0x04003D81 RID: 15745
		private IReference<RuntimeMemberObj>[] m_innerMembers;

		// Token: 0x04003D82 RID: 15746
		private IReference<RuntimeDataTablixGroupLeafObj> m_innerGroupLeafRef;
	}
}
