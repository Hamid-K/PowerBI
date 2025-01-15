using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000229 RID: 553
	[Serializable]
	internal class RollbackTransactionStatement : TransactionStatement
	{
		// Token: 0x0600250D RID: 9485 RVA: 0x0016276A File Offset: 0x0016096A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x00162776 File Offset: 0x00160976
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
