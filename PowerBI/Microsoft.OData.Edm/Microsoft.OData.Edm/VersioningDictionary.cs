using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200006F RID: 111
	internal abstract class VersioningDictionary<TKey, TValue>
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00005623 File Offset: 0x00003823
		protected VersioningDictionary(Func<TKey, TKey, int> compareFunction)
		{
			this.CompareFunction = compareFunction;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00005632 File Offset: 0x00003832
		public static VersioningDictionary<TKey, TValue> Create(Func<TKey, TKey, int> compareFunction)
		{
			return new VersioningDictionary<TKey, TValue>.EmptyVersioningDictionary(compareFunction);
		}

		// Token: 0x06000233 RID: 563
		public abstract VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue);

		// Token: 0x06000234 RID: 564
		public abstract VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove);

		// Token: 0x06000235 RID: 565 RVA: 0x0000563C File Offset: 0x0000383C
		public TValue Get(TKey key)
		{
			TValue tvalue;
			if (this.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			throw new KeyNotFoundException(key.ToString());
		}

		// Token: 0x06000236 RID: 566
		public abstract bool TryGetValue(TKey key, out TValue value);

		// Token: 0x040000CC RID: 204
		protected readonly Func<TKey, TKey, int> CompareFunction;

		// Token: 0x02000219 RID: 537
		internal sealed class EmptyVersioningDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000E2B RID: 3627 RVA: 0x00026847 File Offset: 0x00024A47
			public EmptyVersioningDictionary(Func<TKey, TKey, int> compareFunction)
				: base(compareFunction)
			{
			}

			// Token: 0x06000E2C RID: 3628 RVA: 0x00026850 File Offset: 0x00024A50
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, keyToSet, newValue);
			}

			// Token: 0x06000E2D RID: 3629 RVA: 0x0002685F File Offset: 0x00024A5F
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				throw new KeyNotFoundException(keyToRemove.ToString());
			}

			// Token: 0x06000E2E RID: 3630 RVA: 0x00026873 File Offset: 0x00024A73
			public override bool TryGetValue(TKey key, out TValue value)
			{
				value = default(TValue);
				return false;
			}
		}

		// Token: 0x0200021A RID: 538
		internal sealed class OneKeyDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000E2F RID: 3631 RVA: 0x0002687D File Offset: 0x00024A7D
			public OneKeyDictionary(Func<TKey, TKey, int> compareFunction, TKey key, TValue value)
				: base(compareFunction)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06000E30 RID: 3632 RVA: 0x00026894 File Offset: 0x00024A94
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				if (this.CompareFunction(keyToSet, this.key) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, keyToSet, newValue);
				}
				return new VersioningDictionary<TKey, TValue>.TwoKeyDictionary(this.CompareFunction, this.key, this.value, keyToSet, newValue);
			}

			// Token: 0x06000E31 RID: 3633 RVA: 0x000268D1 File Offset: 0x00024AD1
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				if (this.CompareFunction(keyToRemove, this.key) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.EmptyVersioningDictionary(this.CompareFunction);
				}
				throw new KeyNotFoundException(keyToRemove.ToString());
			}

			// Token: 0x06000E32 RID: 3634 RVA: 0x00026905 File Offset: 0x00024B05
			public override bool TryGetValue(TKey key, out TValue value)
			{
				if (this.CompareFunction(key, this.key) == 0)
				{
					value = this.value;
					return true;
				}
				value = default(TValue);
				return false;
			}

			// Token: 0x040007D1 RID: 2001
			private readonly TKey key;

			// Token: 0x040007D2 RID: 2002
			private readonly TValue value;
		}

		// Token: 0x0200021B RID: 539
		internal sealed class TwoKeyDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000E33 RID: 3635 RVA: 0x00026931 File Offset: 0x00024B31
			public TwoKeyDictionary(Func<TKey, TKey, int> compareFunction, TKey firstKey, TValue firstValue, TKey secondKey, TValue secondValue)
				: base(compareFunction)
			{
				this.firstKey = firstKey;
				this.firstValue = firstValue;
				this.secondKey = secondKey;
				this.secondValue = secondValue;
			}

			// Token: 0x06000E34 RID: 3636 RVA: 0x00026958 File Offset: 0x00024B58
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				if (this.CompareFunction(keyToSet, this.firstKey) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.TwoKeyDictionary(this.CompareFunction, keyToSet, newValue, this.secondKey, this.secondValue);
				}
				if (this.CompareFunction(keyToSet, this.secondKey) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.TwoKeyDictionary(this.CompareFunction, this.firstKey, this.firstValue, keyToSet, newValue);
				}
				return new VersioningDictionary<TKey, TValue>.TreeDictionary(this.CompareFunction, this.firstKey, this.firstValue, this.secondKey, this.secondValue, keyToSet, newValue);
			}

			// Token: 0x06000E35 RID: 3637 RVA: 0x000269E8 File Offset: 0x00024BE8
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				if (this.CompareFunction(keyToRemove, this.firstKey) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, this.secondKey, this.secondValue);
				}
				if (this.CompareFunction(keyToRemove, this.secondKey) == 0)
				{
					return new VersioningDictionary<TKey, TValue>.OneKeyDictionary(this.CompareFunction, this.firstKey, this.firstValue);
				}
				throw new KeyNotFoundException(keyToRemove.ToString());
			}

			// Token: 0x06000E36 RID: 3638 RVA: 0x00026A60 File Offset: 0x00024C60
			public override bool TryGetValue(TKey key, out TValue value)
			{
				if (this.CompareFunction(key, this.firstKey) == 0)
				{
					value = this.firstValue;
					return true;
				}
				if (this.CompareFunction(key, this.secondKey) == 0)
				{
					value = this.secondValue;
					return true;
				}
				value = default(TValue);
				return false;
			}

			// Token: 0x040007D3 RID: 2003
			private readonly TKey firstKey;

			// Token: 0x040007D4 RID: 2004
			private readonly TValue firstValue;

			// Token: 0x040007D5 RID: 2005
			private readonly TKey secondKey;

			// Token: 0x040007D6 RID: 2006
			private readonly TValue secondValue;
		}

		// Token: 0x0200021C RID: 540
		internal sealed class TreeDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000E37 RID: 3639 RVA: 0x00026AB9 File Offset: 0x00024CB9
			public TreeDictionary(Func<TKey, TKey, int> compareFunction, TKey firstKey, TValue firstValue, TKey secondKey, TValue secondValue, TKey thirdKey, TValue thirdValue)
				: base(compareFunction)
			{
				this.tree = new VersioningTree<TKey, TValue>(firstKey, firstValue, null, null).SetKeyValue(secondKey, secondValue, this.CompareFunction).SetKeyValue(thirdKey, thirdValue, this.CompareFunction);
			}

			// Token: 0x06000E38 RID: 3640 RVA: 0x00026AEF File Offset: 0x00024CEF
			public TreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue> tree)
				: base(compareFunction)
			{
				this.tree = tree;
			}

			// Token: 0x06000E39 RID: 3641 RVA: 0x00026B00 File Offset: 0x00024D00
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				if (this.tree.Height > 10)
				{
					return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.tree, keyToSet, newValue);
				}
				return new VersioningDictionary<TKey, TValue>.TreeDictionary(this.CompareFunction, this.tree.SetKeyValue(keyToSet, newValue, this.CompareFunction));
			}

			// Token: 0x06000E3A RID: 3642 RVA: 0x00026B4E File Offset: 0x00024D4E
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				return new VersioningDictionary<TKey, TValue>.TreeDictionary(this.CompareFunction, this.tree.Remove(keyToRemove, this.CompareFunction));
			}

			// Token: 0x06000E3B RID: 3643 RVA: 0x00026B6D File Offset: 0x00024D6D
			public override bool TryGetValue(TKey key, out TValue value)
			{
				if (this.tree == null)
				{
					value = default(TValue);
					return false;
				}
				return this.tree.TryGetValue(key, this.CompareFunction, out value);
			}

			// Token: 0x040007D7 RID: 2007
			private const int MaxTreeHeight = 10;

			// Token: 0x040007D8 RID: 2008
			private readonly VersioningTree<TKey, TValue> tree;
		}

		// Token: 0x0200021D RID: 541
		internal sealed class HashTreeDictionary : VersioningDictionary<TKey, TValue>
		{
			// Token: 0x06000E3C RID: 3644 RVA: 0x00026B93 File Offset: 0x00024D93
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue> tree, TKey key, TValue value)
				: base(compareFunction)
			{
				this.treeBuckets = new VersioningTree<TKey, TValue>[17];
				this.SetKeyValues(tree);
				this.SetKeyValue(key, value);
			}

			// Token: 0x06000E3D RID: 3645 RVA: 0x00026BB9 File Offset: 0x00024DB9
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue>[] trees, TKey key, TValue value)
				: base(compareFunction)
			{
				this.treeBuckets = (VersioningTree<TKey, TValue>[])trees.Clone();
				this.SetKeyValue(key, value);
			}

			// Token: 0x06000E3E RID: 3646 RVA: 0x00026BDC File Offset: 0x00024DDC
			public HashTreeDictionary(Func<TKey, TKey, int> compareFunction, VersioningTree<TKey, TValue>[] trees, TKey key)
				: base(compareFunction)
			{
				this.treeBuckets = (VersioningTree<TKey, TValue>[])trees.Clone();
				this.RemoveKey(key);
			}

			// Token: 0x06000E3F RID: 3647 RVA: 0x00026BFD File Offset: 0x00024DFD
			public override VersioningDictionary<TKey, TValue> Set(TKey keyToSet, TValue newValue)
			{
				return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.treeBuckets, keyToSet, newValue);
			}

			// Token: 0x06000E40 RID: 3648 RVA: 0x00026C12 File Offset: 0x00024E12
			public override VersioningDictionary<TKey, TValue> Remove(TKey keyToRemove)
			{
				return new VersioningDictionary<TKey, TValue>.HashTreeDictionary(this.CompareFunction, this.treeBuckets, keyToRemove);
			}

			// Token: 0x06000E41 RID: 3649 RVA: 0x00026C28 File Offset: 0x00024E28
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

			// Token: 0x06000E42 RID: 3650 RVA: 0x00026C60 File Offset: 0x00024E60
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

			// Token: 0x06000E43 RID: 3651 RVA: 0x00026CAD File Offset: 0x00024EAD
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

			// Token: 0x06000E44 RID: 3652 RVA: 0x00026CE0 File Offset: 0x00024EE0
			private void RemoveKey(TKey keyToRemove)
			{
				int bucket = VersioningDictionary<TKey, TValue>.HashTreeDictionary.GetBucket(keyToRemove);
				if (this.treeBuckets[bucket] == null)
				{
					throw new KeyNotFoundException(keyToRemove.ToString());
				}
				this.treeBuckets[bucket] = this.treeBuckets[bucket].Remove(keyToRemove, this.CompareFunction);
			}

			// Token: 0x06000E45 RID: 3653 RVA: 0x00026D30 File Offset: 0x00024F30
			private static int GetBucket(TKey key)
			{
				int num = key.GetHashCode();
				if (num < 0)
				{
					num = -num;
				}
				return num % 17;
			}

			// Token: 0x040007D9 RID: 2009
			private const int HashSize = 17;

			// Token: 0x040007DA RID: 2010
			private readonly VersioningTree<TKey, TValue>[] treeBuckets;
		}
	}
}
