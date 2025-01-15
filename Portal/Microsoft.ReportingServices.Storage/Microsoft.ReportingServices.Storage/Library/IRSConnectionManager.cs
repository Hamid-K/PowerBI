using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200003E RID: 62
	internal interface IRSConnectionManager
	{
		// Token: 0x060001BB RID: 443
		void AbortTransaction();

		// Token: 0x060001BC RID: 444
		void BeginTransaction();

		// Token: 0x060001BD RID: 445
		void BeginTransaction(IsolationLevel isoLevel);

		// Token: 0x060001BE RID: 446
		void ChangeDatabase(string database);

		// Token: 0x060001BF RID: 447
		void CommitTransaction();

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001C0 RID: 448
		SqlConnection Connection { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001C1 RID: 449
		// (set) Token: 0x060001C2 RID: 450
		ConnectionTransactionType ConnectionTransactionType { get; set; }

		// Token: 0x060001C3 RID: 451
		void DisconnectStorage();

		// Token: 0x060001C4 RID: 452
		void EnsureDBCmptLevel();

		// Token: 0x060001C5 RID: 453
		IDisposable EnterThreadSafeContext();

		// Token: 0x060001C6 RID: 454
		string EscapeAndBracketDBName(string dbName);

		// Token: 0x060001C7 RID: 455
		IsolationLevel GetIsolationLevel();

		// Token: 0x060001C8 RID: 456
		ConnectionTransactionType GetTransactionType();

		// Token: 0x060001C9 RID: 457
		SqlConnection GetUnverifiedConnection();

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001CA RID: 458
		bool IsBatchScoped { get; }

		// Token: 0x060001CB RID: 459
		bool IsSupportedEditionForSharePoint();

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001CC RID: 460
		// (set) Token: 0x060001CD RID: 461
		bool SingleCommitEnabled { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001CE RID: 462
		SqlTransaction Transaction { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001CF RID: 463
		SqlConnection UnverifiedConnection { get; }

		// Token: 0x060001D0 RID: 464
		void VerifyConnection(bool initializeEncryption = true);

		// Token: 0x060001D1 RID: 465
		void VerifyConnectionAndDbVersion();

		// Token: 0x060001D2 RID: 466
		void WillDisconnectStorage();
	}
}
