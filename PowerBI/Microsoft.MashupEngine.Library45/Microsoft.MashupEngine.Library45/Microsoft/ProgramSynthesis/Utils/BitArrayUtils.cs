using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003F5 RID: 1013
	public static class BitArrayUtils
	{
		// Token: 0x060016F5 RID: 5877 RVA: 0x00046423 File Offset: 0x00044623
		public static IEnumerable<int> GetEnabledIndices(this BitArray bitArray)
		{
			int num;
			for (int i = 0; i < bitArray.Length; i = num + 1)
			{
				if (bitArray[i])
				{
					yield return i;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00046433 File Offset: 0x00044633
		public static BitArray Clone(this BitArray bitArray)
		{
			return new BitArray(bitArray);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0004643B File Offset: 0x0004463B
		public static bool AllFalse(this BitArray bitArray)
		{
			return bitArray.Cast<bool>().All((bool b) => !b);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00046468 File Offset: 0x00044668
		public static bool CoversAll(this IEnumerable<BitArray> collection, BitArray enabled)
		{
			return enabled.GetEnabledIndices().All((int i) => collection.Any((BitArray array) => array[i]));
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0004649C File Offset: 0x0004469C
		public static void AddMaximal(this HashSet<BitArray> collection, BitArray max)
		{
			if (collection.Any((BitArray set) => max.IsSubsetOf(set)))
			{
				throw new Exception();
			}
			collection.RemoveWhere((BitArray set) => set.IsSubsetOf(max));
			collection.Add(max);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000464F0 File Offset: 0x000446F0
		public static bool IsSubsetOf(this BitArray a, BitArray b)
		{
			return a.Clone().And(b.Clone().Not()).AllFalse();
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0004650D File Offset: 0x0004470D
		public static bool HasIntersectionWith(this BitArray a, BitArray b)
		{
			return !a.Clone().And(b).GetEnabledIndices()
				.IsEmpty<int>();
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00046528 File Offset: 0x00044728
		public static bool ContainsSupersetOf(this IEnumerable<BitArray> set, BitArray a)
		{
			return set.Any(new Func<BitArray, bool>(a.IsSubsetOf));
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0004653C File Offset: 0x0004473C
		public static int BitCount(this BitArray bitArray)
		{
			return bitArray.Cast<bool>().Count((bool b) => b);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00046568 File Offset: 0x00044768
		public static bool MaximalContains(this IEnumerable<BitArray> collection, BitArray set)
		{
			return collection.Any((BitArray s) => set.IsSubsetOf(s));
		}
	}
}
