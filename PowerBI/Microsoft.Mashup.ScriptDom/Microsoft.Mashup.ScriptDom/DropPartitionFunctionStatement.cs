using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003D6 RID: 982
	[Serializable]
	internal class DropPartitionFunctionStatement : DropUnownedObjectStatement
	{
		// Token: 0x06002F65 RID: 12133 RVA: 0x0016D595 File Offset: 0x0016B795
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x0016D5A1 File Offset: 0x0016B7A1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
