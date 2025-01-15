using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003E2 RID: 994
	[Serializable]
	internal class DropMasterKeyStatement : TSqlStatement
	{
		// Token: 0x06002F8F RID: 12175 RVA: 0x0016D75A File Offset: 0x0016B95A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x0016D766 File Offset: 0x0016B966
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
