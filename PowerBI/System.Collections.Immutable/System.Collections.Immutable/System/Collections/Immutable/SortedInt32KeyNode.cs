using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000049 RID: 73
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{_key} = {_value}")]
	internal sealed class SortedInt32KeyNode<[Nullable(2)] TValue> : IBinaryTree
	{
		// Token: 0x06000377 RID: 887 RVA: 0x00009339 File Offset: 0x00007539
		private SortedInt32KeyNode()
		{
			this._frozen = true;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00009348 File Offset: 0x00007548
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000093B2 File Offset: 0x000075B2
		public bool IsEmpty
		{
			get
			{
				return this._left == null;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600037A RID: 890 RVA: 0x000093BD File Offset: 0x000075BD
		public int Height
		{
			get
			{
				return (int)this._height;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600037B RID: 891 RVA: 0x000093C5 File Offset: 0x000075C5
		[Nullable(new byte[] { 2, 1 })]
		public SortedInt32KeyNode<TValue> Left
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600037C RID: 892 RVA: 0x000093CD File Offset: 0x000075CD
		[Nullable(new byte[] { 2, 1 })]
		public SortedInt32KeyNode<TValue> Right
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._right;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000093D5 File Offset: 0x000075D5
		[Nullable(2)]
		IBinaryTree IBinaryTree.Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600037E RID: 894 RVA: 0x000093DD File Offset: 0x000075DD
		[Nullable(2)]
		IBinaryTree IBinaryTree.Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000093E5 File Offset: 0x000075E5
		int IBinaryTree.Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000380 RID: 896 RVA: 0x000093EC File Offset: 0x000075EC
		[Nullable(new byte[] { 0, 1 })]
		public KeyValuePair<int, TValue> Value
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return new KeyValuePair<int, TValue>(this._key, this._value);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00009400 File Offset: 0x00007600
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

		// Token: 0x06000382 RID: 898 RVA: 0x0000941D File Offset: 0x0000761D
		[NullableContext(0)]
		public SortedInt32KeyNode<TValue>.Enumerator GetEnumerator()
		{
			return new SortedInt32KeyNode<TValue>.Enumerator(this);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00009425 File Offset: 0x00007625
		internal SortedInt32KeyNode<TValue> SetItem(int key, TValue value, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
		{
			Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
			return this.SetOrAdd(key, value, valueComparer, true, out replacedExistingValue, out mutated);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00009440 File Offset: 0x00007640
		internal SortedInt32KeyNode<TValue> Remove(int key, out bool mutated)
		{
			return this.RemoveRecursive(key, out mutated);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000944C File Offset: 0x0000764C
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

		// Token: 0x06000386 RID: 902 RVA: 0x00009498 File Offset: 0x00007698
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

		// Token: 0x06000387 RID: 903 RVA: 0x000094EC File Offset: 0x000076EC
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

		// Token: 0x06000388 RID: 904 RVA: 0x0000953C File Offset: 0x0000773C
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

		// Token: 0x06000389 RID: 905 RVA: 0x00009580 File Offset: 0x00007780
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

		// Token: 0x0600038A RID: 906 RVA: 0x000095C4 File Offset: 0x000077C4
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

		// Token: 0x0600038B RID: 907 RVA: 0x00009604 File Offset: 0x00007804
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

		// Token: 0x0600038C RID: 908 RVA: 0x00009644 File Offset: 0x00007844
		private static int Balance(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return (int)(tree._right._height - tree._left._height);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00009668 File Offset: 0x00007868
		private static bool IsRightHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) >= 2;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00009681 File Offset: 0x00007881
		private static bool IsLeftHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) <= -2;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000969C File Offset: 0x0000789C
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

		// Token: 0x06000390 RID: 912 RVA: 0x00009700 File Offset: 0x00007900
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

		// Token: 0x06000391 RID: 913 RVA: 0x000097E4 File Offset: 0x000079E4
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

		// Token: 0x06000392 RID: 914 RVA: 0x00009918 File Offset: 0x00007B18
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

		// Token: 0x04000044 RID: 68
		internal static readonly SortedInt32KeyNode<TValue> EmptyNode = new SortedInt32KeyNode<TValue>();

		// Token: 0x04000045 RID: 69
		private readonly int _key;

		// Token: 0x04000046 RID: 70
		private readonly TValue _value;

		// Token: 0x04000047 RID: 71
		private bool _frozen;

		// Token: 0x04000048 RID: 72
		private byte _height;

		// Token: 0x04000049 RID: 73
		private SortedInt32KeyNode<TValue> _left;

		// Token: 0x0400004A RID: 74
		private SortedInt32KeyNode<TValue> _right;

		// Token: 0x0200007C RID: 124
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<KeyValuePair<int, TValue>>, IEnumerator, IDisposable, ISecurePooledObjectUser
		{
			// Token: 0x06000630 RID: 1584 RVA: 0x00010A00 File Offset: 0x0000EC00
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

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000631 RID: 1585 RVA: 0x00010A8A File Offset: 0x0000EC8A
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

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000632 RID: 1586 RVA: 0x00010AAB File Offset: 0x0000ECAB
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x06000633 RID: 1587 RVA: 0x00010AB3 File Offset: 0x0000ECB3
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000634 RID: 1588 RVA: 0x00010AC0 File Offset: 0x0000ECC0
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

			// Token: 0x06000635 RID: 1589 RVA: 0x00010B18 File Offset: 0x0000ED18
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

			// Token: 0x06000636 RID: 1590 RVA: 0x00010B74 File Offset: 0x0000ED74
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

			// Token: 0x06000637 RID: 1591 RVA: 0x00010BB5 File Offset: 0x0000EDB5
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<SortedInt32KeyNode<TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<SortedInt32KeyNode<TValue>.Enumerator>(this);
				}
			}

			// Token: 0x06000638 RID: 1592 RVA: 0x00010BE0 File Offset: 0x0000EDE0
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

			// Token: 0x040000FD RID: 253
			private static readonly SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator>();

			// Token: 0x040000FE RID: 254
			private readonly int _poolUserId;

			// Token: 0x040000FF RID: 255
			private SortedInt32KeyNode<TValue> _root;

			// Token: 0x04000100 RID: 256
			private SecurePooledObject<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>> _stack;

			// Token: 0x04000101 RID: 257
			private SortedInt32KeyNode<TValue> _current;
		}
	}
}
