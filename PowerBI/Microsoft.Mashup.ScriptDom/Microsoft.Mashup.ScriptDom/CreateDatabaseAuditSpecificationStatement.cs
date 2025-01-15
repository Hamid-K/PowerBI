using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200043F RID: 1087
	[Serializable]
	internal class CreateDatabaseAuditSpecificationStatement : AuditSpecificationStatement
	{
		// Token: 0x060031AB RID: 12715 RVA: 0x0016F7DA File Offset: 0x0016D9DA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x0016F7E6 File Offset: 0x0016D9E6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
