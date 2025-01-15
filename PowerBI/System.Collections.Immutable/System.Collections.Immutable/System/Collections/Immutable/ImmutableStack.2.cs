using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("IsEmpty = {IsEmpty}; Top = {_head}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableStack<[Nullable(2)] T> : IImmutableStack<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000346 RID: 838 RVA: 0x00008ED2 File Offset: 0x000070D2
		private ImmutableStack()
		{
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00008EDA File Offset: 0x000070DA
		private ImmutableStack(T head, ImmutableStack<T> tail)
		{
			this._head = head;
			this._tail = tail;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00008EF0 File Offset: 0x000070F0
		public static ImmutableStack<T> Empty
		{
			get
			{
				return ImmutableStack<T>.s_EmptyField;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00008EF7 File Offset: 0x000070F7
		public ImmutableStack<T> Clear()
		{
			return ImmutableStack<T>.Empty;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00008EFE File Offset: 0x000070FE
		IImmutableStack<T> IImmutableStack<T>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00008F06 File Offset: 0x00007106
		public bool IsEmpty
		{
			get
			{
				return this._tail == null;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00008F11 File Offset: 0x00007111
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._head;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00008F2C File Offset: 0x0000712C
		public ImmutableStack<T> Push(T value)
		{
			return new ImmutableStack<T>(value, this);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00008F35 File Offset: 0x00007135
		IImmutableStack<T> IImmutableStack<T>.Push(T value)
		{
			return this.Push(value);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00008F3E File Offset: 0x0000713E
		public ImmutableStack<T> Pop()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._tail;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00008F59 File Offset: 0x00007159
		public ImmutableStack<T> Pop(out T value)
		{
			value = this.Peek();
			return this.Pop();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00008F6D File Offset: 0x0000716D
		IImmutableStack<T> IImmutableStack<T>.Pop()
		{
			return this.Pop();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00008F75 File Offset: 0x00007175
		[NullableContext(0)]
		public ImmutableStack<T>.Enumerator GetEnumerator()
		{
			return new ImmutableStack<T>.Enumerator(this);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00008F80 File Offset: 0x00007180
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return new ImmutableStack<T>.EnumeratorObject(this);
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00008FA8 File Offset: 0x000071A8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableStack<T>.EnumeratorObject(this);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00008FB0 File Offset: 0x000071B0
		internal ImmutableStack<T> Reverse()
		{
			ImmutableStack<T> immutableStack = this.Clear();
			ImmutableStack<T> immutableStack2 = this;
			while (!immutableStack2.IsEmpty)
			{
				immutableStack = immutableStack.Push(immutableStack2.Peek());
				immutableStack2 = immutableStack2.Pop();
			}
			return immutableStack;
		}

		// Token: 0x0400003A RID: 58
		private static readonly ImmutableStack<T> s_EmptyField = new ImmutableStack<T>();

		// Token: 0x0400003B RID: 59
		private readonly T _head;

		// Token: 0x0400003C RID: 60
		private readonly ImmutableStack<T> _tail;

		// Token: 0x0200007A RID: 122
		[Nullable(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator
		{
			// Token: 0x06000626 RID: 1574 RVA: 0x00010898 File Offset: 0x0000EA98
			internal Enumerator(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
				this._remainingStack = null;
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000627 RID: 1575 RVA: 0x000108B3 File Offset: 0x0000EAB3
			public T Current
			{
				get
				{
					if (this._remainingStack == null || this._remainingStack.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._remainingStack.Peek();
				}
			}

			// Token: 0x06000628 RID: 1576 RVA: 0x000108DC File Offset: 0x0000EADC
			public bool MoveNext()
			{
				if (this._remainingStack == null)
				{
					this._remainingStack = this._originalStack;
				}
				else if (!this._remainingStack.IsEmpty)
				{
					this._remainingStack = this._remainingStack.Pop();
				}
				return !this._remainingStack.IsEmpty;
			}

			// Token: 0x040000F8 RID: 248
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x040000F9 RID: 249
			private ImmutableStack<T> _remainingStack;
		}

		// Token: 0x0200007B RID: 123
		private class EnumeratorObject : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000629 RID: 1577 RVA: 0x0001092B File Offset: 0x0000EB2B
			internal EnumeratorObject(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x0600062A RID: 1578 RVA: 0x00010945 File Offset: 0x0000EB45
			public T Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._remainingStack == null || this._remainingStack.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._remainingStack.Peek();
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x0600062B RID: 1579 RVA: 0x00010973 File Offset: 0x0000EB73
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x00010980 File Offset: 0x0000EB80
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._remainingStack == null)
				{
					this._remainingStack = this._originalStack;
				}
				else if (!this._remainingStack.IsEmpty)
				{
					this._remainingStack = this._remainingStack.Pop();
				}
				return !this._remainingStack.IsEmpty;
			}

			// Token: 0x0600062D RID: 1581 RVA: 0x000109D5 File Offset: 0x0000EBD5
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingStack = null;
			}

			// Token: 0x0600062E RID: 1582 RVA: 0x000109E4 File Offset: 0x0000EBE4
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x0600062F RID: 1583 RVA: 0x000109ED File Offset: 0x0000EBED
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableStack<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x040000FA RID: 250
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x040000FB RID: 251
			private ImmutableStack<T> _remainingStack;

			// Token: 0x040000FC RID: 252
			private bool _disposed;
		}
	}
}
