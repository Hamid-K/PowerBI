using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003D7 RID: 983
	[Serializable]
	internal class DropPartitionSchemeStatement : DropUnownedObjectStatement
	{
		// Token: 0x06002F68 RID: 12136 RVA: 0x0016D5B2 File Offset: 0x0016B7B2
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x0016D5BE File Offset: 0x0016B7BE
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
