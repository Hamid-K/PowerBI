using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200024B RID: 587
	public static class ExtendedMath
	{
		// Token: 0x06000F0C RID: 3852 RVA: 0x00034018 File Offset: 0x00032218
		public static int LogBase2(int value)
		{
			int num = 0;
			while ((value >>= 1) != 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00034036 File Offset: 0x00032236
		public static bool IsPowerOf2(long value)
		{
			return value > 0L && (value & (value - 1L)) == 0L;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00034049 File Offset: 0x00032249
		public static bool IsPowerOf2(int value)
		{
			return value > 0 && (value & (value - 1)) == 0;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00034049 File Offset: 0x00032249
		public static bool IsPowerOf2(short value)
		{
			return value > 0 && (value & (value - 1)) == 0;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00034059 File Offset: 0x00032259
		public static int Fold(long value)
		{
			return (int)((value >> 32) ^ (value & (long)((ulong)(-1))));
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00034065 File Offset: 0x00032265
		public static int ToInt32(this long value)
		{
			if (value >= 2147483647L)
			{
				return int.MaxValue;
			}
			return (int)value;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00034078 File Offset: 0x00032278
		public static long GreatestCommonDivisor(long a, long b)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(a, "1st number");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(b, "2nd number");
			while (b != 0L)
			{
				long num = b;
				b = a % b;
				a = num;
			}
			return a;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x000340A0 File Offset: 0x000322A0
		public static long LeastCommonMultiple(long a, long b)
		{
			long num = 0L;
			long num2 = ExtendedMath.GreatestCommonDivisor(a, b);
			if (num2 != 0L)
			{
				if (b > a)
				{
					num = b / num2 * a;
				}
				else
				{
					num = a / num2 * b;
				}
			}
			return num;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x000340D0 File Offset: 0x000322D0
		public static bool AreCloseEnough(double a, double b, double acceptableDeviation)
		{
			ExtendedDiagnostics.EnsureOperation(a != 0.0, "Argument 'a' may not be 0");
			double num = Math.Abs(a - b);
			return num == 0.0 || num / a <= acceptableDeviation;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00034116 File Offset: 0x00032316
		public static int SafeAbs(int a)
		{
			if (-2147483648 == a)
			{
				a++;
			}
			return Math.Abs(a);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0003412B File Offset: 0x0003232B
		public static T Max<T>(T a, T b) where T : IComparable<T>
		{
			if (a.CompareTo(b) <= 0)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00034141 File Offset: 0x00032341
		public static T Min<T>(T a, T b) where T : IComparable<T>
		{
			if (a.CompareTo(b) >= 0)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00034157 File Offset: 0x00032357
		public static long BytesToKB(long bytes)
		{
			return bytes / 1024L;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00034161 File Offset: 0x00032361
		public static long BytesToMB(long bytes)
		{
			return ExtendedMath.BytesToKB(bytes) / 1024L;
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00034170 File Offset: 0x00032370
		public static long BytesToMBRounded(long bytes)
		{
			return (long)Math.Round(((double)bytes + 0.0) / 1024.0 / 1024.0, MidpointRounding.AwayFromZero);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00034199 File Offset: 0x00032399
		public static long BytesToMBRoundedUp(long bytes)
		{
			return (long)Math.Ceiling(((double)bytes + 0.0) / 1024.0 / 1024.0);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x000341C1 File Offset: 0x000323C1
		public static long KBtoBytes(long kb)
		{
			return kb * 1024L;
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x000341CB File Offset: 0x000323CB
		public static long MBtoBytes(long mb)
		{
			return mb * 1024L * 1024L;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x000341DC File Offset: 0x000323DC
		public static long GBtoBytes(long gb)
		{
			return gb * 1024L * 1024L * 1024L;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x000341F4 File Offset: 0x000323F4
		public static byte[] Xor(byte[] a, byte[] b)
		{
			byte[] array = new byte[Math.Max(a.Length, b.Length)];
			for (int i = 0; i < array.Length; i++)
			{
				if (i < a.Length)
				{
					if (i < b.Length)
					{
						array[i] = a[i] ^ b[i];
					}
					else
					{
						array[i] = a[i];
					}
				}
				else
				{
					array[i] = b[i];
				}
			}
			return array;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00034248 File Offset: 0x00032448
		public static void XorInPlace(byte[] arrayBytes, int tragetLocation, int firstLocation, int secondLocation, int length)
		{
			for (int i = 0; i < length; i++)
			{
				arrayBytes[i + tragetLocation] = arrayBytes[i + firstLocation] ^ arrayBytes[i + secondLocation];
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00034274 File Offset: 0x00032474
		public static byte[] AddWithOverflow(byte[] a, byte[] b)
		{
			byte[] array = new byte[Math.Max(a.Length, b.Length)];
			for (int i = 0; i < array.Length; i++)
			{
				if (i < a.Length)
				{
					if (i < b.Length)
					{
						array[i] = a[i] + b[i];
					}
					else
					{
						array[i] = a[i];
					}
				}
				else
				{
					array[i] = b[i];
				}
			}
			return array;
		}
	}
}
