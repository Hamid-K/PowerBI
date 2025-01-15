using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000409 RID: 1033
	[Serializable]
	internal class DropMessageTypeStatement : DropUnownedObjectStatement
	{
		// Token: 0x0600306B RID: 12395 RVA: 0x0016E3FC File Offset: 0x0016C5FC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x0016E408 File Offset: 0x0016C608
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
