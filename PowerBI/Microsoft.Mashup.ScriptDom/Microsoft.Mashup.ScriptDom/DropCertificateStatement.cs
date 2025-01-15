using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003E5 RID: 997
	[Serializable]
	internal class DropCertificateStatement : DropUnownedObjectStatement
	{
		// Token: 0x06002F9C RID: 12188 RVA: 0x0016D7D3 File Offset: 0x0016B9D3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x0016D7DF File Offset: 0x0016B9DF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
