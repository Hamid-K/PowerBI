using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000390 RID: 912
	public sealed class TestCommand : ICommand
	{
		// Token: 0x060013AD RID: 5037 RVA: 0x0006FC48 File Offset: 0x0006DE48
		public TestCommand(TestCommand.Arguments args, IHostEnvironment env)
		{
			this._impl = new TestCommand.Impl(args, env);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0006FC5D File Offset: 0x0006DE5D
		public void Run()
		{
			this._impl.Run();
		}

		// Token: 0x04000B6A RID: 2922
		internal const string Summary = "Scores and evaluates a data file.";

		// Token: 0x04000B6B RID: 2923
		private readonly TestCommand.Impl _impl;

		// Token: 0x02000391 RID: 913
		public sealed class Arguments : DataCommand.ArgumentsBase
		{
			// Token: 0x04000B6C RID: 2924
			[Argument(0, HelpText = "Column to use for features", ShortName = "feat", SortOrder = 2)]
			public string featureColumn = "Features";

			// Token: 0x04000B6D RID: 2925
			[Argument(4, HelpText = "Column to use for labels", ShortName = "lab", SortOrder = 3)]
			public string labelColumn = "Label";

			// Token: 0x04000B6E RID: 2926
			[Argument(4, HelpText = "Column to use for example weight", ShortName = "weight", SortOrder = 4)]
			public string weightColumn = "Weight";

			// Token: 0x04000B6F RID: 2927
			[Argument(4, HelpText = "Column to use for grouping", ShortName = "group", SortOrder = 5)]
			public string groupColumn = "GroupId";

			// Token: 0x04000B70 RID: 2928
			[Argument(0, HelpText = "Name column name", ShortName = "name", SortOrder = 6)]
			public string nameColumn = "Name";

			// Token: 0x04000B71 RID: 2929
			[Argument(4, HelpText = "Columns with custom kinds declared through key assignments, e.g., col[Kind]=Name to assign column named 'Name' kind 'Kind'", ShortName = "col", SortOrder = 10)]
			public KeyValuePair<string, string>[] customColumn;

			// Token: 0x04000B72 RID: 2930
			[Argument(4, HelpText = "Scorer to use", NullName = "<Auto>", SortOrder = 101)]
			public SubComponent<IDataScorerTransform, SignatureDataScorer> scorer;

			// Token: 0x04000B73 RID: 2931
			[Argument(4, HelpText = "Evaluator to use", ShortName = "eval", NullName = "<Auto>", SortOrder = 102)]
			public SubComponent<IMamlEvaluator, SignatureMamlEvaluator> evaluator;

			// Token: 0x04000B74 RID: 2932
			[Argument(0, HelpText = "Results summary filename", ShortName = "sf")]
			public string summaryFilename;

			// Token: 0x04000B75 RID: 2933
			[Argument(0, HelpText = "File to save per-instance predictions and metrics to", ShortName = "dout")]
			public string outputDataFile;
		}

		// Token: 0x02000392 RID: 914
		private sealed class Impl : DataCommand.ImplBase<TestCommand.Arguments>
		{
			// Token: 0x060013B0 RID: 5040 RVA: 0x0006FCAC File Offset: 0x0006DEAC
			public Impl(TestCommand.Arguments args, IHostEnvironment env)
				: base("TestCommand", args, env, null)
			{
				Contracts.CheckUserArg(this._host, !string.IsNullOrEmpty(this._args.inputModelFile), "inputModelFile", "The input model file is required.");
				Utils.CheckOptionalUserDirectory(args.summaryFilename, "summaryFilename");
				Utils.CheckOptionalUserDirectory(args.outputDataFile, "outputDataFile");
			}

			// Token: 0x060013B1 RID: 5041 RVA: 0x0006FD18 File Offset: 0x0006DF18
			public override void Run()
			{
				string text = "Test";
				using (IChannel channel = this._host.Start(text))
				{
					string settings = CmdParser.GetSettings(this._args, new TestCommand.Arguments(), 3);
					channel.Info("maml.exe {0} {1}", new object[] { text, settings });
					this.SendTelemetry(channel);
					this.RunCore(channel);
					channel.Done();
				}
			}

			// Token: 0x060013B2 RID: 5042 RVA: 0x0006FD98 File Offset: 0x0006DF98
			private void RunCore(IChannel ch)
			{
				using (new TimerScope(ch))
				{
					ch.Trace("Constructing data pipeline");
					IDataLoader dataLoader = base.CreateLoader("TextLoader");
					IPredictor predictor;
					using (IFileHandle fileHandle = this._host.OpenInputFile(this._args.inputModelFile))
					{
						using (Stream stream = fileHandle.OpenReadStream())
						{
							using (RepositoryReader repositoryReader = RepositoryReader.Open(stream, true))
							{
								ch.Trace("Loading predictor");
								ModelLoadContext.LoadModel<IPredictor, SignatureLoadModel>(out predictor, repositoryReader, "Predictor", new object[] { this._host });
							}
						}
					}
					ch.Trace("Binding columns");
					ISchema schema = dataLoader.Schema;
					string text = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "labelColumn", this._args.labelColumn, "Label");
					string text2 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "featureColumn", this._args.featureColumn, "Features");
					string text3 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "groupColumn", this._args.groupColumn, "GroupId");
					string text4 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "weightColumn", this._args.weightColumn, "Weight");
					string text5 = TrainUtils.MatchNameOrDefaultOrNull(ch, schema, "nameColumn", this._args.nameColumn, "Name");
					IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = TrainUtils.CheckAndGenerateCustomColumns(ch, this._args.customColumn);
					ch.Trace("Scoring and evaluating");
					IDataScorerTransform scorer = ScoreUtils.GetScorer(this._args.scorer, predictor, dataLoader, text2, text3, enumerable, this._host);
					SubComponent<IMamlEvaluator, SignatureMamlEvaluator> subComponent = this._args.evaluator;
					if (!SubComponentExtensions.IsGood(subComponent))
					{
						subComponent = EvaluateUtils.GetEvaluatorType(ch, scorer.Schema);
					}
					IMamlEvaluator mamlEvaluator = ComponentCatalog.CreateInstance<IMamlEvaluator, SignatureMamlEvaluator>(subComponent, new object[] { this._host });
					RoleMappedData roleMappedData = TrainUtils.CreateExamples(scorer, text, null, text3, text4, text5, enumerable);
					Dictionary<string, IDataView> dictionary = mamlEvaluator.Evaluate(roleMappedData);
					MetricWriter.PrintWarnings(ch, dictionary);
					mamlEvaluator.PrintFoldResults(ch, dictionary);
					mamlEvaluator.PrintOverallResults(ch, this._args.summaryFilename, new Dictionary<string, IDataView>[] { dictionary });
					if (!string.IsNullOrWhiteSpace(this._args.outputDataFile))
					{
						IDataTransform perInstanceMetrics = mamlEvaluator.GetPerInstanceMetrics(roleMappedData);
						RoleMappedData roleMappedData2 = TrainUtils.CreateExamples(perInstanceMetrics, text, null, text3, text4, text5, enumerable);
						IDataView perInstanceDataViewToSave = mamlEvaluator.GetPerInstanceDataViewToSave(roleMappedData2);
						MetricWriter.SavePerInstance(this._host, ch, this._args.outputDataFile, perInstanceDataViewToSave, true, true);
					}
				}
			}
		}
	}
}
