using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000408 RID: 1032
	[Serializable]
	internal class DropEndpointStatement : DropUnownedObjectStatement
	{
		// Token: 0x06003068 RID: 12392 RVA: 0x0016E3DF File Offset: 0x0016C5DF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x0016E3EB File Offset: 0x0016C5EB
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
