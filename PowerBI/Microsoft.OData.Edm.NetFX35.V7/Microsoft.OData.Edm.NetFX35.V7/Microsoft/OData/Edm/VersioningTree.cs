using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200000E RID: 14
	internal class VersioningTree<TKey, TValue>
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002C07 File Offset: 0x00000E07
		public VersioningTree(TKey key, TValue value, VersioningTree<TKey, TValue> leftChild, VersioningTree<TKey, TValue> rightChild)
		{
			this.Key = key;
			this.Value = value;
			this.Height = VersioningTree<TKey, TValue>.Max(VersioningTree<TKey, TValue>.GetHeight(leftChild), VersioningTree<TKey, TValue>.GetHeight(rightChild)) + 1;
			this.LeftChild = leftChild;
			this.RightChild = rightChild;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C48 File Offset: 0x00000E48
		public TValue GetValue(TKey key, Func<TKey, TKey, int> compareFunction)
		{
			TValue tvalue;
			if (this.TryGetValue(key, compareFunction, out tvalue))
			{
				return tvalue;
			}
			throw new KeyNotFoundException(key.ToString());
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C78 File Offset: 0x00000E78
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

		// Token: 0x0600003B RID: 59 RVA: 0x00002CC8 File Offset: 0x00000EC8
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

		// Token: 0x0600003C RID: 60 RVA: 0x00002E54 File Offset: 0x00001054
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

		// Token: 0x0600003D RID: 61 RVA: 0x00002F4F File Offset: 0x0000114F
		private static VersioningTree<TKey, TValue> SetKeyValue(VersioningTree<TKey, TValue> me, TKey key, TValue value, Func<TKey, TKey, int> compareFunction)
		{
			if (me == null)
			{
				return new VersioningTree<TKey, TValue>(key, value, null, null);
			}
			return me.SetKeyValue(key, value, compareFunction);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002F67 File Offset: 0x00001167
		private static int GetHeight(VersioningTree<TKey, TValue> tree)
		{
			if (tree != null)
			{
				return tree.Height;
			}
			return 0;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F74 File Offset: 0x00001174
		private static int Max(int x, int y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F80 File Offset: 0x00001180
		private VersioningTree<TKey, TValue> MakeLeftmost(VersioningTree<TKey, TValue> leftmost)
		{
			if (this.LeftChild == null)
			{
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, leftmost, this.RightChild);
			}
			return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild.MakeLeftmost(leftmost), this.RightChild);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002FD4 File Offset: 0x000011D4
		private VersioningTree<TKey, TValue> MakeRightmost(VersioningTree<TKey, TValue> rightmost)
		{
			if (this.RightChild == null)
			{
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild, rightmost);
			}
			return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild, this.RightChild.MakeRightmost(rightmost));
		}

		// Token: 0x04000014 RID: 20
		public readonly TKey Key;

		// Token: 0x04000015 RID: 21
		public readonly TValue Value;

		// Token: 0x04000016 RID: 22
		public readonly int Height;

		// Token: 0x04000017 RID: 23
		public readonly VersioningTree<TKey, TValue> LeftChild;

		// Token: 0x04000018 RID: 24
		public readonly VersioningTree<TKey, TValue> RightChild;
	}
}
