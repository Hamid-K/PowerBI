using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003DC RID: 988
	[Serializable]
	internal class DropFullTextCatalogStatement : DropUnownedObjectStatement
	{
		// Token: 0x06002F79 RID: 12153 RVA: 0x0016D654 File Offset: 0x0016B854
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x0016D660 File Offset: 0x0016B860
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
