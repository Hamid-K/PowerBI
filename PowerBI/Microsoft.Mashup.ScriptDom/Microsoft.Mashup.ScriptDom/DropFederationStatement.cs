using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200049E RID: 1182
	[Serializable]
	internal class DropFederationStatement : DropUnownedObjectStatement
	{
		// Token: 0x060033AF RID: 13231 RVA: 0x00171690 File Offset: 0x0016F890
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x0017169C File Offset: 0x0016F89C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
