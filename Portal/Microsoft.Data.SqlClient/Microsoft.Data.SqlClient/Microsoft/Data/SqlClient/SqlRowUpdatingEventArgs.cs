using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000097 RID: 151
	public sealed class SqlRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x06000C40 RID: 3136 RVA: 0x00024EC7 File Offset: 0x000230C7
		public SqlRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00024ED4 File Offset: 0x000230D4
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x00024EE1 File Offset: 0x000230E1
		public new SqlCommand Command
		{
			get
			{
				return base.Command as SqlCommand;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00024EEA File Offset: 0x000230EA
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x00024EF2 File Offset: 0x000230F2
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = value as SqlCommand;
			}
		}
	}
}
