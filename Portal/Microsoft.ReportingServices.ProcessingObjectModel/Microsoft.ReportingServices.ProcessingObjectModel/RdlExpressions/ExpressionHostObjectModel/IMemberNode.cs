using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000065 RID: 101
	public interface IMemberNode
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600020D RID: 525
		GroupExprHost GroupHost { get; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600020E RID: 526
		SortExprHost SortHost { get; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600020F RID: 527
		IList<DataValueExprHost> CustomPropertyHostsRemotable { get; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000210 RID: 528
		IList<IMemberNode> MemberTreeHostsRemotable { get; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000211 RID: 529
		IList<JoinConditionExprHost> JoinConditionExprHostsRemotable { get; }
	}
}
