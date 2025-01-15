using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000410 RID: 1040
	[Serializable]
	internal class DropSignatureStatement : SignatureStatementBase
	{
		// Token: 0x06003088 RID: 12424 RVA: 0x0016E57E File Offset: 0x0016C77E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x0016E58A File Offset: 0x0016C78A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
