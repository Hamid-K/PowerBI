using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003D9 RID: 985
	[Serializable]
	internal class DropAggregateStatement : DropObjectsStatement
	{
		// Token: 0x06002F6E RID: 12142 RVA: 0x0016D5EC File Offset: 0x0016B7EC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x0016D5F8 File Offset: 0x0016B7F8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
