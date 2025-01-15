using System;
using Microsoft.MachineLearning;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004CB RID: 1227
	public abstract class CalibratorTrainerBase : ICalibratorTrainer
	{
		// Token: 0x06001930 RID: 6448 RVA: 0x0008E4A2 File Offset: 0x0008C6A2
		protected CalibratorTrainerBase(IHostEnvironment env, string name)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckNonWhiteSpace(env, name, "name");
			this._host = env.Register(name);
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x0008E4DA File Offset: 0x0008C6DA
		public bool NeedsTraining
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0008E4DD File Offset: 0x0008C6DD
		public bool ProcessTrainingExample(float output, bool labelIs1, float weight)
		{
			if (this._data == null)
			{
				this._data = new CalibrationDataStore(this.maxNumSamples);
			}
			this._data.AddToStore(output, labelIs1, weight);
			return true;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0008E507 File Offset: 0x0008C707
		public ICalibrator FinishTraining(IChannel ch)
		{
			Contracts.Check(ch, this._data != null, "Calibrator trained on zero instances.");
			return this.CreateCalibrator(ch);
		}

		// Token: 0x06001934 RID: 6452
		public abstract ICalibrator CreateCalibrator(IChannel ch);

		// Token: 0x04000F09 RID: 3849
		protected const int DefaultMaxNumSamples = 1000000;

		// Token: 0x04000F0A RID: 3850
		protected readonly IHost _host;

		// Token: 0x04000F0B RID: 3851
		protected CalibrationDataStore _data;

		// Token: 0x04000F0C RID: 3852
		protected int maxNumSamples = 1000000;
	}
}
