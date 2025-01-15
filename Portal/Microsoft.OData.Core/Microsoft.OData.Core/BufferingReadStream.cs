using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x0200002E RID: 46
	internal sealed class BufferingReadStream : Stream
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00004697 File Offset: 0x00002897
		internal BufferingReadStream(Stream stream)
		{
			this.innerStream = stream;
			this.buffers = new LinkedList<byte[]>();
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00002393 File Offset: 0x00000593
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00002396 File Offset: 0x00000596
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00002396 File Offset: 0x00000596
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00002396 File Offset: 0x00000596
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000046B1 File Offset: 0x000028B1
		internal bool IsBuffering
		{
			get
			{
				return !this.bufferingModeDisabled;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00002396 File Offset: 0x00000596
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000046BC File Offset: 0x000028BC
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

		// Token: 0x060001A6 RID: 422 RVA: 0x00002396 File Offset: 0x00000596
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00002396 File Offset: 0x00000596
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00002396 File Offset: 0x00000596
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000047C0 File Offset: 0x000029C0
		internal void ResetStream()
		{
			this.currentReadNode = this.buffers.First;
			this.positionInCurrentBuffer = 0;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000047DA File Offset: 0x000029DA
		internal void StopBuffering()
		{
			this.ResetStream();
			this.bufferingModeDisabled = true;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000047E9 File Offset: 0x000029E9
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

		// Token: 0x060001AC RID: 428 RVA: 0x00004817 File Offset: 0x00002A17
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

		// Token: 0x04000084 RID: 132
		private readonly LinkedList<byte[]> buffers;

		// Token: 0x04000085 RID: 133
		private Stream innerStream;

		// Token: 0x04000086 RID: 134
		private int positionInCurrentBuffer;

		// Token: 0x04000087 RID: 135
		private bool bufferingModeDisabled;

		// Token: 0x04000088 RID: 136
		private LinkedListNode<byte[]> currentReadNode;
	}
}
