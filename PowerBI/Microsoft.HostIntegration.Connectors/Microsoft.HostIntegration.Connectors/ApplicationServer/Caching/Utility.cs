using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Fabric.Common;
using Microsoft.Win32;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000396 RID: 918
	internal static class Utility
	{
		// Token: 0x06002044 RID: 8260 RVA: 0x0006240C File Offset: 0x0006060C
		public static byte[] PadTo128Bit(int intValue)
		{
			int num = 4;
			byte[] array = new byte[16];
			uint num2 = 255U;
			int i = 0;
			while (i < num)
			{
				array[16 - (num - i)] = byte.Parse(string.Concat((uint)(intValue & (int)num2) >> i * 8), NumberFormatInfo.InvariantInfo);
				i++;
				num2 <<= 8;
			}
			return array;
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x00062468 File Offset: 0x00060668
		internal static bool IsValidFileException(Exception e)
		{
			return e is IOException || e is UnauthorizedAccessException || e is SecurityException;
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x00062485 File Offset: 0x00060685
		internal static bool IsAddressAlreadyInUseException(Exception e)
		{
			return Utility.IsException<AddressAlreadyInUseException>(e);
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0006248D File Offset: 0x0006068D
		internal static bool IsValidBulkGetItemErrorCode(ErrStatus errStatus)
		{
			return errStatus == ErrStatus.KEY_DOES_NOT_EXIST || errStatus == ErrStatus.REGION_DOES_NOT_EXIST;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x0006249A File Offset: 0x0006069A
		internal static bool IsGetBatchRequest(VelocityPacketType type)
		{
			return type == VelocityPacketType.GetBatchByNone || type == VelocityPacketType.GetBatchByIntersection || type == VelocityPacketType.GetBatchByUnion;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000624AD File Offset: 0x000606AD
		internal static AckNack GetResponseSuccessStatus(ErrStatus status)
		{
			if (status != ErrStatus.UNINITIALIZED_ERROR)
			{
				return AckNack.Nack;
			}
			return AckNack.Ack;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000624B8 File Offset: 0x000606B8
		internal static TraceLevel GetTraceLevel(int value, out VelocityDiagMode velMode)
		{
			TraceLevel traceLevel = TraceLevel.Warning;
			velMode = VelocityDiagMode.WarningWithFailedReq;
			switch (value)
			{
			case -1:
				traceLevel = TraceLevel.Off;
				velMode = VelocityDiagMode.NoBuffering;
				break;
			case 0:
				traceLevel = TraceLevel.Error;
				velMode = VelocityDiagMode.NoBuffering;
				break;
			case 1:
				traceLevel = TraceLevel.Warning;
				velMode = VelocityDiagMode.WarningWithFailedReq;
				break;
			case 2:
				traceLevel = TraceLevel.Info;
				velMode = VelocityDiagMode.InfoWithAllReq;
				break;
			case 3:
				traceLevel = TraceLevel.Info;
				velMode = VelocityDiagMode.NoBuffering;
				break;
			case 4:
				traceLevel = TraceLevel.Verbose;
				velMode = VelocityDiagMode.NoBuffering;
				break;
			}
			return traceLevel;
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00062518 File Offset: 0x00060718
		public static TraceLevel GetTraceLevel(string value, out VelocityDiagMode velMode)
		{
			if (value == null)
			{
				velMode = VelocityDiagMode.NoBuffering;
				return TraceLevel.Off;
			}
			velMode = VelocityDiagMode.WarningWithFailedReq;
			TraceLevel traceLevel;
			if (value.Equals("ERROR", StringComparison.OrdinalIgnoreCase))
			{
				traceLevel = TraceLevel.Error;
				velMode = VelocityDiagMode.NoBuffering;
			}
			else if (value.Equals("WARNING", StringComparison.OrdinalIgnoreCase))
			{
				traceLevel = TraceLevel.Warning;
				velMode = VelocityDiagMode.NoBuffering;
			}
			else if (value.Equals("INFO", StringComparison.OrdinalIgnoreCase))
			{
				traceLevel = TraceLevel.Info;
				velMode = VelocityDiagMode.NoBuffering;
			}
			else if (value.Equals("VERBOSE", StringComparison.OrdinalIgnoreCase))
			{
				traceLevel = TraceLevel.Verbose;
				velMode = VelocityDiagMode.NoBuffering;
			}
			else if (value.Equals("WARNINGWITHFAILEDREQ", StringComparison.OrdinalIgnoreCase))
			{
				traceLevel = TraceLevel.Warning;
				velMode = VelocityDiagMode.WarningWithFailedReq;
			}
			else if (value.Equals("INFOWITHALLREQ", StringComparison.OrdinalIgnoreCase))
			{
				traceLevel = TraceLevel.Info;
				velMode = VelocityDiagMode.InfoWithAllReq;
			}
			else if (value.Equals("NOBUFFERING", StringComparison.OrdinalIgnoreCase))
			{
				traceLevel = TraceLevel.Info;
				velMode = VelocityDiagMode.NoBuffering;
			}
			else if (value.Equals("WARNINGWITHFAILEDREQEXT", StringComparison.OrdinalIgnoreCase))
			{
				traceLevel = TraceLevel.Warning;
				velMode = VelocityDiagMode.WarningWithFailedReqExt;
			}
			else if (value.Equals("INFOWITHALLREQLITE", StringComparison.OrdinalIgnoreCase))
			{
				traceLevel = TraceLevel.Info;
				velMode = VelocityDiagMode.InfoWithAllReqLite;
			}
			else
			{
				traceLevel = TraceLevel.Off;
				velMode = VelocityDiagMode.NoBuffering;
			}
			return traceLevel;
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000625FE File Offset: 0x000607FE
		internal static bool IsException<T>(Exception e) where T : Exception
		{
			while (e != null)
			{
				if (e is T)
				{
					return true;
				}
				e = e.InnerException;
			}
			return false;
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00062618 File Offset: 0x00060818
		internal static bool ValidateName(string name)
		{
			return name != null && Utility.IsValidName(name);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00062625 File Offset: 0x00060825
		public static bool IsValidName(string name)
		{
			return Regex.IsMatch(name, Utility._validNamePattern);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00062632 File Offset: 0x00060832
		internal static string GetFQDN()
		{
			return SecurityCommon.GetHostEntry("localhost").HostName;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00062643 File Offset: 0x00060843
		internal static string GetFQDN(string serverName)
		{
			return SecurityCommon.GetHostEntry(serverName).HostName;
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00062650 File Offset: 0x00060850
		internal static string GetNetBiosName(string fqdn)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				return null;
			}
			return fqdn.Split(new char[] { '.' })[0];
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x0006267C File Offset: 0x0006087C
		internal static IHostConfiguration GetHostUsingHostAndServiceNames(string hostName, string serviceName, List<IHostConfiguration> hosts)
		{
			if (string.IsNullOrEmpty(hostName) || hosts == null || hosts.Count == 0)
			{
				return null;
			}
			foreach (IHostConfiguration hostConfiguration in hosts)
			{
				if (serviceName.Equals(hostConfiguration.ServiceName) && hostName.Equals(hostConfiguration.Name, StringComparison.OrdinalIgnoreCase))
				{
					return hostConfiguration;
				}
			}
			foreach (IHostConfiguration hostConfiguration2 in hosts)
			{
				if (serviceName.Equals(hostConfiguration2.ServiceName) && SecurityCommon.AreSameMachines(hostName, hostConfiguration2.Name))
				{
					return hostConfiguration2;
				}
			}
			return null;
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00062754 File Offset: 0x00060954
		public static bool RemoteEndpointUsesNetDataConractSerializer(ClientVersionInfo versionInfo)
		{
			return versionInfo == null || (versionInfo.CodeVersion >= 1L && versionInfo.CodeVersion <= Utility.HighestClientVersionToSupportOnlyNetDataContract()) || versionInfo.CodeVersion == 1000L;
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x00062781 File Offset: 0x00060981
		public static bool RemoteEndpointUsesLegacyWcfValueFlags(ClientVersionInfo versionInfo)
		{
			return versionInfo == null || versionInfo.CodeVersion <= Utility.HighestClientVersionToSupportLegacyWcfFlags_OnPrem() || (versionInfo.CodeVersion >= 1000L && versionInfo.CodeVersion <= Utility.HighestClientVersionToSupportLegacyWcfFlags_Vas());
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000627B5 File Offset: 0x000609B5
		private static long HighestClientVersionToSupportLegacyWcfFlags_OnPrem()
		{
			return 3L;
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000627B9 File Offset: 0x000609B9
		private static long HighestClientVersionToSupportLegacyWcfFlags_Vas()
		{
			return 1001L;
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000627C1 File Offset: 0x000609C1
		private static long HighestClientVersionToSupportOnlyNetDataContract()
		{
			return 2L;
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000627C8 File Offset: 0x000609C8
		internal static IHostConfiguration GetHostUsingHostAndServicePort(string hostName, int servicePort, List<IHostConfiguration> hosts)
		{
			if (string.IsNullOrEmpty(hostName) || hosts == null || hosts.Count == 0)
			{
				return null;
			}
			foreach (IHostConfiguration hostConfiguration in hosts)
			{
				if (servicePort.Equals(hostConfiguration.ServicePort) && SecurityCommon.AreSameMachines(hostName, hostConfiguration.Name))
				{
					return hostConfiguration;
				}
			}
			return null;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x00062848 File Offset: 0x00060A48
		internal static string GetServiceUri(string hostName, int portNo, TransportProtocol protocol)
		{
			if (protocol == TransportProtocol.NetTcp)
			{
				return "net.tcp://" + hostName + ":" + string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { portNo });
			}
			if (protocol == TransportProtocol.Http)
			{
				return "http://" + hostName + ":" + string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { portNo });
			}
			if (protocol == TransportProtocol.Https)
			{
				return "https://" + hostName + ":" + string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { portNo });
			}
			throw new InvalidOperationException("Invalid protocol");
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000628FD File Offset: 0x00060AFD
		internal static bool IsOlderThanCodeVersion3Client(ClientVersionInfo versionInfo)
		{
			return versionInfo == null;
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00062904 File Offset: 0x00060B04
		internal static int GetRegionId(string regionName, int systemRegionCount)
		{
			if (RegionNameProvider.IsSystemRegion(regionName))
			{
				int num = int.Parse(regionName.Substring("Default_Region_".Length), NumberFormatInfo.InvariantInfo);
				double num2 = (double)((systemRegionCount > 1) ? (systemRegionCount - 1) : 1);
				double num3 = (double)num / num2;
				return (int)(num3 * 2147483647.0 + (1.0 - num3) * -2147483648.0);
			}
			return (int)CsHash32.ComputeString(regionName, 0U, true);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x00062970 File Offset: 0x00060B70
		internal static bool IsDataLoss(long newEpoch, long oldEpoch)
		{
			return (newEpoch & 16777215L) != (oldEpoch & 16777215L);
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x00062988 File Offset: 0x00060B88
		internal static DateTime AddTimeSpanToDateTime(TimeSpan ttl)
		{
			DateTime minValue = DateTime.MinValue;
			if (DateTime.MaxValue.Subtract(minValue) > ttl)
			{
				return minValue.Add(ttl);
			}
			return DateTime.MaxValue;
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000629BF File Offset: 0x00060BBF
		internal static long AddTimeSpanToCurrentCounter(TimeSpan ttl)
		{
			return Utility.AddTimeSpanToCounter(Stopwatch.GetTimestamp(), ttl);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000629CC File Offset: 0x00060BCC
		internal static long AddTimeSpanToCurrentCounter(long ttl)
		{
			return Utility.AddTimeSpanToCounter(Stopwatch.GetTimestamp(), ttl);
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000629D9 File Offset: 0x00060BD9
		internal static long AddTimeSpanToCurrentCounter(long counter, long ttl)
		{
			return Utility.AddTimeSpanToCounter(counter, ttl);
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000629E2 File Offset: 0x00060BE2
		internal static long ConvertTimeSpanToCurrentCounter(TimeSpan span)
		{
			return Utility.AddTimeSpanToCounter(0L, span);
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000629EC File Offset: 0x00060BEC
		internal static long AddTimeSpanToCounter(long counter, TimeSpan span)
		{
			long num = long.MaxValue;
			checked
			{
				if (span != TimeSpan.MaxValue)
				{
					try
					{
						long num2 = (long)(unchecked(span.TotalSeconds * (double)Stopwatch.Frequency));
						num = counter + num2;
					}
					catch (OverflowException)
					{
						num = long.MaxValue;
					}
				}
				return num;
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00062A44 File Offset: 0x00060C44
		internal static long AddTimeSpanToCounter(long counter, long span)
		{
			long num = long.MaxValue;
			if (span != 9223372036854775807L)
			{
				try
				{
					num = checked(counter + span);
				}
				catch (OverflowException)
				{
					num = long.MaxValue;
				}
			}
			return num;
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00062A8C File Offset: 0x00060C8C
		internal static long GetTimeStampCounterFromDateTime(DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
			{
				return 0L;
			}
			if (dateTime == DateTime.MaxValue)
			{
				return long.MaxValue;
			}
			return Utility.AddTimeSpanToCurrentCounter(dateTime - DateTime.MinValue);
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00062AC5 File Offset: 0x00060CC5
		internal static long ConvertToTimeStampCounterFromDateTime(DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
			{
				return 0L;
			}
			if (dateTime == DateTime.MaxValue)
			{
				return long.MaxValue;
			}
			return Utility.ConvertTimeSpanToCurrentCounter(dateTime - DateTime.MinValue);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00062B00 File Offset: 0x00060D00
		internal static TimeSpan ConvertCounterToTimeSpan(long counter, long baseCounter)
		{
			if (counter == 9223372036854775807L)
			{
				return TimeSpan.MaxValue;
			}
			if (counter <= baseCounter)
			{
				return TimeSpan.Zero;
			}
			TimeSpan timeSpan;
			try
			{
				double num = (double)(counter - baseCounter) / (double)Stopwatch.Frequency;
				timeSpan = TimeSpan.FromSeconds(num);
			}
			catch (OverflowException)
			{
				timeSpan = TimeSpan.MaxValue;
			}
			return timeSpan;
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00062B58 File Offset: 0x00060D58
		internal static long ConvertRemoteSpanToLocalTicks(long remoteSpan, long remoteFrequency)
		{
			long num = long.MaxValue;
			checked
			{
				try
				{
					num = remoteSpan / remoteFrequency * Stopwatch.Frequency;
					num += Stopwatch.GetTimestamp();
				}
				catch (OverflowException)
				{
					num = long.MaxValue;
				}
				return num;
			}
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x00062BA4 File Offset: 0x00060DA4
		internal static bool IsExpiredTimeStamp(long currentTimeStampValue, long timeToLive)
		{
			return currentTimeStampValue >= timeToLive;
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x00062BAD File Offset: 0x00060DAD
		internal static bool IsExpiredTimeStamp(long timeToLive)
		{
			return Stopwatch.GetTimestamp() >= timeToLive;
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00062BBA File Offset: 0x00060DBA
		internal static bool IsValidExpirationSettings(INamedCacheConfiguration config)
		{
			return (config.IsExpirable || config.ExpirationType == ExpirationType.None) && (!config.IsExpirable || config.ExpirationType != ExpirationType.None);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x00062BE1 File Offset: 0x00060DE1
		internal static void UpdateExpirationSettings(INamedCacheConfiguration config)
		{
			if (!config.IsExpirable && ExpirationType.NotProvided == config.ExpirationType)
			{
				config.ExpirationType = ExpirationType.None;
				return;
			}
			if (config.IsExpirable && ExpirationType.NotProvided == config.ExpirationType)
			{
				config.ExpirationType = ExpirationType.AbsoluteExpiration;
			}
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x00062C14 File Offset: 0x00060E14
		internal static string GetEntryIfPresent(ICollection<string> keys, string key, IEnumerable<string> prefixes, bool throwIfNotFound)
		{
			string text = null;
			if (prefixes == null)
			{
				if (keys.Contains(key))
				{
					text = key;
				}
			}
			else
			{
				foreach (string text2 in prefixes)
				{
					string text3 = text2 + key;
					if (keys.Contains(text3))
					{
						text = text3;
						break;
					}
				}
			}
			if (text == null && throwIfNotFound)
			{
				throw new DataCacheException(string.Format(CultureInfo.CurrentUICulture, "Key {0} not found in the collection", new object[] { key }));
			}
			return text;
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x00062CA8 File Offset: 0x00060EA8
		internal static void AddInfo(RequestBody request, ResponseBody response)
		{
			response.ClientReqId = request.ClientReqId;
			response.ServiceReqId = request.ServiceReqId;
			if (request.IsTrackingIdPresent)
			{
				response.ResponseTrackingId = request.RequestTrackingId;
				response.IsTrackingIdPresent = true;
			}
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00062CDD File Offset: 0x00060EDD
		internal static void AddInfo(ResponseBody sourceResponse, ResponseBody targetResponse)
		{
			targetResponse.ClientReqId = sourceResponse.ClientReqId;
			targetResponse.ServiceReqId = sourceResponse.ServiceReqId;
			if (sourceResponse.IsTrackingIdPresent)
			{
				targetResponse.ResponseTrackingId = sourceResponse.ResponseTrackingId;
				targetResponse.IsTrackingIdPresent = true;
			}
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00062D12 File Offset: 0x00060F12
		internal static Message CreateMessage(string action)
		{
			return Message.CreateMessage(MessageVersion.Soap12WSAddressing10, action);
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x00062D20 File Offset: 0x00060F20
		internal static Message CreateMessage(string action, object body)
		{
			IBinarySerializable binarySerializable = body as IBinarySerializable;
			if (binarySerializable != null)
			{
				return Message.CreateMessage(MessageVersion.Soap12WSAddressing10, action, SerializationUtility.SerializeBinaryFormat(binarySerializable));
			}
			return Message.CreateMessage(MessageVersion.Soap12WSAddressing10, action, body);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x00062D58 File Offset: 0x00060F58
		internal static T GetMessageHeader<T>(Message message, string name, string ns)
		{
			int num = message.Headers.FindHeader(name, ns);
			if (num != -1)
			{
				return message.Headers.GetHeader<T>(num);
			}
			return default(T);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x00062D8D File Offset: 0x00060F8D
		internal static bool IsBinderNeededInClientResponse(ReqType req)
		{
			return req == ReqType.GET_NAMED_CACHE_CONFIGURATION;
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x00062D94 File Offset: 0x00060F94
		internal static void LogException(Exception e, string source, TraceLevel logLevel, int id)
		{
			if (Provider.IsEnabled(logLevel))
			{
				switch (logLevel)
				{
				case TraceLevel.Error:
					EventLogWriter.WriteError(source, Utility._logFormatWithChannelID, new object[] { id, e });
					return;
				case TraceLevel.Warning:
					EventLogWriter.WriteWarning(source, Utility._logFormatWithChannelID, new object[] { id, e });
					return;
				case TraceLevel.Info:
					EventLogWriter.WriteInfo(source, Utility._logFormatWithChannelID, new object[] { id, e });
					return;
				default:
					ReleaseAssert.Fail("Only errors and warnings are allowed.", new object[0]);
					break;
				}
			}
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00062E38 File Offset: 0x00061038
		public static Type LoadAzureClientAssembly()
		{
			Assembly assembly = Assembly.Load(new AssemblyName("Microsoft.ApplicationServer.Caching.AzureClientHelper"));
			return assembly.GetType("Microsoft.ApplicationServer.Caching.AzureClientHelper.RoleUtility");
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00062E60 File Offset: 0x00061060
		internal static void LogMessage(string msg, string source, TraceLevel logLevel)
		{
			if (Provider.IsEnabled(logLevel))
			{
				switch (logLevel)
				{
				case TraceLevel.Error:
					EventLogWriter.WriteError(source, "{0}:  Time={1}", new object[]
					{
						msg,
						DateTime.UtcNow
					});
					return;
				case TraceLevel.Warning:
					EventLogWriter.WriteWarning(source, "{0}:  Time={1}", new object[]
					{
						msg,
						DateTime.UtcNow
					});
					return;
				case TraceLevel.Info:
					EventLogWriter.WriteInfo(source, "{0}:  Time={1}", new object[]
					{
						msg,
						DateTime.UtcNow
					});
					return;
				case TraceLevel.Verbose:
					EventLogWriter.WriteVerbose<string, DateTime>(source, "{0}:  Time={1}", msg, DateTime.UtcNow);
					return;
				default:
					ReleaseAssert.Fail("Unsupported loglevel " + logLevel, new object[0]);
					break;
				}
			}
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x00062F34 File Offset: 0x00061134
		internal static void LogException(Exception e, string source, TraceLevel logLevel)
		{
			if (Provider.IsEnabled(logLevel))
			{
				switch (logLevel)
				{
				case TraceLevel.Error:
					EventLogWriter.WriteError(source, Utility._logFormat, new object[] { e });
					return;
				case TraceLevel.Warning:
					EventLogWriter.WriteWarning(source, Utility._logFormat, new object[] { e });
					return;
				default:
					ReleaseAssert.Fail("Only errors and warnings are allowed.", new object[0]);
					break;
				}
			}
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x00062FA0 File Offset: 0x000611A0
		internal static void LogException(Exception e, string source, TraceLevel logLevel, string cacheName, ErrStatus errorStatus)
		{
			if (Provider.IsEnabled(logLevel))
			{
				switch (logLevel)
				{
				case TraceLevel.Error:
					EventLogWriter.WriteError(source, Utility._logFormatWithChannelInfo, new object[] { e, cacheName, errorStatus });
					return;
				case TraceLevel.Warning:
					EventLogWriter.WriteWarning(source, Utility._logFormatWithChannelInfo, new object[] { e, cacheName, errorStatus });
					return;
				case TraceLevel.Info:
					EventLogWriter.WriteInfo(source, Utility._logFormatWithChannelInfo, new object[] { e, cacheName, errorStatus });
					return;
				default:
					ReleaseAssert.Fail("Only errors, warnings and info are allowed.", new object[0]);
					break;
				}
			}
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x00063052 File Offset: 0x00061252
		internal static double StopwatchTicksToMilliseconds(long ticks)
		{
			return (double)ticks / Utility.StopwatchTicksPerMillisecond;
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x0006305C File Offset: 0x0006125C
		internal static long StopwatchTicksToSystemTicks(long ticks)
		{
			return (long)((double)ticks / Utility.StopwatchTicksPerSystemTick);
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x00063067 File Offset: 0x00061267
		internal static long MillisecondsToStopwatchTicks(double milliseconds)
		{
			return (long)(milliseconds * Utility.StopwatchTicksPerMillisecond);
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x00063074 File Offset: 0x00061274
		internal static int Mix1(int cache, int region)
		{
			int num = region & 65535;
			return num | (cache & -65536);
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x00063098 File Offset: 0x00061298
		internal static int Mix2(int cache, int region)
		{
			int num = region & 252645135;
			return num | (cache & -252645136);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000630BC File Offset: 0x000612BC
		internal static int Mix3(int cache, int region)
		{
			int num = region & 1431655765;
			return num | (cache & -1431655766);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000630DE File Offset: 0x000612DE
		internal static int Mix4(int cache, int region)
		{
			return cache ^ region;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000630E4 File Offset: 0x000612E4
		internal static int Mix5(int cache, int region)
		{
			int num = region & 16777215;
			return num | (cache & -16777216);
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x00063106 File Offset: 0x00061306
		internal static string GetErrorMessage(CultureInfo cultureInfo, int errorCode)
		{
			return GlobalResourceLoader.GetString(cultureInfo, errorCode);
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x0006310F File Offset: 0x0006130F
		internal static bool IsHighAvailabilityBlocked(int replicaCount)
		{
			return replicaCount > 1 && !ConfigManager.IgnoreHighAvailabilityCheck && !NativeMethods.IsHighAvailabilityEnabledEdition();
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00063128 File Offset: 0x00061328
		internal static bool ExecuteCommand(string command, out string result)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c \"" + command + "\"");
			processStartInfo.UseShellExecute = false;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			bool flag;
			using (Process process = new Process())
			{
				process.StartInfo = processStartInfo;
				try
				{
					process.Start();
					process.WaitForExit(60000);
					if (!process.HasExited)
					{
						process.Kill();
						process.WaitForExit();
					}
					if (process.ExitCode == 0)
					{
						result = process.StandardOutput.ReadToEnd();
						flag = true;
					}
					else
					{
						result = process.StandardError.ReadToEnd();
						flag = false;
					}
				}
				catch (Win32Exception ex)
				{
					result = ex.ToString();
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x00063208 File Offset: 0x00061408
		internal static bool ExecuteCommand(string filename, string arguments, out string result)
		{
			string text = filename + ' ' + arguments;
			return Utility.ExecuteCommand(text, out result);
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x0006322C File Offset: 0x0006142C
		internal static ulong GetSystemMemorySize()
		{
			ulong num;
			ulong num2;
			MemoryStatus.GetStatus(out num, out num2);
			return num;
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00063244 File Offset: 0x00061444
		internal static long ConvertToHostSizeFromPercent(int percent)
		{
			ulong num = Utility.GetSystemMemorySize() >> 20;
			int num2 = percent;
			if (CloudUtility.IsVASDeployment && CloudUtility.IsServiceRunningOnDevfabric())
			{
				num2 = 1;
			}
			num = (ulong)(num * ((float)num2 / 100f));
			if (!ConfigManager.Win64BitInstallation && num > 1228UL)
			{
				num = 1228UL;
			}
			return (long)num;
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00063295 File Offset: 0x00061495
		internal static long GetDefaultHostSize()
		{
			return Utility.ConvertToHostSizeFromPercent(50);
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000632A0 File Offset: 0x000614A0
		internal static int Get2DByteArraySize(byte[][] data)
		{
			if (data == null || data.Length == 0)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < data.Length; i++)
			{
				num += ((data[i] != null) ? data[i].Length : 0);
			}
			return num;
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00058766 File Offset: 0x00056966
		internal static int GetCustomHashCode(string key)
		{
			return (int)CsHash32.ComputeString(key, 0U, false);
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000632D8 File Offset: 0x000614D8
		internal static ClusterConfigElement GetClusterConfigSettings(string provider, string connString)
		{
			string text;
			if (provider.Equals("xml", StringComparison.OrdinalIgnoreCase))
			{
				text = Path.Combine(connString, "ClusterConfig.xml");
			}
			else
			{
				text = connString;
			}
			return new ClusterConfigElement(provider, text);
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00063310 File Offset: 0x00061510
		internal static string GetInstallPathFromRegistry()
		{
			string text = string.Empty;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\AppFabric\\V1.0"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("InstallPath");
					if (value != null)
					{
						text = value.ToString();
					}
				}
			}
			return text;
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x0006336C File Offset: 0x0006156C
		public static long ConvertToLong(int msb, int lsb)
		{
			return (long)(((ulong)msb << 32) | (ulong)lsb);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00063384 File Offset: 0x00061584
		internal static bool IsExpectedDuringReflection(Exception e)
		{
			return e is ArgumentNullException || e is ArgumentException || e is NotSupportedException || e is TargetInvocationException || e is MethodAccessException || e is MemberAccessException || e is InvalidComObjectException || e is MissingMethodException || e is COMException || e is TypeLoadException;
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00055C5C File Offset: 0x00053E5C
		internal static bool IsExpectedDuringTypeLoad(Exception e)
		{
			return e is ArgumentNullException || e is TargetInvocationException || e is ArgumentException || e is TypeLoadException || e is FileNotFoundException || e is FileLoadException || e is BadImageFormatException;
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000633E4 File Offset: 0x000615E4
		internal static string GetCacheName(string endpointName)
		{
			Uri uri = new Uri(endpointName);
			string host = uri.Host;
			int num = host.IndexOf('.');
			if (num == 0)
			{
				throw new ArgumentException("endpointName has empty prefix");
			}
			if (num < 0)
			{
				return host;
			}
			return host.Substring(0, num);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00063424 File Offset: 0x00061624
		internal static DataCacheException CreateClientException(ErrStatus errorStatus, string logSource)
		{
			int num;
			int num2;
			switch (errorStatus)
			{
			case ErrStatus.ACCESS_DENIED:
				num = 29;
				num2 = -1;
				break;
			case ErrStatus.QUOTA_EXCEEDED:
				num = 17;
				num2 = 9;
				break;
			default:
				num = 4;
				num2 = -1;
				break;
			}
			return Utility.CreateClientException(logSource, num, num2, null);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00063464 File Offset: 0x00061664
		internal static DataCacheException CreateClientException(string logSource, int errCode, int substatus, Exception inner)
		{
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				ClientPerfCounterUpdate.OnExceptionThrown(errCode, substatus);
			}
			string text;
			if (inner is VelocityPacketTooBigException)
			{
				text = inner.Message;
			}
			else
			{
				text = Utility.GetErrorMessage(errCode, substatus);
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("ErrorCode:").Append(DataCacheException.GetErrorString(errCode)).Append("Substatus:")
					.Append(DataCacheException.GetErrorSubstatusString(substatus))
					.Append(":ComponentName:")
					.Append(logSource)
					.Append(":Message:")
					.Append(text)
					.Append(":")
					.Append(DateTime.UtcNow.ToShortDateString())
					.Append("\n");
				EventLogWriter.WriteInfo(logSource, "{0}", new object[] { stringBuilder });
			}
			return new DataCacheException(logSource, errCode, substatus, text, inner, false);
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x00063540 File Offset: 0x00061740
		internal static string GetErrorMessage(int errCode, int substatus)
		{
			StringBuilder stringBuilder = new StringBuilder(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, errCode));
			if (substatus != -1)
			{
				stringBuilder.Append(" (");
				stringBuilder.Append(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, DataCacheException.GetErrorSubstatusString(substatus)));
				stringBuilder.Append(')');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00063594 File Offset: 0x00061794
		internal static int ConvertToDataCacheErrorCode(ErrStatus status, out int substatus)
		{
			substatus = -1;
			CacheErrorCode cacheErrorCode;
			CacheSubErrorCode cacheSubErrorCode;
			return Utility.ConvertToDataCacheErrorCode(status, out substatus, out cacheErrorCode, out cacheSubErrorCode);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000635B0 File Offset: 0x000617B0
		internal static int ConvertToDataCacheErrorCode(ErrStatus status, out int substatus, out CacheErrorCode cacheErrorCode, out CacheSubErrorCode cacheSubErrorCode)
		{
			substatus = -1;
			cacheSubErrorCode = CacheSubErrorCode.None;
			cacheErrorCode = CacheErrorCode.InvalidArgument;
			int num;
			switch (status)
			{
			case ErrStatus.INTERNAL_ERROR:
				substatus = 10;
				return 17;
			case ErrStatus.INVALID_REGION:
			case ErrStatus.INVALID_CACHE:
				num = 3;
				cacheErrorCode = CacheErrorCode.InvalidArgument;
				return num;
			case ErrStatus.NO_WRITE_QUORUM:
				substatus = 2;
				cacheSubErrorCode = CacheSubErrorCode.NoWriteQuorum;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.REGION_ALREADY_EXISTS:
				num = 7;
				cacheErrorCode = CacheErrorCode.RegionAlreadyExists;
				return num;
			case ErrStatus.TIMEOUT:
				num = 18;
				cacheErrorCode = CacheErrorCode.Timeout;
				return num;
			case ErrStatus.REGION_DOES_NOT_EXIST:
				num = 5;
				cacheErrorCode = CacheErrorCode.RegionDoesNotExist;
				return num;
			case ErrStatus.VERSION_MISMATCH:
				num = 1;
				cacheErrorCode = CacheErrorCode.CacheItemVersionMismatch;
				return num;
			case ErrStatus.KEY_ALREADY_EXISTS:
				num = 8;
				cacheErrorCode = CacheErrorCode.KeyAlreadyExists;
				return num;
			case ErrStatus.KEY_DOES_NOT_EXIST:
				num = 6;
				cacheErrorCode = CacheErrorCode.KeyDoesNotExist;
				return num;
			case ErrStatus.NAMED_CACHE_DOES_NOT_EXIST:
				num = 9;
				cacheErrorCode = CacheErrorCode.NamedCacheDoesNotExist;
				return num;
			case ErrStatus.MAX_NAMED_CACHE_COUNT_EXCEEDED:
				num = 10;
				cacheErrorCode = CacheErrorCode.MaxNamedCacheCountExceeded;
				return num;
			case ErrStatus.OBJECT_LOCKED:
				num = 11;
				cacheErrorCode = CacheErrorCode.ObjectLocked;
				return num;
			case ErrStatus.OBJECT_NOT_LOCKED:
				num = 12;
				cacheErrorCode = CacheErrorCode.ObjectNotLocked;
				return num;
			case ErrStatus.INVALID_LOCK:
				num = 13;
				cacheErrorCode = CacheErrorCode.InvalidCacheLockHandle;
				return num;
			case ErrStatus.INVALID_ENUMERATOR:
				num = 14;
				cacheErrorCode = CacheErrorCode.InvalidEnumerator;
				return num;
			case ErrStatus.OUT_OF_MEMORY:
				substatus = 6;
				cacheSubErrorCode = CacheSubErrorCode.Throttled;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.SERVER_DEAD:
				substatus = 5;
				cacheSubErrorCode = CacheSubErrorCode.CacheServerUnavailable;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.REPLICATION_QUEUE_FULL:
				substatus = 3;
				cacheSubErrorCode = CacheSubErrorCode.ReplicationQueueFull;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.REPLICATION_FAILED:
				substatus = 11;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.REGIONID_NOT_FOUND:
				substatus = 15;
				cacheSubErrorCode = CacheSubErrorCode.CacheUnderReconfiguration;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.KEY_LATCHED:
				substatus = 4;
				cacheSubErrorCode = CacheSubErrorCode.KeyLatched;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.CLIENT_SERVER_VERSION_MISMATCH:
				num = 19;
				cacheErrorCode = CacheErrorCode.ClientServerVersionMismatch;
				return num;
			case ErrStatus.NOT_PRIMARY:
				substatus = 1;
				cacheSubErrorCode = CacheSubErrorCode.NotPrimary;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.CONNECTION_TERMINATED:
				num = 16;
				cacheErrorCode = CacheErrorCode.ConnectionTerminated;
				return num;
			case ErrStatus.THROTTLED:
				substatus = 6;
				cacheSubErrorCode = CacheSubErrorCode.Throttled;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.READ_THROUGH_PROVIDER_FAILURE:
				num = 26;
				cacheErrorCode = CacheErrorCode.ReadThroughProviderFailure;
				return num;
			case ErrStatus.READ_THROUGH_PROVIDER_DID_NOT_RETURN_RESULT:
				num = 27;
				cacheErrorCode = CacheErrorCode.ReadThroughProviderDidNotReturnResult;
				return num;
			case ErrStatus.READ_THROUGH_PROVIDER_NOT_FOUND:
				num = 28;
				cacheErrorCode = CacheErrorCode.ReadThroughProviderNotFound;
				return num;
			case ErrStatus.READ_THROUGH_OBJECT_LOCKED:
			case ErrStatus.READ_THROUGH_DUPLICATE_KEY:
			case ErrStatus.READ_THROUGH_OBJECT_NOT_LOCKED:
			case ErrStatus.READ_THROUGH_INVALID_LOCK:
				substatus = 7;
				cacheSubErrorCode = CacheSubErrorCode.ReadThroughKeyContention;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.QUOTA_EXCEEDED:
				substatus = 9;
				cacheSubErrorCode = CacheSubErrorCode.QuotaExceeded;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.CACHE_DISABLED:
				num = 31;
				cacheErrorCode = CacheErrorCode.CacheDisabled;
				return num;
			case ErrStatus.SERVICE_MEMORY_SHORTAGE:
				num = 17;
				substatus = 8;
				cacheSubErrorCode = CacheSubErrorCode.ServiceMemoryShortage;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.READ_THROUGH_REGION_DOES_NOT_EXIST:
				num = 34;
				cacheErrorCode = CacheErrorCode.ReadThroughRegionDoesNotExist;
				return num;
			case ErrStatus.CACHE_REDIRECTED:
				num = 35;
				cacheErrorCode = CacheErrorCode.CacheRedirected;
				return num;
			case ErrStatus.CONVERT_SIMPLECLIENT:
				num = 36;
				cacheErrorCode = CacheErrorCode.ConvertSimpleClient;
				return num;
			case ErrStatus.NOT_SUPPORTED_VALUE_FORMAT:
				num = 20;
				cacheErrorCode = CacheErrorCode.InvalidValue;
				return num;
			case ErrStatus.OVERFLOW:
				num = 20001;
				cacheErrorCode = CacheErrorCode.OverflowException;
				return num;
			case ErrStatus.MESSAGE_LARGER_THAN_CONFIGURED:
				num = 39;
				cacheErrorCode = CacheErrorCode.MessageLargerThanConfiguredSize;
				return num;
			case ErrStatus.UNSUPPORTED_OPERATION:
				num = 40;
				cacheErrorCode = CacheErrorCode.UnsupportedOperationAttemptedOnPort;
				return num;
			case ErrStatus.AUTH_HEADER_INVALID:
				num = 29;
				cacheErrorCode = CacheErrorCode.InvalidArgument;
				return num;
			case ErrStatus.AUTH_HEADER_EXPIRED:
				num = 30;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			case ErrStatus.CHANNEL_AUTH_FAILED:
				num = 41;
				cacheErrorCode = CacheErrorCode.ChannelAuthenticationFailed;
				return num;
			case ErrStatus.CHANNEL_AUTH_CRL_OFFLINE:
				substatus = 16;
				cacheSubErrorCode = CacheSubErrorCode.CertificateRevocationServerOffline;
				num = 17;
				cacheErrorCode = CacheErrorCode.RetryLater;
				return num;
			}
			num = 4;
			cacheErrorCode = CacheErrorCode.UndefinedError;
			return num;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00063910 File Offset: 0x00061B10
		internal static void LogFormatFailure(string logSource, RequestBody reqBody, MessageFormatStatus status)
		{
			VelocityDiagnostics.Publish(DiagEventName.Error, false, reqBody.RequestStates, TraceLevel.Info, logSource, "{0}:'{1}:{2}':Request Dropped, {3}", new object[] { reqBody.RequestTrackingId, reqBody.ClientReqId, reqBody.ServiceReqId, status });
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00063970 File Offset: 0x00061B70
		internal static void AddInformationToException(DataCacheException ex, ErrStatus errorStatus, object serverErrorInfo)
		{
			if (serverErrorInfo == null)
			{
				return;
			}
			object obj = null;
			try
			{
				obj = SerializationUtility.Deserialize((byte[][])serverErrorInfo, false);
			}
			catch (SerializationException ex2)
			{
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError("DistributedCache.Utility", "serialization exception was caught: {0}", new object[] { ex2 });
				}
				return;
			}
			if (obj != null)
			{
				if (errorStatus != ErrStatus.QUOTA_EXCEEDED)
				{
					return;
				}
				string text = obj as string;
				if (text == null)
				{
					return;
				}
				ex.Data.Add(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "QuotaThrottlingPrefix", text), null);
				return;
			}
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x00063A00 File Offset: 0x00061C00
		internal static void AddDestinationToException(DataCacheException ex, ErrStatus errorStatus, string destination)
		{
			if (errorStatus == ErrStatus.SERVER_DEAD || errorStatus == ErrStatus.TIMEOUT || errorStatus == ErrStatus.CONNECTION_TERMINATED || errorStatus == ErrStatus.NOT_PRIMARY)
			{
				if (string.IsNullOrEmpty(destination))
				{
					return;
				}
				ex.Data.Add(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "RequestDestination", destination), null);
			}
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x00063A3A File Offset: 0x00061C3A
		internal static void AddRequestItemInfo(RequestBody request, ResponseBody response)
		{
			if (request.Key != null)
			{
				response.Key = request.Key.StringValue;
			}
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00063A55 File Offset: 0x00061C55
		public static ClientLocationType? GetClientLocationForIP(string ipAddress, int port)
		{
			if (CloudUtility.IsVASDeployment)
			{
				return CloudUtility.GetClientLocationForIP(ipAddress, port);
			}
			return new ClientLocationType?(ConfigManager.DefaultClientLocationType);
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00063A70 File Offset: 0x00061C70
		public static CacheLookupTableRequest GetVasCacheLookupTableRequest(CacheLookupTable cacheLookupTable)
		{
			if (cacheLookupTable != null)
			{
				return new CacheLookupTableRequest(cacheLookupTable.Ranges, cacheLookupTable.CacheName, cacheLookupTable.GenNumber);
			}
			return null;
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x00063A90 File Offset: 0x00061C90
		public static IEnumerable<T> GetChannelsToCleanup<T>(Hashtable channels, int channelsToRetain)
		{
			IList<KeyValuePair<object, long>> list = Utility.CreateList<KeyValuePair<object, long>>(channels, (DictionaryEntry pair) => new KeyValuePair<object, long>(pair.Key, (long)pair.Value));
			SortUtils.SelectSorted<KeyValuePair<object, long>, long>(list, channelsToRetain, (KeyValuePair<object, long> pair) => ~pair.Value);
			return from pair in list.Skip(channelsToRetain)
				select (T)((object)pair.Key);
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x00063ADC File Offset: 0x00061CDC
		public static IList<T> CreateList<T>(Hashtable hashTable, Converter<DictionaryEntry, T> converter)
		{
			IList<T> list;
			if (hashTable.Count * IntPtr.Size > 65536)
			{
				list = new List<T>(hashTable.Count);
			}
			else
			{
				list = new ListOfList<T>(hashTable.Count);
			}
			foreach (object obj in hashTable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				list.Add(converter(dictionaryEntry));
			}
			return list;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00063B64 File Offset: 0x00061D64
		internal static byte[][] GetChunkedArray(byte[] buffer, int offset)
		{
			byte[][] array;
			using (ChunkStream chunkStream = new ChunkStream(buffer.Length))
			{
				chunkStream.Write(buffer, offset, buffer.Length - offset);
				array = chunkStream.ToChunkedArray();
			}
			return array;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x00063BAC File Offset: 0x00061DAC
		internal static byte[] ConvertToByteArray(byte[][] data)
		{
			int num = 0;
			for (int i = 0; i < data.Length; i++)
			{
				num += data[i].Length;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			for (int j = 0; j < data.Length; j++)
			{
				Buffer.BlockCopy(data[j], 0, array, num2, data[j].Length);
				num2 += data[j].Length;
			}
			return array;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x00063C08 File Offset: 0x00061E08
		internal static string PrintBytes(byte[] array)
		{
			return Utility.PrintBytes(array, 0, array.Length);
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00063C14 File Offset: 0x00061E14
		internal static string PrintBytes(byte[] array, int offset)
		{
			return Utility.PrintBytes(array, offset, array.Length - offset);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00063C22 File Offset: 0x00061E22
		internal static string PrintBytes(ArraySegment<byte> segment)
		{
			return Utility.PrintBytes(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00063C40 File Offset: 0x00061E40
		internal static string PrintBytes(byte[] array, int offset, int count)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				if (offset + i >= array.Length)
				{
					stringBuilder.AppendFormat(" <Truncated|expected {0}", count);
					break;
				}
				if (i % 8 == 0)
				{
					stringBuilder.AppendFormat("{0,3}:", i);
				}
				stringBuilder.AppendFormat(" {0:X2}", array[offset + i]);
				if (i % 8 == 7)
				{
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00063CBB File Offset: 0x00061EBB
		private static void BackupHostsFile()
		{
			File.Copy(Utility.hostsFile, Utility.backupFile, true);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00063CD0 File Offset: 0x00061ED0
		private static void AddHostsFileEntry(string hostName)
		{
			using (StreamWriter streamWriter = File.AppendText(Utility.hostsFile))
			{
				streamWriter.WriteLine("127.0.0.1    " + hostName);
				streamWriter.Flush();
				streamWriter.Close();
			}
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00063D24 File Offset: 0x00061F24
		private static void AddHosts(ClientConfigReader reader)
		{
			foreach (object obj in reader["default"].Hosts)
			{
				Utility.AddHostsFileEntry(((ClientSideHostConfiguration)obj).Name);
			}
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00063D8C File Offset: 0x00061F8C
		internal static void AddToHostsFile()
		{
			Utility.BackupHostsFile();
			Utility.AddHosts(new ClientConfigReader());
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00063D9D File Offset: 0x00061F9D
		internal static void RestoreHostsFile()
		{
			File.Copy(Utility.backupFile, Utility.hostsFile, true);
			File.Delete(Utility.backupFile);
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00063DB9 File Offset: 0x00061FB9
		public static Version GetProductVersion()
		{
			return Utility.ProductVersionHelper.Version;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x00063DC0 File Offset: 0x00061FC0
		internal static string GetCertSubjectIdentity(string targetHostName)
		{
			int num = targetHostName.IndexOf('.');
			if (num <= 0)
			{
				ReleaseAssert.Fail("Ssl require FQDN. parameter passed targetHost = {0}", new object[] { targetHostName });
			}
			string text = targetHostName.Substring(num);
			return "*" + text;
		}

		// Token: 0x0400130B RID: 4875
		private const string _logSource = "DistributedCache.Utility";

		// Token: 0x0400130C RID: 4876
		internal const string LogMessageFormat = "{0}:  Time={1}";

		// Token: 0x0400130D RID: 4877
		private const uint Lower16Bit = 65535U;

		// Token: 0x0400130E RID: 4878
		private const uint Higher16Bit = 4294901760U;

		// Token: 0x0400130F RID: 4879
		private const uint Lower24Bit = 16777215U;

		// Token: 0x04001310 RID: 4880
		private const uint Higher8Bit = 4278190080U;

		// Token: 0x04001311 RID: 4881
		private const uint BitPattern1_1 = 252645135U;

		// Token: 0x04001312 RID: 4882
		private const uint BitPattern1_2 = 4042322160U;

		// Token: 0x04001313 RID: 4883
		private const uint BitPattern2_1 = 1431655765U;

		// Token: 0x04001314 RID: 4884
		private const uint BitPattern2_2 = 2863311530U;

		// Token: 0x04001315 RID: 4885
		private const int _lohDataItemSize = 65536;

		// Token: 0x04001316 RID: 4886
		private static string hostsFile = Path.Combine(Environment.SystemDirectory, "drivers\\etc\\hosts");

		// Token: 0x04001317 RID: 4887
		private static string backupFile = Path.Combine(Environment.SystemDirectory, "drivers\\etc\\hosts.backup");

		// Token: 0x04001318 RID: 4888
		public static double StopwatchTicksPerMillisecond = (double)Stopwatch.Frequency / 1000.0;

		// Token: 0x04001319 RID: 4889
		private static double StopwatchTicksPerSystemTick = (double)Stopwatch.Frequency / 10000000.0;

		// Token: 0x0400131A RID: 4890
		private static string _validNamePattern = "^[\\w\\p{IsHighSurrogates}\\p{IsLowSurrogates}\\d-_]+$";

		// Token: 0x0400131B RID: 4891
		private static string _logFormatWithChannelID = "Recoverable exception raised - ChannelID = {0} \n {1}";

		// Token: 0x0400131C RID: 4892
		private static string _logFormat = "Recoverable exception raised - {0}";

		// Token: 0x0400131D RID: 4893
		private static string _logFormatWithChannelInfo = "Recoverable exception raised - {0} \n CacheName : {1} \n ErrStatus : {2}";

		// Token: 0x02000397 RID: 919
		private static class ProductVersionHelper
		{
			// Token: 0x060020AD RID: 8365 RVA: 0x00063EC0 File Offset: 0x000620C0
			private static Version GetVersion()
			{
				string location = Assembly.GetExecutingAssembly().Location;
				if (string.IsNullOrEmpty(location))
				{
					return null;
				}
				Version version;
				try
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(location);
					version = new Version(versionInfo.ProductMajorPart, versionInfo.ProductMinorPart, versionInfo.ProductBuildPart, versionInfo.ProductPrivatePart);
				}
				catch (FileNotFoundException)
				{
					version = null;
				}
				return version;
			}

			// Token: 0x0400131E RID: 4894
			internal static Version Version = Utility.ProductVersionHelper.GetVersion();
		}
	}
}
