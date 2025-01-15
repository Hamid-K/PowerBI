using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200045D RID: 1117
	[Serializable]
	internal class DropResourcePoolStatement : DropUnownedObjectStatement
	{
		// Token: 0x0600323F RID: 12863 RVA: 0x0016FF9F File Offset: 0x0016E19F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x0016FFAB File Offset: 0x0016E1AB
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
