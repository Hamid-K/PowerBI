using System;
using System.Runtime.InteropServices;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.TMSN.TMSNlearn.MathAlgorithms
{
	// Token: 0x0200042F RID: 1071
	public static class EigenUtils
	{
		// Token: 0x06001643 RID: 5699 RVA: 0x00081A4C File Offset: 0x0007FC4C
		public static void EigenDecomposition(float[] A, out float[] eigenvalues, out float[] eigenvectors)
		{
			int num = A.Length;
			int num2 = (int)Math.Sqrt((double)num);
			eigenvectors = new float[num];
			eigenvalues = new float[num2];
			float[] array = new float[num2];
			EigenUtils.Tred(A, eigenvalues, array, eigenvectors, num2);
			EigenUtils.Imtql(eigenvalues, array, eigenvectors, num2);
			for (int i = 0; i < num2; i++)
			{
				eigenvalues[i] = ((eigenvalues[i] <= 0f) ? 0f : ((float)Math.Sqrt((double)eigenvalues[i])));
			}
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00081AC4 File Offset: 0x0007FCC4
		private static float Hypot(float x, float y)
		{
			x = Math.Abs(x);
			y = Math.Abs(y);
			if (x == 0f || y == 0f)
			{
				return x + y;
			}
			if (x < y)
			{
				double num = (double)(x / y);
				return y * (float)Math.Sqrt(1.0 + num * num);
			}
			double num2 = (double)(y / x);
			return x * (float)Math.Sqrt(1.0 + num2 * num2);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00081B30 File Offset: 0x0007FD30
		private static float CopySign(float x, float y)
		{
			float num = Math.Abs(x);
			if (y >= 0f)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00081B50 File Offset: 0x0007FD50
		private static void Tred(float[] a, float[] d, float[] e, float[] z, int n)
		{
			int i;
			for (i = 0; i < n; i++)
			{
				for (int j = i; j < n; j++)
				{
					z[j + i * n] = a[j + i * n];
				}
				d[i] = a[n - 1 + i * n];
			}
			if (n == 1)
			{
				d[0] = z[0];
				z[0] = 1f;
				e[0] = 0f;
				return;
			}
			i = n;
			while (i-- > 1)
			{
				int num = i - 1;
				float num2 = 0f;
				float num3 = 0f;
				if (num == 0)
				{
					e[1] = d[0];
					d[0] = z[0];
					z[1] = 0f;
					z[n] = 0f;
					d[1] = num2;
				}
				else
				{
					for (int k = 0; k < i; k++)
					{
						num3 += Math.Abs(d[k]);
					}
					if (num3 == 0f)
					{
						e[i] = d[num];
						for (int j = 0; j < i; j++)
						{
							d[j] = z[num + j * n];
							z[i + j * n] = 0f;
							z[j + i * n] = 0f;
						}
						d[i] = num2;
					}
					else
					{
						for (int k = 0; k < i; k++)
						{
							d[k] /= num3;
							num2 += d[k] * d[k];
						}
						float num4 = d[num];
						float num5 = EigenUtils.CopySign((float)Math.Sqrt((double)num2), num4);
						e[i] = num3 * num5;
						num2 -= num4 * num5;
						d[num] = num4 - num5;
						for (int j = 0; j < i; j++)
						{
							e[j] = 0f;
						}
						for (int j = 0; j < i; j++)
						{
							num4 = d[j];
							z[j + i * n] = num4;
							num5 = e[j] + z[j + j * n] * num4;
							if (j + 1 == i)
							{
								e[j] = num5;
							}
							else
							{
								for (int k = j + 1; k < i; k++)
								{
									num5 += z[k + j * n] * d[k];
									e[k] += z[k + j * n] * num4;
								}
								e[j] = num5;
							}
						}
						num4 = 0f;
						for (int j = 0; j < i; j++)
						{
							e[j] /= num2;
							num4 += e[j] * d[j];
						}
						float num6 = num4 / (num2 + num2);
						for (int j = 0; j < i; j++)
						{
							e[j] -= num6 * d[j];
						}
						for (int j = 0; j < i; j++)
						{
							num4 = d[j];
							num5 = e[j];
							for (int k = j; k < i; k++)
							{
								z[k + j * n] = (float)((double)z[k + j * n] - (double)num4 * (double)e[k] - (double)num5 * (double)d[k]);
							}
							d[j] = z[num + j * n];
							z[i + j * n] = 0f;
						}
						d[i] = num2;
					}
				}
			}
			for (i = 1; i < n; i++)
			{
				int num = i - 1;
				z[n - 1 + num * n] = z[num + num * n];
				z[num + num * n] = 1f;
				float num2 = d[i];
				if (num2 != 0f)
				{
					for (int k = 0; k < i; k++)
					{
						d[k] = z[k + i * n] / num2;
					}
					for (int j = 0; j < i; j++)
					{
						float num5 = 0f;
						for (int k = 0; k < i; k++)
						{
							num5 += z[k + i * n] * z[k + j * n];
						}
						for (int k = 0; k < i; k++)
						{
							z[k + j * n] -= num5 * d[k];
						}
					}
				}
				for (int k = 0; k < i; k++)
				{
					z[k + i * n] = 0f;
				}
			}
			for (i = 0; i < n; i++)
			{
				d[i] = z[n - 1 + i * n];
				z[n - 1 + i * n] = 0f;
			}
			z[n * n - 1] = 1f;
			e[0] = 0f;
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00081F54 File Offset: 0x00080154
		private static int Imtql(float[] d, float[] e, float[] z, int n)
		{
			if (n == 1)
			{
				return 0;
			}
			for (int i = 1; i < n; i++)
			{
				e[i - 1] = e[i];
			}
			e[n - 1] = 0f;
			for (int j = 0; j < n; j++)
			{
				int k = 0;
				int num;
				do
				{
					num = j;
					while (num + 1 < n)
					{
						double num2 = (double)(Math.Abs(d[num]) + Math.Abs(d[num + 1]));
						double num3 = num2 + (double)Math.Abs(e[num]);
						if (num3 == num2)
						{
							break;
						}
						num++;
					}
					double num4 = (double)d[j];
					if (num != j)
					{
						if (k++ >= 30)
						{
							return j;
						}
						double num5 = ((double)d[j + 1] - num4) / (double)(e[j] * 2f);
						double num6 = (double)EigenUtils.Hypot((float)num5, 1f);
						num5 = (double)d[num] - num4 + (double)e[j] / (num5 + (double)EigenUtils.CopySign((float)num6, (float)num5));
						double num7 = 1.0;
						double num8 = 1.0;
						num4 = 0.0;
						int i;
						for (i = num - 1; i >= j; i--)
						{
							double num9 = num7 * (double)e[i];
							double num10 = num8 * (double)e[i];
							num6 = (double)EigenUtils.Hypot((float)num9, (float)num5);
							e[i + 1] = (float)num6;
							if (num6 == 0.0)
							{
								d[i + 1] -= (float)num4;
								e[num] = 0f;
								break;
							}
							num7 = num9 / num6;
							num8 = num5 / num6;
							num5 = (double)d[i + 1] - num4;
							num6 = ((double)d[i] - num5) * num7 + num8 * 2.0 * num10;
							num4 = num7 * num6;
							d[i + 1] = (float)(num5 + num4);
							num5 = num8 * num6 - num10;
							for (int l = 0; l < n; l++)
							{
								num9 = (double)z[l + (i + 1) * n];
								z[l + (i + 1) * n] = (float)(num7 * (double)z[l + i * n] + num8 * num9);
								z[l + i * n] = (float)(num8 * (double)z[l + i * n] - num7 * num9);
							}
						}
						if (num6 != 0.0 || i < j)
						{
							d[j] -= (float)num4;
							e[j] = (float)num5;
							e[num] = 0f;
						}
					}
				}
				while (num != j);
			}
			for (int i = 0; i < n; i++)
			{
				int l = i;
				double num4 = (double)d[i];
				for (int k = i + 1; k < n; k++)
				{
					if ((double)d[k] > num4)
					{
						l = k;
						num4 = (double)d[k];
					}
				}
				if (l != i)
				{
					d[l] = d[i];
					d[i] = (float)num4;
					for (int k = 0; k < n; k++)
					{
						num4 = (double)z[k + i * n];
						z[k + i * n] = z[k + l * n];
						z[k + l * n] = (float)num4;
					}
				}
			}
			return 0;
		}

		// Token: 0x06001648 RID: 5704
		[DllImport("Microsoft.MachineLearning.MklImports.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LAPACKE_shseqr")]
		public static extern int Shseqr(EigenUtils.Layout matrixLayout, EigenUtils.Job job, EigenUtils.Compz compz, int n, int ilo, int ihi, [In] float[] h, int idh, [Out] float[] wr, [Out] float[] wi, [Out] float[] z, int ldz);

		// Token: 0x06001649 RID: 5705
		[DllImport("Microsoft.MachineLearning.MklImports.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LAPACKE_dhseqr")]
		public static extern int Dhseqr(EigenUtils.Layout matrixLayout, EigenUtils.Job job, EigenUtils.Compz compz, int n, int ilo, int ihi, [In] double[] h, int idh, [Out] double[] wr, [Out] double[] wi, [Out] double[] z, int ldz);

		// Token: 0x0600164A RID: 5706
		[DllImport("Microsoft.MachineLearning.MklImports.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LAPACKE_ssytrd")]
		public static extern int Ssytrd(EigenUtils.Layout matrixLayout, EigenUtils.Uplo uplo, int n, float[] a, int lda, float[] d, float[] e, float[] tau);

		// Token: 0x0600164B RID: 5707
		[DllImport("Microsoft.MachineLearning.MklImports.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LAPACKE_dsytrd")]
		public static extern int Dsytrd(EigenUtils.Layout matrixLayout, EigenUtils.Uplo uplo, int n, double[] a, int lda, double[] d, double[] e, double[] tau);

		// Token: 0x0600164C RID: 5708
		[DllImport("Microsoft.MachineLearning.MklImports.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LAPACKE_ssteqr")]
		public static extern int Ssteqr(EigenUtils.Layout matrixLayout, EigenUtils.Compz compz, int n, float[] d, float[] e, float[] z, int ldz);

		// Token: 0x0600164D RID: 5709
		[DllImport("Microsoft.MachineLearning.MklImports.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LAPACKE_dsteqr")]
		public static extern int Dsteqr(EigenUtils.Layout matrixLayout, EigenUtils.Compz compz, int n, double[] d, double[] e, double[] z, int ldz);

		// Token: 0x0600164E RID: 5710
		[DllImport("Microsoft.MachineLearning.MklImports.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LAPACKE_sorgtr")]
		public static extern int Sorgtr(EigenUtils.Layout matrixLayout, EigenUtils.Uplo Uplo, int n, float[] a, int lda, float[] tau);

		// Token: 0x0600164F RID: 5711
		[DllImport("Microsoft.MachineLearning.MklImports.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LAPACKE_dorgtr")]
		public static extern int Dorgtr(EigenUtils.Layout matrixLayout, EigenUtils.Uplo Uplo, int n, double[] a, int lda, double[] tau);

		// Token: 0x06001650 RID: 5712 RVA: 0x00082254 File Offset: 0x00080454
		public static bool MklSymmetricEigenDecomposition(float[] A, int size, out float[] eigenValues, out float[] eigenVectors)
		{
			Contracts.CheckParam(size > 0, "The input matrix size must be strictly positive.");
			int num = size * size;
			Contracts.Check(Utils.Size<float>(A) >= num, string.Format("The input matrix must at least have {0} elements.", num));
			eigenValues = null;
			eigenVectors = null;
			if (size == 1)
			{
				eigenValues = new float[] { A[0] };
				eigenVectors = new float[] { 1f };
				return true;
			}
			double[] array = new double[num];
			Array.Copy(A, 0, array, 0, num);
			double[] array2 = new double[size];
			double[] array3 = new double[size - 1];
			double[] array4 = new double[size];
			int num2 = EigenUtils.Dsytrd(EigenUtils.Layout.ColMajor, EigenUtils.Uplo.UpperTriangular, size, array, size, array2, array3, array4);
			if (num2 != 0)
			{
				return false;
			}
			num2 = EigenUtils.Dorgtr(EigenUtils.Layout.ColMajor, EigenUtils.Uplo.UpperTriangular, size, array, size, array4);
			if (num2 != 0)
			{
				return false;
			}
			num2 = EigenUtils.Dsteqr(EigenUtils.Layout.ColMajor, EigenUtils.Compz.SchurA, size, array2, array3, array, size);
			if (num2 != 0)
			{
				return false;
			}
			eigenValues = new float[size];
			for (int i = 0; i < size; i++)
			{
				eigenValues[i] = (float)array2[i];
			}
			eigenVectors = new float[num];
			for (int j = 0; j < num; j++)
			{
				eigenVectors[j] = (float)array[j];
			}
			return true;
		}

		// Token: 0x04000DA6 RID: 3494
		private const string DllName = "Microsoft.MachineLearning.MklImports.dll";

		// Token: 0x02000430 RID: 1072
		public enum Layout
		{
			// Token: 0x04000DA8 RID: 3496
			RowMajor = 101,
			// Token: 0x04000DA9 RID: 3497
			ColMajor
		}

		// Token: 0x02000431 RID: 1073
		public enum Job : byte
		{
			// Token: 0x04000DAB RID: 3499
			EigenValues = 69,
			// Token: 0x04000DAC RID: 3500
			Schur = 83
		}

		// Token: 0x02000432 RID: 1074
		public enum Compz : byte
		{
			// Token: 0x04000DAE RID: 3502
			None = 78,
			// Token: 0x04000DAF RID: 3503
			SchurH = 73,
			// Token: 0x04000DB0 RID: 3504
			SchurA = 86
		}

		// Token: 0x02000433 RID: 1075
		public enum Uplo : byte
		{
			// Token: 0x04000DB2 RID: 3506
			UpperTriangular = 85,
			// Token: 0x04000DB3 RID: 3507
			LowerTriangular = 76
		}
	}
}
