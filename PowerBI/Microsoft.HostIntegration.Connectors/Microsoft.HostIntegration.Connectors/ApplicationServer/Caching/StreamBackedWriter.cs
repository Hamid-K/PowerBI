using System;
using System.IO;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200038A RID: 906
	internal sealed class StreamBackedWriter : GenericWriter, IDisposable
	{
		// Token: 0x06001FF2 RID: 8178 RVA: 0x00060EE6 File Offset: 0x0005F0E6
		public StreamBackedWriter(Stream stream)
			: this(stream, true)
		{
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x00060EF0 File Offset: 0x0005F0F0
		public StreamBackedWriter(Stream stream, bool ownsHandle)
			: base(NumericBitWriter.HostOrder)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite || !stream.CanSeek)
			{
				throw new ArgumentException("Stream passed in arguments is either not writable or seekable.", "stream");
			}
			this._stream = stream;
			this._isOwner = ownsHandle;
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00060F54 File Offset: 0x0005F154
		public override bool AreBytesAvailable(int bytes)
		{
			long length = this._stream.Length;
			long position = this._stream.Position;
			return bytes > 0 && length >= position + (long)bytes;
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x00060F89 File Offset: 0x0005F189
		public override int Length
		{
			get
			{
				return (int)this._stream.Length;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x00060F97 File Offset: 0x0005F197
		public override int Position
		{
			get
			{
				return (int)this._stream.Position;
			}
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x00060FA5 File Offset: 0x0005F1A5
		protected override void WriteOneByte(byte value)
		{
			this._stream.WriteByte(value);
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00060FB3 File Offset: 0x0005F1B3
		protected override ArraySegment<byte> GetEmptyBuffer(int length)
		{
			if (length <= 16)
			{
				return new ArraySegment<byte>(this._tempArray, 0, length);
			}
			return new ArraySegment<byte>(new byte[length], 0, length);
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00060FD5 File Offset: 0x0005F1D5
		protected override void WriteBuffer(ArraySegment<byte> segment)
		{
			this._stream.Write(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00060FF7 File Offset: 0x0005F1F7
		public void Dispose()
		{
			if (this._isOwner)
			{
				this._stream.Flush();
				this._stream.Close();
			}
		}

		// Token: 0x040012E0 RID: 4832
		private const int _tempArrayLength = 16;

		// Token: 0x040012E1 RID: 4833
		private readonly Stream _stream;

		// Token: 0x040012E2 RID: 4834
		private readonly bool _isOwner;

		// Token: 0x040012E3 RID: 4835
		private readonly byte[] _tempArray = new byte[16];
	}
}
