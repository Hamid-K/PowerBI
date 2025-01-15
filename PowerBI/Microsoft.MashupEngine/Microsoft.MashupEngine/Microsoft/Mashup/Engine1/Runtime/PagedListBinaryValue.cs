using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Internal;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015BA RID: 5562
	internal sealed class PagedListBinaryValue : StreamedListValue
	{
		// Token: 0x06008B57 RID: 35671 RVA: 0x001D49B6 File Offset: 0x001D2BB6
		public PagedListBinaryValue(StreamedBinaryValue binary, long pageSize)
		{
			this.binary = binary;
			this.pageSize = pageSize;
		}

		// Token: 0x06008B58 RID: 35672 RVA: 0x001D49CC File Offset: 0x001D2BCC
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new PagedListBinaryValue.StreamedBinaryEnumerator(this.binary, this.pageSize);
		}

		// Token: 0x170024AF RID: 9391
		// (get) Token: 0x06008B59 RID: 35673 RVA: 0x001D49E0 File Offset: 0x001D2BE0
		public override long LargeCount
		{
			get
			{
				long num;
				if (this.TryGetListCount(out num))
				{
					return num;
				}
				return base.LargeCount;
			}
		}

		// Token: 0x06008B5A RID: 35674 RVA: 0x001D4A00 File Offset: 0x001D2C00
		public override IValueReference GetReference(int index)
		{
			if (index < 0)
			{
				throw ValueException.StructureIndexCannotBeNegative(index, this);
			}
			long num;
			if (!this.TryGetListCount(out num))
			{
				return base.GetReference(index);
			}
			if ((long)index >= num)
			{
				throw ValueException.InsufficientElements(this);
			}
			return this.binary.Range(new RowCount((long)index * this.pageSize), new RowCount(this.pageSize));
		}

		// Token: 0x06008B5B RID: 35675 RVA: 0x001D4A5C File Offset: 0x001D2C5C
		private bool TryGetListCount(out long count)
		{
			long num;
			if (this.binary.TryGetLength(out num))
			{
				count = ((num > 0L) ? ((num - 1L) / this.pageSize + 1L) : 0L);
				return true;
			}
			count = 0L;
			return false;
		}

		// Token: 0x04004C51 RID: 19537
		private readonly StreamedBinaryValue binary;

		// Token: 0x04004C52 RID: 19538
		private readonly long pageSize;

		// Token: 0x020015BB RID: 5563
		private class StreamedBinaryEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06008B5C RID: 35676 RVA: 0x001D4A98 File Offset: 0x001D2C98
			public StreamedBinaryEnumerator(StreamedBinaryValue binary, long pageSize)
			{
				this.binary = binary;
				this.pageSize = pageSize;
				this.currentByte = -1;
			}

			// Token: 0x06008B5D RID: 35677 RVA: 0x001D4AB5 File Offset: 0x001D2CB5
			public void Dispose()
			{
				if (this.sharer != null)
				{
					this.sharer.Release();
					this.sharer = null;
				}
			}

			// Token: 0x170024B0 RID: 9392
			// (get) Token: 0x06008B5E RID: 35678 RVA: 0x001D4AD4 File Offset: 0x001D2CD4
			public IValueReference Current
			{
				get
				{
					if (this.current == null)
					{
						if (this.currentByte == -1)
						{
							throw new InvalidOperationException();
						}
						this.current = new PagedListBinaryValue.SubStreamBinaryValue(this.sharer, (byte)this.currentByte, this.currentOffset, this.pageSize);
					}
					return this.current;
				}
			}

			// Token: 0x170024B1 RID: 9393
			// (get) Token: 0x06008B5F RID: 35679 RVA: 0x001D4B22 File Offset: 0x001D2D22
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008B60 RID: 35680 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06008B61 RID: 35681 RVA: 0x001D4B2C File Offset: 0x001D2D2C
			public bool MoveNext()
			{
				this.current = null;
				if (this.sharer == null)
				{
					this.sharer = new PagedListBinaryValue.ReadStreamSharer(this.binary);
					this.currentByte = this.sharer.TryReadByteAt(this.currentOffset);
				}
				else if (this.currentByte != -1)
				{
					this.currentOffset += this.pageSize;
					this.currentByte = this.sharer.TryReadByteAt(this.currentOffset);
				}
				return this.currentByte != -1;
			}

			// Token: 0x04004C53 RID: 19539
			private readonly long pageSize;

			// Token: 0x04004C54 RID: 19540
			private readonly StreamedBinaryValue binary;

			// Token: 0x04004C55 RID: 19541
			private StreamedBinaryValue current;

			// Token: 0x04004C56 RID: 19542
			private PagedListBinaryValue.ReadStreamSharer sharer;

			// Token: 0x04004C57 RID: 19543
			private long currentOffset;

			// Token: 0x04004C58 RID: 19544
			private int currentByte;
		}

		// Token: 0x020015BC RID: 5564
		private class SubStreamBinaryValue : StreamedBinaryValue
		{
			// Token: 0x06008B62 RID: 35682 RVA: 0x001D4BB1 File Offset: 0x001D2DB1
			public SubStreamBinaryValue(PagedListBinaryValue.ReadStreamSharer sharer, byte firstByte, long offset, long pageSize)
			{
				this.sharer = sharer;
				this.pageSize = pageSize;
				this.offset = offset;
				this.firstByte = firstByte;
			}

			// Token: 0x06008B63 RID: 35683 RVA: 0x001D4BD6 File Offset: 0x001D2DD6
			public override Stream Open()
			{
				return this.sharer.Open(this.firstByte, this.offset, this.pageSize);
			}

			// Token: 0x04004C59 RID: 19545
			private readonly PagedListBinaryValue.ReadStreamSharer sharer;

			// Token: 0x04004C5A RID: 19546
			private readonly long pageSize;

			// Token: 0x04004C5B RID: 19547
			private readonly long offset;

			// Token: 0x04004C5C RID: 19548
			private readonly byte firstByte;
		}

		// Token: 0x020015BD RID: 5565
		private class ReadStreamSharer
		{
			// Token: 0x06008B64 RID: 35684 RVA: 0x001D4BF5 File Offset: 0x001D2DF5
			public ReadStreamSharer(StreamedBinaryValue value)
			{
				this.syncRoot = new object();
				this.refCount = 1;
				this.value = value;
				this.stream = this.value.Open();
			}

			// Token: 0x06008B65 RID: 35685 RVA: 0x001D4C28 File Offset: 0x001D2E28
			public int TryReadByteAt(long position)
			{
				object obj = this.syncRoot;
				int num2;
				lock (obj)
				{
					if (this.position != position)
					{
						if (this.stream.CanSeek)
						{
							if (position >= this.stream.Length)
							{
								return -1;
							}
							this.stream.Position = position;
							this.position = position;
						}
						else if (!this.MoveNonSeekable(position))
						{
							return -1;
						}
					}
					int num = this.stream.ReadByte();
					if (num != -1)
					{
						this.position += 1L;
					}
					num2 = num;
				}
				return num2;
			}

			// Token: 0x06008B66 RID: 35686 RVA: 0x001D4CCC File Offset: 0x001D2ECC
			public void Release()
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.stream != null)
					{
						this.refCount--;
						if (this.refCount == 0)
						{
							this.CloseStream();
						}
					}
				}
			}

			// Token: 0x06008B67 RID: 35687 RVA: 0x001D4D2C File Offset: 0x001D2F2C
			public Stream Open(byte firstByte, long offset, long pageSize)
			{
				object obj = this.syncRoot;
				Stream stream;
				lock (obj)
				{
					if (this.stream == null)
					{
						this.stream = this.value.Open();
					}
					this.refCount++;
					stream = new BufferedStream(new PagedListBinaryValue.ReadStreamSharer.SubStream(this, firstByte, offset, pageSize));
				}
				return stream;
			}

			// Token: 0x06008B68 RID: 35688 RVA: 0x001D4DA0 File Offset: 0x001D2FA0
			private void MoveTo(long position)
			{
				if (this.position == position)
				{
					return;
				}
				if (this.stream.CanSeek)
				{
					this.stream.Position = position;
					this.position = position;
					return;
				}
				if (!this.MoveNonSeekable(position))
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06008B69 RID: 35689 RVA: 0x001D4DDC File Offset: 0x001D2FDC
			private int ReadByteAt(long position)
			{
				object obj = this.syncRoot;
				int num2;
				lock (obj)
				{
					this.MoveTo(position);
					int num = this.stream.ReadByte();
					if (num != -1)
					{
						this.position += 1L;
					}
					num2 = num;
				}
				return num2;
			}

			// Token: 0x06008B6A RID: 35690 RVA: 0x001D4E40 File Offset: 0x001D3040
			private bool MoveNonSeekable(long position)
			{
				if (this.position > position)
				{
					this.CloseStream();
					this.stream = this.value.Open();
					if (position == 0L)
					{
						return true;
					}
				}
				else if (this.stream == null)
				{
					this.stream = this.value.Open();
				}
				long num = position - this.position;
				byte[] array = new byte[(int)Math.Min(4096L, num)];
				while (num > 0L)
				{
					int num2 = this.stream.Read(array, 0, (int)Math.Min(num, (long)array.Length));
					if (num2 == 0)
					{
						break;
					}
					num -= (long)num2;
				}
				this.position = position - num;
				return num == 0L;
			}

			// Token: 0x06008B6B RID: 35691 RVA: 0x001D4EDD File Offset: 0x001D30DD
			private void CloseStream()
			{
				Stream stream = this.stream;
				this.stream = null;
				this.position = 0L;
				stream.Close();
			}

			// Token: 0x06008B6C RID: 35692 RVA: 0x001D4EFC File Offset: 0x001D30FC
			private int ReadAt(long position, byte[] buffer, int offset, int count)
			{
				object obj = this.syncRoot;
				int num2;
				lock (obj)
				{
					this.MoveTo(position);
					int num = this.stream.Read(buffer, offset, count);
					this.position += (long)num;
					num2 = num;
				}
				return num2;
			}

			// Token: 0x04004C5D RID: 19549
			private const int DefaultReadChunkSize = 4096;

			// Token: 0x04004C5E RID: 19550
			private readonly StreamedBinaryValue value;

			// Token: 0x04004C5F RID: 19551
			private readonly object syncRoot;

			// Token: 0x04004C60 RID: 19552
			private Stream stream;

			// Token: 0x04004C61 RID: 19553
			private int refCount;

			// Token: 0x04004C62 RID: 19554
			private long position;

			// Token: 0x020015BE RID: 5566
			private class SubStream : ReadOnlyStream
			{
				// Token: 0x06008B6D RID: 35693 RVA: 0x001D4F60 File Offset: 0x001D3160
				public SubStream(PagedListBinaryValue.ReadStreamSharer sharer, byte firstByte, long offset, long pageSize)
				{
					this.sharer = sharer;
					this.offset = offset;
					this.firstByte = firstByte;
					this.canSeek = this.sharer.stream.CanSeek;
					this.length = (this.canSeek ? Math.Min(this.sharer.stream.Length - offset, pageSize) : pageSize);
				}

				// Token: 0x06008B6E RID: 35694 RVA: 0x001D4FC9 File Offset: 0x001D31C9
				public override void Close()
				{
					if (this.sharer != null)
					{
						this.sharer.Release();
						this.sharer = null;
					}
				}

				// Token: 0x170024B2 RID: 9394
				// (get) Token: 0x06008B6F RID: 35695 RVA: 0x001D4FE5 File Offset: 0x001D31E5
				public override bool CanSeek
				{
					get
					{
						return this.canSeek;
					}
				}

				// Token: 0x170024B3 RID: 9395
				// (get) Token: 0x06008B70 RID: 35696 RVA: 0x001D4FED File Offset: 0x001D31ED
				public override long Length
				{
					get
					{
						if (!this.canSeek)
						{
							throw new NotSupportedException();
						}
						return this.length;
					}
				}

				// Token: 0x170024B4 RID: 9396
				// (get) Token: 0x06008B71 RID: 35697 RVA: 0x001D5003 File Offset: 0x001D3203
				// (set) Token: 0x06008B72 RID: 35698 RVA: 0x001D500B File Offset: 0x001D320B
				public override long Position
				{
					get
					{
						return this.position;
					}
					set
					{
						if (!this.canSeek)
						{
							throw new NotSupportedException();
						}
						if (value < 0L || value > this.length)
						{
							throw new ArgumentOutOfRangeException("value");
						}
						this.position = value;
					}
				}

				// Token: 0x06008B73 RID: 35699 RVA: 0x001D503C File Offset: 0x001D323C
				public override int ReadByte()
				{
					if (this.position >= this.length)
					{
						return -1;
					}
					if (this.position == 0L)
					{
						this.position += 1L;
						return (int)this.firstByte;
					}
					int num = this.sharer.ReadByteAt(this.offset + this.position);
					if (num != -1)
					{
						this.position += 1L;
						return num;
					}
					this.length = this.position;
					return num;
				}

				// Token: 0x06008B74 RID: 35700 RVA: 0x001D50B0 File Offset: 0x001D32B0
				public override int Read(byte[] buffer, int offset, int count)
				{
					if (this.position >= this.length || count == 0)
					{
						return 0;
					}
					int num = 0;
					if (this.position == 0L)
					{
						num = 1;
						buffer[offset] = this.firstByte;
						this.position += 1L;
						count--;
						offset++;
						if (this.position == this.length || count == 0)
						{
							return 1;
						}
					}
					int num2 = this.sharer.ReadAt(this.offset + this.position, buffer, offset, (int)Math.Min((long)count, this.length - this.position));
					this.position += (long)num2;
					if (num2 == 0)
					{
						this.length = this.position;
					}
					return num2 + num;
				}

				// Token: 0x06008B75 RID: 35701 RVA: 0x001D5164 File Offset: 0x001D3364
				public override long Seek(long offset, SeekOrigin origin)
				{
					switch (origin)
					{
					case SeekOrigin.Begin:
						this.Position = offset;
						break;
					case SeekOrigin.Current:
						this.Position += offset;
						break;
					case SeekOrigin.End:
						this.Position = this.length + offset;
						break;
					default:
						throw new InvalidOperationException();
					}
					return this.Position;
				}

				// Token: 0x04004C63 RID: 19555
				private readonly bool canSeek;

				// Token: 0x04004C64 RID: 19556
				private readonly long offset;

				// Token: 0x04004C65 RID: 19557
				private readonly byte firstByte;

				// Token: 0x04004C66 RID: 19558
				private PagedListBinaryValue.ReadStreamSharer sharer;

				// Token: 0x04004C67 RID: 19559
				private long position;

				// Token: 0x04004C68 RID: 19560
				private long length;
			}
		}
	}
}
