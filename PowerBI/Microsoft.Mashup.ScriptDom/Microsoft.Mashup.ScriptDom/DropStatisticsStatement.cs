using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002EF RID: 751
	[Serializable]
	internal class DropStatisticsStatement : DropChildObjectsStatement
	{
		// Token: 0x06002991 RID: 10641 RVA: 0x00167543 File Offset: 0x00165743
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0016754F File Offset: 0x0016574F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
