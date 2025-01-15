using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000244 RID: 580
	internal struct MDHOperationInfo
	{
		// Token: 0x0600131A RID: 4890 RVA: 0x0003B234 File Offset: 0x00039434
		internal MDHOperationInfo(MDHOperationType operationType, object searchKey, ADMCacheItem newCacheItem, DMOperationCallBack preOperationCallBack, DMOperationCallBack postOperationCallBack, MultiDirectoryHashtable parentHashTable, object opState)
		{
			this = default(MDHOperationInfo);
			this._retry = false;
			this.Version = InternalCacheItemVersion.Null;
			this._operationType = operationType;
			this._searchKey = searchKey;
			this._newCacheItem = newCacheItem;
			this._preOperationCallBack = preOperationCallBack;
			this._postOperationCallBack = postOperationCallBack;
			this._hashCode = searchKey.GetHashCode();
			this._parentHashTable = parentHashTable;
			this._opState = opState;
			this._oldCacheItem = null;
			this._oldExpiredCacheItem = null;
			this._param1 = null;
			this._param2 = null;
			this._param3 = null;
			this._param4 = null;
			this._param5 = null;
			this._param6 = null;
			this.Protocol = ProtocolType.Regular;
			this._commitType = parentHashTable.CommitType;
			this._operationCompleted = false;
			this._directoryLatched = null;
			this._oldLockHandle = default(DMLockHandle);
			this._oldTTL = 0L;
			this._oldExtensionTimeout = 0L;
			this._oldLockExpirationTime = 0L;
			this._oldFlag = default(CacheItemFlag);
			this.LockKey = false;
			this._lockPlaceHolder = null;
			this.LatchReleased = false;
			this.ParentLinkageCheckRequired = true;
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0003B341 File Offset: 0x00039541
		// (set) Token: 0x0600131C RID: 4892 RVA: 0x0003B349 File Offset: 0x00039549
		internal MDHDirectoryNode DirectoryLatched
		{
			get
			{
				return this._directoryLatched;
			}
			set
			{
				this._directoryLatched = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0003B352 File Offset: 0x00039552
		// (set) Token: 0x0600131E RID: 4894 RVA: 0x0003B35A File Offset: 0x0003955A
		internal bool OperationCompleted
		{
			get
			{
				return this._operationCompleted;
			}
			set
			{
				ReleaseAssert.IsTrue(!this._operationCompleted || value);
				this._operationCompleted = value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0003B374 File Offset: 0x00039574
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x0003B37C File Offset: 0x0003957C
		internal MDHOperationType OperationType
		{
			get
			{
				return this._operationType;
			}
			set
			{
				this._operationType = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0003B385 File Offset: 0x00039585
		// (set) Token: 0x06001322 RID: 4898 RVA: 0x0003B38D File Offset: 0x0003958D
		internal bool Retry
		{
			get
			{
				return this._retry;
			}
			set
			{
				this._retry = value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0003B396 File Offset: 0x00039596
		internal object OpState
		{
			get
			{
				return this._opState;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0003B39E File Offset: 0x0003959E
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x0003B3A8 File Offset: 0x000395A8
		internal ADMCacheItem OldCacheItem
		{
			get
			{
				return this._oldCacheItem;
			}
			set
			{
				this._oldCacheItem = null;
				if (value != null)
				{
					if (value.IsLockPlaceHolderObject)
					{
						this._lockPlaceHolder = value;
						return;
					}
					if (ExpirationType.SlidingExpiration != this._parentHashTable.ExpirationType && value.IsExpired())
					{
						this._oldExpiredCacheItem = value;
						return;
					}
					this._oldCacheItem = value;
				}
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0003B3F6 File Offset: 0x000395F6
		internal ADMCacheItem OldExpiredCacheItem
		{
			get
			{
				return this._oldExpiredCacheItem;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0003B3FE File Offset: 0x000395FE
		// (set) Token: 0x06001328 RID: 4904 RVA: 0x0003B406 File Offset: 0x00039606
		internal ADMCacheItem NewCacheItem
		{
			get
			{
				return this._newCacheItem;
			}
			set
			{
				this._newCacheItem = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x0003B40F File Offset: 0x0003960F
		// (set) Token: 0x0600132A RID: 4906 RVA: 0x0003B417 File Offset: 0x00039617
		internal int HashCode
		{
			get
			{
				return this._hashCode;
			}
			set
			{
				this._hashCode = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0003B420 File Offset: 0x00039620
		// (set) Token: 0x0600132C RID: 4908 RVA: 0x0003B428 File Offset: 0x00039628
		internal object SearchKey
		{
			get
			{
				return this._searchKey;
			}
			set
			{
				this._searchKey = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0003B431 File Offset: 0x00039631
		internal DMOperationCallBack PreOperation
		{
			get
			{
				return this._preOperationCallBack;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0003B439 File Offset: 0x00039639
		internal DMOperationCallBack PostOperation
		{
			get
			{
				return this._postOperationCallBack;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x0003B441 File Offset: 0x00039641
		internal MultiDirectoryHashtable ParentHashTable
		{
			get
			{
				return this._parentHashTable;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0003B449 File Offset: 0x00039649
		// (set) Token: 0x06001331 RID: 4913 RVA: 0x0003B451 File Offset: 0x00039651
		internal object Param1
		{
			get
			{
				return this._param1;
			}
			set
			{
				this._param1 = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0003B45A File Offset: 0x0003965A
		// (set) Token: 0x06001333 RID: 4915 RVA: 0x0003B462 File Offset: 0x00039662
		internal object Param3
		{
			get
			{
				return this._param3;
			}
			set
			{
				this._param3 = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x0003B46B File Offset: 0x0003966B
		// (set) Token: 0x06001335 RID: 4917 RVA: 0x0003B473 File Offset: 0x00039673
		internal object Param4
		{
			get
			{
				return this._param4;
			}
			set
			{
				this._param4 = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x0003B47C File Offset: 0x0003967C
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x0003B484 File Offset: 0x00039684
		internal object Param5
		{
			get
			{
				return this._param5;
			}
			set
			{
				this._param5 = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x0003B48D File Offset: 0x0003968D
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x0003B495 File Offset: 0x00039695
		internal ADMCacheItem LockPlaceHolderObject
		{
			get
			{
				return this._lockPlaceHolder;
			}
			set
			{
				this._lockPlaceHolder = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0003B49E File Offset: 0x0003969E
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x0003B4A6 File Offset: 0x000396A6
		internal object Param2
		{
			get
			{
				return this._param2;
			}
			set
			{
				this._param2 = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0003B4AF File Offset: 0x000396AF
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x0003B4B7 File Offset: 0x000396B7
		internal object Param6
		{
			get
			{
				return this._param6;
			}
			set
			{
				this._param6 = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0003B4C0 File Offset: 0x000396C0
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x0003B4C8 File Offset: 0x000396C8
		internal CommitType Consistency
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

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0003B4D1 File Offset: 0x000396D1
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x0003B4D9 File Offset: 0x000396D9
		internal ProtocolType Protocol { get; set; }

		// Token: 0x06001342 RID: 4930 RVA: 0x0003B4E4 File Offset: 0x000396E4
		internal AMDHObjectNode GetObjectNode()
		{
			this.ClearOldLockHandle();
			switch (this.Consistency)
			{
			case CommitType.DeferredCommit:
				if (this.NewCacheItem != null)
				{
					this.NewCacheItem.IsCommitted = false;
				}
				if (this.OldCacheItem != null)
				{
					this.OldCacheItem.UncommittedCacheItem = this.NewCacheItem;
					this.OldCacheItem.TakeCommitLock();
					return this.OldCacheItem;
				}
				if (this._lockPlaceHolder != null)
				{
					this._lockPlaceHolder.UncommittedCacheItem = this.NewCacheItem;
					this._lockPlaceHolder.TakeCommitLock();
					return this._lockPlaceHolder;
				}
				this.NewCacheItem.TakeCommitLock();
				return this.NewCacheItem;
			case CommitType.ImmediateCommit:
				if (this.NewCacheItem != null)
				{
					this.NewCacheItem.IsCommitted = false;
					this.NewCacheItem.LastCommittedItem = this.OldCacheItem;
				}
				return this.NewCacheItem;
			default:
				return null;
			}
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0003B5C0 File Offset: 0x000397C0
		private void ClearOldLockHandle()
		{
			DMLockHandle dmlockHandle = default(DMLockHandle);
			if (this.OldCacheItem != null)
			{
				dmlockHandle = this.OldCacheItem.GetDMLockHandle();
			}
			dmlockHandle.FreeLockID();
			if (this.NewCacheItem != null)
			{
				this.NewCacheItem.SetLockHandle(dmlockHandle);
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0003B604 File Offset: 0x00039804
		internal InternalCacheItemVersion GetNewVersion()
		{
			InternalCacheItemVersion internalCacheItemVersion;
			if (this.OldCacheItem != null)
			{
				internalCacheItemVersion = this.OldCacheItem.Version;
			}
			else
			{
				internalCacheItemVersion = default(InternalCacheItemVersion);
			}
			InternalCacheItemVersion internalCacheItemVersion2 = new InternalCacheItemVersion(internalCacheItemVersion);
			return internalCacheItemVersion2;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0003B638 File Offset: 0x00039838
		internal void AssignNewInternalCacheItemVersion()
		{
			this.NewCacheItem.Version = this.GetNewVersion();
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0003B64C File Offset: 0x0003984C
		internal void PreOperationCallBack()
		{
			if (this._preOperationCallBack != null)
			{
				if (this._oldExpiredCacheItem != null)
				{
					this._preOperationCallBack(this._oldExpiredCacheItem, this._newCacheItem, this._opState);
					return;
				}
				this._preOperationCallBack(this._oldCacheItem ?? this._lockPlaceHolder, this._newCacheItem ?? this._lockPlaceHolder, this._opState);
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0003B6BC File Offset: 0x000398BC
		internal void PostOperationCallBack()
		{
			ADMCacheItem admcacheItem;
			if (this._oldExpiredCacheItem != null)
			{
				admcacheItem = this._oldExpiredCacheItem;
			}
			else
			{
				admcacheItem = this._oldCacheItem;
			}
			if (this._postOperationCallBack != null)
			{
				this._postOperationCallBack(admcacheItem ?? this._lockPlaceHolder, this._newCacheItem ?? this._lockPlaceHolder, this._opState);
			}
			if (this.Consistency == CommitType.ImmediateCommit)
			{
				ADMCacheItem admcacheItem2 = this.NewCacheItem ?? this._lockPlaceHolder;
				if (admcacheItem2 != null)
				{
					admcacheItem2.IsCommitted = true;
					admcacheItem2.LastCommittedItem = null;
				}
				if (admcacheItem != null)
				{
					admcacheItem.UncommittedCacheItem = null;
				}
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0003B74D File Offset: 0x0003994D
		internal void SetConsistencyImmediateCommit()
		{
			this.Consistency = CommitType.ImmediateCommit;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0003B756 File Offset: 0x00039956
		internal void OperationDone()
		{
			this._operationCompleted = true;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0003B75F File Offset: 0x0003995F
		internal void SetOldCacheItem(ADMCacheItem dMCacheItem)
		{
			this._oldCacheItem = dMCacheItem;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0003B768 File Offset: 0x00039968
		internal void Reset()
		{
			this._retry = false;
			this._oldCacheItem = null;
			this._oldExpiredCacheItem = null;
			this._lockPlaceHolder = null;
			this._directoryLatched = null;
			this._operationCompleted = false;
			this.LatchReleased = false;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0003B79C File Offset: 0x0003999C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OperationInfo = [ (OperationType =");
			stringBuilder.Append(this._operationType.ToString());
			stringBuilder.Append(") (Key = ");
			stringBuilder.Append(this.SearchKey);
			stringBuilder.Append(") (OldCacheItem = ");
			if (this.OldCacheItem != null)
			{
				stringBuilder.Append(this.OldCacheItem);
			}
			else
			{
				stringBuilder.Append("null");
			}
			stringBuilder.Append(") (OldExpiredCacheItem = ");
			if (this.OldExpiredCacheItem != null)
			{
				stringBuilder.Append(this.OldExpiredCacheItem);
			}
			else
			{
				stringBuilder.Append("null");
			}
			stringBuilder.Append(") (NewCacheItem = ");
			if (this.NewCacheItem != null)
			{
				stringBuilder.Append(this.NewCacheItem);
			}
			else
			{
				stringBuilder.Append("null");
			}
			stringBuilder.Append(") (LockPlaceholderObject = ");
			if (this.LockPlaceHolderObject != null)
			{
				stringBuilder.Append(this.LockPlaceHolderObject);
			}
			else
			{
				stringBuilder.Append("null");
			}
			stringBuilder.Append(") (commitType = ");
			stringBuilder.Append(this.Consistency);
			if (this._param1 != null)
			{
				DataCacheLockHandle dataCacheLockHandle = this._param1 as DataCacheLockHandle;
				if (dataCacheLockHandle != null)
				{
					stringBuilder.Append(") (Param1:LockHandle = ");
					stringBuilder.Append(dataCacheLockHandle.ToString());
				}
				else
				{
					stringBuilder.Append(") (Param1:TimeSpan/DateTime = ");
					stringBuilder.Append(this._param1.ToString());
				}
			}
			if (this._param2 != null)
			{
				DataCacheLockHandle dataCacheLockHandle2 = this._param2 as DataCacheLockHandle;
				if (dataCacheLockHandle2 != null)
				{
					stringBuilder.Append(") (Param1:LockHandle = ");
					stringBuilder.Append(dataCacheLockHandle2.ToString());
				}
				else
				{
					stringBuilder.Append(") (Param1:TimeSpan/DateTime = ");
					stringBuilder.Append(this._param2.ToString());
				}
			}
			stringBuilder.Append(") (opState = ");
			if (this._opState != null)
			{
				stringBuilder.Append(this._opState.ToString());
			}
			else
			{
				stringBuilder.Append("null");
			}
			stringBuilder.Append(") (Force Lock = ");
			stringBuilder.Append(this.LockKey);
			if (this._newCacheItem != null)
			{
				stringBuilder.Append(") (TTL = ");
				stringBuilder.Append(this._newCacheItem.TimeToLive);
			}
			stringBuilder.Append(") ]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0003B9EC File Offset: 0x00039BEC
		internal void PreserveDataForRollback()
		{
			ADMCacheItem admcacheItem = this.OldCacheItem ?? this.OldExpiredCacheItem;
			if (!this.LockKey)
			{
				admcacheItem = admcacheItem ?? this.LockPlaceHolderObject;
			}
			if (admcacheItem != null)
			{
				this._oldLockHandle = admcacheItem.GetDMLockHandle();
				this._oldTTL = admcacheItem.TimeToLive;
				this._oldExtensionTimeout = admcacheItem.ExtensionTimeout;
				this._oldLockExpirationTime = admcacheItem.LockExpirationTime;
				this._oldFlag = admcacheItem.Flags;
			}
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0003BA60 File Offset: 0x00039C60
		internal AMDHObjectNode RollbackToOldObjectNode()
		{
			ADMCacheItem admcacheItem = this.OldCacheItem;
			admcacheItem = admcacheItem ?? this.OldExpiredCacheItem;
			if (!this.LockKey)
			{
				admcacheItem = admcacheItem ?? this.LockPlaceHolderObject;
			}
			if (admcacheItem != null)
			{
				admcacheItem.SetLockHandle(this._oldLockHandle);
				admcacheItem.LockExpirationTime = this._oldLockExpirationTime;
				admcacheItem.TimeToLive = this._oldTTL;
				admcacheItem.ExtensionTimeout = this._oldExtensionTimeout;
				admcacheItem.Flags = this._oldFlag;
				if (!admcacheItem.IsCommitted && Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning("DistributedCache.DataManager", "Rolling back to an object that is uncommitted. DM Operation = {0}. Old object = {1}", new object[] { this, admcacheItem });
				}
			}
			return admcacheItem;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0003BB10 File Offset: 0x00039D10
		internal void TakeCommitLockForProperties()
		{
			ADMCacheItem admcacheItem = this.OldCacheItem ?? this.LockPlaceHolderObject;
			if (this.Consistency == CommitType.DeferredCommit && !admcacheItem.TakeCommitLock())
			{
				this.OldCacheItem.UncommittedCacheItem = null;
				throw DMGlobal.GetException(2009);
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0003BB55 File Offset: 0x00039D55
		internal void TakeLatch(int slot, MDHDirectoryNode dir)
		{
			this.DirectoryLatched = dir;
			dir.LatchSlot(slot);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0003BB65 File Offset: 0x00039D65
		internal void ReleaseLatch()
		{
			if (!this.LatchReleased)
			{
				this.DirectoryLatched.UnLatchSlot(this.DirectoryLatched.GetSlotNumber(this.HashCode));
				this.LatchReleased = true;
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0003BB92 File Offset: 0x00039D92
		internal void TryReleaseLatch()
		{
			if (this.Consistency == CommitType.DeferredCommit)
			{
				this.ReleaseLatch();
			}
		}

		// Token: 0x04000B9D RID: 2973
		private MDHOperationType _operationType;

		// Token: 0x04000B9E RID: 2974
		private ADMCacheItem _oldCacheItem;

		// Token: 0x04000B9F RID: 2975
		private ADMCacheItem _oldExpiredCacheItem;

		// Token: 0x04000BA0 RID: 2976
		private ADMCacheItem _newCacheItem;

		// Token: 0x04000BA1 RID: 2977
		private ADMCacheItem _lockPlaceHolder;

		// Token: 0x04000BA2 RID: 2978
		private int _hashCode;

		// Token: 0x04000BA3 RID: 2979
		public bool LockKey;

		// Token: 0x04000BA4 RID: 2980
		private object _searchKey;

		// Token: 0x04000BA5 RID: 2981
		private DMOperationCallBack _preOperationCallBack;

		// Token: 0x04000BA6 RID: 2982
		private DMOperationCallBack _postOperationCallBack;

		// Token: 0x04000BA7 RID: 2983
		private MultiDirectoryHashtable _parentHashTable;

		// Token: 0x04000BA8 RID: 2984
		private CommitType _commitType;

		// Token: 0x04000BA9 RID: 2985
		private object _param1;

		// Token: 0x04000BAA RID: 2986
		private object _param2;

		// Token: 0x04000BAB RID: 2987
		private object _param3;

		// Token: 0x04000BAC RID: 2988
		private object _param4;

		// Token: 0x04000BAD RID: 2989
		private object _param5;

		// Token: 0x04000BAE RID: 2990
		private object _param6;

		// Token: 0x04000BAF RID: 2991
		private bool _operationCompleted;

		// Token: 0x04000BB0 RID: 2992
		private object _opState;

		// Token: 0x04000BB1 RID: 2993
		private bool _retry;

		// Token: 0x04000BB2 RID: 2994
		public bool ParentLinkageCheckRequired;

		// Token: 0x04000BB3 RID: 2995
		public bool LatchReleased;

		// Token: 0x04000BB4 RID: 2996
		public InternalCacheItemVersion Version;

		// Token: 0x04000BB5 RID: 2997
		private MDHDirectoryNode _directoryLatched;

		// Token: 0x04000BB6 RID: 2998
		private DMLockHandle _oldLockHandle;

		// Token: 0x04000BB7 RID: 2999
		private long _oldTTL;

		// Token: 0x04000BB8 RID: 3000
		private long _oldExtensionTimeout;

		// Token: 0x04000BB9 RID: 3001
		private long _oldLockExpirationTime;

		// Token: 0x04000BBA RID: 3002
		private CacheItemFlag _oldFlag;
	}
}
