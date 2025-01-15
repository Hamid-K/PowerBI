using System;

namespace Microsoft.MachineLearning.Training
{
	// Token: 0x02000485 RID: 1157
	public abstract class TrainerBase : ITrainerEx, ITrainer, IHostedComponent
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x0008A6F8 File Offset: 0x000888F8
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06001820 RID: 6176
		public abstract PredictionKind PredictionKind { get; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06001821 RID: 6177
		public abstract bool NeedNormalization { get; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06001822 RID: 6178
		public abstract bool NeedCalibration { get; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06001823 RID: 6179
		public abstract bool WantCaching { get; }

		// Token: 0x06001824 RID: 6180 RVA: 0x0008A700 File Offset: 0x00088900
		protected TrainerBase(IHost host, string name = null)
		{
			Contracts.CheckValue<IHost>(host, "host");
			this._name = ((!string.IsNullOrEmpty(name)) ? name : "Trainer");
			this._host = host;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0008A730 File Offset: 0x00088930
		protected TrainerBase(IHostEnvironment env, string name)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckNonEmpty(name, "name");
			this._name = name;
			this._host = env.Register(this);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0008A763 File Offset: 0x00088963
		IPredictor ITrainer.CreatePredictor()
		{
			return this.CreatePredictorCore();
		}

		// Token: 0x06001827 RID: 6183
		protected abstract IPredictor CreatePredictorCore();

		// Token: 0x04000E88 RID: 3720
		public const string NoTrainingInstancesMessage = "No valid training instances found, all instances have missing features.";

		// Token: 0x04000E89 RID: 3721
		protected readonly string _name;

		// Token: 0x04000E8A RID: 3722
		protected readonly IHost _host;
	}
}
