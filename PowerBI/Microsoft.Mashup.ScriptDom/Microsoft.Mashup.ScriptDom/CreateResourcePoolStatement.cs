using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200045B RID: 1115
	[Serializable]
	internal class CreateResourcePoolStatement : ResourcePoolStatement
	{
		// Token: 0x06003239 RID: 12857 RVA: 0x0016FF65 File Offset: 0x0016E165
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x0016FF71 File Offset: 0x0016E171
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
