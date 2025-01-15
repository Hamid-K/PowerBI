using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000440 RID: 1088
	[Serializable]
	internal class AlterDatabaseAuditSpecificationStatement : AuditSpecificationStatement
	{
		// Token: 0x060031AE RID: 12718 RVA: 0x0016F7F7 File Offset: 0x0016D9F7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x0016F803 File Offset: 0x0016DA03
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
