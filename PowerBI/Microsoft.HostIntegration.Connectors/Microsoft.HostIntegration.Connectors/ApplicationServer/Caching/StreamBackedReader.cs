using System;
using System.IO;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000389 RID: 905
	internal sealed class StreamBackedReader : GenericReader, IDisposable
	{
		// Token: 0x06001FE8 RID: 8168 RVA: 0x00060D1B File Offset: 0x0005EF1B
		public StreamBackedReader(Stream stream)
			: this(stream, true)
		{
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x00060D28 File Offset: 0x0005EF28
		public StreamBackedReader(Stream stream, bool ownsHandle)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead || !stream.CanSeek)
			{
				throw new ArgumentException("Stream passed in arguments is either not readable or seekable.", "stream");
			}
			this._stream = stream;
			this._isOwner = ownsHandle;
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x00060D84 File Offset: 0x0005EF84
		public string ReadString(int length, Encoding encoding, IBufferManager tempBufferAllocator)
		{
			byte[] array = tempBufferAllocator.TakeBuffer(length);
			string @string;
			try
			{
				this.GetNextData(new ArraySegment<byte>(array, 0, length));
				@string = encoding.GetString(array, 0, length);
			}
			finally
			{
				tempBufferAllocator.ReleaseBuffer(array);
			}
			return @string;
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x00060DCC File Offset: 0x0005EFCC
		public override bool AreBytesAvailable(int bytes)
		{
			long length = this._stream.Length;
			long position = this._stream.Position;
			return bytes > 0 && length >= position + (long)bytes;
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x00060E01 File Offset: 0x0005F001
		public override int Length
		{
			get
			{
				return (int)this._stream.Length;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x00060E0F File Offset: 0x0005F00F
		public override int Position
		{
			get
			{
				return (int)this._stream.Position;
			}
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x00060E20 File Offset: 0x0005F020
		protected override byte ReadOneByte()
		{
			int num = this._stream.ReadByte();
			if (num == -1)
			{
				throw new EndOfStreamException();
			}
			return (byte)num;
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x00060E48 File Offset: 0x0005F048
		protected override void GetNextData(ArraySegment<byte> segment)
		{
			int num = segment.Offset;
			int num2 = segment.Offset + segment.Count;
			for (;;)
			{
				int num3 = this._stream.Read(segment.Array, num, num2 - num);
				if (num3 == 0)
				{
					break;
				}
				num += num3;
				if (num >= num2)
				{
					return;
				}
			}
			throw new EndOfStreamException();
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00060E98 File Offset: 0x0005F098
		protected override ArraySegment<byte> GetNextData(int length)
		{
			ArraySegment<byte> arraySegment;
			if (length <= 8)
			{
				arraySegment = new ArraySegment<byte>(this._tempArray, 0, length);
			}
			else
			{
				arraySegment = new ArraySegment<byte>(new byte[length], 0, length);
			}
			this.GetNextData(arraySegment);
			return arraySegment;
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00060ED1 File Offset: 0x0005F0D1
		public void Dispose()
		{
			if (this._isOwner)
			{
				this._stream.Close();
			}
		}

		// Token: 0x040012DC RID: 4828
		private const int _tempArrayLength = 8;

		// Token: 0x040012DD RID: 4829
		private readonly Stream _stream;

		// Token: 0x040012DE RID: 4830
		private readonly bool _isOwner;

		// Token: 0x040012DF RID: 4831
		private readonly byte[] _tempArray = new byte[8];
	}
}
