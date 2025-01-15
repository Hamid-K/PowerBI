using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003E6 RID: 998
	[Serializable]
	internal class DropCredentialStatement : DropUnownedObjectStatement
	{
		// Token: 0x06002F9F RID: 12191 RVA: 0x0016D7F0 File Offset: 0x0016B9F0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x0016D7FC File Offset: 0x0016B9FC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
