using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003DE RID: 990
	[Serializable]
	internal class DropLoginStatement : DropUnownedObjectStatement
	{
		// Token: 0x06002F81 RID: 12161 RVA: 0x0016D6BA File Offset: 0x0016B8BA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x0016D6C6 File Offset: 0x0016B8C6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
