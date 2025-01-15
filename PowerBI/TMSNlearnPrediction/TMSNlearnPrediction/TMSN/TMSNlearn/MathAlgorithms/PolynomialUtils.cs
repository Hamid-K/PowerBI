using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.TMSN.TMSNlearn.MathAlgorithms
{
	// Token: 0x02000434 RID: 1076
	public static class PolynomialUtils
	{
		// Token: 0x06001651 RID: 5713 RVA: 0x0008237A File Offset: 0x0008057A
		private static bool IsZero(double x)
		{
			return Math.Abs(x) <= PolynomialUtils._tol;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0008238C File Offset: 0x0008058C
		internal static void FindQuadraticRoots(double b, double c, out Complex root1, out Complex root2)
		{
			double num = b * b - 4.0 * c;
			double num2 = Math.Sqrt(Math.Abs(num));
			if (num >= 0.0)
			{
				root1 = new Complex((-b + num2) / 2.0, 0.0);
				root2 = new Complex((-b - num2) / 2.0, 0.0);
				return;
			}
			root1 = new Complex(-b / 2.0, num2 / 2.0);
			root2 = new Complex(-b / 2.0, -num2 / 2.0);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0008244C File Offset: 0x0008064C
		private static void CreateFullCompanionMatrix(double[] coefficients, ref double[] companionMatrix)
		{
			int num = coefficients.Length;
			int num2 = num * num;
			if (Utils.Size<double>(companionMatrix) < num2)
			{
				companionMatrix = new double[num2];
			}
			for (int i = 1; i <= num - 1; i++)
			{
				companionMatrix[num * (i - 1) + i] = 1.0;
			}
			for (int i = 0; i < num; i++)
			{
				companionMatrix[num2 - num + i] = -coefficients[i];
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x000824AC File Offset: 0x000806AC
		public static bool FindPolynomialRoots(double[] coefficients, ref Complex[] roots, int roundOffDigits = 6, double doublePrecision = 2.2200000000000002E-100)
		{
			Contracts.CheckParam(doublePrecision > 0.0, "doublePrecision", "The double precision must be positive.");
			Contracts.CheckParam(Utils.Size<double>(coefficients) >= 1, "coefficients", "There must be at least one input coefficient.");
			int num = coefficients.Length;
			bool flag = true;
			PolynomialUtils._tol = doublePrecision;
			if (Utils.Size<Complex>(roots) < num)
			{
				roots = new Complex[num];
			}
			int num2 = 0;
			while (num2 < num && PolynomialUtils.IsZero(coefficients[num2]))
			{
				roots[num - num2 - 1] = Complex.Zero;
				num2++;
			}
			if (num2 == num)
			{
				return true;
			}
			if (num2 == num - 1)
			{
				roots[0] = new Complex(-coefficients[num2], 0.0);
			}
			else if (num2 == num - 2)
			{
				PolynomialUtils.FindQuadraticRoots(coefficients[num2 + 1], coefficients[num2], out roots[0], out roots[1]);
			}
			else
			{
				double[] array = coefficients;
				if (num2 > 0)
				{
					array = new double[num - num2];
					Array.Copy(coefficients, num2, array, 0, num - num2);
				}
				double[] array2 = null;
				double[] array3 = new double[num - num2];
				double[] array4 = new double[num - num2];
				double[] array5 = new double[1];
				PolynomialUtils.CreateFullCompanionMatrix(array, ref array2);
				int num3 = EigenUtils.Dhseqr(EigenUtils.Layout.ColMajor, EigenUtils.Job.EigenValues, EigenUtils.Compz.None, num - num2, 1, num - num2, array2, num - num2, array3, array4, array5, num - num2);
				if (num3 != 0)
				{
					return false;
				}
				for (int i = 0; i < num - num2; i++)
				{
					roots[i] = new Complex(array3[i], array4[i]);
				}
			}
			return flag;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0008262C File Offset: 0x0008082C
		public static bool FindPolynomialCoefficients(Complex[] roots, ref double[] coefficients)
		{
			Contracts.CheckParam(Utils.Size<Complex>(roots) > 0, "roots", "There must be at least 1 input root.");
			int num = roots.Length;
			Dictionary<Complex, PolynomialUtils.FactorMultiplicity> dictionary = new Dictionary<Complex, PolynomialUtils.FactorMultiplicity>();
			int num2 = 0;
			List<PolynomialUtils.PolynomialFactor> list = new List<PolynomialUtils.PolynomialFactor>();
			for (int i = 0; i < num; i++)
			{
				if (double.IsNaN(roots[i].Real) || double.IsNaN(roots[i].Imaginary))
				{
					return false;
				}
				if (roots[i].Equals(Complex.Zero))
				{
					num2++;
				}
				else if (roots[i].Imaginary == 0.0)
				{
					PolynomialUtils.PolynomialFactor polynomialFactor = new PolynomialUtils.PolynomialFactor(new decimal[] { (decimal)(-roots[i].Real) });
					list.Add(polynomialFactor);
				}
				else
				{
					Complex complex = Complex.Conjugate(roots[i]);
					PolynomialUtils.FactorMultiplicity factorMultiplicity;
					if (dictionary.TryGetValue(complex, out factorMultiplicity))
					{
						factorMultiplicity.Multiplicity--;
						PolynomialUtils.PolynomialFactor polynomialFactor2 = new PolynomialUtils.PolynomialFactor(new decimal[]
						{
							(decimal)(roots[i].Real * roots[i].Real + roots[i].Imaginary * roots[i].Imaginary),
							(decimal)(-2.0 * roots[i].Real)
						});
						list.Add(polynomialFactor2);
						if (factorMultiplicity.Multiplicity <= 0)
						{
							dictionary.Remove(complex);
						}
					}
					else if (dictionary.TryGetValue(roots[i], out factorMultiplicity))
					{
						factorMultiplicity.Multiplicity++;
					}
					else
					{
						dictionary.Add(roots[i], new PolynomialUtils.FactorMultiplicity(1));
					}
				}
			}
			if (dictionary.Count > 0)
			{
				return false;
			}
			PolynomialUtils.ByMaximumCoefficient byMaximumCoefficient = new PolynomialUtils.ByMaximumCoefficient();
			list.Sort(byMaximumCoefficient);
			if (num2 < num - 1)
			{
				if (Utils.Size<decimal>(PolynomialUtils.PolynomialFactor.Destination) < num)
				{
					PolynomialUtils.PolynomialFactor.Destination = new decimal[num];
				}
				while (list.Count > 1)
				{
					decimal num3 = Math.Abs(list.ElementAt(0).Key);
					decimal num4 = Math.Abs(list.ElementAt(list.Count - 1).Key);
					PolynomialUtils.PolynomialFactor polynomialFactor3;
					if (num3 < num4)
					{
						polynomialFactor3 = list.ElementAt(0);
						list.RemoveAt(0);
					}
					else
					{
						polynomialFactor3 = list.ElementAt(list.Count - 1);
						list.RemoveAt(list.Count - 1);
					}
					int num5 = list.BinarySearch(new PolynomialUtils.PolynomialFactor(-polynomialFactor3.Key), byMaximumCoefficient);
					if (num5 < 0)
					{
						num5 = ~num5;
					}
					num5 = Math.Min(list.Count - 1, num5);
					PolynomialUtils.PolynomialFactor polynomialFactor4 = list.ElementAt(num5);
					list.RemoveAt(num5);
					polynomialFactor3.Multiply(polynomialFactor4);
					num5 = list.BinarySearch(polynomialFactor3, byMaximumCoefficient);
					if (num5 >= 0)
					{
						list.Insert(num5, polynomialFactor3);
					}
					else
					{
						list.Insert(~num5, polynomialFactor3);
					}
				}
			}
			if (Utils.Size<double>(coefficients) < num)
			{
				coefficients = new double[num];
			}
			for (int i = 0; i < num2; i++)
			{
				coefficients[i] = 0.0;
			}
			if (num2 < num)
			{
				List<decimal> coefficients2 = list.ElementAt(0).Coefficients;
				for (int i = num2; i < num; i++)
				{
					coefficients[i] = decimal.ToDouble(coefficients2[i - num2]);
				}
			}
			return true;
		}

		// Token: 0x04000DB4 RID: 3508
		private static double _tol;

		// Token: 0x02000435 RID: 1077
		private sealed class FactorMultiplicity
		{
			// Token: 0x06001656 RID: 5718 RVA: 0x000829BE File Offset: 0x00080BBE
			public FactorMultiplicity(int multiplicity = 1)
			{
				this.Multiplicity = multiplicity;
			}

			// Token: 0x04000DB5 RID: 3509
			public int Multiplicity;
		}

		// Token: 0x02000436 RID: 1078
		private sealed class PolynomialFactor
		{
			// Token: 0x17000200 RID: 512
			// (get) Token: 0x06001657 RID: 5719 RVA: 0x000829CD File Offset: 0x00080BCD
			public decimal Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x06001658 RID: 5720 RVA: 0x000829D8 File Offset: 0x00080BD8
			private void SetKey()
			{
				decimal num = -1m;
				for (int i = 0; i < this.Coefficients.Count; i++)
				{
					decimal num2 = Math.Abs(this.Coefficients[i]);
					if (num2 > num)
					{
						num = num2;
						this._key = this.Coefficients[i];
					}
				}
			}

			// Token: 0x06001659 RID: 5721 RVA: 0x00082A31 File Offset: 0x00080C31
			public PolynomialFactor(decimal[] coefficients)
			{
				this.Coefficients = new List<decimal>(coefficients);
				this.SetKey();
			}

			// Token: 0x0600165A RID: 5722 RVA: 0x00082A4B File Offset: 0x00080C4B
			internal PolynomialFactor(decimal key)
			{
				this._key = key;
			}

			// Token: 0x0600165B RID: 5723 RVA: 0x00082A5C File Offset: 0x00080C5C
			public void Multiply(PolynomialUtils.PolynomialFactor factor)
			{
				int count = this.Coefficients.Count;
				this.Coefficients.AddRange(factor.Coefficients);
				this.PolynomialMultiplication(0, count, count, factor.Coefficients.Count, 0, 1m, 1m);
				for (int i = 0; i < this.Coefficients.Count; i++)
				{
					this.Coefficients[i] = PolynomialUtils.PolynomialFactor.Destination[i];
				}
				this.SetKey();
			}

			// Token: 0x0600165C RID: 5724 RVA: 0x00082AE0 File Offset: 0x00080CE0
			private void PolynomialMultiplication(int uIndex, int uLen, int vIndex, int vLen, int dstIndex, decimal uCoeff, decimal vCoeff)
			{
				if (uLen == 1 && vLen == 1)
				{
					PolynomialUtils.PolynomialFactor.Destination[dstIndex] = this.Coefficients[uIndex] * this.Coefficients[vIndex];
					PolynomialUtils.PolynomialFactor.Destination[dstIndex + 1] = this.Coefficients[uIndex] + this.Coefficients[vIndex];
					return;
				}
				this.NaivePolynomialMultiplication(uIndex, uLen, vIndex, vLen, dstIndex, uCoeff, vCoeff);
			}

			// Token: 0x0600165D RID: 5725 RVA: 0x00082B68 File Offset: 0x00080D68
			private void NaivePolynomialMultiplication(int uIndex, int uLen, int vIndex, int vLen, int dstIndex, decimal uCoeff, decimal vCoeff)
			{
				int num = uLen + vLen - 1;
				if (vLen < uLen)
				{
					int num2 = vLen;
					vLen = uLen;
					uLen = num2;
					num2 = vIndex;
					vIndex = uIndex;
					uIndex = num2;
				}
				for (int i = 0; i <= num; i++)
				{
					int num3 = Math.Min(uLen, i + 1) - 1;
					int num4 = ((i >= Math.Max(uLen, vLen)) ? (num - i) : (num3 + 1));
					int num5 = Math.Max(0, i - uLen + 1);
					decimal num6 = 0m;
					if (i >= uLen)
					{
						num6 = uCoeff * this.Coefficients[i - uLen + vIndex];
					}
					if (i >= vLen)
					{
						num6 += vCoeff * this.Coefficients[i - vLen + uIndex];
					}
					for (int j = 0; j < num4; j++)
					{
						num6 += this.Coefficients[num3 - j + uIndex] * this.Coefficients[num5 + j + vIndex];
					}
					PolynomialUtils.PolynomialFactor.Destination[i + dstIndex] = num6;
				}
			}

			// Token: 0x04000DB6 RID: 3510
			public List<decimal> Coefficients;

			// Token: 0x04000DB7 RID: 3511
			public static decimal[] Destination;

			// Token: 0x04000DB8 RID: 3512
			private decimal _key;
		}

		// Token: 0x02000437 RID: 1079
		private sealed class ByMaximumCoefficient : IComparer<PolynomialUtils.PolynomialFactor>
		{
			// Token: 0x0600165E RID: 5726 RVA: 0x00082C73 File Offset: 0x00080E73
			public int Compare(PolynomialUtils.PolynomialFactor x, PolynomialUtils.PolynomialFactor y)
			{
				if (x.Key > y.Key)
				{
					return 1;
				}
				if (x.Key < y.Key)
				{
					return -1;
				}
				return 0;
			}
		}
	}
}
