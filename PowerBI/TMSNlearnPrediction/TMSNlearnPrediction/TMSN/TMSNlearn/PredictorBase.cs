using System;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Model;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x02000481 RID: 1153
	public abstract class PredictorBase<TOutput> : IPredictorProducing<TOutput>, IPredictor
	{
		// Token: 0x06001810 RID: 6160 RVA: 0x00089F43 File Offset: 0x00088143
		protected PredictorBase(IHostEnvironment env, string name)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckNonWhiteSpace(env, name, "name");
			this._host = env.Register(name);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00089F70 File Offset: 0x00088170
		protected PredictorBase(IHostEnvironment env, string name, ModelLoadContext ctx)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckNonWhiteSpace(env, name, "name");
			this._host = env.Register(name);
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4, "This file was saved by an incompatible version");
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00089FC8 File Offset: 0x000881C8
		public virtual void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			this.SaveCore(ctx);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00089FE8 File Offset: 0x000881E8
		protected virtual void SaveCore(ModelSaveContext ctx)
		{
			ctx.Writer.Write(4);
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001814 RID: 6164
		public abstract PredictionKind PredictionKind { get; }

		// Token: 0x06001815 RID: 6165 RVA: 0x00089FF8 File Offset: 0x000881F8
		public static bool WarnOnOldNormalizer(ModelLoadContext ctx, Type typePredictor, IChannelProvider provider)
		{
			Contracts.CheckValue<IChannelProvider>(provider, "provider");
			Contracts.CheckValue<ModelLoadContext>(provider, ctx, "ctx");
			Contracts.CheckValue<Type>(provider, typePredictor, "typePredictor");
			if (!ctx.ContainsModel("Normalizer"))
			{
				return false;
			}
			using (IChannel channel = provider.Start("WarnNormalizer"))
			{
				channel.Warning("Ignoring integrated normalizer while loading a predictor of type {0}.{1}   Please contact tlcsupp for assistance with converting legacy models.", new object[]
				{
					typePredictor,
					Environment.NewLine
				});
				channel.Done();
			}
			return true;
		}

		// Token: 0x04000E7D RID: 3709
		public const string NormalizerWarningFormat = "Ignoring integrated normalizer while loading a predictor of type {0}.{1}   Please contact tlcsupp for assistance with converting legacy models.";

		// Token: 0x04000E7E RID: 3710
		protected readonly IHost _host;
	}
}
