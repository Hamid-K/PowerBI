using System;
using System.IO;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000388 RID: 904
	internal class SerializationReader : ISerializationReader, IDisposable
	{
		// Token: 0x06001FD8 RID: 8152 RVA: 0x00060BF6 File Offset: 0x0005EDF6
		public SerializationReader(Stream stream)
		{
			this._inStream = new BinaryReader(stream);
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x00060C0A File Offset: 0x0005EE0A
		public bool ReadBoolean()
		{
			return this._inStream.ReadBoolean();
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x00060C17 File Offset: 0x0005EE17
		public byte ReadByte()
		{
			return this._inStream.ReadByte();
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00060C24 File Offset: 0x0005EE24
		public short ReadInt16()
		{
			return this._inStream.ReadInt16();
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x00060C31 File Offset: 0x0005EE31
		public int ReadInt32()
		{
			return this._inStream.ReadInt32();
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x00060C3E File Offset: 0x0005EE3E
		public long ReadInt64()
		{
			return this._inStream.ReadInt64();
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x00060C4B File Offset: 0x0005EE4B
		public string ReadString()
		{
			return this._inStream.ReadString();
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x00060C58 File Offset: 0x0005EE58
		public int Read(byte[] buffer, int startIndex, int count)
		{
			return this._inStream.Read(buffer, startIndex, count);
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x00060C68 File Offset: 0x0005EE68
		public ushort ReadUInt16()
		{
			return this._inStream.ReadUInt16();
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x00060C75 File Offset: 0x0005EE75
		public uint ReadUInt32()
		{
			return this._inStream.ReadUInt32();
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x00060C82 File Offset: 0x0005EE82
		public ulong ReadUInt64()
		{
			return this._inStream.ReadUInt64();
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x00060C8F File Offset: 0x0005EE8F
		public double ReadDouble()
		{
			return this._inStream.ReadDouble();
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x00060C9C File Offset: 0x0005EE9C
		public byte[][] ReadChunkedByteArray()
		{
			byte[][] array = null;
			int num = this._inStream.ReadInt32();
			if (num > 0)
			{
				using (ChunkStream chunkStream = new ChunkStream(this, num))
				{
					array = chunkStream.ToChunkedArray();
				}
			}
			return array;
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x00060CE8 File Offset: 0x0005EEE8
		public bool IsCurrentEndMarker()
		{
			return this._inStream.ReadByte() == byte.MaxValue;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x00060CFC File Offset: 0x0005EEFC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x00060D0B File Offset: 0x0005EF0B
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._inStream.Close();
			}
		}

		// Token: 0x040012DA RID: 4826
		private const byte EndMarker = 255;

		// Token: 0x040012DB RID: 4827
		private BinaryReader _inStream;
	}
}
