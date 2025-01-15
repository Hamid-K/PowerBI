using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000274 RID: 628
	public static class Randomizer
	{
		// Token: 0x0600108F RID: 4239 RVA: 0x00039A15 File Offset: 0x00037C15
		public static bool GetB()
		{
			return Convert.ToBoolean(Randomizer.GetI32(2));
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00039A22 File Offset: 0x00037C22
		public static char GetC()
		{
			return Convert.ToChar((int)(Randomizer.GetUI8() % 26 + 65));
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00039A34 File Offset: 0x00037C34
		public static short GetI16()
		{
			return (short)Randomizer.RandomGenerator.Next(32767);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00039A46 File Offset: 0x00037C46
		public static short GetI16(short maxValue)
		{
			return (short)Randomizer.RandomGenerator.Next((int)maxValue);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00039A54 File Offset: 0x00037C54
		public static short GetI16(short minValue, short maxValue)
		{
			return (short)Randomizer.RandomGenerator.Next((int)minValue, (int)maxValue);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00039A63 File Offset: 0x00037C63
		public static int GetI32()
		{
			return Randomizer.RandomGenerator.Next();
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00039A6F File Offset: 0x00037C6F
		public static int GetI32(int maxValue)
		{
			return Randomizer.RandomGenerator.Next(maxValue);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00039A7C File Offset: 0x00037C7C
		public static int GetI32(int minValue, int maxValue)
		{
			return Randomizer.RandomGenerator.Next(minValue, maxValue);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00039A8A File Offset: 0x00037C8A
		public static long GetI64()
		{
			return (long)Randomizer.GetUI64();
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00039A91 File Offset: 0x00037C91
		public static long GetI64(long minValue, long maxValue)
		{
			ExtendedDiagnostics.EnsureOperation(maxValue > minValue, "maxValue must be greater than minValue");
			return Randomizer.GetI64() % (maxValue - minValue) + minValue;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00039AAC File Offset: 0x00037CAC
		public static TimeSpan GetTimeSpan(long minTicks, long maxTicks)
		{
			ExtendedDiagnostics.EnsureOperation(maxTicks > minTicks, "maxTicks must be greater than minTicks");
			return new TimeSpan(Randomizer.GetI64() % (maxTicks - minTicks) + minTicks);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00039ACC File Offset: 0x00037CCC
		public static TimeSpan GetTimeSpan()
		{
			return new TimeSpan(Randomizer.GetI64());
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00039AD8 File Offset: 0x00037CD8
		public static double GetF64()
		{
			double num = (double)Randomizer.GetI32();
			double num2 = (double)Randomizer.GetI32(1, int.MaxValue);
			return num / num2;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00039AFA File Offset: 0x00037CFA
		public static double GetNormF64()
		{
			return Randomizer.RandomGenerator.NextDouble();
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00039B06 File Offset: 0x00037D06
		public static byte GetUI8()
		{
			return (byte)Randomizer.RandomGenerator.Next();
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00039B13 File Offset: 0x00037D13
		public static byte GetUI8(byte maxValue)
		{
			return (byte)Randomizer.RandomGenerator.Next((int)maxValue);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00039B21 File Offset: 0x00037D21
		public static byte GetUI8(byte minValue, byte maxValue)
		{
			return (byte)Randomizer.RandomGenerator.Next((int)minValue, (int)maxValue);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00039B30 File Offset: 0x00037D30
		public static ushort GetUI16()
		{
			return (ushort)Randomizer.RandomGenerator.Next();
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00039A63 File Offset: 0x00037C63
		public static uint GetUI32()
		{
			return (uint)Randomizer.RandomGenerator.Next();
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00039B3D File Offset: 0x00037D3D
		public static ulong GetUI64()
		{
			return ((ulong)Randomizer.RandomGenerator.Next() << 32) | (ulong)Randomizer.RandomGenerator.Next();
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00039B59 File Offset: 0x00037D59
		public static string GetS()
		{
			return Randomizer.GetS(20, 50);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00039B64 File Offset: 0x00037D64
		public static double NextDoubleThreadSafe(this Random random)
		{
			double num;
			lock (random)
			{
				num = random.NextDouble();
			}
			return num;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00039BA4 File Offset: 0x00037DA4
		public static int NextThreadSafe(this Random random)
		{
			int num;
			lock (random)
			{
				num = random.Next();
			}
			return num;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00039BE4 File Offset: 0x00037DE4
		public static int NextThreadSafe(this Random random, int max)
		{
			int num;
			lock (random)
			{
				num = random.Next(max);
			}
			return num;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00039C24 File Offset: 0x00037E24
		public static int NextThreadSafe(this Random random, int min, int max)
		{
			int num;
			lock (random)
			{
				num = random.Next(min, max);
			}
			return num;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00039C64 File Offset: 0x00037E64
		public static string GetS(int maxLength)
		{
			return Randomizer.GetS(0, maxLength);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00039C6D File Offset: 0x00037E6D
		public static Guid GetG()
		{
			return new Guid(Randomizer.GetFixedSizeByteArray(16));
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00039C7C File Offset: 0x00037E7C
		public static string GetS(int minLength, int maxLength)
		{
			int num = Randomizer.RandomGenerator.Next(minLength, maxLength);
			StringBuilder stringBuilder = new StringBuilder(num);
			while (num-- > 0)
			{
				stringBuilder.Append(Randomizer.GetC());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00039CB9 File Offset: 0x00037EB9
		public static byte[] GetByteArray(int maxLength, bool useMaxLength = false)
		{
			return Randomizer.GetFixedSizeByteArray(useMaxLength ? maxLength : Randomizer.GetI32(maxLength));
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00039CCC File Offset: 0x00037ECC
		public static byte[] GetFixedSizeByteArray(int fixedLength)
		{
			byte[] array = new byte[fixedLength];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Randomizer.GetUI8();
			}
			return array;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00039CF7 File Offset: 0x00037EF7
		public static DateTime GetDateTime(DateTime from, DateTime to)
		{
			return new DateTime(Randomizer.GetI64(from.Ticks, to.Ticks));
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00039D14 File Offset: 0x00037F14
		public static int GetCryptographicRandomNumber(int minValue, int maxValue)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxValue - minValue, "maxValue must be greater than minValue");
			byte[] array = new byte[4];
			int num = maxValue - minValue;
			long num2 = 4294967296L;
			long num3 = num2 % (long)num;
			uint num4;
			do
			{
				Randomizer.CryptoRandomGenerator.GetBytes(array);
				num4 = BitConverter.ToUInt32(array, 0);
			}
			while ((ulong)num4 >= (ulong)(num2 - num3));
			return (int)((long)minValue + (long)((ulong)num4 % (ulong)((long)num)));
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00039D70 File Offset: 0x00037F70
		public static byte[] GetCryptographicRandomBytes(int length)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(length, "length must be greater than zero");
			byte[] array = new byte[length];
			Randomizer.CryptoRandomGenerator.GetNonZeroBytes(array);
			return array;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00039D9B File Offset: 0x00037F9B
		public static string GetCryptographicRandomBytesAsString(int length)
		{
			return ExtendedText.ByteArrayToStringHex(Randomizer.GetCryptographicRandomBytes(length));
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00039DA8 File Offset: 0x00037FA8
		public static T GetEnumValue<T>()
		{
			IEnumerable<T> values = ExtendedEnum.GetValues<T>();
			int i = Randomizer.GetI32(values.Count<T>() - 1);
			return values.ElementAt(i);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00039DD0 File Offset: 0x00037FD0
		public static IEnumerable<T> GetRandomSubset<T>(IEnumerable<T> values, int minDistinctSubsetSize)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(minDistinctSubsetSize, "minDistinctSubsetSize");
			ExtendedDiagnostics.EnsureOperation(values.Count<T>() >= minDistinctSubsetSize, "The minimal subset size must be equal or lower than the values list");
			int i = Randomizer.GetI32(minDistinctSubsetSize, values.Count<T>() + 1);
			return values.OrderBy((T x) => Guid.NewGuid()).Take(i);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00039E38 File Offset: 0x00038038
		public static IEnumerable<T> GetRandomValues<T>(IEnumerable<T> values, int count)
		{
			if (!values.Any<T>())
			{
				return Enumerable.Empty<T>();
			}
			return from o in Enumerable.Range(0, count)
				select values.ElementAt(Randomizer.GetI32(values.Count<T>()));
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00039E7D File Offset: 0x0003807D
		public static T GetRandomValue<T>(IEnumerable<T> values)
		{
			return Randomizer.GetRandomValues<T>(values, 1).FirstOrDefault<T>();
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00039E8B File Offset: 0x0003808B
		public static T GetRandomValue<T>(Collection<T> values)
		{
			ExtendedDiagnostics.EnsureCollectionNotNullOrEmpty<T>(values, "values");
			return values[Randomizer.GetI32(values.Count)];
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00039EA9 File Offset: 0x000380A9
		public static IDisposable SetRandomBehavior(Random random)
		{
			Random random2 = Randomizer.s_randomGenerator;
			Randomizer.RandomGenerator = random;
			return new Randomizer.RandomizerRestorer(random2);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00039EBB File Offset: 0x000380BB
		public static void SeedCurrentThread(int value)
		{
			Randomizer.RandomGenerator = new Random(value);
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x00039ED0 File Offset: 0x000380D0
		// (set) Token: 0x060010B8 RID: 4280 RVA: 0x00039EC8 File Offset: 0x000380C8
		private static Random RandomGenerator
		{
			get
			{
				Random random = Randomizer.s_randomGenerator;
				if (random == null)
				{
					random = new Random((int)DateTime.UtcNow.Ticks ^ Thread.CurrentThread.GetHashCode());
					Randomizer.RandomGenerator = random;
				}
				return random;
			}
			set
			{
				Randomizer.s_randomGenerator = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00039F14 File Offset: 0x00038114
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x00039F0C File Offset: 0x0003810C
		private static RNGCryptoServiceProvider CryptoRandomGenerator
		{
			get
			{
				RNGCryptoServiceProvider rngcryptoServiceProvider = Randomizer.s_cryptoRandomGenerator;
				if (rngcryptoServiceProvider == null)
				{
					rngcryptoServiceProvider = new RNGCryptoServiceProvider();
					Randomizer.CryptoRandomGenerator = rngcryptoServiceProvider;
				}
				return rngcryptoServiceProvider;
			}
			set
			{
				Randomizer.s_cryptoRandomGenerator = value;
			}
		}

		// Token: 0x0400062F RID: 1583
		[ThreadStatic]
		private static Random s_randomGenerator;

		// Token: 0x04000630 RID: 1584
		[ThreadStatic]
		private static RNGCryptoServiceProvider s_cryptoRandomGenerator;

		// Token: 0x020006E2 RID: 1762
		private class RandomizerRestorer : IDisposable
		{
			// Token: 0x06002ECE RID: 11982 RVA: 0x000A2BDB File Offset: 0x000A0DDB
			internal RandomizerRestorer(Random randomToRestore)
			{
				this.m_randomToRestore = randomToRestore;
			}

			// Token: 0x06002ECF RID: 11983 RVA: 0x000A2BEA File Offset: 0x000A0DEA
			public void Dispose()
			{
				Randomizer.RandomGenerator = this.m_randomToRestore;
			}

			// Token: 0x04001393 RID: 5011
			private Random m_randomToRestore;
		}
	}
}
