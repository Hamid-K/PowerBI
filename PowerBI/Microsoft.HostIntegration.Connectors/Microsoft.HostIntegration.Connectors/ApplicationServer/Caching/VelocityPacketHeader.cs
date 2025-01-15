using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200017C RID: 380
	internal struct VelocityPacketHeader
	{
		// Token: 0x06000C08 RID: 3080 RVA: 0x00028340 File Offset: 0x00026540
		internal void WriteHeader(byte[] array, int offset)
		{
			ArrayBackedWriter arrayBackedWriter = new ArrayBackedWriter(array, offset, 32);
			arrayBackedWriter.Write(this._magicByte);
			arrayBackedWriter.Write((byte)this._packetType);
			arrayBackedWriter.Write(this._responseCode);
			arrayBackedWriter.Write(this._messageID);
			arrayBackedWriter.Write(this._payloadLength);
			arrayBackedWriter.Write((byte)this._headerFlags);
			arrayBackedWriter.Write(this._cacheNameLength);
			arrayBackedWriter.Write(this._extrasLength);
			arrayBackedWriter.Write((int)this._extrasFlags);
			arrayBackedWriter.Write(this._regionLength);
			arrayBackedWriter.Write(this._keyLength);
			arrayBackedWriter.Write(this._valueLength);
			arrayBackedWriter.Write(this.ValueFlags);
			VelocityWireProtocol.TraceBytesOnVerbose("VelocityPacketHeader.WriteHeader", array, 0, 32, this);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002840C File Offset: 0x0002660C
		internal void ReadHeader(byte[] array, int offset)
		{
			ArrayBackedReader arrayBackedReader = new ArrayBackedReader(array, offset, 32);
			this._magicByte = arrayBackedReader.ReadByte();
			this._packetType = (VelocityPacketType)arrayBackedReader.ReadByte();
			this._responseCode = arrayBackedReader.ReadUInt16();
			this._messageID = arrayBackedReader.ReadInt32();
			this._payloadLength = arrayBackedReader.ReadUInt32();
			this._headerFlags = (VelocityPacketHeaderFlags)arrayBackedReader.ReadByte();
			this._cacheNameLength = arrayBackedReader.ReadByte();
			this._extrasLength = arrayBackedReader.ReadUInt16();
			this._extrasFlags = (VelocityPacketExtrasFlags)arrayBackedReader.ReadInt32();
			this._regionLength = arrayBackedReader.ReadUInt16();
			this._keyLength = arrayBackedReader.ReadUInt16();
			this._valueLength = arrayBackedReader.ReadUInt32();
			arrayBackedReader.ReadBytes(this.ValueFlags, 0, 4);
			VelocityWireProtocol.TraceBytesOnVerbose("VelocityPacketHeader.ReadHeader", array, 0, 32, this);
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x000284DA File Offset: 0x000266DA
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x000284E2 File Offset: 0x000266E2
		public byte MagicByte
		{
			get
			{
				return this._magicByte;
			}
			set
			{
				this._magicByte = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x000284EB File Offset: 0x000266EB
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x000284F3 File Offset: 0x000266F3
		public VelocityPacketType PacketType
		{
			get
			{
				return this._packetType;
			}
			set
			{
				this._packetType = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x000284FC File Offset: 0x000266FC
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x00028504 File Offset: 0x00026704
		public ErrStatus ResponseCode
		{
			get
			{
				return (ErrStatus)this._responseCode;
			}
			set
			{
				this._responseCode = checked((ushort)value);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0002850E File Offset: 0x0002670E
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x00028516 File Offset: 0x00026716
		public int MessageID
		{
			get
			{
				return this._messageID;
			}
			set
			{
				this._messageID = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0002851F File Offset: 0x0002671F
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x00028527 File Offset: 0x00026727
		public uint PayloadLength
		{
			get
			{
				return this._payloadLength;
			}
			set
			{
				this._payloadLength = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00028530 File Offset: 0x00026730
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x00028538 File Offset: 0x00026738
		public VelocityPacketHeaderFlags HeaderFlags
		{
			get
			{
				return this._headerFlags;
			}
			set
			{
				this._headerFlags = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00028541 File Offset: 0x00026741
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x00028549 File Offset: 0x00026749
		public byte CacheNameLength
		{
			get
			{
				return this._cacheNameLength;
			}
			set
			{
				this._cacheNameLength = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x00028552 File Offset: 0x00026752
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x0002855A File Offset: 0x0002675A
		public ushort ExtrasLength
		{
			get
			{
				return this._extrasLength;
			}
			set
			{
				this._extrasLength = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00028563 File Offset: 0x00026763
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x0002856B File Offset: 0x0002676B
		public VelocityPacketExtrasFlags ExtrasFlags
		{
			get
			{
				return this._extrasFlags;
			}
			set
			{
				this._extrasFlags = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00028574 File Offset: 0x00026774
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x0002857C File Offset: 0x0002677C
		public ushort RegionLength
		{
			get
			{
				return this._regionLength;
			}
			set
			{
				this._regionLength = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00028585 File Offset: 0x00026785
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x0002858D File Offset: 0x0002678D
		public ushort KeyLength
		{
			get
			{
				return this._keyLength;
			}
			set
			{
				this._keyLength = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00028596 File Offset: 0x00026796
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x0002859E File Offset: 0x0002679E
		public uint ValueLength
		{
			get
			{
				return this._valueLength;
			}
			set
			{
				this._valueLength = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x000285A7 File Offset: 0x000267A7
		public byte[] ValueFlags
		{
			get
			{
				if (this._valueFlags == null)
				{
					this._valueFlags = new byte[4];
				}
				return this._valueFlags;
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000285C4 File Offset: 0x000267C4
		internal bool HasValueFlagsSet()
		{
			byte[] valueFlags = this.ValueFlags;
			return valueFlags[0] != 0 || valueFlags[1] != 0 || valueFlags[2] != 0 || valueFlags[3] != 0;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000285F4 File Offset: 0x000267F4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "VelocityPacketHeader: {0:X2}, Op: {1}, Err: {2} | ID: {3} | PL: {4} | HF: 0x{5:X}, CL: {6}, EL: {7} | EF: {8} (0x{8:X}) | RL: {9}, KL: {10} | VL: {11} | VF: {12:X2} {13:X2} {14:X2} {15:X2}", new object[]
			{
				this._magicByte,
				this._packetType,
				this._responseCode,
				this._messageID,
				this._payloadLength,
				this._headerFlags,
				this._cacheNameLength,
				this._extrasLength,
				this._extrasFlags,
				this._regionLength,
				this._keyLength,
				this._valueLength,
				this.ValueFlags[0],
				this.ValueFlags[1],
				this.ValueFlags[2],
				this.ValueFlags[3]
			});
		}

		// Token: 0x04000886 RID: 2182
		internal const int HeaderSize = 32;

		// Token: 0x04000887 RID: 2183
		internal const int ValueFlagsSize = 4;

		// Token: 0x04000888 RID: 2184
		private byte _magicByte;

		// Token: 0x04000889 RID: 2185
		private VelocityPacketType _packetType;

		// Token: 0x0400088A RID: 2186
		private ushort _responseCode;

		// Token: 0x0400088B RID: 2187
		private int _messageID;

		// Token: 0x0400088C RID: 2188
		private uint _payloadLength;

		// Token: 0x0400088D RID: 2189
		private VelocityPacketHeaderFlags _headerFlags;

		// Token: 0x0400088E RID: 2190
		private byte _cacheNameLength;

		// Token: 0x0400088F RID: 2191
		private ushort _extrasLength;

		// Token: 0x04000890 RID: 2192
		private VelocityPacketExtrasFlags _extrasFlags;

		// Token: 0x04000891 RID: 2193
		private ushort _regionLength;

		// Token: 0x04000892 RID: 2194
		private ushort _keyLength;

		// Token: 0x04000893 RID: 2195
		private uint _valueLength;

		// Token: 0x04000894 RID: 2196
		private byte[] _valueFlags;
	}
}
