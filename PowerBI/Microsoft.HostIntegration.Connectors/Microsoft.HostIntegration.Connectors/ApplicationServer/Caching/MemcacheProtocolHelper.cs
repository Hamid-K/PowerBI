using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000165 RID: 357
	internal class MemcacheProtocolHelper
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x00023F60 File Offset: 0x00022160
		internal static ulong ReadValueAndFlags(string logSource, byte[][] value, bool flagsPresent, out uint flags)
		{
			flags = 0U;
			ulong num3;
			using (ChunkStream chunkStream = new ChunkStream(value))
			{
				int num = (int)chunkStream.Length;
				if (flagsPresent)
				{
					byte[] array = new byte[4];
					chunkStream.Read(array, 0, 4);
					flags = BitConverter.ToUInt32(array, 0);
					num -= 4;
				}
				byte[] array2 = new byte[num];
				chunkStream.Read(array2, 0, num);
				string @string = Encoding.ASCII.GetString(array2);
				ulong num2;
				if (!ulong.TryParse(@string, out num2))
				{
					throw new DataCacheException(logSource, 20, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 20), true);
				}
				num3 = num2;
			}
			return num3;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00024004 File Offset: 0x00022204
		internal static byte[][] WriteValueAndFlags(ulong counter, bool flagsPresent, uint flags)
		{
			int num = 8;
			if (flagsPresent)
			{
				num += 4;
			}
			byte[][] array;
			using (ChunkStream chunkStream = new ChunkStream(num))
			{
				if (flagsPresent)
				{
					byte[] bytes = BitConverter.GetBytes(flags);
					chunkStream.Write(bytes, 0, bytes.Length);
				}
				byte[] bytes2 = Encoding.ASCII.GetBytes(counter.ToString(CultureInfo.InvariantCulture));
				chunkStream.Write(bytes2, 0, bytes2.Length);
				array = chunkStream.ToChunkedArray();
			}
			return array;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00024080 File Offset: 0x00022280
		internal static Dictionary<string, byte[][]> GetMemcacheGeneralStatistics(ServiceConfigurationManager serviceConfigurationManager, OMNamedCacheStats cacheStats, Key filter)
		{
			Dictionary<string, byte[][]> memcacheGeneralStatistics = MemcacheProtocolHelper.GetMemcacheGeneralStatistics(null);
			ulong num = (ulong)(cacheStats.AddReqs + cacheStats.UpsertReqs);
			ulong getRequestMiss = (ulong)cacheStats.GetRequestMiss;
			ulong num2 = (ulong)(cacheStats.GetReqs - (long)getRequestMiss);
			memcacheGeneralStatistics.Add("curr_items", SerializationUtility.Serialize(cacheStats.GetObjectCount().ToString(CultureInfo.InvariantCulture)));
			memcacheGeneralStatistics.Add("bytes", SerializationUtility.Serialize(cacheStats.Size.ToString(CultureInfo.InvariantCulture)));
			memcacheGeneralStatistics.Add("cmd_get", SerializationUtility.Serialize(cacheStats.GetReqs.ToString(CultureInfo.InvariantCulture)));
			memcacheGeneralStatistics.Add("cmd_set", SerializationUtility.Serialize(num.ToString(CultureInfo.InvariantCulture)));
			memcacheGeneralStatistics.Add("get_hits", SerializationUtility.Serialize(num2.ToString(CultureInfo.InvariantCulture)));
			memcacheGeneralStatistics.Add("get_misses", SerializationUtility.Serialize(getRequestMiss.ToString(CultureInfo.InvariantCulture)));
			memcacheGeneralStatistics.Add("bytes_read", SerializationUtility.Serialize(cacheStats.IncomingBandwidth.ToString(CultureInfo.InvariantCulture)));
			memcacheGeneralStatistics.Add("bytes_written", SerializationUtility.Serialize(cacheStats.OutgoingBandwidth.ToString(CultureInfo.InvariantCulture)));
			memcacheGeneralStatistics.Add("limit_maxbytes", SerializationUtility.Serialize(MemcacheProtocolHelper.GetNodeMemorySize(serviceConfigurationManager).ToString(CultureInfo.InvariantCulture)));
			return MemcacheProtocolHelper.Filter(filter, memcacheGeneralStatistics);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000241E8 File Offset: 0x000223E8
		internal static Dictionary<string, byte[][]> GetMemcacheGeneralStatistics(Key filter, long? hostSize, long? totalGet, long? getMiss, long? totalUpsert, long? totalAdd, long? currentItems, long? totalBytes, long? bytesRead, long? bytesWritten)
		{
			Dictionary<string, byte[][]> memcacheGeneralStatistics = MemcacheProtocolHelper.GetMemcacheGeneralStatistics(null);
			if (hostSize != null)
			{
				memcacheGeneralStatistics.Add("limit_maxbytes", SerializationUtility.Serialize(hostSize.Value.ToString(CultureInfo.InvariantCulture)));
			}
			if (currentItems != null)
			{
				memcacheGeneralStatistics.Add("curr_items", SerializationUtility.Serialize(currentItems.Value.ToString(CultureInfo.InvariantCulture)));
			}
			if (totalBytes != null)
			{
				memcacheGeneralStatistics.Add("bytes", SerializationUtility.Serialize(totalBytes.Value.ToString(CultureInfo.InvariantCulture)));
			}
			if (totalGet != null)
			{
				memcacheGeneralStatistics.Add("cmd_get", SerializationUtility.Serialize(totalGet.Value.ToString(CultureInfo.InvariantCulture)));
			}
			if (totalUpsert != null && totalAdd != null)
			{
				memcacheGeneralStatistics.Add("cmd_set", SerializationUtility.Serialize(((ulong)(totalUpsert.Value + totalAdd.Value)).ToString(CultureInfo.InvariantCulture)));
			}
			if (totalGet != null && getMiss != null)
			{
				memcacheGeneralStatistics.Add("get_hits", SerializationUtility.Serialize(((ulong)(totalGet.Value - getMiss.Value)).ToString(CultureInfo.InvariantCulture)));
			}
			if (getMiss != null)
			{
				memcacheGeneralStatistics.Add("get_misses", SerializationUtility.Serialize(getMiss.Value.ToString(CultureInfo.InvariantCulture)));
			}
			if (bytesRead != null)
			{
				memcacheGeneralStatistics.Add("bytes_read", SerializationUtility.Serialize(bytesRead.Value.ToString(CultureInfo.InvariantCulture)));
			}
			if (bytesWritten != null)
			{
				memcacheGeneralStatistics.Add("bytes_written", SerializationUtility.Serialize(bytesWritten.Value.ToString(CultureInfo.InvariantCulture)));
			}
			return MemcacheProtocolHelper.Filter(filter, memcacheGeneralStatistics);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000243C0 File Offset: 0x000225C0
		internal static Dictionary<string, byte[][]> GetMemcacheGeneralStatistics(Key filter)
		{
			Dictionary<string, byte[][]> dictionary = new Dictionary<string, byte[][]>();
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				double totalSeconds = DateTime.Now.Subtract(currentProcess.StartTime).TotalSeconds;
				uint num = (uint)((totalSeconds > 0.0) ? totalSeconds : 0.0);
				uint num2 = (uint)DateTime.UtcNow.Subtract(ConfigManager.UnixEpoch).TotalSeconds;
				int num3 = IntPtr.Size * 8;
				TimeSpan userProcessorTime = currentProcess.UserProcessorTime;
				TimeSpan timeSpan = currentProcess.TotalProcessorTime.Subtract(userProcessorTime);
				dictionary.Add("pid", SerializationUtility.Serialize(currentProcess.Id.ToString(CultureInfo.InvariantCulture)));
				dictionary.Add("uptime", SerializationUtility.Serialize(num.ToString(CultureInfo.InvariantCulture)));
				dictionary.Add("time", SerializationUtility.Serialize(num2.ToString(CultureInfo.InvariantCulture)));
				dictionary.Add("version", SerializationUtility.Serialize("1.3"));
				dictionary.Add("pointer_size", SerializationUtility.Serialize(num3.ToString(CultureInfo.InvariantCulture)));
				dictionary.Add("rusage_user", SerializationUtility.Serialize(userProcessorTime.TotalSeconds.ToString(CultureInfo.InvariantCulture)));
				dictionary.Add("rusage_system", SerializationUtility.Serialize(timeSpan.TotalSeconds.ToString(CultureInfo.InvariantCulture)));
				dictionary.Add("curr_connections", SerializationUtility.Serialize(TcpSocketChannel.CurrentIncomingChannelCount.ToString(CultureInfo.InvariantCulture)));
				dictionary.Add("total_connections", SerializationUtility.Serialize(TcpSocketChannel.TotalIncomingChannelCount.ToString(CultureInfo.InvariantCulture)));
			}
			return MemcacheProtocolHelper.Filter(filter, dictionary);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000245A0 File Offset: 0x000227A0
		private static Dictionary<string, byte[][]> Filter(Key filter, Dictionary<string, byte[][]> stats)
		{
			if (filter == null)
			{
				return stats;
			}
			Dictionary<string, byte[][]> dictionary = new Dictionary<string, byte[][]>();
			byte[][] array;
			if (stats.TryGetValue(filter.StringValue, out array))
			{
				dictionary.Add(filter.StringValue, array);
			}
			return dictionary;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000245D8 File Offset: 0x000227D8
		internal static Dictionary<string, byte[][]> GetMemcachedSettings(MemcacheWireProtocol protocol, ServiceConfigurationManager serviceConfigurationManager, INamedCacheConfiguration cacheConfiguration)
		{
			Dictionary<string, byte[][]> memcachedSettings = MemcacheProtocolHelper.GetMemcachedSettings(protocol.ListeningPort, cacheConfiguration.EvictionType);
			memcachedSettings.Add("maxbytes", SerializationUtility.Serialize(MemcacheProtocolHelper.GetNodeMemorySize(serviceConfigurationManager).ToString(CultureInfo.InvariantCulture)));
			memcachedSettings.Add("item_size_max", SerializationUtility.Serialize(MemcacheProtocolHelper.GetItemSizeMax(serviceConfigurationManager).ToString(CultureInfo.InvariantCulture)));
			memcachedSettings.Add("chunk_size", SerializationUtility.Serialize(serviceConfigurationManager.BufferSize.ToString(CultureInfo.InvariantCulture)));
			return memcachedSettings;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00024664 File Offset: 0x00022864
		internal static Dictionary<string, byte[][]> GetMemcachedSettings(int tcpPort, EvictionType evictionType, long? maxBytes, int? maxItemSize, int? chunkSize)
		{
			Dictionary<string, byte[][]> memcachedSettings = MemcacheProtocolHelper.GetMemcachedSettings(tcpPort, evictionType);
			if (maxBytes != null)
			{
				memcachedSettings.Add("maxbytes", SerializationUtility.Serialize(maxBytes.Value.ToString(CultureInfo.InvariantCulture)));
			}
			if (maxItemSize != null)
			{
				memcachedSettings.Add("item_size_max", SerializationUtility.Serialize(maxItemSize.Value.ToString(CultureInfo.InvariantCulture)));
			}
			if (chunkSize != null)
			{
				memcachedSettings.Add("chunk_size", SerializationUtility.Serialize(chunkSize.Value.ToString(CultureInfo.InvariantCulture)));
			}
			return memcachedSettings;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00024704 File Offset: 0x00022904
		internal static Dictionary<string, byte[][]> GetMemcachedSettings(int tcpPort, EvictionType evictionType)
		{
			return new Dictionary<string, byte[][]>
			{
				{
					"maxconns",
					SerializationUtility.Serialize(short.MaxValue.ToString(CultureInfo.InvariantCulture))
				},
				{
					"tcpport",
					SerializationUtility.Serialize(tcpPort.ToString(CultureInfo.InvariantCulture))
				},
				{
					"inter",
					SerializationUtility.Serialize("NULL")
				},
				{
					"verbosity",
					SerializationUtility.Serialize(MemcacheProtocolHelper.GetVerbosity())
				},
				{
					"evictions",
					SerializationUtility.Serialize(MemcacheProtocolHelper.GetEvictionStatus(evictionType))
				},
				{
					"domain_socket",
					SerializationUtility.Serialize("NULL")
				},
				{
					"stat_key_prefix",
					SerializationUtility.Serialize(":")
				},
				{
					"detail_enabled",
					SerializationUtility.Serialize("no")
				},
				{
					"cas_enabled",
					SerializationUtility.Serialize("yes")
				},
				{
					"auth_enabled_sasl",
					SerializationUtility.Serialize("no")
				},
				{
					"maxconns_fast",
					SerializationUtility.Serialize("no")
				}
			};
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00024814 File Offset: 0x00022A14
		internal static void LogSent(IEnumerable<ArraySegment<byte>> buffers)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				foreach (ArraySegment<byte> arraySegment in buffers)
				{
					MemcacheProtocolHelper.Log(arraySegment, false);
				}
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00024864 File Offset: 0x00022A64
		internal static void LogReceive(byte[] array, int offset, int count)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				MemcacheProtocolHelper.Log(new ArraySegment<byte>(array, offset, count), true);
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002487C File Offset: 0x00022A7C
		internal static long GetNodeMemorySize(ServiceConfigurationManager serviceConfigurationManager)
		{
			return serviceConfigurationManager.NodeProperties.Size * 1024L * 1024L;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00024897 File Offset: 0x00022A97
		private static string GetEvictionStatus(EvictionType evictionType)
		{
			if (evictionType == EvictionType.None)
			{
				return "off";
			}
			return "on";
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000248A8 File Offset: 0x00022AA8
		internal static int GetItemSizeMax(ServiceConfigurationManager serviceConfigurationManager)
		{
			int num;
			if (serviceConfigurationManager == null || serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxBufferSize == -1)
			{
				num = 8388608;
			}
			else
			{
				num = serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxBufferSize;
			}
			return num;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000248E8 File Offset: 0x00022AE8
		private static string GetVerbosity()
		{
			int num = 0;
			switch (Provider.DiagnosticTraceLevel)
			{
			case TraceLevel.Off:
				num = 0;
				break;
			case TraceLevel.Error:
			case TraceLevel.Warning:
			case TraceLevel.Info:
				num = 1;
				break;
			case TraceLevel.Verbose:
				num = 2;
				break;
			}
			return num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00024930 File Offset: 0x00022B30
		private static void Log(ArraySegment<byte> arraySegment, bool receive)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = Math.Min(arraySegment.Count, 512);
				for (int i = arraySegment.Offset; i < arraySegment.Offset + num; i++)
				{
					stringBuilder.Append(arraySegment.Array[i].ToString("X", CultureInfo.InvariantCulture).PadLeft(2, '0'));
					if (i % 4 == 3)
					{
						stringBuilder.Append(" ");
					}
				}
				string @string = Encoding.ASCII.GetString(arraySegment.Array, arraySegment.Offset, num);
				EventLogWriter.WriteVerbose<char, StringBuilder, string>("DistributedCache.MemcacheWireProtocol", "{0} {1} [{2}]", receive ? '>' : '<', stringBuilder, @string);
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000249EC File Offset: 0x00022BEC
		internal static ErrStatus ConvertToErrStatus(int errorCode, int subStatus)
		{
			if (errorCode <= 17)
			{
				if (errorCode != 1)
				{
					switch (errorCode)
					{
					case 6:
						return ErrStatus.KEY_DOES_NOT_EXIST;
					case 7:
						return ErrStatus.INTERNAL_ERROR;
					case 8:
						return ErrStatus.KEY_ALREADY_EXISTS;
					case 9:
						return ErrStatus.NAMED_CACHE_DOES_NOT_EXIST;
					default:
						if (errorCode != 17)
						{
							return ErrStatus.INTERNAL_ERROR;
						}
						if (subStatus == 6)
						{
							return ErrStatus.OUT_OF_MEMORY;
						}
						if (subStatus == 5)
						{
							return ErrStatus.SERVER_DEAD;
						}
						return ErrStatus.INTERNAL_ERROR;
					}
				}
			}
			else
			{
				if (errorCode == 20)
				{
					return ErrStatus.NOT_SUPPORTED_VALUE_FORMAT;
				}
				switch (errorCode)
				{
				case 2002:
					return ErrStatus.KEY_DOES_NOT_EXIST;
				case 2003:
					break;
				default:
					if (errorCode != 20001)
					{
						return ErrStatus.INTERNAL_ERROR;
					}
					return ErrStatus.OVERFLOW;
				}
			}
			return ErrStatus.VERSION_MISMATCH;
		}
	}
}
