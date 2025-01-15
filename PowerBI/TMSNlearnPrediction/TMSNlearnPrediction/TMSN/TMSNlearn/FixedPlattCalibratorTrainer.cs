using System;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004CD RID: 1229
	public sealed class FixedPlattCalibratorTrainer : ICalibratorTrainer
	{
		// Token: 0x06001937 RID: 6455 RVA: 0x0008EA20 File Offset: 0x0008CC20
		public FixedPlattCalibratorTrainer(FixedPlattCalibratorTrainer.Arguments args, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("FixedPlattCalibration");
			this._slope = args.slope;
			this._offset = args.offset;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0008EA5C File Offset: 0x0008CC5C
		public bool NeedsTraining
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0008EA5F File Offset: 0x0008CC5F
		public bool ProcessTrainingExample(float output, bool labelIs1, float weight)
		{
			return false;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0008EA62 File Offset: 0x0008CC62
		public ICalibrator FinishTraining(IChannel ch)
		{
			return new PlattCalibrator(this._host, this._slope, this._offset);
		}

		// Token: 0x04000F12 RID: 3858
		internal const string UserName = "Fixed Sigmoid Calibration";

		// Token: 0x04000F13 RID: 3859
		internal const string LoadName = "FixedPlattCalibration";

		// Token: 0x04000F14 RID: 3860
		internal const string Summary = "Sigmoid calibrator with configurable slope and offset.";

		// Token: 0x04000F15 RID: 3861
		private readonly IHost _host;

		// Token: 0x04000F16 RID: 3862
		private readonly double _slope;

		// Token: 0x04000F17 RID: 3863
		private readonly double _offset;

		// Token: 0x020004CE RID: 1230
		public sealed class Arguments
		{
			// Token: 0x04000F18 RID: 3864
			[Argument(4, HelpText = "The slope parameter of f(x) = 1 / (1 + exp(-slope * x + offset)", ShortName = "a")]
			public double slope = 1.0;

			// Token: 0x04000F19 RID: 3865
			[Argument(4, HelpText = "The offset parameter of f(x) = 1 / (1 + exp(-slope * x + offset)", ShortName = "b")]
			public double offset;
		}
	}
}
