using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003DB RID: 987
	[Serializable]
	internal class DropApplicationRoleStatement : DropUnownedObjectStatement
	{
		// Token: 0x06002F76 RID: 12150 RVA: 0x0016D637 File Offset: 0x0016B837
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x0016D643 File Offset: 0x0016B843
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
