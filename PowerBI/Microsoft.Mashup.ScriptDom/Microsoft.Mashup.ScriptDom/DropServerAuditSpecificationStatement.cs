using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000444 RID: 1092
	[Serializable]
	internal class DropServerAuditSpecificationStatement : DropUnownedObjectStatement
	{
		// Token: 0x060031BA RID: 12730 RVA: 0x0016F86B File Offset: 0x0016DA6B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x0016F877 File Offset: 0x0016DA77
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
