using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000442 RID: 1090
	[Serializable]
	internal class CreateServerAuditSpecificationStatement : AuditSpecificationStatement
	{
		// Token: 0x060031B4 RID: 12724 RVA: 0x0016F831 File Offset: 0x0016DA31
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x0016F83D File Offset: 0x0016DA3D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
