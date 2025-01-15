using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000425 RID: 1061
	[Serializable]
	internal class BackupMasterKeyStatement : BackupRestoreMasterKeyStatementBase
	{
		// Token: 0x06003122 RID: 12578 RVA: 0x0016EFA3 File Offset: 0x0016D1A3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x0016EFAF File Offset: 0x0016D1AF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
