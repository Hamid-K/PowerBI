using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts
{
	// Token: 0x02000007 RID: 7
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RelayPacket
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		public static int HeaderSize
		{
			get
			{
				return RelayPacketHeader.Size;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020BA File Offset: 0x000002BA
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020C2 File Offset: 0x000002C2
		public RelayPacketHeader Header { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020CB File Offset: 0x000002CB
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020D3 File Offset: 0x000002D3
		public byte[] Blob
		{
			get
			{
				return this.m_blob;
			}
			set
			{
				this.m_blob = value;
				this.ExtractHeader();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020E2 File Offset: 0x000002E2
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020EA File Offset: 0x000002EA
		public bool RowsetPacketLimitReached { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020F3 File Offset: 0x000002F3
		public int PhysicalDataSize
		{
			get
			{
				return RelayPacket.GetPhysicalDataSize(this.LogicalDataSize);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002100 File Offset: 0x00000300
		public int LogicalDataSize
		{
			get
			{
				return this.Header.CompressedDataSize;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000210D File Offset: 0x0000030D
		public static int GetPhysicalDataSize(int logicalDataSize)
		{
			return logicalDataSize + RelayPacketHeader.Size;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002116 File Offset: 0x00000316
		public static int GetLogicalDataSize(int physicalDataSize)
		{
			return physicalDataSize - RelayPacketHeader.Size;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000211F File Offset: 0x0000031F
		public static int GetLogicalDataSize(byte[] packetBlob)
		{
			return RelayPacket.GetLogicalDataSize(packetBlob.Length);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002129 File Offset: 0x00000329
		private void ExtractHeader()
		{
			this.Header = RelayPacketHeader.Deserialize(this.Blob);
		}

		// Token: 0x04000011 RID: 17
		private byte[] m_blob;
	}
}
