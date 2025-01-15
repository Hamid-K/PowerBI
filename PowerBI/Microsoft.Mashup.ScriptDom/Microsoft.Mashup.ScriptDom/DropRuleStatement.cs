using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002F5 RID: 757
	[Serializable]
	internal class DropRuleStatement : DropObjectsStatement
	{
		// Token: 0x060029A3 RID: 10659 RVA: 0x001675F1 File Offset: 0x001657F1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x001675FD File Offset: 0x001657FD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
