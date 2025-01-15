using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x02000007 RID: 7
	public sealed class ForecastStats
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003EE4 File Offset: 0x000020E4
		public double Alpha
		{
			get
			{
				return this.xnumAlpha;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003EEC File Offset: 0x000020EC
		public double Beta
		{
			get
			{
				return this.xnumBeta;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003EF4 File Offset: 0x000020F4
		public double Gamma
		{
			get
			{
				return this.xnumGamma;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00003EFC File Offset: 0x000020FC
		public double Loss
		{
			get
			{
				return this.loss;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003F04 File Offset: 0x00002104
		public double LikelihoodCorrection
		{
			get
			{
				return this.likelihoodCorrection;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00003F0C File Offset: 0x0000210C
		public IReadOnlyList<double> FinalState
		{
			get
			{
				return this.finalState;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003F14 File Offset: 0x00002114
		public double TrendDeviation
		{
			get
			{
				return this.trendDeviation;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003F1C File Offset: 0x0000211C
		internal uint LastElementIndex
		{
			get
			{
				return this.lastElementIndex;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003F24 File Offset: 0x00002124
		public bool UseBestLineFit
		{
			get
			{
				return this.useBestLineFit;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003F2C File Offset: 0x0000212C
		public int GetSeasonValue()
		{
			return (int)this.Season;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003F34 File Offset: 0x00002134
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00003F3C File Offset: 0x0000213C
		public bool BetterModelExistsButConfidenceBandTooWide { get; set; }

		// Token: 0x06000034 RID: 52 RVA: 0x00003F48 File Offset: 0x00002148
		public ForecastStats(bool errorIsMultiplicativeIn, bool hasTrendIn, bool seasonIsMultiplicativeIn, IReadOnlyList<double> initialStateIn, uint seasonIn, uint trainLengthIn, double defaultLossValue)
		{
			this.Clean();
			this.ErrorIsMultiplicative = errorIsMultiplicativeIn;
			this.HasTrend = hasTrendIn;
			this.SeasonIsMultiplicative = seasonIsMultiplicativeIn;
			this.InitialState = initialStateIn;
			this.Season = seasonIn;
			this.TrainLength = trainLengthIn;
			this.lossAfterAICCorrection = null;
			this.loss = defaultLossValue;
			this.useBestLineFit = false;
			this.BetterModelExistsButConfidenceBandTooWide = false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003FB0 File Offset: 0x000021B0
		public void Clean()
		{
			this.xnumAlpha = 0.0;
			this.xnumBeta = 0.0;
			this.xnumGamma = 0.0;
			this.loss = 0.0;
			this.likelihoodCorrection = 0.0;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004008 File Offset: 0x00002208
		public double GetAICCorrectedLoss()
		{
			if (this.lossAfterAICCorrection != null && this.lossAfterAICCorrection != null)
			{
				return this.lossAfterAICCorrection.Value;
			}
			int aicqValueForModel = this.GetAICqValueForModel();
			this.lossAfterAICCorrection = new double?(this.Loss * Math.Exp(2.0 * (double)aicqValueForModel / this.TrainLength));
			return this.lossAfterAICCorrection.Value;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004079 File Offset: 0x00002279
		public void ResetBeta(double value = 0.001)
		{
			this.xnumBeta = value;
			this.useBestLineFit = true;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004089 File Offset: 0x00002289
		public void UpdateParameters(double alpha, double beta, double gamma, double lossIn, double likelihoodCorrectionIn, IReadOnlyList<double> finalStateIn, double trendDeviationIn, uint lastElementIndexIn)
		{
			this.xnumAlpha = alpha;
			this.xnumBeta = beta;
			this.xnumGamma = gamma;
			this.loss = lossIn;
			this.likelihoodCorrection = likelihoodCorrectionIn;
			this.finalState = finalStateIn;
			this.trendDeviation = trendDeviationIn;
			this.lastElementIndex = lastElementIndexIn;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000040C8 File Offset: 0x000022C8
		public int GetAICqValueForModel()
		{
			int num = 2;
			if (this.HasTrend)
			{
				num += 2;
			}
			if (this.Season > 1U)
			{
				num++;
				num += (int)this.Season;
			}
			return num;
		}

		// Token: 0x0400003F RID: 63
		private double xnumAlpha;

		// Token: 0x04000040 RID: 64
		private double xnumBeta;

		// Token: 0x04000041 RID: 65
		private double xnumGamma;

		// Token: 0x04000042 RID: 66
		private double loss;

		// Token: 0x04000043 RID: 67
		private double? lossAfterAICCorrection;

		// Token: 0x04000044 RID: 68
		private double likelihoodCorrection;

		// Token: 0x04000045 RID: 69
		private IReadOnlyList<double> finalState;

		// Token: 0x04000046 RID: 70
		private double trendDeviation;

		// Token: 0x04000047 RID: 71
		private uint lastElementIndex;

		// Token: 0x04000048 RID: 72
		private bool useBestLineFit;

		// Token: 0x0400004A RID: 74
		public readonly bool ErrorIsMultiplicative;

		// Token: 0x0400004B RID: 75
		public readonly bool SeasonIsMultiplicative;

		// Token: 0x0400004C RID: 76
		public readonly bool HasTrend;

		// Token: 0x0400004D RID: 77
		public readonly IReadOnlyList<double> InitialState;

		// Token: 0x0400004E RID: 78
		internal readonly uint Season;

		// Token: 0x0400004F RID: 79
		internal readonly uint TrainLength;
	}
}
