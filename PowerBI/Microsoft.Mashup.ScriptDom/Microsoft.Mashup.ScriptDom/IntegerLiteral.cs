using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	internal class IntegerLiteral : Literal
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00006635 File Offset: 0x00004835
		public override LiteralType LiteralType
		{
			get
			{
				return LiteralType.Integer;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006638 File Offset: 0x00004838
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006644 File Offset: 0x00004844
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
