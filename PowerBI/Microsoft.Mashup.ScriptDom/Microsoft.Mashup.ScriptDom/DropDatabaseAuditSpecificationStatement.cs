using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000441 RID: 1089
	[Serializable]
	internal class DropDatabaseAuditSpecificationStatement : DropUnownedObjectStatement
	{
		// Token: 0x060031B1 RID: 12721 RVA: 0x0016F814 File Offset: 0x0016DA14
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x0016F820 File Offset: 0x0016DA20
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
