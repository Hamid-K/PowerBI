using System;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x0200044E RID: 1102
	public static class Test
	{
		// Token: 0x060016E1 RID: 5857 RVA: 0x0008581E File Offset: 0x00083A1E
		private static float QuadTest(float x, out float deriv)
		{
			deriv = 1.32842f * x + -28.38092f;
			return 0.66421f * x * x + -28.38092f * x + 93f;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00085848 File Offset: 0x00083A48
		private static float LogTest(float x, out float deriv)
		{
			double num = Math.Exp((double)x);
			deriv = (float)(-1.0 / (1.0 + num) + num / (1.0 + num) - 0.5);
			return (float)(Math.Log(1.0 + 1.0 / num) + Math.Log(1.0 + num) - 0.5 * (double)x);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000858C8 File Offset: 0x00083AC8
		private static float QuadTest2D(ref VBuffer<float> x, ref VBuffer<float> grad, IProgressChannelProvider progress = null)
		{
			float num = VectorUtils.DotProduct(ref x, ref Test.c1);
			float num2 = VectorUtils.DotProduct(ref x, ref Test.c2);
			float num3 = VectorUtils.DotProduct(ref x, ref Test.c3);
			Test.c3.CopyTo(ref grad);
			VectorUtils.AddMult(ref Test.c1, num, ref grad);
			VectorUtils.AddMult(ref Test.c2, num2, ref grad);
			return 0.5f * (num * num + num2 * num2) + num3 + 55f;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00085931 File Offset: 0x00083B31
		private static void StochasticQuadTest2D(ref VBuffer<float> x, ref VBuffer<float> grad)
		{
			Test.QuadTest2D(ref x, ref grad, null);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0008593C File Offset: 0x00083B3C
		private static void CreateWrapped(out VBuffer<float> vec, params float[] values)
		{
			vec = new VBuffer<float>(Utils.Size<float>(values), values, null);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00085954 File Offset: 0x00083B54
		static Test()
		{
			Test.CreateWrapped(out Test.c1, new float[] { 1f, 2f });
			Test.CreateWrapped(out Test.c2, new float[] { -2f, -3f });
			Test.CreateWrapped(out Test.c3, new float[] { -1f, 3f });
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000859C8 File Offset: 0x00083BC8
		private static void RunTest(DiffFunc1D F)
		{
			CubicInterpLineSearch cubicInterpLineSearch = new CubicInterpLineSearch(1E-08f);
			float num2;
			float num = F(0f, out num2);
			float num3 = cubicInterpLineSearch.Minimize(F, num, num2);
			num = F(num3, out num2);
			Console.WriteLine(num2);
			GoldenSectionSearch goldenSectionSearch = new GoldenSectionSearch(1E-08f);
			num3 = goldenSectionSearch.Minimize(F);
			num = F(num3, out num2);
			Console.WriteLine(num2);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00085AA8 File Offset: 0x00083CA8
		public static void Main(string[] argv)
		{
			Test.RunTest(new DiffFunc1D(Test.QuadTest));
			Test.RunTest(new DiffFunc1D(Test.LogTest));
			VBuffer<float> grad = VBufferUtils.CreateEmpty<float>(2);
			int n = 0;
			bool print = false;
			DTerminate dterminate = delegate(ref VBuffer<float> x)
			{
				Test.QuadTest2D(ref x, ref grad, null);
				float num = VectorUtils.Norm(ref grad);
				if (++n % 1000 == 0 || print)
				{
					Console.WriteLine("{0}\t{1}", n, num);
				}
				return (double)num < 1E-05;
			};
			SGDOptimizer sgdoptimizer = new SGDOptimizer(dterminate, SGDOptimizer.RateScheduleType.Constant, false, 100f, 1, 0.99f, 0);
			float[] array = new float[2];
			VBuffer<float> vbuffer;
			Test.CreateWrapped(out vbuffer, array);
			VBuffer<float> vbuffer2 = default(VBuffer<float>);
			sgdoptimizer.Minimize(new SGDOptimizer.DStochasticGradient(Test.StochasticQuadTest2D), ref vbuffer, ref vbuffer2);
			Test.QuadTest2D(ref vbuffer2, ref grad, null);
			Console.WriteLine(VectorUtils.Norm(ref grad));
			Console.WriteLine();
			Console.WriteLine();
			n = 0;
			GDOptimizer gdoptimizer = new GDOptimizer(dterminate, null, true, 0);
			print = true;
			float[] array2 = new float[2];
			Test.CreateWrapped(out vbuffer, array2);
			gdoptimizer.Minimize(new DifferentiableFunction(Test.QuadTest2D), ref vbuffer, ref vbuffer2);
			Test.QuadTest2D(ref vbuffer2, ref grad, null);
			Console.WriteLine(VectorUtils.Norm(ref grad));
		}

		// Token: 0x04000E06 RID: 3590
		private static VBuffer<float> c1;

		// Token: 0x04000E07 RID: 3591
		private static VBuffer<float> c2;

		// Token: 0x04000E08 RID: 3592
		private static VBuffer<float> c3;
	}
}
