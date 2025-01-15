using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	internal class MoneyLiteral : Literal
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00006696 File Offset: 0x00004896
		public override LiteralType LiteralType
		{
			get
			{
				return LiteralType.Money;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006699 File Offset: 0x00004899
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000066A5 File Offset: 0x000048A5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
