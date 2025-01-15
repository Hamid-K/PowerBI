using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001F2 RID: 498
	[Serializable]
	internal class ReadOnlyForClause : ForClause
	{
		// Token: 0x060023A3 RID: 9123 RVA: 0x00160C7B File Offset: 0x0015EE7B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x00160C87 File Offset: 0x0015EE87
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
