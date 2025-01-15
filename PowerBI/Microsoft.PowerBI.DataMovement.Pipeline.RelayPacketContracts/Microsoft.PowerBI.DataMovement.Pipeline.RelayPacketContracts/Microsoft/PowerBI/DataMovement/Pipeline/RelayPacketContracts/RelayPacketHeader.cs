using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 21)]
	public sealed class RelayPacketHeader
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002144 File Offset: 0x00000344
		public static int Size
		{
			get
			{
				return Marshal.SizeOf(typeof(RelayPacketHeader));
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002155 File Offset: 0x00000355
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000215D File Offset: 0x0000035D
		public ControlFlags ControlFlags
		{
			get
			{
				return this.m_controlFlags;
			}
			set
			{
				this.m_controlFlags = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002166 File Offset: 0x00000366
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002173 File Offset: 0x00000373
		public bool IsLast
		{
			get
			{
				return (this.m_controlFlags & ControlFlags.EndOfData) == ControlFlags.EndOfData;
			}
			set
			{
				if (value)
				{
					this.m_controlFlags |= ControlFlags.EndOfData;
					return;
				}
				this.m_controlFlags &= ~ControlFlags.EndOfData;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002199 File Offset: 0x00000399
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000021A6 File Offset: 0x000003A6
		public bool HasTelemetry
		{
			get
			{
				return (this.m_controlFlags & ControlFlags.HasTelemetry) == ControlFlags.HasTelemetry;
			}
			set
			{
				if (value)
				{
					this.m_controlFlags |= ControlFlags.HasTelemetry;
					return;
				}
				this.m_controlFlags &= ~ControlFlags.HasTelemetry;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021CC File Offset: 0x000003CC
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000021D9 File Offset: 0x000003D9
		public bool HasCorrectDataSize
		{
			get
			{
				return (this.m_controlFlags & ControlFlags.HasCorrectDataSize) == ControlFlags.HasCorrectDataSize;
			}
			set
			{
				if (value)
				{
					this.m_controlFlags |= ControlFlags.HasCorrectDataSize;
					return;
				}
				this.m_controlFlags &= ~ControlFlags.HasCorrectDataSize;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021FF File Offset: 0x000003FF
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002207 File Offset: 0x00000407
		public int Index
		{
			get
			{
				return this.m_index;
			}
			set
			{
				this.m_index = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002210 File Offset: 0x00000410
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002218 File Offset: 0x00000418
		public int UncompressedDataSize
		{
			get
			{
				return this.m_uncompressedDataSize;
			}
			set
			{
				this.m_uncompressedDataSize = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002221 File Offset: 0x00000421
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002229 File Offset: 0x00000429
		public int CompressedDataSize
		{
			get
			{
				return this.m_compressedDataSize;
			}
			set
			{
				this.m_compressedDataSize = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002232 File Offset: 0x00000432
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000223A File Offset: 0x0000043A
		public XPress9Level CompressionAlgorithm
		{
			get
			{
				return this.m_compressionAlgorithm;
			}
			set
			{
				this.m_compressionAlgorithm = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002243 File Offset: 0x00000443
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000224B File Offset: 0x0000044B
		public DeserializationDirective DeserializationDirective
		{
			get
			{
				return this.m_deserializationDirective;
			}
			set
			{
				this.m_deserializationDirective = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002254 File Offset: 0x00000454
		public bool IsInfra
		{
			get
			{
				return this.Index == -1;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000225F File Offset: 0x0000045F
		public bool IsError
		{
			get
			{
				return this.IsInfra && this.IsLast;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002271 File Offset: 0x00000471
		public bool IsTelemetry
		{
			get
			{
				return this.IsInfra && this.HasTelemetry && !this.IsLast;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002290 File Offset: 0x00000490
		public void Serialize(byte[] packet)
		{
			GCHandle gchandle = GCHandle.Alloc(packet, GCHandleType.Pinned);
			Marshal.StructureToPtr<RelayPacketHeader>(this, gchandle.AddrOfPinnedObject(), false);
			gchandle.Free();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000022BC File Offset: 0x000004BC
		public static RelayPacketHeader Deserialize(byte[] packet)
		{
			GCHandle gchandle = GCHandle.Alloc(packet, GCHandleType.Pinned);
			RelayPacketHeader relayPacketHeader = (RelayPacketHeader)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(RelayPacketHeader));
			gchandle.Free();
			return relayPacketHeader;
		}

		// Token: 0x0400001D RID: 29
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.I1)]
		private ControlFlags m_controlFlags;

		// Token: 0x0400001E RID: 30
		[FieldOffset(1)]
		private int m_index;

		// Token: 0x0400001F RID: 31
		[FieldOffset(5)]
		private int m_uncompressedDataSize;

		// Token: 0x04000020 RID: 32
		[FieldOffset(9)]
		private int m_compressedDataSize;

		// Token: 0x04000021 RID: 33
		[FieldOffset(13)]
		[MarshalAs(UnmanagedType.I4)]
		private XPress9Level m_compressionAlgorithm;

		// Token: 0x04000022 RID: 34
		[FieldOffset(17)]
		[MarshalAs(UnmanagedType.I4)]
		private DeserializationDirective m_deserializationDirective;
	}
}
