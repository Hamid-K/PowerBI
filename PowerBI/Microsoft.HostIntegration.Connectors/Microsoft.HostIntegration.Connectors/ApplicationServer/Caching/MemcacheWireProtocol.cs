using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000163 RID: 355
	internal class MemcacheWireProtocol : IServerSocketProtocol
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x000239AC File Offset: 0x00021BAC
		internal MemcacheWireProtocol(string cacheName, int port)
		{
			this.cacheName = cacheName;
			this.listeningPort = port;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x000239C2 File Offset: 0x00021BC2
		internal int ListeningPort
		{
			get
			{
				return this.listeningPort;
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000239CC File Offset: 0x00021BCC
		public IVelocityResponsePacket CreateResponsePacket(VelocityPacketType type, int packetId)
		{
			return new MemcachePacket(this.cacheName)
			{
				MessageType = type,
				MessageID = packetId
			};
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000239F4 File Offset: 0x00021BF4
		public IReplyContext CreateReplyContext(ITcpChannel tcpChannel, IList<IVelocityPacket> packets, VelocityPacketException exception)
		{
			return new MemcacheReplyContext(this, tcpChannel, packets, exception);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000239FF File Offset: 0x00021BFF
		public IVelocityRequestPacket CreateEmptyRequestPacket()
		{
			return new MemcachePacket(this.cacheName);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00023A0C File Offset: 0x00021C0C
		public IEnumerable<ArraySegment<byte>> GetReadRequestBuffer(IList<IVelocityPacket> packets, IBufferManager bufferManager)
		{
			return MemcacheWireProtocol.GetReadPacketBuffer(this.cacheName, packets, bufferManager);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00023A1C File Offset: 0x00021C1C
		public IList<ArraySegment<byte>> GetWriteResponseBuffer(IVelocityResponsePacket packet, IBufferManager bufferManager)
		{
			List<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
			MemcachePacket memcachePacket = packet as MemcachePacket;
			memcachePacket.Initialize();
			byte[] array = bufferManager.TakeBuffer(memcachePacket.HeaderSize);
			int num = 0;
			try
			{
				num = memcachePacket.WriteHeader(array);
			}
			finally
			{
				if (num == 0)
				{
					bufferManager.ReleaseBuffer(array);
				}
				else
				{
					list.Add(new ArraySegment<byte>(array, 0, num));
				}
			}
			if (memcachePacket.BodySize > 0)
			{
				array = bufferManager.TakeBuffer(memcachePacket.BodySize);
				memcachePacket.WriteBody(array);
				list.Add(new ArraySegment<byte>(array, 0, memcachePacket.BodySize));
			}
			return list;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00023AB4 File Offset: 0x00021CB4
		public IList<ArraySegment<byte>> GetWriteResponseBuffer(ICollection<IVelocityResponsePacket> packets, IBufferManager bufferManager)
		{
			List<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
			foreach (IVelocityResponsePacket velocityResponsePacket in packets)
			{
				list.AddRange(this.GetWriteResponseBuffer(velocityResponsePacket, bufferManager));
			}
			return list;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00023B0C File Offset: 0x00021D0C
		private static IEnumerable<ArraySegment<byte>> GetReadPacketBuffer(string cacheName, IList<IVelocityPacket> packets, IBufferManager bufferManager)
		{
			MemcachePacket packet = new MemcachePacket(cacheName);
			packets.Add(packet);
			byte[] magicByte = new byte[1];
			yield return new ArraySegment<byte>(magicByte, 0, 1);
			int count = packet.GetByteReadCount(magicByte[0]);
			byte[] buffer = bufferManager.TakeBuffer(packet.HeaderSize);
			buffer[0] = magicByte[0];
			try
			{
				int offset = 1;
				for (;;)
				{
					int requiredBufferSize = offset + count;
					if (requiredBufferSize > bufferManager.MaxMessageSize)
					{
						break;
					}
					if (requiredBufferSize > buffer.Length)
					{
						int num = buffer.Length * 2;
						byte[] array = bufferManager.TakeBuffer(num);
						Array.Copy(buffer, array, offset);
						bufferManager.ReleaseBuffer(buffer);
						buffer = array;
					}
					ArraySegment<byte> arraySegment = new ArraySegment<byte>(buffer, offset, count);
					yield return arraySegment;
					offset = requiredBufferSize;
					if (packet.ReadHeader(buffer, offset))
					{
						goto Block_8;
					}
				}
				throw new VelocityPacketFormatFatalException("Command too long. Max = {0}.", new object[] { bufferManager.MaxMessageSize });
				Block_8:
				MemcacheProtocolHelper.LogReceive(buffer, 0, offset);
			}
			finally
			{
				bufferManager.ReleaseBuffer(buffer);
			}
			if (packet.BodySize > 0)
			{
				if (packet.BodySize > bufferManager.MaxMessageSize)
				{
					throw new VelocityPacketFormatFatalException("Value too large. Max = {0}, Sent = {1}.", new object[] { bufferManager.MaxMessageSize, packet.BodySize });
				}
				buffer = bufferManager.TakeBuffer(packet.BodySize);
				try
				{
					yield return new ArraySegment<byte>(buffer, 0, packet.BodySize);
					packet.ReadBody(buffer);
				}
				finally
				{
					bufferManager.ReleaseBuffer(buffer);
				}
			}
			yield break;
		}

		// Token: 0x04000761 RID: 1889
		internal const string Pid = "pid";

		// Token: 0x04000762 RID: 1890
		internal const string Uptime = "uptime";

		// Token: 0x04000763 RID: 1891
		internal const string Time = "time";

		// Token: 0x04000764 RID: 1892
		internal const string Version = "version";

		// Token: 0x04000765 RID: 1893
		internal const string PointerSize = "pointer_size";

		// Token: 0x04000766 RID: 1894
		internal const string RUsageUser = "rusage_user";

		// Token: 0x04000767 RID: 1895
		internal const string RUsageSystem = "rusage_system";

		// Token: 0x04000768 RID: 1896
		internal const string CurrentItems = "curr_items";

		// Token: 0x04000769 RID: 1897
		internal const string CurrentConnections = "curr_connections";

		// Token: 0x0400076A RID: 1898
		internal const string TotalConnections = "total_connections";

		// Token: 0x0400076B RID: 1899
		internal const string TotalBytes = "bytes";

		// Token: 0x0400076C RID: 1900
		internal const string TotalGet = "cmd_get";

		// Token: 0x0400076D RID: 1901
		internal const string TotalSet = "cmd_set";

		// Token: 0x0400076E RID: 1902
		internal const string GetHits = "get_hits";

		// Token: 0x0400076F RID: 1903
		internal const string GetMisses = "get_misses";

		// Token: 0x04000770 RID: 1904
		internal const string BytesRead = "bytes_read";

		// Token: 0x04000771 RID: 1905
		internal const string BytesWritten = "bytes_written";

		// Token: 0x04000772 RID: 1906
		internal const string LimitMaxBytes = "limit_maxbytes";

		// Token: 0x04000773 RID: 1907
		internal const string MaxBytes = "maxbytes";

		// Token: 0x04000774 RID: 1908
		internal const string MaxConnections = "maxconns";

		// Token: 0x04000775 RID: 1909
		internal const string TcpPort = "tcpport";

		// Token: 0x04000776 RID: 1910
		internal const string ListenInterface = "inter";

		// Token: 0x04000777 RID: 1911
		internal const string Verbosity = "verbosity";

		// Token: 0x04000778 RID: 1912
		internal const string Evictions = "evictions";

		// Token: 0x04000779 RID: 1913
		internal const string DomainSocket = "domain_socket";

		// Token: 0x0400077A RID: 1914
		internal const string ChunkSize = "chunk_size";

		// Token: 0x0400077B RID: 1915
		internal const string StatKeyPrefix = "stat_key_prefix";

		// Token: 0x0400077C RID: 1916
		internal const string DetailEnabled = "detail_enabled";

		// Token: 0x0400077D RID: 1917
		internal const string CasEnabled = "cas_enabled";

		// Token: 0x0400077E RID: 1918
		internal const string AuthEnabledSasl = "auth_enabled_sasl";

		// Token: 0x0400077F RID: 1919
		internal const string ItemSizeMax = "item_size_max";

		// Token: 0x04000780 RID: 1920
		internal const string MaxConnsFast = "maxconns_fast";

		// Token: 0x04000781 RID: 1921
		internal const string Null = "NULL";

		// Token: 0x04000782 RID: 1922
		internal const string On = "on";

		// Token: 0x04000783 RID: 1923
		internal const string Off = "off";

		// Token: 0x04000784 RID: 1924
		internal const string Yes = "yes";

		// Token: 0x04000785 RID: 1925
		internal const string No = "no";

		// Token: 0x04000786 RID: 1926
		internal const string VersionValue = "1.3";

		// Token: 0x04000787 RID: 1927
		internal const string Settings = "settings";

		// Token: 0x04000788 RID: 1928
		internal const string StatPrefixSeperator = ":";

		// Token: 0x04000789 RID: 1929
		internal const string LogSource = "DistributedCache.MemcacheWireProtocol";

		// Token: 0x0400078A RID: 1930
		private readonly string cacheName;

		// Token: 0x0400078B RID: 1931
		private readonly int listeningPort;
	}
}
