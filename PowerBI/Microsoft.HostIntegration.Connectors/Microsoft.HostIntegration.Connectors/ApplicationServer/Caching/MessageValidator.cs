using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001C5 RID: 453
	internal class MessageValidator
	{
		// Token: 0x06000EE4 RID: 3812 RVA: 0x00032768 File Offset: 0x00030968
		public static bool IsValidAdminRequest(RequestBody reqBody, ref MessageFormatStatus status)
		{
			switch (reqBody.Req)
			{
			case ReqType.GET_NODE_STATS:
			case ReqType.GET_DETAILED_CACHE_NODE_STATS:
			case ReqType.GET_NAMED_CACHES:
			case ReqType.ADVANCED_CONFIG_CHANGED:
			case ReqType.HOSTS_CHANGED:
			case ReqType.RUN_GC:
			case ReqType.GET_LPM:
			case ReqType.SHUTDOWN:
			case ReqType.SHUTDOWN_STATUS:
			case ReqType.CANCEL_SHUTDOWN:
			case ReqType.GET_PARTITIONS_STATUS:
				return true;
			case ReqType.GET_DETAILED_NAMED_CACHE_STATS:
			case ReqType.GET_NAMED_CACHE_STATS:
			case ReqType.DELETE_NAMED_CACHE:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status);
			case ReqType.GET_REGION_STATS:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status);
			case ReqType.GET_REGIONS:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValueNotNull(reqBody.Value, ref status);
			case ReqType.CACHE_CONFIG_CHANGED:
				return MessageValidator.ValidateCacheNameEx(reqBody.CacheName, ref status);
			case ReqType.CREATE_NAMED_CACHE:
				return MessageValidator.ValidateCacheNameEx(reqBody.CacheName, ref status) && MessageValidator.ValueNotNull(reqBody.Value, ref status);
			}
			status = MessageFormatStatus.InvalidReqType;
			return false;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00032863 File Offset: 0x00030A63
		private static bool IsRequestFlagValid(RequestBody requestBody, ref MessageFormatStatus status)
		{
			if (requestBody.ReadThroughAddFlag)
			{
				status = MessageFormatStatus.InvalidReqType;
				return false;
			}
			return true;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00032874 File Offset: 0x00030A74
		public static bool IsValidClientRequest(RequestBody reqBody, ref MessageFormatStatus status)
		{
			if (!MessageValidator.IsRequestFlagValid(reqBody, ref status))
			{
				return false;
			}
			if (reqBody.Fwd != ForwardingType.Routable && reqBody.Fwd != ForwardingType.Routed)
			{
				status = MessageFormatStatus.InValidForwardingType;
				return false;
			}
			if (reqBody.Req > ReqType.SOCKET_HACK)
			{
				return true;
			}
			ReqType req = reqBody.Req;
			switch (req)
			{
			case ReqType.ADD:
			case ReqType.PUT:
				break;
			case ReqType.GET:
			case ReqType.GET_CACHE_ITEM:
			case ReqType.GET_IF_NEWER:
			case ReqType.REMOVE:
			case ReqType.LOCKED_REMOVE:
				goto IL_0172;
			case ReqType.GET_ALL:
			case ReqType.CREATE_REGION:
			case ReqType.REMOVE_REGION:
			case ReqType.CLEAR_REGION:
				goto IL_0349;
			case ReqType.GET_AND_LOCK:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status) && MessageValidator.KeyNotNull(reqBody.Key, ref status) && MessageValidator.TimeToLivePositive(reqBody.TTL, ref status);
			case ReqType.GET_NEXT_BATCH:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status) && MessageValidator.ValidateBatchRequest(reqBody, ref status);
			case ReqType.RESET_TIMEOUT:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status) && MessageValidator.KeyNotNull(reqBody.Key, ref status) && MessageValidator.TimeToLivePositive(reqBody.TTL, ref status);
			case ReqType.PUT_AND_UNLOCK:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status) && MessageValidator.KeyNotNull(reqBody.Key, ref status) && MessageValidator.ValueNotNull(reqBody.Value, ref status) && MessageValidator.TimeToLiveNotNegative(reqBody.TTL, ref status);
			case ReqType.UNLOCK:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status) && MessageValidator.KeyNotNull(reqBody.Key, ref status) && MessageValidator.TimeToLiveNotNegative(reqBody.TTL, ref status);
			case ReqType.BULK_GET:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status) && MessageValidator.KeysNotNull(reqBody.Keys, ref status);
			default:
				switch (req)
				{
				case ReqType.GET_NAMED_CACHE_CONFIGURATION:
				case ReqType.GET_NAMED_CACHE_CONFIGURATION_VERSION_MATCH:
					return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status);
				case ReqType.CREATE_NAMED_CACHE:
				case ReqType.DELETE_NAMED_CACHE:
				case ReqType.SHUTDOWN:
				case ReqType.SHUTDOWN_STATUS:
				case ReqType.CANCEL_SHUTDOWN:
				case ReqType.WRITE_BEHIND_CHECKPOINT:
				case ReqType.GET_PARTITIONS_STATUS:
				case ReqType.BROADCAST:
				case ReqType.GET_HOST_STATUS:
				case (ReqType)48:
					goto IL_0349;
				case ReqType.CLEAR_CACHE:
					return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status);
				case ReqType.PARTITION_CLEAR_CACHE:
					if (reqBody.PartitionId == null)
					{
						status = MessageFormatStatus.InvalidPartitionId;
						return false;
					}
					return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status);
				case ReqType.CACHE_BULK_GET:
					return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.KeysNotNull(reqBody.Keys, ref status);
				case ReqType.INCREMENT:
				case ReqType.DECREMENT:
					return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status) && MessageValidator.ValueNotNull(reqBody.Value, ref status) && MessageValidator.KeyNotNull(reqBody.Key, ref status) && MessageValidator.InitialValueNotNull(reqBody.InitialValue, ref status);
				case ReqType.APPEND:
				case ReqType.PREPEND:
					return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status) && MessageValidator.ValueNotNull(reqBody.Value, ref status) && MessageValidator.KeyNotNull(reqBody.Key, ref status);
				case ReqType.CONTAINSKEY:
					goto IL_0172;
				case ReqType.REPLACE:
					break;
				default:
					goto IL_0349;
				}
				break;
			}
			return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status) && MessageValidator.KeyNotNull(reqBody.Key, ref status) && MessageValidator.ValueNotNull(reqBody.Value, ref status) && MessageValidator.TimeToLiveNotNegative(reqBody.TTL, ref status);
			IL_0172:
			return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status) && MessageValidator.KeyNotNull(reqBody.Key, ref status);
			IL_0349:
			if (CloudUtility.IsVASDeployment && !"__system".Equals(reqBody.CacheName))
			{
				status = MessageFormatStatus.InvalidReqType;
				return false;
			}
			ReqType req2 = reqBody.Req;
			switch (req2)
			{
			case ReqType.CREATE_REGION:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionNameEx(reqBody, false, ref status);
			case ReqType.REMOVE_REGION:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, false, ref status);
			case ReqType.CLEAR_REGION:
				return MessageValidator.ValidateCacheName(reqBody.CacheName, ref status) && MessageValidator.ValidateRegionName(reqBody, true, ref status);
			default:
				switch (req2)
				{
				case ReqType.NOTIFICATION_REQ:
				case ReqType.NOTIFICATION_LSN_REQ:
					return MessageValidator.ValidateNotificationRequest(reqBody.ValObject as NotificationRequest, ref status);
				default:
					status = MessageFormatStatus.InvalidReqType;
					return false;
				}
				break;
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00032C7D File Offset: 0x00030E7D
		public static bool ValidateCacheName(string cacheName, ref MessageFormatStatus status)
		{
			if (string.IsNullOrEmpty(cacheName))
			{
				status = MessageFormatStatus.CacheNameNullOrEmpty;
				return false;
			}
			return true;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00032C8D File Offset: 0x00030E8D
		public static bool ValidateCacheNameEx(string cacheName, ref MessageFormatStatus status)
		{
			if (!MessageValidator.ValidateCacheName(cacheName, ref status))
			{
				return false;
			}
			if (!Utility.IsValidName(cacheName))
			{
				status = MessageFormatStatus.InvalidCacheName;
				return false;
			}
			return true;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00032CA8 File Offset: 0x00030EA8
		internal static bool IsValidSystemRegion(string regionName, int systemRegionCount)
		{
			int num;
			return int.TryParse(regionName.Substring("Default_Region_".Length), out num) && num >= 0 && num < systemRegionCount;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00032CDC File Offset: 0x00030EDC
		public static bool ValidateRegionName(RequestBody request, bool isSystemRegionAccepted, ref MessageFormatStatus status)
		{
			string regionName = request.RegionName;
			if (string.IsNullOrEmpty(regionName))
			{
				status = MessageFormatStatus.RegionNameNullOrEmpty;
				return false;
			}
			if (RegionNameProvider.IsSystemRegion(regionName))
			{
				if (!isSystemRegionAccepted)
				{
					status = MessageFormatStatus.InvalidOperationOnSystemRegion;
					return false;
				}
				if (!MessageValidator.IsValidSystemRegion(regionName, request.SystemRegionCount))
				{
					status = MessageFormatStatus.InvalidRegionName;
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00032D22 File Offset: 0x00030F22
		public static bool ValidateRegionNameEx(RequestBody request, bool isSystemRegionAccepted, ref MessageFormatStatus status)
		{
			if (!MessageValidator.ValidateRegionName(request, isSystemRegionAccepted, ref status))
			{
				return false;
			}
			if (!Utility.IsValidName(request.RegionName))
			{
				status = MessageFormatStatus.InvalidRegionName;
				return false;
			}
			return true;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00032D43 File Offset: 0x00030F43
		public static bool ValueNotNull(byte[][] value, ref MessageFormatStatus status)
		{
			if (value == null)
			{
				status = MessageFormatStatus.ValueNull;
				return false;
			}
			return true;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00032D4E File Offset: 0x00030F4E
		public static bool InitialValueNotNull(byte[][] value, ref MessageFormatStatus status)
		{
			if (value == null)
			{
				status = MessageFormatStatus.InitialValueNull;
				return false;
			}
			return true;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00032D5A File Offset: 0x00030F5A
		public static bool KeyNotNull(Key key, ref MessageFormatStatus status)
		{
			if (key == null)
			{
				status = MessageFormatStatus.KeyNull;
				return false;
			}
			if (key.StringValue == null)
			{
				status = MessageFormatStatus.KeyNull;
				return false;
			}
			return true;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00032D74 File Offset: 0x00030F74
		public static bool KeysNotNull(Key[] keys, ref MessageFormatStatus status)
		{
			if (keys == null)
			{
				status = MessageFormatStatus.KeyNull;
				return false;
			}
			for (int i = 0; i < keys.Length; i++)
			{
				if (keys[i] == null)
				{
					status = MessageFormatStatus.KeyNull;
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00032DA2 File Offset: 0x00030FA2
		public static bool TimeToLiveNotNegative(TimeSpan timeout, ref MessageFormatStatus status)
		{
			if (timeout.CompareTo(TimeSpan.Zero) < 0)
			{
				status = MessageFormatStatus.TimeToLiveNegative;
				return false;
			}
			return true;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00032DB9 File Offset: 0x00030FB9
		public static bool TimeToLivePositive(TimeSpan timeout, ref MessageFormatStatus status)
		{
			if (timeout.CompareTo(TimeSpan.Zero) <= 0)
			{
				status = MessageFormatStatus.TimeToLiveNotGreaterThanZero;
				return false;
			}
			return true;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00032DD1 File Offset: 0x00030FD1
		public static bool ValidateLockHandle(DataCacheLockHandle lockHandle, ref MessageFormatStatus status)
		{
			if (lockHandle == null)
			{
				status = MessageFormatStatus.LockHandleNull;
				return false;
			}
			return true;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00032DDC File Offset: 0x00030FDC
		public static bool ValidateBatchRequest(RequestBody reqBody, ref MessageFormatStatus status)
		{
			if (reqBody.EnumState != null)
			{
				return true;
			}
			switch (reqBody.GetByTagsOp)
			{
			case GetByTagsOperation.ByNone:
				return MessageValidator.ValidateTagsForGetByNone(reqBody.Tags, ref status);
			case GetByTagsOperation.ByIntersection:
			case GetByTagsOperation.ByUnion:
				return MessageValidator.ValidateTags(reqBody.Tags, ref status);
			default:
				return true;
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00032E29 File Offset: 0x00031029
		private static bool ValidateTagsForGetByNone(DataCacheTag[] tags, ref MessageFormatStatus status)
		{
			if (tags == null)
			{
				return true;
			}
			if (tags.Length == 0)
			{
				status = MessageFormatStatus.TagsLengthZero;
				return false;
			}
			if (tags.Length > 1)
			{
				status = MessageFormatStatus.MoreThanOneTag;
				return false;
			}
			if (tags[0] == null)
			{
				status = MessageFormatStatus.NullTag;
				return false;
			}
			return true;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00032E54 File Offset: 0x00031054
		private static bool ValidateTags(DataCacheTag[] tags, ref MessageFormatStatus status)
		{
			if (tags == null)
			{
				status = MessageFormatStatus.NullTags;
				return false;
			}
			if (tags.Length == 0)
			{
				status = MessageFormatStatus.TagsLengthZero;
				return false;
			}
			for (int i = 0; i < tags.Length; i++)
			{
				if (tags[i] == null)
				{
					status = MessageFormatStatus.NullTag;
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00032E90 File Offset: 0x00031090
		public static bool ValidateNotificationRequest(NotificationRequest request, ref MessageFormatStatus status)
		{
			if (request == null)
			{
				status = MessageFormatStatus.InvalidNotificationRequest;
				return false;
			}
			if (request.PartitionReqList == null || request.PartitionReqList.Length == 0)
			{
				status = MessageFormatStatus.InvalidNotificationRequest;
				return false;
			}
			for (int i = 0; i < request.PartitionReqList.Length; i++)
			{
				if (request.PartitionReqList[i].PartitionId == null || request.PartitionReqList[i].PartitionId.ServiceNamespace == null)
				{
					status = MessageFormatStatus.InvalidNotificationRequest;
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00032F01 File Offset: 0x00031101
		public static bool IsValidDOMRequestType(ReqType type, ref MessageFormatStatus status)
		{
			if (type == ReqType.CLEAR_CACHE)
			{
				status = MessageFormatStatus.InvalidDOMRequestType;
				return false;
			}
			return true;
		}
	}
}
