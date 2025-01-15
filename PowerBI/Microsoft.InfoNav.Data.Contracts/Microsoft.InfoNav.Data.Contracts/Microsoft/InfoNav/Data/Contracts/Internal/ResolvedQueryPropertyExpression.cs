using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200024D RID: 589
	[ImmutableObject(true)]
	public abstract class ResolvedQueryPropertyExpression : ResolvedQueryExpression
	{
		// Token: 0x060011C5 RID: 4549 RVA: 0x0001FB8E File Offset: 0x0001DD8E
		protected ResolvedQueryPropertyExpression(ResolvedQueryExpression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060011C6 RID: 4550
		public abstract IConceptualProperty Property { get; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0001FB9D File Offset: 0x0001DD9D
		public ResolvedQueryExpression Expression { get; }
	}
}
