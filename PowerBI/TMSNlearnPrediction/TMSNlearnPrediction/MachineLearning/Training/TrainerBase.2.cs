using System;

namespace Microsoft.MachineLearning.Training
{
	// Token: 0x02000486 RID: 1158
	public abstract class TrainerBase<TPredictor> : TrainerBase where TPredictor : IPredictor
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x0008A76B File Offset: 0x0008896B
		protected TrainerBase(IHost host, string name = null)
			: base(host, name)
		{
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0008A775 File Offset: 0x00088975
		protected TrainerBase(IHostEnvironment env, string name)
			: base(env, name)
		{
		}

		// Token: 0x0600182A RID: 6186
		public abstract TPredictor CreatePredictor();

		// Token: 0x0600182B RID: 6187 RVA: 0x0008A77F File Offset: 0x0008897F
		protected sealed override IPredictor CreatePredictorCore()
		{
			return this.CreatePredictor();
		}
	}
}
