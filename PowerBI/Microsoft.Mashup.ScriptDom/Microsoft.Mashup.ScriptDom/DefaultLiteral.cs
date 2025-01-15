using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	internal class DefaultLiteral : Literal
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00006749 File Offset: 0x00004949
		public override LiteralType LiteralType
		{
			get
			{
				return LiteralType.Default;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000674C File Offset: 0x0000494C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006758 File Offset: 0x00004958
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
