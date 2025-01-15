using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000470 RID: 1136
	[Serializable]
	internal class DropCryptographicProviderStatement : DropUnownedObjectStatement
	{
		// Token: 0x060032A9 RID: 12969 RVA: 0x001705B4 File Offset: 0x0016E7B4
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x001705C0 File Offset: 0x0016E7C0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
