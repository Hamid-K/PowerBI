using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200000C RID: 12
	internal abstract class VersioningDictionary<TKey, TValue>
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002B80 File Offset: 0x00000D80
		protected VersioningDictionary(Func<TKey, TKey, int> compareFunction)
		{
			this.CompareFunction = compareFunction;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B8F File Offset: 0x00000D8F
		public static VersioningDictionary<TKey, TValue> Create(Func<TKey, TKey, int> compareFunction)
		{
			return new VersioningDictionary<TKey, TValue>.EmptyVersioningDictionary(compareFunction);
		}

		// Token: 0x0600002A RID: 42
		public abstract VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue);

		// Token: 0x0600002B RID: 43
		public abstract VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove);

		// Token: 0x0600002C RID: 44 RVA: 0x00002B98 File Offset: 0x00000D98
		public TValue Get(TKey key)
		{
			TValue tvalue;
			if (this.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			throw new KeyNotFoundException(key.ToString());
		}

		// Token: 0x0600002D RID: 45
		public abstract bool TryGetValue(TKey key, out TValue value);

		// Token: 0x04000013 RID: 19
		protected readonly Func<TKey, TKey, int> CompareFunction;

		// Token: 0x02000203 RID: 515
		internal sealed class EmptyVersioningDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000D5A RID: 3418 RVA: 0x000245F4 File Offset: 0x000227F4
			public EmptyVersioningDictionary(Func<TKey, TKey, int> compareFunction)
				: base(compareFunction)
			{
			}

			// Token: 0x06000D5B RID: 3419 RVA: 0x000245FD File Offset: 0x000227FD
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, keyToSet, newValue);
			}

			// Token: 0x06000D5C RID: 3420 RVA: 0x0002460C File Offset: 0x0002280C
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				throw new KeyNotFoundException(keyToRemove.ToString());
			}

			// Token: 0x06000D5D RID: 3421 RVA: 0x00024620 File Offset: 0x00022820
			public override bool TryGetValue(TKey key, out TValue value)
			{
				value = default(TValue);
				return false;
			}
		}

		// Token: 0x02000204 RID: 516
		internal sealed class OneKeyDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000D5E RID: 3422 RVA: 0x0002462A File Offset: 0x0002282A
			public OneKeyDictionary(Func<TKey, TKey, int> compareFunction, TKey key, TValue value)
				: base(compareFunction)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06000D5F RID: 3423 RVA: 0x00024641 File Offset: 0x00022841
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				if (this.CompareFunction.Invoke(keyToSet, this.key) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, keyToSet, newValue);
				}
				return new VersioningDictionary<TKey, TValue>.TwoKeyDictionary(this.CompareFunction, this.key, this.value, keyToSet, newValue);
			}

			// Token: 0x06000D60 RID: 3424 RVA: 0x0002467E File Offset: 0x0002287E
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				if (this.CompareFunction.Invoke(keyToRemove, this.key) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.EmptyVersioningDictionary(this.CompareFunction);
				}
				throw new KeyNotFoundException(keyToRemove.ToString());
			}

			// Token: 0x06000D61 RID: 3425 RVA: 0x000246B2 File Offset: 0x000228B2
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

			// Token: 0x0400074A RID: 1866
			private readonly TKey key;

			// Token: 0x0400074B RID: 1867
			private readonly TValue value;
		}

		// Token: 0x02000205 RID: 517
		internal sealed class TwoKeyDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000D62 RID: 3426 RVA: 0x000246DE File Offset: 0x000228DE
			public TwoKeyDictionary(Func<TKey, TKey, int> compareFunction, TKey firstKey, TValue firstValue, TKey secondKey, TValue secondValue)
				: base(compareFunction)
			{
				this.firstKey = firstKey;
				this.firstValue = firstValue;
				this.secondKey = secondKey;
				this.secondValue = secondValue;
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x00024708 File Offset: 0x00022908
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

			// Token: 0x06000D64 RID: 3428 RVA: 0x00024798 File Offset: 0x00022998
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

			// Token: 0x06000D65 RID: 3429 RVA: 0x00024810 File Offset: 0x00022A10
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

			// Token: 0x0400074C RID: 1868
			private readonly TKey firstKey;

			// Token: 0x0400074D RID: 1869
			private readonly TValue firstValue;

			// Token: 0x0400074E RID: 1870
			private readonly TKey secondKey;

			// Token: 0x0400074F RID: 1871
			private readonly TValue secondValue;
		}

		// Token: 0x02000206 RID: 518
		internal sealed class TreeDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000D66 RID: 3430 RVA: 0x00024869 File Offset: 0x00022A69
			public TreeDictionary(Func<TKey, TKey, int> compareFunction, TKey firstKey, TValue firstValue, TKey secondKey, TValue secondValue, TKey thirdKey, TValue thirdValue)
				: base(compareFunction)
			{
				this.tree = new VersioningTree<TKey, TValue>(firstKey, firstValue, null, null).SetKeyValue(secondKey, secondValue, this.CompareFunction).SetKeyValue(thirdKey, thirdValue, this.CompareFunction);
			}

			// Token: 0x06000D67 RID: 3431 RVA: 0x0002489F File Offset: 0x00022A9F
			public TreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue> tree)
				: base(compareFunction)
			{
				this.tree = tree;
			}

			// Token: 0x06000D68 RID: 3432 RVA: 0x000248B0 File Offset: 0x00022AB0
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				if (this.tree.Height > 10)
				{
					return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.tree, keyToSet, newValue);
				}
				return new VersioningDictionary<TKey, TValue>.TreeDictionary(this.CompareFunction, this.tree.SetKeyValue(keyToSet, newValue, this.CompareFunction));
			}

			// Token: 0x06000D69 RID: 3433 RVA: 0x000248FE File Offset: 0x00022AFE
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				return new VersioningDictionary<TKey, TValue>.TreeDictionary(this.CompareFunction, this.tree.Remove(keyToRemove, this.CompareFunction));
			}

			// Token: 0x06000D6A RID: 3434 RVA: 0x0002491D File Offset: 0x00022B1D
			public override bool TryGetValue(TKey key, out TValue value)
			{
				if (this.tree == null)
				{
					value = default(TValue);
					return false;
				}
				return this.tree.TryGetValue(key, this.CompareFunction, out value);
			}

			// Token: 0x04000750 RID: 1872
			private const int MaxTreeHeight = 10;

			// Token: 0x04000751 RID: 1873
			private readonly VersioningTree<TKey, TValue> tree;
		}

		// Token: 0x02000207 RID: 519
		internal sealed class HashTreeDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000D6B RID: 3435 RVA: 0x00024943 File Offset: 0x00022B43
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue> tree, TKey key, TValue value)
				: base(compareFunction)
			{
				this.treeBuckets = new VersioningTree<TKey, TValue>[17];
				this.SetKeyValues(tree);
				this.SetKeyValue(key, value);
			}

			// Token: 0x06000D6C RID: 3436 RVA: 0x00024969 File Offset: 0x00022B69
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue>[] trees, TKey key, TValue value)
				: base(compareFunction)
			{
				this.treeBuckets = (VersioningTree<TKey, TValue>[])trees.Clone();
				this.SetKeyValue(key, value);
			}

			// Token: 0x06000D6D RID: 3437 RVA: 0x0002498C File Offset: 0x00022B8C
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue>[] trees, TKey key)
				: base(compareFunction)
			{
				this.treeBuckets = (VersioningTree<TKey, TValue>[])trees.Clone();
				this.RemoveKey(key);
			}

			// Token: 0x06000D6E RID: 3438 RVA: 0x000249AD File Offset: 0x00022BAD
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.treeBuckets, keyToSet, newValue);
			}

			// Token: 0x06000D6F RID: 3439 RVA: 0x000249C2 File Offset: 0x00022BC2
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.treeBuckets, keyToRemove);
			}

			// Token: 0x06000D70 RID: 3440 RVA: 0x000249D8 File Offset: 0x00022BD8
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

			// Token: 0x06000D71 RID: 3441 RVA: 0x00024A10 File Offset: 0x00022C10
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

			// Token: 0x06000D72 RID: 3442 RVA: 0x00024A5D File Offset: 0x00022C5D
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

			// Token: 0x06000D73 RID: 3443 RVA: 0x00024A90 File Offset: 0x00022C90
			private void RemoveKey(TKey keyToRemove)
			{
				int bucket = VersioningDictionary<TKey, TValue>.HashTreeDictionary.GetBucket(keyToRemove);
				if (this.treeBuckets[bucket] == null)
				{
					throw new KeyNotFoundException(keyToRemove.ToString());
				}
				this.treeBuckets[bucket] = this.treeBuckets[bucket].Remove(keyToRemove, this.CompareFunction);
			}

			// Token: 0x06000D74 RID: 3444 RVA: 0x00024AE0 File Offset: 0x00022CE0
			private static int GetBucket(TKey key)
			{
				int num = key.GetHashCode();
				if (num < 0)
				{
					num = -num;
				}
				return num % 17;
			}

			// Token: 0x04000752 RID: 1874
			private const int HashSize = 17;

			// Token: 0x04000753 RID: 1875
			private readonly VersioningTree<TKey, TValue>[] treeBuckets;
		}
	}
}
