using System;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001DA RID: 474
	internal abstract class GenericWriter
	{
		// Token: 0x06000F6C RID: 3948
		public abstract bool AreBytesAvailable(int bytes);

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000F6D RID: 3949
		public abstract int Position { get; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000F6E RID: 3950
		public abstract int Length { get; }

		// Token: 0x06000F6F RID: 3951
		protected abstract void WriteOneByte(byte value);

		// Token: 0x06000F70 RID: 3952
		protected abstract ArraySegment<byte> GetEmptyBuffer(int length);

		// Token: 0x06000F71 RID: 3953
		protected abstract void WriteBuffer(ArraySegment<byte> segment);

		// Token: 0x06000F72 RID: 3954 RVA: 0x00034D7B File Offset: 0x00032F7B
		protected GenericWriter(NumericBitWriter numericBitConverter)
		{
			this._numericBitConverter = numericBitConverter;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00034D8A File Offset: 0x00032F8A
		public void Write(byte value)
		{
			this.WriteOneByte(value);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00034D93 File Offset: 0x00032F93
		public void Write(sbyte value)
		{
			this.WriteOneByte((byte)value);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00034D9D File Offset: 0x00032F9D
		public void Write(bool value)
		{
			this.WriteOneByte(value ? 1 : 0);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00034DAC File Offset: 0x00032FAC
		public void Write(char value)
		{
			this.Write((short)value);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00034DB8 File Offset: 0x00032FB8
		public void Write(short value)
		{
			ArraySegment<byte> emptyBuffer = this.GetEmptyBuffer(2);
			this._numericBitConverter.FillBytes(value, emptyBuffer.Array, emptyBuffer.Offset);
			this.WriteBuffer(emptyBuffer);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00034DF0 File Offset: 0x00032FF0
		public void Write(ushort value)
		{
			ArraySegment<byte> emptyBuffer = this.GetEmptyBuffer(2);
			this._numericBitConverter.FillBytes(value, emptyBuffer.Array, emptyBuffer.Offset);
			this.WriteBuffer(emptyBuffer);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00034E28 File Offset: 0x00033028
		public void Write(int value)
		{
			ArraySegment<byte> emptyBuffer = this.GetEmptyBuffer(4);
			this._numericBitConverter.FillBytes(value, emptyBuffer.Array, emptyBuffer.Offset);
			this.WriteBuffer(emptyBuffer);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00034E60 File Offset: 0x00033060
		public void Write(uint value)
		{
			ArraySegment<byte> emptyBuffer = this.GetEmptyBuffer(4);
			this._numericBitConverter.FillBytes(value, emptyBuffer.Array, emptyBuffer.Offset);
			this.WriteBuffer(emptyBuffer);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00034E98 File Offset: 0x00033098
		public void Write(long value)
		{
			ArraySegment<byte> emptyBuffer = this.GetEmptyBuffer(8);
			this._numericBitConverter.FillBytes(value, emptyBuffer.Array, emptyBuffer.Offset);
			this.WriteBuffer(emptyBuffer);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00034ED0 File Offset: 0x000330D0
		public void Write(ulong value)
		{
			ArraySegment<byte> emptyBuffer = this.GetEmptyBuffer(8);
			this._numericBitConverter.FillBytes(value, emptyBuffer.Array, emptyBuffer.Offset);
			this.WriteBuffer(emptyBuffer);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00034F08 File Offset: 0x00033108
		public void Write(float value)
		{
			ArraySegment<byte> emptyBuffer = this.GetEmptyBuffer(4);
			this._numericBitConverter.FillBytes(value, emptyBuffer.Array, emptyBuffer.Offset);
			this.WriteBuffer(emptyBuffer);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00034F40 File Offset: 0x00033140
		public void Write(double value)
		{
			ArraySegment<byte> emptyBuffer = this.GetEmptyBuffer(8);
			this._numericBitConverter.FillBytes(value, emptyBuffer.Array, emptyBuffer.Offset);
			this.WriteBuffer(emptyBuffer);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00034F76 File Offset: 0x00033176
		public void Write(byte[] array)
		{
			this.Write(array, 0, array.Length);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00034F83 File Offset: 0x00033183
		public void Write(byte[] array, int offset, int length)
		{
			this.WriteBuffer(new ArraySegment<byte>(array, offset, length));
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00034F93 File Offset: 0x00033193
		public void Write(string value, Encoding encoding)
		{
			this.Write(encoding.GetBytes(value));
		}

		// Token: 0x04000A94 RID: 2708
		private readonly NumericBitWriter _numericBitConverter;
	}
}
