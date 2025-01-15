using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000038 RID: 56
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableQueue<[Nullable(2)] T> : IImmutableQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000286 RID: 646 RVA: 0x0000770C File Offset: 0x0000590C
		internal ImmutableQueue(ImmutableStack<T> forwards, ImmutableStack<T> backwards)
		{
			this._forwards = forwards;
			this._backwards = backwards;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007722 File Offset: 0x00005922
		public ImmutableQueue<T> Clear()
		{
			return ImmutableQueue<T>.Empty;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00007729 File Offset: 0x00005929
		public bool IsEmpty
		{
			get
			{
				return this._forwards.IsEmpty;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00007736 File Offset: 0x00005936
		public static ImmutableQueue<T> Empty
		{
			get
			{
				return ImmutableQueue<T>.s_EmptyField;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000773D File Offset: 0x0000593D
		IImmutableQueue<T> IImmutableQueue<T>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00007745 File Offset: 0x00005945
		private ImmutableStack<T> BackwardsReversed
		{
			get
			{
				if (this._backwardsReversed == null)
				{
					this._backwardsReversed = this._backwards.Reverse();
				}
				return this._backwardsReversed;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007766 File Offset: 0x00005966
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._forwards.Peek();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007786 File Offset: 0x00005986
		public ImmutableQueue<T> Enqueue(T value)
		{
			if (this.IsEmpty)
			{
				return new ImmutableQueue<T>(ImmutableStack.Create<T>(value), ImmutableStack<T>.Empty);
			}
			return new ImmutableQueue<T>(this._forwards, this._backwards.Push(value));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000077B8 File Offset: 0x000059B8
		IImmutableQueue<T> IImmutableQueue<T>.Enqueue(T value)
		{
			return this.Enqueue(value);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000077C4 File Offset: 0x000059C4
		public ImmutableQueue<T> Dequeue()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			ImmutableStack<T> immutableStack = this._forwards.Pop();
			if (!immutableStack.IsEmpty)
			{
				return new ImmutableQueue<T>(immutableStack, this._backwards);
			}
			if (this._backwards.IsEmpty)
			{
				return ImmutableQueue<T>.Empty;
			}
			return new ImmutableQueue<T>(this.BackwardsReversed, ImmutableStack<T>.Empty);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00007828 File Offset: 0x00005A28
		public ImmutableQueue<T> Dequeue(out T value)
		{
			value = this.Peek();
			return this.Dequeue();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000783C File Offset: 0x00005A3C
		IImmutableQueue<T> IImmutableQueue<T>.Dequeue()
		{
			return this.Dequeue();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00007844 File Offset: 0x00005A44
		[NullableContext(0)]
		public ImmutableQueue<T>.Enumerator GetEnumerator()
		{
			return new ImmutableQueue<T>.Enumerator(this);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000784C File Offset: 0x00005A4C
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return new ImmutableQueue<T>.EnumeratorObject(this);
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00007874 File Offset: 0x00005A74
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableQueue<T>.EnumeratorObject(this);
		}

		// Token: 0x0400002A RID: 42
		private static readonly ImmutableQueue<T> s_EmptyField = new ImmutableQueue<T>(ImmutableStack<T>.Empty, ImmutableStack<T>.Empty);

		// Token: 0x0400002B RID: 43
		private readonly ImmutableStack<T> _backwards;

		// Token: 0x0400002C RID: 44
		private readonly ImmutableStack<T> _forwards;

		// Token: 0x0400002D RID: 45
		private ImmutableStack<T> _backwardsReversed;

		// Token: 0x02000070 RID: 112
		[Nullable(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator
		{
			// Token: 0x06000559 RID: 1369 RVA: 0x0000E36E File Offset: 0x0000C56E
			internal Enumerator(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
				this._remainingForwardsStack = null;
				this._remainingBackwardsStack = null;
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000E388 File Offset: 0x0000C588
			public T Current
			{
				get
				{
					if (this._remainingForwardsStack == null)
					{
						throw new InvalidOperationException();
					}
					if (!this._remainingForwardsStack.IsEmpty)
					{
						return this._remainingForwardsStack.Peek();
					}
					if (!this._remainingBackwardsStack.IsEmpty)
					{
						return this._remainingBackwardsStack.Peek();
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x0600055B RID: 1371 RVA: 0x0000E3DC File Offset: 0x0000C5DC
			public bool MoveNext()
			{
				if (this._remainingForwardsStack == null)
				{
					this._remainingForwardsStack = this._originalQueue._forwards;
					this._remainingBackwardsStack = this._originalQueue.BackwardsReversed;
				}
				else if (!this._remainingForwardsStack.IsEmpty)
				{
					this._remainingForwardsStack = this._remainingForwardsStack.Pop();
				}
				else if (!this._remainingBackwardsStack.IsEmpty)
				{
					this._remainingBackwardsStack = this._remainingBackwardsStack.Pop();
				}
				return !this._remainingForwardsStack.IsEmpty || !this._remainingBackwardsStack.IsEmpty;
			}

			// Token: 0x040000C5 RID: 197
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x040000C6 RID: 198
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x040000C7 RID: 199
			private ImmutableStack<T> _remainingBackwardsStack;
		}

		// Token: 0x02000071 RID: 113
		private class EnumeratorObject : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600055C RID: 1372 RVA: 0x0000E470 File Offset: 0x0000C670
			internal EnumeratorObject(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000E480 File Offset: 0x0000C680
			public T Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._remainingForwardsStack == null)
					{
						throw new InvalidOperationException();
					}
					if (!this._remainingForwardsStack.IsEmpty)
					{
						return this._remainingForwardsStack.Peek();
					}
					if (!this._remainingBackwardsStack.IsEmpty)
					{
						return this._remainingBackwardsStack.Peek();
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x0600055E RID: 1374 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600055F RID: 1375 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._remainingForwardsStack == null)
				{
					this._remainingForwardsStack = this._originalQueue._forwards;
					this._remainingBackwardsStack = this._originalQueue.BackwardsReversed;
				}
				else if (!this._remainingForwardsStack.IsEmpty)
				{
					this._remainingForwardsStack = this._remainingForwardsStack.Pop();
				}
				else if (!this._remainingBackwardsStack.IsEmpty)
				{
					this._remainingBackwardsStack = this._remainingBackwardsStack.Pop();
				}
				return !this._remainingForwardsStack.IsEmpty || !this._remainingBackwardsStack.IsEmpty;
			}

			// Token: 0x06000560 RID: 1376 RVA: 0x0000E582 File Offset: 0x0000C782
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingBackwardsStack = null;
				this._remainingForwardsStack = null;
			}

			// Token: 0x06000561 RID: 1377 RVA: 0x0000E598 File Offset: 0x0000C798
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x06000562 RID: 1378 RVA: 0x0000E5A1 File Offset: 0x0000C7A1
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableQueue<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x040000C8 RID: 200
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x040000C9 RID: 201
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x040000CA RID: 202
			private ImmutableStack<T> _remainingBackwardsStack;

			// Token: 0x040000CB RID: 203
			private bool _disposed;
		}
	}
}
