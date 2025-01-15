using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002C9 RID: 713
	[Serializable]
	internal class CreateServerRoleStatement : CreateRoleStatement
	{
		// Token: 0x060028C0 RID: 10432 RVA: 0x00166712 File Offset: 0x00164912
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x0016671E File Offset: 0x0016491E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
