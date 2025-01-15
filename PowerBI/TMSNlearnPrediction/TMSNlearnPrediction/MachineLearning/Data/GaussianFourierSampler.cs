using System;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000B4 RID: 180
	public class GaussianFourierSampler : IFourierDistributionSampler, ICanSaveModel
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00015B7F File Offset: 0x00013D7F
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("RND GAUS", 65537U, 65537U, 65537U, "RandGaussFourierExec", null);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00015BA0 File Offset: 0x00013DA0
		public GaussianFourierSampler(GaussianFourierSampler.Arguments args, IHostEnvironment env, float avgDist)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("GaussianRandom");
			Contracts.CheckValue<GaussianFourierSampler.Arguments>(this._host, args, "args");
			this._gamma = args.gamma / avgDist;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00015BEE File Offset: 0x00013DEE
		public static GaussianFourierSampler Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(GaussianFourierSampler.GetVersionInfo());
			return new GaussianFourierSampler(ctx, env);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00015C1C File Offset: 0x00013E1C
		private GaussianFourierSampler(ModelLoadContext ctx, IHostEnvironment env)
		{
			this._host = env.Register("GaussianRandom");
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4);
			this._gamma = Utils.ReadFloat(ctx.Reader);
			Contracts.CheckDecode(this._host, FloatUtils.IsFinite(this._gamma));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00015C82 File Offset: 0x00013E82
		public void Save(ModelSaveContext ctx)
		{
			ctx.SetVersionInfo(GaussianFourierSampler.GetVersionInfo());
			ctx.Writer.Write(4);
			ctx.Writer.Write(this._gamma);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00015CAC File Offset: 0x00013EAC
		public float Next(IRandom rand)
		{
			return (float)Stats.SampleFromGaussian(rand) * MathUtils.Sqrt(2f * this._gamma);
		}

		// Token: 0x04000189 RID: 393
		public const string LoaderSignature = "RandGaussFourierExec";

		// Token: 0x0400018A RID: 394
		public const string LoadName = "GaussianRandom";

		// Token: 0x0400018B RID: 395
		protected readonly IHost _host;

		// Token: 0x0400018C RID: 396
		private readonly float _gamma;

		// Token: 0x020000B5 RID: 181
		public class Arguments
		{
			// Token: 0x0400018D RID: 397
			[Argument(0, HelpText = "gamma in the kernel definition: exp(-gamma*||x-y||^2 / r^2). r is an estimate of the average intra-example distance", ShortName = "g")]
			public float gamma = 1f;
		}
	}
}
