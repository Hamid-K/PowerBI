using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000177 RID: 375
	internal class VelocityPacket : IVelocityRequestPacket, IVelocityResponsePacket, IVelocityPacket
	{
		// Token: 0x06000BC5 RID: 3013 RVA: 0x00027860 File Offset: 0x00025A60
		public VelocityPacket()
		{
			this.CacheName = "default";
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00027896 File Offset: 0x00025A96
		internal void ReadHeader(byte[] array)
		{
			this._packetHeader.ReadHeader(array, 0);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000278A5 File Offset: 0x00025AA5
		internal void WriteHeader(byte[] array)
		{
			this._packetHeader.WriteHeader(array, 0);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x000278B4 File Offset: 0x00025AB4
		internal void ReadPayload(byte[] array, int offset)
		{
			checked
			{
				long num = unchecked((long)(checked((ushort)this._packetHeader.CacheNameLength + this._packetHeader.ExtrasLength + this._packetHeader.RegionLength + this._packetHeader.KeyLength))) + (long)(unchecked((ulong)this._packetHeader.ValueLength));
				if (num > (long)(unchecked((ulong)this._packetHeader.PayloadLength)))
				{
					throw new VelocityPacketFormatFatalException("Inconsistent packet lengths: {0} + {1} + {2} + {3} + {4} > {5}", new object[]
					{
						this._packetHeader.CacheNameLength,
						this._packetHeader.ExtrasLength,
						this._packetHeader.RegionLength,
						this._packetHeader.KeyLength,
						this._packetHeader.ValueLength,
						this._packetHeader.PayloadLength
					});
				}
				if (this._packetHeader.CacheNameLength > 0)
				{
					this.CacheName = VelocityPacket.TextEncoding.GetString(array, offset, (int)this._packetHeader.CacheNameLength);
					offset += (int)this._packetHeader.CacheNameLength;
				}
				else
				{
					this.CacheName = "default";
				}
				if (this._packetHeader.ExtrasLength > 0 || this._packetHeader.ExtrasFlags != VelocityPacketExtrasFlags.None)
				{
					this._packetExtras.ReadFrom(this._packetHeader.ExtrasFlags, array, offset, (int)this._packetHeader.ExtrasLength, this.CacheName);
					offset += (int)this._packetHeader.ExtrasLength;
				}
				if (this._packetHeader.RegionLength > 0)
				{
					this.Region = VelocityPacket.TextEncoding.GetString(array, offset, (int)this._packetHeader.RegionLength);
					offset += (int)this._packetHeader.RegionLength;
				}
				if (this._packetHeader.KeyLength > 0)
				{
					this.Key = VelocityPacket.TextEncoding.GetString(array, offset, (int)this._packetHeader.KeyLength);
					offset += (int)this._packetHeader.KeyLength;
				}
				if (this._packetHeader.ValueLength > 0U || this._packetHeader.HasValueFlagsSet())
				{
					int num2 = (int)this._packetHeader.ValueLength;
					if (!this.HasHeaderFlagSet(VelocityPacketHeaderFlags.MemcacheProtocol))
					{
						this.Value = ValueFlagsUtility.GetChunkedArray(this._packetHeader.ValueFlags, new ArraySegment<byte>(array, offset, num2), this.MagicByte == 193);
					}
					else if (num2 > 0)
					{
						using (ChunkStream chunkStream = new ChunkStream(num2))
						{
							chunkStream.Write(array, offset, num2);
							this.Value = chunkStream.ToChunkedArray();
						}
					}
				}
				long num3 = (long)(unchecked((ulong)this._packetHeader.PayloadLength) - (ulong)num);
				if (num3 > 0L)
				{
					if (num > 2147483647L || num3 > 2147483647L)
					{
						throw new VelocityPacketFormatFatalException("Property bag too large: offset {0}, length {1}", new object[] { num, num3 });
					}
					this._propertyBag.FromByteArray(array, (int)num, (int)num3);
				}
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00027BA8 File Offset: 0x00025DA8
		internal IList<ArraySegment<byte>> GetPayloadSegments(IBufferManager bufferManager, out IList<AllocationType> allocationType)
		{
			bool flag = false;
			if ((ulong)this._packetHeader.ValueLength <= 4096UL)
			{
				flag = true;
			}
			IList<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
			checked
			{
				byte[] array;
				if (flag)
				{
					int num = 32 + (int)this.PayloadLength;
					array = bufferManager.TakeBuffer(num);
					list = new List<ArraySegment<byte>>(1);
					list.Add(new ArraySegment<byte>(array, 0, num));
				}
				else
				{
					int num2 = (int)((ushort)(32 + this._packetHeader.CacheNameLength) + this._packetHeader.ExtrasLength + this._packetHeader.RegionLength + this._packetHeader.KeyLength);
					array = bufferManager.TakeBuffer(num2);
					list = new List<ArraySegment<byte>>(2 + this.Value.Length);
					list.Add(new ArraySegment<byte>(array, 0, num2));
				}
				allocationType = new List<AllocationType>(list.Count);
				allocationType.Add(AllocationType.BufferManager);
				this.WriteHeader(array);
				int num3 = 32;
				if (this.CacheName != "default")
				{
					VelocityPacket.WriteString(this.CacheName, array, ref num3, (int)this._packetHeader.CacheNameLength);
				}
				this._packetExtras.WriteTo(array, ref num3);
				VelocityPacket.WriteString(this.Region, array, ref num3, (int)this._packetHeader.RegionLength);
				VelocityPacket.WriteString(this.Key, array, ref num3, (int)this._packetHeader.KeyLength);
				int num4 = num3;
				int num5 = (this.HasHeaderFlagSet(VelocityPacketHeaderFlags.MemcacheProtocol) ? 0 : 4);
				if (this.Value != null)
				{
					foreach (byte[] array2 in this.Value)
					{
						if (array2.Length > num5)
						{
							if (flag)
							{
								Array.Copy(array2, num5, array, num3, array2.Length - num5);
								num3 += array2.Length - num5;
							}
							else
							{
								list.Add(new ArraySegment<byte>(array2, num5, array2.Length - num5));
								allocationType.Add(AllocationType.DirectHeap);
							}
							num5 = 0;
						}
						else
						{
							num5 -= array2.Length;
						}
					}
				}
				if (flag)
				{
					this._propertyBag.Write(array, ref num3);
				}
				else
				{
					int num6 = 32 + (int)(this._packetHeader.PayloadLength - this._packetHeader.ValueLength) - num4;
					if (num6 > 0)
					{
						byte[] array3 = bufferManager.TakeBuffer(num6);
						num3 = 0;
						this._propertyBag.Write(array3, ref num3);
						list.Add(new ArraySegment<byte>(array3, 0, num6));
						allocationType.Add(AllocationType.BufferManager);
					}
				}
				return list;
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00027DF0 File Offset: 0x00025FF0
		private static void WriteString(string str, byte[] array, ref int offset, int verifyLength)
		{
			if (!string.IsNullOrEmpty(str))
			{
				int bytes = VelocityPacket.TextEncoding.GetBytes(str, 0, str.Length, array, offset);
				offset += bytes;
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00027E24 File Offset: 0x00026024
		internal void PrepareHeaderForWrite()
		{
			checked
			{
				if (this.CacheName != null && this.CacheName != "default")
				{
					this._packetHeader.CacheNameLength = (byte)VelocityPacket.TextEncoding.GetByteCount(this.CacheName);
				}
				else
				{
					this._packetHeader.CacheNameLength = 0;
				}
				VelocityPacketExtrasFlags velocityPacketExtrasFlags;
				this._packetHeader.ExtrasLength = (ushort)this._packetExtras.GetLength(out velocityPacketExtrasFlags);
				this._packetHeader.ExtrasFlags = velocityPacketExtrasFlags;
				if (this.Region != null)
				{
					int byteCount = VelocityPacket.TextEncoding.GetByteCount(this.Region);
					if (byteCount > VelocityWireProtocol.MaxRegionLength)
					{
						throw Utility.CreateClientException("VelocityPacket", 38, 14, null);
					}
					this._packetHeader.RegionLength = (ushort)byteCount;
				}
				else
				{
					this._packetHeader.RegionLength = 0;
				}
				if (this.Key != null)
				{
					int byteCount2 = VelocityPacket.TextEncoding.GetByteCount(this.Key);
					if (byteCount2 > VelocityWireProtocol.MaxKeyLength)
					{
						throw Utility.CreateClientException("VelocityPacket", 38, 13, null);
					}
					this._packetHeader.KeyLength = (ushort)byteCount2;
				}
				else
				{
					this._packetHeader.KeyLength = 0;
				}
				if (this.Value != null)
				{
					uint num = (uint)Utility.Get2DByteArraySize(this.Value);
					if (this.HasHeaderFlagSet(VelocityPacketHeaderFlags.MemcacheProtocol))
					{
						Array.Clear(this._packetHeader.ValueFlags, 0, 4);
						this._packetHeader.ValueLength = num;
					}
					else
					{
						using (ChunkStream chunkStream = new ChunkStream(this.Value, false))
						{
							chunkStream.Read(this._packetHeader.ValueFlags, 0, 4);
						}
						this._packetHeader.ValueLength = num - 4U;
					}
				}
				else
				{
					Array.Clear(this._packetHeader.ValueFlags, 0, 4);
					this._packetHeader.ValueLength = 0U;
				}
				this._packetHeader.PayloadLength = (uint)(unchecked((long)(checked((ushort)this._packetHeader.CacheNameLength + this._packetHeader.ExtrasLength + this._packetHeader.RegionLength + this._packetHeader.KeyLength))) + (long)(unchecked((ulong)this._packetHeader.ValueLength)) + (long)(unchecked((ulong)this._propertyBag.GetLength())));
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00028038 File Offset: 0x00026238
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x00028045 File Offset: 0x00026245
		internal uint PayloadLength
		{
			get
			{
				return this._packetHeader.PayloadLength;
			}
			set
			{
				this._packetHeader.PayloadLength = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00028053 File Offset: 0x00026253
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x00028060 File Offset: 0x00026260
		public byte MagicByte
		{
			get
			{
				return this._packetHeader.MagicByte;
			}
			set
			{
				this._packetHeader.MagicByte = value;
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002806E File Offset: 0x0002626E
		internal bool HasHeaderFlagSet(VelocityPacketHeaderFlags flag)
		{
			return (byte)(this.HeaderFlags & flag) != 0;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0002807F File Offset: 0x0002627F
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x0002808C File Offset: 0x0002628C
		public VelocityPacketType MessageType
		{
			get
			{
				return this._packetHeader.PacketType;
			}
			set
			{
				this._packetHeader.PacketType = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0002809A File Offset: 0x0002629A
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x000280A7 File Offset: 0x000262A7
		public ErrStatus ResponseCode
		{
			get
			{
				return this._packetHeader.ResponseCode;
			}
			set
			{
				this._packetHeader.ResponseCode = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x000280B5 File Offset: 0x000262B5
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x000280C2 File Offset: 0x000262C2
		public int MessageID
		{
			get
			{
				return this._packetHeader.MessageID;
			}
			set
			{
				this._packetHeader.MessageID = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x000280D0 File Offset: 0x000262D0
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x000280DD File Offset: 0x000262DD
		public VelocityPacketHeaderFlags HeaderFlags
		{
			get
			{
				return this._packetHeader.HeaderFlags;
			}
			set
			{
				this._packetHeader.HeaderFlags = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x000280EB File Offset: 0x000262EB
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x000280F8 File Offset: 0x000262F8
		public DataCacheItemVersion Version
		{
			get
			{
				return this._packetExtras.Version;
			}
			set
			{
				this._packetExtras.Version = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00028106 File Offset: 0x00026306
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0002811B File Offset: 0x0002631B
		public bool IsMemcacheProtocol
		{
			get
			{
				return (byte)(this._packetHeader.HeaderFlags & VelocityPacketHeaderFlags.MemcacheProtocol) == 64;
			}
			set
			{
				if (value)
				{
					this._packetHeader.HeaderFlags = this._packetHeader.HeaderFlags | VelocityPacketHeaderFlags.MemcacheProtocol;
					return;
				}
				this._packetHeader.HeaderFlags = this._packetHeader.HeaderFlags & ~VelocityPacketHeaderFlags.MemcacheProtocol;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0002814E File Offset: 0x0002634E
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0002815B File Offset: 0x0002635B
		public int CacheItemMask
		{
			get
			{
				return this._packetExtras.CacheItemMask;
			}
			set
			{
				this._packetExtras.CacheItemMask = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00028169 File Offset: 0x00026369
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x00028176 File Offset: 0x00026376
		public uint? ExpiryTTL
		{
			get
			{
				return this._packetExtras.ExpiryTTL;
			}
			set
			{
				this._packetExtras.ExpiryTTL = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00028184 File Offset: 0x00026384
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x00028191 File Offset: 0x00026391
		public DataCacheLockHandle LockHandle
		{
			get
			{
				return this._packetExtras.LockHandle;
			}
			set
			{
				this._packetExtras.LockHandle = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002819F File Offset: 0x0002639F
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x000281AC File Offset: 0x000263AC
		public PartitionId PartitionKey
		{
			get
			{
				return this._packetExtras.PartitionKey;
			}
			set
			{
				this._packetExtras.PartitionKey = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x000281BA File Offset: 0x000263BA
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x000281C2 File Offset: 0x000263C2
		public string CacheName { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000281CB File Offset: 0x000263CB
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x000281D3 File Offset: 0x000263D3
		public string Region { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x000281DC File Offset: 0x000263DC
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x000281E4 File Offset: 0x000263E4
		public string Key { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x000281ED File Offset: 0x000263ED
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x000281F5 File Offset: 0x000263F5
		public byte[][] Value { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x000281FE File Offset: 0x000263FE
		public IVelocityPacketPropertyBag PropertyBag
		{
			get
			{
				return this._propertyBag;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00003CAB File Offset: 0x00001EAB
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x00003CAB File Offset: 0x00001EAB
		public object Opaque
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00003CAB File Offset: 0x00001EAB
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x00003CAB File Offset: 0x00001EAB
		public IList<string> Keys
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00003CAB File Offset: 0x00001EAB
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00003CAB File Offset: 0x00001EAB
		public bool IsEmptyPacket
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00002B16 File Offset: 0x00000D16
		public TcpPacketSendTypes SendType
		{
			get
			{
				return TcpPacketSendTypes.Immediate;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00028206 File Offset: 0x00026406
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x0002820E File Offset: 0x0002640E
		public Exception Exception { get; set; }

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00028218 File Offset: 0x00026418
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "VelocityPacket:\r\n {0},\r\n {1},\r\n Cache: {2}, Rgn: {3}, Key: {4}, Value exists: {5}", new object[]
			{
				this._packetHeader,
				this._packetExtras,
				this.CacheName,
				this.Region,
				this.Key,
				this.Value != null
			});
		}

		// Token: 0x04000876 RID: 2166
		private static readonly Encoding TextEncoding = new UTF8Encoding(false, false);

		// Token: 0x04000877 RID: 2167
		private VelocityPacketHeader _packetHeader = default(VelocityPacketHeader);

		// Token: 0x04000878 RID: 2168
		private VelocityPacketExtras _packetExtras = default(VelocityPacketExtras);

		// Token: 0x04000879 RID: 2169
		private readonly VelocityPacketPropertyBag _propertyBag = new VelocityPacketPropertyBag();
	}
}
