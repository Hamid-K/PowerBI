using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000203 RID: 515
	[Serializable]
	internal class CoalesceExpression : PrimaryExpression
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x001612D2 File Offset: 0x0015F4D2
		public IList<ScalarExpression> Expressions
		{
			get
			{
				return this._expressions;
			}
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x001612DA File Offset: 0x0015F4DA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x001612E8 File Offset: 0x0015F4E8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Expressions.Count;
			while (i < count)
			{
				this.Expressions[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A98 RID: 6808
		private List<ScalarExpression> _expressions = new List<ScalarExpression>();
	}
}
