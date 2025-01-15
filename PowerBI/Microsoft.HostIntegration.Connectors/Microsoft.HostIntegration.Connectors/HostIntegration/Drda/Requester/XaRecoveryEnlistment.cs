using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.DDM;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200098A RID: 2442
	public class XaRecoveryEnlistment : IXaRecoveryEnlistment
	{
		// Token: 0x06004B95 RID: 19349 RVA: 0x0012D814 File Offset: 0x0012BA14
		private static Exception MakeException(string errorMessage, string sqlState, int sqlCode, int errorCode)
		{
			return new Exception(errorMessage);
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06004B96 RID: 19350 RVA: 0x0012D81C File Offset: 0x0012BA1C
		// (set) Token: 0x06004B97 RID: 19351 RVA: 0x0012D824 File Offset: 0x0012BA24
		public int ResourceManagerId { get; set; }

		// Token: 0x06004B98 RID: 19352 RVA: 0x0012D830 File Offset: 0x0012BA30
		public XaReturnCode Open(string xaInfo, XaFlags flags)
		{
			string[] array = xaInfo.Substring(6).Split(new char[] { ';' });
			this._requester = new Requester(array);
			this._requester.Initialize(this._traceContainer, new Func<string, string, int, int, Exception>(XaRecoveryEnlistment.MakeException));
			this._requester.IsDuw = false;
			this._requester.ConnectAsync(null, false, CancellationToken.None).GetAwaiter().GetResult();
			return XaReturnCode.Ok;
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x0012D8AC File Offset: 0x0012BAAC
		public XaReturnCode Close(string xaInfo, XaFlags flags)
		{
			this._requester.Disconnect(false, CancellationToken.None).GetAwaiter().GetResult();
			return XaReturnCode.Ok;
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x0012D8D8 File Offset: 0x0012BAD8
		public XaReturnCode Start(Microsoft.HostIntegration.XaClient.Xid xid, XaFlags flags)
		{
			XaManager xaManager = this._requester.TransactionManager as XaManager;
			Microsoft.HostIntegration.Drda.Common.Xid xid2 = xaManager.Convert(xid);
			return (XaReturnCode)xaManager.SubmitSyncctl(xid2, SyncType.SYNCTYPE_NEW_UOW, (int)flags, false, CancellationToken.None).GetAwaiter().GetResult()
				.XaReturnValue;
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0012D920 File Offset: 0x0012BB20
		public XaReturnCode End(Microsoft.HostIntegration.XaClient.Xid xid, XaFlags flags)
		{
			XaManager xaManager = this._requester.TransactionManager as XaManager;
			Microsoft.HostIntegration.Drda.Common.Xid xid2 = xaManager.Convert(xid);
			return (XaReturnCode)xaManager.SubmitSyncctl(xid2, SyncType.SYNCTYPE_END_UOW, (int)flags, false, CancellationToken.None).GetAwaiter().GetResult()
				.XaReturnValue;
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x0012D968 File Offset: 0x0012BB68
		public XaReturnCode Rollback(Microsoft.HostIntegration.XaClient.Xid xid, XaFlags flags)
		{
			XaManager xaManager = this._requester.TransactionManager as XaManager;
			Microsoft.HostIntegration.Drda.Common.Xid xid2 = xaManager.Convert(xid);
			return (XaReturnCode)xaManager.SubmitSyncctl(xid2, SyncType.SYNCTYPE_ROLLBACK, (int)flags, false, CancellationToken.None).GetAwaiter().GetResult()
				.XaReturnValue;
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x0012D9B0 File Offset: 0x0012BBB0
		public XaReturnCode Prepare(Microsoft.HostIntegration.XaClient.Xid xid, XaFlags flags)
		{
			XaManager xaManager = this._requester.TransactionManager as XaManager;
			Microsoft.HostIntegration.Drda.Common.Xid xid2 = xaManager.Convert(xid);
			return (XaReturnCode)xaManager.SubmitSyncctl(xid2, SyncType.SYNCTYPE_PREPARE, (int)flags, false, CancellationToken.None).GetAwaiter().GetResult()
				.XaReturnValue;
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x0012D9F8 File Offset: 0x0012BBF8
		public XaReturnCode Commit(Microsoft.HostIntegration.XaClient.Xid xid, XaFlags flags)
		{
			XaManager xaManager = this._requester.TransactionManager as XaManager;
			Microsoft.HostIntegration.Drda.Common.Xid xid2 = xaManager.Convert(xid);
			return (XaReturnCode)xaManager.SubmitSyncctl(xid2, SyncType.SYNCTYPE_COMMITTED, (int)flags, false, CancellationToken.None).GetAwaiter().GetResult()
				.XaReturnValue;
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x0012DA40 File Offset: 0x0012BC40
		public XaReturnCode Forget(Microsoft.HostIntegration.XaClient.Xid xid, XaFlags flags)
		{
			XaManager xaManager = this._requester.TransactionManager as XaManager;
			Microsoft.HostIntegration.Drda.Common.Xid xid2 = xaManager.Convert(xid);
			return (XaReturnCode)xaManager.SubmitSyncctl(xid2, SyncType.SYNCTYPE_FORGET, (int)flags, false, CancellationToken.None).GetAwaiter().GetResult()
				.XaReturnValue;
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x0012DA88 File Offset: 0x0012BC88
		public XaReturnCode Recover(XaFlags flags, int maximumNumberOfXids, out Microsoft.HostIntegration.XaClient.Xid[] xids)
		{
			xids = null;
			if ((flags & XaFlags.StartRecoveryScan) != XaFlags.None)
			{
				XaManager xaManager = this._requester.TransactionManager as XaManager;
				SYNCCRD result = xaManager.SubmitSyncctl(null, SyncType.SYNCTYPE_INDOUBT, 0, false, CancellationToken.None).GetAwaiter().GetResult();
				this._recoveryList = result.IndoubtList;
				this._currentRecoveryIndex = 0;
				if (xaManager.TracePoint.IsEnabled(TraceFlags.Information))
				{
					xaManager.TracePoint.Trace(TraceFlags.Information, "XaRecoveryEnlistment::Recover: SYNCCRD Reveived for End Type: XaReturnCode: " + result.XaReturnValue.ToString());
				}
				if (result.XaReturnValue != 0)
				{
					return XaReturnCode.ResourceManagerError;
				}
			}
			int num = this._recoveryList.Count - this._currentRecoveryIndex;
			if (num > maximumNumberOfXids)
			{
				num = maximumNumberOfXids;
			}
			if (num > 0)
			{
				xids = new Microsoft.HostIntegration.XaClient.Xid[num];
				for (int i = 0; i < num; i++)
				{
					Microsoft.HostIntegration.Drda.Common.Xid xid = this._recoveryList[this._currentRecoveryIndex + i];
					byte[] array = new byte[xid.BranchQualifier.Length + xid.GlobalTransactionId.Length];
					global::System.Buffer.BlockCopy(xid.GlobalTransactionId, 0, array, 0, xid.GlobalTransactionId.Length);
					global::System.Buffer.BlockCopy(xid.BranchQualifier, 0, array, xid.GlobalTransactionId.Length, xid.BranchQualifier.Length);
					xids[i] = new Microsoft.HostIntegration.XaClient.Xid(xid.FormatId, xid.GlobalTransactionId.Length, xid.BranchQualifier.Length, array);
				}
				this._currentRecoveryIndex += num;
			}
			return XaReturnCode.Ok;
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x0012DC00 File Offset: 0x0012BE00
		public XaReturnCode Complete(int handle, XaFlags flags, out int returnValue)
		{
			returnValue = 0;
			return XaReturnCode.Ok;
		}

		// Token: 0x04003B89 RID: 15241
		private Requester _requester;

		// Token: 0x04003B8A RID: 15242
		private DrdaClientTraceContainer _traceContainer = new DrdaClientTraceContainer();

		// Token: 0x04003B8B RID: 15243
		private List<Microsoft.HostIntegration.Drda.Common.Xid> _recoveryList;

		// Token: 0x04003B8C RID: 15244
		private int _currentRecoveryIndex = -1;
	}
}
