using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002DD RID: 733
	[Serializable]
	internal class CloseCursorStatement : CursorStatement
	{
		// Token: 0x06002934 RID: 10548 RVA: 0x00166F19 File Offset: 0x00165119
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x00166F25 File Offset: 0x00165125
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
