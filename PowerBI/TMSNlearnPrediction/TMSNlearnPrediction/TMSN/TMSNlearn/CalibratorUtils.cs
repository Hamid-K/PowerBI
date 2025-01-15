using System;
using System.Reflection;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Training;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004C8 RID: 1224
	public static class CalibratorUtils
	{
		// Token: 0x0600191C RID: 6428 RVA: 0x0008DA80 File Offset: 0x0008BC80
		private static bool NeedCalibration(IChannel ch, SubComponent<ICalibratorTrainer, SignatureCalibrator> calibrator, ITrainer trainer, IPredictor predictor, RoleMappedSchema schema, out IValueMapper vm)
		{
			vm = predictor as IValueMapper;
			ITrainerEx trainerEx = trainer as ITrainerEx;
			if (trainerEx == null || !trainerEx.NeedCalibration)
			{
				ch.Info("Not training a calibrator because it is not needed.");
				return false;
			}
			if (!SubComponentExtensions.IsGood(calibrator))
			{
				ch.Info("Not training a calibrator because a valid calibrator trainer was not provided.");
				return false;
			}
			if (schema.Feature == null)
			{
				ch.Info("Not training a calibrator because there is no features column.");
				return false;
			}
			if (schema.Label == null)
			{
				ch.Info("Not training a calibrator because there is no label column.");
				return false;
			}
			if (!(predictor is IPredictorProducing<float>))
			{
				ch.Info("Not training a calibrator because the predictor does not implement IPredictorProducing<float>.");
				return false;
			}
			if (vm == null)
			{
				ch.Info("Not training a calibrator because the predictor does not implement IValueMapper.");
				return false;
			}
			if (vm.OutputType != NumberType.Float)
			{
				ch.Info("Not training a calibrator because the predictor output is {0}, but expected to be {1}.", new object[]
				{
					vm.OutputType,
					NumberType.R4
				});
				return false;
			}
			ColumnType inputType = vm.InputType;
			ColumnType type = schema.Feature.Type;
			if (!inputType.Equals(type))
			{
				VectorType vectorType = inputType as VectorType;
				VectorType vectorType2 = type as VectorType;
				if (vectorType == null || vectorType2 == null || !vectorType2.IsSubtypeOf(vectorType))
				{
					ch.Info("Not training a calibrator because the predictor input type is {0}, but expected to be {1}.", new object[] { inputType, type });
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0008DBBC File Offset: 0x0008BDBC
		public static IPredictor TrainCalibratorIfNeeded(IHostEnvironment env, IChannel ch, SubComponent<ICalibratorTrainer, SignatureCalibrator> calibrator, int maxRows, ITrainer trainer, IPredictor predictor, RoleMappedData data)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IChannel>(env, ch, "ch");
			Contracts.CheckValue<ITrainer>(ch, trainer, "trainer");
			Contracts.CheckValue<IPredictor>(ch, predictor, "predictor");
			Contracts.CheckValue<RoleMappedData>(ch, data, "data");
			IValueMapper valueMapper;
			if (!CalibratorUtils.NeedCalibration(ch, calibrator, trainer, predictor, data.Schema, out valueMapper))
			{
				return predictor;
			}
			IPredictorProducing<float> predictorProducing;
			if (!CalibratorUtils.TryTrainCalibratorCore(env, ch, calibrator, maxRows, valueMapper, data, out predictorProducing))
			{
				return predictor;
			}
			return predictorProducing;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0008DC34 File Offset: 0x0008BE34
		public static IPredictor TrainCalibrator(IHostEnvironment env, IChannel ch, SubComponent<ICalibratorTrainer, SignatureCalibrator> calibrator, int maxRows, IPredictor predictor, RoleMappedData data)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IChannel>(env, ch, "ch");
			Contracts.CheckValue<IPredictor>(ch, predictor, "predictor");
			Contracts.CheckValue<RoleMappedData>(ch, data, "data");
			Contracts.CheckParam(ch, data.Schema.Feature != null, "data", "data must have a Feature column");
			Contracts.CheckParam(ch, data.Schema.Label != null, "data", "data must have a Label column of type Float");
			IValueMapper valueMapper = predictor as IValueMapper;
			if (valueMapper == null || valueMapper.OutputType != NumberType.Float)
			{
				throw Contracts.Except(ch, "Predictor does not implement IValueMapper with Float output type");
			}
			ColumnType inputType = valueMapper.InputType;
			ColumnType type = data.Schema.Feature.Type;
			if (!inputType.Equals(type))
			{
				VectorType vectorType = inputType as VectorType;
				VectorType vectorType2 = type as VectorType;
				if (vectorType == null || vectorType2 == null || !vectorType2.IsSubtypeOf(vectorType))
				{
					throw Contracts.Except(ch, "The data's Feature column type and predictor's input type do not match: {0} vs {1}", new object[] { type, inputType });
				}
			}
			IPredictorProducing<float> predictorProducing;
			if (!CalibratorUtils.TryTrainCalibratorCore(env, ch, calibrator, maxRows, valueMapper, data, out predictorProducing))
			{
				throw Contracts.ExceptParam(ch, "data", "The Label column has an incompatible type: {0}", new object[] { data.Schema.Label.Type });
			}
			return predictorProducing;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0008DD80 File Offset: 0x0008BF80
		private static bool TryTrainCalibratorCore(IHostEnvironment env, IChannel ch, SubComponent<ICalibratorTrainer, SignatureCalibrator> calibrator, int maxRows, IValueMapper predictor, RoleMappedData data, out IPredictorProducing<float> res)
		{
			ICalibratorTrainer calibratorTrainer = ComponentCatalog.CreateInstance<ICalibratorTrainer, SignatureCalibrator>(calibrator, new object[] { env });
			if (calibratorTrainer.NeedsTraining)
			{
				bool flag;
				using (IChannel channel = ch.Start("Calibrator training"))
				{
					channel.Info("Training calibrator.");
					Func<ICalibratorTrainer, int, IValueMapper, RoleMappedData, bool> func = new Func<ICalibratorTrainer, int, IValueMapper, RoleMappedData, bool>(CalibratorUtils.TryTrainCalibratorCore<int>);
					MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { predictor.InputType.RawType });
					flag = (bool)methodInfo.Invoke(null, new object[] { calibratorTrainer, maxRows, predictor, data });
					channel.Done();
				}
				if (!flag)
				{
					res = null;
					return false;
				}
			}
			ICalibrator calibrator2 = calibratorTrainer.FinishTraining(ch);
			res = CalibratorUtils.CreateCalibratedPredictor(env, (IPredictorProducing<float>)predictor, calibrator2);
			return true;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0008DE78 File Offset: 0x0008C078
		private static bool TryTrainCalibratorCore<TInput>(ICalibratorTrainer caliTrainer, int maxRows, IValueMapper predictor, RoleMappedData data)
		{
			ValueMapper<TInput, float> mapper = predictor.GetMapper<TInput, float>();
			int index = data.Schema.Feature.Index;
			using (FloatLabelCursor floatLabelCursor = new FloatLabelCursor(data, CursOpt.Weight | CursOpt.Label, null, new int[] { index }))
			{
				ValueGetter<TInput> getter = floatLabelCursor.Row.GetGetter<TInput>(index);
				TInput tinput = default(TInput);
				float num = 0f;
				int num2 = 0;
				while (floatLabelCursor.MoveNext())
				{
					getter.Invoke(ref tinput);
					mapper.Invoke(ref tinput, ref num);
					if (FloatUtils.IsFinite(num) && (!caliTrainer.ProcessTrainingExample(num, floatLabelCursor.Label > 0f, floatLabelCursor.Weight) || (maxRows > 0 && ++num2 >= maxRows)))
					{
						break;
					}
				}
			}
			return true;
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0008DF44 File Offset: 0x0008C144
		private static IPredictorProducing<float> CreateCalibratedPredictor(IHostEnvironment env, IPredictorProducing<float> predictor, ICalibrator cali)
		{
			if (cali == null)
			{
				return predictor;
			}
			for (;;)
			{
				CalibratedPredictorBase calibratedPredictorBase = predictor as CalibratedPredictorBase;
				if (calibratedPredictorBase == null)
				{
					break;
				}
				predictor = calibratedPredictorBase.SubPredictor;
			}
			IPredictorWithFeatureWeights<float> predictorWithFeatureWeights = predictor as IPredictorWithFeatureWeights<float>;
			if (predictorWithFeatureWeights != null && predictor is IParameterMixer<float> && cali is IParameterMixer)
			{
				return new ParameterMixingCalibratedPredictor(env, predictorWithFeatureWeights, cali);
			}
			return new CalibratedPredictor(env, predictor, cali);
		}
	}
}
