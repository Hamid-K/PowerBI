using System;
using System.IO;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001DB RID: 475
	internal sealed class ArrayBackedWriter : GenericWriter
	{
		// Token: 0x06000F82 RID: 3970 RVA: 0x00034FA2 File Offset: 0x000331A2
		public ArrayBackedWriter(byte[] array)
			: this(array, 0, array.Length)
		{
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00034FB0 File Offset: 0x000331B0
		public ArrayBackedWriter(byte[] array, int offset, int length)
			: base(NumericBitWriter.HostOrder)
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

		// Token: 0x06000F84 RID: 3972 RVA: 0x00035036 File Offset: 0x00033236
		public ArrayBackedWriter(ArraySegment<byte> segment)
			: this(segment.Array, segment.Offset, segment.Count)
		{
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00035053 File Offset: 0x00033253
		public override bool AreBytesAvailable(int bytes)
		{
			return bytes > 0 && this._maxOffset >= this._offset + bytes;
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0003506E File Offset: 0x0003326E
		public override int Position
		{
			get
			{
				return this._offset - this._minOffset;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003507D File Offset: 0x0003327D
		public override int Length
		{
			get
			{
				return this._maxOffset - this._minOffset;
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0003508C File Offset: 0x0003328C
		protected override void WriteOneByte(byte value)
		{
			this._array[this._offset++] = value;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x000350B2 File Offset: 0x000332B2
		protected override ArraySegment<byte> GetEmptyBuffer(int length)
		{
			if (!this.AreBytesAvailable(length))
			{
				throw new EndOfStreamException();
			}
			return new ArraySegment<byte>(this._array, this._offset, length);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000350D8 File Offset: 0x000332D8
		protected override void WriteBuffer(ArraySegment<byte> segment)
		{
			if (segment.Array != this._array || segment.Offset != this._offset)
			{
				Buffer.BlockCopy(segment.Array, segment.Offset, this._array, this._offset, segment.Count);
			}
			this._offset += segment.Count;
		}

		// Token: 0x04000A95 RID: 2709
		private readonly byte[] _array;

		// Token: 0x04000A96 RID: 2710
		private readonly int _minOffset;

		// Token: 0x04000A97 RID: 2711
		private readonly int _maxOffset;

		// Token: 0x04000A98 RID: 2712
		private int _offset;
	}
}
