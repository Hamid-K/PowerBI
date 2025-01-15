using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020000FE RID: 254
	internal sealed class BufferingReadStream : Stream
	{
		// Token: 0x060006A3 RID: 1699 RVA: 0x00017A0F File Offset: 0x00015C0F
		internal BufferingReadStream(Stream stream)
		{
			this.innerStream = stream;
			this.buffers = new LinkedList<byte[]>();
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00017A29 File Offset: 0x00015C29
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00017A2C File Offset: 0x00015C2C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00017A2F File Offset: 0x00015C2F
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00017A32 File Offset: 0x00015C32
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00017A39 File Offset: 0x00015C39
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x00017A40 File Offset: 0x00015C40
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

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00017A47 File Offset: 0x00015C47
		internal bool IsBuffering
		{
			get
			{
				return !this.bufferingModeDisabled;
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00017A52 File Offset: 0x00015C52
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00017A5C File Offset: 0x00015C5C
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

		// Token: 0x060006AD RID: 1709 RVA: 0x00017B5F File Offset: 0x00015D5F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00017B66 File Offset: 0x00015D66
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00017B6D File Offset: 0x00015D6D
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00017B74 File Offset: 0x00015D74
		internal void ResetStream()
		{
			this.currentReadNode = this.buffers.First;
			this.positionInCurrentBuffer = 0;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00017B8E File Offset: 0x00015D8E
		internal void StopBuffering()
		{
			this.ResetStream();
			this.bufferingModeDisabled = true;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00017B9D File Offset: 0x00015D9D
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

		// Token: 0x060006B3 RID: 1715 RVA: 0x00017BCB File Offset: 0x00015DCB
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

		// Token: 0x0400029D RID: 669
		private readonly LinkedList<byte[]> buffers;

		// Token: 0x0400029E RID: 670
		private Stream innerStream;

		// Token: 0x0400029F RID: 671
		private int positionInCurrentBuffer;

		// Token: 0x040002A0 RID: 672
		private bool bufferingModeDisabled;

		// Token: 0x040002A1 RID: 673
		private LinkedListNode<byte[]> currentReadNode;
	}
}
