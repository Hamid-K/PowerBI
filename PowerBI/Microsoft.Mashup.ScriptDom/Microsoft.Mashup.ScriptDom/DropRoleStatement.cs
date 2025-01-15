using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003DF RID: 991
	[Serializable]
	internal class DropRoleStatement : DropUnownedObjectStatement
	{
		// Token: 0x06002F84 RID: 12164 RVA: 0x0016D6D7 File Offset: 0x0016B8D7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x0016D6E3 File Offset: 0x0016B8E3
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
