using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.IO;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003E1 RID: 993
	public static class TrainUtils
	{
		// Token: 0x06001519 RID: 5401 RVA: 0x0007A3E4 File Offset: 0x000785E4
		public static ComponentCatalog.LoadableClassInfo CheckTrainer<TSig>(IExceptionContext ectx, SubComponent<ITrainer, TSig> trainer, string dataFile)
		{
			Contracts.CheckUserArg(ectx, SubComponentExtensions.IsGood(trainer), "trainer", "A trainer is required.");
			ComponentCatalog.LoadableClassInfo loadableClassInfo = ComponentCatalog.GetLoadableClassInfo<TSig>(trainer.Kind);
			if (loadableClassInfo == null)
			{
				throw Contracts.ExceptUserArg(ectx, "trainer", "Unknown trainer: '{0}'", new object[] { trainer.Kind });
			}
			if (!typeof(ITrainer).IsAssignableFrom(loadableClassInfo.Type))
			{
				throw Contracts.Except(ectx, "Loadable class '{0}' does not implement 'ITrainer'", new object[] { loadableClassInfo.LoadNames[0] });
			}
			if (string.IsNullOrWhiteSpace(dataFile))
			{
				throw Contracts.ExceptUserArg(ectx, "dataFile", "Data file must be defined.");
			}
			return loadableClassInfo;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0007A48C File Offset: 0x0007868C
		public static string MatchNameOrDefaultOrNull(IExceptionContext ectx, ISchema schema, string argName, string userName, string defaultName)
		{
			Contracts.CheckValue<ISchema>(ectx, schema, "schema");
			Contracts.CheckNonEmpty(ectx, argName, "argName");
			Contracts.CheckValue<string>(ectx, defaultName, "defaultName");
			if (string.IsNullOrWhiteSpace(userName))
			{
				return null;
			}
			int num;
			if (schema.TryGetColumnIndex(userName, ref num))
			{
				return userName;
			}
			if (userName == defaultName)
			{
				return null;
			}
			throw Contracts.ExceptUserArg(ectx, argName, "Could not find column '{0}'", new object[] { userName });
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0007A4FC File Offset: 0x000786FC
		public static IPredictor Train(IHostEnvironment env, IChannel ch, RoleMappedData data, ITrainer trainer, string name, RoleMappedData validData, SubComponent<ICalibratorTrainer, SignatureCalibrator> calibrator, int maxCalibrationExamples, bool? cacheData, IPredictor inpPredictor = null)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IChannel>(env, ch, "ch");
			Contracts.CheckValue<RoleMappedData>(ch, data, "data");
			Contracts.CheckValue<ITrainer>(ch, trainer, "trainer");
			Contracts.CheckNonEmpty(ch, name, "name");
			ITrainer<RoleMappedData> trainer2 = trainer as ITrainer<RoleMappedData>;
			if (trainer2 == null)
			{
				throw Contracts.ExceptUserArg(ch, "trainer", "Trainer '{0}' does not accept known training data type", new object[] { name });
			}
			Action<IChannel, ITrainer, Action<object>, object, object, object> action = new Action<IChannel, ITrainer, Action<object>, object, object, object>(TrainUtils.TrainCore<object, object>);
			TrainUtils.AddCacheIfWanted(env, ch, trainer, ref data, cacheData);
			ch.Trace("Training");
			if (validData != null)
			{
				TrainUtils.AddCacheIfWanted(env, ch, trainer, ref validData, cacheData);
			}
			MethodInfo methodInfo = action.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[]
			{
				typeof(RoleMappedData),
				(inpPredictor != null) ? inpPredictor.GetType() : typeof(IPredictor)
			});
			Action<RoleMappedData> action2 = new Action<RoleMappedData>(trainer2.Train);
			methodInfo.Invoke(null, new object[] { ch, trainer2, action2, data, validData, inpPredictor });
			ch.Trace("Constructing predictor");
			IPredictor predictor = trainer2.CreatePredictor();
			return CalibratorUtils.TrainCalibratorIfNeeded(env, ch, calibrator, maxCalibrationExamples, trainer, predictor, data);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0007A64D File Offset: 0x0007884D
		public static bool CanUseValidationData(ITrainer trainer)
		{
			Contracts.CheckValue<ITrainer>(trainer, "trainer");
			return trainer is ITrainer<RoleMappedData> && trainer is IValidatingTrainer<RoleMappedData>;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0007A670 File Offset: 0x00078870
		private static void TrainCore<TDataSet, TPredictor>(IChannel ch, ITrainer trainer, Action<TDataSet> train, TDataSet data, TDataSet validData = default(TDataSet), TPredictor predictor = default(TPredictor)) where TDataSet : class where TPredictor : class
		{
			if (validData != null)
			{
				if (predictor != null)
				{
					IIncrementalValidatingTrainer<TDataSet, TPredictor> incrementalValidatingTrainer = trainer as IIncrementalValidatingTrainer<TDataSet, TPredictor>;
					if (incrementalValidatingTrainer != null)
					{
						incrementalValidatingTrainer.Train(data, validData, predictor);
						return;
					}
					ch.Warning("Ignoring inputModelFile: Trainer is not an incremental trainer.");
				}
				IValidatingTrainer<TDataSet> validatingTrainer = trainer as IValidatingTrainer<TDataSet>;
				validatingTrainer.Train(data, validData);
				return;
			}
			if (predictor != null)
			{
				IIncrementalTrainer<TDataSet, TPredictor> incrementalTrainer = trainer as IIncrementalTrainer<TDataSet, TPredictor>;
				if (incrementalTrainer != null)
				{
					incrementalTrainer.Train(data, predictor);
					return;
				}
				ch.Warning("Ignoring inputModelFile: Trainer is not an incremental trainer.");
			}
			train(data);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0007A6F0 File Offset: 0x000788F0
		internal static bool TryLoadPredictor(IChannel ch, IHostEnvironment env, string inputModelFile, out IPredictor inputPredictor)
		{
			if (!string.IsNullOrEmpty(inputModelFile))
			{
				ch.Trace("Constructing predictor from input model");
				using (IFileHandle fileHandle = env.OpenInputFile(inputModelFile))
				{
					using (Stream stream = fileHandle.OpenReadStream())
					{
						using (RepositoryReader repositoryReader = RepositoryReader.Open(stream, true))
						{
							ch.Trace("Loading predictor");
							return ModelLoadContext.LoadModelOrNull<IPredictor, SignatureLoadModel>(out inputPredictor, repositoryReader, "Predictor", new object[] { env });
						}
					}
				}
			}
			inputPredictor = null;
			return false;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0007A79C File Offset: 0x0007899C
		public static void SaveModel(IHostEnvironment env, IChannel ch, IFileHandle output, IPredictor predictor, RoleMappedData data, string command = null)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IChannel>(env, ch, "ch");
			Contracts.CheckParam(ch, output != null && output.CanWrite, "output");
			Contracts.CheckValue<RoleMappedData>(ch, data, "data");
			using (Stream stream = output.CreateWriteStream())
			{
				TrainUtils.SaveModel(env, ch, stream, predictor, data, command);
			}
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0007A814 File Offset: 0x00078A14
		public static void SaveModel(IHostEnvironment env, IChannel ch, Stream outputStream, IPredictor predictor, RoleMappedData data, string command = null)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IChannel>(env, ch, "ch");
			Contracts.CheckValue<Stream>(ch, outputStream, "outputStream");
			Contracts.CheckValue<RoleMappedData>(ch, data, "data");
			using (IChannel channel = ch.Start("SaveModel"))
			{
				using (env.StartProgressChannel("Saving model"))
				{
					using (RepositoryWriter repositoryWriter = RepositoryWriter.CreateNew(outputStream, true))
					{
						if (predictor != null)
						{
							channel.Trace("Saving predictor");
							ModelSaveContext.SaveModel<IPredictor>(repositoryWriter, predictor, "Predictor");
						}
						channel.Trace("Saving loader and transformations");
						IDataView data2 = data.Data;
						if (data2 is IDataLoader)
						{
							ModelSaveContext.SaveModel<IDataView>(repositoryWriter, data2, "DataLoaderModel");
						}
						else
						{
							TrainUtils.SaveDataPipe(env, repositoryWriter, data2, false);
						}
						if (!string.IsNullOrWhiteSpace(command))
						{
							using (Repository.Entry entry = repositoryWriter.CreateEntry("TrainingInfo", "Command.txt"))
							{
								using (StreamWriter streamWriter = Utils.OpenWriter(entry.Stream, null, 1024, true))
								{
									streamWriter.WriteLine(command);
								}
							}
						}
						if (data.Schema.Feature != null)
						{
							ISchema schema = data2.Schema;
							ColumnInfo feature = data.Schema.Feature;
							VBuffer<DvText> vbuffer = VBufferUtils.CreateEmpty<DvText>(feature.Type.VectorSize);
							if (MetadataUtils.HasSlotNames(schema, feature.Index, feature.Type.VectorSize))
							{
								schema.GetMetadata<VBuffer<DvText>>("SlotNames", feature.Index, ref vbuffer);
							}
							Contracts.Check(channel, vbuffer.Length == feature.Type.VectorSize);
							using (ModelSaveContext modelSaveContext = new ModelSaveContext(repositoryWriter, "TrainingInfo", "FeatureNames.bin"))
							{
								FeatureNameCollection.Save(modelSaveContext, ref vbuffer);
								modelSaveContext.Done();
							}
						}
						ModelFileUtils.SaveRoleMappings(env, ch, data.Schema, repositoryWriter);
						repositoryWriter.Commit();
					}
					channel.Done();
				}
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0007AAC0 File Offset: 0x00078CC0
		public static void SaveDataPipe(IHostEnvironment env, RepositoryWriter repositoryWriter, IDataView dataPipe, bool blankLoader = false)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<RepositoryWriter>(env, repositoryWriter, "repositoryWriter");
			Contracts.CheckValue<IDataView>(env, dataPipe, "dataPipe");
			IDataView pipeStart;
			List<IDataTransform> list = TrainUtils.BacktrackPipe(dataPipe, out pipeStart);
			IDataLoader dataLoader;
			Action<ModelSaveContext> action;
			if (!blankLoader && (dataLoader = pipeStart as IDataLoader) != null)
			{
				action = new Action<ModelSaveContext>(dataLoader.Save);
			}
			else
			{
				action = delegate(ModelSaveContext ctx)
				{
					BinaryLoader.SaveInstance(env, ctx, pipeStart.Schema);
				};
			}
			using (ModelSaveContext dataModelSavingContext = ModelFileUtils.GetDataModelSavingContext(repositoryWriter))
			{
				CompositeDataLoader.SavePipe(env, dataModelSavingContext, action, list);
				dataModelSavingContext.Done();
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0007AB94 File Offset: 0x00078D94
		private static List<IDataTransform> BacktrackPipe(IDataView dataPipe, out IDataView pipeStart)
		{
			List<IDataTransform> list = new List<IDataTransform>();
			for (;;)
			{
				IDataTransform dataTransform = dataPipe as IDataTransform;
				if (dataTransform == null)
				{
					break;
				}
				list.Add(dataTransform);
				dataPipe = dataTransform.Source;
			}
			pipeStart = dataPipe;
			list.Reverse();
			return list;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0007ABCC File Offset: 0x00078DCC
		public static bool AddNormalizerIfNeeded(IHostEnvironment env, IChannel ch, ITrainer trainer, ref IDataView view, string featureColumn, NormalizeOption autoNorm)
		{
			Contracts.CheckUserArg(ch, Enum.IsDefined(typeof(NormalizeOption), autoNorm), "norm", "Normalize option is invalid. Specify one of 'norm=No', 'norm=Warn', 'norm=Auto', or 'norm=Yes'.");
			if (autoNorm == NormalizeOption.No)
			{
				ch.Info("Not adding a normalizer.");
				return false;
			}
			if (string.IsNullOrEmpty(featureColumn))
			{
				return false;
			}
			ISchema schema = view.Schema;
			int num;
			if (schema.TryGetColumnIndex(featureColumn, ref num))
			{
				if (autoNorm != NormalizeOption.Yes)
				{
					ITrainerEx trainerEx = trainer as ITrainerEx;
					DvBool @false = DvBool.False;
					if (trainerEx == null || !trainerEx.NeedNormalization || (MetadataUtils.TryGetMetadata<DvBool>(schema, BoolType.Instance, "IsNormalized", num, ref @false) && @false.IsTrue))
					{
						ch.Info("Not adding a normalizer.");
						return false;
					}
					if (autoNorm == NormalizeOption.Warn)
					{
						ch.Warning("A normalizer is needed for this trainer. Either add a normalizing transform or use the 'norm=Auto', 'norm=Yes' or 'norm=No' options.");
						return false;
					}
				}
				ch.Info("Automatically adding a MinMax normalization transform, use 'norm=Warn' or 'norm=No' to turn this behavior off.");
				string text = featureColumn;
				StringBuilder stringBuilder = new StringBuilder();
				if (CmdQuoter.QuoteValue(text, stringBuilder, false))
				{
					text = stringBuilder.ToString();
				}
				SubComponent<IDataTransform, SignatureDataTransform> subComponent = new SubComponent<IDataTransform, SignatureDataTransform>("MinMax", new string[] { string.Format("col={{ name={0} source={0} }}", text) });
				IDataLoader dataLoader = view as IDataLoader;
				if (dataLoader != null)
				{
					view = CompositeDataLoader.Create(env, dataLoader, new KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>[]
					{
						new KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>(null, subComponent)
					});
				}
				else
				{
					view = ComponentCatalog.CreateInstance<IDataTransform, SignatureDataTransform>(subComponent, new object[] { env, view });
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0007AD44 File Offset: 0x00078F44
		private static bool AddCacheIfWanted(IHostEnvironment env, IChannel ch, ITrainer trainer, ref RoleMappedData data, bool? cacheData)
		{
			ITrainerEx trainerEx = trainer as ITrainerEx;
			bool flag = cacheData ?? (!(data.Data is BinaryLoader) && (trainerEx == null || trainerEx.WantCaching));
			if (flag)
			{
				ch.Trace("Caching");
				int[] array = (from kc in data.Schema.GetColumnRoles()
					select kc.Value.Index).ToArray<int>();
				CacheDataView cacheDataView = new CacheDataView(env, data.Data, array);
				data = RoleMappedData.Create(cacheDataView, data.Schema.GetColumnRoleNames());
			}
			else
			{
				ch.Trace("Not caching");
			}
			return flag;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0007AE28 File Offset: 0x00079028
		public static IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> CheckAndGenerateCustomColumns(IExceptionContext ectx, KeyValuePair<string, string>[] customColumnArg)
		{
			if (customColumnArg == null)
			{
				return Enumerable.Empty<KeyValuePair<RoleMappedSchema.ColumnRole, string>>();
			}
			foreach (KeyValuePair<string, string> keyValuePair in customColumnArg)
			{
				Contracts.CheckUserArg(ectx, !string.IsNullOrWhiteSpace(keyValuePair.Value), "customColumns", "Names for columns with custom kind must not be empty");
				if (string.IsNullOrWhiteSpace(keyValuePair.Key))
				{
					throw Contracts.ExceptUserArg(ectx, "customColumns", "Custom column with name '{0}' needs a kind. Use col[<Kind>]={0}", new object[] { keyValuePair.Value });
				}
			}
			return customColumnArg.Select((KeyValuePair<string, string> kindName) => new RoleMappedSchema.ColumnRole(kindName.Key).Bind(kindName.Value));
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0007AED0 File Offset: 0x000790D0
		public static RoleMappedSchema CreateRoleMappedSchemaOpt(ISchema schema, string feature, string group, IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> custom = null)
		{
			List<KeyValuePair<RoleMappedSchema.ColumnRole, string>> list = new List<KeyValuePair<RoleMappedSchema.ColumnRole, string>>();
			if (!string.IsNullOrWhiteSpace(feature))
			{
				list.Add(RoleMappedSchema.ColumnRole.Feature.Bind(feature));
			}
			if (!string.IsNullOrWhiteSpace(group))
			{
				list.Add(RoleMappedSchema.ColumnRole.Group.Bind(group));
			}
			if (custom != null)
			{
				list.AddRange(custom);
			}
			return RoleMappedSchema.CreateOpt(schema, list);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0007AF2C File Offset: 0x0007912C
		public static RoleMappedData CreateExamples(IDataView view, string label, string feature, string group = null, string weight = null, string name = null, IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> custom = null)
		{
			List<KeyValuePair<RoleMappedSchema.ColumnRole, string>> list = new List<KeyValuePair<RoleMappedSchema.ColumnRole, string>>();
			if (!string.IsNullOrWhiteSpace(label))
			{
				list.Add(RoleMappedSchema.ColumnRole.Label.Bind(label));
			}
			if (!string.IsNullOrWhiteSpace(feature))
			{
				list.Add(RoleMappedSchema.ColumnRole.Feature.Bind(feature));
			}
			if (!string.IsNullOrWhiteSpace(group))
			{
				list.Add(RoleMappedSchema.ColumnRole.Group.Bind(group));
			}
			if (!string.IsNullOrWhiteSpace(weight))
			{
				list.Add(RoleMappedSchema.ColumnRole.Weight.Bind(weight));
			}
			if (!string.IsNullOrWhiteSpace(name))
			{
				list.Add(RoleMappedSchema.ColumnRole.Name.Bind(name));
			}
			if (custom != null)
			{
				list.AddRange(custom);
			}
			return RoleMappedData.Create(view, list);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0007AFE4 File Offset: 0x000791E4
		public static RoleMappedData CreateExamplesOpt(IDataView view, string label, string feature, string group = null, string weight = null, string name = null, IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> custom = null)
		{
			List<KeyValuePair<RoleMappedSchema.ColumnRole, string>> list = new List<KeyValuePair<RoleMappedSchema.ColumnRole, string>>();
			if (!string.IsNullOrWhiteSpace(label))
			{
				list.Add(RoleMappedSchema.ColumnRole.Label.Bind(label));
			}
			if (!string.IsNullOrWhiteSpace(feature))
			{
				list.Add(RoleMappedSchema.ColumnRole.Feature.Bind(feature));
			}
			if (!string.IsNullOrWhiteSpace(group))
			{
				list.Add(RoleMappedSchema.ColumnRole.Group.Bind(group));
			}
			if (!string.IsNullOrWhiteSpace(weight))
			{
				list.Add(RoleMappedSchema.ColumnRole.Weight.Bind(weight));
			}
			if (!string.IsNullOrWhiteSpace(name))
			{
				list.Add(RoleMappedSchema.ColumnRole.Name.Bind(name));
			}
			if (custom != null)
			{
				list.AddRange(custom);
			}
			return RoleMappedData.CreateOpt(view, list);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0007B09C File Offset: 0x0007929C
		private static KeyValuePair<RoleMappedSchema.ColumnRole, T> Pair<T>(RoleMappedSchema.ColumnRole kind, T value)
		{
			return new KeyValuePair<RoleMappedSchema.ColumnRole, T>(kind, value);
		}
	}
}
