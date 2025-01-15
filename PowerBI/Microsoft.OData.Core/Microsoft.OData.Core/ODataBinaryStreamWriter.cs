using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.OData.Buffers;
using Microsoft.OData.Json;

namespace Microsoft.OData
{
	// Token: 0x0200000F RID: 15
	internal sealed class ODataBinaryStreamWriter : Stream
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00003278 File Offset: 0x00001478
		public ODataBinaryStreamWriter(TextWriter writer)
		{
			this.Writer = writer;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003293 File Offset: 0x00001493
		public ODataBinaryStreamWriter(TextWriter writer, ref char[] streamingBuffer, ICharArrayPool bufferPool)
		{
			this.Writer = writer;
			this.streamingBuffer = streamingBuffer;
			this.bufferPool = bufferPool;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002393 File Offset: 0x00000593
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000032BD File Offset: 0x000014BD
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000032BD File Offset: 0x000014BD
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000032BD File Offset: 0x000014BD
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000032C4 File Offset: 0x000014C4
		public override void Write(byte[] bytes, int offset, int count)
		{
			byte[] array = ODataBinaryStreamWriter.emptyByteArray;
			int num = this.trailingBytes.Length;
			int num2 = ((num > 0) ? (3 - num) : 0);
			if (count + num < 3)
			{
				this.trailingBytes = this.trailingBytes.Concat(bytes.Skip(offset).Take(count)).ToArray<byte>();
				return;
			}
			if (num > 0)
			{
				array = this.trailingBytes.Concat(bytes.Skip(offset).Take(num2)).ToArray<byte>();
			}
			int num3 = (count - num2) % 3;
			this.trailingBytes = bytes.Skip(offset + count - num3).Take(num3).ToArray<byte>();
			JsonValueUtils.WriteBinaryString(this.Writer, array.Concat(bytes.Skip(offset + num2).Take(count - num2 - num3)).ToArray<byte>(), ref this.streamingBuffer, this.bufferPool);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003390 File Offset: 0x00001590
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.Write(buffer, offset, count);
			});
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000032BD File Offset: 0x000014BD
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000032BD File Offset: 0x000014BD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000032BD File Offset: 0x000014BD
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000033D0 File Offset: 0x000015D0
		public override void Flush()
		{
			this.Writer.Flush();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000033E0 File Offset: 0x000015E0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.trailingBytes != null && this.trailingBytes.Length != 0)
			{
				this.Writer.Write(Convert.ToBase64String(this.trailingBytes, 0, this.trailingBytes.Length));
				this.trailingBytes = null;
			}
			this.Writer.Flush();
			base.Dispose(disposing);
		}

		// Token: 0x04000023 RID: 35
		private readonly TextWriter Writer;

		// Token: 0x04000024 RID: 36
		private byte[] trailingBytes = new byte[0];

		// Token: 0x04000025 RID: 37
		private char[] streamingBuffer;

		// Token: 0x04000026 RID: 38
		private ICharArrayPool bufferPool;

		// Token: 0x04000027 RID: 39
		private static byte[] emptyByteArray = new byte[0];
	}
}
