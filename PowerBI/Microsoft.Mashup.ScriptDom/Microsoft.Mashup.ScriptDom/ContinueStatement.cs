using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200022B RID: 555
	[Serializable]
	internal class ContinueStatement : TSqlStatement
	{
		// Token: 0x06002513 RID: 9491 RVA: 0x001627A4 File Offset: 0x001609A4
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x001627B0 File Offset: 0x001609B0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
