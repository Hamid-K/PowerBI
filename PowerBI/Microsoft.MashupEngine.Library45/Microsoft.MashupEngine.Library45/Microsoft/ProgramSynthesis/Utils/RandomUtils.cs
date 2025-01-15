using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004EF RID: 1263
	public static class RandomUtils
	{
		// Token: 0x06001C13 RID: 7187 RVA: 0x00054050 File Offset: 0x00052250
		public static BigInteger Next(this Random random, BigInteger maxValue)
		{
			if (maxValue <= 0L)
			{
				throw new ArgumentException("Upper bound must be greater than 0", "maxValue");
			}
			byte[] array = maxValue.ToByteArray();
			byte[] array2 = new byte[array.Length];
			int num = array.Length - 1;
			int num2 = 1 << RandomUtils.highestBitSet[(int)array[num]] + 1;
			int i = 32;
			while (i > 0)
			{
				random.NextBytes(array2);
				array2[num] = (byte)random.Next(num2);
				BigInteger bigInteger = new BigInteger(array2);
				if (bigInteger < maxValue)
				{
					return bigInteger;
				}
			}
			return maxValue - 1;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000540E0 File Offset: 0x000522E0
		private static int[] PopulateHighestBitSet()
		{
			int[] array = new int[256];
			for (int i = 0; i <= 256; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if ((i & (1 << j)) != 0)
					{
						array[i] = j;
					}
				}
			}
			return array;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00054124 File Offset: 0x00052324
		public static Random NewRandom()
		{
			object obj = RandomUtils.globalLock;
			Random random;
			lock (obj)
			{
				random = new Random(RandomUtils.globalRandom.Next());
			}
			return random;
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x00054170 File Offset: 0x00052370
		public static Random ThreadLocalRandom
		{
			get
			{
				return RandomUtils.threadRandom.Value;
			}
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x0005417C File Offset: 0x0005237C
		public static T Next<T>(this Random random, ICollection<T> xs)
		{
			return xs.ElementAt(random.Next(xs.Count));
		}

		// Token: 0x04000DAD RID: 3501
		private static int[] highestBitSet = RandomUtils.PopulateHighestBitSet();

		// Token: 0x04000DAE RID: 3502
		private static readonly Random globalRandom = new Random();

		// Token: 0x04000DAF RID: 3503
		private static readonly object globalLock = new object();

		// Token: 0x04000DB0 RID: 3504
		private static readonly ThreadLocal<Random> threadRandom = new ThreadLocal<Random>(new Func<Random>(RandomUtils.NewRandom));
	}
}
