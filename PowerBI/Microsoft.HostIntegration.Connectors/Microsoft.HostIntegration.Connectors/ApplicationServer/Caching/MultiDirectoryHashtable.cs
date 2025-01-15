using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000245 RID: 581
	internal sealed class MultiDirectoryHashtable : IHashtable
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0003BBA2 File Offset: 0x00039DA2
		internal MDHDirectoryNode Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0003BBAA File Offset: 0x00039DAA
		// (set) Token: 0x06001355 RID: 4949 RVA: 0x0003BBB2 File Offset: 0x00039DB2
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

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0003BBBB File Offset: 0x00039DBB
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x0003BBC3 File Offset: 0x00039DC3
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

		// Token: 0x06001358 RID: 4952 RVA: 0x0003BBCC File Offset: 0x00039DCC
		public MultiDirectoryHashtable(IDirectoryNodeFactory directoryNodeFactory)
		{
			this._directoryNodeFactory = directoryNodeFactory;
			this._root = this._directoryNodeFactory.GetDirectory(0, null, -1);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0003BC14 File Offset: 0x00039E14
		public MultiDirectoryHashtable(IStoreSchema iSchema, IDirectoryNodeFactory directoryNodeFactory)
		{
			this._directoryNodeFactory = directoryNodeFactory;
			this._root = this._directoryNodeFactory.GetDirectory(0, null, -1);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0003BC5C File Offset: 0x00039E5C
		private static void CheckLock(bool check)
		{
			if (check)
			{
				throw DMGlobal.GetException(2009);
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0003BC6C File Offset: 0x00039E6C
		internal bool PreProcess(AMDHObjectNode oldObjectNode, ref AMDHObjectNode newObjectNode, ref MDHOperationInfo operationInfo)
		{
			bool flag = false;
			if (oldObjectNode != null)
			{
				operationInfo.OldCacheItem = oldObjectNode.GetCacheItem();
				flag = oldObjectNode.IsCommitLocked;
			}
			operationInfo.PreserveDataForRollback();
			ADMCacheItem admcacheItem = oldObjectNode as ADMCacheItem;
			RequestBody requestBody = operationInfo.OpState as RequestBody;
			if (requestBody != null)
			{
				VelocityDiagnostics.Publish(DiagEventName.DMOperation, true, requestBody.RequestStates, TraceLevel.Info, "DistributedCache.DataManager", "DM Operation = {0}, OldObjectNode = {1}", new object[] { operationInfo, oldObjectNode });
			}
			else if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.DataManager", "DM Operation = {0}, OldObjectNode = {1}", new object[] { operationInfo, oldObjectNode });
			}
			switch (operationInfo.OperationType)
			{
			case MDHOperationType.ADD:
				MultiDirectoryHashtable.CheckLock(flag);
				if (operationInfo.OldCacheItem != null)
				{
					throw MultiDirectoryHashtable.GetException(2001);
				}
				operationInfo.AssignNewInternalCacheItemVersion();
				newObjectNode = operationInfo.GetObjectNode();
				return true;
			case MDHOperationType.DELETE:
				MultiDirectoryHashtable.CheckLock(flag);
				if (operationInfo.OldCacheItem != null)
				{
					newObjectNode = operationInfo.GetObjectNode();
					if (newObjectNode != null)
					{
						newObjectNode.IsItemGettingDeleted = true;
					}
					return true;
				}
				MultiDirectoryHashtable.FreeResource(ref operationInfo);
				return false;
			case MDHOperationType.UPSERT:
				MultiDirectoryHashtable.CheckLock(flag);
				operationInfo.AssignNewInternalCacheItemVersion();
				newObjectNode = operationInfo.GetObjectNode();
				return true;
			case MDHOperationType.VERSION_DELETE:
				MultiDirectoryHashtable.CheckLock(flag);
				if (operationInfo.OldCacheItem != null)
				{
					MultiDirectoryHashtable.CheckVersion(ref operationInfo);
					newObjectNode = operationInfo.GetObjectNode();
					if (newObjectNode != null)
					{
						newObjectNode.IsItemGettingDeleted = true;
					}
					return true;
				}
				MultiDirectoryHashtable.FreeResource(ref operationInfo);
				return false;
			case MDHOperationType.VERSION_UPSERT:
				MultiDirectoryHashtable.CheckLock(flag);
				MultiDirectoryHashtable.CheckVersion(ref operationInfo);
				operationInfo.AssignNewInternalCacheItemVersion();
				newObjectNode = operationInfo.GetObjectNode();
				return true;
			case MDHOperationType.COMMIT:
			case MDHOperationType.COMMIT_DELETE:
			{
				ADMCacheItem admcacheItem2 = null;
				ADMCacheItem admcacheItem3 = null;
				if (oldObjectNode != null)
				{
					oldObjectNode.Commit(out admcacheItem2, out admcacheItem3);
					operationInfo.OldCacheItem = admcacheItem2;
					operationInfo.NewCacheItem = admcacheItem3;
					newObjectNode = admcacheItem3;
					return true;
				}
				return false;
			}
			case MDHOperationType.GET_AND_LOCK:
			{
				MultiDirectoryHashtable.CheckLock(flag);
				admcacheItem = operationInfo.OldCacheItem ?? operationInfo.LockPlaceHolderObject;
				bool lockKey = operationInfo.LockKey;
				if (admcacheItem == null)
				{
					if (!lockKey)
					{
						throw MultiDirectoryHashtable.GetException(2002);
					}
					admcacheItem = this.GetPlaceHolderObject(operationInfo.SearchKey);
					operationInfo.LockPlaceHolderObject = admcacheItem;
				}
				else if (admcacheItem.IsLockPlaceHolderObject && !lockKey)
				{
					throw MultiDirectoryHashtable.GetException(2002);
				}
				if (admcacheItem.IsLocked())
				{
					if (admcacheItem.IsCurrentLockNotExpired())
					{
						throw MultiDirectoryHashtable.GetException(2004);
					}
					admcacheItem.ClearLock();
				}
				admcacheItem.SetLock((TimeSpan)operationInfo.Param1);
				newObjectNode = admcacheItem;
				operationInfo.TakeCommitLockForProperties();
				return true;
			}
			case MDHOperationType.PUT_AND_UNLOCK:
			case MDHOperationType.INTERNAL_PUT_AND_UNLOCK:
				MultiDirectoryHashtable.CheckLock(flag);
				admcacheItem = operationInfo.OldCacheItem ?? operationInfo.LockPlaceHolderObject;
				if (admcacheItem == null)
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					throw MultiDirectoryHashtable.GetException(2002);
				}
				if (!admcacheItem.IsLocked() || !admcacheItem.IsCurrentLockNotExpired() || admcacheItem.IsRtLockPlaceHolderObject)
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					throw MultiDirectoryHashtable.GetException(2007);
				}
				if (admcacheItem.IsLockHandleEqual((DataCacheLockHandle)operationInfo.Param1))
				{
					admcacheItem.ClearLock();
					if (operationInfo.OperationType == MDHOperationType.PUT_AND_UNLOCK)
					{
						operationInfo.AssignNewInternalCacheItemVersion();
					}
					newObjectNode = operationInfo.GetObjectNode();
					return true;
				}
				MultiDirectoryHashtable.FreeResource(ref operationInfo);
				throw MultiDirectoryHashtable.GetException(2006);
			case MDHOperationType.UNLOCK:
				MultiDirectoryHashtable.CheckLock(flag);
				admcacheItem = operationInfo.OldCacheItem ?? operationInfo.LockPlaceHolderObject;
				if (admcacheItem == null)
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					throw MultiDirectoryHashtable.GetException(2002);
				}
				if (!admcacheItem.IsLocked() || !admcacheItem.IsCurrentLockNotExpired())
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					throw MultiDirectoryHashtable.GetException(2007);
				}
				if (admcacheItem.IsLockHandleEqual((DataCacheLockHandle)operationInfo.Param1))
				{
					admcacheItem.ClearLock();
					newObjectNode = admcacheItem;
					operationInfo.TakeCommitLockForProperties();
					if (operationInfo.Param2 != null)
					{
						long timeStampCounterFromDateTime = Utility.GetTimeStampCounterFromDateTime((DateTime)operationInfo.Param2);
						if (timeStampCounterFromDateTime != 0L)
						{
							admcacheItem.TimeToLive = timeStampCounterFromDateTime;
							if (ExpirationType.SlidingExpiration == this.ExpirationType)
							{
								admcacheItem.ExtensionTimeout = Utility.ConvertToTimeStampCounterFromDateTime((DateTime)operationInfo.Param2);
							}
						}
					}
					return true;
				}
				MultiDirectoryHashtable.FreeResource(ref operationInfo);
				throw MultiDirectoryHashtable.GetException(2006);
			case MDHOperationType.INTERNAL_LOCK_UPDATE:
			{
				bool flag2 = false;
				if (operationInfo.LockKey && (admcacheItem == null || (!admcacheItem.IsLockPlaceHolderObject && operationInfo.Version.CompareTo(admcacheItem.Version) > 0)))
				{
					admcacheItem = this.GetPlaceHolderObject(operationInfo.SearchKey);
					operationInfo.LockPlaceHolderObject = admcacheItem;
					flag2 = true;
				}
				if (admcacheItem == null)
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					throw MultiDirectoryHashtable.GetException(2002);
				}
				admcacheItem.SetLockHandle((DataCacheLockHandle)operationInfo.Param2);
				newObjectNode = admcacheItem;
				admcacheItem.SetLockTimeOut((TimeSpan)operationInfo.Param1);
				admcacheItem.Version = operationInfo.Version;
				return flag2;
			}
			case MDHOperationType.INTERNAL_UPSERT:
				MultiDirectoryHashtable.CheckLock(flag);
				newObjectNode = operationInfo.GetObjectNode();
				return true;
			case MDHOperationType.RESET_TIMEOUT:
				MultiDirectoryHashtable.CheckLock(flag);
				if (operationInfo.OldCacheItem == null)
				{
					throw MultiDirectoryHashtable.GetException(2002);
				}
				admcacheItem = operationInfo.OldCacheItem;
				admcacheItem.TimeToLive = Utility.GetTimeStampCounterFromDateTime((DateTime)operationInfo.Param1);
				if (ExpirationType.SlidingExpiration == this.ExpirationType)
				{
					admcacheItem.ExtensionTimeout = Utility.ConvertToTimeStampCounterFromDateTime((DateTime)operationInfo.Param1);
				}
				newObjectNode = oldObjectNode;
				operationInfo.TakeCommitLockForProperties();
				return true;
			case MDHOperationType.FORCE_UPSERT:
				newObjectNode = operationInfo.NewCacheItem;
				return true;
			case MDHOperationType.INTERNAL_DELETE:
				MultiDirectoryHashtable.CheckLock(flag);
				admcacheItem = operationInfo.OldCacheItem ?? operationInfo.LockPlaceHolderObject;
				if (admcacheItem != null || operationInfo.OldExpiredCacheItem != null)
				{
					if (operationInfo.OldExpiredCacheItem != null)
					{
						if (!MultiDirectoryHashtable.CheckVersionMatch(operationInfo.OldExpiredCacheItem, (InternalCacheItemVersion)operationInfo.Param1))
						{
							return false;
						}
						operationInfo.SetOldCacheItem(operationInfo.OldExpiredCacheItem);
					}
					else if (!MultiDirectoryHashtable.CheckVersionMatch(admcacheItem, (InternalCacheItemVersion)operationInfo.Param1))
					{
						return false;
					}
					newObjectNode = operationInfo.GetObjectNode();
					if (newObjectNode != null)
					{
						newObjectNode.IsItemGettingDeleted = true;
					}
					return true;
				}
				MultiDirectoryHashtable.FreeResource(ref operationInfo);
				return false;
			case MDHOperationType.FORCED_UNLOCK:
				admcacheItem = operationInfo.OldCacheItem ?? operationInfo.LockPlaceHolderObject;
				if (admcacheItem == null)
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					throw MultiDirectoryHashtable.GetException(2002);
				}
				admcacheItem.ForcedUnsetLockHandle((DataCacheLockHandle)operationInfo.Param1);
				if (operationInfo.Param2 != null)
				{
					DateTime dateTime = (DateTime)operationInfo.Param2;
					long timeStampCounterFromDateTime2 = Utility.GetTimeStampCounterFromDateTime(dateTime);
					if (timeStampCounterFromDateTime2 != 0L)
					{
						admcacheItem.TimeToLive = timeStampCounterFromDateTime2;
						if (ExpirationType.SlidingExpiration == this.ExpirationType)
						{
							admcacheItem.ExtensionTimeout = Utility.ConvertToTimeStampCounterFromDateTime(dateTime);
						}
					}
				}
				if (!admcacheItem.IsLockPlaceHolderObject)
				{
					newObjectNode = oldObjectNode;
				}
				else
				{
					newObjectNode = null;
				}
				return true;
			case MDHOperationType.FORCED_RESET_TIMEOUT:
				if (admcacheItem == null)
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					throw MultiDirectoryHashtable.GetException(2002);
				}
				admcacheItem.TimeToLive = Utility.GetTimeStampCounterFromDateTime((DateTime)operationInfo.Param1);
				if (ExpirationType.SlidingExpiration == this.ExpirationType)
				{
					admcacheItem.ExtensionTimeout = Utility.ConvertToTimeStampCounterFromDateTime((DateTime)operationInfo.Param1);
				}
				newObjectNode = oldObjectNode;
				return false;
			case MDHOperationType.BEGIN_DELETE:
				MultiDirectoryHashtable.CheckLock(flag);
				if (oldObjectNode == null)
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					return false;
				}
				operationInfo.SetOldCacheItem(oldObjectNode.DMCacheItem);
				if (!MultiDirectoryHashtable.CheckVersionMatch(oldObjectNode.DMCacheItem, (InternalCacheItemVersion)operationInfo.Param1))
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					return false;
				}
				newObjectNode = operationInfo.GetObjectNode();
				newObjectNode.IsItemGettingDeleted = true;
				return true;
			case MDHOperationType.ABORT_DELETE:
				if (oldObjectNode != null)
				{
					oldObjectNode.IsItemGettingDeleted = false;
					oldObjectNode.DMCacheItem.ReleaseCommitLock();
				}
				return false;
			case MDHOperationType.LOCK_DELETE:
				MultiDirectoryHashtable.CheckLock(flag);
				admcacheItem = operationInfo.OldCacheItem ?? operationInfo.LockPlaceHolderObject;
				if (admcacheItem == null)
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					return false;
				}
				if (!admcacheItem.IsLocked() || !admcacheItem.IsCurrentLockNotExpired())
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					throw MultiDirectoryHashtable.GetException(2007);
				}
				if (admcacheItem.IsLockHandleEqual((DataCacheLockHandle)operationInfo.Param1))
				{
					newObjectNode = operationInfo.GetObjectNode();
					if (newObjectNode != null)
					{
						newObjectNode.IsItemGettingDeleted = true;
					}
					return true;
				}
				MultiDirectoryHashtable.FreeResource(ref operationInfo);
				throw MultiDirectoryHashtable.GetException(2006);
			case MDHOperationType.RT_LOCK:
				MultiDirectoryHashtable.CheckLock(flag);
				admcacheItem = operationInfo.OldCacheItem ?? operationInfo.LockPlaceHolderObject;
				if (admcacheItem == null)
				{
					admcacheItem = this.GetRTPlaceHolderObject(operationInfo.SearchKey);
					operationInfo.LockPlaceHolderObject = admcacheItem;
					admcacheItem.SetReadThroughLock((TimeSpan)operationInfo.Param1);
					newObjectNode = admcacheItem;
				}
				else
				{
					if (!admcacheItem.IsLockPlaceHolderObject)
					{
						throw MultiDirectoryHashtable.GetException(2011);
					}
					if (admcacheItem.IsLocked())
					{
						if (admcacheItem.IsCurrentLockExpired())
						{
							admcacheItem.ClearLock();
							admcacheItem.SetReadThroughLock((TimeSpan)operationInfo.Param1);
							newObjectNode = admcacheItem;
						}
						else
						{
							if (admcacheItem.IsRtLockPlaceHolderObject)
							{
								operationInfo.LockPlaceHolderObject = admcacheItem;
								return false;
							}
							if (admcacheItem.IsLockPlaceHolderObject)
							{
								throw MultiDirectoryHashtable.GetException(2010);
							}
						}
					}
					else
					{
						admcacheItem.SetReadThroughLock((TimeSpan)operationInfo.Param1);
						newObjectNode = admcacheItem;
					}
				}
				return true;
			case MDHOperationType.RT_UNLOCK:
				MultiDirectoryHashtable.CheckLock(flag);
				admcacheItem = operationInfo.OldCacheItem ?? operationInfo.LockPlaceHolderObject;
				if (admcacheItem == null)
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					throw MultiDirectoryHashtable.GetException(2012);
				}
				if (!admcacheItem.IsLocked() || !admcacheItem.IsCurrentLockNotExpired() || !admcacheItem.IsRtLockPlaceHolderObject)
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					throw MultiDirectoryHashtable.GetException(2012);
				}
				if (admcacheItem.IsLockHandleEqual((DataCacheLockHandle)operationInfo.Param1))
				{
					admcacheItem.ClearLock();
					newObjectNode = null;
					return true;
				}
				MultiDirectoryHashtable.FreeResource(ref operationInfo);
				throw MultiDirectoryHashtable.GetException(2013);
			case MDHOperationType.RT_PUT_AND_UNLOCK:
				MultiDirectoryHashtable.CheckLock(flag);
				admcacheItem = operationInfo.OldCacheItem ?? operationInfo.LockPlaceHolderObject;
				if (admcacheItem == null || (admcacheItem.IsLocked() && admcacheItem.IsCurrentLockExpired()))
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					throw MultiDirectoryHashtable.GetException(2012);
				}
				if (!admcacheItem.IsLocked())
				{
					newObjectNode = admcacheItem;
					return false;
				}
				if (!admcacheItem.IsRtLockPlaceHolderObject)
				{
					throw MultiDirectoryHashtable.GetException(2010);
				}
				if (admcacheItem.IsLockHandleEqual((DataCacheLockHandle)operationInfo.Param1))
				{
					admcacheItem.ClearLock();
					operationInfo.AssignNewInternalCacheItemVersion();
					newObjectNode = operationInfo.GetObjectNode();
					return true;
				}
				MultiDirectoryHashtable.FreeResource(ref operationInfo);
				throw MultiDirectoryHashtable.GetException(2013);
			case MDHOperationType.FORCED_DELETE:
				MultiDirectoryHashtable.CheckLock(flag);
				if ((operationInfo.OldCacheItem ?? operationInfo.LockPlaceHolderObject) == null)
				{
					MultiDirectoryHashtable.FreeResource(ref operationInfo);
					return false;
				}
				newObjectNode = operationInfo.GetObjectNode();
				if (newObjectNode != null)
				{
					newObjectNode.IsItemGettingDeleted = true;
				}
				return true;
			case MDHOperationType.INCREMENT:
			{
				MultiDirectoryHashtable.CheckLock(flag);
				AOMCacheItem aomcacheItem = (AOMCacheItem)operationInfo.OldCacheItem;
				byte[][] array = ProtocolHandler.PerformOperation(aomcacheItem, (InternalCacheItemVersion)operationInfo.Param2, operationInfo.OpState);
				if (aomcacheItem == null)
				{
					operationInfo.NewCacheItem.Reinit((Key)operationInfo.Param3, array, operationInfo.NewCacheItem.TimeToLive, operationInfo.NewCacheItem.ExtensionTimeout, null);
				}
				else
				{
					SerializationCategory serializationCategory = ProtocolHandler.GetSerializationCategory(operationInfo.OpState);
					if (serializationCategory == SerializationCategory.Memcache)
					{
						operationInfo.NewCacheItem.Reinit(aomcacheItem.Key, array, aomcacheItem.TimeToLive, aomcacheItem.ExtensionTimeout, aomcacheItem.Tags);
					}
					else
					{
						operationInfo.NewCacheItem.Reinit(aomcacheItem.Key, array, operationInfo.NewCacheItem.TimeToLive, operationInfo.NewCacheItem.ExtensionTimeout, aomcacheItem.Tags);
					}
				}
				newObjectNode = operationInfo.GetObjectNode();
				return true;
			}
			case MDHOperationType.CONCATENATE:
			{
				MultiDirectoryHashtable.CheckLock(flag);
				AOMCacheItem aomcacheItem2 = (AOMCacheItem)operationInfo.OldCacheItem;
				byte[][] array2 = ProtocolHandler.PerformOperation(aomcacheItem2, (InternalCacheItemVersion)operationInfo.Param2, operationInfo.OpState);
				ReleaseAssert.IsTrue(aomcacheItem2 != null, "Throw exception if the item does not exist on primary for concat");
				SerializationCategory serializationCategory2 = ProtocolHandler.GetSerializationCategory(operationInfo.OpState);
				if (serializationCategory2 == SerializationCategory.Memcache)
				{
					operationInfo.NewCacheItem.Reinit(aomcacheItem2.Key, array2, aomcacheItem2.TimeToLive, operationInfo.NewCacheItem.ExtensionTimeout, aomcacheItem2.Tags);
				}
				else
				{
					operationInfo.NewCacheItem.Reinit(aomcacheItem2.Key, array2, operationInfo.NewCacheItem.TimeToLive, operationInfo.NewCacheItem.ExtensionTimeout, aomcacheItem2.Tags);
				}
				newObjectNode = operationInfo.GetObjectNode();
				return true;
			}
			case MDHOperationType.VERSION_REPLACE:
				MultiDirectoryHashtable.CheckLock(flag);
				if (operationInfo.OldCacheItem == null)
				{
					throw MultiDirectoryHashtable.GetException(2002);
				}
				if ((InternalCacheItemVersion)operationInfo.Param1 != InternalCacheItemVersion.Null)
				{
					MultiDirectoryHashtable.CheckVersion(ref operationInfo);
				}
				operationInfo.AssignNewInternalCacheItemVersion();
				newObjectNode = operationInfo.GetObjectNode();
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0003C7AC File Offset: 0x0003A9AC
		private ADMCacheItem GetPlaceHolderObject(object key)
		{
			ADMCacheItem admcacheItem = this._lockCallBack(key);
			admcacheItem.IsLockPlaceHolderObject = true;
			return admcacheItem;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0003C7D0 File Offset: 0x0003A9D0
		private ADMCacheItem GetRTPlaceHolderObject(object key)
		{
			ADMCacheItem placeHolderObject = this.GetPlaceHolderObject(key);
			placeHolderObject.IsRtLockPlaceHolderObject = true;
			return placeHolderObject;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0003C7ED File Offset: 0x0003A9ED
		public void RegisterLockPlaceHolderObject(GetLockPlaceHolderObject callback)
		{
			this._lockCallBack = callback;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0003C7F8 File Offset: 0x0003A9F8
		private MDHDirectoryNode CreateSubdirectoryToResolveSlotCollision(int diff, MDHDirectoryNode parent, int parentIndex)
		{
			int num = 0;
			int num2 = MDHGlobals.IndexMasks[0];
			while ((diff & num2) == 0)
			{
				num++;
				num2 = MDHGlobals.IndexMasks[num];
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose("DistributedCache.DataManager", "Creating a subdirectory to resolve collision");
			}
			return this._directoryNodeFactory.GetDirectory((short)num, parent, (short)parentIndex);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0003C84C File Offset: 0x0003AA4C
		internal static void AddConflictNode(int slot, MDHDirectoryNode dir, ref MDHOperationInfo operationInfo)
		{
			MDHLeafNode mdhleafNode = (MDHLeafNode)dir.GetNodeInSlot(slot);
			AMDHObjectNode amdhobjectNode = null;
			MDHConflictingHashNode mdhconflictingHashNode = null;
			if (mdhleafNode != null && mdhleafNode.NodeType == MDHNodeType.MDHConflictingHashNode)
			{
				mdhconflictingHashNode = (MDHConflictingHashNode)mdhleafNode;
			}
			if (mdhconflictingHashNode != null)
			{
				mdhconflictingHashNode.Put(ref operationInfo);
				return;
			}
			if (operationInfo.ParentHashTable.PreProcess(null, ref amdhobjectNode, ref operationInfo))
			{
				operationInfo.PreOperationCallBack();
				if (amdhobjectNode != null)
				{
					AMDHObjectNode amdhobjectNode2 = null;
					if (mdhleafNode != null && mdhleafNode.NodeType == MDHNodeType.MDHObjectNode)
					{
						amdhobjectNode2 = (AMDHObjectNode)mdhleafNode;
					}
					if (amdhobjectNode2 != null)
					{
						dir.PutNodeInSlot(slot, new MDHConflictingHashNode(amdhobjectNode2, amdhobjectNode)
						{
							Parent = dir,
							ParentIndex = slot
						});
						operationInfo.TryReleaseLatch();
					}
				}
				operationInfo.OperationDone();
				operationInfo.PostOperationCallBack();
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0003C8EC File Offset: 0x0003AAEC
		internal static void PutNodeInSlot(ref MDHOperationInfo operationInfo, MDHDirectoryNode dir, int slotIndex)
		{
			MDHNode nodeInSlot = dir.GetNodeInSlot(slotIndex);
			AMDHObjectNode amdhobjectNode = null;
			if (nodeInSlot != null && nodeInSlot.NodeType == MDHNodeType.MDHObjectNode)
			{
				amdhobjectNode = (AMDHObjectNode)nodeInSlot;
			}
			AMDHObjectNode amdhobjectNode2 = null;
			if (nodeInSlot == null || amdhobjectNode != null)
			{
				bool flag = operationInfo.ParentHashTable.PreProcess(amdhobjectNode, ref amdhobjectNode2, ref operationInfo);
				if (flag)
				{
					if (amdhobjectNode2 != null)
					{
						dir.PutNodeInSlot(slotIndex, amdhobjectNode2);
						if (!operationInfo.ParentLinkageCheckRequired || dir.AtomicallyCheckParentalLinkage())
						{
							operationInfo.TryReleaseLatch();
							operationInfo.PreOperationCallBack();
							operationInfo.OperationDone();
							operationInfo.PostOperationCallBack();
							return;
						}
						dir.PutNodeInSlot(slotIndex, operationInfo.RollbackToOldObjectNode());
						operationInfo.Retry = true;
						return;
					}
					else
					{
						operationInfo.PreOperationCallBack();
						dir.PutNodeInSlot(slotIndex, amdhobjectNode2);
						operationInfo.TryReleaseLatch();
						operationInfo.OperationDone();
						operationInfo.PostOperationCallBack();
					}
				}
				return;
			}
			MDHNode nodeInSlot2 = dir.GetNodeInSlot(slotIndex);
			if (nodeInSlot2 != null && nodeInSlot2.NodeType == MDHNodeType.MDHConflictingHashNode)
			{
				((MDHConflictingHashNode)dir.GetNodeInSlot(slotIndex)).Put(ref operationInfo);
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0003C9C5 File Offset: 0x0003ABC5
		internal void VerifyState()
		{
			this._root.VerifyState();
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0003C9D4 File Offset: 0x0003ABD4
		internal void SplitDirectory(int slot, MDHDirectoryNode dir, ref MDHOperationInfo operationInfo)
		{
			MDHLeafNode mdhleafNode = (MDHLeafNode)dir.GetNodeInSlot(slot);
			AMDHObjectNode amdhobjectNode = null;
			if (operationInfo.ParentHashTable.PreProcess(null, ref amdhobjectNode, ref operationInfo))
			{
				operationInfo.PreOperationCallBack();
				if (amdhobjectNode != null)
				{
					int num = operationInfo.HashCode ^ mdhleafNode.HashCode;
					MDHDirectoryNode mdhdirectoryNode = this.CreateSubdirectoryToResolveSlotCollision(num, dir, slot);
					mdhdirectoryNode.PutNode(amdhobjectNode);
					int num2 = mdhdirectoryNode.PutNode(mdhleafNode);
					MDHConflictingHashNode mdhconflictingHashNode = null;
					if (mdhleafNode != null && mdhleafNode.NodeType == MDHNodeType.MDHConflictingHashNode)
					{
						mdhconflictingHashNode = (MDHConflictingHashNode)mdhleafNode;
					}
					if (mdhconflictingHashNode != null)
					{
						mdhconflictingHashNode.Parent = mdhdirectoryNode;
						mdhconflictingHashNode.ParentIndex = num2;
					}
					dir.PutNodeInSlot(slot, mdhdirectoryNode);
					operationInfo.TryReleaseLatch();
					Interlocked.Add(ref this._noOfSplits, 1);
				}
				operationInfo.OperationDone();
				operationInfo.PostOperationCallBack();
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0003CA8C File Offset: 0x0003AC8C
		private void GetDirectoryNodeSlot(int hkey, out MDHDirectoryNode dir, out int slot, out MDHNode node)
		{
			dir = this._root;
			slot = dir.GetSlotNumber(hkey);
			node = dir.GetNodeInSlot(slot);
			MDHDirectoryNode mdhdirectoryNode = null;
			while (node != null && node.NodeType == MDHNodeType.MDHDirectoryNode)
			{
				dir = (MDHDirectoryNode)node;
				slot = dir.GetSlotNumber(hkey);
				node = dir.GetNodeInSlot(slot);
				if (mdhdirectoryNode == dir)
				{
					Thread.Sleep(1);
				}
				mdhdirectoryNode = dir;
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0003CAFC File Offset: 0x0003ACFC
		private void PerformOperation(ref MDHOperationInfo operationInfo)
		{
			bool flag;
			do
			{
				flag = this.TryOperation(ref operationInfo);
				if (operationInfo.Retry)
				{
					flag = false;
				}
				if (!flag)
				{
					operationInfo.Reset();
				}
			}
			while (!flag);
			if (operationInfo.Consistency == CommitType.ImmediateCommit)
			{
				MultiDirectoryHashtable.UpdateEvictionRelatedData(ref operationInfo);
				if (ExpirationType.SlidingExpiration == this.ExpirationType)
				{
					MultiDirectoryHashtable.UpdateExpirationRelatedData(ref operationInfo);
				}
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0003CB45 File Offset: 0x0003AD45
		private static void UpdateEvictionRelatedData(ref MDHOperationInfo operationInfo)
		{
			if (operationInfo.NewCacheItem != null)
			{
				operationInfo.NewCacheItem.UpdateLastAccessTime();
			}
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0003CB5A File Offset: 0x0003AD5A
		private static void UpdateExpirationRelatedData(ref MDHOperationInfo operationInfo)
		{
			if (operationInfo.NewCacheItem != null)
			{
				operationInfo.NewCacheItem.UpdateTTL();
			}
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0003CB70 File Offset: 0x0003AD70
		private bool TryOperation(ref MDHOperationInfo operationInfo)
		{
			bool flag = false;
			MDHDirectoryNode mdhdirectoryNode;
			int num;
			MDHNode nodeInSlot;
			this.GetDirectoryNodeSlot(operationInfo.HashCode, out mdhdirectoryNode, out num, out nodeInSlot);
			operationInfo.TakeLatch(num, mdhdirectoryNode);
			try
			{
				nodeInSlot = mdhdirectoryNode.GetNodeInSlot(num);
				if (nodeInSlot == null)
				{
					MultiDirectoryHashtable.PutNodeInSlot(ref operationInfo, mdhdirectoryNode, num);
					flag = true;
				}
				else
				{
					MDHLeafNode mdhleafNode = null;
					if (nodeInSlot != null && nodeInSlot.NodeType != MDHNodeType.MDHDirectoryNode)
					{
						mdhleafNode = (MDHLeafNode)nodeInSlot;
					}
					if (mdhleafNode != null)
					{
						if (mdhleafNode.CompareHashCode(operationInfo.HashCode))
						{
							if ((operationInfo.OperationType == MDHOperationType.COMMIT && object.ReferenceEquals(mdhleafNode, operationInfo.OpState)) || mdhleafNode.CompareKey(operationInfo.SearchKey))
							{
								MultiDirectoryHashtable.PutNodeInSlot(ref operationInfo, mdhdirectoryNode, num);
							}
							else
							{
								EventLogWriter.WriteVerbose("DistributedCache.DataManager", "Adding conflict node.");
								MultiDirectoryHashtable.AddConflictNode(num, mdhdirectoryNode, ref operationInfo);
							}
						}
						else
						{
							this.SplitDirectory(num, mdhdirectoryNode, ref operationInfo);
						}
						flag = true;
					}
				}
			}
			catch (Exception ex)
			{
				if (operationInfo.OperationCompleted)
				{
					this.RollBack(ref operationInfo, ex);
				}
				MultiDirectoryHashtable.FreeResource(ref operationInfo);
				throw;
			}
			finally
			{
				operationInfo.ReleaseLatch();
			}
			return flag;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0003CC74 File Offset: 0x0003AE74
		private void RollBack(ref MDHOperationInfo operationInfo, Exception e)
		{
			bool flag = false;
			switch (operationInfo.OperationType)
			{
			case MDHOperationType.ADD:
			case MDHOperationType.DELETE:
			case MDHOperationType.UPSERT:
			case MDHOperationType.VERSION_DELETE:
			case MDHOperationType.VERSION_UPSERT:
			case MDHOperationType.GET_AND_LOCK:
			case MDHOperationType.PUT_AND_UNLOCK:
			case MDHOperationType.UNLOCK:
			case MDHOperationType.RESET_TIMEOUT:
			case MDHOperationType.LOCK_DELETE:
			case MDHOperationType.RT_LOCK:
			case MDHOperationType.RT_UNLOCK:
			case MDHOperationType.RT_PUT_AND_UNLOCK:
			case MDHOperationType.INTERNAL_PUT_AND_UNLOCK:
			case MDHOperationType.INCREMENT:
			case MDHOperationType.CONCATENATE:
			case MDHOperationType.VERSION_REPLACE:
				this.RollbackInternal(ref operationInfo);
				break;
			case MDHOperationType.COMMIT:
			case MDHOperationType.INTERNAL_UPSERT:
			case MDHOperationType.INTERNAL_DELETE:
			case MDHOperationType.FORCED_UNLOCK:
			case MDHOperationType.FORCED_RESET_TIMEOUT:
			case MDHOperationType.BEGIN_DELETE:
			case MDHOperationType.COMMIT_DELETE:
			case MDHOperationType.ABORT_DELETE:
				flag = true;
				break;
			case MDHOperationType.INTERNAL_LOCK_UPDATE:
			case MDHOperationType.FORCE_UPSERT:
				if (MultiDirectoryHashtable.IsNonFatalException(e))
				{
					this.RollbackInternal(ref operationInfo);
				}
				else
				{
					flag = true;
				}
				break;
			}
			if (flag)
			{
				ReleaseAssert.Fail("Operation of type {0} is not expected to fail during postoperation - {1}", new object[] { operationInfo.OperationType, e });
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0003CD4C File Offset: 0x0003AF4C
		private void RollbackInternal(ref MDHOperationInfo operationInfo)
		{
			AMDHObjectNode amdhobjectNode = operationInfo.RollbackToOldObjectNode();
			MDHDirectoryNode mdhdirectoryNode;
			int slotNumber;
			MDHNode nodeInSlot;
			bool flag;
			do
			{
				this.GetDirectoryNodeSlot(operationInfo.HashCode, out mdhdirectoryNode, out slotNumber, out nodeInSlot);
				slotNumber = mdhdirectoryNode.GetSlotNumber(operationInfo.HashCode);
				if (operationInfo.Consistency == CommitType.DeferredCommit || !mdhdirectoryNode.IsLatched(slotNumber))
				{
					mdhdirectoryNode.LatchSlot(slotNumber);
				}
				flag = true;
				nodeInSlot = mdhdirectoryNode.GetNodeInSlot(slotNumber);
				ReleaseAssert.IsTrue(nodeInSlot != null, "Could not find the node for rollback.");
				if (nodeInSlot.NodeType == MDHNodeType.MDHDirectoryNode)
				{
					mdhdirectoryNode.UnLatchSlot(slotNumber);
					flag = false;
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo("DistributedCache.DataManager", "Rollback operation could not locate the leaf node. Node found was :{0}. Retrying...", new object[] { nodeInSlot.NodeType });
					}
				}
			}
			while (!flag);
			if (nodeInSlot.NodeType == MDHNodeType.MDHObjectNode)
			{
				AMDHObjectNode amdhobjectNode2 = (AMDHObjectNode)nodeInSlot;
				ReleaseAssert.IsTrue(amdhobjectNode2.EqualsKey(operationInfo.HashCode, operationInfo.SearchKey), "Rollback mapped to wrong node");
				mdhdirectoryNode.PutNodeInSlot(slotNumber, amdhobjectNode);
			}
			else
			{
				EventLogWriter.WriteVerbose("DistributedCache.DataManager", "ConflictNodeRollback.DM Operation" + operationInfo);
				MDHConflictingHashNode mdhconflictingHashNode = (MDHConflictingHashNode)nodeInSlot;
				mdhconflictingHashNode.Put(operationInfo.SearchKey, amdhobjectNode);
			}
			mdhdirectoryNode.UnLatchSlot(slotNumber);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0003CE74 File Offset: 0x0003B074
		private static bool IsNonFatalException(Exception e)
		{
			DataCacheException ex = e as DataCacheException;
			return ex != null && ex.ErrorCode.Equals(18001);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00038C11 File Offset: 0x00036E11
		private static DataCacheException GetException(int errorCode)
		{
			return DMGlobal.GetException(errorCode);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0003CEA3 File Offset: 0x0003B0A3
		private static void CheckVersion(ref MDHOperationInfo operationInfo)
		{
			if (!MultiDirectoryHashtable.CheckVersionMatch(operationInfo.OldCacheItem, (InternalCacheItemVersion)operationInfo.Param1, operationInfo.Protocol))
			{
				throw MultiDirectoryHashtable.GetException(2003);
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0003CECE File Offset: 0x0003B0CE
		private static bool CheckVersionMatch(ADMCacheItem oldItem, InternalCacheItemVersion version)
		{
			return MultiDirectoryHashtable.CheckVersionMatch(oldItem, version, ProtocolType.Regular);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0003CED8 File Offset: 0x0003B0D8
		private static bool CheckVersionMatch(ADMCacheItem oldItem, InternalCacheItemVersion version, ProtocolType protocolType)
		{
			if (oldItem == null)
			{
				if (protocolType == ProtocolType.Memcache)
				{
					throw MultiDirectoryHashtable.GetException(2002);
				}
				return false;
			}
			else
			{
				InternalCacheItemVersion version2 = oldItem.Version;
				if (protocolType == ProtocolType.Memcache)
				{
					return version2.MemcacheEquals(version);
				}
				return version2.Equals(version);
			}
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x000036A9 File Offset: 0x000018A9
		private static void FreeResource(ref MDHOperationInfo operationInfo)
		{
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0003CF15 File Offset: 0x0003B115
		public void Add(ADMCacheItem dmCacheItem, DMOperationCallBack preOperation, DMOperationCallBack postOperation)
		{
			this.Add(dmCacheItem, preOperation, postOperation, null);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0003CF24 File Offset: 0x0003B124
		public void Add(ADMCacheItem dmCacheItem, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.ADD, dmCacheItem, dmCacheItem, preOperation, postOperation, this, opState);
			this.PerformOperation(ref mdhoperationInfo);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0003CF50 File Offset: 0x0003B150
		public ADMCacheItem IncrementDecrement(object key, ADMCacheItem dmCacheItem, object value, object initialValue, SerializationCategory serializationCategory, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.INCREMENT, dmCacheItem, dmCacheItem, preOperation, postOperation, this, opState)
			{
				Param2 = version,
				Param3 = key,
				Param4 = value,
				Param5 = serializationCategory,
				Param6 = initialValue
			};
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.NewCacheItem;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0003CFB8 File Offset: 0x0003B1B8
		public ADMCacheItem Concatenate(object key, ADMCacheItem dmCacheItem, object value, bool isAppend, SerializationCategory serializationCategory, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.CONCATENATE, dmCacheItem, dmCacheItem, preOperation, postOperation, this, opState)
			{
				Param2 = version,
				Param3 = key,
				Param4 = value,
				Param5 = isAppend,
				Param6 = serializationCategory
			};
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.NewCacheItem;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0003D024 File Offset: 0x0003B224
		public ADMCacheItem Get(object key)
		{
			int hashCode = key.GetHashCode();
			MDHDirectoryNode mdhdirectoryNode;
			int num;
			MDHNode mdhnode;
			this.GetDirectoryNodeSlot(hashCode, out mdhdirectoryNode, out num, out mdhnode);
			if (mdhnode == null)
			{
				return null;
			}
			ADMCacheItem admcacheItem = (ADMCacheItem)((MDHLeafNode)mdhnode).Get(key, hashCode);
			if (admcacheItem == null)
			{
				return null;
			}
			if (admcacheItem.IsLockPlaceHolderObject)
			{
				return null;
			}
			bool flag = false;
			if (admcacheItem != null)
			{
				DateTime utcNow = DateTime.UtcNow;
				if (admcacheItem.TimeToLive != 9223372036854775807L && ExpirationType.SlidingExpiration != this.ExpirationType)
				{
					flag = admcacheItem.IsExpired();
				}
				admcacheItem.UpdateLastAccessTime(utcNow);
				if (ExpirationType.SlidingExpiration == this.ExpirationType)
				{
					admcacheItem.UpdateTTL();
				}
			}
			if (!flag)
			{
				return admcacheItem;
			}
			return null;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0003D0C2 File Offset: 0x0003B2C2
		public ADMCacheItem Upsert(ADMCacheItem dmCacheItem, DMOperationCallBack preOperation, DMOperationCallBack postOperation)
		{
			return this.Upsert(dmCacheItem, preOperation, postOperation, null);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0003D0D0 File Offset: 0x0003B2D0
		public ADMCacheItem Upsert(ADMCacheItem dmCacheItem, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.UPSERT, dmCacheItem, dmCacheItem, preOperation, postOperation, this, opState);
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0003D100 File Offset: 0x0003B300
		public void ForceUpsert(ADMCacheItem dmCacheItem, DMOperationCallBack postOperation, object state)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.FORCE_UPSERT, dmCacheItem, dmCacheItem, null, postOperation, this, state);
			mdhoperationInfo.SetConsistencyImmediateCommit();
			this.PerformOperation(ref mdhoperationInfo);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0003D130 File Offset: 0x0003B330
		public ADMCacheItem ForceDelete(object key, DMOperationCallBack postOperation)
		{
			return this.ForceDelete(key, postOperation, null);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0003D13C File Offset: 0x0003B33C
		public ADMCacheItem ForceDelete(object key, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.FORCED_DELETE, key, null, null, postOperation, this, opState);
			mdhoperationInfo.SetConsistencyImmediateCommit();
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem ?? mdhoperationInfo.OldExpiredCacheItem;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0003D180 File Offset: 0x0003B380
		public bool ForcedUnlock(object key, DataCacheLockHandle lockHandle, DateTime objectTimeOut, DMOperationCallBack postOp, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.FORCED_UNLOCK, key, null, null, postOp, this, opState);
			mdhoperationInfo.Param1 = lockHandle;
			mdhoperationInfo.Param2 = objectTimeOut;
			mdhoperationInfo.SetConsistencyImmediateCommit();
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem != null;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0003D1D4 File Offset: 0x0003B3D4
		public bool ForceResetTimeOut(object key, DateTime timeOut)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.FORCED_RESET_TIMEOUT, key, null, null, null, this, null);
			mdhoperationInfo.Param1 = timeOut;
			mdhoperationInfo.SetConsistencyImmediateCommit();
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem != null;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0003D21E File Offset: 0x0003B41E
		public ADMCacheItem Delete(object key, DMOperationCallBack preOperation, DMOperationCallBack postOperation)
		{
			return this.Delete(key, preOperation, postOperation, null);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0003D22C File Offset: 0x0003B42C
		public ADMCacheItem Delete(object key, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.DELETE, key, null, preOperation, postOperation, this, opState);
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0003D25C File Offset: 0x0003B45C
		public ADMCacheItem Upsert(ADMCacheItem dmCacheItem, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation)
		{
			return this.Upsert(dmCacheItem, version, ProtocolType.Regular, preOperation, postOperation, null);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0003D26C File Offset: 0x0003B46C
		public ADMCacheItem Upsert(ADMCacheItem dmCacheItem, InternalCacheItemVersion version, ProtocolType protocolType, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.VERSION_UPSERT, dmCacheItem, dmCacheItem, preOperation, postOperation, this, opState)
			{
				Param1 = version,
				Protocol = protocolType
			};
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0003D2B0 File Offset: 0x0003B4B0
		public ADMCacheItem Replace(ADMCacheItem dmCacheItem, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.VERSION_REPLACE, dmCacheItem, dmCacheItem, preOperation, postOperation, this, opState)
			{
				Param1 = version,
				Protocol = ProtocolType.Memcache
			};
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0003D2F4 File Offset: 0x0003B4F4
		public ADMCacheItem Delete(object key, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation)
		{
			return this.Delete(key, version, preOperation, postOperation, null);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0003D304 File Offset: 0x0003B504
		public ADMCacheItem Delete(object key, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.VERSION_DELETE, key, null, preOperation, postOperation, this, opState)
			{
				Param1 = version,
				Protocol = ProtocolType.Regular
			};
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0003D347 File Offset: 0x0003B547
		public ADMCacheItem Delete(object key, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation)
		{
			return this.Delete(key, lockHandle, preOperation, postOperation, null);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0003D358 File Offset: 0x0003B558
		public ADMCacheItem Delete(object key, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.LOCK_DELETE, key, null, preOperation, postOperation, this, opState);
			mdhoperationInfo.Param1 = lockHandle;
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem ?? mdhoperationInfo.LockPlaceHolderObject;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0003D3A0 File Offset: 0x0003B5A0
		public ADMCacheItem InternalDelete(object key, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object state)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.INTERNAL_DELETE, key, null, preOperation, postOperation, this, state);
			mdhoperationInfo.Param1 = version;
			mdhoperationInfo.SetConsistencyImmediateCommit();
			this.PerformOperation(ref mdhoperationInfo);
			if (!mdhoperationInfo.OperationCompleted)
			{
				return null;
			}
			if (mdhoperationInfo.OldExpiredCacheItem != null)
			{
				return mdhoperationInfo.OldExpiredCacheItem;
			}
			return mdhoperationInfo.OldCacheItem ?? mdhoperationInfo.LockPlaceHolderObject;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0003D410 File Offset: 0x0003B610
		public ADMCacheItem InternalUpsert(ADMCacheItem dmCacheItem, DMOperationCallBack preOperation, DMOperationCallBack postOperation)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.INTERNAL_UPSERT, dmCacheItem, dmCacheItem, preOperation, postOperation, this, null);
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0003D440 File Offset: 0x0003B640
		public ADMCacheItem InternalPutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.INTERNAL_PUT_AND_UNLOCK, dmCacheItem, dmCacheItem, preOperation, postOperation, this, null);
			mdhoperationInfo.Param1 = lockHandle;
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0003D479 File Offset: 0x0003B679
		public ADMCacheItem GetAndLock(object key, TimeSpan lockTimeOut, DMOperationCallBack preOperation, DMOperationCallBack postOperation)
		{
			return this.GetAndLock(key, lockTimeOut, false, preOperation, postOperation, null);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0003D488 File Offset: 0x0003B688
		public ADMCacheItem GetAndLock(object key, TimeSpan lockTimeOut, bool lockKey, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.GET_AND_LOCK, key, null, preOperation, postOperation, this, opState);
			mdhoperationInfo.Param1 = lockTimeOut;
			mdhoperationInfo.LockKey = lockKey;
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem ?? mdhoperationInfo.LockPlaceHolderObject;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0003D4DC File Offset: 0x0003B6DC
		public ADMCacheItem ReadThroughLock(object key, TimeSpan lockTimeOut, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.RT_LOCK, key, null, null, postOperation, this, opState);
			mdhoperationInfo.SetConsistencyImmediateCommit();
			mdhoperationInfo.Param1 = lockTimeOut;
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.LockPlaceHolderObject;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0003D524 File Offset: 0x0003B724
		public void ReadThroughUnlock(object key, DataCacheLockHandle lockHandle, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.RT_UNLOCK, key, null, null, postOperation, this, opState);
			mdhoperationInfo.SetConsistencyImmediateCommit();
			mdhoperationInfo.Param1 = lockHandle;
			this.PerformOperation(ref mdhoperationInfo);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0003D560 File Offset: 0x0003B760
		public ADMCacheItem ReadThroughPutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.RT_PUT_AND_UNLOCK, dmCacheItem, dmCacheItem, preOperation, postOperation, this, opState);
			mdhoperationInfo.Param1 = lockHandle;
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0003D59A File Offset: 0x0003B79A
		public ADMCacheItem PutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation)
		{
			return this.PutAndUnlock(dmCacheItem, lockHandle, preOperation, postOperation, null);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0003D5A8 File Offset: 0x0003B7A8
		public ADMCacheItem PutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.PUT_AND_UNLOCK, dmCacheItem, dmCacheItem, preOperation, postOperation, this, opState);
			mdhoperationInfo.Param1 = lockHandle;
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem ?? mdhoperationInfo.LockPlaceHolderObject;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0003D5EC File Offset: 0x0003B7EC
		public bool Unlock(object key, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation)
		{
			return this.Unlock(key, lockHandle, preOperation, postOperation, null);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0003D5FC File Offset: 0x0003B7FC
		public bool Unlock(object key, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.UNLOCK, key, null, preOperation, postOperation, this, opState);
			mdhoperationInfo.Param1 = lockHandle;
			this.PerformOperation(ref mdhoperationInfo);
			return (mdhoperationInfo.OldCacheItem ?? mdhoperationInfo.LockPlaceHolderObject) != null;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0003D648 File Offset: 0x0003B848
		public bool Unlock(object key, DataCacheLockHandle lockHandle, DateTime objectTimeOut, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.UNLOCK, key, null, preOperation, postOperation, this, opState);
			mdhoperationInfo.Param1 = lockHandle;
			mdhoperationInfo.Param2 = objectTimeOut;
			this.PerformOperation(ref mdhoperationInfo);
			return (mdhoperationInfo.OldCacheItem ?? mdhoperationInfo.LockPlaceHolderObject) != null;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0003D6A0 File Offset: 0x0003B8A0
		public void InternalLockUpdate(object key, TimeSpan lockTimeOut, DataCacheLockHandle lockHandle, bool lockKey, InternalCacheItemVersion version, DMOperationCallBack postOp, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.INTERNAL_LOCK_UPDATE, key, null, null, postOp, this, opState);
			mdhoperationInfo.SetConsistencyImmediateCommit();
			mdhoperationInfo.Param1 = lockTimeOut;
			mdhoperationInfo.Param2 = lockHandle;
			mdhoperationInfo.LockKey = lockKey;
			mdhoperationInfo.Version = version;
			this.PerformOperation(ref mdhoperationInfo);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0003D6FC File Offset: 0x0003B8FC
		public bool ResetTimeOut(object key, DateTime timeOut, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.RESET_TIMEOUT, key, null, preOperation, postOperation, this, opState);
			mdhoperationInfo.Param1 = timeOut;
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem != null;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0003D744 File Offset: 0x0003B944
		public ADMCacheItem Commit(object key, object item)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.COMMIT, key, null, null, null, this, item);
			mdhoperationInfo.SetConsistencyImmediateCommit();
			mdhoperationInfo.ParentLinkageCheckRequired = false;
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0003823B File Offset: 0x0003643B
		public IStoreSchema GetSchema()
		{
			return new MultiDirectoryHashtableSchema();
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0003D782 File Offset: 0x0003B982
		public IHashtableEnumerator Enumerate()
		{
			return new MDHEnumerator(this._root, MDHEnumerator.EnumerationScope.ENTIRE_SUBTREE);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0003D790 File Offset: 0x0003B990
		public bool GetBatch(IScanner scanner, EnumeratorState enumeratorState)
		{
			BaseEnumeratorState baseEnumeratorState = enumeratorState as BaseEnumeratorState;
			bool invalidateOnChange = scanner.InvalidateOnChange;
			if (invalidateOnChange && (!baseEnumeratorState.IsValidState(this._lastCompactionEpoch) || !baseEnumeratorState.IsValidState(this)))
			{
				throw MultiDirectoryHashtable.GetException(2008);
			}
			if (!baseEnumeratorState.Exhausted && this._root.GetBatch(scanner, baseEnumeratorState))
			{
				baseEnumeratorState.Exhausted = true;
			}
			if (invalidateOnChange && !baseEnumeratorState.IsValidState(Thread.VolatileRead(ref this._lastCompactionEpoch)))
			{
				throw MultiDirectoryHashtable.GetException(2008);
			}
			return baseEnumeratorState.Exhausted;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0003D818 File Offset: 0x0003BA18
		public int DoCompaction()
		{
			int num = 0;
			this._root.DoCompactionOnSubDirectories(-1, ref num, ref this._lastCompactionEpoch);
			if (num > 0)
			{
				Interlocked.Add(ref this._noOfSplits, -1 * num);
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.DataManager", "{0} Directory deleted", new object[] { num });
			}
			return num;
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x0003D877 File Offset: 0x0003BA77
		public int SplitCount
		{
			get
			{
				return this._noOfSplits;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0003D87F File Offset: 0x0003BA7F
		public long LastCompactionEpoch
		{
			get
			{
				return this._lastCompactionEpoch;
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0003D888 File Offset: 0x0003BA88
		public bool BeginDelete(object key, InternalCacheItemVersion version)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.BEGIN_DELETE, key, null, null, null, this, null);
			mdhoperationInfo.Param1 = version;
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OperationCompleted;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0003D8C8 File Offset: 0x0003BAC8
		public ADMCacheItem CommitDelete(object key, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.COMMIT_DELETE, key, null, preOperation, postOperation, this, opState);
			mdhoperationInfo.SetConsistencyImmediateCommit();
			mdhoperationInfo.ParentLinkageCheckRequired = false;
			this.PerformOperation(ref mdhoperationInfo);
			return mdhoperationInfo.OldCacheItem;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0003D908 File Offset: 0x0003BB08
		public void AbortDelete(object key, InternalCacheItemVersion version)
		{
			MDHOperationInfo mdhoperationInfo = new MDHOperationInfo(MDHOperationType.ABORT_DELETE, key, null, null, null, this, null);
			mdhoperationInfo.Param1 = version;
			mdhoperationInfo.SetConsistencyImmediateCommit();
			mdhoperationInfo.ParentLinkageCheckRequired = false;
			this.PerformOperation(ref mdhoperationInfo);
		}

		// Token: 0x04000BBC RID: 3004
		private IDirectoryNodeFactory _directoryNodeFactory;

		// Token: 0x04000BBD RID: 3005
		private MDHDirectoryNode _root;

		// Token: 0x04000BBE RID: 3006
		private CommitType _commitType = CommitType.ImmediateCommit;

		// Token: 0x04000BBF RID: 3007
		private ExpirationType _expirationType;

		// Token: 0x04000BC0 RID: 3008
		private long _lastCompactionEpoch = DateTime.UtcNow.Ticks;

		// Token: 0x04000BC1 RID: 3009
		private int _noOfSplits;

		// Token: 0x04000BC2 RID: 3010
		private GetLockPlaceHolderObject _lockCallBack;

		// Token: 0x02000246 RID: 582
		private enum StatusType
		{
			// Token: 0x04000BC4 RID: 3012
			NoError,
			// Token: 0x04000BC5 RID: 3013
			DuplicateKeyError,
			// Token: 0x04000BC6 RID: 3014
			VersionMismatchError,
			// Token: 0x04000BC7 RID: 3015
			KeyNotFoundError
		}
	}
}
