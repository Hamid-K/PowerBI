using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002E2 RID: 738
	[Serializable]
	internal class CloseMasterKeyStatement : TSqlStatement
	{
		// Token: 0x06002953 RID: 10579 RVA: 0x001670D4 File Offset: 0x001652D4
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x001670E0 File Offset: 0x001652E0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
