using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000446 RID: 1094
	[Serializable]
	internal class CreateServerAuditStatement : ServerAuditStatement
	{
		// Token: 0x060031C6 RID: 12742 RVA: 0x0016F965 File Offset: 0x0016DB65
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x0016F971 File Offset: 0x0016DB71
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
