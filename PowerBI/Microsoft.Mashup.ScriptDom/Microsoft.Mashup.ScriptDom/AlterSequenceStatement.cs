using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000270 RID: 624
	[Serializable]
	internal class AlterSequenceStatement : SequenceStatement
	{
		// Token: 0x060026B9 RID: 9913 RVA: 0x001644B7 File Offset: 0x001626B7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x001644C3 File Offset: 0x001626C3
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
