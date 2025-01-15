using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006ED RID: 1773
	internal sealed class DbExpressionList : ReadOnlyCollection<DbExpression>
	{
		// Token: 0x06005287 RID: 21127 RVA: 0x0012844A File Offset: 0x0012664A
		internal DbExpressionList(IList<DbExpression> elements)
			: base(elements)
		{
		}
	}
}
