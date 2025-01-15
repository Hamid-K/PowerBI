using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001BA RID: 442
	internal static class ClientPerfCounterUpdate
	{
		// Token: 0x06000E6E RID: 3694 RVA: 0x00030C20 File Offset: 0x0002EE20
		internal static void OnRequestSent(RequestBody reqBody, bool isMultiRequest)
		{
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalOutstandingRequests);
			if (reqBody != null)
			{
				if (reqBody.IsReadRequest())
				{
					ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalReads);
				}
				else if (reqBody.IsWriteRequest())
				{
					ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalWrites);
				}
			}
			if (!isMultiRequest)
			{
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalRequestsPerSecond);
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalRequests);
			}
			if (reqBody != null)
			{
				VelocityPacket velocityPacket = reqBody.Packet as VelocityPacket;
				if (velocityPacket != null && velocityPacket.MessageType == VelocityPacketType.Put)
				{
					long num = (long)((ulong)velocityPacket.PayloadLength);
					ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.OutgoingDataRate, num);
				}
			}
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00030C94 File Offset: 0x0002EE94
		internal static void OnResponseReceivedFromServer(SendReceiveSynchronizer s, ResponseBody respBody)
		{
			ClientPerfCounterUpdate.DecrementCount(ClientPerfCounterUpdate.StatName.TotalOutstandingRequests);
			if (s == null || s.IsRequestTimedOut)
			{
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.ServerResponsesDroppedPerSecond);
			}
			if (respBody != null)
			{
				VelocityPacket velocityPacket = respBody.Packet as VelocityPacket;
				if (velocityPacket != null && velocityPacket.MessageType == VelocityPacketType.Get)
				{
					if (s != null)
					{
						double num = TimeSpan.FromTicks(Utility.StopwatchTicksToSystemTicks(Stopwatch.GetTimestamp() - s.RequestSentTime)).TotalMilliseconds * 1000.0;
						ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.AverageCacheGetNetworkLatency, (long)num);
						ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.AverageCacheGetNetworkLatencyBase);
					}
					long num2 = (long)((ulong)velocityPacket.PayloadLength);
					ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.IncomingDataRate, num2);
				}
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00030D1E File Offset: 0x0002EF1E
		internal static void OnErrorResponseGenerated()
		{
			ClientPerfCounterUpdate.DecrementCount(ClientPerfCounterUpdate.StatName.TotalOutstandingRequests);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00030D27 File Offset: 0x0002EF27
		internal static void OnLocalCacheItemCountChanged(long itemCount)
		{
			ClientPerfCounterUpdate.SetValue(ClientPerfCounterUpdate.StatName.TotalItemsInLocalCache, itemCount);
			ClientPerfCounterUpdate.SetValue(ClientPerfCounterUpdate.StatName.PercentageLocalCacheFull, itemCount);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00030D39 File Offset: 0x0002EF39
		internal static void OnChannelCreated()
		{
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalConnections);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00030D42 File Offset: 0x0002EF42
		internal static void OnConnectionRequestFailed()
		{
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalConnectionRequestsFailed);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00030D4B File Offset: 0x0002EF4B
		internal static void OnConnectionsClosed()
		{
			ClientPerfCounterUpdate.DecrementCount(ClientPerfCounterUpdate.StatName.TotalConnections);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00030D54 File Offset: 0x0002EF54
		internal static void OnNotificationsReceived(int notifications)
		{
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalNotificationsReceived, (long)notifications);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00030D60 File Offset: 0x0002EF60
		internal static void OnRequestCompleted(long latencyInMicroseconds, ReqType op)
		{
			switch (op)
			{
			case ReqType.PUT:
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.AverageCachePutLatency, latencyInMicroseconds);
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.AverageCachePutLatencyBase);
				return;
			case ReqType.GET:
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.AverageCacheGetLatency, latencyInMicroseconds);
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.AverageCacheGetLatencyBase);
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.PercentageLocalCacheHitsBase);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00030DA2 File Offset: 0x0002EFA2
		internal static void OnLocalCacheInstanceCreated(long localCacheCapacity)
		{
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.PercentageLocalCacheFullBase, localCacheCapacity);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00030DAC File Offset: 0x0002EFAC
		internal static void OnLocalCacheInstanceDisposed(long itemsInLocalCache, long localCacheCapacity)
		{
			ClientPerfCounterUpdate.DecrementCount(ClientPerfCounterUpdate.StatName.TotalItemsInLocalCache, itemsInLocalCache);
			ClientPerfCounterUpdate.DecrementCount(ClientPerfCounterUpdate.StatName.PercentageLocalCacheFullBase, localCacheCapacity);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00030DBE File Offset: 0x0002EFBE
		internal static void OnLocalCacheUsedForGet()
		{
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalRequests);
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalRequestsPerSecond);
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalReads);
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalLocalCacheHits);
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.PercentageLocalCacheHits);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00030DE3 File Offset: 0x0002EFE3
		internal static void OnLocalCacheUsedForBulkGet(int localItemsCount, int totalItemCount)
		{
			if (localItemsCount > 0)
			{
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalRequests, (long)localItemsCount);
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalRequestsPerSecond, (long)localItemsCount);
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalReads, (long)localItemsCount);
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalLocalCacheHits, (long)localItemsCount);
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.PercentageLocalCacheHits, (long)localItemsCount);
			}
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.PercentageLocalCacheHitsBase, (long)totalItemCount);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00030E20 File Offset: 0x0002F020
		internal static void OnExceptionThrown(int errorCode, int substatus)
		{
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalFailureExceptions);
			ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.FailureExceptionRate);
			switch (errorCode)
			{
			case 16:
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalNetworkExceptions);
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.NetworkExceptionRate);
				break;
			case 17:
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalRetryExceptions);
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.RetryExceptionRate);
				if (substatus == 5)
				{
					ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalNetworkExceptions);
					ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.NetworkExceptionRate);
					return;
				}
				break;
			case 18:
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TotalTimeoutExceptions);
				ClientPerfCounterUpdate.IncrementCount(ClientPerfCounterUpdate.StatName.TimeoutExceptionRate);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00030E8E File Offset: 0x0002F08E
		internal static void IncrementCount(ClientPerfCounterUpdate.StatName statName)
		{
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				Interlocked.Increment(ref ClientPerfCounterUpdate.Stats[(int)statName]);
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00030EA8 File Offset: 0x0002F0A8
		internal static void IncrementCount(ClientPerfCounterUpdate.StatName statName, long value)
		{
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				Interlocked.Add(ref ClientPerfCounterUpdate.Stats[(int)statName], value);
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00030EC4 File Offset: 0x0002F0C4
		internal static void DecrementCount(ClientPerfCounterUpdate.StatName statName)
		{
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				Interlocked.Decrement(ref ClientPerfCounterUpdate.Stats[(int)statName]);
				if (Interlocked.Read(ref ClientPerfCounterUpdate.Stats[(int)statName]) < 0L)
				{
					Interlocked.Exchange(ref ClientPerfCounterUpdate.Stats[(int)statName], 0L);
				}
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00030F10 File Offset: 0x0002F110
		internal static void DecrementCount(ClientPerfCounterUpdate.StatName statName, long value)
		{
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				Interlocked.Add(ref ClientPerfCounterUpdate.Stats[(int)statName], -value);
				if (Interlocked.Read(ref ClientPerfCounterUpdate.Stats[(int)statName]) < 0L)
				{
					Interlocked.Exchange(ref ClientPerfCounterUpdate.Stats[(int)statName], 0L);
				}
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00030F5E File Offset: 0x0002F15E
		internal static void SetValue(ClientPerfCounterUpdate.StatName statName, long value)
		{
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				Interlocked.Exchange(ref ClientPerfCounterUpdate.Stats[(int)statName], value);
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00030F79 File Offset: 0x0002F179
		internal static bool IsReadRequest(RequestBody reqBody)
		{
			return reqBody.Packet.MessageType == VelocityPacketType.Get || reqBody.Packet.MessageType == VelocityPacketType.GetAndLock || reqBody.Packet.MessageType == VelocityPacketType.GetCacheItem || reqBody.Packet.MessageType == VelocityPacketType.GetIfNewer;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00030FB8 File Offset: 0x0002F1B8
		internal static bool IsWriteRequest(RequestBody reqBody)
		{
			return reqBody.Packet.MessageType == VelocityPacketType.Put || reqBody.Packet.MessageType == VelocityPacketType.PutAndUnlock || reqBody.Packet.MessageType == VelocityPacketType.Append || reqBody.Packet.MessageType == VelocityPacketType.Prepend || reqBody.Packet.MessageType == VelocityPacketType.Add || reqBody.Packet.MessageType == VelocityPacketType.Replace || reqBody.Packet.MessageType == VelocityPacketType.Increment;
		}

		// Token: 0x040009F6 RID: 2550
		internal static bool IsPerfCounterEnabled;

		// Token: 0x040009F7 RID: 2551
		internal static long[] Stats = new long[Enum.GetValues(typeof(ClientPerfCounterUpdate.StatName)).Length];

		// Token: 0x040009F8 RID: 2552
		internal static long[] LastPublishedStats = new long[Enum.GetValues(typeof(ClientPerfCounterUpdate.StatName)).Length];

		// Token: 0x020001BB RID: 443
		internal enum StatName
		{
			// Token: 0x040009FA RID: 2554
			AverageCacheGetLatency,
			// Token: 0x040009FB RID: 2555
			AverageCacheGetLatencyBase,
			// Token: 0x040009FC RID: 2556
			AverageCacheGetNetworkLatency,
			// Token: 0x040009FD RID: 2557
			AverageCacheGetNetworkLatencyBase,
			// Token: 0x040009FE RID: 2558
			AverageCachePutLatency,
			// Token: 0x040009FF RID: 2559
			AverageCachePutLatencyBase,
			// Token: 0x04000A00 RID: 2560
			FailureExceptionRate,
			// Token: 0x04000A01 RID: 2561
			IncomingDataRate,
			// Token: 0x04000A02 RID: 2562
			NetworkExceptionRate,
			// Token: 0x04000A03 RID: 2563
			OutgoingDataRate,
			// Token: 0x04000A04 RID: 2564
			PercentageLocalCacheFull,
			// Token: 0x04000A05 RID: 2565
			PercentageLocalCacheFullBase,
			// Token: 0x04000A06 RID: 2566
			PercentageLocalCacheHits,
			// Token: 0x04000A07 RID: 2567
			PercentageLocalCacheHitsBase,
			// Token: 0x04000A08 RID: 2568
			RetryExceptionRate,
			// Token: 0x04000A09 RID: 2569
			ServerResponsesDroppedPerSecond,
			// Token: 0x04000A0A RID: 2570
			TimeoutExceptionRate,
			// Token: 0x04000A0B RID: 2571
			TotalConnectionRequestsFailed,
			// Token: 0x04000A0C RID: 2572
			TotalConnections,
			// Token: 0x04000A0D RID: 2573
			TotalFailureExceptions,
			// Token: 0x04000A0E RID: 2574
			TotalItemsInLocalCache,
			// Token: 0x04000A0F RID: 2575
			TotalLocalCacheHits,
			// Token: 0x04000A10 RID: 2576
			TotalNetworkExceptions,
			// Token: 0x04000A11 RID: 2577
			TotalNotificationsReceived,
			// Token: 0x04000A12 RID: 2578
			TotalOutstandingRequests,
			// Token: 0x04000A13 RID: 2579
			TotalReads,
			// Token: 0x04000A14 RID: 2580
			TotalRequests,
			// Token: 0x04000A15 RID: 2581
			TotalRequestsPerSecond,
			// Token: 0x04000A16 RID: 2582
			TotalRetryExceptions,
			// Token: 0x04000A17 RID: 2583
			TotalTimeoutExceptions,
			// Token: 0x04000A18 RID: 2584
			TotalWrites
		}
	}
}
