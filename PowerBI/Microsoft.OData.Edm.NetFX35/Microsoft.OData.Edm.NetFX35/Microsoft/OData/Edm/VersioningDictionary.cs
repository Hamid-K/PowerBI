using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200015A RID: 346
	internal abstract class VersioningDictionary<TKey, TValue>
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x0000F316 File Offset: 0x0000D516
		protected VersioningDictionary(Func<TKey, TKey, int> compareFunction)
		{
			this.CompareFunction = compareFunction;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000F325 File Offset: 0x0000D525
		public static VersioningDictionary<TKey, TValue> Create(Func<TKey, TKey, int> compareFunction)
		{
			return new VersioningDictionary<TKey, TValue>.EmptyVersioningDictionary(compareFunction);
		}

		// Token: 0x0600066E RID: 1646
		public abstract VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue);

		// Token: 0x0600066F RID: 1647
		public abstract VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove);

		// Token: 0x06000670 RID: 1648 RVA: 0x0000F330 File Offset: 0x0000D530
		public TValue Get(TKey key)
		{
			TValue tvalue;
			if (this.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			throw new KeyNotFoundException(key.ToString());
		}

		// Token: 0x06000671 RID: 1649
		public abstract bool TryGetValue(TKey key, out TValue value);

		// Token: 0x040002A3 RID: 675
		protected readonly Func<TKey, TKey, int> CompareFunction;

		// Token: 0x0200015B RID: 347
		internal sealed class EmptyVersioningDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000672 RID: 1650 RVA: 0x0000F35C File Offset: 0x0000D55C
			public EmptyVersioningDictionary(Func<TKey, TKey, int> compareFunction)
				: base(compareFunction)
			{
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x0000F365 File Offset: 0x0000D565
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, keyToSet, newValue);
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x0000F374 File Offset: 0x0000D574
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				throw new KeyNotFoundException(keyToRemove.ToString());
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x0000F388 File Offset: 0x0000D588
			public override bool TryGetValue(TKey key, out TValue value)
			{
				value = default(TValue);
				return false;
			}
		}

		// Token: 0x0200015C RID: 348
		internal sealed class OneKeyDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000676 RID: 1654 RVA: 0x0000F392 File Offset: 0x0000D592
			public OneKeyDictionary(Func<TKey, TKey, int> compareFunction, TKey key, TValue value)
				: base(compareFunction)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x0000F3A9 File Offset: 0x0000D5A9
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				if (this.CompareFunction.Invoke(keyToSet, this.key) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, keyToSet, newValue);
				}
				return new VersioningDictionary<TKey, TValue>.TwoKeyDictionary(this.CompareFunction, this.key, this.value, keyToSet, newValue);
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x0000F3E6 File Offset: 0x0000D5E6
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				if (this.CompareFunction.Invoke(keyToRemove, this.key) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.EmptyVersioningDictionary(this.CompareFunction);
				}
				throw new KeyNotFoundException(keyToRemove.ToString());
			}

			// Token: 0x06000679 RID: 1657 RVA: 0x0000F41A File Offset: 0x0000D61A
			public override bool TryGetValue(TKey key, out TValue value)
			{
				if (this.CompareFunction.Invoke(key, this.key) == 0)
				{
					value = this.value;
					return true;
				}
				value = default(TValue);
				return false;
			}

			// Token: 0x040002A4 RID: 676
			private readonly TKey key;

			// Token: 0x040002A5 RID: 677
			private readonly TValue value;
		}

		// Token: 0x0200015D RID: 349
		internal sealed class TwoKeyDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x0600067A RID: 1658 RVA: 0x0000F446 File Offset: 0x0000D646
			public TwoKeyDictionary(Func<TKey, TKey, int> compareFunction, TKey firstKey, TValue firstValue, TKey secondKey, TValue secondValue)
				: base(compareFunction)
			{
				this.firstKey = firstKey;
				this.firstValue = firstValue;
				this.secondKey = secondKey;
				this.secondValue = secondValue;
			}

			// Token: 0x0600067B RID: 1659 RVA: 0x0000F470 File Offset: 0x0000D670
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				if (this.CompareFunction.Invoke(keyToSet, this.firstKey) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.TwoKeyDictionary(this.CompareFunction, keyToSet, newValue, this.secondKey, this.secondValue);
				}
				if (this.CompareFunction.Invoke(keyToSet, this.secondKey) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.TwoKeyDictionary(this.CompareFunction, this.firstKey, this.firstValue, keyToSet, newValue);
				}
				return new VersioningDictionary<TKey, TValue>.TreeDictionary(this.CompareFunction, this.firstKey, this.firstValue, this.secondKey, this.secondValue, keyToSet, newValue);
			}

			// Token: 0x0600067C RID: 1660 RVA: 0x0000F500 File Offset: 0x0000D700
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				if (this.CompareFunction.Invoke(keyToRemove, this.firstKey) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, this.secondKey, this.secondValue);
				}
				if (this.CompareFunction.Invoke(keyToRemove, this.secondKey) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, this.firstKey, this.firstValue);
				}
				throw new KeyNotFoundException(keyToRemove.ToString());
			}

			// Token: 0x0600067D RID: 1661 RVA: 0x0000F578 File Offset: 0x0000D778
			public override bool TryGetValue(TKey key, out TValue value)
			{
				if (this.CompareFunction.Invoke(key, this.firstKey) == 0)
				{
					value = this.firstValue;
					return true;
				}
				if (this.CompareFunction.Invoke(key, this.secondKey) == 0)
				{
					value = this.secondValue;
					return true;
				}
				value = default(TValue);
				return false;
			}

			// Token: 0x040002A6 RID: 678
			private readonly TKey firstKey;

			// Token: 0x040002A7 RID: 679
			private readonly TValue firstValue;

			// Token: 0x040002A8 RID: 680
			private readonly TKey secondKey;

			// Token: 0x040002A9 RID: 681
			private readonly TValue secondValue;
		}

		// Token: 0x0200015E RID: 350
		internal sealed class TreeDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x0600067E RID: 1662 RVA: 0x0000F5D1 File Offset: 0x0000D7D1
			public TreeDictionary(Func<TKey, TKey, int> compareFunction, TKey firstKey, TValue firstValue, TKey secondKey, TValue secondValue, TKey thirdKey, TValue thirdValue)
				: base(compareFunction)
			{
				this.tree = new VersioningTree<TKey, TValue>(firstKey, firstValue, null, null).SetKeyValue(secondKey, secondValue, this.CompareFunction).SetKeyValue(thirdKey, thirdValue, this.CompareFunction);
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x0000F607 File Offset: 0x0000D807
			public TreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue> tree)
				: base(compareFunction)
			{
				this.tree = tree;
			}

			// Token: 0x06000680 RID: 1664 RVA: 0x0000F618 File Offset: 0x0000D818
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				if (this.tree.Height > 10)
				{
					return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.tree, keyToSet, newValue);
				}
				return new VersioningDictionary<TKey, TValue>.TreeDictionary(this.CompareFunction, this.tree.SetKeyValue(keyToSet, newValue, this.CompareFunction));
			}

			// Token: 0x06000681 RID: 1665 RVA: 0x0000F666 File Offset: 0x0000D866
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				return new VersioningDictionary<TKey, TValue>.TreeDictionary(this.CompareFunction, this.tree.Remove(keyToRemove, this.CompareFunction));
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x0000F685 File Offset: 0x0000D885
			public override bool TryGetValue(TKey key, out TValue value)
			{
				if (this.tree == null)
				{
					value = default(TValue);
					return false;
				}
				return this.tree.TryGetValue(key, this.CompareFunction, out value);
			}

			// Token: 0x040002AA RID: 682
			private const int MaxTreeHeight = 10;

			// Token: 0x040002AB RID: 683
			private readonly VersioningTree<TKey, TValue> tree;
		}

		// Token: 0x0200015F RID: 351
		internal sealed class HashTreeDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000683 RID: 1667 RVA: 0x0000F6AB File Offset: 0x0000D8AB
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue> tree, TKey key, TValue value)
				: base(compareFunction)
			{
				this.treeBuckets = new VersioningTree<TKey, TValue>[17];
				this.SetKeyValues(tree);
				this.SetKeyValue(key, value);
			}

			// Token: 0x06000684 RID: 1668 RVA: 0x0000F6D1 File Offset: 0x0000D8D1
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue>[] trees, TKey key, TValue value)
				: base(compareFunction)
			{
				this.treeBuckets = (VersioningTree<TKey, TValue>[])trees.Clone();
				this.SetKeyValue(key, value);
			}

			// Token: 0x06000685 RID: 1669 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue>[] trees, TKey key)
				: base(compareFunction)
			{
				this.treeBuckets = (VersioningTree<TKey, TValue>[])trees.Clone();
				this.RemoveKey(key);
			}

			// Token: 0x06000686 RID: 1670 RVA: 0x0000F715 File Offset: 0x0000D915
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.treeBuckets, keyToSet, newValue);
			}

			// Token: 0x06000687 RID: 1671 RVA: 0x0000F72A File Offset: 0x0000D92A
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.treeBuckets, keyToRemove);
			}

			// Token: 0x06000688 RID: 1672 RVA: 0x0000F740 File Offset: 0x0000D940
			public override bool TryGetValue(TKey key, out TValue value)
			{
				VersioningTree<TKey, TValue> versioningTree = this.treeBuckets[VersioningDictionary<TKey, TValue>.HashTreeDictionary.GetBucket(key)];
				if (versioningTree == null)
				{
					value = default(TValue);
					return false;
				}
				return versioningTree.TryGetValue(key, this.CompareFunction, out value);
			}

			// Token: 0x06000689 RID: 1673 RVA: 0x0000F778 File Offset: 0x0000D978
			private void SetKeyValue(TKey keyToSet, TValue newValue)
			{
				int bucket = VersioningDictionary<TKey, TValue>.HashTreeDictionary.GetBucket(keyToSet);
				if (this.treeBuckets[bucket] == null)
				{
					this.treeBuckets[bucket] = new VersioningTree<TKey, TValue>(keyToSet, newValue, null, null);
					return;
				}
				this.treeBuckets[bucket] = this.treeBuckets[bucket].SetKeyValue(keyToSet, newValue, this.CompareFunction);
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x0000F7C5 File Offset: 0x0000D9C5
			private void SetKeyValues(VersioningTree<TKey, TValue> tree)
			{
				if (tree == null)
				{
					return;
				}
				this.SetKeyValue(tree.Key, tree.Value);
				this.SetKeyValues(tree.LeftChild);
				this.SetKeyValues(tree.RightChild);
			}

			// Token: 0x0600068B RID: 1675 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
			private void RemoveKey(TKey keyToRemove)
			{
				int bucket = VersioningDictionary<TKey, TValue>.HashTreeDictionary.GetBucket(keyToRemove);
				if (this.treeBuckets[bucket] == null)
				{
					throw new KeyNotFoundException(keyToRemove.ToString());
				}
				this.treeBuckets[bucket] = this.treeBuckets[bucket].Remove(keyToRemove, this.CompareFunction);
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x0000F848 File Offset: 0x0000DA48
			private static int GetBucket(TKey key)
			{
				int num = key.GetHashCode();
				if (num < 0)
				{
					num = -num;
				}
				return num % 17;
			}

			// Token: 0x040002AC RID: 684
			private const int HashSize = 17;

			// Token: 0x040002AD RID: 685
			private readonly VersioningTree<TKey, TValue>[] treeBuckets;
		}
	}
}
