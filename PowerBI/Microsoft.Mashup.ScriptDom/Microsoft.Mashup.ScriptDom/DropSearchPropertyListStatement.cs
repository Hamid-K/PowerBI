using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003F6 RID: 1014
	[Serializable]
	internal class DropSearchPropertyListStatement : DropUnownedObjectStatement
	{
		// Token: 0x06003003 RID: 12291 RVA: 0x0016DE11 File Offset: 0x0016C011
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x0016DE1D File Offset: 0x0016C01D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
