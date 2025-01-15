using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003D8 RID: 984
	[Serializable]
	internal class DropSynonymStatement : DropObjectsStatement
	{
		// Token: 0x06002F6B RID: 12139 RVA: 0x0016D5CF File Offset: 0x0016B7CF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x0016D5DB File Offset: 0x0016B7DB
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
