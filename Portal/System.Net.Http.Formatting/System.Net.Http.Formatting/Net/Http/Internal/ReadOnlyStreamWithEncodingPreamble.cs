using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Internal
{
	// Token: 0x0200002E RID: 46
	internal class ReadOnlyStreamWithEncodingPreamble : Stream
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000649C File Offset: 0x0000469C
		public ReadOnlyStreamWithEncodingPreamble(Stream innerStream, Encoding encoding)
		{
			this._innerStream = innerStream;
			byte[] preamble = encoding.GetPreamble();
			int num = preamble.Length;
			if (num <= 0)
			{
				return;
			}
			int num2 = num * 2;
			byte[] array = new byte[num2];
			int i = num;
			preamble.CopyTo(array, 0);
			while (i < num2)
			{
				int num3 = innerStream.ReadByte();
				if (num3 == -1)
				{
					break;
				}
				array[i] = (byte)num3;
				i++;
			}
			if (i == num2)
			{
				bool flag = true;
				for (int j = 0; j < num; j++)
				{
					if (array[j] != array[j + num])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					i = num;
				}
			}
			this._remainingBytes = new ArraySegment<byte>(array, 0, i);
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000653C File Offset: 0x0000473C
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00006317 File Offset: 0x00004517
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006317 File Offset: 0x00004517
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000653F File Offset: 0x0000473F
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000653F File Offset: 0x0000473F
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000653F File Offset: 0x0000473F
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

		// Token: 0x060001C6 RID: 454 RVA: 0x0000653F File Offset: 0x0000473F
		public override void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006546 File Offset: 0x00004746
		private static Task<int> GetCancelledTask()
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			taskCompletionSource.SetCanceled();
			return taskCompletionSource.Task;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006558 File Offset: 0x00004758
		public override int Read(byte[] buffer, int offset, int count)
		{
			byte[] array = this._remainingBytes.Array;
			if (array == null)
			{
				return this._innerStream.Read(buffer, offset, count);
			}
			int count2 = this._remainingBytes.Count;
			int offset2 = this._remainingBytes.Offset;
			int num = Math.Min(count, count2);
			for (int i = 0; i < num; i++)
			{
				buffer[offset + i] = array[offset2 + i];
			}
			if (num == count2)
			{
				this._remainingBytes = default(ArraySegment<byte>);
			}
			else
			{
				this._remainingBytes = new ArraySegment<byte>(array, offset2 + num, count2 - num);
			}
			return num;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000065E4 File Offset: 0x000047E4
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (this._remainingBytes.Array == null)
			{
				return this._innerStream.ReadAsync(buffer, offset, count, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return ReadOnlyStreamWithEncodingPreamble._cancelledTask;
			}
			return Task.FromResult<int>(this.Read(buffer, offset, count));
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000653F File Offset: 0x0000473F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000653F File Offset: 0x0000473F
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000653F File Offset: 0x0000473F
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400008C RID: 140
		private static Task<int> _cancelledTask = ReadOnlyStreamWithEncodingPreamble.GetCancelledTask();

		// Token: 0x0400008D RID: 141
		private Stream _innerStream;

		// Token: 0x0400008E RID: 142
		private ArraySegment<byte> _remainingBytes;
	}
}
