using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000226 RID: 550
	[Serializable]
	internal class BreakStatement : TSqlStatement
	{
		// Token: 0x06002500 RID: 9472 RVA: 0x001626D6 File Offset: 0x001608D6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x001626E2 File Offset: 0x001608E2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
