using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200040D RID: 1037
	[Serializable]
	internal class DropServiceStatement : DropUnownedObjectStatement
	{
		// Token: 0x06003079 RID: 12409 RVA: 0x0016E49C File Offset: 0x0016C69C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x0016E4A8 File Offset: 0x0016C6A8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
