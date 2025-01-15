using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000469 RID: 1129
	[Serializable]
	internal class DropBrokerPriorityStatement : DropUnownedObjectStatement
	{
		// Token: 0x06003274 RID: 12916 RVA: 0x0017028A File Offset: 0x0016E48A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x00170296 File Offset: 0x0016E496
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
