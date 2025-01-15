using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020C0 RID: 8384
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("IsEmpty = {IsEmpty}; Top = {_head}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableStack<[Nullable(2)] T> : IImmutableStack<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06011993 RID: 72083 RVA: 0x00002130 File Offset: 0x00000330
		private ImmutableStack()
		{
		}

		// Token: 0x06011994 RID: 72084 RVA: 0x003C3126 File Offset: 0x003C1326
		private ImmutableStack(T head, ImmutableStack<T> tail)
		{
			this._head = head;
			this._tail = tail;
		}

		// Token: 0x17002F36 RID: 12086
		// (get) Token: 0x06011995 RID: 72085 RVA: 0x003C313C File Offset: 0x003C133C
		public static ImmutableStack<T> Empty
		{
			get
			{
				return ImmutableStack<T>.s_EmptyField;
			}
		}

		// Token: 0x06011996 RID: 72086 RVA: 0x003C3143 File Offset: 0x003C1343
		public ImmutableStack<T> Clear()
		{
			return ImmutableStack<T>.Empty;
		}

		// Token: 0x06011997 RID: 72087 RVA: 0x003C314A File Offset: 0x003C134A
		IImmutableStack<T> IImmutableStack<T>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17002F37 RID: 12087
		// (get) Token: 0x06011998 RID: 72088 RVA: 0x003C3152 File Offset: 0x003C1352
		public bool IsEmpty
		{
			get
			{
				return this._tail == null;
			}
		}

		// Token: 0x06011999 RID: 72089 RVA: 0x003C315D File Offset: 0x003C135D
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._head;
		}

		// Token: 0x0601199A RID: 72090 RVA: 0x003C3178 File Offset: 0x003C1378
		public readonly ref T PeekRef()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return ref this._head;
		}

		// Token: 0x0601199B RID: 72091 RVA: 0x003C3193 File Offset: 0x003C1393
		public ImmutableStack<T> Push(T value)
		{
			return new ImmutableStack<T>(value, this);
		}

		// Token: 0x0601199C RID: 72092 RVA: 0x003C319C File Offset: 0x003C139C
		IImmutableStack<T> IImmutableStack<T>.Push(T value)
		{
			return this.Push(value);
		}

		// Token: 0x0601199D RID: 72093 RVA: 0x003C31A5 File Offset: 0x003C13A5
		public ImmutableStack<T> Pop()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._tail;
		}

		// Token: 0x0601199E RID: 72094 RVA: 0x003C31C0 File Offset: 0x003C13C0
		public ImmutableStack<T> Pop(out T value)
		{
			value = this.Peek();
			return this.Pop();
		}

		// Token: 0x0601199F RID: 72095 RVA: 0x003C31D4 File Offset: 0x003C13D4
		IImmutableStack<T> IImmutableStack<T>.Pop()
		{
			return this.Pop();
		}

		// Token: 0x060119A0 RID: 72096 RVA: 0x003C31DC File Offset: 0x003C13DC
		[NullableContext(0)]
		public ImmutableStack<T>.Enumerator GetEnumerator()
		{
			return new ImmutableStack<T>.Enumerator(this);
		}

		// Token: 0x060119A1 RID: 72097 RVA: 0x003C31E4 File Offset: 0x003C13E4
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return new ImmutableStack<T>.EnumeratorObject(this);
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x060119A2 RID: 72098 RVA: 0x003C320C File Offset: 0x003C140C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableStack<T>.EnumeratorObject(this);
		}

		// Token: 0x060119A3 RID: 72099 RVA: 0x003C3214 File Offset: 0x003C1414
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

		// Token: 0x0400698F RID: 27023
		private static readonly ImmutableStack<T> s_EmptyField = new ImmutableStack<T>();

		// Token: 0x04006990 RID: 27024
		private readonly T _head;

		// Token: 0x04006991 RID: 27025
		private readonly ImmutableStack<T> _tail;

		// Token: 0x020020C1 RID: 8385
		[Nullable(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator
		{
			// Token: 0x060119A5 RID: 72101 RVA: 0x003C3255 File Offset: 0x003C1455
			internal Enumerator(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
				this._remainingStack = null;
			}

			// Token: 0x17002F38 RID: 12088
			// (get) Token: 0x060119A6 RID: 72102 RVA: 0x003C3270 File Offset: 0x003C1470
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

			// Token: 0x060119A7 RID: 72103 RVA: 0x003C3298 File Offset: 0x003C1498
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

			// Token: 0x04006992 RID: 27026
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x04006993 RID: 27027
			private ImmutableStack<T> _remainingStack;
		}

		// Token: 0x020020C2 RID: 8386
		private class EnumeratorObject : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060119A8 RID: 72104 RVA: 0x003C32E7 File Offset: 0x003C14E7
			internal EnumeratorObject(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
			}

			// Token: 0x17002F39 RID: 12089
			// (get) Token: 0x060119A9 RID: 72105 RVA: 0x003C3301 File Offset: 0x003C1501
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

			// Token: 0x17002F3A RID: 12090
			// (get) Token: 0x060119AA RID: 72106 RVA: 0x003C332F File Offset: 0x003C152F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060119AB RID: 72107 RVA: 0x003C333C File Offset: 0x003C153C
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

			// Token: 0x060119AC RID: 72108 RVA: 0x003C3391 File Offset: 0x003C1591
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingStack = null;
			}

			// Token: 0x060119AD RID: 72109 RVA: 0x003C33A0 File Offset: 0x003C15A0
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x060119AE RID: 72110 RVA: 0x003C33A9 File Offset: 0x003C15A9
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableStack<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x04006994 RID: 27028
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x04006995 RID: 27029
			private ImmutableStack<T> _remainingStack;

			// Token: 0x04006996 RID: 27030
			private bool _disposed;
		}
	}
}
