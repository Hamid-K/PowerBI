using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200022A RID: 554
	[Serializable]
	internal class SaveTransactionStatement : TransactionStatement
	{
		// Token: 0x06002510 RID: 9488 RVA: 0x00162787 File Offset: 0x00160987
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00162793 File Offset: 0x00160993
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
