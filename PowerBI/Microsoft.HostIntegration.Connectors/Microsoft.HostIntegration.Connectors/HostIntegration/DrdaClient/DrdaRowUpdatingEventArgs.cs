using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009F3 RID: 2547
	public sealed class DrdaRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x06004F72 RID: 20338 RVA: 0x0013EE39 File Offset: 0x0013D039
		public DrdaRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x06004F73 RID: 20339 RVA: 0x0013EE46 File Offset: 0x0013D046
		// (set) Token: 0x06004F74 RID: 20340 RVA: 0x0013EE53 File Offset: 0x0013D053
		public new DrdaCommand Command
		{
			get
			{
				return base.Command as DrdaCommand;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06004F75 RID: 20341 RVA: 0x0013EE5C File Offset: 0x0013D05C
		// (set) Token: 0x06004F76 RID: 20342 RVA: 0x0013EE64 File Offset: 0x0013D064
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = value as DrdaCommand;
			}
		}
	}
}
