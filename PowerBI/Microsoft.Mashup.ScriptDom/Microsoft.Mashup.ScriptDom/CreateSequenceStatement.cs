using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200026F RID: 623
	[Serializable]
	internal class CreateSequenceStatement : SequenceStatement
	{
		// Token: 0x060026B6 RID: 9910 RVA: 0x0016449A File Offset: 0x0016269A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x001644A6 File Offset: 0x001626A6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
