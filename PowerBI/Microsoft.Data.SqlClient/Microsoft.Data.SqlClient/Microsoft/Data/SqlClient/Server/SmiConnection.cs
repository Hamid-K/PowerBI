using System;
using System.Data;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000147 RID: 327
	internal abstract class SmiConnection : IDisposable
	{
		// Token: 0x0600198B RID: 6539
		internal abstract string GetCurrentDatabase(SmiEventSink eventSink);

		// Token: 0x0600198C RID: 6540
		internal abstract void SetCurrentDatabase(string databaseName, SmiEventSink eventSink);

		// Token: 0x0600198D RID: 6541 RVA: 0x000605F2 File Offset: 0x0005E7F2
		public virtual void Dispose()
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000605F2 File Offset: 0x0005E7F2
		public virtual void Close(SmiEventSink eventSink)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600198F RID: 6543
		internal abstract void BeginTransaction(string name, IsolationLevel level, SmiEventSink eventSink);

		// Token: 0x06001990 RID: 6544
		internal abstract void CommitTransaction(long transactionId, SmiEventSink eventSink);

		// Token: 0x06001991 RID: 6545
		internal abstract void CreateTransactionSavePoint(long transactionId, string name, SmiEventSink eventSink);

		// Token: 0x06001992 RID: 6546
		internal abstract byte[] GetDTCAddress(SmiEventSink eventSink);

		// Token: 0x06001993 RID: 6547
		internal abstract void EnlistTransaction(byte[] token, SmiEventSink eventSink);

		// Token: 0x06001994 RID: 6548
		internal abstract byte[] PromoteTransaction(long transactionId, SmiEventSink eventSink);

		// Token: 0x06001995 RID: 6549
		internal abstract void RollbackTransaction(long transactionId, string savePointName, SmiEventSink eventSink);
	}
}
