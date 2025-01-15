using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	internal class AlterViewStatement : ViewStatementBody
	{
		// Token: 0x06002256 RID: 8790 RVA: 0x0015F4A6 File Offset: 0x0015D6A6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0015F4B2 File Offset: 0x0015D6B2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
