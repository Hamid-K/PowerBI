using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	internal class RealLiteral : Literal
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00006676 File Offset: 0x00004876
		public override LiteralType LiteralType
		{
			get
			{
				return LiteralType.Real;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006679 File Offset: 0x00004879
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006685 File Offset: 0x00004885
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
