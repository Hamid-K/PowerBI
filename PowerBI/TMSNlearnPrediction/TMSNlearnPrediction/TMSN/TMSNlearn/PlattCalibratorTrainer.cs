using System;
using Microsoft.MachineLearning;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004CC RID: 1228
	public sealed class PlattCalibratorTrainer : CalibratorTrainerBase
	{
		// Token: 0x06001935 RID: 6453 RVA: 0x0008E527 File Offset: 0x0008C727
		public PlattCalibratorTrainer(IHostEnvironment env)
			: base(env, "PlattCalibration")
		{
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0008E538 File Offset: 0x0008C738
		public override ICalibrator CreateCalibrator(IChannel ch)
		{
			this._paramA = 0.0;
			this._paramB = 0.0;
			double num = 0.0;
			double num2 = 0.0;
			long num3 = 0L;
			foreach (CalibrationDataStore.DataItem dataItem in this._data)
			{
				float weight = dataItem.Weight;
				if (dataItem.Target)
				{
					num2 += (double)weight;
				}
				else
				{
					num += (double)weight;
				}
				num3 += 1L;
			}
			if (num3 == 0L)
			{
				return new PlattCalibrator(this._host, this._paramA, this._paramB);
			}
			this._paramA = 0.0;
			this._paramB = Math.Log((num + 1.0) / (num2 + 1.0));
			double num4 = (num2 + 1.0) / (num2 + 2.0);
			double num5 = 1.0 / (num + 2.0);
			double num6 = 0.001;
			double num7 = 8.988465674311579E+307;
			float[] array = new float[num3];
			float num8 = (float)((num2 + 1.0) / (num + num2 + 2.0));
			int num9 = 0;
			while ((long)num9 < num3)
			{
				array[num9] = num8;
				num9++;
			}
			int num10 = 0;
			int i = 0;
			while (i < 100)
			{
				double num11 = 0.0;
				double num12 = 0.0;
				double num13 = 0.0;
				double num14 = 0.0;
				double num15 = 0.0;
				int num16 = 0;
				foreach (CalibrationDataStore.DataItem dataItem2 in this._data)
				{
					float weight2 = dataItem2.Weight;
					float score = dataItem2.Score;
					double num17 = (dataItem2.Target ? num4 : num5);
					float num18 = array[num16];
					double num19 = (double)(num18 * (1f - num18) * weight2);
					double num20 = ((double)num18 - num17) * (double)weight2;
					num11 += (double)(score * score) * num19;
					num12 += num19;
					num13 += (double)score * num19;
					num14 += (double)score * num20;
					num15 += num20;
					num16++;
				}
				if (num14 <= -1E-09 || num14 >= 1E-09 || num15 <= -1E-09 || num15 >= 1E-09)
				{
					double num21 = 0.0;
					double paramA = this._paramA;
					double paramB = this._paramB;
					for (;;)
					{
						double num22 = (num11 + num6) * (num12 + num6) - num13 * num13;
						if (num22 == 0.0)
						{
							num6 *= 10.0;
						}
						else
						{
							this._paramA = paramA + ((num12 + num6) * num14 - num13 * num15) / num22;
							this._paramB = paramB + ((num11 + num6) * num15 - num13 * num14) / num22;
							num21 = 0.0;
							num16 = 0;
							foreach (CalibrationDataStore.DataItem dataItem3 in this._data)
							{
								float num23 = PlattCalibrator.PredictProbability(dataItem3.Score, this._paramA, this._paramB);
								double num24 = (dataItem3.Target ? num4 : num5);
								float weight3 = dataItem3.Weight;
								array[num16] = num23;
								double num25 = -200.0;
								double num26 = -200.0;
								if ((double)num23 > 0.0)
								{
									num25 = Math.Log((double)num23);
								}
								if ((double)num23 < 1.0)
								{
									num26 = Math.Log((double)(1f - num23));
								}
								num21 -= (num24 * num25 + (1.0 - num24) * num26) * (double)weight3;
								num16++;
							}
							if (num21 < num7 * 1.0000001)
							{
								goto Block_10;
							}
							num6 *= 10.0;
							if (num6 >= 1000000.0)
							{
								break;
							}
						}
					}
					IL_0434:
					double num27 = num21 - num7;
					double num28 = 0.5 * (num21 + num7 + 1.0);
					if (num27 > -0.001 * num28 && num27 < 1E-07 * num28)
					{
						num10++;
					}
					else
					{
						num10 = 0;
					}
					num7 = num21;
					if (num10 != 3)
					{
						i++;
						continue;
					}
					break;
					Block_10:
					num6 *= 0.1;
					goto IL_0434;
				}
				break;
			}
			return new PlattCalibrator(this._host, this._paramA, this._paramB);
		}

		// Token: 0x04000F0D RID: 3853
		internal const string UserName = "Sigmoid Calibration";

		// Token: 0x04000F0E RID: 3854
		internal const string LoadName = "PlattCalibration";

		// Token: 0x04000F0F RID: 3855
		internal const string Summary = "This model was introduced by Platt in the paper Probabilistic Outputs for Support Vector Machines and Comparisons to Regularized Likelihood Methods";

		// Token: 0x04000F10 RID: 3856
		private double _paramA;

		// Token: 0x04000F11 RID: 3857
		private double _paramB;
	}
}
