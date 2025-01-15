using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x0200045B RID: 1115
	internal sealed class Binding
	{
		// Token: 0x060036BE RID: 14014 RVA: 0x000B0BA2 File Offset: 0x000AEDA2
		internal Binding(Expression linqExpression, DbExpression cqtExpression)
		{
			this.LinqExpression = linqExpression;
			this.CqtExpression = cqtExpression;
		}

		// Token: 0x040011C2 RID: 4546
		internal readonly Expression LinqExpression;

		// Token: 0x040011C3 RID: 4547
		internal readonly DbExpression CqtExpression;
	}
}
