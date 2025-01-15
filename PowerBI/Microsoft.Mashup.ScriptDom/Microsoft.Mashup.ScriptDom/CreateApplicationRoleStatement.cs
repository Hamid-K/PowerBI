using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002C0 RID: 704
	[Serializable]
	internal class CreateApplicationRoleStatement : ApplicationRoleStatement
	{
		// Token: 0x0600289B RID: 10395 RVA: 0x0016651D File Offset: 0x0016471D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x00166529 File Offset: 0x00164729
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
