using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000008 RID: 8
	internal sealed class BufferingReadStream : Stream
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000261A File Offset: 0x0000081A
		internal BufferingReadStream(Stream stream)
		{
			this.innerStream = stream;
			this.buffers = new LinkedList<byte[]>();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002503 File Offset: 0x00000703
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002500 File Offset: 0x00000700
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002500 File Offset: 0x00000700
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002506 File Offset: 0x00000706
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002506 File Offset: 0x00000706
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002506 File Offset: 0x00000706
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002634 File Offset: 0x00000834
		internal bool IsBuffering
		{
			get
			{
				return !this.bufferingModeDisabled;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002506 File Offset: 0x00000706
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002640 File Offset: 0x00000840
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

		// Token: 0x06000036 RID: 54 RVA: 0x00002506 File Offset: 0x00000706
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002506 File Offset: 0x00000706
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002506 File Offset: 0x00000706
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002744 File Offset: 0x00000944
		internal void ResetStream()
		{
			this.currentReadNode = this.buffers.First;
			this.positionInCurrentBuffer = 0;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000275E File Offset: 0x0000095E
		internal void StopBuffering()
		{
			this.ResetStream();
			this.bufferingModeDisabled = true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000276D File Offset: 0x0000096D
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

		// Token: 0x0600003C RID: 60 RVA: 0x0000279B File Offset: 0x0000099B
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

		// Token: 0x04000017 RID: 23
		private readonly LinkedList<byte[]> buffers;

		// Token: 0x04000018 RID: 24
		private Stream innerStream;

		// Token: 0x04000019 RID: 25
		private int positionInCurrentBuffer;

		// Token: 0x0400001A RID: 26
		private bool bufferingModeDisabled;

		// Token: 0x0400001B RID: 27
		private LinkedListNode<byte[]> currentReadNode;
	}
}
