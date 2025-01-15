using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002F3 RID: 755
	[Serializable]
	internal class DropViewStatement : DropObjectsStatement
	{
		// Token: 0x0600299D RID: 10653 RVA: 0x001675B7 File Offset: 0x001657B7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x001675C3 File Offset: 0x001657C3
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
