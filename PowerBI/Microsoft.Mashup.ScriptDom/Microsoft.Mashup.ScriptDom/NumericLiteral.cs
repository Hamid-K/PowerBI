using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	internal class NumericLiteral : Literal
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00006655 File Offset: 0x00004855
		public override LiteralType LiteralType
		{
			get
			{
				return LiteralType.Numeric;
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006659 File Offset: 0x00004859
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006665 File Offset: 0x00004865
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
