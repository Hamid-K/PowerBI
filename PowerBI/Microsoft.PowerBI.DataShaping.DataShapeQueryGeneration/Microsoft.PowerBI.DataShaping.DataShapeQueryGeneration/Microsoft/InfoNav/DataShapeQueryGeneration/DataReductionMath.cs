using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200001E RID: 30
	internal static class DataReductionMath
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00006004 File Offset: 0x00004204
		internal static int DivideAndCeiling(int dividend, int divisor)
		{
			return (int)Math.Ceiling((double)dividend / (double)divisor);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006011 File Offset: 0x00004211
		internal static int DivideAndThresholdRound(int dividend, int divisor)
		{
			return DataReductionMath.ThresholdRound((double)dividend / (double)divisor);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000601D File Offset: 0x0000421D
		internal static int ThresholdRound(double value)
		{
			if (value > 6.0)
			{
				return (int)Math.Ceiling(value);
			}
			return Math.Max((int)Math.Floor(value), 1);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006040 File Offset: 0x00004240
		internal static double NthRoot(int input, int n)
		{
			return Math.Pow((double)input, 1.0 / (double)n);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006058 File Offset: 0x00004258
		internal static void ApplyLinearScaling(IntermediateSimpleLimit primaryLimit, IntermediateSimpleLimit secondaryLimit, int targetIntersectionCount)
		{
			double num = (double)primaryLimit.Count.Value;
			double num2 = (double)secondaryLimit.Count.Value;
			double num3 = num / num2;
			double num4 = Math.Sqrt((double)targetIntersectionCount * num3);
			double num5 = (double)targetIntersectionCount / num4;
			primaryLimit.Count = new int?((int)num4);
			secondaryLimit.Count = new int?((int)num5);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000060B2 File Offset: 0x000042B2
		internal static int MultiplyOptional(int value1, int? value2)
		{
			if (value2 == null)
			{
				return value1;
			}
			return value1 * value2.Value;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000060C8 File Offset: 0x000042C8
		internal static int? MultiplyOptional(int? value1, int? value2)
		{
			if (value1 == null)
			{
				return value2;
			}
			if (value2 == null)
			{
				return value1;
			}
			return new int?(value1.Value * value2.Value);
		}
	}
}
