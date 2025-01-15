using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000929 RID: 2345
	internal class LocalTransactionManager : TransactionManager
	{
		// Token: 0x060049BB RID: 18875 RVA: 0x00113045 File Offset: 0x00111245
		public LocalTransactionManager(Requester requester)
			: base(requester)
		{
			this._tracePoint = requester.TracePoint;
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x0011305A File Offset: 0x0011125A
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x00113062 File Offset: 0x00111262
		public override void Reset()
		{
			base.Reset();
			this._tracePoint = this._requester.TracePoint;
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x0011307C File Offset: 0x0011127C
		public override async Task CommitAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter LocalTransactionManager::CommitAsync");
			}
			await this.EndTransaction(CodePoint.RDBCMM, false, isAsync, cancellationToken);
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit LocalTransactionManager::CommitAsync");
			}
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x001130D4 File Offset: 0x001112D4
		public override async Task RollbackAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter LocalTransactionManager::RollbackAsync");
			}
			await this.EndTransaction(CodePoint.RDBRLLBCK, false, isAsync, cancellationToken);
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit LocalTransactionManager::RollbackAsync");
			}
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x00113129 File Offset: 0x00111329
		public override Task EnlistAsync(Transaction transaction, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Error))
			{
				this._tracePoint.Trace(TraceFlags.Error, "Enlist is not supported at Local Transaction, check your connection string for DUW setting.");
			}
			throw new NotImplementedException(RequesterResource.NoEnlistForLocalTransaction);
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x00113154 File Offset: 0x00111354
		public override async Task RefreshTransactionPoolStateAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter LocalTransactionManager::RefreshTransactionPoolStateAsync");
			}
			await this.EndTransaction(CodePoint.RDBRLLBCK, true, isAsync, cancellationToken);
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit LocalTransactionManager::RefreshTransactionPoolStateAsync");
			}
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x001131AC File Offset: 0x001113AC
		private async Task EndTransaction(CodePoint cp, bool needReleaseConversation, bool isAsync, CancellationToken cancellationToken)
		{
			Manager.ReplyInfo replyInfo = null;
			if (needReleaseConversation)
			{
				this.canBeReused = false;
			}
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this._tracePoint.Trace(TraceFlags.Verbose, "LocalTransactionManager::EndTransaction: sending " + cp.ToString());
				}
				await base.WriteDDMCodepoint(cp, needReleaseConversation ? CodePoint.RLSCONV : DBNull.Value, DssType.Request, 1, 0, true, isAsync, cancellationToken);
				do
				{
					CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					if (currentCP == CodePoint.ENDUOWRM)
					{
						if (this._tracePoint.IsEnabled(TraceFlags.Information))
						{
							this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint ENDUOWRM");
						}
						bool flag = await this.ReadEnduowrmAsync(isAsync, cancellationToken);
						if (needReleaseConversation)
						{
							this.canBeReused = flag;
						}
					}
					else
					{
						Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, isAsync, cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Warning))
						{
							this._tracePoint.Trace(TraceFlags.Warning, "LocalTransactionManager::EndTransaction(): Read unexpected CodePoint: " + currentCP.ToString());
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
					this._tracePoint.Trace(TraceFlags.Error, "LocalTransactionManager::EndTransaction: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -1039, ex.HResult);
			}
			if (replyInfo != null)
			{
				base.ProcessReplyInfo(null, replyInfo, "LocalTransactionManager::EndTransaction");
			}
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x00113214 File Offset: 0x00111414
		private async Task<bool> ReadEnduowrmAsync(bool isAsync, CancellationToken cancellationToken)
		{
			LocalTransactionManager.<>c__DisplayClass8_0 CS$<>8__locals1 = new LocalTransactionManager.<>c__DisplayClass8_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.isAsync = isAsync;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Reading ENDUOWRM...");
			}
			CS$<>8__locals1.reuseConnection = false;
			await base.ReadDdmCodepoint(CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken, delegate(ObjectInfo ddmObj)
			{
				LocalTransactionManager.<>c__DisplayClass8_0.<<ReadEnduowrmAsync>b__0>d <<ReadEnduowrmAsync>b__0>d;
				<<ReadEnduowrmAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<ReadEnduowrmAsync>b__0>d.ddmObj = ddmObj;
				<<ReadEnduowrmAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<ReadEnduowrmAsync>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder<bool> <>t__builder = <<ReadEnduowrmAsync>b__0>d.<>t__builder;
				<>t__builder.Start<LocalTransactionManager.<>c__DisplayClass8_0.<<ReadEnduowrmAsync>b__0>d>(ref <<ReadEnduowrmAsync>b__0>d);
				return <<ReadEnduowrmAsync>b__0>d.<>t__builder.Task;
			});
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Finished reading ENDUOWRM.");
			}
			return CS$<>8__locals1.reuseConnection;
		}
	}
}
