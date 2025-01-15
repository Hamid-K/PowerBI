using System;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Model;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004C6 RID: 1222
	public sealed class CalibratedPredictor : CalibratedPredictorBase, ICanSaveModel
	{
		// Token: 0x0600190D RID: 6413 RVA: 0x0008D763 File Offset: 0x0008B963
		public CalibratedPredictor(IHostEnvironment env, IPredictorProducing<float> predictor, ICalibrator calibrator)
			: base(env, "CalibratedPredictor", predictor, calibrator)
		{
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0008D773 File Offset: 0x0008B973
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("CALIPRED", 65537U, 65537U, 65537U, "CaliPredExec", null);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0008D794 File Offset: 0x0008B994
		private static VersionInfo GetVersionInfoBulk()
		{
			return new VersionInfo("BCALPRED", 65537U, 65537U, 65537U, "CaliPredExec", null);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0008D7B5 File Offset: 0x0008B9B5
		private CalibratedPredictor(ModelLoadContext ctx, IHostEnvironment env)
			: base(env, "CalibratedPredictor", CalibratedPredictorBase.GetPredictor(ctx, env), CalibratedPredictorBase.GetCalibrator(ctx, env))
		{
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0008D7D4 File Offset: 0x0008B9D4
		public static CalibratedPredictor Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<ModelLoadContext>(ctx, "ctx");
			VersionInfo versionInfo = CalibratedPredictor.GetVersionInfo();
			VersionInfo versionInfoBulk = CalibratedPredictor.GetVersionInfoBulk();
			VersionInfo versionInfo2 = ((ctx.Header.ModelSignature == versionInfoBulk.ModelSignature) ? versionInfoBulk : versionInfo);
			ctx.CheckAtModel(versionInfo2);
			return new CalibratedPredictor(ctx, env);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0008D81F File Offset: 0x0008BA1F
		public void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(CalibratedPredictor.GetVersionInfo());
			base.SaveCore(ctx);
		}

		// Token: 0x04000EF0 RID: 3824
		public const string LoaderSignature = "CaliPredExec";

		// Token: 0x04000EF1 RID: 3825
		public const string RegistrationName = "CalibratedPredictor";
	}
}
