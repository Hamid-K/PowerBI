using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000448 RID: 1096
	[Serializable]
	internal class DropServerAuditStatement : DropUnownedObjectStatement
	{
		// Token: 0x060031D0 RID: 12752 RVA: 0x0016F9DC File Offset: 0x0016DBDC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x0016F9E8 File Offset: 0x0016DBE8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
