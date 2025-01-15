using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200049B RID: 1179
	[Serializable]
	internal class DropAvailabilityGroupStatement : DropUnownedObjectStatement
	{
		// Token: 0x06003398 RID: 13208 RVA: 0x00171507 File Offset: 0x0016F707
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x00171513 File Offset: 0x0016F713
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
