using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020CB RID: 8395
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{_key} = {_value}")]
	internal sealed class SortedInt32KeyNode<[Nullable(2)] TValue> : IBinaryTree
	{
		// Token: 0x060119CF RID: 72143 RVA: 0x003C36CD File Offset: 0x003C18CD
		private SortedInt32KeyNode()
		{
			this._frozen = true;
		}

		// Token: 0x060119D0 RID: 72144 RVA: 0x003C36DC File Offset: 0x003C18DC
		private SortedInt32KeyNode(int key, TValue value, SortedInt32KeyNode<TValue> left, SortedInt32KeyNode<TValue> right, bool frozen = false)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(left, "left");
			Requires.NotNull<SortedInt32KeyNode<TValue>>(right, "right");
			this._key = key;
			this._value = value;
			this._left = left;
			this._right = right;
			this._frozen = frozen;
			this._height = checked(1 + Math.Max(left._height, right._height));
		}

		// Token: 0x17002F42 RID: 12098
		// (get) Token: 0x060119D1 RID: 72145 RVA: 0x003C3746 File Offset: 0x003C1946
		public bool IsEmpty
		{
			get
			{
				return this._left == null;
			}
		}

		// Token: 0x17002F43 RID: 12099
		// (get) Token: 0x060119D2 RID: 72146 RVA: 0x003C3751 File Offset: 0x003C1951
		public int Height
		{
			get
			{
				return (int)this._height;
			}
		}

		// Token: 0x17002F44 RID: 12100
		// (get) Token: 0x060119D3 RID: 72147 RVA: 0x003C3759 File Offset: 0x003C1959
		[Nullable(new byte[] { 2, 1 })]
		public SortedInt32KeyNode<TValue> Left
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._left;
			}
		}

		// Token: 0x17002F45 RID: 12101
		// (get) Token: 0x060119D4 RID: 72148 RVA: 0x003C3761 File Offset: 0x003C1961
		[Nullable(new byte[] { 2, 1 })]
		public SortedInt32KeyNode<TValue> Right
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._right;
			}
		}

		// Token: 0x17002F46 RID: 12102
		// (get) Token: 0x060119D5 RID: 72149 RVA: 0x003C3759 File Offset: 0x003C1959
		[Nullable(2)]
		IBinaryTree IBinaryTree.Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17002F47 RID: 12103
		// (get) Token: 0x060119D6 RID: 72150 RVA: 0x003C3761 File Offset: 0x003C1961
		[Nullable(2)]
		IBinaryTree IBinaryTree.Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x17002F48 RID: 12104
		// (get) Token: 0x060119D7 RID: 72151 RVA: 0x00002C72 File Offset: 0x00000E72
		int IBinaryTree.Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002F49 RID: 12105
		// (get) Token: 0x060119D8 RID: 72152 RVA: 0x003C3769 File Offset: 0x003C1969
		[Nullable(new byte[] { 0, 1 })]
		public KeyValuePair<int, TValue> Value
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return new KeyValuePair<int, TValue>(this._key, this._value);
			}
		}

		// Token: 0x17002F4A RID: 12106
		// (get) Token: 0x060119D9 RID: 72153 RVA: 0x003C377C File Offset: 0x003C197C
		internal IEnumerable<TValue> Values
		{
			get
			{
				foreach (KeyValuePair<int, TValue> keyValuePair in this)
				{
					yield return keyValuePair.Value;
				}
				SortedInt32KeyNode<TValue>.Enumerator enumerator = default(SortedInt32KeyNode<TValue>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060119DA RID: 72154 RVA: 0x003C3799 File Offset: 0x003C1999
		[NullableContext(0)]
		public SortedInt32KeyNode<TValue>.Enumerator GetEnumerator()
		{
			return new SortedInt32KeyNode<TValue>.Enumerator(this);
		}

		// Token: 0x060119DB RID: 72155 RVA: 0x003C37A1 File Offset: 0x003C19A1
		internal SortedInt32KeyNode<TValue> SetItem(int key, TValue value, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
		{
			Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
			return this.SetOrAdd(key, value, valueComparer, true, out replacedExistingValue, out mutated);
		}

		// Token: 0x060119DC RID: 72156 RVA: 0x003C37BC File Offset: 0x003C19BC
		internal SortedInt32KeyNode<TValue> Remove(int key, out bool mutated)
		{
			return this.RemoveRecursive(key, out mutated);
		}

		// Token: 0x060119DD RID: 72157 RVA: 0x003C37C8 File Offset: 0x003C19C8
		[NullableContext(2)]
		internal TValue GetValueOrDefault(int key)
		{
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			while (!sortedInt32KeyNode.IsEmpty)
			{
				if (key == sortedInt32KeyNode._key)
				{
					return sortedInt32KeyNode._value;
				}
				if (key > sortedInt32KeyNode._key)
				{
					sortedInt32KeyNode = sortedInt32KeyNode._right;
				}
				else
				{
					sortedInt32KeyNode = sortedInt32KeyNode._left;
				}
			}
			return default(TValue);
		}

		// Token: 0x060119DE RID: 72158 RVA: 0x003C3814 File Offset: 0x003C1A14
		internal bool TryGetValue(int key, [MaybeNullWhen(false)] out TValue value)
		{
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			while (!sortedInt32KeyNode.IsEmpty)
			{
				if (key == sortedInt32KeyNode._key)
				{
					value = sortedInt32KeyNode._value;
					return true;
				}
				if (key > sortedInt32KeyNode._key)
				{
					sortedInt32KeyNode = sortedInt32KeyNode._right;
				}
				else
				{
					sortedInt32KeyNode = sortedInt32KeyNode._left;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060119DF RID: 72159 RVA: 0x003C3868 File Offset: 0x003C1A68
		internal void Freeze([Nullable(new byte[] { 2, 0, 1 })] Action<KeyValuePair<int, TValue>> freezeAction = null)
		{
			if (!this._frozen)
			{
				if (freezeAction != null)
				{
					freezeAction(new KeyValuePair<int, TValue>(this._key, this._value));
				}
				this._left.Freeze(freezeAction);
				this._right.Freeze(freezeAction);
				this._frozen = true;
			}
		}

		// Token: 0x060119E0 RID: 72160 RVA: 0x003C38B8 File Offset: 0x003C1AB8
		private static SortedInt32KeyNode<TValue> RotateLeft(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._right.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> right = tree._right;
			return right.Mutate(tree.Mutate(null, right._left), null);
		}

		// Token: 0x060119E1 RID: 72161 RVA: 0x003C38FC File Offset: 0x003C1AFC
		private static SortedInt32KeyNode<TValue> RotateRight(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._left.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> left = tree._left;
			return left.Mutate(null, tree.Mutate(left._right, null));
		}

		// Token: 0x060119E2 RID: 72162 RVA: 0x003C3940 File Offset: 0x003C1B40
		private static SortedInt32KeyNode<TValue> DoubleLeft(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._right.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = tree.Mutate(null, SortedInt32KeyNode<TValue>.RotateRight(tree._right));
			return SortedInt32KeyNode<TValue>.RotateLeft(sortedInt32KeyNode);
		}

		// Token: 0x060119E3 RID: 72163 RVA: 0x003C3980 File Offset: 0x003C1B80
		private static SortedInt32KeyNode<TValue> DoubleRight(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._left.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = tree.Mutate(SortedInt32KeyNode<TValue>.RotateLeft(tree._left), null);
			return SortedInt32KeyNode<TValue>.RotateRight(sortedInt32KeyNode);
		}

		// Token: 0x060119E4 RID: 72164 RVA: 0x003C39C0 File Offset: 0x003C1BC0
		private static int Balance(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return (int)(tree._right._height - tree._left._height);
		}

		// Token: 0x060119E5 RID: 72165 RVA: 0x003C39E4 File Offset: 0x003C1BE4
		private static bool IsRightHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) >= 2;
		}

		// Token: 0x060119E6 RID: 72166 RVA: 0x003C39FD File Offset: 0x003C1BFD
		private static bool IsLeftHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) <= -2;
		}

		// Token: 0x060119E7 RID: 72167 RVA: 0x003C3A18 File Offset: 0x003C1C18
		private static SortedInt32KeyNode<TValue> MakeBalanced(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (SortedInt32KeyNode<TValue>.IsRightHeavy(tree))
			{
				if (SortedInt32KeyNode<TValue>.Balance(tree._right) >= 0)
				{
					return SortedInt32KeyNode<TValue>.RotateLeft(tree);
				}
				return SortedInt32KeyNode<TValue>.DoubleLeft(tree);
			}
			else
			{
				if (!SortedInt32KeyNode<TValue>.IsLeftHeavy(tree))
				{
					return tree;
				}
				if (SortedInt32KeyNode<TValue>.Balance(tree._left) <= 0)
				{
					return SortedInt32KeyNode<TValue>.RotateRight(tree);
				}
				return SortedInt32KeyNode<TValue>.DoubleRight(tree);
			}
		}

		// Token: 0x060119E8 RID: 72168 RVA: 0x003C3A7C File Offset: 0x003C1C7C
		private SortedInt32KeyNode<TValue> SetOrAdd(int key, TValue value, IEqualityComparer<TValue> valueComparer, bool overwriteExistingValue, out bool replacedExistingValue, out bool mutated)
		{
			replacedExistingValue = false;
			if (this.IsEmpty)
			{
				mutated = true;
				return new SortedInt32KeyNode<TValue>(key, value, this, this, false);
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			if (key > this._key)
			{
				SortedInt32KeyNode<TValue> sortedInt32KeyNode2 = this._right.SetOrAdd(key, value, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(null, sortedInt32KeyNode2);
				}
			}
			else if (key < this._key)
			{
				SortedInt32KeyNode<TValue> sortedInt32KeyNode3 = this._left.SetOrAdd(key, value, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(sortedInt32KeyNode3, null);
				}
			}
			else
			{
				if (valueComparer.Equals(this._value, value))
				{
					mutated = false;
					return this;
				}
				if (!overwriteExistingValue)
				{
					throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
				}
				mutated = true;
				replacedExistingValue = true;
				sortedInt32KeyNode = new SortedInt32KeyNode<TValue>(key, value, this._left, this._right, false);
			}
			if (!mutated)
			{
				return sortedInt32KeyNode;
			}
			return SortedInt32KeyNode<TValue>.MakeBalanced(sortedInt32KeyNode);
		}

		// Token: 0x060119E9 RID: 72169 RVA: 0x003C3B60 File Offset: 0x003C1D60
		private SortedInt32KeyNode<TValue> RemoveRecursive(int key, out bool mutated)
		{
			if (this.IsEmpty)
			{
				mutated = false;
				return this;
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			if (key == this._key)
			{
				mutated = true;
				if (this._right.IsEmpty && this._left.IsEmpty)
				{
					sortedInt32KeyNode = SortedInt32KeyNode<TValue>.EmptyNode;
				}
				else if (this._right.IsEmpty && !this._left.IsEmpty)
				{
					sortedInt32KeyNode = this._left;
				}
				else if (!this._right.IsEmpty && this._left.IsEmpty)
				{
					sortedInt32KeyNode = this._right;
				}
				else
				{
					SortedInt32KeyNode<TValue> sortedInt32KeyNode2 = this._right;
					while (!sortedInt32KeyNode2._left.IsEmpty)
					{
						sortedInt32KeyNode2 = sortedInt32KeyNode2._left;
					}
					bool flag;
					SortedInt32KeyNode<TValue> sortedInt32KeyNode3 = this._right.Remove(sortedInt32KeyNode2._key, out flag);
					sortedInt32KeyNode = sortedInt32KeyNode2.Mutate(this._left, sortedInt32KeyNode3);
				}
			}
			else if (key < this._key)
			{
				SortedInt32KeyNode<TValue> sortedInt32KeyNode4 = this._left.Remove(key, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(sortedInt32KeyNode4, null);
				}
			}
			else
			{
				SortedInt32KeyNode<TValue> sortedInt32KeyNode5 = this._right.Remove(key, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(null, sortedInt32KeyNode5);
				}
			}
			if (!sortedInt32KeyNode.IsEmpty)
			{
				return SortedInt32KeyNode<TValue>.MakeBalanced(sortedInt32KeyNode);
			}
			return sortedInt32KeyNode;
		}

		// Token: 0x060119EA RID: 72170 RVA: 0x003C3C94 File Offset: 0x003C1E94
		private SortedInt32KeyNode<TValue> Mutate(SortedInt32KeyNode<TValue> left = null, SortedInt32KeyNode<TValue> right = null)
		{
			if (this._frozen)
			{
				return new SortedInt32KeyNode<TValue>(this._key, this._value, left ?? this._left, right ?? this._right, false);
			}
			if (left != null)
			{
				this._left = left;
			}
			if (right != null)
			{
				this._right = right;
			}
			this._height = checked(1 + Math.Max(this._left._height, this._right._height));
			return this;
		}

		// Token: 0x0400699E RID: 27038
		internal static readonly SortedInt32KeyNode<TValue> EmptyNode = new SortedInt32KeyNode<TValue>();

		// Token: 0x0400699F RID: 27039
		private readonly int _key;

		// Token: 0x040069A0 RID: 27040
		private readonly TValue _value;

		// Token: 0x040069A1 RID: 27041
		private bool _frozen;

		// Token: 0x040069A2 RID: 27042
		private byte _height;

		// Token: 0x040069A3 RID: 27043
		private SortedInt32KeyNode<TValue> _left;

		// Token: 0x040069A4 RID: 27044
		private SortedInt32KeyNode<TValue> _right;

		// Token: 0x020020CC RID: 8396
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<KeyValuePair<int, TValue>>, IDisposable, IEnumerator, ISecurePooledObjectUser
		{
			// Token: 0x060119EC RID: 72172 RVA: 0x003C3D18 File Offset: 0x003C1F18
			[NullableContext(1)]
			internal Enumerator(SortedInt32KeyNode<TValue> root)
			{
				Requires.NotNull<SortedInt32KeyNode<TValue>>(root, "root");
				this._root = root;
				this._current = null;
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!this._root.IsEmpty)
				{
					if (!SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
					{
						this._stack = SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>(root.Height));
					}
					this.PushLeft(this._root);
				}
			}

			// Token: 0x17002F4B RID: 12107
			// (get) Token: 0x060119ED RID: 72173 RVA: 0x003C3DA2 File Offset: 0x003C1FA2
			[Nullable(new byte[] { 0, 1 })]
			public KeyValuePair<int, TValue> Current
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					this.ThrowIfDisposed();
					if (this._current != null)
					{
						return this._current.Value;
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17002F4C RID: 12108
			// (get) Token: 0x060119EE RID: 72174 RVA: 0x003C3DC3 File Offset: 0x003C1FC3
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17002F4D RID: 12109
			// (get) Token: 0x060119EF RID: 72175 RVA: 0x003C3DCB File Offset: 0x003C1FCB
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060119F0 RID: 72176 RVA: 0x003C3DD8 File Offset: 0x003C1FD8
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack;
				if (this._stack != null && this._stack.TryUse<SortedInt32KeyNode<TValue>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<SortedInt32KeyNode<TValue>>>();
					SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x060119F1 RID: 72177 RVA: 0x003C3E30 File Offset: 0x003C2030
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._stack != null)
				{
					Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
					if (stack.Count > 0)
					{
						SortedInt32KeyNode<TValue> value = stack.Pop().Value;
						this._current = value;
						this.PushLeft(value.Right);
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x060119F2 RID: 72178 RVA: 0x003C3E8C File Offset: 0x003C208C
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._current = null;
				if (this._stack != null)
				{
					Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
					stack.ClearFastWhenEmpty<RefAsValueType<SortedInt32KeyNode<TValue>>>();
					this.PushLeft(this._root);
				}
			}

			// Token: 0x060119F3 RID: 72179 RVA: 0x003C3ECD File Offset: 0x003C20CD
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<SortedInt32KeyNode<TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<SortedInt32KeyNode<TValue>.Enumerator>(this);
				}
			}

			// Token: 0x060119F4 RID: 72180 RVA: 0x003C3EF8 File Offset: 0x003C20F8
			private void PushLeft(SortedInt32KeyNode<TValue> node)
			{
				Requires.NotNull<SortedInt32KeyNode<TValue>>(node, "node");
				Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<SortedInt32KeyNode<TValue>>(node));
					node = node.Left;
				}
			}

			// Token: 0x040069A5 RID: 27045
			private static readonly SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator>();

			// Token: 0x040069A6 RID: 27046
			private readonly int _poolUserId;

			// Token: 0x040069A7 RID: 27047
			private SortedInt32KeyNode<TValue> _root;

			// Token: 0x040069A8 RID: 27048
			private SecurePooledObject<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>> _stack;

			// Token: 0x040069A9 RID: 27049
			private SortedInt32KeyNode<TValue> _current;
		}
	}
}
