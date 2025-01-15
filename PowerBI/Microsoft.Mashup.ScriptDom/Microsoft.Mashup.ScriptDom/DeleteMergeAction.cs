using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000436 RID: 1078
	[Serializable]
	internal class DeleteMergeAction : MergeAction
	{
		// Token: 0x0600317A RID: 12666 RVA: 0x0016F489 File Offset: 0x0016D689
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x0016F495 File Offset: 0x0016D695
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
