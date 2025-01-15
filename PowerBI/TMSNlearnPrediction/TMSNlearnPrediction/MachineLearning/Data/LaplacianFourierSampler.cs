using System;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000B6 RID: 182
	public class LaplacianFourierSampler : IFourierDistributionSampler, ICanSaveModel
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x00015CDA File Offset: 0x00013EDA
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("RND LPLC", 65537U, 65537U, 65537U, "RandLaplacianFourierExec", null);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00015CFC File Offset: 0x00013EFC
		public LaplacianFourierSampler(LaplacianFourierSampler.Arguments args, IHostEnvironment env, float avgDist)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("LaplacianRandom");
			Contracts.CheckValue<LaplacianFourierSampler.Arguments>(this._host, args, "args");
			this._a = args.a / avgDist;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00015D4A File Offset: 0x00013F4A
		public static LaplacianFourierSampler Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(LaplacianFourierSampler.GetVersionInfo());
			return new LaplacianFourierSampler(ctx, env);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00015D78 File Offset: 0x00013F78
		private LaplacianFourierSampler(ModelLoadContext ctx, IHostEnvironment env)
		{
			this._host = env.Register("LaplacianRandom");
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4);
			this._a = Utils.ReadFloat(ctx.Reader);
			Contracts.CheckDecode(this._host, FloatUtils.IsFinite(this._a));
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00015DDE File Offset: 0x00013FDE
		public void Save(ModelSaveContext ctx)
		{
			ctx.SetVersionInfo(LaplacianFourierSampler.GetVersionInfo());
			ctx.Writer.Write(4);
			ctx.Writer.Write(this._a);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00015E08 File Offset: 0x00014008
		public float Next(IRandom rand)
		{
			return this._a * Stats.SampleFromCauchy(rand);
		}

		// Token: 0x0400018E RID: 398
		public const string LoaderSignature = "RandLaplacianFourierExec";

		// Token: 0x0400018F RID: 399
		public const string RegistrationName = "LaplacianRandom";

		// Token: 0x04000190 RID: 400
		protected readonly IHost _host;

		// Token: 0x04000191 RID: 401
		private readonly float _a;

		// Token: 0x020000B7 RID: 183
		public class Arguments
		{
			// Token: 0x04000192 RID: 402
			[Argument(0, HelpText = "a in the term exp(-a|x| / r). r is an estimate of the average intra-example L1 distance")]
			public float a = 1f;
		}
	}
}
