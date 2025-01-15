using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200040C RID: 1036
	[Serializable]
	internal class DropRouteStatement : DropUnownedObjectStatement
	{
		// Token: 0x06003076 RID: 12406 RVA: 0x0016E47F File Offset: 0x0016C67F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x0016E48B File Offset: 0x0016C68B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
