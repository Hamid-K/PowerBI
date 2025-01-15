using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200040F RID: 1039
	[Serializable]
	internal class AddSignatureStatement : SignatureStatementBase
	{
		// Token: 0x06003085 RID: 12421 RVA: 0x0016E561 File Offset: 0x0016C761
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x0016E56D File Offset: 0x0016C76D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
