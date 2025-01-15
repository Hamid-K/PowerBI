using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002F4 RID: 756
	[Serializable]
	internal class DropDefaultStatement : DropObjectsStatement
	{
		// Token: 0x060029A0 RID: 10656 RVA: 0x001675D4 File Offset: 0x001657D4
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x001675E0 File Offset: 0x001657E0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
