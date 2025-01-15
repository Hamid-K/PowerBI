using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001BE RID: 446
	[Serializable]
	internal class AlterTriggerStatement : TriggerStatementBody
	{
		// Token: 0x06002286 RID: 8838 RVA: 0x0015F786 File Offset: 0x0015D986
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x0015F792 File Offset: 0x0015D992
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
