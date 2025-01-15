using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000071 RID: 113
	internal class VersioningTree<TKey, TValue>
	{
		// Token: 0x06000241 RID: 577 RVA: 0x000056AB File Offset: 0x000038AB
		public VersioningTree(TKey key, TValue value, VersioningTree<TKey, TValue> leftChild, VersioningTree<TKey, TValue> rightChild)
		{
			this.Key = key;
			this.Value = value;
			this.Height = VersioningTree<TKey, TValue>.Max(VersioningTree<TKey, TValue>.GetHeight(leftChild), VersioningTree<TKey, TValue>.GetHeight(rightChild)) + 1;
			this.LeftChild = leftChild;
			this.RightChild = rightChild;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000056EC File Offset: 0x000038EC
		public TValue GetValue(TKey key, Func<TKey, TKey, int> compareFunction)
		{
			TValue tvalue;
			if (this.TryGetValue(key, compareFunction, out tvalue))
			{
				return tvalue;
			}
			throw new KeyNotFoundException(key.ToString());
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000571C File Offset: 0x0000391C
		public bool TryGetValue(TKey key, Func<TKey, TKey, int> compareFunction, out TValue value)
		{
			int num;
			for (VersioningTree<TKey, TValue> versioningTree = this; versioningTree != null; versioningTree = ((num < 0) ? versioningTree.LeftChild : versioningTree.RightChild))
			{
				num = compareFunction(key, versioningTree.Key);
				if (num == 0)
				{
					value = versioningTree.Value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000576C File Offset: 0x0000396C
		public VersioningTree<TKey, TValue> SetKeyValue(TKey key, TValue value, Func<TKey, TKey, int> compareFunction)
		{
			VersioningTree<TKey, TValue> leftChild = this.LeftChild;
			VersioningTree<TKey, TValue> rightChild = this.RightChild;
			int num = compareFunction(key, this.Key);
			if (num < 0)
			{
				if (VersioningTree<TKey, TValue>.GetHeight(leftChild) > VersioningTree<TKey, TValue>.GetHeight(rightChild))
				{
					int num2 = compareFunction(key, leftChild.Key);
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
					int num3 = compareFunction(key, rightChild.Key);
					VersioningTree<TKey, TValue> versioningTree3 = new VersioningTree<TKey, TValue>(this.Key, this.Value, leftChild, (num3 < 0) ? VersioningTree<TKey, TValue>.SetKeyValue(rightChild.LeftChild, key, value, compareFunction) : rightChild.LeftChild);
					VersioningTree<TKey, TValue> versioningTree4 = ((num3 > 0) ? VersioningTree<TKey, TValue>.SetKeyValue(rightChild.RightChild, key, value, compareFunction) : rightChild.RightChild);
					return new VersioningTree<TKey, TValue>((num3 == 0) ? key : rightChild.Key, (num3 == 0) ? value : rightChild.Value, versioningTree3, versioningTree4);
				}
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, leftChild, VersioningTree<TKey, TValue>.SetKeyValue(rightChild, key, value, compareFunction));
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000058F8 File Offset: 0x00003AF8
		public VersioningTree<TKey, TValue> Remove(TKey key, Func<TKey, TKey, int> compareFunction)
		{
			int num = compareFunction(key, this.Key);
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

		// Token: 0x06000246 RID: 582 RVA: 0x000059F3 File Offset: 0x00003BF3
		private static VersioningTree<TKey, TValue> SetKeyValue(VersioningTree<TKey, TValue> me, TKey key, TValue value, Func<TKey, TKey, int> compareFunction)
		{
			if (me == null)
			{
				return new VersioningTree<TKey, TValue>(key, value, null, null);
			}
			return me.SetKeyValue(key, value, compareFunction);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00005A0B File Offset: 0x00003C0B
		private static int GetHeight(VersioningTree<TKey, TValue> tree)
		{
			if (tree != null)
			{
				return tree.Height;
			}
			return 0;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00005A18 File Offset: 0x00003C18
		private static int Max(int x, int y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00005A24 File Offset: 0x00003C24
		private VersioningTree<TKey, TValue> MakeLeftmost(VersioningTree<TKey, TValue> leftmost)
		{
			if (this.LeftChild == null)
			{
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, leftmost, this.RightChild);
			}
			return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild.MakeLeftmost(leftmost), this.RightChild);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00005A78 File Offset: 0x00003C78
		private VersioningTree<TKey, TValue> MakeRightmost(VersioningTree<TKey, TValue> rightmost)
		{
			if (this.RightChild == null)
			{
				return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild, rightmost);
			}
			return new VersioningTree<TKey, TValue>(this.Key, this.Value, this.LeftChild, this.RightChild.MakeRightmost(rightmost));
		}

		// Token: 0x040000CD RID: 205
		public readonly TKey Key;

		// Token: 0x040000CE RID: 206
		public readonly TValue Value;

		// Token: 0x040000CF RID: 207
		public readonly int Height;

		// Token: 0x040000D0 RID: 208
		public readonly VersioningTree<TKey, TValue> LeftChild;

		// Token: 0x040000D1 RID: 209
		public readonly VersioningTree<TKey, TValue> RightChild;
	}
}
