using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004AE RID: 1198
	public static class MathUtils
	{
		// Token: 0x06001AD0 RID: 6864 RVA: 0x00050DA0 File Offset: 0x0004EFA0
		public static IEnumerable<int> GetDivisors(this int number)
		{
			HashSet<int> was = new HashSet<int>();
			int bound = (int)Math.Sqrt((double)number) + 1;
			int num;
			for (int i = 1; i <= bound; i = num)
			{
				if (number % i == 0)
				{
					if (!was.Contains(i))
					{
						yield return i;
						was.Add(i);
					}
					int other = number / i;
					if (!was.Contains(other))
					{
						yield return other;
						was.Add(other);
					}
				}
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00050DB0 File Offset: 0x0004EFB0
		public static double LogGamma(double x)
		{
			double num = x - 1.0;
			if (num <= 0.0)
			{
				return 0.0;
			}
			return num * Math.Log(num) - num;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00050DEC File Offset: 0x0004EFEC
		public static double LogSumExp(double x, double y)
		{
			if (double.IsNegativeInfinity(x))
			{
				return y;
			}
			if (double.IsNegativeInfinity(y))
			{
				return x;
			}
			if (x > y)
			{
				return x + Math.Log(1.0 + Math.Exp(y - x));
			}
			return y + Math.Log(1.0 + Math.Exp(x - y));
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00050E44 File Offset: 0x0004F044
		public static Dictionary<T, double> NormalizeDictionary<T>(IReadOnlyDictionary<T, double> dict)
		{
			MathUtils.<>c__DisplayClass3_0<T> CS$<>8__locals1 = new MathUtils.<>c__DisplayClass3_0<T>();
			MathUtils.<>c__DisplayClass3_0<T> CS$<>8__locals2 = CS$<>8__locals1;
			IEnumerable<double> values = dict.Values;
			double negativeInfinity = double.NegativeInfinity;
			Func<double, double, double> func;
			if ((func = MathUtils.<>O.<0>__LogSumExp) == null)
			{
				func = (MathUtils.<>O.<0>__LogSumExp = new Func<double, double, double>(MathUtils.LogSumExp));
			}
			CS$<>8__locals2.logZ = values.Aggregate(negativeInfinity, func);
			return dict.ToDictionary((KeyValuePair<T, double> kvp) => kvp.Key, (KeyValuePair<T, double> kvp) => kvp.Value - CS$<>8__locals1.logZ);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00050EBD File Offset: 0x0004F0BD
		public static int GCD(int a, int b)
		{
			if (b != 0)
			{
				return MathUtils.GCD(b, a % b);
			}
			return a;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00050ED0 File Offset: 0x0004F0D0
		public static decimal GCD(decimal a, decimal b)
		{
			if (a < 0m)
			{
				throw new ArgumentOutOfRangeException("a", "Cannot take gcd of negative numbers.");
			}
			if (b < 0m)
			{
				throw new ArgumentOutOfRangeException("b", "Cannot take gcd of negative numbers.");
			}
			if (b > a)
			{
				decimal num = a;
				a = b;
				b = num;
			}
			while (b != 0m)
			{
				decimal num2 = b;
				b = a % b;
				a = num2;
			}
			return a;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00050F44 File Offset: 0x0004F144
		public static int OrderIndependentPreHash(int y)
		{
			uint num = (((uint)y >> 16) ^ (uint)y) * 23216779U;
			num = ((num >> 16) ^ num) * 23216779U;
			return (int)((num >> 16) ^ num);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00050F73 File Offset: 0x0004F173
		public static int OrderIndependentCombiner(int x, int y)
		{
			return x + MathUtils.OrderIndependentPreHash(y);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00050F7D File Offset: 0x0004F17D
		public static int OrderIndependentCombinerInverse(int x, int y)
		{
			return x - MathUtils.OrderIndependentPreHash(y);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00050F87 File Offset: 0x0004F187
		public static int OrderIndependentKeyValueHashCode<TKey, TValue>(KeyValuePair<TKey, TValue> item)
		{
			return MathUtils.OrderIndependentHashCode<KeyValuePair<TKey, TValue>>(item, KeyValueComparer<TKey, TValue>.DefaultEqualityInstance);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00050F94 File Offset: 0x0004F194
		public static int OrderIndependentHashCode<T>(T item) where T : IEquatable<T>
		{
			return MathUtils.OrderIndependentHashCode<T>(item, EqualityComparer<T>.Default);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00050FA1 File Offset: 0x0004F1A1
		public static int OrderIndependentHashCode<T>(T item, IEqualityComparer<T> comparer)
		{
			return MathUtils.OrderIndependentCombiner(397, comparer.GetHashCode(item));
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00050FB4 File Offset: 0x0004F1B4
		public static int OrderIndependentKeyValueHashCode<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			return collection.OrderIndependentHashCode(KeyValueComparer<TKey, TValue>.DefaultEqualityInstance);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00050FC1 File Offset: 0x0004F1C1
		public static int OrderIndependentHashCode<T>(this IEnumerable<T> collection) where T : IEquatable<T>
		{
			return collection.OrderIndependentHashCode(EqualityComparer<T>.Default);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00050FD0 File Offset: 0x0004F1D0
		public static int OrderIndependentHashCode<T>(this IEnumerable<T> collection, IEqualityComparer<T> comparer)
		{
			int num = 397;
			foreach (T t in collection)
			{
				num = MathUtils.OrderIndependentCombiner(num, comparer.GetHashCode(t));
			}
			return num;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00051028 File Offset: 0x0004F228
		public static int OrderIndependentHashCode<T>(this IEnumerable<T> collection, Func<T, int> hasher)
		{
			int num = 397;
			foreach (T t in collection)
			{
				num = MathUtils.OrderIndependentCombiner(num, hasher(t));
			}
			return num;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00051080 File Offset: 0x0004F280
		public static int OrderDependentKeyValueHashCode<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			return collection.OrderDependentHashCode(KeyValueComparer<TKey, TValue>.DefaultEqualityInstance);
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0005108D File Offset: 0x0004F28D
		public static int OrderDependentHashCode<T>(this IEnumerable<T> collection) where T : IEquatable<T>
		{
			return collection.OrderDependentHashCode(EqualityComparer<T>.Default);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0005109C File Offset: 0x0004F29C
		public static int OrderDependentHashCode<T>(this IEnumerable<T> collection, IEqualityComparer<T> comparer)
		{
			int num = 23173;
			foreach (T t in collection)
			{
				num ^= comparer.GetHashCode(t) * 22853;
				num *= 23173;
			}
			return num;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000510FC File Offset: 0x0004F2FC
		public static int OrderDependentHashCode(this IEnumerable collection, IEqualityComparer comparer)
		{
			int num = 23173;
			foreach (object obj in collection)
			{
				num ^= comparer.GetHashCode(obj) * 22853;
				num *= 23173;
			}
			return num;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00051164 File Offset: 0x0004F364
		public static decimal Normalize(this decimal d)
		{
			return d / 1.0000000000000000000000000000m;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00051184 File Offset: 0x0004F384
		public static double StandardDeviation(this IEnumerable<double> values, double? mean = null)
		{
			IReadOnlyCollection<double> readOnlyCollection = (values as IReadOnlyCollection<double>) ?? values.ToList<double>();
			double theMean = mean ?? readOnlyCollection.Average();
			return Math.Sqrt(readOnlyCollection.Select((double v) => (theMean - v) * (theMean - v)).Average());
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x000511E4 File Offset: 0x0004F3E4
		public static double StandardDeviation(this IEnumerable<long> values, double? mean = null)
		{
			IReadOnlyCollection<long> readOnlyCollection = (values as IReadOnlyCollection<long>) ?? values.ToList<long>();
			double theMean = mean ?? readOnlyCollection.Average();
			return Math.Sqrt(readOnlyCollection.Select((long v) => (theMean - (double)v) * (theMean - (double)v)).Average());
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00051244 File Offset: 0x0004F444
		public static double StandardDeviation(this IEnumerable<int> values, double? mean = null)
		{
			IReadOnlyCollection<int> readOnlyCollection = (values as IReadOnlyCollection<int>) ?? values.ToList<int>();
			double theMean = mean ?? readOnlyCollection.Average();
			return Math.Sqrt(readOnlyCollection.Select((int v) => (theMean - (double)v) * (theMean - (double)v)).Average());
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000512A4 File Offset: 0x0004F4A4
		public static double ProportionTrue<T>(this IEnumerable<T> xs, Func<T, bool> predicate)
		{
			int num = 0;
			int num2 = 0;
			foreach (T t in xs)
			{
				if (predicate(t))
				{
					num++;
				}
				num2++;
			}
			return (double)num / (double)num2;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00051300 File Offset: 0x0004F500
		public static Optional<double> Median(this IReadOnlyList<int> values)
		{
			if (values.Count == 0)
			{
				return Optional<double>.Nothing;
			}
			IReadOnlyList<int> readOnlyList = values.OrderBy((int v) => v).Take(values.Count / 2 + 1).ToList<int>();
			if (values.Count % 2 == 1)
			{
				return ((double)readOnlyList[values.Count / 2]).Some<double>();
			}
			return (((double)readOnlyList[values.Count / 2] + (double)readOnlyList[values.Count / 2 - 1]) / 2.0).Some<double>();
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x000513A8 File Offset: 0x0004F5A8
		public static int[] GetBits(ushort mask, int length = 16)
		{
			int[] array = new int[length];
			int num = 0;
			while (mask > 0)
			{
				array[num] = (int)(mask & 1);
				mask = (ushort)(mask >> 1);
				num++;
			}
			return array;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x000513D5 File Offset: 0x0004F5D5
		public static uint CountBits(uint i)
		{
			i -= (i >> 1) & 1431655765U;
			i = (i & 858993459U) + ((i >> 2) & 858993459U);
			return ((i + (i >> 4)) & 252645135U) * 16843009U >> 24;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0005140B File Offset: 0x0004F60B
		public static uint CountBits(ulong l)
		{
			return MathUtils.CountBits((uint)l) + MathUtils.CountBits((uint)(l >> 32));
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00051420 File Offset: 0x0004F620
		public static double GetEntropy(IReadOnlyList<double> values, double sum)
		{
			if (values == null || !values.Any<double>())
			{
				return 0.0;
			}
			double num = 0.0;
			if (sum <= 0.0)
			{
				return 0.0;
			}
			foreach (double num2 in values)
			{
				num += -num2 / sum * Math.Log(num2 / sum);
			}
			return num;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000514A8 File Offset: 0x0004F6A8
		public static bool WithinTolerance(double center, double other, double tolerance)
		{
			return Math.Abs(other - center) <= tolerance;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000514B8 File Offset: 0x0004F6B8
		public static bool WithinTolerance(double center, double other, double tolerance, double modulo)
		{
			double num = (other - center) % modulo;
			if (num < 0.0)
			{
				num += modulo;
			}
			return num <= tolerance || modulo - num <= tolerance;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000514EA File Offset: 0x0004F6EA
		public static bool WithinTolerance(int center, int other, int tolerance)
		{
			return Math.Abs(other - center) <= tolerance;
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000514FA File Offset: 0x0004F6FA
		public static double UpdateAverage(double previousAverage, int count, double newValue)
		{
			return previousAverage + (newValue - previousAverage) / (double)count;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00051504 File Offset: 0x0004F704
		public static float ToDegrees(float radians)
		{
			return (float)((double)(radians * 180f) / 3.141592653589793);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00051519 File Offset: 0x0004F719
		public static double ToDegrees(double radians)
		{
			return radians * 180.0 / 3.141592653589793;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00051530 File Offset: 0x0004F730
		public static double NormalizeAngle(double radians)
		{
			while (radians > 6.283185307179586)
			{
				radians -= 6.283185307179586;
			}
			while (radians < 0.0)
			{
				radians += 6.283185307179586;
			}
			return radians;
		}

		// Token: 0x04000D3A RID: 3386
		public const int OrderIndependentSeed = 397;

		// Token: 0x04000D3B RID: 3387
		public const int OrderDependentSeed = 23173;

		// Token: 0x04000D3C RID: 3388
		public const int OrderDependentElementHashMultiplier = 22853;

		// Token: 0x020004AF RID: 1199
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000D3D RID: 3389
			public static Func<double, double, double> <0>__LogSumExp;
		}
	}
}
