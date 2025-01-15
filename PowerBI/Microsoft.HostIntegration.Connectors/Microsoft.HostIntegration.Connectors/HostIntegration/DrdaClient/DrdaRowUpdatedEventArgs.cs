using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009F2 RID: 2546
	public sealed class DrdaRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		// Token: 0x06004F70 RID: 20336 RVA: 0x0013EE1F File Offset: 0x0013D01F
		public DrdaRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06004F71 RID: 20337 RVA: 0x0013EE2C File Offset: 0x0013D02C
		public new DrdaCommand Command
		{
			get
			{
				return (DrdaCommand)base.Command;
			}
		}
	}
}
