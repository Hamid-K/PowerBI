using System;

namespace Microsoft.MachineLearning.Training
{
	// Token: 0x02000487 RID: 1159
	public abstract class TrainerBase<TDataSet, TPredictor> : TrainerBase<TPredictor>, ITrainer<TDataSet, TPredictor>, ITrainer<TDataSet>, ITrainer where TPredictor : IPredictor
	{
		// Token: 0x0600182C RID: 6188 RVA: 0x0008A78C File Offset: 0x0008898C
		protected TrainerBase(IHost host, string name = null)
			: base(host, name)
		{
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0008A796 File Offset: 0x00088996
		protected TrainerBase(IHostEnvironment env, string name)
			: base(env, name)
		{
		}

		// Token: 0x0600182E RID: 6190
		public abstract void Train(TDataSet data);
	}
}
