using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003DE RID: 990
	public sealed class TrainCommand : ICommand
	{
		// Token: 0x06001512 RID: 5394 RVA: 0x00079F89 File Offset: 0x00078189
		public TrainCommand(TrainCommand.Arguments args, IHostEnvironment env)
		{
			this._impl = new TrainCommand.Impl(args, env);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00079F9E File Offset: 0x0007819E
		public void Run()
		{
			this._impl.Run();
		}

		// Token: 0x04000CA5 RID: 3237
		internal const string Summary = "Trains a predictor.";

		// Token: 0x04000CA6 RID: 3238
		private readonly TrainCommand.Impl _impl;

		// Token: 0x020003DF RID: 991
		public sealed class Arguments : DataCommand.ArgumentsBase
		{
			// Token: 0x04000CA7 RID: 3239
			[Argument(4, HelpText = "Column to use for features", ShortName = "feat", SortOrder = 2)]
			public string featureColumn = "Features";

			// Token: 0x04000CA8 RID: 3240
			[Argument(4, HelpText = "Column to use for labels", ShortName = "lab", SortOrder = 3)]
			public string labelColumn = "Label";

			// Token: 0x04000CA9 RID: 3241
			[Argument(4, HelpText = "Column to use for example weight", ShortName = "weight", SortOrder = 4)]
			public string weightColumn = "Weight";

			// Token: 0x04000CAA RID: 3242
			[Argument(4, HelpText = "Column to use for grouping", ShortName = "group", SortOrder = 5)]
			public string groupColumn = "GroupId";

			// Token: 0x04000CAB RID: 3243
			[Argument(0, HelpText = "Name column name", ShortName = "name", SortOrder = 6)]
			public string nameColumn = "Name";

			// Token: 0x04000CAC RID: 3244
			[Argument(4, HelpText = "Columns with custom kinds declared through key assignments, e.g., col[Kind]=Name to assign column named 'Name' kind 'Kind'", ShortName = "col", SortOrder = 10)]
			public KeyValuePair<string, string>[] customColumn;

			// Token: 0x04000CAD RID: 3245
			[Argument(4, HelpText = "Normalize option for the feature column", ShortName = "norm")]
			public NormalizeOption normalizeFeatures = NormalizeOption.Auto;

			// Token: 0x04000CAE RID: 3246
			[Argument(4, HelpText = "Trainer to use", ShortName = "tr")]
			public SubComponent<ITrainer, SignatureTrainer> trainer = new SubComponent<ITrainer, SignatureTrainer>("AveragedPerceptron");

			// Token: 0x04000CAF RID: 3247
			[Argument(0, IsInputFileName = true, HelpText = "The validation data file", ShortName = "valid")]
			public string validationFile;

			// Token: 0x04000CB0 RID: 3248
			[Argument(4, HelpText = "Whether we should cache input training data", ShortName = "cache")]
			public bool? cacheData;

			// Token: 0x04000CB1 RID: 3249
			[Argument(4, HelpText = "Output calibrator", ShortName = "cali", NullName = "<None>")]
			public SubComponent<ICalibratorTrainer, SignatureCalibrator> calibrator = new SubComponent<ICalibratorTrainer, SignatureCalibrator>("PlattCalibration");

			// Token: 0x04000CB2 RID: 3250
			[Argument(4, HelpText = "Number of instances to train the calibrator", ShortName = "numcali")]
			public int maxCalibrationExamples = 1000000000;

			// Token: 0x04000CB3 RID: 3251
			[Argument(4, HelpText = "Whether we should load predictor from input model and use it as the initial model state", ShortName = "cont")]
			public bool continueTrain;
		}

		// Token: 0x020003E0 RID: 992
		private sealed class Impl : DataCommand.ImplBase<TrainCommand.Arguments>
		{
			// Token: 0x06001515 RID: 5397 RVA: 0x0007A028 File Offset: 0x00078228
			public Impl(TrainCommand.Arguments args, IHostEnvironment env)
				: base("TrainCommand", args, env, null)
			{
				Contracts.CheckUserArg(this._host, !string.IsNullOrWhiteSpace(args.outputModelFile), "outputModelFile", "outputModelFile required");
				this._info = TrainUtils.CheckTrainer<SignatureTrainer>(this._host, args.trainer, args.dataFile);
				this._trainer = args.trainer;
				this._labelColumn = args.labelColumn;
				this._featureColumn = args.featureColumn;
				this._groupColumn = args.groupColumn;
				this._weightColumn = args.weightColumn;
				this._nameColumn = args.nameColumn;
			}

			// Token: 0x06001516 RID: 5398 RVA: 0x0007A0D4 File Offset: 0x000782D4
			public override void Run()
			{
				string text = "Train";
				using (IChannel channel = this._host.Start(text))
				{
					string settings = CmdParser.GetSettings(this._args, new TrainCommand.Arguments(), 3);
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

			// Token: 0x06001517 RID: 5399 RVA: 0x0007A170 File Offset: 0x00078370
			protected override void SendTelemetryCore(IPipe<TelemetryMessage> pipe)
			{
				base.SendTelemetryComponent(pipe, this._trainer);
				base.SendTelemetryCore(pipe);
			}

			// Token: 0x06001518 RID: 5400 RVA: 0x0007A188 File Offset: 0x00078388
			private void RunCore(IChannel ch, string cmd)
			{
				ch.Trace("Constructing trainer");
				ITrainer trainer = ComponentCatalog.CreateInstance<ITrainer, SignatureTrainer>(this._trainer, new object[] { this._host });
				IPredictor predictor = null;
				if (this._args.continueTrain && !TrainUtils.TryLoadPredictor(ch, this._host, this._args.inputModelFile, out predictor))
				{
					ch.Warning("No input model file specified or model file did not contain a predictor. The model state cannot be initialized.");
				}
				ch.Trace("Constructing data pipeline");
				IDataView dataView = base.CreateLoader("TextLoader");
				ISchema schema = dataView.Schema;
				string text = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "labelColumn", this._labelColumn, "Label");
				string text2 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "featureColumn", this._featureColumn, "Features");
				string text3 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "groupColumn", this._groupColumn, "GroupId");
				string text4 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "weightColumn", this._weightColumn, "Weight");
				string text5 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "nameColumn", this._nameColumn, "Name");
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
				using (IFileHandle fileHandle = this._host.CreateOutputFile(this._args.outputModelFile))
				{
					TrainUtils.SaveModel(this._host, ch, fileHandle, predictor2, roleMappedData, cmd);
				}
			}

			// Token: 0x04000CB4 RID: 3252
			private readonly ComponentCatalog.LoadableClassInfo _info;

			// Token: 0x04000CB5 RID: 3253
			private readonly SubComponent<ITrainer, SignatureTrainer> _trainer;

			// Token: 0x04000CB6 RID: 3254
			private readonly string _labelColumn;

			// Token: 0x04000CB7 RID: 3255
			private readonly string _featureColumn;

			// Token: 0x04000CB8 RID: 3256
			private readonly string _groupColumn;

			// Token: 0x04000CB9 RID: 3257
			private readonly string _weightColumn;

			// Token: 0x04000CBA RID: 3258
			private readonly string _nameColumn;
		}
	}
}
