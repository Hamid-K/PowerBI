using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200040B RID: 1035
	[Serializable]
	internal class DropRemoteServiceBindingStatement : DropUnownedObjectStatement
	{
		// Token: 0x06003073 RID: 12403 RVA: 0x0016E462 File Offset: 0x0016C662
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x0016E46E File Offset: 0x0016C66E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
