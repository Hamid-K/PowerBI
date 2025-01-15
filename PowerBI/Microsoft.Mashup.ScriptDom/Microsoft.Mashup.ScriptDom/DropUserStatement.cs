using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003E1 RID: 993
	[Serializable]
	internal class DropUserStatement : DropUnownedObjectStatement
	{
		// Token: 0x06002F8C RID: 12172 RVA: 0x0016D73D File Offset: 0x0016B93D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x0016D749 File Offset: 0x0016B949
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
