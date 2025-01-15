using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x0200043D RID: 1085
	public static class GradientTester
	{
		// Token: 0x0600167B RID: 5755 RVA: 0x000837A6 File Offset: 0x000819A6
		public static float Test(DifferentiableFunction f, ref VBuffer<float> x)
		{
			return GradientTester.Test(f, ref x, false);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x000837B0 File Offset: 0x000819B0
		public static float Test(DifferentiableFunction f, ref VBuffer<float> x, bool quiet)
		{
			VBuffer<float> vbuffer = default(VBuffer<float>);
			VBuffer<float> vbuffer2 = default(VBuffer<float>);
			VBuffer<float> vbuffer3 = default(VBuffer<float>);
			VectorUtils.Norm(ref x);
			f(ref x, ref vbuffer, null);
			if (!quiet)
			{
				Console.WriteLine(GradientTester.Header);
			}
			float num = float.NegativeInfinity;
			int num2 = Math.Min(x.Length, 10);
			int num3 = Math.Min(x.Length / 2, 100);
			for (int i = 1; i <= num2; i++)
			{
				int num4 = Math.Min(i * 10, num3);
				List<int> list = new List<int>(num4);
				List<float> list2 = new List<float>(num4);
				for (int j = 0; j < num4; j++)
				{
					int num5 = GradientTester.r.Next(x.Length);
					while (list.IndexOf(num5) >= 0)
					{
						num5 = GradientTester.r.Next(x.Length);
					}
					list.Add(num5);
					list2.Add(GradientTester.SampleFromGaussian(GradientTester.r));
				}
				VBuffer<float> vbuffer4 = new VBuffer<float>(x.Length, list2.Count, list2.ToArray(), list.ToArray());
				float num6 = VectorUtils.Norm(ref vbuffer4);
				VectorUtils.ScaleBy(ref vbuffer4, 1f / num6);
				VectorUtils.AddMultInto(ref x, 4.79E-06f, ref vbuffer4, ref vbuffer3);
				float num7 = f(ref vbuffer3, ref vbuffer2, null);
				VectorUtils.AddMultInto(ref x, -4.79E-06f, ref vbuffer4, ref vbuffer3);
				float num8 = f(ref vbuffer3, ref vbuffer2, null);
				float num9 = VectorUtils.DotProduct(ref vbuffer, ref vbuffer4);
				float num10 = (num7 - num8) / 9.58E-06f;
				float num11 = Math.Abs(1f - num10 / num9);
				float num12 = num10 - num9;
				if (!quiet)
				{
					Console.WriteLine("{0,-9}{1,-18:0.0000e0}{2,-18:0.0000e0}{3,-15:0.0000e0}{4,0:0.0000e0}", new object[] { i, num10, num9, num12, num11 });
				}
				num = Math.Max(num, num11);
			}
			return num;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000839B0 File Offset: 0x00081BB0
		public static void TestAllCoords(DifferentiableFunction f, ref VBuffer<float> x)
		{
			VBuffer<float> vbuffer = default(VBuffer<float>);
			VBuffer<float> vbuffer2 = default(VBuffer<float>);
			VBuffer<float> vbuffer3 = default(VBuffer<float>);
			f(ref x, ref vbuffer, null);
			VectorUtils.Norm(ref x);
			Console.WriteLine(GradientTester.Header);
			new Random(5);
			int length = x.Length;
			int num = 1;
			float[] array = new float[] { 1f };
			int[] array2 = new int[1];
			VBuffer<float> vbuffer4 = new VBuffer<float>(length, num, array, array2);
			for (int i = 0; i < x.Length; i++)
			{
				vbuffer4.Values[0] = (float)i;
				VectorUtils.AddMultInto(ref x, 4.79E-06f, ref vbuffer4, ref vbuffer3);
				float num2 = f(ref vbuffer3, ref vbuffer2, null);
				VectorUtils.AddMultInto(ref x, -4.79E-06f, ref vbuffer4, ref vbuffer3);
				float num3 = f(ref vbuffer3, ref vbuffer2, null);
				float num4 = VectorUtils.DotProduct(ref vbuffer, ref vbuffer4);
				float num5 = (num2 - num3) / 9.58E-06f;
				float num6 = Math.Abs(1f - num5 / num4);
				float num7 = num5 - num4;
				if (num7 != 0f)
				{
					Console.WriteLine("{0,-9}{1,-18:0.0000e0}{2,-18:0.0000e0}{3,-15:0.0000e0}{4,0:0.0000e0}", new object[] { i, num5, num4, num7, num6 });
				}
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00083B0C File Offset: 0x00081D0C
		public static void TestCoords(DifferentiableFunction f, ref VBuffer<float> x, IList<int> coords)
		{
			VBuffer<float> vbuffer = default(VBuffer<float>);
			VBuffer<float> vbuffer2 = default(VBuffer<float>);
			VBuffer<float> vbuffer3 = default(VBuffer<float>);
			f(ref x, ref vbuffer, null);
			VectorUtils.Norm(ref x);
			Console.WriteLine(GradientTester.Header);
			new Random(5);
			int length = x.Length;
			int num = 1;
			float[] array = new float[] { 1f };
			int[] array2 = new int[1];
			VBuffer<float> vbuffer4 = new VBuffer<float>(length, num, array, array2);
			foreach (int num2 in coords)
			{
				vbuffer4.Values[0] = (float)num2;
				VectorUtils.AddMultInto(ref x, 4.79E-06f, ref vbuffer4, ref vbuffer3);
				float num3 = f(ref vbuffer3, ref vbuffer2, null);
				VectorUtils.AddMultInto(ref x, -4.79E-06f, ref vbuffer4, ref vbuffer3);
				float num4 = f(ref vbuffer3, ref vbuffer2, null);
				float num5 = VectorUtils.DotProduct(ref vbuffer, ref vbuffer4);
				float num6 = (num3 - num4) / 9.58E-06f;
				float num7 = Math.Abs(1f - num6 / num5);
				float num8 = num6 - num5;
				Console.WriteLine("{0,-9}{1,-18:0.0000e0}{2,-18:0.0000e0}{3,-15:0.0000e0}{4,0:0.0000e0}", new object[] { num2, num6, num5, num8, num7 });
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00083C84 File Offset: 0x00081E84
		public static float Test(DifferentiableFunction f, ref VBuffer<float> x, ref VBuffer<float> dir, bool quiet, ref VBuffer<float> newGrad, ref VBuffer<float> newX)
		{
			float num = VectorUtils.Norm(ref dir);
			f(ref x, ref newGrad, null);
			float num2 = VectorUtils.DotProduct(ref newGrad, ref dir);
			float num3 = 4.79E-06f / num;
			VectorUtils.AddMultInto(ref x, num3, ref dir, ref newX);
			float num4 = f(ref newX, ref newGrad, null);
			VectorUtils.AddMultInto(ref x, -num3, ref dir, ref newX);
			float num5 = f(ref newX, ref newGrad, null);
			float num6 = (num4 - num5) / (2f * num3);
			float num7 = Math.Abs(1f - num6 / num2);
			float num8 = num6 - num2;
			if (!quiet)
			{
				Console.WriteLine("{0,-18:0.0000e0}{1,-18:0.0000e0}{2,-15:0.0000e0}{3,0:0.0000e0}", new object[] { num6, num2, num8, num7 });
			}
			return num7;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00083D4C File Offset: 0x00081F4C
		private static float SampleFromGaussian(Random r)
		{
			double num = r.NextDouble();
			double num2 = r.NextDouble();
			return (float)(Math.Sqrt(-2.0 * Math.Log(num)) * MathUtils.Cos(6.283185307179586 * num2));
		}

		// Token: 0x04000DCA RID: 3530
		private const float EPS = 4.79E-06f;

		// Token: 0x04000DCB RID: 3531
		private static Random r = new Random(5);

		// Token: 0x04000DCC RID: 3532
		public static readonly string Header = "Trial    Numeric deriv     Analytic deriv    Difference     Normalized";
	}
}
