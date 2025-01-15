using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002CC RID: 716
	[Serializable]
	internal class DropServerRoleStatement : DropUnownedObjectStatement
	{
		// Token: 0x060028CA RID: 10442 RVA: 0x00166789 File Offset: 0x00164989
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x00166795 File Offset: 0x00164995
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
