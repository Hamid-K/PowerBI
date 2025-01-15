using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000296 RID: 662
	public sealed class InputFork
	{
		// Token: 0x060011C8 RID: 4552 RVA: 0x0003E450 File Offset: 0x0003C650
		public InputFork(Stream source, int readerCount)
			: this(source, readerCount, 32767)
		{
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0003E460 File Offset: 0x0003C660
		public InputFork([NotNull] Stream source, int readerCount, int bufferSize)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Stream>(source, "source");
			ExtendedDiagnostics.EnsureArgumentIsPositive(readerCount, "readerCount");
			ExtendedDiagnostics.EnsureOperation(source.CanRead, "'source' must be readable");
			this.m_source = source;
			this.m_serializer = new Serializer<int>();
			this.m_readers = (from i in Enumerable.Range(0, readerCount)
				select new InputFork.Reader(this, this.m_serializer, i)).ToArray<InputFork.Reader>();
			this.m_buffer = new byte[bufferSize];
			this.m_fillGate = new AsyncManualResetEvent();
		}

		// Token: 0x1700027A RID: 634
		public Stream this[int readerIndex]
		{
			get
			{
				return this.m_readers[readerIndex];
			}
		}

		// Token: 0x0400069E RID: 1694
		private const int c_defaultBufferSize = 32767;

		// Token: 0x0400069F RID: 1695
		private readonly Stream m_source;

		// Token: 0x040006A0 RID: 1696
		private readonly Serializer<int> m_serializer;

		// Token: 0x040006A1 RID: 1697
		private readonly InputFork.Reader[] m_readers;

		// Token: 0x040006A2 RID: 1698
		private readonly byte[] m_buffer;

		// Token: 0x040006A3 RID: 1699
		private int m_start;

		// Token: 0x040006A4 RID: 1700
		private int m_count;

		// Token: 0x040006A5 RID: 1701
		private AsyncManualResetEvent m_fillGate;

		// Token: 0x040006A6 RID: 1702
		private int m_pendingReaderCount;

		// Token: 0x02000747 RID: 1863
		private sealed class ReadNextFlow : Sequencer
		{
			// Token: 0x06002FF5 RID: 12277 RVA: 0x000A4BDA File Offset: 0x000A2DDA
			public ReadNextFlow(InputFork owner)
			{
				this.m_owner = owner;
			}

			// Token: 0x06002FF6 RID: 12278 RVA: 0x000A4BE9 File Offset: 0x000A2DE9
			protected override IEnumerable<IFlowStep> Run()
			{
				AsyncManualResetEvent oldFillGate = this.m_owner.m_fillGate;
				int num = Interlocked.Increment(ref this.m_owner.m_pendingReaderCount);
				if (num < this.m_owner.m_readers.Length)
				{
					yield return base.RunAsyncStep("wait for fill gate ({0})".FormatWithInvariantCulture(new object[] { num }), new Sequencer.AsyncBeginFunction(oldFillGate.BeginWait), new Sequencer.AsyncEndFunction(oldFillGate.EndWait));
					yield break;
				}
				this.m_owner.m_fillGate = new AsyncManualResetEvent();
				this.m_owner.m_pendingReaderCount = 0;
				int bytesRead = 0;
				yield return base.RunAsyncStep<byte[], int, int>("Read from source [{0}, {1})".FormatWithInvariantCulture(new object[]
				{
					this.m_owner.m_start + this.m_owner.m_count,
					this.m_owner.m_buffer.Length
				}), delegate(string step, Exception ex)
				{
					oldFillGate.Set(ex);
					return ex;
				}, new Sequencer.AsyncBeginFunction<byte[], int, int>(this.m_owner.m_source.BeginRead), delegate(IAsyncResult ar)
				{
					bytesRead = this.m_owner.m_source.EndRead(ar);
				}, this.m_owner.m_buffer, 0, this.m_owner.m_buffer.Length);
				this.m_owner.m_start += this.m_owner.m_count;
				this.m_owner.m_count = bytesRead;
				oldFillGate.Set();
				yield break;
			}

			// Token: 0x04001576 RID: 5494
			private readonly InputFork m_owner;
		}

		// Token: 0x02000748 RID: 1864
		private sealed class Reader : Stream
		{
			// Token: 0x06002FF7 RID: 12279 RVA: 0x000A4BF9 File Offset: 0x000A2DF9
			public Reader(InputFork owner, ISerializer<int> serializer, int index)
			{
				this.m_owner = owner;
				this.m_position = 0;
				this.m_lock = serializer;
				this.m_index = index;
			}

			// Token: 0x17000753 RID: 1875
			// (get) Token: 0x06002FF8 RID: 12280 RVA: 0x000034FD File Offset: 0x000016FD
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000754 RID: 1876
			// (get) Token: 0x06002FF9 RID: 12281 RVA: 0x0000E568 File Offset: 0x0000C768
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000755 RID: 1877
			// (get) Token: 0x06002FFA RID: 12282 RVA: 0x0000E568 File Offset: 0x0000C768
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06002FFB RID: 12283 RVA: 0x00009B3B File Offset: 0x00007D3B
			public override void Flush()
			{
			}

			// Token: 0x17000756 RID: 1878
			// (get) Token: 0x06002FFC RID: 12284 RVA: 0x000A4C1D File Offset: 0x000A2E1D
			public override long Length
			{
				get
				{
					return this.m_owner.m_source.Length;
				}
			}

			// Token: 0x17000757 RID: 1879
			// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000A4C2F File Offset: 0x000A2E2F
			// (set) Token: 0x06002FFE RID: 12286 RVA: 0x00014B8A File Offset: 0x00012D8A
			public override long Position
			{
				get
				{
					return (long)this.m_position;
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06002FFF RID: 12287 RVA: 0x000A4C38 File Offset: 0x000A2E38
			public override int Read(byte[] buffer, int offset, int count)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<byte[]>(buffer, "buffer");
				ExtendedDiagnostics.EnsureArgumentIsNotNegative(offset, "offset");
				ExtendedDiagnostics.EnsureArgumentIsNotNegative(count, "count");
				return this.EndRead(this.BeginRead(buffer, offset, count, null, null));
			}

			// Token: 0x06003000 RID: 12288 RVA: 0x00014B8A File Offset: 0x00012D8A
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06003001 RID: 12289 RVA: 0x00014B8A File Offset: 0x00012D8A
			public override void SetLength(long value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06003002 RID: 12290 RVA: 0x00014B8A File Offset: 0x00012D8A
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06003003 RID: 12291 RVA: 0x000A4C6C File Offset: 0x000A2E6C
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<byte[]>(buffer, "buffer");
				ExtendedDiagnostics.EnsureArgumentIsNotNegative(offset, "offset");
				ExtendedDiagnostics.EnsureArgumentIsNotNegative(count, "count");
				InputFork.Reader.ReadFlow readFlow = new InputFork.Reader.ReadFlow(this, buffer, offset, count);
				return ChainedAsyncOperationInvoker.Begin<InputFork.Reader.ReadFlow>(readFlow, null, new Func<AsyncCallback, object, IAsyncResult>(readFlow.BeginExecute), asyncCallback, asyncState);
			}

			// Token: 0x06003004 RID: 12292 RVA: 0x000A4CBC File Offset: 0x000A2EBC
			public override int EndRead(IAsyncResult asyncResult)
			{
				return ChainedAsyncOperationInvoker.End<InputFork.Reader.ReadFlow, int>(asyncResult, delegate(InputFork.Reader.ReadFlow flow, IAsyncResult ar)
				{
					flow.EndExecute(ar);
					return flow.BytesRead;
				});
			}

			// Token: 0x06003005 RID: 12293 RVA: 0x000A4CE4 File Offset: 0x000A2EE4
			private int ReadFromBuffer(byte[] buffer, int offset, int requestedCount)
			{
				int num = this.m_position - this.m_owner.m_start;
				int num2 = Math.Min(this.m_owner.m_count - num, requestedCount);
				Buffer.BlockCopy(this.m_owner.m_buffer, num, buffer, offset, num2);
				this.m_position += num2;
				return num2;
			}

			// Token: 0x04001577 RID: 5495
			private readonly InputFork m_owner;

			// Token: 0x04001578 RID: 5496
			private readonly ISerializer<int> m_lock;

			// Token: 0x04001579 RID: 5497
			private readonly int m_index;

			// Token: 0x0400157A RID: 5498
			private int m_position;

			// Token: 0x02000881 RID: 2177
			private sealed class ReadFlow : Sequencer
			{
				// Token: 0x060033BD RID: 13245 RVA: 0x000AD26F File Offset: 0x000AB46F
				public ReadFlow(InputFork.Reader reader, byte[] buffer, int offset, int requestedCount)
				{
					this.m_reader = reader;
					this.m_buffer = buffer;
					this.m_offset = offset;
					this.m_requestedCount = requestedCount;
				}

				// Token: 0x1700078F RID: 1935
				// (get) Token: 0x060033BE RID: 13246 RVA: 0x000AD294 File Offset: 0x000AB494
				// (set) Token: 0x060033BF RID: 13247 RVA: 0x000AD29C File Offset: 0x000AB49C
				public int BytesRead { get; private set; }

				// Token: 0x060033C0 RID: 13248 RVA: 0x000AD2A5 File Offset: 0x000AB4A5
				protected override IEnumerable<IFlowStep> Run()
				{
					if (this.m_requestedCount == 0)
					{
						yield break;
					}
					IDisposable h = null;
					yield return base.RunAsyncStep<int>("Grab serializer", new Sequencer.AsyncBeginFunction<int>(this.m_reader.m_lock.BeginAcquireLock), delegate(IAsyncResult ar)
					{
						h = this.m_reader.m_lock.EndAcquireLock(ar);
					}, this.m_reader.m_index);
					using (h)
					{
						this.BytesRead = this.m_reader.ReadFromBuffer(this.m_buffer, this.m_offset, this.m_requestedCount);
						if (this.BytesRead > 0)
						{
							yield break;
						}
						InputFork.ReadNextFlow readNextFlow = new InputFork.ReadNextFlow(this.m_reader.m_owner);
						yield return base.RunAsyncStep("Request to fill buffer", new Sequencer.AsyncBeginFunction(readNextFlow.BeginExecute), new Sequencer.AsyncEndFunction(readNextFlow.EndExecute));
						this.BytesRead = this.m_reader.ReadFromBuffer(this.m_buffer, this.m_offset, this.m_requestedCount);
					}
					IDisposable disposable = null;
					yield break;
					yield break;
				}

				// Token: 0x04001A14 RID: 6676
				private readonly InputFork.Reader m_reader;

				// Token: 0x04001A15 RID: 6677
				private readonly byte[] m_buffer;

				// Token: 0x04001A16 RID: 6678
				private readonly int m_offset;

				// Token: 0x04001A17 RID: 6679
				private readonly int m_requestedCount;
			}
		}
	}
}
