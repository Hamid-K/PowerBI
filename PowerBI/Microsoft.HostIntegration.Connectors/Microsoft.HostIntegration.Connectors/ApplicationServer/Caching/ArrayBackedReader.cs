using System;
using System.IO;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001D9 RID: 473
	internal sealed class ArrayBackedReader : GenericReader
	{
		// Token: 0x06000F62 RID: 3938 RVA: 0x00034B7F File Offset: 0x00032D7F
		public ArrayBackedReader(byte[] array)
			: this(array, 0, array.Length)
		{
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00034B8C File Offset: 0x00032D8C
		public ArrayBackedReader(byte[] array, int offset, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0 || offset >= array.Length)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset not within array bounds");
			}
			if (length <= 0 || length > array.Length - offset)
			{
				throw new ArgumentOutOfRangeException("length", offset, "Length not within array bounds");
			}
			this._array = array;
			this._offset = offset;
			this._minOffset = offset;
			this._maxOffset = offset + length;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00034C0D File Offset: 0x00032E0D
		public ArrayBackedReader(ArraySegment<byte> segment)
			: this(segment.Array, segment.Offset, segment.Count)
		{
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00034C2C File Offset: 0x00032E2C
		public void Skip(int byteCount)
		{
			if (this._offset + byteCount > this._maxOffset || this._offset + byteCount < this._minOffset)
			{
				throw new ArgumentOutOfRangeException("byteCount", byteCount, "Skip parameter goes beyong acceptable bounds.");
			}
			this._offset += byteCount;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00034C7D File Offset: 0x00032E7D
		public override bool AreBytesAvailable(int bytes)
		{
			return bytes > 0 && this._maxOffset >= this._offset + bytes;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00034C98 File Offset: 0x00032E98
		public override int Position
		{
			get
			{
				return this._offset - this._minOffset;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x00034CA7 File Offset: 0x00032EA7
		public override int Length
		{
			get
			{
				return this._maxOffset - this._minOffset;
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00034CB8 File Offset: 0x00032EB8
		protected override byte ReadOneByte()
		{
			return this._array[this._offset++];
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00034CE0 File Offset: 0x00032EE0
		protected override void GetNextData(ArraySegment<byte> segment)
		{
			if (!this.AreBytesAvailable(segment.Count))
			{
				throw new EndOfStreamException();
			}
			Array.Copy(this._array, this._offset, segment.Array, segment.Offset, segment.Count);
			this._offset += segment.Count;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00034D3C File Offset: 0x00032F3C
		protected override ArraySegment<byte> GetNextData(int length)
		{
			if (!this.AreBytesAvailable(length))
			{
				throw new EndOfStreamException();
			}
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(this._array, this._offset, length);
			this._offset += length;
			return arraySegment;
		}

		// Token: 0x04000A90 RID: 2704
		private readonly byte[] _array;

		// Token: 0x04000A91 RID: 2705
		private readonly int _minOffset;

		// Token: 0x04000A92 RID: 2706
		private readonly int _maxOffset;

		// Token: 0x04000A93 RID: 2707
		private int _offset;
	}
}
