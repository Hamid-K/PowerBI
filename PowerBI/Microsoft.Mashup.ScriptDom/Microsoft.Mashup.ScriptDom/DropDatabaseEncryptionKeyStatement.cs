using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000457 RID: 1111
	[Serializable]
	internal class DropDatabaseEncryptionKeyStatement : TSqlStatement
	{
		// Token: 0x0600321D RID: 12829 RVA: 0x0016FD78 File Offset: 0x0016DF78
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x0016FD84 File Offset: 0x0016DF84
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
