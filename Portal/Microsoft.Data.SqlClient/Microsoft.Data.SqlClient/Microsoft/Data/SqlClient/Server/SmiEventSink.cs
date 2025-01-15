using System;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200011D RID: 285
	internal abstract class SmiEventSink
	{
		// Token: 0x06001671 RID: 5745
		internal abstract void BatchCompleted();

		// Token: 0x06001672 RID: 5746 RVA: 0x000605F2 File Offset: 0x0005E7F2
		internal virtual void ParameterAvailable(SmiParameterMetaData metaData, SmiTypedGetterSetter paramValue, int ordinal)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001673 RID: 5747
		internal abstract void DefaultDatabaseChanged(string databaseName);

		// Token: 0x06001674 RID: 5748
		internal abstract void MessagePosted(int number, byte state, byte errorClass, string server, string message, string procedure, int lineNumber);

		// Token: 0x06001675 RID: 5749
		internal abstract void MetaDataAvailable(SmiQueryMetaData[] metaData, bool nextEventIsRow);

		// Token: 0x06001676 RID: 5750 RVA: 0x000605F2 File Offset: 0x0005E7F2
		internal virtual void RowAvailable(SmiTypedGetterSetter rowData)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001677 RID: 5751
		internal abstract void StatementCompleted(int rowsAffected);

		// Token: 0x06001678 RID: 5752
		internal abstract void TransactionCommitted(long transactionId);

		// Token: 0x06001679 RID: 5753
		internal abstract void TransactionDefected(long transactionId);

		// Token: 0x0600167A RID: 5754
		internal abstract void TransactionEnlisted(long transactionId);

		// Token: 0x0600167B RID: 5755
		internal abstract void TransactionEnded(long transactionId);

		// Token: 0x0600167C RID: 5756
		internal abstract void TransactionRolledBack(long transactionId);

		// Token: 0x0600167D RID: 5757
		internal abstract void TransactionStarted(long transactionId);

		// Token: 0x0600167E RID: 5758 RVA: 0x000605F2 File Offset: 0x0005E7F2
		internal virtual void ParametersAvailable(SmiParameterMetaData[] metaData, ITypedGettersV3 paramValues)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x000605F2 File Offset: 0x0005E7F2
		internal virtual void RowAvailable(ITypedGettersV3 rowData)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x000605F2 File Offset: 0x0005E7F2
		internal virtual void RowAvailable(ITypedGetters rowData)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}
