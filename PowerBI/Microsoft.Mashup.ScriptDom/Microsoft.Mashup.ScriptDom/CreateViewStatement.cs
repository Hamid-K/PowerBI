using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001B8 RID: 440
	[Serializable]
	internal class CreateViewStatement : ViewStatementBody
	{
		// Token: 0x06002259 RID: 8793 RVA: 0x0015F4C3 File Offset: 0x0015D6C3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0015F4CF File Offset: 0x0015D6CF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
