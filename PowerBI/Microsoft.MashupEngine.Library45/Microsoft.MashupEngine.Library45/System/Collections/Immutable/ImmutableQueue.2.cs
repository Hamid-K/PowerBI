using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020AD RID: 8365
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableQueue<[Nullable(2)] T> : IImmutableQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060117FB RID: 71675 RVA: 0x003BF4A4 File Offset: 0x003BD6A4
		internal ImmutableQueue(ImmutableStack<T> forwards, ImmutableStack<T> backwards)
		{
			this._forwards = forwards;
			this._backwards = backwards;
		}

		// Token: 0x060117FC RID: 71676 RVA: 0x003BF4BA File Offset: 0x003BD6BA
		public ImmutableQueue<T> Clear()
		{
			return ImmutableQueue<T>.Empty;
		}

		// Token: 0x17002ED1 RID: 11985
		// (get) Token: 0x060117FD RID: 71677 RVA: 0x003BF4C1 File Offset: 0x003BD6C1
		public bool IsEmpty
		{
			get
			{
				return this._forwards.IsEmpty;
			}
		}

		// Token: 0x17002ED2 RID: 11986
		// (get) Token: 0x060117FE RID: 71678 RVA: 0x003BF4CE File Offset: 0x003BD6CE
		public static ImmutableQueue<T> Empty
		{
			get
			{
				return ImmutableQueue<T>.s_EmptyField;
			}
		}

		// Token: 0x060117FF RID: 71679 RVA: 0x003BF4D5 File Offset: 0x003BD6D5
		IImmutableQueue<T> IImmutableQueue<T>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17002ED3 RID: 11987
		// (get) Token: 0x06011800 RID: 71680 RVA: 0x003BF4DD File Offset: 0x003BD6DD
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

		// Token: 0x06011801 RID: 71681 RVA: 0x003BF4FE File Offset: 0x003BD6FE
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._forwards.Peek();
		}

		// Token: 0x06011802 RID: 71682 RVA: 0x003BF51E File Offset: 0x003BD71E
		public readonly ref T PeekRef()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._forwards.PeekRef();
		}

		// Token: 0x06011803 RID: 71683 RVA: 0x003BF53E File Offset: 0x003BD73E
		public ImmutableQueue<T> Enqueue(T value)
		{
			if (this.IsEmpty)
			{
				return new ImmutableQueue<T>(ImmutableStack.Create<T>(value), ImmutableStack<T>.Empty);
			}
			return new ImmutableQueue<T>(this._forwards, this._backwards.Push(value));
		}

		// Token: 0x06011804 RID: 71684 RVA: 0x003BF570 File Offset: 0x003BD770
		IImmutableQueue<T> IImmutableQueue<T>.Enqueue(T value)
		{
			return this.Enqueue(value);
		}

		// Token: 0x06011805 RID: 71685 RVA: 0x003BF57C File Offset: 0x003BD77C
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

		// Token: 0x06011806 RID: 71686 RVA: 0x003BF5E0 File Offset: 0x003BD7E0
		public ImmutableQueue<T> Dequeue(out T value)
		{
			value = this.Peek();
			return this.Dequeue();
		}

		// Token: 0x06011807 RID: 71687 RVA: 0x003BF5F4 File Offset: 0x003BD7F4
		IImmutableQueue<T> IImmutableQueue<T>.Dequeue()
		{
			return this.Dequeue();
		}

		// Token: 0x06011808 RID: 71688 RVA: 0x003BF5FC File Offset: 0x003BD7FC
		[NullableContext(0)]
		public ImmutableQueue<T>.Enumerator GetEnumerator()
		{
			return new ImmutableQueue<T>.Enumerator(this);
		}

		// Token: 0x06011809 RID: 71689 RVA: 0x003BF604 File Offset: 0x003BD804
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return new ImmutableQueue<T>.EnumeratorObject(this);
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x0601180A RID: 71690 RVA: 0x003BF62C File Offset: 0x003BD82C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableQueue<T>.EnumeratorObject(this);
		}

		// Token: 0x04006949 RID: 26953
		private static readonly ImmutableQueue<T> s_EmptyField = new ImmutableQueue<T>(ImmutableStack<T>.Empty, ImmutableStack<T>.Empty);

		// Token: 0x0400694A RID: 26954
		private readonly ImmutableStack<T> _backwards;

		// Token: 0x0400694B RID: 26955
		private readonly ImmutableStack<T> _forwards;

		// Token: 0x0400694C RID: 26956
		private ImmutableStack<T> _backwardsReversed;

		// Token: 0x020020AE RID: 8366
		[Nullable(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator
		{
			// Token: 0x0601180C RID: 71692 RVA: 0x003BF64A File Offset: 0x003BD84A
			internal Enumerator(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
				this._remainingForwardsStack = null;
				this._remainingBackwardsStack = null;
			}

			// Token: 0x17002ED4 RID: 11988
			// (get) Token: 0x0601180D RID: 71693 RVA: 0x003BF664 File Offset: 0x003BD864
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

			// Token: 0x0601180E RID: 71694 RVA: 0x003BF6B8 File Offset: 0x003BD8B8
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

			// Token: 0x0400694D RID: 26957
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x0400694E RID: 26958
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x0400694F RID: 26959
			private ImmutableStack<T> _remainingBackwardsStack;
		}

		// Token: 0x020020AF RID: 8367
		private class EnumeratorObject : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0601180F RID: 71695 RVA: 0x003BF74C File Offset: 0x003BD94C
			internal EnumeratorObject(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
			}

			// Token: 0x17002ED5 RID: 11989
			// (get) Token: 0x06011810 RID: 71696 RVA: 0x003BF75C File Offset: 0x003BD95C
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

			// Token: 0x17002ED6 RID: 11990
			// (get) Token: 0x06011811 RID: 71697 RVA: 0x003BF7B4 File Offset: 0x003BD9B4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06011812 RID: 71698 RVA: 0x003BF7C4 File Offset: 0x003BD9C4
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

			// Token: 0x06011813 RID: 71699 RVA: 0x003BF85E File Offset: 0x003BDA5E
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingBackwardsStack = null;
				this._remainingForwardsStack = null;
			}

			// Token: 0x06011814 RID: 71700 RVA: 0x003BF874 File Offset: 0x003BDA74
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x06011815 RID: 71701 RVA: 0x003BF87D File Offset: 0x003BDA7D
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableQueue<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x04006950 RID: 26960
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x04006951 RID: 26961
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x04006952 RID: 26962
			private ImmutableStack<T> _remainingBackwardsStack;

			// Token: 0x04006953 RID: 26963
			private bool _disposed;
		}
	}
}
