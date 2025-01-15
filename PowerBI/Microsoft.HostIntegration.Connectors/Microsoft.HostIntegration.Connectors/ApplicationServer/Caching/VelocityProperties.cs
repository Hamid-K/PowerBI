using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Fabric.Common;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000183 RID: 387
	internal static class VelocityProperties
	{
		// Token: 0x06000C44 RID: 3140 RVA: 0x00028FB8 File Offset: 0x000271B8
		public static bool TryGetNamedCacheProperty(VelocityPacketProperty propertyName, out NamedCacheProperty namedCacheProperty)
		{
			switch (propertyName)
			{
			case VelocityPacketProperty.DeploymentMode:
				namedCacheProperty = NamedCacheProperty.AdditionalRoutingProps;
				break;
			case VelocityPacketProperty.PartitionCount:
				namedCacheProperty = NamedCacheProperty.PartitionCount;
				break;
			case VelocityPacketProperty.RegionCount:
				namedCacheProperty = NamedCacheProperty.RegionCount;
				break;
			case VelocityPacketProperty.DefaultTTL:
				namedCacheProperty = NamedCacheProperty.DefaultTTL;
				break;
			case VelocityPacketProperty.EvictionType:
				namedCacheProperty = NamedCacheProperty.EvictionType;
				break;
			case VelocityPacketProperty.ExpirationType:
				namedCacheProperty = NamedCacheProperty.ExpirationType;
				break;
			case VelocityPacketProperty.NotificationProperties:
				namedCacheProperty = NamedCacheProperty.NotificationProps;
				break;
			default:
				namedCacheProperty = NamedCacheProperty.CacheName;
				return false;
			}
			return true;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002901C File Offset: 0x0002721C
		public static T GetProperty<T>(this IVelocityPacketPropertyBag propBag, VelocityPacketProperty propertyName, T defaultValue, Converter<byte[], T> conv)
		{
			try
			{
				byte[] array;
				if (propBag.TryGetElement(propertyName, out array))
				{
					return conv(array);
				}
			}
			catch (VelocityPacketFormatException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<VelocityPacketFormatException>("VelocityProperties.GetProperty", "Exception when parsing property: {0}", ex);
				}
			}
			return defaultValue;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00029070 File Offset: 0x00027270
		public static void SetProperty<T>(this IVelocityPacketPropertyBag propBag, VelocityPacketProperty propertyName, T value, Converter<T, byte[]> conv)
		{
			propBag.SetElement(propertyName, conv(value));
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00029081 File Offset: 0x00027281
		public static string GetProperty(this IVelocityPacketPropertyBag propBag, VelocityPacketProperty propertyName, string defaultValue)
		{
			return propBag.GetProperty(propertyName, defaultValue, new Converter<byte[], string>(VelocityProperties._encodingType.GetString));
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002909C File Offset: 0x0002729C
		public static string GetProperty(this IVelocityPacketPropertyBag propBag, VelocityPacketProperty propertyName)
		{
			return propBag.GetProperty(propertyName, null);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000290A6 File Offset: 0x000272A6
		public static void SetProperty(this IVelocityPacketPropertyBag propBag, VelocityPacketProperty propertyName, string value)
		{
			propBag.SetProperty(propertyName, value, new Converter<string, byte[]>(VelocityProperties._encodingType.GetBytes));
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000290C1 File Offset: 0x000272C1
		public static IEnumerable<DataCacheTag> GetTags(this IVelocityPacketPropertyBag propBag)
		{
			return propBag.GetProperty(VelocityPacketProperty.Tags, null, new Converter<byte[], IList<DataCacheTag>>(VelocityProperties.DeserializeTags));
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000290D7 File Offset: 0x000272D7
		public static void SetTags(this IVelocityPacketPropertyBag propBag, IEnumerable<DataCacheTag> tags)
		{
			propBag.SetProperty(VelocityPacketProperty.Tags, tags, new Converter<IEnumerable<DataCacheTag>, byte[]>(VelocityProperties.SerializeTags));
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000290ED File Offset: 0x000272ED
		public static Guid GetMessageTrackingId(this IVelocityPacketPropertyBag propBag)
		{
			return propBag.GetProperty(VelocityPacketProperty.MessageTrackingId, Guid.Empty, new Converter<byte[], Guid>(VelocityProperties.DeserializeGuid));
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002910B File Offset: 0x0002730B
		public static void SetMessageTrackingId(this IVelocityPacketPropertyBag propBag, Guid guid)
		{
			propBag.SetProperty(VelocityPacketProperty.MessageTrackingId, guid, new Converter<Guid, byte[]>(VelocityProperties.SerializeGuid));
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00029125 File Offset: 0x00027325
		public static string GetAuthorizationToken(this IVelocityPacketPropertyBag propBag)
		{
			return propBag.GetProperty(VelocityPacketProperty.AuthorizationToken, null);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00029133 File Offset: 0x00027333
		public static void SetAuthorizationToken(this IVelocityPacketPropertyBag propBag, string header)
		{
			propBag.SetProperty(VelocityPacketProperty.AuthorizationToken, header);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00029144 File Offset: 0x00027344
		public static T GetRequestTracker<T>(this IVelocityPacketPropertyBag propBag, VelocityPacketProperty property)
		{
			return propBag.GetProperty(property, default(T), new Converter<byte[], T>(VelocityProperties.DeserializeInternal<T>));
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002916D File Offset: 0x0002736D
		public static void SetRequestTracker<T>(this IVelocityPacketPropertyBag propBag, VelocityPacketProperty property, T value)
		{
			propBag.SetProperty(property, value, new Converter<T, byte[]>(VelocityProperties.SerializeInternal<T>));
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00029183 File Offset: 0x00027383
		public static void SetNotificationRequest(this IVelocityPacketPropertyBag propBag, VelocityPacketProperty propertyName, NotificationRequest request)
		{
			propBag.SetElement(propertyName, VelocityProperties.SerializeNotificationRequest(request, propertyName));
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00029194 File Offset: 0x00027394
		public static NotificationRequest GetNotificationRequest(this IVelocityPacketPropertyBag propBag, VelocityPacketProperty propertyName, string cacheName)
		{
			NotificationRequest notificationRequest = null;
			try
			{
				byte[] array = null;
				if (propBag.TryGetElement(propertyName, out array))
				{
					notificationRequest = VelocityProperties.DeserializeNotificationRequest(array, propertyName, cacheName);
				}
			}
			catch (VelocityPacketFormatException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("VelocityProperties.GetProperty", "Exception when parsing property: {0}", new object[] { ex });
				}
			}
			return notificationRequest;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x000291F4 File Offset: 0x000273F4
		public static void SetNotificationReply(this IVelocityPacketPropertyBag propBag, VelocityPacketProperty propertyName, NotificationReply reply)
		{
			propBag.SetElement(propertyName, VelocityProperties.SerializeNotificationReply(reply));
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00029204 File Offset: 0x00027404
		public static NotificationReply GetNotificationReply(this IVelocityPacketPropertyBag propBag, VelocityPacketProperty propertyName, string cacheName)
		{
			NotificationReply notificationReply = null;
			try
			{
				byte[] array;
				if (propBag.TryGetElement(propertyName, out array))
				{
					notificationReply = VelocityProperties.DeserializeNotificationReply(array, cacheName);
				}
			}
			catch (VelocityPacketFormatException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("VelocityProperties.GetNotificationReply", "Exception when parsing property: {0}", new object[] { ex });
				}
			}
			return notificationReply;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00029260 File Offset: 0x00027460
		public static CacheLookupTableTransfer GetLookupTable(this IVelocityPacketPropertyBag propBag, string cacheName)
		{
			CacheLookupTableTransfer cacheLookupTableTransfer = null;
			try
			{
				byte[] array;
				if (propBag.TryGetElement(VelocityPacketProperty.LookupTable, out array))
				{
					cacheLookupTableTransfer = VelocityProperties.DeserializeLookupTable(array, cacheName);
				}
				else if (propBag.TryGetElement(VelocityPacketProperty.ExternalLookupTableWithIdentifiers, out array))
				{
					cacheLookupTableTransfer = VelocityProperties.DeserializeLookupTable(array, cacheName);
				}
			}
			catch (VelocityPacketFormatException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("VelocityProperties.GetLookupTable", "Exception when parsing property: {0}", new object[] { ex });
				}
			}
			return cacheLookupTableTransfer;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000292D4 File Offset: 0x000274D4
		public static void SetLookupTable(this IVelocityPacketPropertyBag propBag, CacheLookupTableTransfer table)
		{
			propBag.SetProperty(VelocityPacketProperty.LookupTable, table, new Converter<CacheLookupTableTransfer, byte[]>(VelocityProperties.SerializeLookupTable));
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000292EC File Offset: 0x000274EC
		internal static byte[] SerializeNamedCachePropertyFromConfiguration(ServiceConfigurationManager mgr, INamedCacheConfiguration config, NamedCacheProperty key, DataCacheDeploymentMode deploymentMode)
		{
			switch (key)
			{
			case NamedCacheProperty.CacheName:
				return VelocityProperties._encodingType.GetBytes(config.Name);
			case NamedCacheProperty.PartitionCount:
				return VelocityProperties.WriteInteger(config.GetPartitionCount(mgr.BasePartitionCount));
			case NamedCacheProperty.RegionCount:
				return VelocityProperties.WriteInteger(config.SystemRegionCount);
			case NamedCacheProperty.Secondaries:
				return VelocityProperties.WriteInteger(config.Secondaries);
			case NamedCacheProperty.DefaultTTL:
				try
				{
					uint? ttl = VelocityWireProtocol.GetTtl(TimeSpan.FromMinutes((double)config.DefaultTTL));
					if (config.IsExpirable && ttl != null)
					{
						return VelocityProperties.WriteInteger((int)ttl.Value);
					}
				}
				catch (OverflowException)
				{
				}
				return VelocityProperties.WriteInteger(0);
			case NamedCacheProperty.EvictionType:
				return VelocityProperties.WriteInteger((int)config.EvictionType);
			case NamedCacheProperty.NotificationProps:
				if (config.Notification.IsEnabled)
				{
					return VelocityProperties.WriteInteger(config.Notification.MaxEvents);
				}
				return VelocityProperties.WriteInteger(0);
			case NamedCacheProperty.AdditionalRoutingProps:
				return VelocityProperties.WriteInteger((int)deploymentMode);
			case NamedCacheProperty.ExpirationType:
				if (config.IsExpirable)
				{
					return VelocityProperties.WriteInteger((int)config.ExpirationType);
				}
				return VelocityProperties.WriteInteger(0);
			}
			return null;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00029414 File Offset: 0x00027614
		internal static void DeserializeNamedCachePropertyToConfiguration(INamedCacheConfiguration namedCacheconfig, NamedCacheProperty key, byte[] value)
		{
			switch (key)
			{
			case NamedCacheProperty.CacheName:
				namedCacheconfig.Name = VelocityProperties._encodingType.GetString(value);
				return;
			case NamedCacheProperty.CacheDeploymentMode:
			case NamedCacheProperty.IsExpirable:
				break;
			case NamedCacheProperty.PartitionCount:
				namedCacheconfig.PartitionCount = VelocityProperties.ReadInteger(key, value);
				return;
			case NamedCacheProperty.RegionCount:
				namedCacheconfig.SystemRegionCount = VelocityProperties.ReadInteger(key, value);
				return;
			case NamedCacheProperty.Secondaries:
				namedCacheconfig.Secondaries = VelocityProperties.ReadInteger(key, value);
				return;
			case NamedCacheProperty.DefaultTTL:
				namedCacheconfig.DefaultTTL = (long)VelocityWireProtocol.GetTimeSpan(new uint?((uint)VelocityProperties.ReadInteger(key, value))).Minutes;
				return;
			case NamedCacheProperty.EvictionType:
				namedCacheconfig.EvictionType = (EvictionType)VelocityProperties.ReadInteger(key, value);
				return;
			case NamedCacheProperty.NotificationProps:
			{
				int num = VelocityProperties.ReadInteger(key, value);
				if (num == 0)
				{
					namedCacheconfig.Notification = new ServerNotificationProperties();
					return;
				}
				namedCacheconfig.Notification = new ServerNotificationProperties
				{
					IsEnabled = true,
					MaxEvents = num
				};
				return;
			}
			case NamedCacheProperty.AdditionalRoutingProps:
				namedCacheconfig.DeploymentMode = new DeploymentModeElement((DataCacheDeploymentMode)VelocityProperties.ReadInteger(key, value));
				break;
			case NamedCacheProperty.ExpirationType:
				namedCacheconfig.ExpirationType = (ExpirationType)VelocityProperties.ReadInteger(key, value);
				namedCacheconfig.IsExpirable = namedCacheconfig.ExpirationType != ExpirationType.None;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00029528 File Offset: 0x00027728
		internal static bool TrySerializeNamedCacheStats(OMNamedCacheStats stats, VelocityPacketProperty key, out byte[] value)
		{
			value = null;
			if (stats == null)
			{
				return false;
			}
			long num;
			switch (key)
			{
			case VelocityPacketProperty.GetRequestCount:
				num = stats.GetReqs;
				break;
			case VelocityPacketProperty.GetMissRequestCount:
				num = stats.GetRequestMiss;
				break;
			case VelocityPacketProperty.UpsertRequestCount:
				num = stats.UpsertReqs;
				break;
			case VelocityPacketProperty.AddRequestCount:
				num = stats.AddReqs;
				break;
			default:
				switch (key)
				{
				case VelocityPacketProperty.TotalObjectCount:
					num = stats.GetObjectCount();
					break;
				case VelocityPacketProperty.TotalObjectSize:
					num = stats.Size;
					break;
				case VelocityPacketProperty.IncomingBandwidth:
					num = stats.IncomingBandwidth;
					break;
				case VelocityPacketProperty.OutgoingBandwidth:
					num = stats.OutgoingBandwidth;
					break;
				default:
					return false;
				}
				break;
			}
			value = VelocityProperties.WriteLong(num);
			return true;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000295CC File Offset: 0x000277CC
		internal static byte[] WriteInteger(int integer)
		{
			return BitConverter.GetBytes(integer);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000295D4 File Offset: 0x000277D4
		internal static byte[] WriteLong(long value)
		{
			return BitConverter.GetBytes(value);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x000295DC File Offset: 0x000277DC
		internal static long ReadLong(byte[] value)
		{
			return BitConverter.ToInt64(value, 0);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000295E5 File Offset: 0x000277E5
		internal static int ReadInteger(byte[] bytes)
		{
			return BitConverter.ToInt32(bytes, 0);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000295F0 File Offset: 0x000277F0
		internal static int ReadInteger(NamedCacheProperty property, byte[] bytes)
		{
			if (bytes == null)
			{
				throw new VelocityPacketFormatException("Property bag item for named cache property not found: {0}", new object[] { property });
			}
			if (bytes.Length != 4)
			{
				throw new VelocityPacketFormatException("Property bag item length mismatch for named cache property {0}. Expected {0}, received {1}.", new object[] { property, bytes.Length });
			}
			return BitConverter.ToInt32(bytes, 0);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00029650 File Offset: 0x00027850
		public static byte[] SerializeLookupTable(CacheLookupTableTransfer transfer)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, VelocityProperties._encodingType))
				{
					if (transfer.Entries == null)
					{
						binaryWriter.Write(0);
					}
					else
					{
						binaryWriter.Write((ushort)transfer.Entries.Count);
						foreach (CacheLookupTableEntry cacheLookupTableEntry in transfer.Entries)
						{
							binaryWriter.Write(cacheLookupTableEntry.Pid.LowKey);
							binaryWriter.Write(cacheLookupTableEntry.Pid.HighKey);
							VelocityProperties.WriteString(binaryWriter, cacheLookupTableEntry.Config.Primary);
							binaryWriter.Write(cacheLookupTableEntry.Config.Version);
						}
					}
					CacheGenerationNumber cacheGenerationNumber = transfer.CacheGenerationNumber;
					binaryWriter.Write(cacheGenerationNumber.Number);
					VelocityProperties.WriteString(binaryWriter, cacheGenerationNumber.Owner);
					if (transfer.Ranges == null || transfer.Ranges.Ranges == null)
					{
						binaryWriter.Write(0);
					}
					else
					{
						binaryWriter.Write((ushort)transfer.Ranges.Ranges.Count);
						foreach (VersionRange versionRange in transfer.Ranges.Ranges)
						{
							binaryWriter.Write(versionRange.StartVersion);
							binaryWriter.Write(versionRange.EndVersion);
						}
					}
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00029834 File Offset: 0x00027A34
		public static byte[] SerializeNotificationRequest(NotificationRequest request, VelocityPacketProperty property)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, VelocityProperties._encodingType))
				{
					binaryWriter.Write(request.PartitionReqList.Length);
					for (int i = 0; i < request.PartitionReqList.Length; i++)
					{
						if (property == VelocityPacketProperty.NotificationRequest)
						{
							VelocityProperties.WritePartitionNotificationRequest((PartitionNotificationRequest)request.PartitionReqList[i], binaryWriter);
						}
						else
						{
							VelocityProperties.WritePartitionNotificationLsnRequest((PartitionNotificationLsnRequest)request.PartitionReqList[i], binaryWriter);
						}
					}
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000298DC File Offset: 0x00027ADC
		public static NotificationRequest DeserializeNotificationRequest(byte[] data, VelocityPacketProperty propertyName, string cacheName)
		{
			ArrayBackedReader arrayBackedReader = new ArrayBackedReader(data);
			NotificationRequest notificationRequest;
			try
			{
				int num = arrayBackedReader.ReadInt32();
				if (num <= 0)
				{
					throw new VelocityPacketFormatException(string.Format(CultureInfo.InvariantCulture, "Number of partitions in notification request should be more than zero: count {0}", new object[] { num }));
				}
				Func<ArrayBackedReader, string, IPartitionRequest> func;
				int num2;
				if (propertyName == VelocityPacketProperty.NotificationRequest)
				{
					func = new Func<ArrayBackedReader, string, IPartitionRequest>(VelocityProperties.ReadPartitionNotificationRequest);
					num2 = 28;
				}
				else
				{
					func = new Func<ArrayBackedReader, string, IPartitionRequest>(VelocityProperties.ReadPartitionNotificationLsnRequest);
					num2 = 8;
				}
				if (num > arrayBackedReader.Length / num2)
				{
					throw new VelocityPacketFormatException(string.Format(CultureInfo.InvariantCulture, "Number of partition requests is more than buffer can hold count:{0}, length {1}", new object[] { num, arrayBackedReader.Length }));
				}
				IPartitionRequest[] array = new IPartitionRequest[num];
				for (int i = 0; i < num; i++)
				{
					IPartitionRequest partitionRequest = func(arrayBackedReader, cacheName);
					array[i] = partitionRequest;
				}
				notificationRequest = new NotificationRequest(array);
			}
			catch (EndOfStreamException ex)
			{
				throw new VelocityPacketFormatException("End of stream reading notification request", ex);
			}
			return notificationRequest;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000299E8 File Offset: 0x00027BE8
		public static byte[] SerializeNotificationReply(NotificationReply reply)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, VelocityProperties._encodingType))
				{
					int count = reply.PartitionNotificationReplyList.Count;
					binaryWriter.Write(count);
					for (int i = 0; i < count; i++)
					{
						VelocityProperties.WritePartitionNotificationReply(binaryWriter, reply.PartitionNotificationReplyList[i]);
					}
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00029A78 File Offset: 0x00027C78
		public static NotificationReply DeserializeNotificationReply(byte[] data, string cacheName)
		{
			ArrayBackedReader arrayBackedReader = new ArrayBackedReader(data);
			NotificationReply notificationReply;
			try
			{
				int num = arrayBackedReader.ReadInt32();
				if (num <= 0)
				{
					throw new VelocityPacketFormatException(string.Format(CultureInfo.InvariantCulture, "Number of partitions in notification request should be more than zero {0}", new object[] { num }));
				}
				List<PartitionNotificationReply> list = new List<PartitionNotificationReply>();
				for (int i = 0; i < num; i++)
				{
					PartitionNotificationReply partitionNotificationReply = VelocityProperties.ReadPartitionNotificationReply(arrayBackedReader, cacheName);
					list.Add(partitionNotificationReply);
				}
				notificationReply = new NotificationReply(list);
			}
			catch (EndOfStreamException ex)
			{
				throw new VelocityPacketFormatException("End of stream reading notification request", ex);
			}
			return notificationReply;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00029B10 File Offset: 0x00027D10
		private static void WritePartitionNotificationReply(BinaryWriter writer, PartitionNotificationReply pReply)
		{
			VelocityProperties.WritePartitionId(pReply.PartitionId, writer);
			writer.Write((byte)pReply.RespStatus);
			writer.Write((byte)pReply.NotificationRespStatus);
			NotificationLsn notificationLsn = pReply.LastLsnResp ?? new NotificationLsn(0L, 0L);
			VelocityProperties.WriteNotificationLsn(writer, notificationLsn);
			int num = 0;
			if (pReply.RcvdEventList != null)
			{
				num = pReply.RcvdEventList.Count;
			}
			List<DataCacheOperationDescriptor> list = new List<DataCacheOperationDescriptor>(num);
			for (int i = 0; i < num; i++)
			{
				DataCacheOperationDescriptor dataCacheOperationDescriptor = pReply.RcvdEventList[i] as DataCacheOperationDescriptor;
				if (dataCacheOperationDescriptor != null)
				{
					list.Add(dataCacheOperationDescriptor);
				}
				else
				{
					BulkOpNotificationEvent bulkOpNotificationEvent = (BulkOpNotificationEvent)pReply.RcvdEventList[i];
					list.AddRange(bulkOpNotificationEvent.GetSimpleEvents());
				}
			}
			num = list.Count;
			writer.Write(num);
			for (int j = 0; j < num; j++)
			{
				VelocityProperties.WriteSimpleEvent(writer, list[j]);
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00029BF4 File Offset: 0x00027DF4
		private static PartitionNotificationReply ReadPartitionNotificationReply(ArrayBackedReader reader, string cacheName)
		{
			PartitionId partitionId = VelocityProperties.ReadPartitionId(reader, cacheName);
			byte b = reader.ReadByte();
			byte b2 = reader.ReadByte();
			NotificationLsn notificationLsn = VelocityProperties.ReadNotificationLsn(reader);
			int num = reader.ReadInt32();
			int num2 = reader.Length - reader.Position;
			if (num > num2 / 20)
			{
				throw new VelocityPacketFormatException(string.Format(CultureInfo.InvariantCulture, "Number of notifications in reply is more than buffer can hold: count {0}, length {1}", new object[] { num, num2 }));
			}
			List<BaseOperationNotification> list = new List<BaseOperationNotification>(num);
			for (int i = 0; i < num; i++)
			{
				list.Add(VelocityProperties.ReadSimpleEvent(reader, cacheName));
			}
			return new PartitionNotificationReply(partitionId, notificationLsn, (PartitionRespStatus)b)
			{
				RcvdEventList = list,
				NotificationRespStatus = (NotificationRespStatus)b2
			};
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00029CB8 File Offset: 0x00027EB8
		private static void WriteSimpleEvent(BinaryWriter writer, DataCacheOperationDescriptor operation)
		{
			writer.Write((int)operation.OperationType);
			VelocityProperties.WriteVersion(writer, operation.InternalVersion);
			VelocityProperties.WriteString(writer, operation.RegionName);
			VelocityProperties.WriteString(writer, operation.Key);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00029CEC File Offset: 0x00027EEC
		private static DataCacheOperationDescriptor ReadSimpleEvent(ArrayBackedReader reader, string cacheName)
		{
			CacheEventType cacheEventType = (CacheEventType)reader.ReadInt32();
			InternalCacheItemVersion internalCacheItemVersion = VelocityProperties.ReadVersion(reader);
			string text = VelocityProperties.ReadString(reader);
			string text2 = VelocityProperties.ReadString(reader);
			return new DataCacheOperationDescriptor(cacheName, text, text2, cacheEventType, internalCacheItemVersion);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00029D20 File Offset: 0x00027F20
		private static InternalCacheItemVersion ReadVersion(ArrayBackedReader reader)
		{
			long num = reader.ReadInt64();
			long num2 = reader.ReadInt64();
			return new InternalCacheItemVersion(num, num2);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00029D42 File Offset: 0x00027F42
		private static void WriteVersion(BinaryWriter writer, InternalCacheItemVersion version)
		{
			writer.Write(version.Epoch);
			writer.Write(version.Lsn);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00029D60 File Offset: 0x00027F60
		private static void WritePartitionNotificationRequest(PartitionNotificationRequest pRequest, BinaryWriter writer)
		{
			PartitionId partitionId = pRequest.PartitionId;
			VelocityProperties.WritePartitionId(partitionId, writer);
			ushort num = 0;
			if (pRequest.RegionList == null)
			{
				writer.Write(num);
			}
			else
			{
				if (pRequest.RegionList.Count <= 65535)
				{
					num = (ushort)pRequest.RegionList.Count;
				}
				writer.Write(num);
				for (int i = 0; i < (int)num; i++)
				{
					VelocityProperties.WriteString(writer, pRequest.RegionList[i]);
				}
			}
			VelocityProperties.WriteNotificationLsn(writer, pRequest.LastLsn);
			writer.Write(pRequest.CountNotificationToPoll);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00029DEC File Offset: 0x00027FEC
		private static void WritePartitionNotificationLsnRequest(PartitionNotificationLsnRequest pRequest, BinaryWriter writer)
		{
			PartitionId partitionId = pRequest.PartitionId;
			VelocityProperties.WritePartitionId(partitionId, writer);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00029E08 File Offset: 0x00028008
		private static PartitionNotificationRequest ReadPartitionNotificationRequest(ArrayBackedReader reader, string cacheName)
		{
			PartitionId partitionId = VelocityProperties.ReadPartitionId(reader, cacheName);
			ushort num = reader.ReadUInt16();
			List<string> list = null;
			if (num > 0)
			{
				list = new List<string>((int)num);
				for (int i = 0; i < (int)num; i++)
				{
					list.Add(VelocityProperties.ReadString(reader));
				}
			}
			NotificationLsn notificationLsn = VelocityProperties.ReadNotificationLsn(reader);
			int num2 = reader.ReadInt32();
			return new PartitionNotificationRequest(partitionId, num == 0, list, notificationLsn, num2);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00029E6C File Offset: 0x0002806C
		private static PartitionNotificationLsnRequest ReadPartitionNotificationLsnRequest(ArrayBackedReader reader, string cacheName)
		{
			PartitionId partitionId = VelocityProperties.ReadPartitionId(reader, cacheName);
			return new PartitionNotificationLsnRequest(partitionId);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00029E89 File Offset: 0x00028089
		private static void WritePartitionId(PartitionId id, BinaryWriter writer)
		{
			writer.Write(id.LowKey);
			writer.Write(id.HighKey);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00029EA4 File Offset: 0x000280A4
		private static NotificationLsn ReadNotificationLsn(ArrayBackedReader reader)
		{
			long num = reader.ReadInt64();
			long num2 = reader.ReadInt64();
			return new NotificationLsn(num, num2);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00029EC8 File Offset: 0x000280C8
		private static void WriteNotificationLsn(BinaryWriter writer, NotificationLsn lsn)
		{
			writer.Write(lsn.Epoch);
			writer.Write(lsn.Lsn);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00029EE4 File Offset: 0x000280E4
		private static PartitionId ReadPartitionId(ArrayBackedReader reader, string cacheName)
		{
			int num = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			if (num2 <= num)
			{
				throw new VelocityPacketFormatException(string.Format(CultureInfo.InvariantCulture, "Invalid partition id: low {0}, high {1}", new object[] { num, num2 }));
			}
			return new PartitionId(cacheName, num, num2);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00029F3C File Offset: 0x0002813C
		public static CacheLookupTableTransfer DeserializeLookupTable(byte[] array, string cacheName)
		{
			ArrayBackedReader arrayBackedReader = new ArrayBackedReader(array);
			VelocityProperties.AssertBufferAvailable(arrayBackedReader, 2);
			ushort num = arrayBackedReader.ReadUInt16();
			List<CacheLookupTableEntry> list = new List<CacheLookupTableEntry>((int)num);
			for (int i = 0; i < (int)num; i++)
			{
				VelocityProperties.AssertBufferAvailable(arrayBackedReader, 8);
				int num2 = arrayBackedReader.ReadInt32();
				int num3 = arrayBackedReader.ReadInt32();
				string text = VelocityProperties.ReadString(arrayBackedReader);
				VelocityProperties.AssertBufferAvailable(arrayBackedReader, 8);
				long num4 = arrayBackedReader.ReadInt64();
				list.Add(new CacheLookupTableEntry(new CachePartitionId(cacheName, num2, num3), new CachePartitionConfig(text, num4)));
			}
			VelocityProperties.AssertBufferAvailable(arrayBackedReader, 8);
			long num5 = arrayBackedReader.ReadInt64();
			string text2 = VelocityProperties.ReadString(arrayBackedReader);
			GenerationNumber generationNumber = new GenerationNumber(num5, text2);
			VelocityProperties.AssertBufferAvailable(arrayBackedReader, 2);
			num = arrayBackedReader.ReadUInt16();
			VersionRanges versionRanges = new VersionRanges();
			for (int j = 0; j < (int)num; j++)
			{
				VelocityProperties.AssertBufferAvailable(arrayBackedReader, 16);
				long num6 = arrayBackedReader.ReadInt64();
				long num7 = arrayBackedReader.ReadInt64();
				versionRanges.Ranges.Add(new VersionRange(num6, num7));
			}
			return new CacheLookupTableTransfer(list, versionRanges, generationNumber);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002A03C File Offset: 0x0002823C
		private static byte[] SerializeInternal<T>(T obj)
		{
			Func<byte[], IEnumerable<byte>> func = null;
			Func<byte[], byte, byte> func2 = null;
			if (obj == null)
			{
				return null;
			}
			byte[][] array = SerializationUtility.Serialize(obj);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			if (array.Length == 1)
			{
				return array[0];
			}
			IEnumerable<byte[]> enumerable = array;
			if (func == null)
			{
				func = (byte[] arr) => arr;
			}
			Func<byte[], IEnumerable<byte>> func3 = func;
			if (func2 == null)
			{
				func2 = (byte[] arr, byte element) => element;
			}
			return enumerable.SelectMany(func3, func2).ToArray<byte>();
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002A0A4 File Offset: 0x000282A4
		private static T DeserializeInternal<T>(byte[] sequence)
		{
			try
			{
				object obj = SerializationUtility.Deserialize(new byte[][] { sequence }, false);
				if (obj is T)
				{
					return (T)((object)obj);
				}
			}
			catch (SerializationException)
			{
			}
			return default(T);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0002A0F8 File Offset: 0x000282F8
		private static byte[] SerializeGuid(Guid guid)
		{
			if (guid == Guid.Empty)
			{
				return null;
			}
			return guid.ToByteArray();
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002A110 File Offset: 0x00028310
		private static Guid DeserializeGuid(byte[] array)
		{
			if (array.Length != 16)
			{
				throw new VelocityPacketFormatException("Guid parsing failed. Expected length {0}, actual {1}.", new object[] { 16, array.Length });
			}
			return new Guid(array);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002A154 File Offset: 0x00028354
		internal static byte[] SerializeVersion(Version version)
		{
			if (version == null)
			{
				return null;
			}
			byte[] array = new byte[16];
			ArrayBackedWriter arrayBackedWriter = new ArrayBackedWriter(array);
			arrayBackedWriter.Write(version.Major);
			arrayBackedWriter.Write(version.Minor);
			arrayBackedWriter.Write(version.Build);
			arrayBackedWriter.Write(version.Revision);
			return array;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002A1AC File Offset: 0x000283AC
		internal static Version DeserializeVersion(byte[] array)
		{
			if (array.Length != 16)
			{
				throw new VelocityPacketFormatException("Version parsing failed. Expected length {0}, actual {1}.", new object[] { 16, array.Length });
			}
			ArrayBackedReader arrayBackedReader = new ArrayBackedReader(array);
			int num = arrayBackedReader.ReadInt32();
			int num2 = arrayBackedReader.ReadInt32();
			int num3 = arrayBackedReader.ReadInt32();
			int num4 = arrayBackedReader.ReadInt32();
			return new Version(num, num2, num3, num4);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002A21A File Offset: 0x0002841A
		private static byte[] SerializeTags(IEnumerable<DataCacheTag> tags)
		{
			return SerializationUtility.SerializeTags(tags, VelocityProperties._encodingType);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002A228 File Offset: 0x00028428
		private static IList<DataCacheTag> DeserializeTags(byte[] buffer)
		{
			return SerializationUtility.DeserializeTags(buffer, VelocityProperties._encodingType).ToArray<DataCacheTag>();
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002A248 File Offset: 0x00028448
		private static void WriteString(BinaryWriter writer, string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				writer.Write(0);
				return;
			}
			byte[] bytes = VelocityProperties._encodingType.GetBytes(str);
			writer.Write((ushort)bytes.Length);
			writer.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002A288 File Offset: 0x00028488
		private static string ReadString(GenericReader reader)
		{
			VelocityProperties.AssertBufferAvailable(reader, 2);
			ushort num = reader.ReadUInt16();
			if (num == 0)
			{
				return string.Empty;
			}
			VelocityProperties.AssertBufferAvailable(reader, (int)num);
			return reader.ReadString((int)num, VelocityProperties._encodingType);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002A2C0 File Offset: 0x000284C0
		private static void AssertBufferAvailable(GenericReader reader, int bytes)
		{
			if (!reader.AreBytesAvailable(bytes))
			{
				throw new VelocityPacketFormatException("Buffer truncated. Expected: {0}, Actual: {1}", new object[]
				{
					bytes + reader.Position,
					reader.Length
				});
			}
		}

		// Token: 0x040008EF RID: 2287
		private const int _versionSize = 16;

		// Token: 0x040008F0 RID: 2288
		private static readonly Encoding _encodingType = new UTF8Encoding(false, false);
	}
}
