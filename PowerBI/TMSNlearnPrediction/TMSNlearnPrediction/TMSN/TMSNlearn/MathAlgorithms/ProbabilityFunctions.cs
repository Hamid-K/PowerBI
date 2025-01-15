using System;
using Microsoft.MachineLearning;

namespace Microsoft.TMSN.TMSNlearn.MathAlgorithms
{
	// Token: 0x02000438 RID: 1080
	public sealed class ProbabilityFunctions
	{
		// Token: 0x06001660 RID: 5728 RVA: 0x00082CA8 File Offset: 0x00080EA8
		public static double Erfc(double x)
		{
			if (double.IsInfinity(x))
			{
				if (!double.IsPositiveInfinity(x))
				{
					return 2.0;
				}
				return 0.0;
			}
			else
			{
				double num = 1.0 / (1.0 + 0.3275911 * Math.Abs(x));
				double num2 = ((((1.061405429 * num + -1.453152027) * num + 1.421413741) * num + -0.284496736) * num + 0.254829592) * num * Math.Exp(-(x * x));
				if (x < 0.0)
				{
					return 2.0 - num2;
				}
				return num2;
			}
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00082D60 File Offset: 0x00080F60
		public static double Erf(double x)
		{
			if (double.IsInfinity(x))
			{
				if (!double.IsPositiveInfinity(x))
				{
					return -1.0;
				}
				return 1.0;
			}
			else
			{
				double num = 1.0 / (1.0 + 0.3275911 * Math.Abs(x));
				double num2 = 1.0 - ((((1.061405429 * num + -1.453152027) * num + 1.421413741) * num + -0.284496736) * num + 0.254829592) * num * Math.Exp(-(x * x));
				if (x < 0.0)
				{
					return -num2;
				}
				return num2;
			}
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00082E18 File Offset: 0x00081018
		public static double Erfinv(double x)
		{
			if (x > 1.0 || x < -1.0)
			{
				return double.NaN;
			}
			if (x == 1.0)
			{
				return double.PositiveInfinity;
			}
			if (x == -1.0)
			{
				return double.NegativeInfinity;
			}
			double[] array = new double[1000];
			array[0] = 1.0;
			for (int i = 1; i < array.Length; i++)
			{
				for (int j = 0; j < i; j++)
				{
					array[i] += array[j] * array[i - 1 - j] / (double)(j + 1) / (double)(j + j + 1);
				}
			}
			double num = Math.Sqrt(3.141592653589793) / 2.0;
			double num2 = 0.7853981633974483;
			double num3 = x;
			double num4 = x * x;
			double num5 = 0.0;
			for (int k = 0; k < array.Length; k++)
			{
				num5 += array[k] * num * num3 / (double)(2 * k + 1);
				num *= num2;
				num3 *= num4;
			}
			return num5;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00082F3C File Offset: 0x0008113C
		public static double Probit(double p)
		{
			double num = p - 0.5;
			double num2;
			if (Math.Abs(num) <= 0.425)
			{
				num2 = 0.180625 - num * num;
				return num * (((((((ProbabilityFunctions.ProbA[7] * num2 + ProbabilityFunctions.ProbA[6]) * num2 + ProbabilityFunctions.ProbA[5]) * num2 + ProbabilityFunctions.ProbA[4]) * num2 + ProbabilityFunctions.ProbA[3]) * num2 + ProbabilityFunctions.ProbA[2]) * num2 + ProbabilityFunctions.ProbA[1]) * num2 + ProbabilityFunctions.ProbA[0]) / (((((((ProbabilityFunctions.ProbB[6] * num2 + ProbabilityFunctions.ProbB[5]) * num2 + ProbabilityFunctions.ProbB[4]) * num2 + ProbabilityFunctions.ProbB[3]) * num2 + ProbabilityFunctions.ProbB[2]) * num2 + ProbabilityFunctions.ProbB[1]) * num2 + ProbabilityFunctions.ProbB[0]) * num2 + 1.0);
			}
			if (num < 0.0)
			{
				num2 = p;
			}
			else
			{
				num2 = 1.0 - p;
			}
			if (num2 < 0.0)
			{
				throw Contracts.ExceptParam("p", "Illegal input value");
			}
			num2 = Math.Sqrt(-Math.Log(num2));
			double num3;
			if (num2 < 5.0)
			{
				num2 -= 1.6;
				num3 = (((((((ProbabilityFunctions.ProbC[7] * num2 + ProbabilityFunctions.ProbC[6]) * num2 + ProbabilityFunctions.ProbC[5]) * num2 + ProbabilityFunctions.ProbC[4]) * num2 + ProbabilityFunctions.ProbC[3]) * num2 + ProbabilityFunctions.ProbC[2]) * num2 + ProbabilityFunctions.ProbC[1]) * num2 + ProbabilityFunctions.ProbC[0]) / (((((((ProbabilityFunctions.ProbD[6] * num2 + ProbabilityFunctions.ProbD[5]) * num2 + ProbabilityFunctions.ProbD[4]) * num2 + ProbabilityFunctions.ProbD[3]) * num2 + ProbabilityFunctions.ProbD[2]) * num2 + ProbabilityFunctions.ProbD[1]) * num2 + ProbabilityFunctions.ProbD[0]) * num2 + 1.0);
			}
			else
			{
				num2 -= 5.0;
				num3 = (((((((ProbabilityFunctions.ProbE[7] * num2 + ProbabilityFunctions.ProbE[6]) * num2 + ProbabilityFunctions.ProbE[5]) * num2 + ProbabilityFunctions.ProbE[4]) * num2 + ProbabilityFunctions.ProbE[3]) * num2 + ProbabilityFunctions.ProbE[2]) * num2 + ProbabilityFunctions.ProbE[1]) * num2 + ProbabilityFunctions.ProbE[0]) / (((((((ProbabilityFunctions.ProbF[6] * num2 + ProbabilityFunctions.ProbF[5]) * num2 + ProbabilityFunctions.ProbF[4]) * num2 + ProbabilityFunctions.ProbF[3]) * num2 + ProbabilityFunctions.ProbF[2]) * num2 + ProbabilityFunctions.ProbF[1]) * num2 + ProbabilityFunctions.ProbF[0]) * num2 + 1.0);
			}
			if (num < 0.0)
			{
				return -num3;
			}
			return num3;
		}

		// Token: 0x04000DB9 RID: 3513
		private static readonly double[] ProbA = new double[] { 3.3871328727963665, 133.14166789178438, 1971.5909503065513, 13731.69376550946, 45921.95393154987, 67265.7709270087, 33430.57558358813, 2509.0809287301227 };

		// Token: 0x04000DBA RID: 3514
		private static readonly double[] ProbB = new double[] { 42.31333070160091, 687.1870074920579, 5394.196021424751, 21213.794301586597, 39307.89580009271, 28729.085735721943, 5226.495278852854 };

		// Token: 0x04000DBB RID: 3515
		private static readonly double[] ProbC = new double[] { 1.4234371107496835, 4.630337846156546, 5.769497221460691, 3.6478483247632045, 1.2704582524523684, 0.2417807251774506, 0.022723844989269184, 0.0007745450142783414 };

		// Token: 0x04000DBC RID: 3516
		private static readonly double[] ProbD = new double[] { 2.053191626637759, 1.6763848301838038, 0.6897673349851, 0.14810397642748008, 0.015198666563616457, 0.0005475938084995345, 1.0507500716444169E-09 };

		// Token: 0x04000DBD RID: 3517
		private static readonly double[] ProbE = new double[] { 6.657904643501103, 5.463784911164114, 1.7848265399172913, 0.29656057182850487, 0.026532189526576124, 0.0012426609473880784, 2.7115555687434876E-05, 2.0103343992922881E-07 };

		// Token: 0x04000DBE RID: 3518
		private static readonly double[] ProbF = new double[] { 0.599832206555888, 0.1369298809227358, 0.014875361290850615, 0.0007868691311456133, 1.8463183175100548E-05, 1.421511758316446E-07, 2.0442631033899397E-15 };
	}
}
