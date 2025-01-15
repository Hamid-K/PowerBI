using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	internal class MaxLiteral : Literal
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00006769 File Offset: 0x00004969
		public override LiteralType LiteralType
		{
			get
			{
				return LiteralType.Max;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000676C File Offset: 0x0000496C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006778 File Offset: 0x00004978
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
