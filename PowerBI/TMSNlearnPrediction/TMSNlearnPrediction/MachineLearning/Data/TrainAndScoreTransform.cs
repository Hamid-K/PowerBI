using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000160 RID: 352
	public static class TrainAndScoreTransform
	{
		// Token: 0x060006FF RID: 1791 RVA: 0x00025EDC File Offset: 0x000240DC
		public static IDataTransform Create(TrainAndScoreTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<TrainAndScoreTransform.Arguments>(env, args, "args");
			Contracts.CheckValue<IDataView>(env, input, "input");
			Contracts.CheckUserArg(env, SubComponentExtensions.IsGood(args.trainer), "trainer", "Trainer cannot be null. If your model is already trained, please use ScoreTransform instead.");
			IHost host = env.Register("TrainAndScoreTransform");
			IDataTransform scorer;
			using (IChannel channel = host.Start("Train"))
			{
				channel.Trace("Constructing trainer");
				ITrainer trainer = ComponentCatalog.CreateInstance<ITrainer, SignatureTrainer>(args.trainer, new object[] { host });
				IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = TrainUtils.CheckAndGenerateCustomColumns(env, args.customColumn);
				string text;
				string text2;
				RoleMappedData roleMappedData = TrainAndScoreTransform.CreateDataFromArgs<SignatureTrainer>(channel, input, args, out text, out text2);
				IPredictor predictor = TrainUtils.Train(host, channel, roleMappedData, trainer, args.trainer.Kind, null, args.calibrator, args.maxCalibrationExamples, null, null);
				channel.Done();
				scorer = ScoreUtils.GetScorer(args.scorer, predictor, input, text, text2, enumerable, env);
			}
			return scorer;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00025FEC File Offset: 0x000241EC
		public static RoleMappedData CreateDataFromArgs<TSigTrainer>(IExceptionContext ectx, IDataView input, TrainAndScoreTransform.ArgumentsBase<TSigTrainer> args)
		{
			string text;
			string text2;
			return TrainAndScoreTransform.CreateDataFromArgs<TSigTrainer>(ectx, input, args, out text, out text2);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00026008 File Offset: 0x00024208
		private static RoleMappedData CreateDataFromArgs<TSigTrainer>(IExceptionContext ectx, IDataView input, TrainAndScoreTransform.ArgumentsBase<TSigTrainer> args, out string feat, out string group)
		{
			ISchema schema = input.Schema;
			feat = TrainUtils.MatchNameOrDefaultOrNull(ectx, schema, "featureColumn", args.featureColumn, "Features");
			string text = TrainUtils.MatchNameOrDefaultOrNull(ectx, schema, "labelColumn", args.labelColumn, "Label");
			group = TrainUtils.MatchNameOrDefaultOrNull(ectx, schema, "groupColumn", args.groupColumn, "GroupId");
			string text2 = TrainUtils.MatchNameOrDefaultOrNull(ectx, schema, "weightColumn", args.weightColumn, "Weight");
			string text3 = TrainUtils.MatchNameOrDefaultOrNull(ectx, schema, "nameColumn", args.nameColumn, "Name");
			IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = TrainUtils.CheckAndGenerateCustomColumns(ectx, args.customColumn);
			return TrainUtils.CreateExamples(input, text, feat, group, text2, text3, enumerable);
		}

		// Token: 0x040003A0 RID: 928
		internal const string Summary = "Trains a predictor, or loads it from a file, and runs it on the data.";

		// Token: 0x02000161 RID: 353
		public abstract class ArgumentsBase
		{
			// Token: 0x06000702 RID: 1794 RVA: 0x000260B8 File Offset: 0x000242B8
			public void CopyTo(TrainAndScoreTransform.ArgumentsBase other)
			{
				other.featureColumn = this.featureColumn;
				other.labelColumn = this.labelColumn;
				other.groupColumn = this.groupColumn;
				other.weightColumn = this.weightColumn;
				other.nameColumn = this.nameColumn;
				other.customColumn = this.customColumn;
			}

			// Token: 0x040003A1 RID: 929
			[Argument(4, HelpText = "Column to use for features when scorer is not defined", ShortName = "feat", SortOrder = 102, Purpose = "ColumnName")]
			public string featureColumn = "Features";

			// Token: 0x040003A2 RID: 930
			[Argument(4, HelpText = "Column to use for labels", ShortName = "lab", SortOrder = 103, Purpose = "ColumnName")]
			public string labelColumn = "Label";

			// Token: 0x040003A3 RID: 931
			[Argument(4, HelpText = "Column to use for grouping", ShortName = "group", SortOrder = 105, Purpose = "ColumnName")]
			public string groupColumn = "GroupId";

			// Token: 0x040003A4 RID: 932
			[Argument(4, HelpText = "Column to use for example weight", ShortName = "weight", SortOrder = 104, Purpose = "ColumnName")]
			public string weightColumn = "Weight";

			// Token: 0x040003A5 RID: 933
			[Argument(0, HelpText = "Name column name", ShortName = "name", SortOrder = 106, Purpose = "ColumnName")]
			public string nameColumn = "Name";

			// Token: 0x040003A6 RID: 934
			[Argument(4, HelpText = "Input columns: Columns with custom kinds declared through key assignments, e.g., col[Kind]=Name to assign column named 'Name' kind 'Kind'", ShortName = "col", SortOrder = 110, Purpose = "ColumnSelector")]
			public KeyValuePair<string, string>[] customColumn;
		}

		// Token: 0x02000162 RID: 354
		public abstract class ArgumentsBase<TSigTrainer> : TrainAndScoreTransform.ArgumentsBase
		{
			// Token: 0x040003A7 RID: 935
			[Argument(4, HelpText = "Trainer to use", ShortName = "tr", NullName = "<None>", SortOrder = 1)]
			public SubComponent<ITrainer, TSigTrainer> trainer;
		}

		// Token: 0x02000163 RID: 355
		public sealed class Arguments : TrainAndScoreTransform.ArgumentsBase<SignatureTrainer>
		{
			// Token: 0x040003A8 RID: 936
			[Argument(4, HelpText = "Output calibrator", ShortName = "cali", NullName = "<None>")]
			public SubComponent<ICalibratorTrainer, SignatureCalibrator> calibrator = new SubComponent<ICalibratorTrainer, SignatureCalibrator>("PlattCalibration");

			// Token: 0x040003A9 RID: 937
			[Argument(4, HelpText = "Number of instances to train the calibrator", ShortName = "numcali")]
			public int maxCalibrationExamples = 1000000000;

			// Token: 0x040003AA RID: 938
			[Argument(4, HelpText = "Scorer to use", NullName = "<Auto>")]
			public SubComponent<IDataScorerTransform, SignatureDataScorer> scorer;
		}
	}
}
