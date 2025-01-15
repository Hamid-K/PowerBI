using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000228 RID: 552
	[Serializable]
	internal class CommitTransactionStatement : TransactionStatement
	{
		// Token: 0x0600250A RID: 9482 RVA: 0x0016274D File Offset: 0x0016094D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00162759 File Offset: 0x00160959
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
