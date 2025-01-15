using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001F1 RID: 497
	[Serializable]
	internal class BrowseForClause : ForClause
	{
		// Token: 0x060023A0 RID: 9120 RVA: 0x00160C5E File Offset: 0x0015EE5E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x00160C6A File Offset: 0x0015EE6A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
