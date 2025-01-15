using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Fabric.Common;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000166 RID: 358
	internal class MemcachePacket : IVelocityRequestPacket, IVelocityResponsePacket, IVelocityPacket
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x00024A67 File Offset: 0x00022C67
		internal MemcachePacket(string cacheName)
		{
			this.CacheName = cacheName;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00024A81 File Offset: 0x00022C81
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x00024A89 File Offset: 0x00022C89
		public int MessageID { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00024A92 File Offset: 0x00022C92
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x00024A9F File Offset: 0x00022C9F
		public object Opaque
		{
			get
			{
				return this.memcachePacketBase.OpaqueData;
			}
			set
			{
				this.opaqueProperties = (MemcachePacket.OpaquePropeties)value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00024AAD File Offset: 0x00022CAD
		public TcpPacketSendTypes SendType
		{
			get
			{
				return this.memcachePacketBase.SendType;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00024ABA File Offset: 0x00022CBA
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x000036A9 File Offset: 0x000018A9
		public VelocityPacketType MessageType
		{
			get
			{
				return this.memcachePacketBase.PacketType;
			}
			set
			{
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00024AC7 File Offset: 0x00022CC7
		public IVelocityPacketPropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00024AD0 File Offset: 0x00022CD0
		internal int GetByteReadCount(byte magicByte)
		{
			int num;
			if (magicByte == 128)
			{
				this.memcachePacketBase = new MemcachePacket.MemcacheBinaryProtocolPacket(this);
				num = this.HeaderSize - 1;
			}
			else
			{
				this.memcachePacketBase = new MemcachePacket.MemcacheTextProtocolPacket(this);
				num = 1;
			}
			return num;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00024B0B File Offset: 0x00022D0B
		public int HeaderSize
		{
			get
			{
				return this.memcachePacketBase.HeaderLength;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00024B18 File Offset: 0x00022D18
		public int BodySize
		{
			get
			{
				return this.memcachePacketBase.BodyLength;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00024B25 File Offset: 0x00022D25
		internal void Initialize()
		{
			if (this.opaqueProperties.IsBinary)
			{
				this.memcachePacketBase = new MemcachePacket.MemcacheBinaryProtocolPacket(this);
			}
			else
			{
				this.memcachePacketBase = new MemcachePacket.MemcacheTextProtocolPacket(this);
			}
			this.memcachePacketBase.Initialize(this.opaqueProperties);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00024B5F File Offset: 0x00022D5F
		internal bool ReadHeader(byte[] buffer, int count)
		{
			return this.memcachePacketBase.ReadHeader(buffer, count);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00024B6E File Offset: 0x00022D6E
		internal void ReadBody(byte[] buffer)
		{
			this.memcachePacketBase.ReadBody(buffer);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00024B7C File Offset: 0x00022D7C
		internal int WriteHeader(byte[] buffer)
		{
			return this.memcachePacketBase.WriteHeader(buffer);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00024B8A File Offset: 0x00022D8A
		internal void WriteBody(byte[] buffer)
		{
			this.memcachePacketBase.WriteBody(buffer);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00008948 File Offset: 0x00006B48
		private static uint GetExpiry(int expiry)
		{
			return (uint)expiry;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00024B98 File Offset: 0x00022D98
		internal static TimeSpan GetPassThroughTime(uint? memcacheTime)
		{
			if (memcacheTime == null)
			{
				return TimeSpan.Zero;
			}
			if (memcacheTime.Value == 0U)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromSeconds(memcacheTime.Value);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00024BC8 File Offset: 0x00022DC8
		internal static uint? GetMemcacheTime(TimeSpan passThroughTime)
		{
			if (passThroughTime == TimeSpan.Zero)
			{
				return null;
			}
			if (passThroughTime == TimeSpan.MaxValue)
			{
				return new uint?(0U);
			}
			return new uint?((uint)passThroughTime.TotalSeconds);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00024C10 File Offset: 0x00022E10
		public static TimeSpan GetTimeSpan(uint? memcacheTime)
		{
			if (memcacheTime == null)
			{
				return TimeSpan.Zero;
			}
			if (memcacheTime == 4294967295U)
			{
				return TimeSpan.FromSeconds(4294967295.0);
			}
			double num = (double)memcacheTime.Value;
			if (num > 2592000.0)
			{
				num = (ConfigManager.UnixEpoch.AddSeconds(num) - DateTime.UtcNow).TotalSeconds;
			}
			if (num > 0.0)
			{
				return TimeSpan.FromSeconds(num);
			}
			if (num == 0.0)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromMilliseconds(1.0);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00024CBC File Offset: 0x00022EBC
		private static MemcachePacket.ResponseStatus GetResponseStatus(ErrStatus errStatus, string logSource)
		{
			switch (errStatus)
			{
			case ErrStatus.NO_WRITE_QUORUM:
				break;
			case ErrStatus.REGION_ALREADY_EXISTS:
			case ErrStatus.LSN_MISMATCH:
			case ErrStatus.TIMEOUT:
			case ErrStatus.INVALID_CACHE:
			case ErrStatus.REGION_DOES_NOT_EXIST:
			case ErrStatus.NAMED_CACHE_DOES_NOT_EXIST:
				goto IL_010C;
			case ErrStatus.VERSION_MISMATCH:
			case ErrStatus.KEY_ALREADY_EXISTS:
				return MemcachePacket.ResponseStatus.KeyExists;
			case ErrStatus.KEY_DOES_NOT_EXIST:
				return MemcachePacket.ResponseStatus.KeyNotFound;
			default:
				switch (errStatus)
				{
				case ErrStatus.OUT_OF_MEMORY:
					break;
				case ErrStatus.SERVER_DEAD:
				case ErrStatus.REPLICATION_FAILED:
				case ErrStatus.KEY_LATCHED:
				case ErrStatus.NOT_PRIMARY:
				case ErrStatus.THROTTLED:
					goto IL_00F2;
				case ErrStatus.INVALID_REQUEST_BODY:
					return MemcachePacket.ResponseStatus.InvalidArguments;
				case ErrStatus.STORE_ACCESS_FAILURE:
				case ErrStatus.DELETE_IN_PROGRESS:
				case ErrStatus.CHANNEL_FOUND_CLOSED:
				case ErrStatus.REGIONID_NOT_FOUND:
				case ErrStatus.CLIENT_SERVER_VERSION_MISMATCH:
				case ErrStatus.REGION_RANGE_CLOSED:
				case ErrStatus.CONNECTION_TERMINATED:
					goto IL_010C;
				case ErrStatus.REPLICATION_QUEUE_FULL:
				case ErrStatus.PM_NOT_READY:
					goto IL_00D8;
				default:
					switch (errStatus)
					{
					case ErrStatus.QUOTA_EXCEEDED:
					case ErrStatus.CONNECTION_QUOTA_EXCEEDED:
						goto IL_00F2;
					case ErrStatus.CACHE_DISABLED:
					case ErrStatus.READ_THROUGH_REGION_DOES_NOT_EXIST:
					case ErrStatus.CACHE_REDIRECTED:
					case ErrStatus.CONVERT_SIMPLECLIENT:
						goto IL_010C;
					case ErrStatus.SERVICE_MEMORY_SHORTAGE:
						break;
					case ErrStatus.NOT_SUPPORTED_VALUE_FORMAT:
						return MemcachePacket.ResponseStatus.IncrementDecrementOnNonNumeric;
					case ErrStatus.OVERFLOW:
						return MemcachePacket.ResponseStatus.ItemNotStored;
					case ErrStatus.REQUEST_TYPE_NOT_SUPPORTED:
						return MemcachePacket.ResponseStatus.UnknownCommand;
					default:
						goto IL_010C;
					}
					break;
				}
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<ErrStatus>(logSource, "ErrStatus: {0} - OutOfMemory", errStatus);
				}
				return MemcachePacket.ResponseStatus.OutOfMemory;
				IL_00F2:
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<ErrStatus>(logSource, "ErrStatus: {0} - TemporaryFailure", errStatus);
				}
				return MemcachePacket.ResponseStatus.TemporaryFailure;
			}
			IL_00D8:
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<ErrStatus>(logSource, "ErrStatus: {0} - Busy", errStatus);
			}
			return MemcachePacket.ResponseStatus.Busy;
			IL_010C:
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<ErrStatus>(logSource, "ErrStatus: {0} - InternalError", errStatus);
			}
			return MemcachePacket.ResponseStatus.InternalError;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00024DEE File Offset: 0x00022FEE
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x00024DF6 File Offset: 0x00022FF6
		public ErrStatus ResponseCode { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00024DFF File Offset: 0x00022FFF
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x00024E07 File Offset: 0x00023007
		public VelocityPacketHeaderFlags HeaderFlags { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00024E10 File Offset: 0x00023010
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x00024E18 File Offset: 0x00023018
		public DataCacheItemVersion Version { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00024E21 File Offset: 0x00023021
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x00024E29 File Offset: 0x00023029
		public bool IsMemcacheProtocol { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00024E32 File Offset: 0x00023032
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x00024E3A File Offset: 0x0002303A
		public uint? ExpiryTTL { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00024E43 File Offset: 0x00023043
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x00024E4B File Offset: 0x0002304B
		public DataCacheLockHandle LockHandle { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00024E54 File Offset: 0x00023054
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x00024E5C File Offset: 0x0002305C
		public PartitionId PartitionKey { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00024E65 File Offset: 0x00023065
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x00024E6D File Offset: 0x0002306D
		public string CacheName { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00024E76 File Offset: 0x00023076
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x00024E7E File Offset: 0x0002307E
		public string Region { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00024E87 File Offset: 0x00023087
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x00024E8F File Offset: 0x0002308F
		public string Key { get; set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00024E98 File Offset: 0x00023098
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x00024EA0 File Offset: 0x000230A0
		public byte[][] Value { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00024EA9 File Offset: 0x000230A9
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x00024EB1 File Offset: 0x000230B1
		public int CacheItemMask { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00024EBA File Offset: 0x000230BA
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x00024EC2 File Offset: 0x000230C2
		public IList<string> Keys { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00024ECB File Offset: 0x000230CB
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x00024ED3 File Offset: 0x000230D3
		public bool IsEmptyPacket { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00024EDC File Offset: 0x000230DC
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x00024EE4 File Offset: 0x000230E4
		public Exception Exception { get; set; }

		// Token: 0x0400079C RID: 1948
		private const uint MaxKeyLength = 250U;

		// Token: 0x0400079D RID: 1949
		private const uint SecondsIn30Days = 2592000U;

		// Token: 0x0400079E RID: 1950
		internal const byte RequestMagicByte = 128;

		// Token: 0x0400079F RID: 1951
		internal const byte ResponseMagicByte = 129;

		// Token: 0x040007A0 RID: 1952
		private const int MemcacheEpoch = 0;

		// Token: 0x040007A1 RID: 1953
		internal const ushort FlagsLength = 4;

		// Token: 0x040007A2 RID: 1954
		private static readonly DataCacheItemVersion DataCacheItemVersionNull = new DataCacheItemVersion(InternalCacheItemVersion.Null);

		// Token: 0x040007A3 RID: 1955
		private MemcachePacket.MemcachePacketBase memcachePacketBase;

		// Token: 0x040007A4 RID: 1956
		private MemcachePacket.OpaquePropeties opaqueProperties;

		// Token: 0x040007A5 RID: 1957
		private readonly VelocityPacketPropertyBag propertyBag = new VelocityPacketPropertyBag();

		// Token: 0x02000167 RID: 359
		private enum ResponseStatus : ushort
		{
			// Token: 0x040007B7 RID: 1975
			NoError,
			// Token: 0x040007B8 RID: 1976
			KeyNotFound,
			// Token: 0x040007B9 RID: 1977
			KeyExists,
			// Token: 0x040007BA RID: 1978
			ValueTooLarge,
			// Token: 0x040007BB RID: 1979
			InvalidArguments,
			// Token: 0x040007BC RID: 1980
			ItemNotStored,
			// Token: 0x040007BD RID: 1981
			IncrementDecrementOnNonNumeric,
			// Token: 0x040007BE RID: 1982
			IncorrectServerForVBucket,
			// Token: 0x040007BF RID: 1983
			AuthenticationError,
			// Token: 0x040007C0 RID: 1984
			AuthenticationContinue,
			// Token: 0x040007C1 RID: 1985
			UnknownCommand = 129,
			// Token: 0x040007C2 RID: 1986
			OutOfMemory,
			// Token: 0x040007C3 RID: 1987
			NotSupported,
			// Token: 0x040007C4 RID: 1988
			InternalError,
			// Token: 0x040007C5 RID: 1989
			Busy,
			// Token: 0x040007C6 RID: 1990
			TemporaryFailure
		}

		// Token: 0x02000168 RID: 360
		internal abstract class MemcachePacketBase
		{
			// Token: 0x06000B4E RID: 2894 RVA: 0x00024EFE File Offset: 0x000230FE
			protected MemcachePacketBase(MemcachePacket memcachePacket)
			{
				this.MemcachePacketContainer = memcachePacket;
			}

			// Token: 0x06000B4F RID: 2895
			internal abstract void Initialize(MemcachePacket.OpaquePropeties opaqueProperties);

			// Token: 0x06000B50 RID: 2896
			internal abstract bool ReadHeader(byte[] buffer, int count);

			// Token: 0x06000B51 RID: 2897
			internal abstract void ReadBody(byte[] buffer);

			// Token: 0x06000B52 RID: 2898
			internal abstract int WriteHeader(byte[] buffer);

			// Token: 0x06000B53 RID: 2899
			internal abstract void WriteBody(byte[] buffer);

			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x06000B54 RID: 2900
			internal abstract int HeaderLength { get; }

			// Token: 0x170002A1 RID: 673
			// (get) Token: 0x06000B55 RID: 2901
			internal abstract int BodyLength { get; }

			// Token: 0x170002A2 RID: 674
			// (get) Token: 0x06000B56 RID: 2902
			internal abstract object OpaqueData { get; }

			// Token: 0x170002A3 RID: 675
			// (get) Token: 0x06000B57 RID: 2903
			internal abstract TcpPacketSendTypes SendType { get; }

			// Token: 0x170002A4 RID: 676
			// (get) Token: 0x06000B58 RID: 2904
			internal abstract VelocityPacketType PacketType { get; }

			// Token: 0x040007C7 RID: 1991
			protected MemcachePacket MemcachePacketContainer;
		}

		// Token: 0x02000169 RID: 361
		internal class MemcacheBinaryProtocolPacket : MemcachePacket.MemcachePacketBase
		{
			// Token: 0x06000B59 RID: 2905 RVA: 0x00024F0D File Offset: 0x0002310D
			internal MemcacheBinaryProtocolPacket(MemcachePacket memcachePacket)
				: base(memcachePacket)
			{
			}

			// Token: 0x170002A5 RID: 677
			// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00024F16 File Offset: 0x00023116
			internal override int HeaderLength
			{
				get
				{
					return 24;
				}
			}

			// Token: 0x170002A6 RID: 678
			// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00024F1A File Offset: 0x0002311A
			internal override int BodyLength
			{
				get
				{
					return this.bodyLength;
				}
			}

			// Token: 0x170002A7 RID: 679
			// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00024F24 File Offset: 0x00023124
			internal override object OpaqueData
			{
				get
				{
					return new MemcachePacket.OpaquePropeties
					{
						Opcode = this.opcode,
						Opaque = this.opaque,
						IsBinary = true,
						Key = this.key
					};
				}
			}

			// Token: 0x06000B5D RID: 2909 RVA: 0x00024F68 File Offset: 0x00023168
			internal override bool ReadHeader(byte[] buffer, int count)
			{
				this.opcode = MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcode(buffer[1]);
				this.keyLength = MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt16(buffer, 2);
				this.extrasLength = (ushort)buffer[4];
				this.dataType = (ushort)buffer[5];
				this.reserved = MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt16(buffer, 6);
				this.bodyLength = (int)MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt32(buffer, 8);
				this.opaque = MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt32(buffer, 12);
				this.cas = MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt64(buffer, 16);
				this.ValidateKeyLength();
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose("DistributedCache.MemcacheBinaryProtocolPacket", string.Format(CultureInfo.InvariantCulture, "Header [Opcode: {0}, Key Length: {1}, Extras Length: {2}, DataType: 0x{3:X2}, Reserved:  0x{4:X4}, Body Length: {5}, Opaque: 0x{6:X8}, Cas: 0x{7:X16}]", new object[] { this.opcode, this.keyLength, this.extrasLength, this.dataType, this.reserved, this.bodyLength, this.opaque, this.cas }));
				}
				return true;
			}

			// Token: 0x06000B5E RID: 2910 RVA: 0x0002507C File Offset: 0x0002327C
			internal override void ReadBody(byte[] buffer)
			{
				this.key = Encoding.Default.GetString(buffer, (int)this.extrasLength, (int)this.keyLength);
				if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody))
				{
					int num = 0;
					int num2 = 0;
					int num3 = this.bodyLength - (int)this.keyLength - (int)this.extrasLength;
					int num4 = num3;
					if (this.extrasLength > 0)
					{
						if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestFlags))
						{
							num4 += 4;
							num += 4;
						}
						if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestInitialAndDeltaValue))
						{
							num2 = num;
							num += 8;
							ulong @uint = MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt64(buffer, num);
							if (Provider.IsEnabled(TraceLevel.Verbose))
							{
								EventLogWriter.WriteVerbose<ulong>("DistributedCache.MemcacheBinaryProtocolPacket", "Body [Initial Value: {0}]", @uint);
							}
							byte[] bytes = Encoding.ASCII.GetBytes(@uint.ToString(CultureInfo.InvariantCulture));
							this.MemcachePacketContainer.PropertyBag.SetElement(VelocityPacketProperty.InitialValue, bytes);
							num += 8;
						}
						this.expiry = new uint?(MemcachePacket.GetExpiry((int)MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt32(buffer, num)));
					}
					using (ChunkStream chunkStream = new ChunkStream(num4))
					{
						if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestFlags))
						{
							chunkStream.Write(buffer, 0, 4);
						}
						if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestInitialAndDeltaValue))
						{
							ulong uint2 = MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt64(buffer, num2);
							byte[] bytes2 = Encoding.ASCII.GetBytes(uint2.ToString(CultureInfo.InvariantCulture));
							chunkStream.Write(bytes2, 0, bytes2.Length);
							if (Provider.IsEnabled(TraceLevel.Verbose))
							{
								EventLogWriter.WriteVerbose<ulong>("DistributedCache.MemcacheBinaryProtocolPacket", "Body [Delta Value: {0}]", uint2);
							}
						}
						else
						{
							chunkStream.Write(buffer, (int)(this.extrasLength + this.keyLength), num3);
						}
						this.value = chunkStream.ToChunkedArray();
					}
				}
				this.MemcachePacketContainer.Key = this.key;
				this.MemcachePacketContainer.Value = this.value;
				this.MemcachePacketContainer.Version = ((this.cas == 0UL) ? MemcachePacket.DataCacheItemVersionNull : new DataCacheItemVersion(new InternalCacheItemVersion(0L, (long)this.cas)));
				this.MemcachePacketContainer.ExpiryTTL = this.expiry;
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string, uint?>("DistributedCache.MemcacheBinaryProtocolPacket", "Body [Key: {0}, Expiry: {1}]", this.key, this.expiry);
				}
			}

			// Token: 0x06000B5F RID: 2911 RVA: 0x000252B0 File Offset: 0x000234B0
			internal override void Initialize(MemcachePacket.OpaquePropeties opaqueProperties)
			{
				this.opcode = (MemcachePacket.MemcacheBinaryProtocolPacket.Opcode)opaqueProperties.Opcode;
				this.opaque = opaqueProperties.Opaque;
				if (this.MemcachePacketContainer.ResponseCode == ErrStatus.UNINITIALIZED_ERROR)
				{
					this.responseStatus = MemcachePacket.ResponseStatus.NoError;
					if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion))
					{
						this.cas = (ulong)this.MemcachePacketContainer.Version.InternalVersion.GetMemcacheVersion();
					}
					if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody))
					{
						if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseItem))
						{
							this.cas = (ulong)this.MemcachePacketContainer.Version.InternalVersion.GetMemcacheVersion();
							this.key = this.MemcachePacketContainer.Key;
							this.value = this.MemcachePacketContainer.Value;
							this.extrasLength = 4;
							foreach (byte[] array2 in this.value)
							{
								this.bodyLength += array2.Length;
							}
							if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseKey))
							{
								this.key = opaqueProperties.Key;
								this.GetKey();
								return;
							}
						}
						else
						{
							if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseInitialValue))
							{
								uint num2;
								ulong num = MemcacheProtocolHelper.ReadValueAndFlags("DistributedCache.MemcacheBinaryProtocolPacket", this.MemcachePacketContainer.Value, true, out num2);
								byte[] bytes = BitConverter.GetBytes(num);
								Array.Reverse(bytes);
								this.value = Utility.GetChunkedArray(bytes, 0);
								this.bodyLength += 8;
								return;
							}
							if (this.opcode == MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Version)
							{
								this.value = Utility.GetChunkedArray(MemcachePacket.MemcacheBinaryProtocolPacket.VersionArray, 0);
								this.bodyLength += MemcachePacket.MemcacheBinaryProtocolPacket.VersionArray.Length;
								return;
							}
							this.isEmptyPacket = this.MemcachePacketContainer.IsEmptyPacket;
							if (!this.isEmptyPacket)
							{
								this.key = this.MemcachePacketContainer.Key;
								this.GetKey();
								byte[] bytes2 = Encoding.ASCII.GetBytes((string)SerializationUtility.Deserialize(this.MemcachePacketContainer.Value, false));
								this.bodyLength += bytes2.Length;
								this.value = Utility.GetChunkedArray(bytes2, 0);
								return;
							}
						}
					}
				}
				else
				{
					this.responseStatus = MemcachePacket.GetResponseStatus(this.MemcachePacketContainer.ResponseCode, "DistributedCache.MemcacheBinaryProtocolPacket");
					byte[] errorBytes = MemcachePacket.MemcacheBinaryProtocolPacket.GetErrorBytes(this.responseStatus, this.MemcachePacketContainer.ResponseCode);
					this.bodyLength = errorBytes.Length;
					this.value = Utility.GetChunkedArray(errorBytes, 0);
				}
			}

			// Token: 0x06000B60 RID: 2912 RVA: 0x0002552C File Offset: 0x0002372C
			internal override int WriteHeader(byte[] buffer)
			{
				Array.Clear(buffer, 0, buffer.Length);
				buffer[0] = 129;
				buffer[1] = (byte)this.opcode;
				if (this.responseStatus == MemcachePacket.ResponseStatus.NoError)
				{
					byte[] array = BitConverter.GetBytes(this.keyLength);
					MemcachePacket.MemcacheBinaryProtocolPacket.CopyArray(buffer, 2, 2, array);
					buffer[4] = (byte)this.extrasLength;
					buffer[5] = (byte)this.dataType;
					array = BitConverter.GetBytes(this.reserved);
					MemcachePacket.MemcacheBinaryProtocolPacket.CopyArray(buffer, 6, 2, array);
					array = BitConverter.GetBytes(this.bodyLength);
					MemcachePacket.MemcacheBinaryProtocolPacket.CopyArray(buffer, 8, 4, array);
					array = BitConverter.GetBytes(this.opaque);
					MemcachePacket.MemcacheBinaryProtocolPacket.CopyArray(buffer, 12, 4, array);
					array = BitConverter.GetBytes(this.cas);
					MemcachePacket.MemcacheBinaryProtocolPacket.CopyArray(buffer, 16, 8, array);
				}
				else
				{
					byte[] array2 = BitConverter.GetBytes((ushort)this.responseStatus);
					MemcachePacket.MemcacheBinaryProtocolPacket.CopyArray(buffer, 6, 2, array2);
					array2 = BitConverter.GetBytes(this.bodyLength);
					MemcachePacket.MemcacheBinaryProtocolPacket.CopyArray(buffer, 8, 4, array2);
					array2 = BitConverter.GetBytes(this.opaque);
					MemcachePacket.MemcacheBinaryProtocolPacket.CopyArray(buffer, 12, 4, array2);
				}
				return this.HeaderLength;
			}

			// Token: 0x06000B61 RID: 2913 RVA: 0x00025628 File Offset: 0x00023828
			internal override void WriteBody(byte[] buffer)
			{
				if (this.responseStatus != MemcachePacket.ResponseStatus.NoError || MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody))
				{
					int num = 0;
					if (this.responseStatus == MemcachePacket.ResponseStatus.NoError && MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseKey))
					{
						byte[] bytes = Encoding.Default.GetBytes(this.key);
						bytes.CopyTo(buffer, num);
						num += bytes.Length;
					}
					foreach (byte[] array2 in this.value)
					{
						array2.CopyTo(buffer, num);
						num += array2.Length;
					}
				}
			}

			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x06000B62 RID: 2914 RVA: 0x000256B0 File Offset: 0x000238B0
			internal override TcpPacketSendTypes SendType
			{
				get
				{
					TcpPacketSendTypes tcpPacketSendTypes = TcpPacketSendTypes.None;
					if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet))
					{
						tcpPacketSendTypes |= TcpPacketSendTypes.Queue;
						if (this.responseStatus == MemcachePacket.ResponseStatus.NoError)
						{
							if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess))
							{
								tcpPacketSendTypes |= TcpPacketSendTypes.Ignore;
							}
						}
						else if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietFailure))
						{
							tcpPacketSendTypes |= TcpPacketSendTypes.Ignore;
						}
					}
					else
					{
						tcpPacketSendTypes |= TcpPacketSendTypes.Immediate;
					}
					if (this.opcode == MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Quit || this.opcode == MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.QuitQ)
					{
						tcpPacketSendTypes |= TcpPacketSendTypes.Quit;
					}
					return tcpPacketSendTypes;
				}
			}

			// Token: 0x170002A9 RID: 681
			// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00025725 File Offset: 0x00023925
			internal override VelocityPacketType PacketType
			{
				get
				{
					return MemcachePacket.MemcacheBinaryProtocolPacket.GetVelocityPacketType(this.opcode);
				}
			}

			// Token: 0x06000B64 RID: 2916 RVA: 0x00025734 File Offset: 0x00023934
			private void ValidateKeyLength()
			{
				if (MemcachePacket.MemcacheBinaryProtocolPacket.GetOpcodeBehavior(this.opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey) && this.keyLength == 0)
				{
					throw new VelocityPacketFormatException("Key length cannot be zero");
				}
				if (this.keyLength > 250)
				{
					throw new VelocityPacketFormatException("Key length cannot exceed: {0}", new object[] { 250U });
				}
			}

			// Token: 0x06000B65 RID: 2917 RVA: 0x00025790 File Offset: 0x00023990
			private void GetKey()
			{
				byte[] bytes = Encoding.Default.GetBytes(this.key);
				this.keyLength = (ushort)bytes.Length;
				this.bodyLength += bytes.Length;
			}

			// Token: 0x06000B66 RID: 2918 RVA: 0x000257C8 File Offset: 0x000239C8
			private static void CopyArray(byte[] destination, int start, int length, byte[] source)
			{
				for (int i = 0; i < length; i++)
				{
					destination[start + i] = source[length - i - 1];
				}
			}

			// Token: 0x06000B67 RID: 2919 RVA: 0x000257F0 File Offset: 0x000239F0
			private static MemcachePacket.MemcacheBinaryProtocolPacket.Opcode GetOpcode(byte opcode)
			{
				if (opcode < 0 || opcode >= 27)
				{
					throw new VelocityPacketFormatFatalException("Unknown opcode: {0}", new object[] { (MemcachePacket.MemcacheBinaryProtocolPacket.Opcode)opcode });
				}
				return (MemcachePacket.MemcacheBinaryProtocolPacket.Opcode)opcode;
			}

			// Token: 0x06000B68 RID: 2920 RVA: 0x00025828 File Offset: 0x00023A28
			private static VelocityPacketType GetVelocityPacketType(MemcachePacket.MemcacheBinaryProtocolPacket.Opcode opcode)
			{
				switch (opcode)
				{
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Get:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.GetQ:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.GetK:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.GetKq:
					return VelocityPacketType.Get;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Set:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.SetQ:
					return VelocityPacketType.Put;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Add:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.AddQ:
					return VelocityPacketType.Add;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Replace:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.ReplaceQ:
					return VelocityPacketType.Replace;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Delete:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.DeleteQ:
					return VelocityPacketType.Remove;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Increment:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.IncrementQ:
					return VelocityPacketType.Increment;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Decrement:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.DecrementQ:
					return VelocityPacketType.Memcache_Decrement;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Quit:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.QuitQ:
					return VelocityPacketType.Memcache_Noop;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Flush:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.FlushQ:
					return VelocityPacketType.Clear;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Noop:
					return VelocityPacketType.Memcache_Noop;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Version:
					return VelocityPacketType.Memcache_Noop;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Append:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.AppendQ:
					return VelocityPacketType.Append;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Prepend:
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.PrependQ:
					return VelocityPacketType.Prepend;
				case MemcachePacket.MemcacheBinaryProtocolPacket.Opcode.Stat:
					return VelocityPacketType.Memcache_Stat;
				default:
					return VelocityPacketType.None;
				}
			}

			// Token: 0x06000B69 RID: 2921 RVA: 0x000258E0 File Offset: 0x00023AE0
			private static bool GetOpcodeBehavior(MemcachePacket.MemcacheBinaryProtocolPacket.Opcode opcode, MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior option)
			{
				return (MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehaviors[(int)opcode] & option) == option;
			}

			// Token: 0x06000B6A RID: 2922 RVA: 0x000258F0 File Offset: 0x00023AF0
			private static byte[] GetErrorBytes(MemcachePacket.ResponseStatus responseStatus, ErrStatus errStatus)
			{
				switch (responseStatus)
				{
				case MemcachePacket.ResponseStatus.KeyNotFound:
					return MemcachePacket.MemcacheBinaryProtocolPacket.KeyNotFoundArray;
				case MemcachePacket.ResponseStatus.KeyExists:
					return MemcachePacket.MemcacheBinaryProtocolPacket.KeyExistsArray;
				case MemcachePacket.ResponseStatus.ValueTooLarge:
					break;
				case MemcachePacket.ResponseStatus.InvalidArguments:
					return MemcachePacket.MemcacheBinaryProtocolPacket.InvalidArgumentsArray;
				case MemcachePacket.ResponseStatus.ItemNotStored:
					return MemcachePacket.MemcacheBinaryProtocolPacket.ItemNotStoredArray;
				case MemcachePacket.ResponseStatus.IncrementDecrementOnNonNumeric:
					return MemcachePacket.MemcacheBinaryProtocolPacket.IncrementDecrementOnNonNumericArray;
				default:
					switch (responseStatus)
					{
					case MemcachePacket.ResponseStatus.UnknownCommand:
						return MemcachePacket.MemcacheBinaryProtocolPacket.UnknownCommandArray;
					case MemcachePacket.ResponseStatus.OutOfMemory:
						return MemcachePacket.MemcacheBinaryProtocolPacket.OutOfMemoryArray;
					}
					break;
				}
				int num2;
				int num = Utility.ConvertToDataCacheErrorCode(errStatus, out num2);
				string errorMessage = Utility.GetErrorMessage(num, num2);
				return Encoding.ASCII.GetBytes(errorMessage);
			}

			// Token: 0x06000B6B RID: 2923 RVA: 0x0002598B File Offset: 0x00023B8B
			private static ushort GetUInt16(byte[] buffer, int offset)
			{
				return (ushort)buffer[offset + 1] | (ushort)(buffer[offset] << 8);
			}

			// Token: 0x06000B6C RID: 2924 RVA: 0x0002599A File Offset: 0x00023B9A
			private static uint GetUInt32(byte[] buffer, int offset)
			{
				return (uint)((int)MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt16(buffer, offset + 2) | ((int)MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt16(buffer, offset) << 16));
			}

			// Token: 0x06000B6D RID: 2925 RVA: 0x000259B0 File Offset: 0x00023BB0
			private static ulong GetUInt64(byte[] buffer, int offset)
			{
				return (ulong)MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt32(buffer, offset + 4) | ((ulong)MemcachePacket.MemcacheBinaryProtocolPacket.GetUInt32(buffer, offset) << 32);
			}

			// Token: 0x040007C8 RID: 1992
			private const int OpcodeOffset = 1;

			// Token: 0x040007C9 RID: 1993
			private const int KeyLengthOffset = 2;

			// Token: 0x040007CA RID: 1994
			private const int ExtrasLengthOffset = 4;

			// Token: 0x040007CB RID: 1995
			private const int DataTypeOffset = 5;

			// Token: 0x040007CC RID: 1996
			private const int ReservedOffset = 6;

			// Token: 0x040007CD RID: 1997
			private const int BodyLengthOffset = 8;

			// Token: 0x040007CE RID: 1998
			private const int OpaqueOffset = 12;

			// Token: 0x040007CF RID: 1999
			private const int CasOffset = 16;

			// Token: 0x040007D0 RID: 2000
			private const int HeaderSizeInBytes = 24;

			// Token: 0x040007D1 RID: 2001
			private const string LogSource = "DistributedCache.MemcacheBinaryProtocolPacket";

			// Token: 0x040007D2 RID: 2002
			private static readonly MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior[] OpcodeBehaviors = new MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior[]
			{
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseItem,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestFlags | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestFlags | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestFlags | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestInitialAndDeltaValue | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseInitialValue | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestInitialAndDeltaValue | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseInitialValue | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.None,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.None,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseItem | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietFailure,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.None,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseItem,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseItem | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietFailure,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseKey,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestFlags | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestFlags | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestFlags | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestInitialAndDeltaValue | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseInitialValue | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestInitialAndDeltaValue | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseInitialValue | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.Quiet | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestKey | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.ResponseVersion | MemcachePacket.MemcacheBinaryProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess
			};

			// Token: 0x040007D3 RID: 2003
			private MemcachePacket.MemcacheBinaryProtocolPacket.Opcode opcode;

			// Token: 0x040007D4 RID: 2004
			private ushort keyLength;

			// Token: 0x040007D5 RID: 2005
			private ushort extrasLength;

			// Token: 0x040007D6 RID: 2006
			private ushort dataType;

			// Token: 0x040007D7 RID: 2007
			private ushort reserved;

			// Token: 0x040007D8 RID: 2008
			private int bodyLength;

			// Token: 0x040007D9 RID: 2009
			private uint opaque;

			// Token: 0x040007DA RID: 2010
			private ulong cas;

			// Token: 0x040007DB RID: 2011
			private MemcachePacket.ResponseStatus responseStatus;

			// Token: 0x040007DC RID: 2012
			private uint? expiry;

			// Token: 0x040007DD RID: 2013
			private string key;

			// Token: 0x040007DE RID: 2014
			private byte[][] value;

			// Token: 0x040007DF RID: 2015
			private bool isEmptyPacket;

			// Token: 0x040007E0 RID: 2016
			private static readonly byte[] KeyExistsArray = Encoding.ASCII.GetBytes("Key Exists");

			// Token: 0x040007E1 RID: 2017
			private static readonly byte[] KeyNotFoundArray = Encoding.ASCII.GetBytes("Key Not Found");

			// Token: 0x040007E2 RID: 2018
			private static readonly byte[] InvalidArgumentsArray = Encoding.ASCII.GetBytes("Invalid Arguments");

			// Token: 0x040007E3 RID: 2019
			private static readonly byte[] ItemNotStoredArray = Encoding.ASCII.GetBytes("Item Not Stored");

			// Token: 0x040007E4 RID: 2020
			private static readonly byte[] IncrementDecrementOnNonNumericArray = Encoding.ASCII.GetBytes("Increment Decrement On Non Numeric");

			// Token: 0x040007E5 RID: 2021
			private static readonly byte[] UnknownCommandArray = Encoding.ASCII.GetBytes("Unknown Command");

			// Token: 0x040007E6 RID: 2022
			private static readonly byte[] OutOfMemoryArray = Encoding.ASCII.GetBytes("Out of Memory");

			// Token: 0x040007E7 RID: 2023
			private static readonly byte[] VersionArray = Encoding.ASCII.GetBytes("1.3");

			// Token: 0x0200016A RID: 362
			[Flags]
			private enum OpcodeBehavior
			{
				// Token: 0x040007E9 RID: 2025
				None = 0,
				// Token: 0x040007EA RID: 2026
				Quiet = 1,
				// Token: 0x040007EB RID: 2027
				RequestKey = 2,
				// Token: 0x040007EC RID: 2028
				RequestBody = 4,
				// Token: 0x040007ED RID: 2029
				RequestFlags = 8,
				// Token: 0x040007EE RID: 2030
				RequestInitialAndDeltaValue = 16,
				// Token: 0x040007EF RID: 2031
				ResponseBody = 32,
				// Token: 0x040007F0 RID: 2032
				ResponseKey = 64,
				// Token: 0x040007F1 RID: 2033
				ResponseInitialValue = 128,
				// Token: 0x040007F2 RID: 2034
				ResponseItem = 256,
				// Token: 0x040007F3 RID: 2035
				ResponseVersion = 512,
				// Token: 0x040007F4 RID: 2036
				NoResponseQuietSuccess = 1024,
				// Token: 0x040007F5 RID: 2037
				NoResponseQuietFailure = 2048
			}

			// Token: 0x0200016B RID: 363
			private enum Opcode
			{
				// Token: 0x040007F7 RID: 2039
				Get,
				// Token: 0x040007F8 RID: 2040
				Set,
				// Token: 0x040007F9 RID: 2041
				Add,
				// Token: 0x040007FA RID: 2042
				Replace,
				// Token: 0x040007FB RID: 2043
				Delete,
				// Token: 0x040007FC RID: 2044
				Increment,
				// Token: 0x040007FD RID: 2045
				Decrement,
				// Token: 0x040007FE RID: 2046
				Quit,
				// Token: 0x040007FF RID: 2047
				Flush,
				// Token: 0x04000800 RID: 2048
				GetQ,
				// Token: 0x04000801 RID: 2049
				Noop,
				// Token: 0x04000802 RID: 2050
				Version,
				// Token: 0x04000803 RID: 2051
				GetK,
				// Token: 0x04000804 RID: 2052
				GetKq,
				// Token: 0x04000805 RID: 2053
				Append,
				// Token: 0x04000806 RID: 2054
				Prepend,
				// Token: 0x04000807 RID: 2055
				Stat,
				// Token: 0x04000808 RID: 2056
				SetQ,
				// Token: 0x04000809 RID: 2057
				AddQ,
				// Token: 0x0400080A RID: 2058
				ReplaceQ,
				// Token: 0x0400080B RID: 2059
				DeleteQ,
				// Token: 0x0400080C RID: 2060
				IncrementQ,
				// Token: 0x0400080D RID: 2061
				DecrementQ,
				// Token: 0x0400080E RID: 2062
				QuitQ,
				// Token: 0x0400080F RID: 2063
				FlushQ,
				// Token: 0x04000810 RID: 2064
				AppendQ,
				// Token: 0x04000811 RID: 2065
				PrependQ,
				// Token: 0x04000812 RID: 2066
				MaxDefined
			}
		}

		// Token: 0x0200016C RID: 364
		internal class MemcacheTextProtocolPacket : MemcachePacket.MemcachePacketBase
		{
			// Token: 0x06000B6F RID: 2927 RVA: 0x00024F0D File Offset: 0x0002310D
			internal MemcacheTextProtocolPacket(MemcachePacket memcachePacket)
				: base(memcachePacket)
			{
			}

			// Token: 0x170002AA RID: 682
			// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00025B4A File Offset: 0x00023D4A
			internal override int HeaderLength
			{
				get
				{
					return 512;
				}
			}

			// Token: 0x170002AB RID: 683
			// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00025B51 File Offset: 0x00023D51
			internal override int BodyLength
			{
				get
				{
					return this.bodyLength;
				}
			}

			// Token: 0x170002AC RID: 684
			// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00025B5C File Offset: 0x00023D5C
			internal override object OpaqueData
			{
				get
				{
					return new MemcachePacket.OpaquePropeties
					{
						Opcode = this.opcode,
						Opaque = this.opaque,
						IsBinary = false,
						Key = this.key,
						NoReply = this.noReply
					};
				}
			}

			// Token: 0x06000B73 RID: 2931 RVA: 0x00025BAC File Offset: 0x00023DAC
			internal override bool ReadHeader(byte[] buffer, int count)
			{
				if (count < MemcachePacket.MemcacheTextProtocolPacket.DelimiterArray.Length)
				{
					return false;
				}
				for (int i = 0; i < MemcachePacket.MemcacheTextProtocolPacket.DelimiterArray.Length; i++)
				{
					if (buffer[count - MemcachePacket.MemcacheTextProtocolPacket.DelimiterArray.Length + i] != MemcachePacket.MemcacheTextProtocolPacket.DelimiterArray[i])
					{
						return false;
					}
				}
				string @string = Encoding.ASCII.GetString(buffer, 0, count);
				int num = 0;
				string nextToken = MemcachePacket.MemcacheTextProtocolPacket.GetNextToken(@string, ref num);
				this.opcode = MemcachePacket.MemcacheTextProtocolPacket.GetOpcode(nextToken);
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key))
				{
					this.key = MemcachePacket.MemcacheTextProtocolPacket.GetNextToken(@string, ref num);
					if (this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Stat)
					{
						if (!MemcachePacket.MemcacheTextProtocolPacket.IsValidStatKey(this.key))
						{
							throw new VelocityPacketFormatException("Unknown parameter to stat: {0}", new object[] { this.key });
						}
					}
					else
					{
						MemcachePacket.MemcacheTextProtocolPacket.ValidateKey(this.key);
					}
					if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Keys))
					{
						for (;;)
						{
							string nextToken2 = MemcachePacket.MemcacheTextProtocolPacket.GetNextToken(@string, ref num);
							if (string.IsNullOrEmpty(nextToken2))
							{
								break;
							}
							MemcachePacket.MemcacheTextProtocolPacket.ValidateKey(this.key);
							if (this.keys == null)
							{
								this.opcode = MemcachePacket.MemcacheTextProtocolPacket.GetBulkOpcode(this.opcode);
								this.keys = new List<string> { this.key };
							}
							this.keys.Add(nextToken2);
						}
					}
				}
				this.item = new MemcachePacket.MemcacheTextProtocolPacket.MemcacheItem();
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Flags))
				{
					this.item.Flag = MemcachePacket.MemcacheTextProtocolPacket.ParseUInt32(MemcachePacket.MemcacheTextProtocolPacket.GetNextToken(@string, ref num));
				}
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Expiry))
				{
					this.expiry = new uint?(MemcachePacket.GetExpiry(MemcachePacket.MemcacheTextProtocolPacket.ParseInt32(MemcachePacket.MemcacheTextProtocolPacket.GetNextToken(@string, ref num))));
				}
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Bytes))
				{
					this.item.BodyLength = (int)MemcachePacket.MemcacheTextProtocolPacket.ParseUInt32(MemcachePacket.MemcacheTextProtocolPacket.GetNextToken(@string, ref num));
					this.item.BodyLength += "\r\n".Length;
					this.bodyLength = this.item.BodyLength;
				}
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Cas))
				{
					this.item.Cas = MemcachePacket.MemcacheTextProtocolPacket.ParseUInt64(MemcachePacket.MemcacheTextProtocolPacket.GetNextToken(@string, ref num));
				}
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Value))
				{
					this.deltaValue = MemcachePacket.MemcacheTextProtocolPacket.ParseUInt64(MemcachePacket.MemcacheTextProtocolPacket.GetNextToken(@string, ref num));
					byte[] bytes = Encoding.ASCII.GetBytes(0UL.ToString(CultureInfo.InvariantCulture));
					this.MemcachePacketContainer.PropertyBag.SetElement(VelocityPacketProperty.InitialValue, bytes);
				}
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestOptionalTime))
				{
					int num2 = num;
					uint num3;
					if (uint.TryParse(MemcachePacket.MemcacheTextProtocolPacket.GetNextToken(@string, ref num), out num3))
					{
						if (this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Delete && num3 != 0U)
						{
							throw new VelocityPacketFormatException("Unsupported time value: {0}", new object[] { num3 });
						}
					}
					else
					{
						num = num2;
					}
				}
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply))
				{
					string nextToken3 = MemcachePacket.MemcacheTextProtocolPacket.GetNextToken(@string, ref num);
					if (!string.IsNullOrEmpty(nextToken3))
					{
						if (string.Compare(nextToken3, "noreply", StringComparison.Ordinal) != 0)
						{
							throw new VelocityPacketFormatException("Unexpected token: {0}", new object[] { nextToken3 });
						}
						this.noReply = true;
					}
				}
				string nextToken4 = MemcachePacket.MemcacheTextProtocolPacket.GetNextToken(@string, ref num);
				if (!string.IsNullOrEmpty(nextToken4))
				{
					throw new VelocityPacketFormatException("Unexpected token: {0}", new object[] { nextToken4 });
				}
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBulk))
				{
					this.MemcachePacketContainer.Keys = this.keys;
				}
				else
				{
					this.MemcachePacketContainer.Key = this.key;
					this.MemcachePacketContainer.Version = ((this.item.Cas == 0UL) ? MemcachePacket.DataCacheItemVersionNull : new DataCacheItemVersion(new InternalCacheItemVersion(0L, (long)this.item.Cas)));
					this.MemcachePacketContainer.ExpiryTTL = this.expiry;
				}
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Value))
				{
					this.MemcachePacketContainer.Value = MemcacheProtocolHelper.WriteValueAndFlags(this.deltaValue, false, 0U);
					this.MemcachePacketContainer.ExpiryTTL = new uint?(uint.MaxValue);
				}
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose("DistributedCache.MemcacheTextProtocolPacket", string.Format(CultureInfo.InvariantCulture, "Header [Opcode: {0}, Key: {1}, Flags: 0x{2:X8}, Expiry: {3}, Bytes: {4}, Cas: 0x{5:X16}, Value: {6}, No Reply: {7}]", new object[]
					{
						this.opcode,
						this.key,
						this.item.Flag,
						this.expiry,
						this.bodyLength,
						this.item.Cas,
						this.deltaValue,
						this.noReply
					}));
				}
				return true;
			}

			// Token: 0x06000B74 RID: 2932 RVA: 0x0002604C File Offset: 0x0002424C
			internal override void ReadBody(byte[] buffer)
			{
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBody))
				{
					int num = this.item.BodyLength - "\r\n".Length;
					int num2 = num;
					if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestFlags))
					{
						num2 += 4;
					}
					using (ChunkStream chunkStream = new ChunkStream(num2))
					{
						if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestFlags))
						{
							byte[] bytes = BitConverter.GetBytes(this.item.Flag);
							chunkStream.Write(bytes, 0, 4);
						}
						chunkStream.Write(buffer, 0, num);
						this.item.Value = chunkStream.ToChunkedArray();
					}
				}
				this.MemcachePacketContainer.Value = this.item.Value;
			}

			// Token: 0x06000B75 RID: 2933 RVA: 0x0002611C File Offset: 0x0002431C
			internal override void Initialize(MemcachePacket.OpaquePropeties opaqueProperties)
			{
				this.opcode = (MemcachePacket.MemcacheTextProtocolPacket.Opcode)opaqueProperties.Opcode;
				this.opaque = opaqueProperties.Opaque;
				this.noReply = opaqueProperties.NoReply;
				if (this.MemcachePacketContainer.ResponseCode == ErrStatus.UNINITIALIZED_ERROR)
				{
					if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseItem) || this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Stat)
					{
						if (this.IsGetOperation())
						{
							this.key = opaqueProperties.Key;
						}
						else
						{
							this.isEmptyPacket = this.MemcachePacketContainer.IsEmptyPacket;
							this.key = this.MemcachePacketContainer.Key;
						}
					}
					else if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseValue))
					{
						uint num;
						this.deltaValue = MemcacheProtocolHelper.ReadValueAndFlags("DistributedCache.MemcacheTextProtocolPacket", this.MemcachePacketContainer.Value, true, out num);
					}
					this.item = this.GetMemcacheItem(this.MemcachePacketContainer);
				}
				else
				{
					this.item = new MemcachePacket.MemcacheTextProtocolPacket.MemcacheItem
					{
						Status = MemcachePacket.MemcacheTextProtocolPacket.GetResponseStatus(this.opcode, this.MemcachePacketContainer.ResponseCode)
					};
				}
				this.bodyLength = this.GetBodyLength();
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x00026234 File Offset: 0x00024434
			internal override int WriteHeader(byte[] buffer)
			{
				if (!this.IsResponseHeaderOnly())
				{
					return 0;
				}
				if (this.item.Status == MemcachePacket.ResponseStatus.NoError)
				{
					return MemcachePacket.MemcacheTextProtocolPacket.WriteSuccessStatus(this.opcode, buffer);
				}
				return MemcachePacket.MemcacheTextProtocolPacket.WriteErrorStatus(this.item.Status, this.MemcachePacketContainer.ResponseCode, buffer);
			}

			// Token: 0x06000B77 RID: 2935 RVA: 0x00026284 File Offset: 0x00024484
			internal override void WriteBody(byte[] buffer)
			{
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseBody))
				{
					if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseItem))
					{
						this.WriteMemcacheItem(buffer);
						return;
					}
					if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseValue))
					{
						string text = string.Format(CultureInfo.InvariantCulture, "{0}\r\n", new object[] { this.deltaValue });
						Encoding.ASCII.GetBytes(text).CopyTo(buffer, 0);
						return;
					}
					if (this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Version)
					{
						Encoding.ASCII.GetBytes(MemcachePacket.MemcacheTextProtocolPacket.VersionResponseString).CopyTo(buffer, 0);
						return;
					}
					this.WriteStat(buffer);
				}
			}

			// Token: 0x170002AD RID: 685
			// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00026334 File Offset: 0x00024534
			internal override TcpPacketSendTypes SendType
			{
				get
				{
					TcpPacketSendTypes tcpPacketSendTypes = TcpPacketSendTypes.None;
					if (this.noReply)
					{
						tcpPacketSendTypes |= TcpPacketSendTypes.Queue;
						if (this.IsOperationSuccess() && MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess))
						{
							tcpPacketSendTypes |= TcpPacketSendTypes.Ignore;
						}
					}
					else
					{
						tcpPacketSendTypes |= TcpPacketSendTypes.Immediate;
					}
					if (this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Quit && this.MemcachePacketContainer.ResponseCode == ErrStatus.UNINITIALIZED_ERROR)
					{
						tcpPacketSendTypes |= TcpPacketSendTypes.Quit | TcpPacketSendTypes.Ignore;
					}
					return tcpPacketSendTypes;
				}
			}

			// Token: 0x170002AE RID: 686
			// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0002638F File Offset: 0x0002458F
			internal override VelocityPacketType PacketType
			{
				get
				{
					return MemcachePacket.MemcacheTextProtocolPacket.GetVelocityPacketType(this.opcode);
				}
			}

			// Token: 0x06000B7A RID: 2938 RVA: 0x0002639C File Offset: 0x0002459C
			private bool IsOperationSuccess()
			{
				return this.item.Status == MemcachePacket.ResponseStatus.NoError || (this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Cas && this.item.Status == MemcachePacket.ResponseStatus.KeyExists) || ((this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Add || this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Replace || this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Append || this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Prepend) && this.item.Status == MemcachePacket.ResponseStatus.ItemNotStored) || (this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Delete && this.item.Status == MemcachePacket.ResponseStatus.KeyNotFound);
			}

			// Token: 0x06000B7B RID: 2939 RVA: 0x0002641D File Offset: 0x0002461D
			private bool IsGetOperation()
			{
				return this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Get || this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Gets;
			}

			// Token: 0x06000B7C RID: 2940 RVA: 0x00026434 File Offset: 0x00024634
			private bool IsResponseHeaderOnly()
			{
				return this.item.Status == MemcachePacket.ResponseStatus.InvalidArguments || !MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseBody) || (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseValue) && this.item.Status != MemcachePacket.ResponseStatus.NoError);
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x00026488 File Offset: 0x00024688
			private MemcachePacket.MemcacheTextProtocolPacket.MemcacheItem GetMemcacheItem(IVelocityResponsePacket velocityPacket)
			{
				MemcachePacket.MemcacheTextProtocolPacket.MemcacheItem memcacheItem = new MemcachePacket.MemcacheTextProtocolPacket.MemcacheItem
				{
					Status = MemcachePacket.ResponseStatus.NoError,
					Value = velocityPacket.Value
				};
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseItem) && !velocityPacket.IsEmptyPacket)
				{
					memcacheItem.Cas = (ulong)velocityPacket.Version.InternalVersion.GetMemcacheVersion();
					for (int i = 0; i < memcacheItem.Value.Length; i++)
					{
						byte[] array = memcacheItem.Value[i];
						memcacheItem.BodyLength += array.Length;
						if (i == 0)
						{
							memcacheItem.Flag = BitConverter.ToUInt32(array, 0);
							memcacheItem.BodyLength -= 4;
						}
					}
				}
				return memcacheItem;
			}

			// Token: 0x06000B7E RID: 2942 RVA: 0x00026530 File Offset: 0x00024730
			private void WriteMemcacheItem(byte[] buffer)
			{
				int num = 0;
				if (this.item.Status == MemcachePacket.ResponseStatus.NoError && !this.isEmptyPacket)
				{
					string text = (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseVersion) ? string.Format(CultureInfo.InvariantCulture, "VALUE {0} {1} {2} {3}\r\n", new object[]
					{
						this.key,
						this.item.Flag,
						this.item.BodyLength,
						this.item.Cas
					}) : string.Format(CultureInfo.InvariantCulture, "VALUE {0} {1} {2}\r\n", new object[]
					{
						this.key,
						this.item.Flag,
						this.item.BodyLength
					}));
					byte[] bytes = Encoding.ASCII.GetBytes(text);
					num = bytes.Length;
					bytes.CopyTo(buffer, 0);
					for (int i = 0; i < this.item.Value.Length; i++)
					{
						byte[] array = this.item.Value[i];
						if (i == 0)
						{
							int num2 = array.Length - 4;
							Array.Copy(array, 4, buffer, num, num2);
							num += num2;
						}
						else
						{
							array.CopyTo(buffer, num);
							num += array.Length;
						}
					}
					Array.Copy(MemcachePacket.MemcacheTextProtocolPacket.DelimiterArray, 0, buffer, num, MemcachePacket.MemcacheTextProtocolPacket.DelimiterArray.Length);
					num += MemcachePacket.MemcacheTextProtocolPacket.DelimiterArray.Length;
				}
				if (this.IsGetOperation() || this.isEmptyPacket)
				{
					MemcachePacket.MemcacheTextProtocolPacket.EndArray.CopyTo(buffer, num);
				}
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x000266C0 File Offset: 0x000248C0
			private void WriteStat(byte[] buffer)
			{
				if (this.isEmptyPacket)
				{
					MemcachePacket.MemcacheTextProtocolPacket.EndArray.CopyTo(buffer, 0);
					return;
				}
				string text = string.Format(CultureInfo.InvariantCulture, "STAT {0} {1}\r\n", new object[]
				{
					this.key,
					(string)SerializationUtility.Deserialize(this.item.Value, false)
				});
				byte[] bytes = Encoding.ASCII.GetBytes(text);
				bytes.CopyTo(buffer, 0);
			}

			// Token: 0x06000B80 RID: 2944 RVA: 0x00026730 File Offset: 0x00024930
			private int GetBodyLength()
			{
				if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseBody))
				{
					int num = 0;
					if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseItem))
					{
						if (this.item.Status == MemcachePacket.ResponseStatus.NoError && !this.isEmptyPacket)
						{
							num = this.key.Length + this.item.Flag.ToString(CultureInfo.InvariantCulture).Length + this.item.BodyLength.ToString(CultureInfo.InvariantCulture).Length;
							num += (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseVersion) ? (this.item.Cas.ToString(CultureInfo.InvariantCulture).Length + 11) : 10);
							for (int i = 0; i < this.item.Value.Length; i++)
							{
								byte[] array = this.item.Value[i];
								num += array.Length;
								if (i == 0)
								{
									num -= 4;
								}
							}
							num += MemcachePacket.MemcacheTextProtocolPacket.DelimiterArray.Length;
						}
						if (this.item.Status != MemcachePacket.ResponseStatus.InvalidArguments && (this.IsGetOperation() || this.isEmptyPacket))
						{
							num += MemcachePacket.MemcacheTextProtocolPacket.EndArray.Length;
						}
					}
					else if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(this.opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseValue))
					{
						if (this.item.Status == MemcachePacket.ResponseStatus.NoError)
						{
							num += 2 + this.deltaValue.ToString(CultureInfo.InvariantCulture).Length;
						}
					}
					else if (this.opcode == MemcachePacket.MemcacheTextProtocolPacket.Opcode.Version)
					{
						if (this.item.Status == MemcachePacket.ResponseStatus.NoError)
						{
							num += MemcachePacket.MemcacheTextProtocolPacket.VersionResponseString.Length;
						}
					}
					else if (this.item.Status == MemcachePacket.ResponseStatus.NoError)
					{
						if (this.isEmptyPacket)
						{
							num += MemcachePacket.MemcacheTextProtocolPacket.EndArray.Length;
						}
						else
						{
							string text = (string)SerializationUtility.Deserialize(this.item.Value, false);
							num += 8 + this.key.ToString(CultureInfo.InvariantCulture).Length + text.ToString(CultureInfo.InvariantCulture).Length;
						}
					}
					return num;
				}
				return 0;
			}

			// Token: 0x06000B81 RID: 2945 RVA: 0x00026948 File Offset: 0x00024B48
			private static MemcachePacket.MemcacheTextProtocolPacket.Opcode GetBulkOpcode(MemcachePacket.MemcacheTextProtocolPacket.Opcode opcode)
			{
				switch (opcode)
				{
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Get:
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.BulkGet;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Gets:
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.BulkGets;
				default:
					throw new ArgumentException("Opcode");
				}
			}

			// Token: 0x06000B82 RID: 2946 RVA: 0x00026978 File Offset: 0x00024B78
			private static void ValidateKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					throw new VelocityPacketFormatException("Key was expected but not found");
				}
				if ((long)key.Length > 250L)
				{
					throw new VelocityPacketFormatException("Key length cannot exceed: {0}", new object[] { 250U });
				}
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x000269C8 File Offset: 0x00024BC8
			private static bool IsValidStatKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return true;
				}
				switch (key)
				{
				case "pid":
				case "uptime":
				case "time":
				case "version":
				case "pointer_size":
				case "rusage_system":
				case "rusage_user":
				case "curr_items":
				case "curr_connections":
				case "total_connections":
				case "bytes":
				case "cmd_get":
				case "cmd_set":
				case "get_hits":
				case "get_misses":
				case "bytes_read":
				case "bytes_written":
				case "limit_maxbytes":
				case "settings":
					return true;
				}
				return false;
			}

			// Token: 0x06000B84 RID: 2948 RVA: 0x00026B58 File Offset: 0x00024D58
			private static VelocityPacketType GetVelocityPacketType(MemcachePacket.MemcacheTextProtocolPacket.Opcode opcode)
			{
				switch (opcode)
				{
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Get:
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Gets:
					return VelocityPacketType.Get;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Set:
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Cas:
					return VelocityPacketType.Put;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Add:
					return VelocityPacketType.Add;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Replace:
					return VelocityPacketType.Replace;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Delete:
					return VelocityPacketType.Remove;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Increment:
					return VelocityPacketType.Increment;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Decrement:
					return VelocityPacketType.Memcache_Decrement;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Quit:
					return VelocityPacketType.Memcache_Noop;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Flush:
					return VelocityPacketType.Clear;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Noop:
					return VelocityPacketType.Memcache_Noop;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Version:
					return VelocityPacketType.Memcache_Noop;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Append:
					return VelocityPacketType.Append;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Prepend:
					return VelocityPacketType.Prepend;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Stat:
					return VelocityPacketType.Memcache_Stat;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Touch:
					return VelocityPacketType.ResetTimeout;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Verbosity:
					return VelocityPacketType.Memcache_Noop;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.BulkGet:
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.BulkGets:
					return VelocityPacketType.Memcache_CacheBulkGet;
				default:
					return VelocityPacketType.None;
				}
			}

			// Token: 0x06000B85 RID: 2949 RVA: 0x00026C03 File Offset: 0x00024E03
			private static bool IsOpcodeBehavior(MemcachePacket.MemcacheTextProtocolPacket.Opcode opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior option)
			{
				return (MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehaviors[(int)opcode] & option) == option;
			}

			// Token: 0x06000B86 RID: 2950 RVA: 0x00026C14 File Offset: 0x00024E14
			private static MemcachePacket.MemcacheTextProtocolPacket.Opcode GetOpcode(string opcode)
			{
				switch (opcode)
				{
				case "set":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Set;
				case "add":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Add;
				case "replace":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Replace;
				case "cas":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Cas;
				case "append":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Append;
				case "prepend":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Prepend;
				case "get":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Get;
				case "gets":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Gets;
				case "delete":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Delete;
				case "incr":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Increment;
				case "decr":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Decrement;
				case "flush_all":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Flush;
				case "version":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Version;
				case "stats":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Stat;
				case "touch":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Touch;
				case "quit":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Quit;
				case "verbosity":
					return MemcachePacket.MemcacheTextProtocolPacket.Opcode.Verbosity;
				}
				throw new VelocityPacketFormatFatalException("Unknown opcode: {0}", new object[] { opcode });
			}

			// Token: 0x06000B87 RID: 2951 RVA: 0x00026DB4 File Offset: 0x00024FB4
			private static MemcachePacket.ResponseStatus GetResponseStatus(MemcachePacket.MemcacheTextProtocolPacket.Opcode opcode, ErrStatus errStatus)
			{
				switch (errStatus)
				{
				case ErrStatus.VERSION_MISMATCH:
					return MemcachePacket.ResponseStatus.KeyExists;
				case ErrStatus.KEY_ALREADY_EXISTS:
					if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBody))
					{
						return MemcachePacket.ResponseStatus.ItemNotStored;
					}
					return MemcachePacket.ResponseStatus.KeyExists;
				case ErrStatus.KEY_DOES_NOT_EXIST:
					if (MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBody) && !MemcachePacket.MemcacheTextProtocolPacket.IsOpcodeBehavior(opcode, MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Cas))
					{
						return MemcachePacket.ResponseStatus.ItemNotStored;
					}
					return MemcachePacket.ResponseStatus.KeyNotFound;
				default:
					return MemcachePacket.GetResponseStatus(errStatus, "DistributedCache.MemcacheTextProtocolPacket");
				}
			}

			// Token: 0x06000B88 RID: 2952 RVA: 0x00026E14 File Offset: 0x00025014
			private static int WriteErrorStatus(MemcachePacket.ResponseStatus responseStatus, ErrStatus errStatus, byte[] buffer)
			{
				byte[] array;
				switch (responseStatus)
				{
				case MemcachePacket.ResponseStatus.KeyNotFound:
					array = MemcachePacket.MemcacheTextProtocolPacket.NotFoundArray;
					goto IL_0134;
				case MemcachePacket.ResponseStatus.KeyExists:
					array = MemcachePacket.MemcacheTextProtocolPacket.ExistsArray;
					goto IL_0134;
				case MemcachePacket.ResponseStatus.ValueTooLarge:
					break;
				case MemcachePacket.ResponseStatus.InvalidArguments:
					array = MemcachePacket.MemcacheTextProtocolPacket.InvalidArgumentsArray;
					goto IL_0134;
				case MemcachePacket.ResponseStatus.ItemNotStored:
					array = MemcachePacket.MemcacheTextProtocolPacket.NotStoredArray;
					goto IL_0134;
				case MemcachePacket.ResponseStatus.IncrementDecrementOnNonNumeric:
					array = MemcachePacket.MemcacheTextProtocolPacket.IncrementDecrementOnNonNumericArray;
					goto IL_0134;
				default:
					switch (responseStatus)
					{
					case MemcachePacket.ResponseStatus.UnknownCommand:
						array = MemcachePacket.MemcacheTextProtocolPacket.UnknownCommandArray;
						goto IL_0134;
					case MemcachePacket.ResponseStatus.OutOfMemory:
						array = MemcachePacket.MemcacheTextProtocolPacket.OutofMemoryErrorArray;
						goto IL_0134;
					}
					break;
				}
				int num2;
				int num = Utility.ConvertToDataCacheErrorCode(errStatus, out num2);
				string text = Utility.GetErrorMessage(num, num2);
				foreach (char c in new char[] { '\r', '\n' })
				{
					text = text.Replace(c, ' ');
				}
				int num3 = buffer.Length - "SERVER_ERROR ".Length - "\r\n".Length;
				if (text.Length > num3)
				{
					text = text.Substring(0, num3);
				}
				string text2 = "SERVER_ERROR " + text + "\r\n";
				array = Encoding.ASCII.GetBytes(text2);
				IL_0134:
				array.CopyTo(buffer, 0);
				return array.Length;
			}

			// Token: 0x06000B89 RID: 2953 RVA: 0x00026F60 File Offset: 0x00025160
			private static int WriteSuccessStatus(MemcachePacket.MemcacheTextProtocolPacket.Opcode opcode, byte[] buffer)
			{
				byte[] array;
				switch (opcode)
				{
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Set:
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Add:
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Replace:
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Append:
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Prepend:
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Cas:
					array = MemcachePacket.MemcacheTextProtocolPacket.StoredArray;
					goto IL_0077;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Delete:
					array = MemcachePacket.MemcacheTextProtocolPacket.DeletedArray;
					goto IL_0077;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Quit:
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Flush:
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Verbosity:
					array = MemcachePacket.MemcacheTextProtocolPacket.OkArray;
					goto IL_0077;
				case MemcachePacket.MemcacheTextProtocolPacket.Opcode.Touch:
					array = MemcachePacket.MemcacheTextProtocolPacket.TouchedArray;
					goto IL_0077;
				}
				throw new ArgumentException("Opcode");
				IL_0077:
				array.CopyTo(buffer, 0);
				return array.Length;
			}

			// Token: 0x06000B8A RID: 2954 RVA: 0x00026FF0 File Offset: 0x000251F0
			private static string GetNextToken(string header, ref int index)
			{
				if (string.Compare(header, index, "\r\n", 0, "\r\n".Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return null;
				}
				int num = index++;
				index = header.IndexOf(' ', index);
				if (index == -1)
				{
					index = header.IndexOf("\r\n", num, StringComparison.OrdinalIgnoreCase);
				}
				num = ((num == 0) ? num : (num + 1));
				return header.Substring(num, index - num);
			}

			// Token: 0x06000B8B RID: 2955 RVA: 0x0002705C File Offset: 0x0002525C
			private static uint ParseUInt32(string token)
			{
				uint num;
				if (!uint.TryParse(token, out num))
				{
					throw new VelocityPacketFormatException("Parse failure for token: {0} [expected UInt32]", new object[] { token });
				}
				return num;
			}

			// Token: 0x06000B8C RID: 2956 RVA: 0x0002708C File Offset: 0x0002528C
			private static int ParseInt32(string token)
			{
				int num;
				if (!int.TryParse(token, out num))
				{
					throw new VelocityPacketFormatException("Parse failure for token: {0} [expected Int32]", new object[] { token });
				}
				return num;
			}

			// Token: 0x06000B8D RID: 2957 RVA: 0x000270BC File Offset: 0x000252BC
			private static ulong ParseUInt64(string token)
			{
				ulong num;
				if (!ulong.TryParse(token, out num))
				{
					throw new VelocityPacketFormatException("Parse failure for token: {0} [expected UInt64]", new object[] { token });
				}
				return num;
			}

			// Token: 0x04000813 RID: 2067
			private const int HeaderSizeInBytes = 512;

			// Token: 0x04000814 RID: 2068
			private const int ValueFormatLength = 10;

			// Token: 0x04000815 RID: 2069
			private const int ValueFormatWithCasLength = 11;

			// Token: 0x04000816 RID: 2070
			private const int IncrDecrFormatLength = 2;

			// Token: 0x04000817 RID: 2071
			private const int StatFormatLength = 8;

			// Token: 0x04000818 RID: 2072
			private const string Delimiter = "\r\n";

			// Token: 0x04000819 RID: 2073
			private const string ValueFormatString = "VALUE {0} {1} {2}\r\n";

			// Token: 0x0400081A RID: 2074
			private const string ValueFormatWithCasString = "VALUE {0} {1} {2} {3}\r\n";

			// Token: 0x0400081B RID: 2075
			private const string IncrDecrFormatString = "{0}\r\n";

			// Token: 0x0400081C RID: 2076
			private const string StatFormatString = "STAT {0} {1}\r\n";

			// Token: 0x0400081D RID: 2077
			private const string NoReply = "noreply";

			// Token: 0x0400081E RID: 2078
			private const string LogSource = "DistributedCache.MemcacheTextProtocolPacket";

			// Token: 0x0400081F RID: 2079
			private const string CustomErrorPrefix = "SERVER_ERROR ";

			// Token: 0x04000820 RID: 2080
			private const string CustomErrorSuffix = "\r\n";

			// Token: 0x04000821 RID: 2081
			private static readonly string VersionResponseString = string.Format(CultureInfo.InvariantCulture, "VERSION {0}\r\n", new object[] { "1.3" });

			// Token: 0x04000822 RID: 2082
			private static readonly MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior[] OpcodeBehaviors = new MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior[]
			{
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Keys | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseItem | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Keys | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseItem | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseVersion | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Flags | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Expiry | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Bytes | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestFlags | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Flags | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Expiry | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Bytes | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestFlags | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Flags | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Expiry | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Bytes | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestFlags | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestOptionalTime | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Value | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseValue | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Value | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseValue | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.None,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestOptionalTime | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.None,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseBody,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Flags | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Expiry | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Bytes | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Flags | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Expiry | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Bytes | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseBody,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Flags | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Expiry | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Bytes | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Cas | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBody | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestFlags | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Key | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Expiry | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.Value | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoReply | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.NoResponseQuietSuccess,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBulk | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseItem | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseBody,
				MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.RequestBulk | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseItem | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseBody | MemcachePacket.MemcacheTextProtocolPacket.OpcodeBehavior.ResponseVersion
			};

			// Token: 0x04000823 RID: 2083
			private MemcachePacket.MemcacheTextProtocolPacket.Opcode opcode;

			// Token: 0x04000824 RID: 2084
			private uint opaque;

			// Token: 0x04000825 RID: 2085
			private ulong deltaValue;

			// Token: 0x04000826 RID: 2086
			private bool noReply;

			// Token: 0x04000827 RID: 2087
			private uint? expiry;

			// Token: 0x04000828 RID: 2088
			private string key;

			// Token: 0x04000829 RID: 2089
			private MemcachePacket.MemcacheTextProtocolPacket.MemcacheItem item;

			// Token: 0x0400082A RID: 2090
			private bool isEmptyPacket;

			// Token: 0x0400082B RID: 2091
			private IList<string> keys;

			// Token: 0x0400082C RID: 2092
			private int bodyLength;

			// Token: 0x0400082D RID: 2093
			private static readonly byte[] DelimiterArray = Encoding.ASCII.GetBytes("\r\n");

			// Token: 0x0400082E RID: 2094
			private static readonly byte[] OkArray = Encoding.ASCII.GetBytes("OK\r\n");

			// Token: 0x0400082F RID: 2095
			private static readonly byte[] EndArray = Encoding.ASCII.GetBytes("END\r\n");

			// Token: 0x04000830 RID: 2096
			private static readonly byte[] StoredArray = Encoding.ASCII.GetBytes("STORED\r\n");

			// Token: 0x04000831 RID: 2097
			private static readonly byte[] DeletedArray = Encoding.ASCII.GetBytes("DELETED\r\n");

			// Token: 0x04000832 RID: 2098
			private static readonly byte[] TouchedArray = Encoding.ASCII.GetBytes("TOUCHED\r\n");

			// Token: 0x04000833 RID: 2099
			private static readonly byte[] ExistsArray = Encoding.ASCII.GetBytes("EXISTS\r\n");

			// Token: 0x04000834 RID: 2100
			private static readonly byte[] NotFoundArray = Encoding.ASCII.GetBytes("NOT_FOUND\r\n");

			// Token: 0x04000835 RID: 2101
			private static readonly byte[] InvalidArgumentsArray = Encoding.ASCII.GetBytes("CLIENT_ERROR Invalid arguments\r\n");

			// Token: 0x04000836 RID: 2102
			private static readonly byte[] NotStoredArray = Encoding.ASCII.GetBytes("NOT_STORED\r\n");

			// Token: 0x04000837 RID: 2103
			private static readonly byte[] IncrementDecrementOnNonNumericArray = Encoding.ASCII.GetBytes("CLIENT_ERROR Cannot increment or decrement non-numeric value\r\n");

			// Token: 0x04000838 RID: 2104
			private static readonly byte[] UnknownCommandArray = Encoding.ASCII.GetBytes("ERROR\r\n");

			// Token: 0x04000839 RID: 2105
			private static readonly byte[] OutofMemoryErrorArray = Encoding.ASCII.GetBytes("SERVER_ERROR Out of memory\r\n");

			// Token: 0x0200016D RID: 365
			[Flags]
			private enum OpcodeBehavior
			{
				// Token: 0x0400083B RID: 2107
				None = 0,
				// Token: 0x0400083C RID: 2108
				Key = 1,
				// Token: 0x0400083D RID: 2109
				Flags = 2,
				// Token: 0x0400083E RID: 2110
				Expiry = 4,
				// Token: 0x0400083F RID: 2111
				Bytes = 8,
				// Token: 0x04000840 RID: 2112
				Cas = 16,
				// Token: 0x04000841 RID: 2113
				Value = 32,
				// Token: 0x04000842 RID: 2114
				NoReply = 64,
				// Token: 0x04000843 RID: 2115
				Keys = 128,
				// Token: 0x04000844 RID: 2116
				RequestBody = 256,
				// Token: 0x04000845 RID: 2117
				RequestFlags = 512,
				// Token: 0x04000846 RID: 2118
				RequestOptionalTime = 1024,
				// Token: 0x04000847 RID: 2119
				RequestBulk = 2048,
				// Token: 0x04000848 RID: 2120
				ResponseItem = 4096,
				// Token: 0x04000849 RID: 2121
				ResponseValue = 8192,
				// Token: 0x0400084A RID: 2122
				ResponseBody = 16384,
				// Token: 0x0400084B RID: 2123
				ResponseVersion = 32768,
				// Token: 0x0400084C RID: 2124
				NoResponseQuietSuccess = 65536
			}

			// Token: 0x0200016E RID: 366
			private enum Opcode
			{
				// Token: 0x0400084E RID: 2126
				Get,
				// Token: 0x0400084F RID: 2127
				Gets,
				// Token: 0x04000850 RID: 2128
				Set,
				// Token: 0x04000851 RID: 2129
				Add,
				// Token: 0x04000852 RID: 2130
				Replace,
				// Token: 0x04000853 RID: 2131
				Delete,
				// Token: 0x04000854 RID: 2132
				Increment,
				// Token: 0x04000855 RID: 2133
				Decrement,
				// Token: 0x04000856 RID: 2134
				Quit,
				// Token: 0x04000857 RID: 2135
				Flush,
				// Token: 0x04000858 RID: 2136
				Noop,
				// Token: 0x04000859 RID: 2137
				Version,
				// Token: 0x0400085A RID: 2138
				Append,
				// Token: 0x0400085B RID: 2139
				Prepend,
				// Token: 0x0400085C RID: 2140
				Stat,
				// Token: 0x0400085D RID: 2141
				Cas,
				// Token: 0x0400085E RID: 2142
				Touch,
				// Token: 0x0400085F RID: 2143
				Verbosity,
				// Token: 0x04000860 RID: 2144
				BulkGet,
				// Token: 0x04000861 RID: 2145
				BulkGets
			}

			// Token: 0x0200016F RID: 367
			private class MemcacheItem
			{
				// Token: 0x170002AF RID: 687
				// (get) Token: 0x06000B8F RID: 2959 RVA: 0x000272C9 File Offset: 0x000254C9
				// (set) Token: 0x06000B90 RID: 2960 RVA: 0x000272D1 File Offset: 0x000254D1
				internal uint Flag { get; set; }

				// Token: 0x170002B0 RID: 688
				// (get) Token: 0x06000B91 RID: 2961 RVA: 0x000272DA File Offset: 0x000254DA
				// (set) Token: 0x06000B92 RID: 2962 RVA: 0x000272E2 File Offset: 0x000254E2
				internal int BodyLength { get; set; }

				// Token: 0x170002B1 RID: 689
				// (get) Token: 0x06000B93 RID: 2963 RVA: 0x000272EB File Offset: 0x000254EB
				// (set) Token: 0x06000B94 RID: 2964 RVA: 0x000272F3 File Offset: 0x000254F3
				internal ulong Cas { get; set; }

				// Token: 0x170002B2 RID: 690
				// (get) Token: 0x06000B95 RID: 2965 RVA: 0x000272FC File Offset: 0x000254FC
				// (set) Token: 0x06000B96 RID: 2966 RVA: 0x00027304 File Offset: 0x00025504
				internal byte[][] Value { get; set; }

				// Token: 0x170002B3 RID: 691
				// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0002730D File Offset: 0x0002550D
				// (set) Token: 0x06000B98 RID: 2968 RVA: 0x00027315 File Offset: 0x00025515
				internal MemcachePacket.ResponseStatus Status { get; set; }
			}
		}

		// Token: 0x02000170 RID: 368
		internal class OpaquePropeties
		{
			// Token: 0x170002B4 RID: 692
			// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0002731E File Offset: 0x0002551E
			// (set) Token: 0x06000B9B RID: 2971 RVA: 0x00027326 File Offset: 0x00025526
			internal object Opcode { get; set; }

			// Token: 0x170002B5 RID: 693
			// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0002732F File Offset: 0x0002552F
			// (set) Token: 0x06000B9D RID: 2973 RVA: 0x00027337 File Offset: 0x00025537
			internal uint Opaque { get; set; }

			// Token: 0x170002B6 RID: 694
			// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00027340 File Offset: 0x00025540
			// (set) Token: 0x06000B9F RID: 2975 RVA: 0x00027348 File Offset: 0x00025548
			internal string Key { get; set; }

			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00027351 File Offset: 0x00025551
			// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x00027359 File Offset: 0x00025559
			internal bool IsBinary { get; set; }

			// Token: 0x170002B8 RID: 696
			// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00027362 File Offset: 0x00025562
			// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x0002736A File Offset: 0x0002556A
			internal bool NoReply { get; set; }
		}
	}
}
