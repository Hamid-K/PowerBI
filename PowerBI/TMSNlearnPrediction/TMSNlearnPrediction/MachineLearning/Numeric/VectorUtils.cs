using System;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.CpuMath;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x020004DD RID: 1245
	public static class VectorUtils
	{
		// Token: 0x06001970 RID: 6512 RVA: 0x0008FCAB File Offset: 0x0008DEAB
		public static float NormSquared(ref VBuffer<float> a)
		{
			if (a.Count == 0)
			{
				return 0f;
			}
			return SseUtils.SumSq(a.Values, 0, a.Count);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0008FCCD File Offset: 0x0008DECD
		public static float NormSquared(float[] a, int offset, int count)
		{
			return SseUtils.SumSq(a, offset, count);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0008FCD7 File Offset: 0x0008DED7
		public static float Norm(ref VBuffer<float> a)
		{
			return MathUtils.Sqrt(VectorUtils.NormSquared(ref a));
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0008FCE4 File Offset: 0x0008DEE4
		public static float L1Norm(ref VBuffer<float> a)
		{
			if (a.Count == 0)
			{
				return 0f;
			}
			return SseUtils.SumAbs(a.Values, 0, a.Count);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0008FD06 File Offset: 0x0008DF06
		public static float MaxNorm(ref VBuffer<float> a)
		{
			if (a.Count == 0)
			{
				return 0f;
			}
			return SseUtils.MaxAbs(a.Values, 0, a.Count);
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0008FD28 File Offset: 0x0008DF28
		public static float Sum(ref VBuffer<float> a)
		{
			if (a.Count == 0)
			{
				return 0f;
			}
			return SseUtils.Sum(a.Values, 0, a.Count);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0008FD4A File Offset: 0x0008DF4A
		public static void ScaleBy(ref VBuffer<float> dst, float c)
		{
			if (c == 1f || dst.Count == 0)
			{
				return;
			}
			if (c != 0f)
			{
				SseUtils.Scale(c, dst.Values, dst.Count);
				return;
			}
			Array.Clear(dst.Values, 0, dst.Count);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0008FD8C File Offset: 0x0008DF8C
		public static void ScaleBy(ref VBuffer<float> src, ref VBuffer<float> dst, float c)
		{
			int length = src.Length;
			int count = src.Count;
			if (count == 0)
			{
				dst = new VBuffer<float>(length, 0, dst.Values, dst.Indices);
				return;
			}
			float[] array = ((Utils.Size<float>(dst.Values) >= count) ? dst.Values : new float[count]);
			if (src.IsDense)
			{
				if (c == 0f)
				{
					Array.Clear(array, 0, length);
				}
				else
				{
					SseUtils.Scale(c, src.Values, array, length);
				}
				dst = new VBuffer<float>(length, array, dst.Indices);
				return;
			}
			int[] array2 = ((Utils.Size<int>(dst.Indices) >= count) ? dst.Indices : new int[count]);
			Array.Copy(src.Indices, array2, count);
			if (c == 0f)
			{
				Array.Clear(array, 0, count);
			}
			else
			{
				SseUtils.Scale(c, src.Values, array, count);
			}
			dst = new VBuffer<float>(length, count, array, array2);
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0008FE80 File Offset: 0x0008E080
		public static void Add(ref VBuffer<float> src, ref VBuffer<float> dst)
		{
			Contracts.Check(src.Length == dst.Length, "Vectors must have the same dimensionality.");
			if (src.Count == 0)
			{
				return;
			}
			if (!dst.IsDense)
			{
				VBufferUtils.ApplyWith<float, float>(ref src, ref dst, delegate(int i, float v1, ref float v2)
				{
					v2 += v1;
				});
				return;
			}
			if (src.IsDense)
			{
				SseUtils.Add(src.Values, dst.Values, src.Length);
				return;
			}
			SseUtils.Add(src.Values, src.Indices, dst.Values, src.Count);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0008FF30 File Offset: 0x0008E130
		public static void AddMult(ref VBuffer<float> src, float c, ref VBuffer<float> dst)
		{
			Contracts.Check(src.Length == dst.Length, "Vectors must have the same dimensionality.");
			if (src.Count == 0 || c == 0f)
			{
				return;
			}
			if (!dst.IsDense)
			{
				VBufferUtils.ApplyWith<float, float>(ref src, ref dst, delegate(int i, float v1, ref float v2)
				{
					v2 += c * v1;
				});
				return;
			}
			if (src.IsDense)
			{
				SseUtils.AddScale(c, src.Values, dst.Values, src.Length);
				return;
			}
			SseUtils.AddScale(c, src.Values, src.Indices, dst.Values, src.Count);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0008FFF4 File Offset: 0x0008E1F4
		public static void AddMult(ref VBuffer<float> src, float c, ref VBuffer<float> dst, ref VBuffer<float> res)
		{
			Contracts.Check(src.Length == dst.Length, "Vectors must have the same dimensionality.");
			int length = src.Length;
			if (src.Count == 0 || c == 0f)
			{
				dst.CopyTo(ref res);
				return;
			}
			if (dst.IsDense && src.IsDense)
			{
				float[] array = ((Utils.Size<float>(res.Values) >= length) ? res.Values : new float[length]);
				SseUtils.AddScaleCopy(c, src.Values, dst.Values, array, length);
				res = new VBuffer<float>(length, array, res.Indices);
				return;
			}
			VBufferUtils.ApplyWithCopy<float, float>(ref src, ref dst, ref res, delegate(int i, float v1, float v2, ref float v3)
			{
				v3 = v2 + c * v1;
			});
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000900CC File Offset: 0x0008E2CC
		public static void AddMultInto(ref VBuffer<float> a, float c, ref VBuffer<float> b, ref VBuffer<float> dst)
		{
			Contracts.Check(a.Length == b.Length, "Vectors must have the same dimensionality.");
			if (c == 0f || b.Count == 0)
			{
				a.CopyTo(ref dst);
				return;
			}
			if (a.Count == 0)
			{
				VectorUtils.ScaleInto(ref b, c, ref dst);
				return;
			}
			VBufferUtils.ApplyInto<float, float, float>(ref a, ref b, ref dst, (int ind, float v1, float v2) => v1 + c * v2);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00090148 File Offset: 0x0008E348
		public static void AddMultWithOffset(ref VBuffer<float> src, float c, ref VBuffer<float> dst, int offset)
		{
			Contracts.CheckParam(0 <= offset && offset <= dst.Length, "offset");
			Contracts.CheckParam(src.Length <= dst.Length - offset, "offset");
			if (src.Count == 0 || c == 0f)
			{
				return;
			}
			if (!dst.IsDense)
			{
				int num = ((dst.Count == 0) ? 0 : Utils.FindIndexSorted(dst.Indices, 0, dst.Count, offset));
				int num2 = ((dst.Count == 0) ? 0 : Utils.FindIndexSorted(dst.Indices, num, dst.Count, offset + src.Length));
				int num3;
				if (src.IsDense)
				{
					num3 = src.Length - (num2 - num);
				}
				else
				{
					num3 = src.Count;
					int num4 = 0;
					int num5 = num;
					while (num4 < src.Count && num5 < num2)
					{
						int num6 = src.Indices[num4] - dst.Indices[num5] + offset;
						if (num6 < 0)
						{
							num4++;
						}
						else if (num6 > 0)
						{
							num5++;
						}
						else
						{
							num4++;
							num5++;
							num3--;
						}
					}
				}
				int[] indices = dst.Indices;
				float[] values = dst.Values;
				if (num3 > 0)
				{
					Utils.EnsureSize<int>(ref indices, dst.Count + num3, dst.Length, true);
					Utils.EnsureSize<float>(ref values, dst.Count + num3, dst.Length, true);
					if (dst.Count != num2)
					{
						Array.Copy(indices, num2, indices, num2 + num3, dst.Count - num2);
						Array.Copy(values, num2, values, num2 + num3, dst.Count - num2);
					}
				}
				if (src.IsDense)
				{
					int num7 = num2 - 1;
					int num8 = src.Length - 1;
					int num9 = num2 + num3;
					while (--num9 >= num)
					{
						if (num7 >= 0 && offset + num8 == dst.Indices[num7])
						{
							values[num9] = dst.Values[num7--] + c * src.Values[num8];
						}
						else
						{
							values[num9] = c * src.Values[num8];
						}
						indices[num9] = offset + num8;
						num8--;
					}
				}
				else
				{
					int num10 = num2 - 1;
					int num11 = src.Count - 1;
					int num12 = ((num11 < 0) ? (-1) : src.Indices[num11]);
					int num13 = ((num10 < 0) ? (-1) : (dst.Indices[num10] - offset));
					int num14 = num2 + num3;
					while (--num14 >= num)
					{
						int num15 = num12 - num13;
						if (num15 == 0)
						{
							indices[num14] = dst.Indices[num10];
							values[num14] = dst.Values[num10--] + c * src.Values[num11--];
							num12 = ((num11 < 0) ? (-1) : src.Indices[num11]);
							num13 = ((num10 < 0) ? (-1) : (dst.Indices[num10] - offset));
						}
						else if (num15 < 0)
						{
							indices[num14] = dst.Indices[num10];
							values[num14] = dst.Values[num10--];
							num13 = ((num10 < 0) ? (-1) : (dst.Indices[num10] - offset));
						}
						else
						{
							indices[num14] = num12 + offset;
							values[num14] = c * src.Values[num11--];
							num12 = ((num11 < 0) ? (-1) : src.Indices[num11]);
						}
					}
				}
				dst = new VBuffer<float>(dst.Length, dst.Count + num3, values, indices);
				return;
			}
			if (src.IsDense)
			{
				SseUtils.AddScale(c, src.Values, dst.Values, offset, src.Count);
				return;
			}
			SseUtils.AddScale(c, src.Values, src.Indices, dst.Values, offset, src.Count);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000904E8 File Offset: 0x0008E6E8
		public static void ScaleInto(ref VBuffer<float> src, float c, ref VBuffer<float> dst)
		{
			if (c == 1f)
			{
				src.CopyTo(ref dst);
				return;
			}
			if (src.Count == 0 || c == 0f)
			{
				if (src.Length > 0 && src.IsDense)
				{
					float[] values = dst.Values;
					Utils.EnsureSize<float>(ref values, src.Length, src.Length, false);
					if (values == dst.Values)
					{
						Array.Clear(values, 0, src.Length);
					}
					dst = new VBuffer<float>(src.Length, values, dst.Indices);
					return;
				}
				dst = new VBuffer<float>(src.Length, 0, dst.Values, dst.Indices);
				return;
			}
			else
			{
				if (c == -1f)
				{
					VBufferUtils.ApplyIntoEitherDefined<float, float>(ref src, ref dst, (int i, float v) => -v);
					return;
				}
				VBufferUtils.ApplyIntoEitherDefined<float, float>(ref src, ref dst, (int i, float v) => c * v);
				return;
			}
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000905EC File Offset: 0x0008E7EC
		public static int ArgMax(ref VBuffer<float> src)
		{
			if (src.Length == 0)
			{
				return -1;
			}
			if (src.Count == 0)
			{
				return 0;
			}
			int num = MathUtils.ArgMax(src.Values, src.Count);
			if (src.IsDense)
			{
				return num;
			}
			if (num >= 0)
			{
				if (src.Values[num] > 0f)
				{
					return src.Indices[num];
				}
				if (src.Values[num] == 0f && src.Indices[num] == num)
				{
					return num;
				}
			}
			num = 0;
			while (num < src.Count && src.Indices[num] == num)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00090680 File Offset: 0x0008E880
		public static int ArgMin(ref VBuffer<float> src)
		{
			if (src.Length == 0)
			{
				return -1;
			}
			if (src.Count == 0)
			{
				return 0;
			}
			int num = MathUtils.ArgMin(src.Values, src.Count);
			if (src.IsDense)
			{
				return num;
			}
			if (num >= 0)
			{
				if (src.Values[num] < 0f)
				{
					return src.Indices[num];
				}
				if (src.Values[num] == 0f && src.Indices[num] == num)
				{
					return num;
				}
			}
			num = 0;
			while (num < src.Count && src.Indices[num] == num)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00090712 File Offset: 0x0008E912
		public static float DotProduct(float[] a, float[] b)
		{
			Contracts.Check(Utils.Size<float>(a) == Utils.Size<float>(b), "Arrays must have the same length");
			Contracts.Check(Utils.Size<float>(a) > 0);
			return SseUtils.DotProductDense(a, b, a.Length);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00090744 File Offset: 0x0008E944
		public static float DotProduct(float[] a, ref VBuffer<float> b)
		{
			Contracts.Check(Utils.Size<float>(a) == b.Length, "Vectors must have the same dimensionality.");
			if (b.Count == 0)
			{
				return 0f;
			}
			if (b.IsDense)
			{
				return SseUtils.DotProductDense(a, b.Values, b.Length);
			}
			return SseUtils.DotProductSparse(a, b.Values, b.Indices, b.Count);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000907AC File Offset: 0x0008E9AC
		public static float DotProduct(ref VBuffer<float> a, ref VBuffer<float> b)
		{
			Contracts.Check(a.Length == b.Length, "Vectors must have the same dimensionality.");
			if (a.Count == 0 || b.Count == 0)
			{
				return 0f;
			}
			if (a.IsDense)
			{
				if (b.IsDense)
				{
					return SseUtils.DotProductDense(a.Values, b.Values, a.Length);
				}
				return SseUtils.DotProductSparse(a.Values, b.Values, b.Indices, b.Count);
			}
			else
			{
				if (b.IsDense)
				{
					return SseUtils.DotProductSparse(b.Values, a.Values, a.Indices, a.Count);
				}
				return VectorUtils.DotProductSparse(a.Values, a.Indices, 0, a.Count, b.Values, b.Indices, 0, b.Count, 0);
			}
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00090880 File Offset: 0x0008EA80
		private static float L2DistSquaredSparse(float[] valuesA, int[] indicesA, int countA, float[] valuesB, int[] indicesB, int countB, int length)
		{
			float num = 0f;
			int i = 0;
			int j = 0;
			while (i < countA)
			{
				if (j >= countB)
				{
					break;
				}
				int num2 = indicesA[i] - indicesB[j];
				float num3;
				if (num2 == 0)
				{
					num3 = valuesA[i] - valuesB[j];
					i++;
					j++;
				}
				else if (num2 < 0)
				{
					num3 = valuesA[i];
					i++;
				}
				else
				{
					num3 = valuesB[j];
					j++;
				}
				num += num3 * num3;
			}
			while (i < countA)
			{
				float num4 = valuesA[i];
				num += num4 * num4;
				i++;
			}
			while (j < countB)
			{
				float num5 = valuesB[j];
				num += num5 * num5;
				j++;
			}
			return num;
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00090914 File Offset: 0x0008EB14
		private static float L2DistSquaredHalfSparse(float[] valuesA, int lengthA, float[] valuesB, int[] indicesB, int countB)
		{
			float num = SseUtils.SumSq(valuesA, 0, lengthA);
			if (countB == 0)
			{
				return num;
			}
			float num2 = SseUtils.SumSq(valuesB, 0, countB);
			float num3 = SseUtils.DotProductSparse(valuesA, valuesB, indicesB, countB);
			float num4 = num + num2 - 2f * num3;
			if (num4 >= 0f)
			{
				return num4;
			}
			return 0f;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00090960 File Offset: 0x0008EB60
		private static float L2DiffSquaredDense(float[] valuesA, float[] valuesB, int length)
		{
			if (length == 0)
			{
				return 0f;
			}
			return SseUtils.L2DistSquared(valuesA, valuesB, length);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00090974 File Offset: 0x0008EB74
		public static float DotProductWithOffset(ref VBuffer<float> a, int offset, ref VBuffer<float> b)
		{
			Contracts.Check(0 <= offset && offset <= a.Length);
			Contracts.Check(b.Length <= a.Length - offset, "VBuffer b must be no longer than a.Length - offset.");
			if (a.Count == 0 || b.Count == 0)
			{
				return 0f;
			}
			if (a.IsDense)
			{
				if (b.IsDense)
				{
					return SseUtils.DotProductDense(a.Values, offset, b.Values, b.Length);
				}
				return SseUtils.DotProductSparse(a.Values, offset, b.Values, b.Indices, b.Count);
			}
			else
			{
				float num = 0f;
				int num2 = Utils.FindIndexSorted(a.Indices, 0, a.Count, offset);
				int num3 = Utils.FindIndexSorted(a.Indices, 0, a.Count, offset + b.Length);
				if (b.IsDense)
				{
					for (int i = num2; i < num3; i++)
					{
						num += a.Values[i] * b.Values[a.Indices[i] - offset];
					}
					return num;
				}
				int num4 = num2;
				int num5 = 0;
				while (num4 < num3 && num5 < b.Count)
				{
					int num6 = a.Indices[num4];
					int num7 = b.Indices[num5];
					int num8 = num6 - offset - num7;
					if (num8 == 0)
					{
						num += a.Values[num4++] * b.Values[num5++];
					}
					else if (num8 < 0)
					{
						num4++;
					}
					else
					{
						num5++;
					}
				}
				return num;
			}
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00090AEC File Offset: 0x0008ECEC
		public static float DotProductWithOffset(float[] a, int offset, ref VBuffer<float> b)
		{
			Contracts.Check(0 <= offset && offset <= a.Length);
			Contracts.Check(b.Length <= a.Length - offset, "VBuffer b must be no longer than a.Length - offset.");
			if (b.Count == 0)
			{
				return 0f;
			}
			if (b.IsDense)
			{
				return SseUtils.DotProductDense(a, offset, b.Values, b.Length);
			}
			return SseUtils.DotProductSparse(a, offset, b.Values, b.Indices, b.Count);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00090B6C File Offset: 0x0008ED6C
		private static float DotProductSparse(float[] aValues, int[] aIndices, int ia, int iaLim, float[] bValues, int[] bIndices, int ib, int ibLim, int offset)
		{
			float num = 0f;
			for (;;)
			{
				int num2 = aIndices[ia] - offset - bIndices[ib];
				if (num2 == 0)
				{
					num += aValues[ia] * bValues[ib];
					if (++ia >= iaLim)
					{
						break;
					}
					if (++ib >= ibLim)
					{
						break;
					}
				}
				else if (num2 < 0)
				{
					ia++;
					if (num2 < -20)
					{
						ia = Utils.FindIndexSorted(aIndices, ia, iaLim, bIndices[ib] + offset);
					}
					if (ia >= iaLim)
					{
						break;
					}
				}
				else
				{
					ib++;
					if (num2 > 20)
					{
						ib = Utils.FindIndexSorted(bIndices, ib, ibLim, aIndices[ia] - offset);
					}
					if (ib >= ibLim)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00090C1C File Offset: 0x0008EE1C
		public static float L1Distance(ref VBuffer<float> a, ref VBuffer<float> b)
		{
			float res = 0f;
			VBufferUtils.ForEachEitherDefined<float>(ref a, ref b, delegate(int slot, float val1, float val2)
			{
				res += Math.Abs(val1 - val2);
			});
			return res;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00090C53 File Offset: 0x0008EE53
		public static float Distance(ref VBuffer<float> a, ref VBuffer<float> b)
		{
			return MathUtils.Sqrt(VectorUtils.L2DistSquared(ref a, ref b));
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00090C64 File Offset: 0x0008EE64
		public static float L2DistSquared(ref VBuffer<float> a, ref VBuffer<float> b)
		{
			Contracts.Check(a.Length == b.Length, "Vectors must have the same dimensionality.");
			if (a.IsDense)
			{
				if (b.IsDense)
				{
					return VectorUtils.L2DiffSquaredDense(a.Values, b.Values, b.Length);
				}
				return VectorUtils.L2DistSquaredHalfSparse(a.Values, a.Length, b.Values, b.Indices, b.Count);
			}
			else
			{
				if (b.IsDense)
				{
					return VectorUtils.L2DistSquaredHalfSparse(b.Values, b.Length, a.Values, a.Indices, a.Count);
				}
				return VectorUtils.L2DistSquaredSparse(a.Values, a.Indices, a.Count, b.Values, b.Indices, b.Count, a.Length);
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00090D30 File Offset: 0x0008EF30
		public static float L2DistSquared(float[] a, ref VBuffer<float> b)
		{
			Contracts.CheckValue<float[]>(a, "a");
			Contracts.Check(Utils.Size<float>(a) == b.Length, "Vectors must have the same dimensionality.");
			if (b.IsDense)
			{
				return VectorUtils.L2DiffSquaredDense(a, b.Values, b.Length);
			}
			return VectorUtils.L2DistSquaredHalfSparse(a, a.Length, b.Values, b.Indices, b.Count);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00090D98 File Offset: 0x0008EF98
		public static void AddMult(ref VBuffer<float> src, float[] dst, float c)
		{
			Contracts.CheckValue<float[]>(dst, "b");
			Contracts.Check(src.Length == dst.Length, "Arrays must have the same dimensionality.");
			if (src.Count == 0 || c == 0f)
			{
				return;
			}
			if (src.IsDense)
			{
				SseUtils.AddScale(c, src.Values, dst, src.Count);
				return;
			}
			for (int i = 0; i < src.Count; i++)
			{
				dst[src.Indices[i]] += c * src.Values[i];
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00090E28 File Offset: 0x0008F028
		public static void AddMultWithOffset(ref VBuffer<float> src, float[] dst, int offset, float c)
		{
			Contracts.CheckValue<float[]>(dst, "dst");
			Contracts.Check(0 <= offset && offset <= dst.Length);
			Contracts.Check(src.Length <= dst.Length - offset, "Vector src must be no longer than dst.Length - offset.");
			if (src.Count == 0 || c == 0f)
			{
				return;
			}
			if (src.IsDense)
			{
				for (int i = 0; i < src.Length; i++)
				{
					dst[i + offset] += c * src.Values[i];
				}
				return;
			}
			for (int j = 0; j < src.Count; j++)
			{
				dst[src.Indices[j] + offset] += c * src.Values[j];
			}
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00090EF0 File Offset: 0x0008F0F0
		public static void AddMult(float[] src, float[] dst, float c)
		{
			Contracts.Check(src.Length == dst.Length, "Arrays must have the same dimensionality.");
			if (c == 0f)
			{
				return;
			}
			SseUtils.AddScale(c, src, dst, src.Length);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00090F18 File Offset: 0x0008F118
		public static float Norm(float[] a)
		{
			return MathUtils.Sqrt(SseUtils.SumSq(a, a.Length));
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00090F28 File Offset: 0x0008F128
		public static void ScaleBy(float[] dst, float c)
		{
			if (c == 1f)
			{
				return;
			}
			if (c != 0f)
			{
				SseUtils.Scale(c, dst, dst.Length);
				return;
			}
			Array.Clear(dst, 0, dst.Length);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00090F50 File Offset: 0x0008F150
		public static float Distance(float[] a, float[] b)
		{
			Contracts.Check(a.Length == b.Length, "Arrays must have the same dimensionality.");
			float num = 0f;
			for (int i = 0; i < a.Length; i++)
			{
				float num2 = a[i] - b[i];
				num += num2 * num2;
			}
			return num;
		}
	}
}
