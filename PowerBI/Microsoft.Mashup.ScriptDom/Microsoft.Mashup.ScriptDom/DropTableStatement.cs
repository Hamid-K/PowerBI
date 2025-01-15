using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002F0 RID: 752
	[Serializable]
	internal class DropTableStatement : DropObjectsStatement
	{
		// Token: 0x06002994 RID: 10644 RVA: 0x00167560 File Offset: 0x00165760
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0016756C File Offset: 0x0016576C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
