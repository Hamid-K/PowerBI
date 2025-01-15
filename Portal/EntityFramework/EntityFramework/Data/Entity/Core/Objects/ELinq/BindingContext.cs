using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x0200045C RID: 1116
	internal sealed class BindingContext
	{
		// Token: 0x060036BF RID: 14015 RVA: 0x000B0BB8 File Offset: 0x000AEDB8
		internal BindingContext()
		{
			this._scopes = new Stack<Binding>();
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000B0BCB File Offset: 0x000AEDCB
		internal void PushBindingScope(Binding binding)
		{
			this._scopes.Push(binding);
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000B0BD9 File Offset: 0x000AEDD9
		internal void PopBindingScope()
		{
			this._scopes.Pop();
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000B0BE8 File Offset: 0x000AEDE8
		internal bool TryGetBoundExpression(Expression linqExpression, out DbExpression cqtExpression)
		{
			cqtExpression = (from binding in this._scopes
				where binding.LinqExpression == linqExpression
				select binding.CqtExpression).FirstOrDefault<DbExpression>();
			return cqtExpression != null;
		}

		// Token: 0x040011C4 RID: 4548
		private readonly Stack<Binding> _scopes;
	}
}
