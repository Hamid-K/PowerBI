using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x0200006F RID: 111
	internal sealed class BufferingReadStream : Stream
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x000110A0 File Offset: 0x0000F2A0
		internal BufferingReadStream(Stream stream)
		{
			this.innerStream = stream;
			this.buffers = new LinkedList<byte[]>();
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000110BA File Offset: 0x0000F2BA
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000110BD File Offset: 0x0000F2BD
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000110C0 File Offset: 0x0000F2C0
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000110C3 File Offset: 0x0000F2C3
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000110CA File Offset: 0x0000F2CA
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x000110D1 File Offset: 0x0000F2D1
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000110D8 File Offset: 0x0000F2D8
		internal bool IsBuffering
		{
			get
			{
				return !this.bufferingModeDisabled;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000110E3 File Offset: 0x0000F2E3
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000110EC File Offset: 0x0000F2EC
		public override int Read(byte[] userBuffer, int offset, int count)
		{
			ExceptionUtils.CheckArgumentNotNull<byte[]>(userBuffer, "userBuffer");
			ExceptionUtils.CheckIntegerNotNegative(offset, "offset");
			ExceptionUtils.CheckIntegerPositive(count, "count");
			int num = 0;
			while (this.currentReadNode != null && count > 0)
			{
				byte[] value = this.currentReadNode.Value;
				int num2 = value.Length - this.positionInCurrentBuffer;
				if (num2 == count)
				{
					Buffer.BlockCopy(value, this.positionInCurrentBuffer, userBuffer, offset, count);
					num += count;
					this.MoveToNextBuffer();
					return num;
				}
				if (num2 > count)
				{
					Buffer.BlockCopy(value, this.positionInCurrentBuffer, userBuffer, offset, count);
					num += count;
					this.positionInCurrentBuffer += count;
					return num;
				}
				Buffer.BlockCopy(value, this.positionInCurrentBuffer, userBuffer, offset, num2);
				num += num2;
				offset += num2;
				count -= num2;
				this.MoveToNextBuffer();
			}
			int num3 = this.innerStream.Read(userBuffer, offset, count);
			if (!this.bufferingModeDisabled && num3 > 0)
			{
				byte[] array = new byte[num3];
				Buffer.BlockCopy(userBuffer, offset, array, 0, num3);
				this.buffers.AddLast(array);
			}
			return num + num3;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000111EF File Offset: 0x0000F3EF
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000111F6 File Offset: 0x0000F3F6
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000111FD File Offset: 0x0000F3FD
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00011204 File Offset: 0x0000F404
		internal void ResetStream()
		{
			this.currentReadNode = this.buffers.First;
			this.positionInCurrentBuffer = 0;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001121E File Offset: 0x0000F41E
		internal void StopBuffering()
		{
			this.ResetStream();
			this.bufferingModeDisabled = true;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001122D File Offset: 0x0000F42D
		protected override void Dispose(bool disposing)
		{
			if (this.bufferingModeDisabled)
			{
				if (disposing && this.innerStream != null)
				{
					this.innerStream.Dispose();
					this.innerStream = null;
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001125B File Offset: 0x0000F45B
		private void MoveToNextBuffer()
		{
			if (this.bufferingModeDisabled)
			{
				this.buffers.RemoveFirst();
				this.currentReadNode = this.buffers.First;
			}
			else
			{
				this.currentReadNode = this.currentReadNode.Next;
			}
			this.positionInCurrentBuffer = 0;
		}

		// Token: 0x04000206 RID: 518
		private readonly LinkedList<byte[]> buffers;

		// Token: 0x04000207 RID: 519
		private Stream innerStream;

		// Token: 0x04000208 RID: 520
		private int positionInCurrentBuffer;

		// Token: 0x04000209 RID: 521
		private bool bufferingModeDisabled;

		// Token: 0x0400020A RID: 522
		private LinkedListNode<byte[]> currentReadNode;
	}
}
