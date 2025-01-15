using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000167 RID: 359
	internal class VersioningTree<TKey, TValue>
	{
		// Token: 0x060006C0 RID: 1728 RVA: 0x0000FC31 File Offset: 0x0000DE31
		public VersioningTree(TKey key, TValue value, VersioningTree<TKey, TValue> leftChild, VersioningTree<TKey, TValue> rightChild)
		{
			this.Key = key;
			this.Value = value;
			this.Height = VersioningTree<TKey, TValue>.Max(VersioningTree<TKey, TValue>.GetHeight(leftChild), VersioningTree<TKey, TValue>.GetHeight(rightChild)) + 1;
			this.LeftChild = leftChild;
			this.RightChild = rightChild;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000FC70 File Offset: 0x0000DE70
		public TValue GetValue(TKey key, Func<TKey, TKey, int> compareFunction)
		{
			TValue tvalue;
			if (this.TryGetValue(key, compareFunction, out tvalue))
			{
				return tvalue;
			}
			throw new KeyNotFoundException(key.ToString());
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000FCA0 File Offset: 0x0000DEA0
		public bool TryGetValue(TKey key, Func<TKey, TKey, int> compareFunction, out TValue value)
		{
			int num;
			for (VersioningTree<TKey, TValue> versioningTree = this; versioningTree != null; versioningTree = ((num < 0) ? versioningTree.LeftChild : versioningTree.RightChild))
			{
				num = compareFunction.Invoke(key, versioningTree.Key);
				if (num == 0)
				{
					value = versioningTree.Value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public VersioningTree<TKey, TValue> SetKeyValue(TKey key, TValue value, Func<TKey, TKey, int> compareFunction)
		{
			VersioningTree<TKey, TValue> leftChild = this.LeftChild;
			VersioningTree<TKey, TValue> rightChild = this.RightChild;
			int num = compareFunction.Invoke(key, this.Key);
			if (num < 0)
			{
				if (VersioningTree<TKey, TValue>.GetHeight(leftChild) > VersioningTree<TKey, TValue>.GetHeight(rightChild))
				{
					int num2 = compareFunction.Invoke(key, leftChild.Key);
					VersioningTree<TKey, TValue> versioningTree = ((num2 < 0) ? VersioningTree<TKey, TValue>.SetKeyValue(leftChild.LeftChild, key, value, compareFunction) : leftChild.LeftChild);
					VersioningTree<TKey, TValue> versioningTree2 = new VersioningTree<TKey, TValue>(this.Key, this.Value, (num2 > 0) ? VersioningTree<TKey, TValue>.SetKeyValue(leftChild.RightChild, key, value, compareFunction) : leftChild.RightChild, rightChild);
					return new VersioningTree<TKey, TValue>((num2 == 0) ? key : leftChild.Key, (num2 == 0) ? value : leftChild.Value, versioningTree, versioningTree2);
				}
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, VersioningTree<TKey, TValue>.SetKeyValue(leftChild, key, value, compareFunction), rightChild);
			}
			else
			{
				if (num == 0)
				{
					return new VersioningTree<TKey, TValue>(key, value, leftChild, rightChild);
				}
				if (VersioningTree<TKey, TValue>.GetHeight(leftChild) < VersioningTree<TKey, TValue>.GetHeight(rightChild))
				{
					int num3 = compareFunction.Invoke(key, rightChild.Key);
					VersioningTree<TKey, TValue> versioningTree3 = new VersioningTree<TKey, TValue>(this.Key, this.Value, leftChild, (num3 < 0) ? VersioningTree<TKey, TValue>.SetKeyValue(rightChild.LeftChild, key, value, compareFunction) : rightChild.LeftChild);
					VersioningTree<TKey, TValue> versioningTree4 = ((num3 > 0) ? VersioningTree<TKey, TValue>.SetKeyValue(rightChild.RightChild, key, value, compareFunction) : rightChild.RightChild);
					return new VersioningTree<TKey, TValue>((num3 == 0) ? key : rightChild.Key, (num3 == 0) ? value : rightChild.Value, versioningTree3, versioningTree4);
				}
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, leftChild, VersioningTree<TKey, TValue>.SetKeyValue(rightChild, key, value, compareFunction));
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000FE7C File Offset: 0x0000E07C
		public VersioningTree<TKey, TValue> Remove(TKey key, Func<TKey, TKey, int> compareFunction)
		{
			int num = compareFunction.Invoke(key, this.Key);
			if (num < 0)
			{
				if (this.LeftChild == null)
				{
					throw new KeyNotFoundException(key.ToString());
				}
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild.Remove(key, compareFunction), this.RightChild);
			}
			else if (num == 0)
			{
				if (this.LeftChild == null)
				{
					return this.RightChild;
				}
				if (this.RightChild == null)
				{
					return this.LeftChild;
				}
				if (this.LeftChild.Height < this.RightChild.Height)
				{
					return this.LeftChild.MakeRightmost(this.RightChild);
				}
				return this.RightChild.MakeLeftmost(this.LeftChild);
			}
			else
			{
				if (this.RightChild == null)
				{
					throw new KeyNotFoundException(key.ToString());
				}
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild, this.RightChild.Remove(key, compareFunction));
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000FF77 File Offset: 0x0000E177
		private static VersioningTree<TKey, TValue> SetKeyValue(VersioningTree<TKey, TValue> me, TKey key, TValue value, Func<TKey, TKey, int> compareFunction)
		{
			if (me == null)
			{
				return new VersioningTree<TKey, TValue>(key, value, null, null);
			}
			return me.SetKeyValue(key, value, compareFunction);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000FF8F File Offset: 0x0000E18F
		private static int GetHeight(VersioningTree<TKey, TValue> tree)
		{
			if (tree != null)
			{
				return tree.Height;
			}
			return 0;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000FF9C File Offset: 0x0000E19C
		private static int Max(int x, int y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		private VersioningTree<TKey, TValue> MakeLeftmost(VersioningTree<TKey, TValue> leftmost)
		{
			if (this.LeftChild == null)
			{
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, leftmost, this.RightChild);
			}
			return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild.MakeLeftmost(leftmost), this.RightChild);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		private VersioningTree<TKey, TValue> MakeRightmost(VersioningTree<TKey, TValue> rightmost)
		{
			if (this.RightChild == null)
			{
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild, rightmost);
			}
			return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild, this.RightChild.MakeRightmost(rightmost));
		}

		// Token: 0x040002B6 RID: 694
		public readonly TKey Key;

		// Token: 0x040002B7 RID: 695
		public readonly TValue Value;

		// Token: 0x040002B8 RID: 696
		public readonly int Height;

		// Token: 0x040002B9 RID: 697
		public readonly VersioningTree<TKey, TValue> LeftChild;

		// Token: 0x040002BA RID: 698
		public readonly VersioningTree<TKey, TValue> RightChild;
	}
}
