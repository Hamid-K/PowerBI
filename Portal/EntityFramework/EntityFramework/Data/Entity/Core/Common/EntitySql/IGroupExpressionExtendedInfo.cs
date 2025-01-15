using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000658 RID: 1624
	internal interface IGroupExpressionExtendedInfo
	{
		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06004DF3 RID: 19955
		DbExpression GroupVarBasedExpression { get; }

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06004DF4 RID: 19956
		DbExpression GroupAggBasedExpression { get; }
	}
}
