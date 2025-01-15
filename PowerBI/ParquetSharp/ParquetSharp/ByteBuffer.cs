using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000011 RID: 17
	public sealed class ByteBuffer : IDisposable
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002A84 File Offset: 0x00000C84
		public ByteBuffer(int blockSize, int leaveMegabytes = 0)
		{
			this._blockSize = blockSize;
			this._leaveMegabytes = leaveMegabytes;
			this._blocks = default(ByteBuffer.BlockList);
			this._currentBlock = -1;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public void Dispose()
		{
			this.Clear();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002AC0 File Offset: 0x00000CC0
		~ByteBuffer()
		{
			this.Clear();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public void Clear()
		{
			foreach (ByteBuffer.Block block in this._blocks)
			{
				block.Dispose();
			}
			this._blocks.Clear();
			this._currentBlock = -1;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B58 File Offset: 0x00000D58
		public ByteArray Allocate(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			while (this._currentBlock >= 0 && this._currentBlock < this._blocks.Count - 1 && this._blocks[this._currentBlock].Available < length)
			{
				this._currentBlock++;
			}
			if (this._currentBlock < 0 || (this._currentBlock == this._blocks.Count - 1 && this._blocks[this._currentBlock].Available < length))
			{
				int nextCapacity = this.GetNextCapacity(length);
				using (new MemoryFailPoint(1 + (nextCapacity - 1) / 1048576 + this._leaveMegabytes))
				{
					this._blocks.Add(new ByteBuffer.Block(nextCapacity));
				}
				this._currentBlock++;
			}
			ByteBuffer.Block block = this._blocks[this._currentBlock];
			ByteArray byteArray = block.Allocate(length);
			this._blocks[this._currentBlock] = block;
			return byteArray;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C90 File Offset: 0x00000E90
		public void FreeAllAndReuse()
		{
			int count = this._blocks.Count;
			for (int i = 0; i < count; i++)
			{
				ByteBuffer.Block block = this._blocks[i];
				block.FreeAll();
				this._blocks[i] = block;
			}
			this._currentBlock = ((count > 0) ? 0 : (-1));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public void FreeAllClearAndReuse()
		{
			int count = this._blocks.Count;
			for (int i = 0; i < count; i++)
			{
				ByteBuffer.Block block = this._blocks[i];
				block.Clear();
				this._blocks[i] = block;
			}
			this._currentBlock = ((count > 0) ? 0 : (-1));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002D58 File Offset: 0x00000F58
		private int GetNextCapacity(int length)
		{
			if (this._blocks.Count == 0)
			{
				return Math.Max(length, this._blockSize);
			}
			int capacity = this._blocks[this._blocks.Count - 1].Capacity;
			int num = Math.Max(2, capacity + capacity / 2);
			return Math.Max(length, num);
		}

		// Token: 0x04000023 RID: 35
		private readonly int _blockSize;

		// Token: 0x04000024 RID: 36
		private readonly int _leaveMegabytes;

		// Token: 0x04000025 RID: 37
		private ByteBuffer.BlockList _blocks;

		// Token: 0x04000026 RID: 38
		private int _currentBlock;

		// Token: 0x020000FB RID: 251
		private struct Block : IDisposable
		{
			// Token: 0x0600090A RID: 2314 RVA: 0x0002B680 File Offset: 0x00029880
			public Block(int capacity)
			{
				this._buffer = new byte[capacity];
				this._handle = GCHandle.Alloc(this._buffer, GCHandleType.Pinned);
				this._size = 0;
			}

			// Token: 0x0600090B RID: 2315 RVA: 0x0002B6A8 File Offset: 0x000298A8
			public void Dispose()
			{
				this._handle.Free();
			}

			// Token: 0x0600090C RID: 2316 RVA: 0x0002B6B8 File Offset: 0x000298B8
			public void FreeAll()
			{
				this._size = 0;
			}

			// Token: 0x0600090D RID: 2317 RVA: 0x0002B6C4 File Offset: 0x000298C4
			public void Clear()
			{
				Array.Clear(this._buffer, 0, this._buffer.Length);
				this._size = 0;
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x0600090E RID: 2318 RVA: 0x0002B6E4 File Offset: 0x000298E4
			public int Available
			{
				get
				{
					return this._buffer.Length - this._size;
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x0600090F RID: 2319 RVA: 0x0002B6F8 File Offset: 0x000298F8
			public int Capacity
			{
				get
				{
					return this._buffer.Length;
				}
			}

			// Token: 0x06000910 RID: 2320 RVA: 0x0002B704 File Offset: 0x00029904
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ByteArray Allocate(int length)
			{
				ByteArray byteArray = new ByteArray(this._handle.AddrOfPinnedObject() + this._size, length);
				this._size += length;
				return byteArray;
			}

			// Token: 0x040002C1 RID: 705
			[Nullable(1)]
			private readonly byte[] _buffer;

			// Token: 0x040002C2 RID: 706
			private GCHandle _handle;

			// Token: 0x040002C3 RID: 707
			private int _size;
		}

		// Token: 0x020000FC RID: 252
		private struct BlockList : IEnumerable<ByteBuffer.Block>, IEnumerable
		{
			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000911 RID: 2321 RVA: 0x0002B730 File Offset: 0x00029930
			public int Count
			{
				get
				{
					List<ByteBuffer.Block> blocks = this._blocks;
					return ((blocks != null) ? blocks.Count : 0) + (((this._firstBlock != null) > false) ? 1 : 0);
				}
			}

			// Token: 0x17000113 RID: 275
			public ByteBuffer.Block this[int i]
			{
				get
				{
					if (i < 0 || i >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					if (i != 0)
					{
						return this._blocks[i - 1];
					}
					return this._firstBlock.Value;
				}
				set
				{
					if (i < 0 || i >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					if (i == 0)
					{
						this._firstBlock = new ByteBuffer.Block?(value);
						return;
					}
					this._blocks[i - 1] = value;
				}
			}

			// Token: 0x06000914 RID: 2324 RVA: 0x0002B7D8 File Offset: 0x000299D8
			public void Add(ByteBuffer.Block block)
			{
				if (this._firstBlock == null)
				{
					this._firstBlock = new ByteBuffer.Block?(block);
					return;
				}
				if (this._blocks == null)
				{
					this._blocks = new List<ByteBuffer.Block>();
				}
				this._blocks.Add(block);
			}

			// Token: 0x06000915 RID: 2325 RVA: 0x0002B828 File Offset: 0x00029A28
			public void Clear()
			{
				this._firstBlock = null;
				List<ByteBuffer.Block> blocks = this._blocks;
				if (blocks == null)
				{
					return;
				}
				blocks.Clear();
			}

			// Token: 0x06000916 RID: 2326 RVA: 0x0002B84C File Offset: 0x00029A4C
			[NullableContext(1)]
			public IEnumerator<ByteBuffer.Block> GetEnumerator()
			{
				if (this._firstBlock != null)
				{
					yield return this._firstBlock.Value;
				}
				if (this._blocks != null)
				{
					foreach (ByteBuffer.Block block in this._blocks)
					{
						yield return block;
					}
					List<ByteBuffer.Block>.Enumerator enumerator = default(List<ByteBuffer.Block>.Enumerator);
				}
				yield break;
				yield break;
			}

			// Token: 0x06000917 RID: 2327 RVA: 0x0002B860 File Offset: 0x00029A60
			[NullableContext(1)]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040002C4 RID: 708
			private ByteBuffer.Block? _firstBlock;

			// Token: 0x040002C5 RID: 709
			[Nullable(1)]
			private List<ByteBuffer.Block> _blocks;
		}
	}
}
