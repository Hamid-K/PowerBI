using System;
using System.EnterpriseServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.DDM;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200098B RID: 2443
	internal class XaManager : TransactionManager, IXaClientEnlistment
	{
		// Token: 0x06004BA2 RID: 19362 RVA: 0x0012DC06 File Offset: 0x0012BE06
		public XaManager(Requester requester)
			: base(requester)
		{
			this._tracePoint = new XaManagerTracePoint(requester.TracePoint);
			this._managerCodepoint = ManagerCodePoint.XAMGR;
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06004BA3 RID: 19363 RVA: 0x0012DC2B File Offset: 0x0012BE2B
		// (set) Token: 0x06004BA4 RID: 19364 RVA: 0x0012DC33 File Offset: 0x0012BE33
		public bool IsInTransaction { get; private set; }

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06004BA5 RID: 19365 RVA: 0x0012DC3C File Offset: 0x0012BE3C
		// (set) Token: 0x06004BA6 RID: 19366 RVA: 0x0012DC44 File Offset: 0x0012BE44
		public SemaphoreSlim XaLock { get; private set; }

		// Token: 0x06004BA7 RID: 19367 RVA: 0x0012DC4D File Offset: 0x0012BE4D
		public override void Initialize()
		{
			base.Initialize();
			this.XaLock = new SemaphoreSlim(1);
			this.IsInTransaction = false;
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x0012DC68 File Offset: 0x0012BE68
		public override void Reset()
		{
			base.Reset();
			if (this._requester.TracePoint != null && this._tracePoint.TraceContainer != this._requester.TracePoint.TraceContainer)
			{
				this._tracePoint = new XaManagerTracePoint(this._requester.TracePoint);
			}
			this._xid = null;
			this.IsInTransaction = false;
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x0012DCC9 File Offset: 0x0012BEC9
		public void Abort()
		{
			this._isAborted = true;
			this._requester = null;
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x0012DCDC File Offset: 0x0012BEDC
		public override async Task EnlistAsync(Transaction transaction, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this._tracePoint.Trace(TraceFlags.Verbose, "Enter XaManager::EnlistAsync");
			}
			Transaction transaction2 = Transaction.Current;
			try
			{
				if (transaction == null)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "XaManager::EnlistAsync Transaction parameter is null.");
					}
				}
				else if (Transaction.Current != ContextUtil.SystemTransaction || Transaction.Current == null)
				{
					Transaction.Current = transaction;
				}
				Microsoft.HostIntegration.XaClient.Xid xid = XaClient.EnlistXa(this, "MSDRDA" + this._requester.ConnectionString);
				this._xid = this.Convert(xid);
				this.IsInTransaction = true;
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "XaManager::EnlistAsync: " + ex.ToString());
				}
				throw this._requester.MakeException(ex.Message, "HY000", -1042, ex.HResult);
			}
			finally
			{
				if (Transaction.Current != ContextUtil.SystemTransaction)
				{
					Transaction.Current = transaction2;
				}
			}
			SYNCCRD synccrd = await this.SubmitSyncctl(this._xid, SyncType.SYNCTYPE_NEW_UOW, 0, isAsync, cancellationToken);
			if (synccrd.XaReturnValue != 0)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "XaManager::EnlistAsync: SYNCCRD deosn't have XaReturnCode.Ok: " + synccrd.XaReturnValue.ToString());
				}
				throw this._requester.MakeException(RequesterResource.UnexpectedXaResult(synccrd.XaReturnValue), "HY000", -1034, synccrd.XaReturnValue);
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this._tracePoint.Trace(TraceFlags.Verbose, "Exit XaManager::EnlistAsync");
			}
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x0012DD3C File Offset: 0x0012BF3C
		public Microsoft.HostIntegration.Drda.Common.Xid Convert(Microsoft.HostIntegration.XaClient.Xid xid)
		{
			byte[] array = new byte[xid.TransactionIdLength];
			byte[] array2 = new byte[xid.BranchQualifierLength];
			global::System.Buffer.BlockCopy(xid.Data, 0, array, 0, xid.TransactionIdLength);
			global::System.Buffer.BlockCopy(xid.Data, xid.TransactionIdLength, array2, 0, xid.BranchQualifierLength);
			return new Microsoft.HostIntegration.Drda.Common.Xid(xid.FormatId, array, array2);
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x0012DD9C File Offset: 0x0012BF9C
		public override async Task RefreshTransactionPoolStateAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (!this.IsInTransaction)
			{
				await this._requester.LocalTransactionManager.RefreshTransactionPoolStateAsync(isAsync, cancellationToken);
				this.canBeReused = this._requester.LocalTransactionManager.canBeReused;
			}
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x0012DDF4 File Offset: 0x0012BFF4
		public async Task<SYNCCRD> SubmitSyncctl(Microsoft.HostIntegration.Drda.Common.Xid xid, SyncType syncType, int xaFlags, bool isAsync, CancellationToken cancellationToken)
		{
			SYNCCTL syncctl = new SYNCCTL(null, null, -1);
			base.InitializeCodepoint(syncctl);
			syncctl.SyncType = syncType;
			syncctl.Xaflags = xaFlags;
			syncctl.Xid = xid;
			Manager.ReplyInfo replyInfo = null;
			SYNCCRD synccrd = null;
			try
			{
				await syncctl.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 0, isAsync, cancellationToken);
				do
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					if (currentCP == CodePoint.SYNCCRD)
					{
						synccrd = new SYNCCRD();
						await synccrd.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
					}
					else
					{
						Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Warning))
						{
							this._tracePoint.Trace(TraceFlags.Warning, "XaManager::SubmitSyncctl(): Read unexpected CodePoint: " + currentCP.ToString());
						}
						if (replyInfo2 != null)
						{
							replyInfo = replyInfo2;
						}
					}
				}
				while (base.NeedReadMoreDdmCodepoint(1));
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "XaManager::SubmitSyncctl: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -1042, ex.HResult);
			}
			base.ProcessReplyInfo(null, replyInfo, "XaManager::SubmitSyncctl");
			return synccrd;
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x0012DE64 File Offset: 0x0012C064
		public void Commit()
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this._tracePoint.Trace(TraceFlags.Verbose, "Enter XaManager::Commit");
			}
			if (this._isAborted)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Warning))
				{
					this._tracePoint.Trace(TraceFlags.Warning, "XaManager::Commit being called when XaManager is retired.");
				}
				throw new Exception("XaManager is retired.");
			}
			if (!this._needReleaseLock)
			{
				this._requester.Enter();
			}
			try
			{
				SYNCCRD result = this.SubmitSyncctl(this._xid, SyncType.SYNCTYPE_COMMITTED, 0, false, CancellationToken.None).GetAwaiter().GetResult();
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "XaManager::Commit: SYNCCRD Reveived XaReturnCode: " + result.XaReturnValue.ToString());
				}
				if (result.XaReturnValue != 0)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "XaManager::Commit: SYNCCRD deosn't have XaReturnCode.Ok: " + result.XaReturnValue.ToString());
					}
					throw this._requester.MakeException(RequesterResource.UnexpectedXaResult(result.XaReturnValue), "HY000", -1034, result.XaReturnValue);
				}
			}
			finally
			{
				this.IsInTransaction = false;
				this.XaLock.Release();
				this._needReleaseLock = false;
			}
			this._requester.DisconnectIfWaitingForTransactionClose();
			if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this._tracePoint.Trace(TraceFlags.Verbose, "Exit XaManager::Commit");
			}
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x0012DFF0 File Offset: 0x0012C1F0
		public XaReturnCode Prepare(bool singlePhase)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this._tracePoint.Trace(TraceFlags.Verbose, "Enter XaManager::Prepare");
			}
			if (this._isAborted)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Warning))
				{
					this._tracePoint.Trace(TraceFlags.Warning, "XaManager::Prepare being called when XaManager is retired.");
				}
				throw new Exception("XaManager is retired.");
			}
			SYNCCRD synccrd = null;
			bool flag = true;
			this._requester.Enter();
			try
			{
				if (this._requester.State == Requester.RequesterState.Closed)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this._tracePoint.Trace(TraceFlags.Verbose, "XaManager::Prepare:  Requester has been closed already.");
					}
					return XaReturnCode.ResourceManagerUnavailable;
				}
				SYNCCTL syncctl = new SYNCCTL(null, null, -1);
				base.InitializeCodepoint(syncctl);
				synccrd = this.SubmitSyncctl(this._xid, SyncType.SYNCTYPE_END_UOW, 67108864, false, CancellationToken.None).GetAwaiter().GetResult();
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "XaManager::Prepare: SYNCCRD Reveived for End Type: XaReturnCode: " + synccrd.XaReturnValue.ToString());
				}
				if (synccrd.XaReturnValue != 0)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "XaManager::Prepare: SYNCCRD deosn't have XaReturnCode.Ok: " + synccrd.XaReturnValue.ToString());
					}
					throw this._requester.MakeException(RequesterResource.UnexpectedXaResult(synccrd.XaReturnValue), "HY000", -1034, synccrd.XaReturnValue);
				}
				if (singlePhase)
				{
					synccrd = this.SubmitSyncctl(this._xid, SyncType.SYNCTYPE_COMMITTED, 1073741824, false, CancellationToken.None).GetAwaiter().GetResult();
				}
				else
				{
					synccrd = this.SubmitSyncctl(this._xid, SyncType.SYNCTYPE_PREPARE, 0, false, CancellationToken.None).GetAwaiter().GetResult();
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "XaManager::Prepare: SYNCCRD Reveived XaReturnCode: " + synccrd.XaReturnValue.ToString());
				}
				if (singlePhase)
				{
					if (synccrd.XaReturnValue == 0)
					{
						synccrd.XaReturnValue = 1000;
					}
				}
				else if (synccrd.XaReturnValue == 0)
				{
					flag = false;
					this._needReleaseLock = true;
				}
			}
			finally
			{
				if (flag)
				{
					this._requester.Leave();
					this.IsInTransaction = false;
				}
			}
			if (!this.IsInTransaction)
			{
				this._requester.DisconnectIfWaitingForTransactionClose();
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this._tracePoint.Trace(TraceFlags.Verbose, "Exit XaManager::Prepare");
			}
			return (XaReturnCode)synccrd.XaReturnValue;
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x0012E288 File Offset: 0x0012C488
		public void Rollback()
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this._tracePoint.Trace(TraceFlags.Verbose, "Enter XaManager::Rollback");
			}
			if (this._isAborted)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Warning))
				{
					this._tracePoint.Trace(TraceFlags.Warning, "XaManager::Rollback being called when XaManager is retired.");
				}
				throw new Exception("XaManager is retired.");
			}
			if (!this._needReleaseLock)
			{
				this._requester.Enter();
			}
			try
			{
				if (this._requester.State == Requester.RequesterState.Closed)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this._tracePoint.Trace(TraceFlags.Verbose, "XaManager::Rollback:  Requester has been closed already.");
					}
					throw new Exception("Not able to rollback since Drda Connection has already been closed.");
				}
				SYNCCRD synccrd = this.SubmitSyncctl(this._xid, SyncType.SYNCTYPE_END_UOW, 67108864, false, CancellationToken.None).GetAwaiter().GetResult();
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "XaManager::Rollback: SYNCCRD Reveived for End Type: XaReturnCode: " + synccrd.XaReturnValue.ToString());
				}
				if (synccrd.XaReturnValue != 0)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "XaManager::Rollback: SYNCCRD deosn't have XaReturnCode.Ok: " + synccrd.XaReturnValue.ToString());
					}
					throw this._requester.MakeException(RequesterResource.UnexpectedXaResult(synccrd.XaReturnValue), "HY000", -1034, synccrd.XaReturnValue);
				}
				synccrd = this.SubmitSyncctl(this._xid, SyncType.SYNCTYPE_ROLLBACK, 0, false, CancellationToken.None).GetAwaiter().GetResult();
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "XaManager::Rollback: SYNCCRD Reveived for Prepare Type: XaReturnCode: " + synccrd.XaReturnValue.ToString());
				}
				if (synccrd.XaReturnValue != 0)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Error))
					{
						this._tracePoint.Trace(TraceFlags.Error, "XaManager::Rollback: SYNCCRD deosn't have XaReturnCode.Ok: " + synccrd.XaReturnValue.ToString());
					}
					throw this._requester.MakeException(RequesterResource.UnexpectedXaResult(synccrd.XaReturnValue), "HY000", -1034, synccrd.XaReturnValue);
				}
			}
			finally
			{
				this.IsInTransaction = false;
				this.XaLock.Release();
				this._needReleaseLock = false;
			}
			this._requester.DisconnectIfWaitingForTransactionClose();
			if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this._tracePoint.Trace(TraceFlags.Verbose, "Exit XaManager::Rollback");
			}
		}

		// Token: 0x04003B8E RID: 15246
		private Microsoft.HostIntegration.Drda.Common.Xid _xid;

		// Token: 0x04003B8F RID: 15247
		private volatile bool _needReleaseLock;

		// Token: 0x04003B90 RID: 15248
		private bool _isAborted;

		// Token: 0x0200098C RID: 2444
		public enum XaFlags
		{
			// Token: 0x04003B94 RID: 15252
			NoFlags,
			// Token: 0x04003B95 RID: 15253
			Recover = 524288,
			// Token: 0x04003B96 RID: 15254
			Join = 2097152,
			// Token: 0x04003B97 RID: 15255
			LCS = 8388608,
			// Token: 0x04003B98 RID: 15256
			Suspend = 33554432,
			// Token: 0x04003B99 RID: 15257
			Success = 67108864,
			// Token: 0x04003B9A RID: 15258
			Resume = 134217728,
			// Token: 0x04003B9B RID: 15259
			LocalXA = 268435456,
			// Token: 0x04003B9C RID: 15260
			Fail = 536870912,
			// Token: 0x04003B9D RID: 15261
			OnePhase = 1073741824
		}
	}
}
