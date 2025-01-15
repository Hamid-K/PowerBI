using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001BF RID: 447
	[Serializable]
	internal class CreateTriggerStatement : TriggerStatementBody
	{
		// Token: 0x06002289 RID: 8841 RVA: 0x0015F7A3 File Offset: 0x0015D9A3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0015F7AF File Offset: 0x0015D9AF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
