using System;
using System.Collections.Generic;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004C9 RID: 1225
	public sealed class NaiveCalibratorTrainer : ICalibratorTrainer
	{
		// Token: 0x06001922 RID: 6434 RVA: 0x0008DF98 File Offset: 0x0008C198
		public NaiveCalibratorTrainer(IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("NaiveCalibrator");
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x0008DFFE File Offset: 0x0008C1FE
		public bool NeedsTraining
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0008E001 File Offset: 0x0008C201
		public bool ProcessTrainingExample(float output, bool labelIs1, float weight)
		{
			if (labelIs1)
			{
				this.cMargins.Add(output);
			}
			else
			{
				this.ncMargins.Add(output);
			}
			return true;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0008E024 File Offset: 0x0008C224
		public ICalibrator FinishTraining(IChannel ch)
		{
			float[] array = this.cMargins.ToArray();
			Contracts.Check(ch, array.Length > 0, "Calibrator trained on zero instances.");
			float num = MathUtils.Min(array);
			float num2 = MathUtils.Max(array);
			float[] array2 = this.ncMargins.ToArray();
			float num3 = MathUtils.Min(array2);
			float num4 = MathUtils.Max(array2);
			this.min = ((num < num3) ? num : num3);
			this.max = ((num2 > num4) ? num2 : num4);
			this.binSize = (this.max - this.min) / (float)this.numBins;
			float[] array3 = new float[this.numBins];
			float[] array4 = new float[this.numBins];
			foreach (float num5 in array)
			{
				int binIdx = NaiveCalibrator.GetBinIdx(num5, this.min, this.binSize, this.numBins);
				array3[binIdx] += 1f;
			}
			foreach (float num6 in array2)
			{
				int binIdx2 = NaiveCalibrator.GetBinIdx(num6, this.min, this.binSize, this.numBins);
				array4[binIdx2] += 1f;
			}
			this.binProbs = new float[this.numBins];
			for (int k = 0; k < this.numBins; k++)
			{
				if (array3[k] + array4[k] == 0f)
				{
					this.binProbs[k] = 0f;
				}
				else
				{
					this.binProbs[k] = array3[k] / (array3[k] + array4[k]);
				}
			}
			return new NaiveCalibrator(this._host, this.min, this.binSize, this.binProbs);
		}

		// Token: 0x04000EF7 RID: 3831
		internal const string UserName = "Naive Calibrator";

		// Token: 0x04000EF8 RID: 3832
		internal const string LoadName = "NaiveCalibrator";

		// Token: 0x04000EF9 RID: 3833
		internal const string Summary = "Naïve calibrator divides the range of the outputs into equally sized bins. In each bin, the probability of belonging to class 1 is the number of class 1 instances in the bin, divided by the total number of instances in the bin.";

		// Token: 0x04000EFA RID: 3834
		private readonly IHost _host;

		// Token: 0x04000EFB RID: 3835
		private List<float> cMargins = new List<float>();

		// Token: 0x04000EFC RID: 3836
		private List<float> ncMargins = new List<float>();

		// Token: 0x04000EFD RID: 3837
		private int numBins = 200;

		// Token: 0x04000EFE RID: 3838
		private float binSize;

		// Token: 0x04000EFF RID: 3839
		private float min = float.MaxValue;

		// Token: 0x04000F00 RID: 3840
		private float max = float.MinValue;

		// Token: 0x04000F01 RID: 3841
		private float[] binProbs;
	}
}
