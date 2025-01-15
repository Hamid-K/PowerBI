using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000095 RID: 149
	public sealed class SqlRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		// Token: 0x06000C3A RID: 3130 RVA: 0x00024EAD File Offset: 0x000230AD
		public SqlRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00024EBA File Offset: 0x000230BA
		public new SqlCommand Command
		{
			get
			{
				return (SqlCommand)base.Command;
			}
		}
	}
}
