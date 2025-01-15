using System;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Internal
{
	// Token: 0x0200011F RID: 287
	internal class VersioningTree<TKey, TValue>
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x0000D685 File Offset: 0x0000B885
		public VersioningTree(TKey key, TValue value, VersioningTree<TKey, TValue> leftChild, VersioningTree<TKey, TValue> rightChild)
		{
			this.Key = key;
			this.Value = value;
			this.Height = VersioningTree<TKey, TValue>.Max(VersioningTree<TKey, TValue>.GetHeight(leftChild), VersioningTree<TKey, TValue>.GetHeight(rightChild)) + 1;
			this.LeftChild = leftChild;
			this.RightChild = rightChild;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		public TValue GetValue(TKey key, Func<TKey, TKey, int> compareFunction)
		{
			TValue tvalue;
			if (this.TryGetValue(key, compareFunction, out tvalue))
			{
				return tvalue;
			}
			throw new KeyNotFoundException(key.ToString());
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000D6F4 File Offset: 0x0000B8F4
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

		// Token: 0x06000582 RID: 1410 RVA: 0x0000D744 File Offset: 0x0000B944
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

		// Token: 0x06000583 RID: 1411 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
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

		// Token: 0x06000584 RID: 1412 RVA: 0x0000D9CB File Offset: 0x0000BBCB
		private static VersioningTree<TKey, TValue> SetKeyValue(VersioningTree<TKey, TValue> me, TKey key, TValue value, Func<TKey, TKey, int> compareFunction)
		{
			if (me == null)
			{
				return new VersioningTree<TKey, TValue>(key, value, null, null);
			}
			return me.SetKeyValue(key, value, compareFunction);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000D9E3 File Offset: 0x0000BBE3
		private static int GetHeight(VersioningTree<TKey, TValue> tree)
		{
			if (tree != null)
			{
				return tree.Height;
			}
			return 0;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
		private static int Max(int x, int y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		private VersioningTree<TKey, TValue> MakeLeftmost(VersioningTree<TKey, TValue> leftmost)
		{
			if (this.LeftChild == null)
			{
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, leftmost, this.RightChild);
			}
			return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild.MakeLeftmost(leftmost), this.RightChild);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000DA50 File Offset: 0x0000BC50
		private VersioningTree<TKey, TValue> MakeRightmost(VersioningTree<TKey, TValue> rightmost)
		{
			if (this.RightChild == null)
			{
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild, rightmost);
			}
			return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild, this.RightChild.MakeRightmost(rightmost));
		}

		// Token: 0x040001FB RID: 507
		public readonly TKey Key;

		// Token: 0x040001FC RID: 508
		public readonly TValue Value;

		// Token: 0x040001FD RID: 509
		public readonly int Height;

		// Token: 0x040001FE RID: 510
		public readonly VersioningTree<TKey, TValue> LeftChild;

		// Token: 0x040001FF RID: 511
		public readonly VersioningTree<TKey, TValue> RightChild;
	}
}
