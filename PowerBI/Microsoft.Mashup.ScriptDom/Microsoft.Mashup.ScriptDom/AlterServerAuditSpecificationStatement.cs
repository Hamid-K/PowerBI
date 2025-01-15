using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000443 RID: 1091
	[Serializable]
	internal class AlterServerAuditSpecificationStatement : AuditSpecificationStatement
	{
		// Token: 0x060031B7 RID: 12727 RVA: 0x0016F84E File Offset: 0x0016DA4E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x0016F85A File Offset: 0x0016DA5A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
