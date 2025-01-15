using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002D0 RID: 720
	[Serializable]
	internal class AlterUserStatement : UserStatement
	{
		// Token: 0x060028DE RID: 10462 RVA: 0x00166913 File Offset: 0x00164B13
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x0016691F File Offset: 0x00164B1F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
