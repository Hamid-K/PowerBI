using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000480 RID: 1152
	[Serializable]
	internal class DropEventSessionStatement : DropUnownedObjectStatement
	{
		// Token: 0x06003308 RID: 13064 RVA: 0x00170C3A File Offset: 0x0016EE3A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x00170C46 File Offset: 0x0016EE46
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
