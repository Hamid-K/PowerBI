using System;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006BF RID: 1727
	public sealed class DbGroupAggregate : DbAggregate
	{
		// Token: 0x060050C9 RID: 20681 RVA: 0x00121F4A File Offset: 0x0012014A
		internal DbGroupAggregate(TypeUsage resultType, DbExpressionList arguments)
			: base(resultType, arguments)
		{
		}
	}
}
