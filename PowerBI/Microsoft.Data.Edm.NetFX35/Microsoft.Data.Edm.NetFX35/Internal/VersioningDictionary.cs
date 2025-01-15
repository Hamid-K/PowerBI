using System;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Internal
{
	// Token: 0x02000112 RID: 274
	internal abstract class VersioningDictionary<TKey, TValue>
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		protected VersioningDictionary(Func<TKey, TKey, int> compareFunction)
		{
			this.CompareFunction = compareFunction;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000CD7B File Offset: 0x0000AF7B
		public static VersioningDictionary<TKey, TValue> Create(Func<TKey, TKey, int> compareFunction)
		{
			return new VersioningDictionary<TKey, TValue>.EmptyVersioningDictionary(compareFunction);
		}

		// Token: 0x0600052D RID: 1325
		public abstract VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue);

		// Token: 0x0600052E RID: 1326
		public abstract VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove);

		// Token: 0x0600052F RID: 1327 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public TValue Get(TKey key)
		{
			TValue tvalue;
			if (this.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			throw new KeyNotFoundException(key.ToString());
		}

		// Token: 0x06000530 RID: 1328
		public abstract bool TryGetValue(TKey key, out TValue value);

		// Token: 0x040001E8 RID: 488
		protected readonly Func<TKey, TKey, int> CompareFunction;

		// Token: 0x02000113 RID: 275
		internal sealed class EmptyVersioningDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000531 RID: 1329 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
			public EmptyVersioningDictionary(Func<TKey, TKey, int> compareFunction)
				: base(compareFunction)
			{
			}

			// Token: 0x06000532 RID: 1330 RVA: 0x0000CDB9 File Offset: 0x0000AFB9
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, keyToSet, newValue);
			}

			// Token: 0x06000533 RID: 1331 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				throw new KeyNotFoundException(keyToRemove.ToString());
			}

			// Token: 0x06000534 RID: 1332 RVA: 0x0000CDDC File Offset: 0x0000AFDC
			public override bool TryGetValue(TKey key, out TValue value)
			{
				value = default(TValue);
				return false;
			}
		}

		// Token: 0x02000114 RID: 276
		internal sealed class OneKeyDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000535 RID: 1333 RVA: 0x0000CDE6 File Offset: 0x0000AFE6
			public OneKeyDictionary(Func<TKey, TKey, int> compareFunction, TKey key, TValue value)
				: base(compareFunction)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06000536 RID: 1334 RVA: 0x0000CDFD File Offset: 0x0000AFFD
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				if (this.CompareFunction.Invoke(keyToSet, this.key) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, keyToSet, newValue);
				}
				return new VersioningDictionary<TKey, TValue>.TwoKeyDictionary(this.CompareFunction, this.key, this.value, keyToSet, newValue);
			}

			// Token: 0x06000537 RID: 1335 RVA: 0x0000CE3A File Offset: 0x0000B03A
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				if (this.CompareFunction.Invoke(keyToRemove, this.key) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.EmptyVersioningDictionary(this.CompareFunction);
				}
				throw new KeyNotFoundException(keyToRemove.ToString());
			}

			// Token: 0x06000538 RID: 1336 RVA: 0x0000CE6E File Offset: 0x0000B06E
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

			// Token: 0x040001E9 RID: 489
			private readonly TKey key;

			// Token: 0x040001EA RID: 490
			private readonly TValue value;
		}

		// Token: 0x02000115 RID: 277
		internal sealed class TwoKeyDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000539 RID: 1337 RVA: 0x0000CE9A File Offset: 0x0000B09A
			public TwoKeyDictionary(Func<TKey, TKey, int> compareFunction, TKey firstKey, TValue firstValue, TKey secondKey, TValue secondValue)
				: base(compareFunction)
			{
				this.firstKey = firstKey;
				this.firstValue = firstValue;
				this.secondKey = secondKey;
				this.secondValue = secondValue;
			}

			// Token: 0x0600053A RID: 1338 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
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

			// Token: 0x0600053B RID: 1339 RVA: 0x0000CF54 File Offset: 0x0000B154
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

			// Token: 0x0600053C RID: 1340 RVA: 0x0000CFCC File Offset: 0x0000B1CC
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

			// Token: 0x040001EB RID: 491
			private readonly TKey firstKey;

			// Token: 0x040001EC RID: 492
			private readonly TValue firstValue;

			// Token: 0x040001ED RID: 493
			private readonly TKey secondKey;

			// Token: 0x040001EE RID: 494
			private readonly TValue secondValue;
		}

		// Token: 0x02000116 RID: 278
		internal sealed class TreeDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x0600053D RID: 1341 RVA: 0x0000D025 File Offset: 0x0000B225
			public TreeDictionary(Func<TKey, TKey, int> compareFunction, TKey firstKey, TValue firstValue, TKey secondKey, TValue secondValue, TKey thirdKey, TValue thirdValue)
				: base(compareFunction)
			{
				this.tree = new VersioningTree<TKey, TValue>(firstKey, firstValue, null, null).SetKeyValue(secondKey, secondValue, this.CompareFunction).SetKeyValue(thirdKey, thirdValue, this.CompareFunction);
			}

			// Token: 0x0600053E RID: 1342 RVA: 0x0000D05B File Offset: 0x0000B25B
			public TreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue> tree)
				: base(compareFunction)
			{
				this.tree = tree;
			}

			// Token: 0x0600053F RID: 1343 RVA: 0x0000D06C File Offset: 0x0000B26C
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				if (this.tree.Height > 10)
				{
					return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.tree, keyToSet, newValue);
				}
				return new VersioningDictionary<TKey, TValue>.TreeDictionary(this.CompareFunction, this.tree.SetKeyValue(keyToSet, newValue, this.CompareFunction));
			}

			// Token: 0x06000540 RID: 1344 RVA: 0x0000D0BA File Offset: 0x0000B2BA
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				return new VersioningDictionary<TKey, TValue>.TreeDictionary(this.CompareFunction, this.tree.Remove(keyToRemove, this.CompareFunction));
			}

			// Token: 0x06000541 RID: 1345 RVA: 0x0000D0D9 File Offset: 0x0000B2D9
			public override bool TryGetValue(TKey key, out TValue value)
			{
				if (this.tree == null)
				{
					value = default(TValue);
					return false;
				}
				return this.tree.TryGetValue(key, this.CompareFunction, out value);
			}

			// Token: 0x040001EF RID: 495
			private const int MaxTreeHeight = 10;

			// Token: 0x040001F0 RID: 496
			private readonly VersioningTree<TKey, TValue> tree;
		}

		// Token: 0x02000117 RID: 279
		internal sealed class HashTreeDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000542 RID: 1346 RVA: 0x0000D0FF File Offset: 0x0000B2FF
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue> tree, TKey key, TValue value)
				: base(compareFunction)
			{
				this.treeBuckets = new VersioningTree<TKey, TValue>[17];
				this.SetKeyValues(tree);
				this.SetKeyValue(key, value);
			}

			// Token: 0x06000543 RID: 1347 RVA: 0x0000D125 File Offset: 0x0000B325
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue>[] trees, TKey key, TValue value)
				: base(compareFunction)
			{
				this.treeBuckets = (VersioningTree<TKey, TValue>[])trees.Clone();
				this.SetKeyValue(key, value);
			}

			// Token: 0x06000544 RID: 1348 RVA: 0x0000D148 File Offset: 0x0000B348
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue>[] trees, TKey key)
				: base(compareFunction)
			{
				this.treeBuckets = (VersioningTree<TKey, TValue>[])trees.Clone();
				this.RemoveKey(key);
			}

			// Token: 0x06000545 RID: 1349 RVA: 0x0000D169 File Offset: 0x0000B369
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.treeBuckets, keyToSet, newValue);
			}

			// Token: 0x06000546 RID: 1350 RVA: 0x0000D17E File Offset: 0x0000B37E
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.treeBuckets, keyToRemove);
			}

			// Token: 0x06000547 RID: 1351 RVA: 0x0000D194 File Offset: 0x0000B394
			public override bool TryGetValue(TKey key, out TValue value)
			{
				VersioningTree<TKey, TValue> versioningTree = this.treeBuckets[this.GetBucket(key)];
				if (versioningTree == null)
				{
					value = default(TValue);
					return false;
				}
				return versioningTree.TryGetValue(key, this.CompareFunction, out value);
			}

			// Token: 0x06000548 RID: 1352 RVA: 0x0000D1CC File Offset: 0x0000B3CC
			private void SetKeyValue(TKey keyToSet, TValue newValue)
			{
				int bucket = this.GetBucket(keyToSet);
				if (this.treeBuckets[bucket] == null)
				{
					this.treeBuckets[bucket] = new VersioningTree<TKey, TValue>(keyToSet, newValue, null, null);
					return;
				}
				this.treeBuckets[bucket] = this.treeBuckets[bucket].SetKeyValue(keyToSet, newValue, this.CompareFunction);
			}

			// Token: 0x06000549 RID: 1353 RVA: 0x0000D21A File Offset: 0x0000B41A
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

			// Token: 0x0600054A RID: 1354 RVA: 0x0000D24C File Offset: 0x0000B44C
			private void RemoveKey(TKey keyToRemove)
			{
				int bucket = this.GetBucket(keyToRemove);
				if (this.treeBuckets[bucket] == null)
				{
					throw new KeyNotFoundException(keyToRemove.ToString());
				}
				this.treeBuckets[bucket] = this.treeBuckets[bucket].Remove(keyToRemove, this.CompareFunction);
			}

			// Token: 0x0600054B RID: 1355 RVA: 0x0000D29C File Offset: 0x0000B49C
			private int GetBucket(TKey key)
			{
				int num = key.GetHashCode();
				if (num < 0)
				{
					num = -num;
				}
				return num % 17;
			}

			// Token: 0x040001F1 RID: 497
			private const int HashSize = 17;

			// Token: 0x040001F2 RID: 498
			private readonly VersioningTree<TKey, TValue>[] treeBuckets;
		}
	}
}
