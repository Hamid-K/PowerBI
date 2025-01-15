using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000473 RID: 1139
	[Serializable]
	internal class CreateEventSessionStatement : EventSessionStatement
	{
		// Token: 0x060032B9 RID: 12985 RVA: 0x0017072E File Offset: 0x0016E92E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x0017073A File Offset: 0x0016E93A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
