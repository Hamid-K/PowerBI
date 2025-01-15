using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200034C RID: 844
	[Serializable]
	internal class BackupTransactionLogStatement : BackupStatement
	{
		// Token: 0x06002BE7 RID: 11239 RVA: 0x001698ED File Offset: 0x00167AED
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x001698F9 File Offset: 0x00167AF9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
