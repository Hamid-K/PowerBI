using System;
using Microsoft.Data.SqlClient;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200003F RID: 63
	internal interface IRSStorage
	{
		// Token: 0x060001D3 RID: 467
		void AbortTransaction();

		// Token: 0x060001D4 RID: 468
		void Commit();

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001D5 RID: 469
		SqlConnection Connection { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D6 RID: 470
		// (set) Token: 0x060001D7 RID: 471
		ConnectionManager ConnectionManager { get; set; }

		// Token: 0x060001D8 RID: 472
		void Disconnect();

		// Token: 0x060001D9 RID: 473
		void DisconnectStorage();

		// Token: 0x060001DA RID: 474
		CancelableSqlCommand NewCancelableStandardSqlCommand(string storedProcedureName);

		// Token: 0x060001DB RID: 475
		InstrumentedSqlCommand NewStandardSqlCommand(string storedProcedureName, SqlConnection connection = null);

		// Token: 0x060001DC RID: 476
		InstrumentedSqlCommand NewStandardSqlCommandQuery(string query);

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001DD RID: 477
		int SqlCommandTimeout { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001DE RID: 478
		SqlTransaction Transaction { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001DF RID: 479
		SqlConnection UnverifiedConnection { get; }
	}
}
