using System;
using System.IO;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000386 RID: 902
	internal class SerializationWriter : ISerializationWriter, IDisposable
	{
		// Token: 0x06001FBB RID: 8123 RVA: 0x00060AA4 File Offset: 0x0005ECA4
		public SerializationWriter(Stream stream)
		{
			this._outStream = new BinaryWriter(stream);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00060AB8 File Offset: 0x0005ECB8
		public void Write(bool value)
		{
			this._outStream.Write(value);
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00060AC6 File Offset: 0x0005ECC6
		public void Write(byte value)
		{
			this._outStream.Write(value);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00060AD4 File Offset: 0x0005ECD4
		public void Write(short value)
		{
			this._outStream.Write(value);
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00060AE2 File Offset: 0x0005ECE2
		public void Write(int value)
		{
			this._outStream.Write(value);
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00060AF0 File Offset: 0x0005ECF0
		public void Write(long value)
		{
			this._outStream.Write(value);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00060AFE File Offset: 0x0005ECFE
		public void Write(string value)
		{
			this._outStream.Write(value);
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x00060B0C File Offset: 0x0005ED0C
		public void Write(double value)
		{
			this._outStream.Write(value);
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00060B1A File Offset: 0x0005ED1A
		public void Write(byte[] buffer, int startIndex, int count)
		{
			this._outStream.Write(buffer, startIndex, count);
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00060B2A File Offset: 0x0005ED2A
		public void Write(byte[] buffer)
		{
			this._outStream.Write(buffer);
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x00060B38 File Offset: 0x0005ED38
		public void Write(byte[][] buffer)
		{
			int num = 0;
			if (buffer == null)
			{
				this._outStream.Write(num);
				return;
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				num += buffer[i].Length;
			}
			this._outStream.Write(num);
			for (int j = 0; j < buffer.Length; j++)
			{
				this._outStream.Write(buffer[j], 0, buffer[j].Length);
			}
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x00060B9B File Offset: 0x0005ED9B
		public void Write(ulong value)
		{
			this._outStream.Write(value);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x00060BA9 File Offset: 0x0005EDA9
		public void Write(uint value)
		{
			this._outStream.Write(value);
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x00060BB7 File Offset: 0x0005EDB7
		public void Write(ushort value)
		{
			this._outStream.Write(value);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x00060BC5 File Offset: 0x0005EDC5
		public void WriteEndMarker()
		{
			this._outStream.Write(byte.MaxValue);
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x00060BD7 File Offset: 0x0005EDD7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x00060BE6 File Offset: 0x0005EDE6
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._outStream.Close();
			}
		}

		// Token: 0x040012D8 RID: 4824
		private const byte EndMarker = 255;

		// Token: 0x040012D9 RID: 4825
		private BinaryWriter _outStream;
	}
}
