using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000423 RID: 1059
	[Serializable]
	internal class BackupServiceMasterKeyStatement : BackupRestoreMasterKeyStatementBase
	{
		// Token: 0x0600311A RID: 12570 RVA: 0x0016EF58 File Offset: 0x0016D158
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x0016EF64 File Offset: 0x0016D164
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
