using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003A3 RID: 931
	[Serializable]
	internal class AlterServiceStatement : AlterCreateServiceStatementBase
	{
		// Token: 0x06002E14 RID: 11796 RVA: 0x0016BE27 File Offset: 0x0016A027
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x0016BE33 File Offset: 0x0016A033
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
