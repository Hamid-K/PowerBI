using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000988 RID: 2440
	internal abstract class TransactionManager : Manager
	{
		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06004B86 RID: 19334 RVA: 0x0012D538 File Offset: 0x0012B738
		public virtual string PackageId
		{
			get
			{
				if (this._requester.Flavor == DrdaFlavor.Informix)
				{
					return "SYSSN500          ";
				}
				switch (this.IsolationLevel)
				{
				case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationCHG:
					return "MSUR001           ";
				case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationALL:
					return "MSRS001           ";
				case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationRR:
					return "MSRR001           ";
				case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationNC:
					return "MSNC001           ";
				}
				return "MSCS001           ";
			}
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06004B87 RID: 19335 RVA: 0x0012D5A0 File Offset: 0x0012B7A0
		public Microsoft.HostIntegration.Drda.Common.IsolationLevel IsolationLevel
		{
			get
			{
				Microsoft.HostIntegration.Drda.Common.IsolationLevel isolationLevel = Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationCS;
				switch (this._requester.IsolationLevel)
				{
				case SqlIsolationLevels.ReadUncommitted:
					isolationLevel = Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationCHG;
					break;
				case SqlIsolationLevels.ReadCommitted:
					isolationLevel = Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationCS;
					break;
				case SqlIsolationLevels.RepeatableRead:
					isolationLevel = Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationALL;
					break;
				case SqlIsolationLevels.Serializable:
					isolationLevel = Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationRR;
					break;
				}
				if (this._requester.HostType == HostType.AS400 && this._requester.AutoCommit)
				{
					isolationLevel = Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationNC;
				}
				return isolationLevel;
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06004B88 RID: 19336 RVA: 0x0012D620 File Offset: 0x0012B820
		public virtual byte[] PackageConsistencyToken
		{
			get
			{
				switch (this.IsolationLevel)
				{
				case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationCHG:
					return PackageManager.PackageToken_UR;
				case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationALL:
					return PackageManager.PackageToken_RS;
				case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationRR:
					return PackageManager.PackageToken_RR;
				case Microsoft.HostIntegration.Drda.Common.IsolationLevel.IsolationNC:
					return PackageManager.PackageToken_NC;
				}
				return PackageManager.PackageToken_CS;
			}
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x0012D673 File Offset: 0x0012B873
		public TransactionManager(Requester requester)
			: base(requester)
		{
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x0012D683 File Offset: 0x0012B883
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x0012D68B File Offset: 0x0012B88B
		public override void Reset()
		{
			this._level = 0;
			this.canBeReused = true;
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual Task CommitAsync(bool isAsync, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B8D RID: 19341 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual Task RollbackAsync(bool isAsync, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual Task EnlistAsync(Transaction transaction, bool isAsync, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x0012D69C File Offset: 0x0012B89C
		public async Task<bool> CanReuseConnection(bool isAsync, CancellationToken cancellationToken)
		{
			bool flag;
			if (this._requester.Pool == null)
			{
				flag = false;
			}
			else
			{
				this.canBeReused = false;
				await this.RefreshTransactionPoolStateAsync(isAsync, cancellationToken);
				flag = this.canBeReused;
			}
			return flag;
		}

		// Token: 0x06004B90 RID: 19344
		public abstract Task RefreshTransactionPoolStateAsync(bool isAsync, CancellationToken cancellationToken);

		// Token: 0x06004B91 RID: 19345 RVA: 0x0012D6F1 File Offset: 0x0012B8F1
		public static TransactionManager GetInstance(Requester requester)
		{
			if (requester.IsDuw)
			{
				return new XaManager(requester);
			}
			return new LocalTransactionManager(requester);
		}

		// Token: 0x04003B82 RID: 15234
		public bool canBeReused = true;
	}
}
