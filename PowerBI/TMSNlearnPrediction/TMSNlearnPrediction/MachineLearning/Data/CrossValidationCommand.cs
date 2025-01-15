using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001F0 RID: 496
	public class CrossValidationCommand : ICommand
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x0003B25D File Offset: 0x0003945D
		public CrossValidationCommand(CrossValidationCommand.Arguments args, IHostEnvironment env)
		{
			this._impl = new CrossValidationCommand.Impl(args, env);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0003B272 File Offset: 0x00039472
		public void Run()
		{
			this._impl.Run();
		}

		// Token: 0x040005D5 RID: 1493
		private readonly CrossValidationCommand.Impl _impl;

		// Token: 0x020001F1 RID: 497
		public sealed class Arguments : DataCommand.ArgumentsBase
		{
			// Token: 0x040005D6 RID: 1494
			[Argument(4, HelpText = "Trainer to use", ShortName = "tr")]
			public SubComponent<ITrainer, SignatureTrainer> trainer = new SubComponent<ITrainer, SignatureTrainer>("AveragedPerceptron");

			// Token: 0x040005D7 RID: 1495
			[Argument(4, HelpText = "Scorer to use", NullName = "<Auto>", SortOrder = 101)]
			public SubComponent<IDataScorerTransform, SignatureDataScorer> scorer;

			// Token: 0x040005D8 RID: 1496
			[Argument(4, HelpText = "Evaluator to use", ShortName = "eval", NullName = "<Auto>", SortOrder = 102)]
			public SubComponent<IMamlEvaluator, SignatureMamlEvaluator> evaluator;

			// Token: 0x040005D9 RID: 1497
			[Argument(0, HelpText = "Results summary filename", ShortName = "sf")]
			public string summaryFilename;

			// Token: 0x040005DA RID: 1498
			[Argument(4, HelpText = "Column to use for features", ShortName = "feat", SortOrder = 2)]
			public string featureColumn = "Features";

			// Token: 0x040005DB RID: 1499
			[Argument(4, HelpText = "Column to use for labels", ShortName = "lab", SortOrder = 3)]
			public string labelColumn = "Label";

			// Token: 0x040005DC RID: 1500
			[Argument(4, HelpText = "Column to use for example weight", ShortName = "weight", SortOrder = 4)]
			public string weightColumn = "Weight";

			// Token: 0x040005DD RID: 1501
			[Argument(4, HelpText = "Column to use for grouping", ShortName = "group", SortOrder = 5)]
			public string groupColumn = "GroupId";

			// Token: 0x040005DE RID: 1502
			[Argument(0, HelpText = "Name column name", ShortName = "name", SortOrder = 6)]
			public string nameColumn = "Name";

			// Token: 0x040005DF RID: 1503
			[Argument(4, HelpText = "Column to use for stratification", ShortName = "strat", SortOrder = 7)]
			public string stratificationColumn;

			// Token: 0x040005E0 RID: 1504
			[Argument(4, HelpText = "Columns with custom kinds declared through key assignments, e.g., col[Kind]=Name to assign column named 'Name' kind 'Kind'", ShortName = "col", SortOrder = 10)]
			public KeyValuePair<string, string>[] customColumn;

			// Token: 0x040005E1 RID: 1505
			[Argument(4, HelpText = "Number of folds in k-fold cross-validation", ShortName = "k")]
			public int numFolds = 2;

			// Token: 0x040005E2 RID: 1506
			[Argument(4, HelpText = "Use threads", ShortName = "threads")]
			public bool useThreads = true;

			// Token: 0x040005E3 RID: 1507
			[Argument(4, HelpText = "Normalize option for the feature column", ShortName = "norm")]
			public NormalizeOption normalizeFeatures = NormalizeOption.Auto;

			// Token: 0x040005E4 RID: 1508
			[Argument(4, HelpText = "Whether we should cache input training data", ShortName = "cache")]
			public bool? cacheData;

			// Token: 0x040005E5 RID: 1509
			[Argument(4, HelpText = "Transforms to apply prior to splitting the data into folds", ShortName = "prexf")]
			public KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>[] preTransform;

			// Token: 0x040005E6 RID: 1510
			[Argument(0, IsInputFileName = true, HelpText = "The validation data file", ShortName = "valid")]
			public string validationFile;

			// Token: 0x040005E7 RID: 1511
			[Argument(4, HelpText = "Output calibrator", ShortName = "cali", NullName = "<None>")]
			public SubComponent<ICalibratorTrainer, SignatureCalibrator> calibrator = new SubComponent<ICalibratorTrainer, SignatureCalibrator>("PlattCalibration");

			// Token: 0x040005E8 RID: 1512
			[Argument(4, HelpText = "Number of instances to train the calibrator", ShortName = "numcali")]
			public int maxCalibrationExamples = 1000000000;

			// Token: 0x040005E9 RID: 1513
			[Argument(4, HelpText = "File to save per-instance predictions and metrics to", ShortName = "dout")]
			public string outputDataFile;

			// Token: 0x040005EA RID: 1514
			[Argument(0, HelpText = "Print the run/fold index in per-instance output", ShortName = "opf")]
			public bool outputExampleFoldIndex;

			// Token: 0x040005EB RID: 1515
			[Argument(4, HelpText = "Whether we should load predictor from input model and use it as the initial model state", ShortName = "cont")]
			public bool continueTrain;
		}

		// Token: 0x020001F2 RID: 498
		private sealed class Impl : DataCommand.ImplBase<CrossValidationCommand.Arguments>
		{
			// Token: 0x06000B1C RID: 2844 RVA: 0x0003B30C File Offset: 0x0003950C
			public Impl(CrossValidationCommand.Arguments args, IHostEnvironment env)
				: base("CrossValidationCommand", args, env, null)
			{
				Contracts.CheckUserArg(this._host, args.numFolds >= 2, "numFolds", "Number of folds must be greater than or equal to 2.");
				this._info = TrainUtils.CheckTrainer<SignatureTrainer>(this._host, args.trainer, args.dataFile);
				Utils.CheckOptionalUserDirectory(this._args.summaryFilename, "summaryFilename");
				Utils.CheckOptionalUserDirectory(this._args.outputDataFile, "outputDataFile");
			}

			// Token: 0x06000B1D RID: 2845 RVA: 0x0003B397 File Offset: 0x00039597
			private Impl(CrossValidationCommand.Impl impl)
				: base(impl)
			{
				this._info = impl._info;
			}

			// Token: 0x06000B1E RID: 2846 RVA: 0x0003B3AC File Offset: 0x000395AC
			public override void Run()
			{
				string text = "CV";
				using (IChannel channel = this._host.Start(text))
				{
					string settings = CmdParser.GetSettings(this._args, new CrossValidationCommand.Arguments(), 3);
					string text2 = string.Format("maml.exe {0} {1}", text, settings);
					channel.Info(text2);
					this.SendTelemetry(channel);
					using (new TimerScope(channel))
					{
						this.RunCore(channel, text2);
					}
					channel.Done();
				}
			}

			// Token: 0x06000B1F RID: 2847 RVA: 0x0003B448 File Offset: 0x00039648
			protected override void SendTelemetryCore(IPipe<TelemetryMessage> pipe)
			{
				base.SendTelemetryComponent(pipe, this._args.trainer);
				base.SendTelemetryCore(pipe);
			}

			// Token: 0x06000B20 RID: 2848 RVA: 0x0003B534 File Offset: 0x00039734
			private void RunCore(IChannel ch, string cmd)
			{
				IPredictor predictor = null;
				if (this._args.continueTrain && !TrainUtils.TryLoadPredictor(ch, this._host, this._args.inputModelFile, out predictor))
				{
					ch.Warning("No input model file specified or model file did not contain a predictor. The model state cannot be initialized.");
				}
				ch.Trace("Constructing data pipeline");
				IDataLoader dataLoader = base.CreateRawLoader("TextLoader", null);
				KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>[] array = this._args.preTransform;
				if (!string.IsNullOrEmpty(this._args.outputDataFile) && TrainUtils.MatchNameOrDefaultOrNull(ch, dataLoader.Schema, "nameColumn", this._args.nameColumn, "Name") == null)
				{
					string settings = CmdParser.GetSettings(new GenerateNumberTransform.Arguments
					{
						column = new GenerateNumberTransform.Column[]
						{
							new GenerateNumberTransform.Column
							{
								name = "Name"
							}
						},
						useCounter = true
					}, new GenerateNumberTransform.Arguments(), 3);
					array = array.Concat(new KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>[]
					{
						new KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>("", new SubComponent<IDataTransform, SignatureDataTransform>("GenerateNumberTransform", new string[] { settings }))
					}).ToArray<KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>>();
				}
				dataLoader = CompositeDataLoader.Create(this._host, dataLoader, array);
				ch.Trace("Binding label and features columns");
				IDataView dataView = dataLoader;
				string splitColumn = this.GetSplitColumn(ch, dataLoader, ref dataView);
				SubComponent<IDataScorerTransform, SignatureDataScorer> scorer = this._args.scorer;
				SubComponent<IMamlEvaluator, SignatureMamlEvaluator> subComponent = this._args.evaluator;
				Func<IDataView> func = null;
				if (this._args.validationFile != null)
				{
					func = delegate
					{
						CrossValidationCommand.Impl impl = new CrossValidationCommand.Impl(this);
						return impl.CreateRawLoader("TextLoader", this._args.validationFile);
					};
				}
				CrossValidationCommand.FoldHelper foldHelper = new CrossValidationCommand.FoldHelper(this._host, "CrossValidationCommand", dataView, splitColumn, this._args, new Func<IHostEnvironment, IChannel, IDataView, ITrainer, RoleMappedData>(this.CreateRoleMappedData), new Func<IHostEnvironment, IChannel, IDataView, RoleMappedData, IDataView, RoleMappedData>(this.ApplyAllTransformsToData), scorer, subComponent, func, new Func<IHostEnvironment, IChannel, IDataView, RoleMappedData, IDataView, RoleMappedData>(this.ApplyAllTransformsToData), predictor, cmd, dataLoader, !string.IsNullOrEmpty(this._args.outputDataFile));
				Task<CrossValidationCommand.FoldHelper.FoldResult>[] crossValidationTasks = foldHelper.GetCrossValidationTasks();
				if (!SubComponentExtensions.IsGood(subComponent))
				{
					subComponent = EvaluateUtils.GetEvaluatorType(ch, crossValidationTasks[0].Result.ScoreSchema);
				}
				IMamlEvaluator mamlEvaluator = ComponentCatalog.CreateInstance<IMamlEvaluator, SignatureMamlEvaluator>(subComponent, new object[] { this._host });
				for (int j = 0; j < crossValidationTasks.Length; j++)
				{
					Dictionary<string, IDataView> metrics = crossValidationTasks[j].Result.Metrics;
					MetricWriter.PrintWarnings(ch, metrics);
					mamlEvaluator.PrintFoldResults(ch, metrics);
				}
				mamlEvaluator.PrintOverallResults(ch, this._args.summaryFilename, crossValidationTasks.Select((Task<CrossValidationCommand.FoldHelper.FoldResult> t) => t.Result.Metrics).ToArray<Dictionary<string, IDataView>>());
				if (!string.IsNullOrWhiteSpace(this._args.outputDataFile))
				{
					Func<Task<CrossValidationCommand.FoldHelper.FoldResult>, int, IDataView> func2 = delegate(Task<CrossValidationCommand.FoldHelper.FoldResult> task, int i)
					{
						if (!this._args.outputExampleFoldIndex)
						{
							return task.Result.PerInstanceResults;
						}
						string columnName = task.Result.PerInstanceResults.Schema.GetColumnName(0);
						ColumnType columnType = task.Result.PerInstanceResults.Schema.GetColumnType(0);
						return Utils.MarshalInvoke<IHost, IDataView, string, string, ColumnType, int, int, string, ValueGetter<VBuffer<DvText>>, IDataView>(new Func<IHost, IDataView, string, string, ColumnType, int, int, string, ValueGetter<VBuffer<DvText>>, IDataView>(EvaluateUtils.AddKeyColumn<int>), columnType.RawType, this._host, task.Result.PerInstanceResults, columnName, "Fold Index", columnType, this._args.numFolds, i + 1, "FoldIndex", null);
					};
					IDataView dataView2 = AppendRowsDataView.Create(this._host, null, crossValidationTasks.Select(func2).ToArray<IDataView>());
					MetricWriter.SavePerInstance(this._host, ch, this._args.outputDataFile, dataView2, true, true);
				}
			}

			// Token: 0x06000B21 RID: 2849 RVA: 0x0003B848 File Offset: 0x00039A48
			private RoleMappedData ApplyAllTransformsToData(IHostEnvironment env, IChannel ch, IDataView dstData, RoleMappedData srcData, IDataView marker)
			{
				IDataView dataView = ApplyTransformUtils.ApplyAllTransformsToData(env, srcData.Data, dstData, marker);
				return RoleMappedData.Create(dataView, srcData.Schema.GetColumnRoleNames());
			}

			// Token: 0x06000B22 RID: 2850 RVA: 0x0003B878 File Offset: 0x00039A78
			private RoleMappedData CreateRoleMappedData(IHostEnvironment env, IChannel ch, IDataView data, ITrainer trainer)
			{
				foreach (KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>> keyValuePair in this._args.transform)
				{
					data = ComponentCatalog.CreateInstance<IDataTransform, SignatureDataTransform>(keyValuePair.Value, new object[] { env, data });
				}
				ISchema schema = data.Schema;
				string text = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "labelColumn", this._args.labelColumn, "Label");
				string text2 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "featureColumn", this._args.featureColumn, "Features");
				string text3 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "weightColumn", this._args.weightColumn, "Weight");
				string text4 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "nameColumn", this._args.nameColumn, "Name");
				string text5 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "groupColumn", this._args.groupColumn, "GroupId");
				TrainUtils.AddNormalizerIfNeeded(env, ch, trainer, ref data, text2, this._args.normalizeFeatures);
				IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = TrainUtils.CheckAndGenerateCustomColumns(ch, this._args.customColumn);
				return TrainUtils.CreateExamples(data, text, text2, text5, text3, text4, enumerable);
			}

			// Token: 0x06000B23 RID: 2851 RVA: 0x0003B9AC File Offset: 0x00039BAC
			private string GetSplitColumn(IChannel ch, IDataView input, ref IDataView output)
			{
				ISchema schema = input.Schema;
				output = input;
				string text = null;
				if (!string.IsNullOrWhiteSpace(this._args.stratificationColumn))
				{
					text = this._args.stratificationColumn;
				}
				else
				{
					string text2 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "groupColumn", this._args.groupColumn, "GroupId");
					int num;
					if (text2 != null && schema.TryGetColumnIndex(text2, ref num))
					{
						ColumnType columnType = schema.GetColumnType(num);
						if (RangeFilter.IsValidRangeFilterColumnType(ch, columnType))
						{
							text = text2;
						}
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = "StratificationColumn";
					int num2 = 0;
					int num3;
					while (input.Schema.TryGetColumnIndex(text, ref num3))
					{
						text = string.Format("StratificationColumn_{0:000}", ++num2);
					}
					output = new GenerateNumberTransform(new GenerateNumberTransform.Arguments
					{
						column = new GenerateNumberTransform.Column[]
						{
							new GenerateNumberTransform.Column
							{
								name = text
							}
						}
					}, this._host, input);
				}
				else
				{
					int num4;
					if (!input.Schema.TryGetColumnIndex(text, ref num4))
					{
						throw Contracts.ExceptUserArg(ch, "stratificationColumn", "Column '{0}' does not exist", new object[] { text });
					}
					ColumnType columnType2 = input.Schema.GetColumnType(num4);
					if (!RangeFilter.IsValidRangeFilterColumnType(ch, columnType2))
					{
						ch.Info("Hashing the stratification column");
						string text3 = text;
						int num5 = 0;
						int num6;
						while (input.Schema.TryGetColumnIndex(text, ref num6))
						{
							text = string.Format("{0}_{1:000}", text3, ++num5);
						}
						output = new HashTransform(new HashTransform.Arguments
						{
							column = new HashTransform.Column[]
							{
								new HashTransform.Column
								{
									source = text3,
									name = text
								}
							},
							hashBits = 30
						}, this._host, input);
					}
				}
				return text;
			}

			// Token: 0x040005EC RID: 1516
			private const string RegistrationName = "CrossValidationCommand";

			// Token: 0x040005ED RID: 1517
			private readonly ComponentCatalog.LoadableClassInfo _info;
		}

		// Token: 0x020001F3 RID: 499
		private sealed class FoldHelper
		{
			// Token: 0x06000B27 RID: 2855 RVA: 0x0003BB7C File Offset: 0x00039D7C
			public FoldHelper(IHostEnvironment env, string registrationName, IDataView inputDataView, string splitColumn, CrossValidationCommand.Arguments args, Func<IHostEnvironment, IChannel, IDataView, ITrainer, RoleMappedData> createExamples, Func<IHostEnvironment, IChannel, IDataView, RoleMappedData, IDataView, RoleMappedData> applyTransformsToTestData, SubComponent<IDataScorerTransform, SignatureDataScorer> scorer, SubComponent<IMamlEvaluator, SignatureMamlEvaluator> evaluator, Func<IDataView> getValidationDataView = null, Func<IHostEnvironment, IChannel, IDataView, RoleMappedData, IDataView, RoleMappedData> applyTransformsToValidationData = null, IPredictor inputPredictor = null, string cmd = null, IDataLoader loader = null, bool savePerInstance = false)
			{
				Contracts.CheckValue<IHostEnvironment>(env, "env");
				Contracts.CheckNonWhiteSpace(env, registrationName, "registrationName");
				Contracts.CheckValue<IDataView>(env, inputDataView, "input");
				Contracts.CheckValue<string>(env, splitColumn, "splitColumn");
				Contracts.CheckParam(env, args.numFolds > 1, "numFolds");
				Contracts.CheckValue<Func<IHostEnvironment, IChannel, IDataView, ITrainer, RoleMappedData>>(env, createExamples, "createExamples");
				Contracts.CheckValue<Func<IHostEnvironment, IChannel, IDataView, RoleMappedData, IDataView, RoleMappedData>>(env, applyTransformsToTestData, "applyTransformsToTestData");
				Contracts.CheckParam(env, SubComponentExtensions.IsGood(args.trainer), "trainer");
				Contracts.CheckParam(env, args.maxCalibrationExamples > 0, "maxCalibrationExamples");
				Contracts.CheckParam(env, getValidationDataView == null || applyTransformsToValidationData != null, "applyTransformsToValidationData");
				this._env = env;
				this._registrationName = registrationName;
				this._inputDataView = inputDataView;
				this._splitColumn = splitColumn;
				this._numFolds = args.numFolds;
				this._createExamples = createExamples;
				this._applyTransformsToTestData = applyTransformsToTestData;
				this._trainer = args.trainer;
				this._scorer = scorer;
				this._evaluator = evaluator;
				this._calibrator = args.calibrator;
				this._maxCalibrationExamples = args.maxCalibrationExamples;
				this._useThreads = args.useThreads;
				this._cacheData = args.cacheData;
				this._getValidationDataView = getValidationDataView;
				this._applyTransformsToValidationData = applyTransformsToValidationData;
				this._inputPredictor = inputPredictor;
				this._cmd = cmd;
				this._outputModelFile = args.outputModelFile;
				this._loader = loader;
				this._savePerInstance = savePerInstance;
			}

			// Token: 0x06000B28 RID: 2856 RVA: 0x0003BD00 File Offset: 0x00039F00
			private IHost GetHost()
			{
				IHostEnvironment hostEnvironment = this._env.Fork(null, null, null);
				return hostEnvironment.Register(this._registrationName);
			}

			// Token: 0x06000B29 RID: 2857 RVA: 0x0003BD5C File Offset: 0x00039F5C
			public Task<CrossValidationCommand.FoldHelper.FoldResult>[] GetCrossValidationTasks()
			{
				Task<CrossValidationCommand.FoldHelper.FoldResult>[] array = new Task<CrossValidationCommand.FoldHelper.FoldResult>[this._numFolds];
				for (int i = 0; i < this._numFolds; i++)
				{
					int fold = i;
					array[i] = new Task<CrossValidationCommand.FoldHelper.FoldResult>(() => this.RunFold(fold));
					if (this._useThreads)
					{
						array[i].Start();
					}
					else
					{
						array[i].RunSynchronously();
					}
				}
				Task.WaitAll(array);
				return array;
			}

			// Token: 0x06000B2A RID: 2858 RVA: 0x0003BDEC File Offset: 0x00039FEC
			private CrossValidationCommand.FoldHelper.FoldResult RunFold(int fold)
			{
				IHost host = this.GetHost();
				CrossValidationCommand.FoldHelper.FoldResult foldResult;
				using (IChannel channel = host.Start(string.Format("Fold {0}", fold)))
				{
					channel.Trace("Constructing trainer");
					ITrainer trainer = ComponentCatalog.CreateInstance<ITrainer, SignatureTrainer>(this._trainer, new object[] { host });
					RangeFilter.Arguments arguments = new RangeFilter.Arguments();
					arguments.column = this._splitColumn;
					arguments.min = new double?((double)fold / (double)this._numFolds);
					arguments.max = new double?((double)(fold + 1) / (double)this._numFolds);
					arguments.complement = true;
					IDataView dataView = new RangeFilter(arguments, host, this._inputDataView);
					dataView = new OpaqueDataView(dataView);
					RoleMappedData trainData = this._createExamples(host, channel, dataView, trainer);
					IDataView dataView2 = new RangeFilter(new RangeFilter.Arguments
					{
						column = arguments.column,
						min = arguments.min,
						max = arguments.max
					}, host, this._inputDataView);
					dataView2 = new OpaqueDataView(dataView2);
					RoleMappedData roleMappedData = this._applyTransformsToTestData(host, channel, dataView2, trainData, dataView);
					RoleMappedData roleMappedData2 = null;
					if (this._getValidationDataView != null)
					{
						if (!TrainUtils.CanUseValidationData(trainer))
						{
							channel.Warning("Trainer does not accept validation dataset.");
						}
						else
						{
							channel.Trace("Constructing the validation pipeline");
							IDataView dataView3 = this._getValidationDataView();
							IDataView dataView4 = ApplyTransformUtils.ApplyAllTransformsToData(host, this._inputDataView, dataView3, null);
							dataView4 = new OpaqueDataView(dataView4);
							roleMappedData2 = this._applyTransformsToValidationData(host, channel, dataView4, trainData, dataView);
						}
					}
					IPredictor predictor = TrainUtils.Train(host, channel, trainData, trainer, this._trainer.Kind, roleMappedData2, this._calibrator, this._maxCalibrationExamples, this._cacheData, this._inputPredictor);
					channel.Trace("Scoring and evaluating");
					ISchemaBindableMapper schemaBindableMapper = ScoreUtils.GetSchemaBindableMapper(host, predictor, this._scorer);
					ISchemaBoundMapper schemaBoundMapper = schemaBindableMapper.Bind(host, roleMappedData.Schema);
					SubComponent<IDataScorerTransform, SignatureDataScorer> subComponent = (SubComponentExtensions.IsGood(this._scorer) ? this._scorer : ScoreUtils.GetScorerComponent(schemaBoundMapper));
					IDataScorerTransform dataScorerTransform = ComponentCatalog.CreateInstance<IDataScorerTransform, SignatureDataScorer>(subComponent, new object[] { host, roleMappedData.Data, schemaBoundMapper });
					string text = this.ConstructModelName(this._outputModelFile, fold);
					if (text != null && this._loader != null)
					{
						using (IFileHandle fileHandle = host.CreateOutputFile(text))
						{
							RoleMappedData roleMappedData3 = RoleMappedData.Create(CompositeDataLoader.ApplyTransform(host, this._loader, null, null, (IHostEnvironment e, IDataView newSource) => ApplyTransformUtils.ApplyAllTransformsToData(e, trainData.Data, newSource, null)), trainData.Schema.GetColumnRoleNames());
							TrainUtils.SaveModel(host, channel, fileHandle, predictor, roleMappedData3, this._cmd);
						}
					}
					SubComponent<IMamlEvaluator, SignatureMamlEvaluator> subComponent2 = this._evaluator;
					if (!SubComponentExtensions.IsGood(subComponent2))
					{
						subComponent2 = EvaluateUtils.GetEvaluatorType(channel, dataScorerTransform.Schema);
					}
					IMamlEvaluator mamlEvaluator = ComponentCatalog.CreateInstance<IMamlEvaluator, SignatureMamlEvaluator>(subComponent2, new object[] { host });
					RoleMappedData roleMappedData4 = RoleMappedData.CreateOpt(dataScorerTransform, roleMappedData.Schema.GetColumnRoleNames());
					Dictionary<string, IDataView> dictionary = mamlEvaluator.Evaluate(roleMappedData4);
					IDataView dataView5 = null;
					if (this._savePerInstance)
					{
						IDataTransform perInstanceMetrics = mamlEvaluator.GetPerInstanceMetrics(roleMappedData4);
						RoleMappedData roleMappedData5 = RoleMappedData.CreateOpt(perInstanceMetrics, roleMappedData4.Schema.GetColumnRoleNames());
						dataView5 = mamlEvaluator.GetPerInstanceDataViewToSave(roleMappedData5);
					}
					channel.Done();
					foldResult = new CrossValidationCommand.FoldHelper.FoldResult(dictionary, roleMappedData4.Schema.Schema, dataView5);
				}
				return foldResult;
			}

			// Token: 0x06000B2B RID: 2859 RVA: 0x0003C1A0 File Offset: 0x0003A3A0
			private string ConstructModelName(string outputModelFile, int fold)
			{
				if (string.IsNullOrWhiteSpace(outputModelFile))
				{
					return null;
				}
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputModelFile);
				return Path.Combine(Path.GetDirectoryName(outputModelFile), string.Format("{0}.fold{1:000}{2}", fileNameWithoutExtension, fold, Path.GetExtension(outputModelFile)));
			}

			// Token: 0x040005EF RID: 1519
			private readonly IHostEnvironment _env;

			// Token: 0x040005F0 RID: 1520
			private readonly string _registrationName;

			// Token: 0x040005F1 RID: 1521
			private readonly IDataView _inputDataView;

			// Token: 0x040005F2 RID: 1522
			private readonly string _splitColumn;

			// Token: 0x040005F3 RID: 1523
			private readonly int _numFolds;

			// Token: 0x040005F4 RID: 1524
			private readonly SubComponent<ITrainer, SignatureTrainer> _trainer;

			// Token: 0x040005F5 RID: 1525
			private readonly SubComponent<IDataScorerTransform, SignatureDataScorer> _scorer;

			// Token: 0x040005F6 RID: 1526
			private readonly SubComponent<IMamlEvaluator, SignatureMamlEvaluator> _evaluator;

			// Token: 0x040005F7 RID: 1527
			private readonly SubComponent<ICalibratorTrainer, SignatureCalibrator> _calibrator;

			// Token: 0x040005F8 RID: 1528
			private readonly int _maxCalibrationExamples;

			// Token: 0x040005F9 RID: 1529
			private readonly bool _useThreads;

			// Token: 0x040005FA RID: 1530
			private readonly bool? _cacheData;

			// Token: 0x040005FB RID: 1531
			private readonly IPredictor _inputPredictor;

			// Token: 0x040005FC RID: 1532
			private readonly string _cmd;

			// Token: 0x040005FD RID: 1533
			private readonly string _outputModelFile;

			// Token: 0x040005FE RID: 1534
			private readonly IDataLoader _loader;

			// Token: 0x040005FF RID: 1535
			private readonly bool _savePerInstance;

			// Token: 0x04000600 RID: 1536
			private readonly Func<IHostEnvironment, IChannel, IDataView, ITrainer, RoleMappedData> _createExamples;

			// Token: 0x04000601 RID: 1537
			private readonly Func<IHostEnvironment, IChannel, IDataView, RoleMappedData, IDataView, RoleMappedData> _applyTransformsToTestData;

			// Token: 0x04000602 RID: 1538
			private readonly Func<IDataView> _getValidationDataView;

			// Token: 0x04000603 RID: 1539
			private readonly Func<IHostEnvironment, IChannel, IDataView, RoleMappedData, IDataView, RoleMappedData> _applyTransformsToValidationData;

			// Token: 0x020001F4 RID: 500
			public struct FoldResult
			{
				// Token: 0x06000B2C RID: 2860 RVA: 0x0003C1E0 File Offset: 0x0003A3E0
				public FoldResult(Dictionary<string, IDataView> metrics, ISchema scoreSchema, IDataView perInstance)
				{
					this.Metrics = metrics;
					this.ScoreSchema = scoreSchema;
					this.PerInstanceResults = perInstance;
				}

				// Token: 0x04000604 RID: 1540
				public readonly Dictionary<string, IDataView> Metrics;

				// Token: 0x04000605 RID: 1541
				public readonly ISchema ScoreSchema;

				// Token: 0x04000606 RID: 1542
				public readonly IDataView PerInstanceResults;
			}
		}
	}
}
