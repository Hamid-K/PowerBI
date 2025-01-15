using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000043 RID: 67
	[DebuggerDisplay("Count={CountForDebugging}")]
	[DebuggerTypeProxy(typeof(ReorderingBuffer<>.DebugView))]
	internal sealed class ReorderingBuffer<TOutput> : IReorderingBuffer
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000092C3 File Offset: 0x000074C3
		private object ValueLock
		{
			get
			{
				return this._reorderingBuffer;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000092CB File Offset: 0x000074CB
		internal ReorderingBuffer(object owningSource, Action<object, TOutput> outputAction)
		{
			this._owningSource = owningSource;
			this._outputAction = outputAction;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000092EC File Offset: 0x000074EC
		internal void AddItem(long id, TOutput item, bool itemIsValid)
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				if (this._nextReorderedIdToOutput == id)
				{
					this.OutputNextItem(item, itemIsValid);
				}
				else
				{
					this._reorderingBuffer.Add(id, new KeyValuePair<bool, TOutput>(itemIsValid, item));
				}
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000934C File Offset: 0x0000754C
		internal bool? AddItemIfNextAndTrusted(long id, TOutput item, bool isTrusted)
		{
			object valueLock = this.ValueLock;
			bool? flag2;
			lock (valueLock)
			{
				if (this._nextReorderedIdToOutput == id)
				{
					if (isTrusted)
					{
						this.OutputNextItem(item, true);
						flag2 = null;
						flag2 = flag2;
					}
					else
					{
						flag2 = new bool?(true);
					}
				}
				else
				{
					flag2 = new bool?(false);
				}
			}
			return flag2;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000093B8 File Offset: 0x000075B8
		public void IgnoreItem(long id)
		{
			this.AddItem(id, default(TOutput), false);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000093D8 File Offset: 0x000075D8
		private void OutputNextItem(TOutput theNextItem, bool itemIsValid)
		{
			this._nextReorderedIdToOutput += 1L;
			if (itemIsValid)
			{
				this._outputAction(this._owningSource, theNextItem);
			}
			KeyValuePair<bool, TOutput> keyValuePair;
			while (this._reorderingBuffer.TryGetValue(this._nextReorderedIdToOutput, out keyValuePair))
			{
				this._reorderingBuffer.Remove(this._nextReorderedIdToOutput);
				this._nextReorderedIdToOutput += 1L;
				if (keyValuePair.Key)
				{
					this._outputAction(this._owningSource, keyValuePair.Value);
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009462 File Offset: 0x00007662
		private int CountForDebugging
		{
			get
			{
				return this._reorderingBuffer.Count;
			}
		}

		// Token: 0x0400009E RID: 158
		private readonly object _owningSource;

		// Token: 0x0400009F RID: 159
		private readonly Dictionary<long, KeyValuePair<bool, TOutput>> _reorderingBuffer = new Dictionary<long, KeyValuePair<bool, TOutput>>();

		// Token: 0x040000A0 RID: 160
		private readonly Action<object, TOutput> _outputAction;

		// Token: 0x040000A1 RID: 161
		private long _nextReorderedIdToOutput;

		// Token: 0x02000087 RID: 135
		private sealed class DebugView
		{
			// Token: 0x0600043A RID: 1082 RVA: 0x0000FE32 File Offset: 0x0000E032
			public DebugView(ReorderingBuffer<TOutput> buffer)
			{
				this._buffer = buffer;
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000FE41 File Offset: 0x0000E041
			public Dictionary<long, KeyValuePair<bool, TOutput>> ItemsBuffered
			{
				get
				{
					return this._buffer._reorderingBuffer;
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000FE4E File Offset: 0x0000E04E
			public long NextIdRequired
			{
				get
				{
					return this._buffer._nextReorderedIdToOutput;
				}
			}

			// Token: 0x040001BB RID: 443
			private readonly ReorderingBuffer<TOutput> _buffer;
		}
	}
}
