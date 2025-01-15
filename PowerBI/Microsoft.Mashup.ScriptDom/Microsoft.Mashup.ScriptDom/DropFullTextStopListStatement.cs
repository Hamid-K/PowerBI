using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200046D RID: 1133
	[Serializable]
	internal class DropFullTextStopListStatement : DropUnownedObjectStatement
	{
		// Token: 0x06003296 RID: 12950 RVA: 0x0017049C File Offset: 0x0016E69C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x001704A8 File Offset: 0x0016E6A8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
