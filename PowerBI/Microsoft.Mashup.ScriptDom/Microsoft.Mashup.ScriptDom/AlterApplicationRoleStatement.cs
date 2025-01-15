using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002C1 RID: 705
	[Serializable]
	internal class AlterApplicationRoleStatement : ApplicationRoleStatement
	{
		// Token: 0x0600289E RID: 10398 RVA: 0x0016653A File Offset: 0x0016473A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x00166546 File Offset: 0x00164746
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
