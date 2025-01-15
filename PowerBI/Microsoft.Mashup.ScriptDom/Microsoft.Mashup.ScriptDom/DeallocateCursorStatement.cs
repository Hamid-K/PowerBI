using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002E3 RID: 739
	[Serializable]
	internal class DeallocateCursorStatement : CursorStatement
	{
		// Token: 0x06002956 RID: 10582 RVA: 0x001670F1 File Offset: 0x001652F1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x001670FD File Offset: 0x001652FD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
