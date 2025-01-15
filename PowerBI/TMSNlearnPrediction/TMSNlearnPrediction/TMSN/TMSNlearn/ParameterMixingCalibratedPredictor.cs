using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Model;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004C7 RID: 1223
	public sealed class ParameterMixingCalibratedPredictor : CalibratedPredictorBase, IParameterMixer<float>, IPredictorWithFeatureWeights<float>, IHaveFeatureWeights, IPredictorProducing<float>, IPredictor, ICanSaveModel
	{
		// Token: 0x06001913 RID: 6419 RVA: 0x0008D844 File Offset: 0x0008BA44
		public ParameterMixingCalibratedPredictor(IHostEnvironment env, IPredictorWithFeatureWeights<float> predictor, ICalibrator calibrator)
			: base(env, "ParameterMixingCalibratedPredictor", predictor, calibrator)
		{
			Contracts.Check(this._host, predictor is IParameterMixer<float>, "Predictor does not implement IParameterMixer");
			Contracts.Check(this._host, calibrator is IParameterMixer, "Calibrator does not implement IParameterMixer");
			this._featureWeights = predictor;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0008D898 File Offset: 0x0008BA98
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("PMIXCALP", 65537U, 65537U, 65537U, "PMixCaliPredExec", null);
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0008D8BC File Offset: 0x0008BABC
		private ParameterMixingCalibratedPredictor(ModelLoadContext ctx, IHostEnvironment env)
			: base(env, "ParameterMixingCalibratedPredictor", CalibratedPredictorBase.GetPredictor(ctx, env), CalibratedPredictorBase.GetCalibrator(ctx, env))
		{
			Contracts.CheckDecode(this._host, this._predictor is IParameterMixer<float>, "Predictor does not implement IParameterMixer");
			Contracts.CheckDecode(this._host, this._predictor is IPredictorWithFeatureWeights<float>, "Predictor does not implement IReturnsFeatureScores");
			Contracts.CheckDecode(this._host, this._calibrator is IParameterMixer, "Calibrator does not implement IParameterMixer");
			this._featureWeights = (IPredictorWithFeatureWeights<float>)this._predictor;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0008D94E File Offset: 0x0008BB4E
		public static ParameterMixingCalibratedPredictor Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(ParameterMixingCalibratedPredictor.GetVersionInfo());
			return new ParameterMixingCalibratedPredictor(ctx, env);
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0008D979 File Offset: 0x0008BB79
		public void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(ParameterMixingCalibratedPredictor.GetVersionInfo());
			base.SaveCore(ctx);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0008D9A4 File Offset: 0x0008BBA4
		public void GetFeatureWeights(ref VBuffer<float> weights)
		{
			this._featureWeights.GetFeatureWeights(ref weights);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0008D9F4 File Offset: 0x0008BBF4
		public IParameterMixer<float> CombineParameters(IList<IParameterMixer<float>> models)
		{
			IParameterMixer<float>[] array = models.Select(delegate(IParameterMixer<float> m)
			{
				ParameterMixingCalibratedPredictor parameterMixingCalibratedPredictor = m as ParameterMixingCalibratedPredictor;
				return (IParameterMixer<float>)parameterMixingCalibratedPredictor.SubPredictor;
			}).ToArray<IParameterMixer<float>>();
			IParameterMixer[] array2 = models.Select(delegate(IParameterMixer<float> m)
			{
				ParameterMixingCalibratedPredictor parameterMixingCalibratedPredictor2 = m as ParameterMixingCalibratedPredictor;
				return (IParameterMixer)parameterMixingCalibratedPredictor2.Calibrator;
			}).ToArray<IParameterMixer>();
			IParameterMixer<float> parameterMixer = array[0].CombineParameters(array);
			IParameterMixer parameterMixer2 = array2[0].CombineParameters(array2);
			return new ParameterMixingCalibratedPredictor(this._host, (IPredictorWithFeatureWeights<float>)parameterMixer, (ICalibrator)parameterMixer2);
		}

		// Token: 0x04000EF2 RID: 3826
		public const string LoaderSignature = "PMixCaliPredExec";

		// Token: 0x04000EF3 RID: 3827
		public const string RegistrationName = "ParameterMixingCalibratedPredictor";

		// Token: 0x04000EF4 RID: 3828
		private readonly IPredictorWithFeatureWeights<float> _featureWeights;
	}
}
