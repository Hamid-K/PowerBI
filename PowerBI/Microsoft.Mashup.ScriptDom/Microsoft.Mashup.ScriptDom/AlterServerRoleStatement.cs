using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002CA RID: 714
	[Serializable]
	internal class AlterServerRoleStatement : AlterRoleStatement
	{
		// Token: 0x060028C3 RID: 10435 RVA: 0x0016672F File Offset: 0x0016492F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x0016673B File Offset: 0x0016493B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
