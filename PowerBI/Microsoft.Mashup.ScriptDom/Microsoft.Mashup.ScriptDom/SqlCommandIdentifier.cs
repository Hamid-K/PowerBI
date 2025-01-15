using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000258 RID: 600
	[Serializable]
	internal class SqlCommandIdentifier : Identifier
	{
		// Token: 0x06002639 RID: 9785 RVA: 0x00163D29 File Offset: 0x00161F29
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00163D35 File Offset: 0x00161F35
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
