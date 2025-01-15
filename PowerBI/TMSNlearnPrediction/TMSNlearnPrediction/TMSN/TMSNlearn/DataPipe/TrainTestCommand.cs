using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.TMSN.TMSNlearn.DataPipe
{
	// Token: 0x020003E2 RID: 994
	public sealed class TrainTestCommand : ICommand
	{
		// Token: 0x0600152C RID: 5420 RVA: 0x0007B0A5 File Offset: 0x000792A5
		public TrainTestCommand(TrainTestCommand.Arguments args, IHostEnvironment env)
		{
			this._impl = new TrainTestCommand.Impl(args, env);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0007B0BA File Offset: 0x000792BA
		public void Run()
		{
			this._impl.Run();
		}

		// Token: 0x04000CBD RID: 3261
		internal const string Summary = "Trains a predictor using the train file and then scores and evaluates the predictor using the test file.";

		// Token: 0x04000CBE RID: 3262
		private readonly TrainTestCommand.Impl _impl;

		// Token: 0x020003E3 RID: 995
		public sealed class Arguments : DataCommand.ArgumentsBase
		{
			// Token: 0x04000CBF RID: 3263
			[Argument(0, IsInputFileName = true, HelpText = "The test data file", ShortName = "test", SortOrder = 1)]
			public string testFile;

			// Token: 0x04000CC0 RID: 3264
			[Argument(4, HelpText = "Trainer to use", ShortName = "tr")]
			public SubComponent<ITrainer, SignatureTrainer> trainer = new SubComponent<ITrainer, SignatureTrainer>("AveragedPerceptron");

			// Token: 0x04000CC1 RID: 3265
			[Argument(4, HelpText = "Scorer to use", NullName = "<Auto>", SortOrder = 101)]
			public SubComponent<IDataScorerTransform, SignatureDataScorer> scorer;

			// Token: 0x04000CC2 RID: 3266
			[Argument(4, HelpText = "Evaluator to use", ShortName = "eval", NullName = "<Auto>", SortOrder = 102)]
			public SubComponent<IMamlEvaluator, SignatureMamlEvaluator> evaluator;

			// Token: 0x04000CC3 RID: 3267
			[Argument(0, HelpText = "Results summary filename", ShortName = "sf")]
			public string summaryFilename;

			// Token: 0x04000CC4 RID: 3268
			[Argument(4, HelpText = "Column to use for features", ShortName = "feat", SortOrder = 2)]
			public string featureColumn = "Features";

			// Token: 0x04000CC5 RID: 3269
			[Argument(4, HelpText = "Column to use for labels", ShortName = "lab", SortOrder = 3)]
			public string labelColumn = "Label";

			// Token: 0x04000CC6 RID: 3270
			[Argument(4, HelpText = "Column to use for example weight", ShortName = "weight", SortOrder = 4)]
			public string weightColumn = "Weight";

			// Token: 0x04000CC7 RID: 3271
			[Argument(4, HelpText = "Column to use for grouping", ShortName = "group", SortOrder = 5)]
			public string groupColumn = "GroupId";

			// Token: 0x04000CC8 RID: 3272
			[Argument(0, HelpText = "Name column name", ShortName = "name", SortOrder = 6)]
			public string nameColumn = "Name";

			// Token: 0x04000CC9 RID: 3273
			[Argument(4, HelpText = "Columns with custom kinds declared through key assignments, e.g., col[Kind]=Name to assign column named 'Name' kind 'Kind'", ShortName = "col", SortOrder = 10)]
			public KeyValuePair<string, string>[] customColumn;

			// Token: 0x04000CCA RID: 3274
			[Argument(4, HelpText = "Normalize option for the feature column", ShortName = "norm")]
			public NormalizeOption normalizeFeatures = NormalizeOption.Auto;

			// Token: 0x04000CCB RID: 3275
			[Argument(0, IsInputFileName = true, HelpText = "The validation data file", ShortName = "valid")]
			public string validationFile;

			// Token: 0x04000CCC RID: 3276
			[Argument(4, HelpText = "Whether we should cache input training data", ShortName = "cache")]
			public bool? cacheData;

			// Token: 0x04000CCD RID: 3277
			[Argument(4, HelpText = "Output calibrator", ShortName = "cali", NullName = "<None>")]
			public SubComponent<ICalibratorTrainer, SignatureCalibrator> calibrator = new SubComponent<ICalibratorTrainer, SignatureCalibrator>("PlattCalibration");

			// Token: 0x04000CCE RID: 3278
			[Argument(4, HelpText = "Number of instances to train the calibrator", ShortName = "numcali")]
			public int maxCalibrationExamples = 1000000000;

			// Token: 0x04000CCF RID: 3279
			[Argument(0, HelpText = "File to save per-instance predictions and metrics to", ShortName = "dout")]
			public string outputDataFile;

			// Token: 0x04000CD0 RID: 3280
			[Argument(4, HelpText = "Whether we should load predictor from input model and use it as the initial model state", ShortName = "cont")]
			public bool continueTrain;
		}

		// Token: 0x020003E4 RID: 996
		private sealed class Impl : DataCommand.ImplBase<TrainTestCommand.Arguments>
		{
			// Token: 0x0600152F RID: 5423 RVA: 0x0007B144 File Offset: 0x00079344
			public Impl(TrainTestCommand.Arguments args, IHostEnvironment env)
				: base("TrainTestCommand", args, env, null)
			{
				Utils.CheckOptionalUserDirectory(args.summaryFilename, "summaryFilename");
				Utils.CheckOptionalUserDirectory(args.outputDataFile, "outputDataFile");
				this._info = TrainUtils.CheckTrainer<SignatureTrainer>(this._host, args.trainer, args.dataFile);
				if (string.IsNullOrWhiteSpace(args.testFile))
				{
					throw Contracts.ExceptUserArg(this._host, "testFile", "Test file must be defined.");
				}
			}

			// Token: 0x06001530 RID: 5424 RVA: 0x0007B1C8 File Offset: 0x000793C8
			public override void Run()
			{
				string text = "TrainTest";
				using (IChannel channel = this._host.Start(text))
				{
					string settings = CmdParser.GetSettings(this._args, new TrainTestCommand.Arguments(), 3);
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

			// Token: 0x06001531 RID: 5425 RVA: 0x0007B264 File Offset: 0x00079464
			protected override void SendTelemetryCore(IPipe<TelemetryMessage> pipe)
			{
				base.SendTelemetryComponent(pipe, this._args.trainer);
				base.SendTelemetryCore(pipe);
			}

			// Token: 0x06001532 RID: 5426 RVA: 0x0007B280 File Offset: 0x00079480
			private void RunCore(IChannel ch, string cmd)
			{
				ch.Trace("Constructing trainer");
				ITrainer trainer = ComponentCatalog.CreateInstance<ITrainer, SignatureTrainer>(this._args.trainer, new object[] { this._host });
				IPredictor predictor = null;
				if (this._args.continueTrain && !TrainUtils.TryLoadPredictor(ch, this._host, this._args.inputModelFile, out predictor))
				{
					ch.Warning("No input model file specified or model file did not contain a predictor. The model state cannot be initialized.");
				}
				ch.Trace("Constructing the training pipeline");
				IDataView dataView = base.CreateLoader("TextLoader");
				ISchema schema = dataView.Schema;
				string text = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "labelColumn", this._args.labelColumn, "Label");
				string text2 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "featureColumn", this._args.featureColumn, "Features");
				string text3 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "groupColumn", this._args.groupColumn, "GroupId");
				string text4 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "weightColumn", this._args.weightColumn, "Weight");
				string text5 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "nameColumn", this._args.nameColumn, "Name");
				TrainUtils.AddNormalizerIfNeeded(this._host, ch, trainer, ref dataView, text2, this._args.normalizeFeatures);
				ch.Trace("Binding columns");
				IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = TrainUtils.CheckAndGenerateCustomColumns(ch, this._args.customColumn);
				RoleMappedData roleMappedData = TrainUtils.CreateExamples(dataView, text, text2, text3, text4, text5, enumerable);
				RoleMappedData roleMappedData2 = null;
				if (!string.IsNullOrWhiteSpace(this._args.validationFile))
				{
					if (!TrainUtils.CanUseValidationData(trainer))
					{
						ch.Warning("Ignoring validationFile: Trainer does not accept validation dataset.");
					}
					else
					{
						ch.Trace("Constructing the validation pipeline");
						IDataView dataView2 = base.CreateRawLoader("TextLoader", this._args.validationFile);
						dataView2 = ApplyTransformUtils.ApplyAllTransformsToData(this._host, dataView, dataView2, null);
						roleMappedData2 = RoleMappedData.Create(dataView2, roleMappedData.Schema.GetColumnRoleNames());
					}
				}
				IPredictor predictor2 = TrainUtils.Train(this._host, ch, roleMappedData, trainer, this._info.LoadNames[0], roleMappedData2, this._args.calibrator, this._args.maxCalibrationExamples, this._args.cacheData, predictor);
				IDataLoader dataLoader;
				using (IFileHandle fileHandle = ((!string.IsNullOrEmpty(this._args.outputModelFile)) ? this._host.CreateOutputFile(this._args.outputModelFile) : this._host.CreateTempFile(".zip", null)))
				{
					TrainUtils.SaveModel(this._host, ch, fileHandle, predictor2, roleMappedData, cmd);
					ch.Trace("Constructing the testing pipeline");
					using (Stream stream = fileHandle.OpenReadStream())
					{
						using (RepositoryReader repositoryReader = RepositoryReader.Open(stream, true))
						{
							dataLoader = base.LoadLoader(repositoryReader, this._args.testFile, true);
						}
					}
				}
				ch.Trace("Scoring and evaluating");
				IDataScorerTransform scorer = ScoreUtils.GetScorer(this._args.scorer, predictor2, dataLoader, text2, text3, enumerable, this._host);
				SubComponent<IMamlEvaluator, SignatureMamlEvaluator> subComponent = this._args.evaluator;
				if (!SubComponentExtensions.IsGood(subComponent))
				{
					subComponent = EvaluateUtils.GetEvaluatorType(ch, scorer.Schema);
				}
				IMamlEvaluator mamlEvaluator = ComponentCatalog.CreateInstance<IMamlEvaluator, SignatureMamlEvaluator>(subComponent, new object[] { this._host });
				RoleMappedData roleMappedData3 = TrainUtils.CreateExamplesOpt(scorer, text, text2, text3, text4, text5, enumerable);
				Dictionary<string, IDataView> dictionary = mamlEvaluator.Evaluate(roleMappedData3);
				MetricWriter.PrintWarnings(ch, dictionary);
				mamlEvaluator.PrintFoldResults(ch, dictionary);
				mamlEvaluator.PrintOverallResults(ch, this._args.summaryFilename, new Dictionary<string, IDataView>[] { dictionary });
				if (!string.IsNullOrWhiteSpace(this._args.outputDataFile))
				{
					IDataTransform perInstanceMetrics = mamlEvaluator.GetPerInstanceMetrics(roleMappedData3);
					RoleMappedData roleMappedData4 = TrainUtils.CreateExamples(perInstanceMetrics, text, null, text3, text4, text5, enumerable);
					IDataView perInstanceDataViewToSave = mamlEvaluator.GetPerInstanceDataViewToSave(roleMappedData4);
					MetricWriter.SavePerInstance(this._host, ch, this._args.outputDataFile, perInstanceDataViewToSave, true, true);
				}
			}

			// Token: 0x04000CD1 RID: 3281
			private readonly ComponentCatalog.LoadableClassInfo _info;
		}
	}
}
