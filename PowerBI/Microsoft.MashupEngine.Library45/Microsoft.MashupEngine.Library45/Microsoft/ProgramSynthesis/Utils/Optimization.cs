using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004D6 RID: 1238
	public static class Optimization
	{
		// Token: 0x06001B87 RID: 7047 RVA: 0x0005292C File Offset: 0x00050B2C
		public static double[] HillClimb(Func<double[], double> f, double[] x0, int steps, double stepSize = 1.0)
		{
			double[] array = new double[x0.Length];
			Array.Copy(x0, array, array.Length);
			double num = f(array);
			for (int i = 0; i < steps; i++)
			{
				bool flag = false;
				for (int j = 0; j < x0.Length; j++)
				{
					foreach (double num2 in new double[] { -1.0, 1.0 })
					{
						array[j] += num2 * stepSize;
						double num3 = f(array);
						if (num3 > num)
						{
							num = num3;
							flag = true;
							Trace.WriteLine("f([" + string.Join<double>(",", array) + "])=" + num.ToString());
							break;
						}
						array[j] -= num2 * stepSize;
					}
				}
				if (!flag)
				{
					Trace.WriteLine(FormattableString.Invariant(FormattableStringFactory.Create("Reached local maximum after {0} steps", new object[] { i })));
					break;
				}
			}
			return array;
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00052A40 File Offset: 0x00050C40
		public static double[] GradientAscent(Func<double[], Record<double, double[]>> objectiveAndGradient, double[] x0, int steps, double learningRate, double decayRate = 1.0, bool verbose = false)
		{
			double[] array = new double[x0.Length];
			double[] array2 = new double[x0.Length];
			Array.Copy(x0, array, array.Length);
			Array.Copy(x0, array2, x0.Length);
			double num = double.NegativeInfinity;
			if (verbose)
			{
				Trace.WriteLine(FormattableString.Invariant(FormattableStringFactory.Create("GradientAscent: learningRate = {0}, decayRate = {1}", new object[] { learningRate, decayRate })));
				Trace.WriteLine("N\t|df|^2\tf(x)");
			}
			for (int i = 0; i < steps; i++)
			{
				Record<double, double[]> record = objectiveAndGradient(array);
				double[] item = record.Item2;
				double item2 = record.Item1;
				if (double.IsNegativeInfinity(num) || item2 > num)
				{
					Array.Copy(array, array2, x0.Length);
					num = item2;
				}
				for (int j = 0; j < array.Length; j++)
				{
					array[j] += learningRate * item[j];
				}
				learningRate *= decayRate;
				if (verbose)
				{
					double num2 = item.InnerProduct(item);
					Trace.WriteLine(FormattableString.Invariant(FormattableStringFactory.Create("{0,-5} {1,10:f2} {2,10:f3}", new object[] { i, num2, item2 })));
				}
			}
			return array2;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00052B6C File Offset: 0x00050D6C
		public static double[] Adagrad(Func<double[], Record<double, double[]>> objectiveAndGradient, double[] x0, int steps, double learningRate = 0.5, double decayRate = 1.0, bool verbose = false, double epsilon = 1E-08)
		{
			double[] array = Enumerable.Repeat<double>(0.0, x0.Length).ToArray<double>();
			double[] array2 = new double[x0.Length];
			double[] array3 = new double[x0.Length];
			Array.Copy(x0, array2, array2.Length);
			Array.Copy(x0, array3, x0.Length);
			double num = double.NegativeInfinity;
			if (verbose)
			{
				Trace.WriteLine(FormattableString.Invariant(FormattableStringFactory.Create("Adagrad: learningRate = {0}, decayRate = {1}", new object[] { learningRate, decayRate })));
				Trace.WriteLine("N\t|df|^2\tf(x)");
			}
			for (int i = 0; i < steps; i++)
			{
				Record<double, double[]> record = objectiveAndGradient(array2);
				double[] item = record.Item2;
				double item2 = record.Item1;
				if (double.IsNegativeInfinity(num) || item2 > num)
				{
					Array.Copy(array2, array3, x0.Length);
					num = item2;
				}
				for (int j = 0; j < array2.Length; j++)
				{
					array[j] += item[j] * item[j];
					array2[j] += learningRate * item[j] / (epsilon + Math.Sqrt(array[j]));
				}
				learningRate *= decayRate;
				if (verbose)
				{
					double num2 = item.InnerProduct(item);
					Trace.WriteLine(FormattableString.Invariant(FormattableStringFactory.Create("{0,-5} {1,10:f2} {2,10:f3}", new object[] { i, num2, item2 })));
				}
			}
			return array3;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00052CD8 File Offset: 0x00050ED8
		public static double[] Adam(Func<double[], Record<double, double[]>> objectiveAndGradient, double[] x0, int steps, double learningRate = 0.001, bool verbose = false, double epsilon = 1E-08)
		{
			double num = 0.9;
			double num2 = 0.999;
			double[] array = Enumerable.Repeat<double>(0.0, x0.Length).ToArray<double>();
			double[] array2 = Enumerable.Repeat<double>(0.0, x0.Length).ToArray<double>();
			double[] array3 = new double[x0.Length];
			double[] array4 = new double[x0.Length];
			Array.Copy(x0, array3, array3.Length);
			Array.Copy(x0, array4, x0.Length);
			double num3 = double.NegativeInfinity;
			double num4 = num;
			double num5 = num2;
			if (verbose)
			{
				Trace.WriteLine(FormattableString.Invariant(FormattableStringFactory.Create("Adam: learningRate = {0}", new object[] { learningRate })));
				Trace.WriteLine("N\t|df|^2\tf(x)");
			}
			for (int i = 1; i <= steps; i++)
			{
				Record<double, double[]> record = objectiveAndGradient(array3);
				double[] item = record.Item2;
				double item2 = record.Item1;
				if (double.IsNegativeInfinity(num3) || item2 > num3)
				{
					Array.Copy(array3, array4, x0.Length);
					num3 = item2;
				}
				for (int j = 0; j < array3.Length; j++)
				{
					array[j] = num * array[j] + (1.0 - num) * item[j];
					array2[j] = num2 * array2[j] + (1.0 - num2) * item[j] * item[j];
					double num6 = array[j] / (1.0 - num4);
					double num7 = array2[j] / (1.0 - num5);
					array3[j] += learningRate * num6 / (Math.Sqrt(num7) + epsilon);
				}
				num4 *= num;
				num5 *= num2;
				if (verbose)
				{
					double num8 = item.InnerProduct(item);
					Trace.WriteLine(FormattableString.Invariant(FormattableStringFactory.Create("{0,-5} {1,10:f2} {2,10:f3}", new object[] { i, num8, item2 })));
				}
			}
			return array4;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00052ED0 File Offset: 0x000510D0
		public static double[] RMSProp(Func<double[], Record<double, double[]>> objectiveAndGradient, double[] x0, int steps, double learningRate = 0.1, double decayRate = 1.0, bool verbose = false, double epsilon = 1E-08)
		{
			double[] array = Enumerable.Repeat<double>(0.0, x0.Length).ToArray<double>();
			double[] array2 = new double[x0.Length];
			double[] array3 = new double[x0.Length];
			Array.Copy(x0, array2, array2.Length);
			Array.Copy(x0, array3, x0.Length);
			double num = double.NegativeInfinity;
			double num2 = 0.75;
			if (verbose)
			{
				Trace.WriteLine(FormattableString.Invariant(FormattableStringFactory.Create("RMSprop: learningRate = {0}, decayRate = {1}", new object[] { learningRate, decayRate })));
				Trace.WriteLine("N\t|df|^2\tf(x)");
			}
			for (int i = 1; i <= steps; i++)
			{
				Record<double, double[]> record = objectiveAndGradient(array2);
				double[] item = record.Item2;
				double item2 = record.Item1;
				if (double.IsNegativeInfinity(num) || item2 > num)
				{
					Array.Copy(array2, array3, x0.Length);
					num = item2;
				}
				for (int j = 0; j < array2.Length; j++)
				{
					array[j] = num2 * array[j] + (1.0 - num2) * item[j] * item[j];
					array2[j] += learningRate * item[j] / (Math.Sqrt(array[j]) + epsilon);
				}
				learningRate *= decayRate;
				if (verbose)
				{
					double num3 = item.InnerProduct(item);
					Trace.WriteLine(FormattableString.Invariant(FormattableStringFactory.Create("{0,-5} {1,10:f2} {2,10:f3}", new object[] { i, num3, item2 })));
				}
			}
			return array3;
		}
	}
}
