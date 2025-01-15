using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000407 RID: 1031
	[Serializable]
	internal class DropContractStatement : DropUnownedObjectStatement
	{
		// Token: 0x06003065 RID: 12389 RVA: 0x0016E3C2 File Offset: 0x0016C5C2
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x0016E3CE File Offset: 0x0016C5CE
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
