using System;
using System.Data;
using System.Data.SqlTypes;
using System.Security.Principal;
using System.Transactions;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000148 RID: 328
	internal abstract class SmiContext
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001997 RID: 6551
		// (remove) Token: 0x06001998 RID: 6552
		internal abstract event EventHandler OutOfScope;

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001999 RID: 6553
		internal abstract SmiConnection ContextConnection { get; }

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x0600199A RID: 6554
		internal abstract long ContextTransactionId { get; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600199B RID: 6555
		internal abstract Transaction ContextTransaction { get; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600199C RID: 6556
		internal abstract bool HasContextPipe { get; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x0600199D RID: 6557
		internal abstract WindowsIdentity WindowsIdentity { get; }

		// Token: 0x0600199E RID: 6558
		internal abstract SmiRecordBuffer CreateRecordBuffer(SmiExtendedMetaData[] columnMetaData, SmiEventSink eventSink);

		// Token: 0x0600199F RID: 6559
		internal abstract SmiRequestExecutor CreateRequestExecutor(string commandText, CommandType commandType, SmiParameterMetaData[] parameterMetaData, SmiEventSink eventSink);

		// Token: 0x060019A0 RID: 6560
		internal abstract object GetContextValue(int key);

		// Token: 0x060019A1 RID: 6561
		internal abstract void GetTriggerInfo(SmiEventSink eventSink, out bool[] columnsUpdated, out TriggerAction action, out SqlXml eventInstanceData);

		// Token: 0x060019A2 RID: 6562
		internal abstract void SendMessageToPipe(string message, SmiEventSink eventSink);

		// Token: 0x060019A3 RID: 6563
		internal abstract void SendResultsStartToPipe(SmiRecordBuffer recordBuffer, SmiEventSink eventSink);

		// Token: 0x060019A4 RID: 6564
		internal abstract void SendResultsRowToPipe(SmiRecordBuffer recordBuffer, SmiEventSink eventSink);

		// Token: 0x060019A5 RID: 6565
		internal abstract void SendResultsEndToPipe(SmiRecordBuffer recordBuffer, SmiEventSink eventSink);

		// Token: 0x060019A6 RID: 6566
		internal abstract void SetContextValue(int key, object value);

		// Token: 0x060019A7 RID: 6567 RVA: 0x0006B48C File Offset: 0x0006968C
		internal virtual SmiStream GetScratchStream(SmiEventSink sink)
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
			return null;
		}
	}
}
