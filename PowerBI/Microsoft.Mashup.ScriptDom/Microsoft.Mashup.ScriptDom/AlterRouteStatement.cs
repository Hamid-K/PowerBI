using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002A1 RID: 673
	[Serializable]
	internal class AlterRouteStatement : RouteStatement
	{
		// Token: 0x060027D0 RID: 10192 RVA: 0x001656E5 File Offset: 0x001638E5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x001656F1 File Offset: 0x001638F1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
