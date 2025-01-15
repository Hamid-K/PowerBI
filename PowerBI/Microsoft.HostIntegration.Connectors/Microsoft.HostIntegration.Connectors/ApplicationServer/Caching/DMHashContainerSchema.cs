using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000216 RID: 534
	internal class DMHashContainerSchema : IContainerSchema
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00038EDD File Offset: 0x000370DD
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x00038ED4 File Offset: 0x000370D4
		public IStoreSchema BaseStoreSchema
		{
			get
			{
				return this._hashTableStoreSchema;
			}
			set
			{
				this._hashTableStoreSchema = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00038EE5 File Offset: 0x000370E5
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x00038EED File Offset: 0x000370ED
		internal DMOperationCallBack[] OperationCallBackArray
		{
			get
			{
				return this._operationCallBackArray;
			}
			set
			{
				this._operationCallBackArray = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00038EF6 File Offset: 0x000370F6
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x00038EFE File Offset: 0x000370FE
		public CommitType CommitType
		{
			get
			{
				return this._commitType;
			}
			set
			{
				this._commitType = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00038F07 File Offset: 0x00037107
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x00038F0F File Offset: 0x0003710F
		public ExpirationType ExpirationType
		{
			get
			{
				return this._expirationType;
			}
			set
			{
				this._expirationType = value;
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00038F18 File Offset: 0x00037118
		public void AddIndex(IIndexSchema iIndexSchema)
		{
			this._indexSchema = iIndexSchema;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00038F21 File Offset: 0x00037121
		public void AddCallBack(DMCallBackType callBackType, DMOperationCallBack callBack)
		{
			this._operationCallBackArray[(int)callBackType] = callBack;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00038F2C File Offset: 0x0003712C
		public IIndexSchema GetIndexSchema()
		{
			return this._indexSchema;
		}

		// Token: 0x04000AFF RID: 2815
		private DMOperationCallBack[] _operationCallBackArray = new DMOperationCallBack[33];

		// Token: 0x04000B00 RID: 2816
		private IStoreSchema _hashTableStoreSchema;

		// Token: 0x04000B01 RID: 2817
		private IIndexSchema _indexSchema;

		// Token: 0x04000B02 RID: 2818
		private CommitType _commitType = CommitType.ImmediateCommit;

		// Token: 0x04000B03 RID: 2819
		private ExpirationType _expirationType = ExpirationType.AbsoluteExpiration;
	}
}
