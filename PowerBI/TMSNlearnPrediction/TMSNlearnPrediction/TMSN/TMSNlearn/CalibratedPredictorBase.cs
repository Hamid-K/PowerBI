using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Model;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004C5 RID: 1221
	public abstract class CalibratedPredictorBase : IDistPredictorProducing<float, float>, IPredictorProducing<float>, IPredictor, IValueMapperDist, IValueMapper, ICanSaveInIniFormat, ICanSaveInIniFormatOld, ICanSaveInTextFormat, ICanSaveInSourceCode, ICanSaveSummary, ICanGetSummaryInKeyValuePairs
	{
		// Token: 0x060018FB RID: 6395 RVA: 0x0008D424 File Offset: 0x0008B624
		protected CalibratedPredictorBase(IHostEnvironment env, string name, IPredictorProducing<float> predictor, ICalibrator calibrator)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckNonWhiteSpace(env, name, "name");
			this._host = env.Register(name);
			Contracts.CheckValue<IPredictorProducing<float>>(this._host, predictor, "predictor");
			Contracts.CheckValue<ICalibrator>(this._host, calibrator, "calibrator");
			this._mapper = predictor as IValueMapper;
			Contracts.Check(this._host, this._mapper != null, "The predictor does not implement IValueMapper");
			Contracts.Check(this._host, this._mapper.OutputType == NumberType.Float, "The output type of the predictor is expected to be Float");
			this._predictor = predictor;
			this._calibrator = calibrator;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0008D4D8 File Offset: 0x0008B6D8
		public IPredictorProducing<float> SubPredictor
		{
			get
			{
				return this._predictor;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0008D4E0 File Offset: 0x0008B6E0
		public ICalibrator Calibrator
		{
			get
			{
				return this._calibrator;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x0008D4E8 File Offset: 0x0008B6E8
		public PredictionKind PredictionKind
		{
			get
			{
				return this._predictor.PredictionKind;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0008D4F5 File Offset: 0x0008B6F5
		public ColumnType InputType
		{
			get
			{
				return this._mapper.InputType;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0008D502 File Offset: 0x0008B702
		public ColumnType OutputType
		{
			get
			{
				return this._mapper.OutputType;
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0008D50F File Offset: 0x0008B70F
		public ValueMapper<TIn, TOut> GetMapper<TIn, TOut>()
		{
			return this._mapper.GetMapper<TIn, TOut>();
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0008D51C File Offset: 0x0008B71C
		public ColumnType DistType
		{
			get
			{
				return NumberType.Float;
			}
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0008D550 File Offset: 0x0008B750
		public ValueMapper<TIn, TOut, TDist> GetMapper<TIn, TOut, TDist>()
		{
			Contracts.Check(this._host, typeof(TOut) == typeof(float));
			Contracts.Check(this._host, typeof(TDist) == typeof(float));
			ValueMapper<TIn, float> map = this.GetMapper<TIn, float>();
			ValueMapper<TIn, float, float> valueMapper = delegate(ref TIn src, ref float score, ref float prob)
			{
				map.Invoke(ref src, ref score);
				prob = this._calibrator.PredictProbability(score);
			};
			return (ValueMapper<TIn, TOut, TDist>)valueMapper;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0008D5D4 File Offset: 0x0008B7D4
		public void SaveAsIni(TextWriter writer, RoleMappedSchema schema, ICalibrator calibrator = null)
		{
			Contracts.Check(this._host, calibrator == null, "Too many calibrators.");
			ICanSaveInIniFormat canSaveInIniFormat = this._predictor as ICanSaveInIniFormat;
			if (canSaveInIniFormat != null)
			{
				canSaveInIniFormat.SaveAsIni(writer, schema, this._calibrator);
				return;
			}
			this.SaveAsIniOld(writer, FeatureNameCollection.Create(schema), calibrator);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0008D624 File Offset: 0x0008B824
		public void SaveAsIniOld(TextWriter writer, FeatureNameCollection names, ICalibrator calibrator = null)
		{
			Contracts.Check(calibrator == null, "Too many calibrators.");
			ICanSaveInIniFormatOld canSaveInIniFormatOld = this._predictor as ICanSaveInIniFormatOld;
			if (canSaveInIniFormatOld != null)
			{
				canSaveInIniFormatOld.SaveAsIniOld(writer, names, this._calibrator);
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0008D65C File Offset: 0x0008B85C
		public void SaveAsText(TextWriter writer, FeatureNameCollection names)
		{
			ICanSaveInTextFormat canSaveInTextFormat = this._predictor as ICanSaveInTextFormat;
			if (canSaveInTextFormat != null)
			{
				canSaveInTextFormat.SaveAsText(writer, names);
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0008D680 File Offset: 0x0008B880
		public void SaveAsCode(TextWriter writer, FeatureNameCollection names)
		{
			ICanSaveInSourceCode canSaveInSourceCode = this._predictor as ICanSaveInSourceCode;
			if (canSaveInSourceCode != null)
			{
				canSaveInSourceCode.SaveAsCode(writer, names);
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0008D6A4 File Offset: 0x0008B8A4
		public void SaveSummary(TextWriter writer, FeatureNameCollection names)
		{
			ICanSaveSummary canSaveSummary = this._predictor as ICanSaveSummary;
			if (canSaveSummary != null)
			{
				canSaveSummary.SaveSummary(writer, names);
			}
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0008D6C8 File Offset: 0x0008B8C8
		public KeyValuePair<string, object>[] GetSummaryInKeyValuePairs(FeatureNameCollection names)
		{
			ICanGetSummaryInKeyValuePairs canGetSummaryInKeyValuePairs = this._predictor as ICanGetSummaryInKeyValuePairs;
			if (canGetSummaryInKeyValuePairs != null)
			{
				return canGetSummaryInKeyValuePairs.GetSummaryInKeyValuePairs(names);
			}
			return null;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0008D6ED File Offset: 0x0008B8ED
		protected void SaveCore(ModelSaveContext ctx)
		{
			ctx.SaveModel<IPredictorProducing<float>>(this._predictor, "Predictor");
			ctx.SaveModel<ICalibrator>(this._calibrator, "Calibrator");
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0008D714 File Offset: 0x0008B914
		protected static IPredictorProducing<float> GetPredictor(ModelLoadContext ctx, IHostEnvironment env)
		{
			IPredictorProducing<float> predictorProducing;
			ctx.LoadModel<IPredictorProducing<float>, SignatureLoadModel>(out predictorProducing, "Predictor", new object[] { env });
			return predictorProducing;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0008D73C File Offset: 0x0008B93C
		protected static ICalibrator GetCalibrator(ModelLoadContext ctx, IHostEnvironment env)
		{
			ICalibrator calibrator;
			ctx.LoadModel<ICalibrator, SignatureLoadModel>(out calibrator, "Calibrator", new object[] { env });
			return calibrator;
		}

		// Token: 0x04000EEC RID: 3820
		protected readonly IHost _host;

		// Token: 0x04000EED RID: 3821
		protected readonly IPredictorProducing<float> _predictor;

		// Token: 0x04000EEE RID: 3822
		protected readonly IValueMapper _mapper;

		// Token: 0x04000EEF RID: 3823
		protected readonly ICalibrator _calibrator;
	}
}
