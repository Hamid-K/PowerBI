using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	internal class NullLiteral : Literal
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00006729 File Offset: 0x00004929
		public override LiteralType LiteralType
		{
			get
			{
				return LiteralType.Null;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000672C File Offset: 0x0000492C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006738 File Offset: 0x00004938
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
