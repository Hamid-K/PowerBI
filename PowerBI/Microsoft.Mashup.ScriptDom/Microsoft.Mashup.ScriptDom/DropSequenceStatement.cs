using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000272 RID: 626
	[Serializable]
	internal class DropSequenceStatement : DropObjectsStatement
	{
		// Token: 0x060026BF RID: 9919 RVA: 0x0016452D File Offset: 0x0016272D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x00164539 File Offset: 0x00162739
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
