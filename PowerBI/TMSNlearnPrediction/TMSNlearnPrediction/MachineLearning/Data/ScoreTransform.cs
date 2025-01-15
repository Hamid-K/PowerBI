using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200015E RID: 350
	public static class ScoreTransform
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x00025DA4 File Offset: 0x00023FA4
		public static IDataTransform Create(ScoreTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckUserArg(env, !string.IsNullOrWhiteSpace(args.inputModelFile), "inputModelFile", "The input model file is required.");
			IPredictor predictor;
			using (IFileHandle fileHandle = env.OpenInputFile(args.inputModelFile))
			{
				using (Stream stream = fileHandle.OpenReadStream())
				{
					using (RepositoryReader repositoryReader = RepositoryReader.Open(stream, true))
					{
						ModelLoadContext.LoadModel<IPredictor, SignatureLoadModel>(out predictor, repositoryReader, "Predictor", new object[] { env });
					}
				}
			}
			string text = TrainUtils.MatchNameOrDefaultOrNull(env, input.Schema, "featureColumn", args.featureColumn, "Features");
			string text2 = TrainUtils.MatchNameOrDefaultOrNull(env, input.Schema, "groupColumn", args.groupColumn, "GroupId");
			IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = TrainUtils.CheckAndGenerateCustomColumns(env, args.customColumn);
			return ScoreUtils.GetScorer(args.scorer, predictor, input, text, text2, enumerable, env);
		}

		// Token: 0x0400039A RID: 922
		internal const string Summary = "Runs a previously trained predictor on the data.";

		// Token: 0x0200015F RID: 351
		public sealed class Arguments
		{
			// Token: 0x0400039B RID: 923
			[Argument(4, HelpText = "Column to use for features when scorer is not defined", ShortName = "feat", SortOrder = 1, Purpose = "ColumnName")]
			public string featureColumn = "Features";

			// Token: 0x0400039C RID: 924
			[Argument(0, HelpText = "Group column name", ShortName = "group", SortOrder = 100, Purpose = "ColumnName")]
			public string groupColumn = "GroupId";

			// Token: 0x0400039D RID: 925
			[Argument(4, HelpText = "Input columns: Columns with custom kinds declared through key assignments, e.g., col[Kind]=Name to assign column named 'Name' kind 'Kind'", ShortName = "col", SortOrder = 101, Purpose = "ColumnSelector")]
			public KeyValuePair<string, string>[] customColumn;

			// Token: 0x0400039E RID: 926
			[Argument(4, HelpText = "Scorer to use", NullName = "<Auto>")]
			public SubComponent<IDataScorerTransform, SignatureDataScorer> scorer;

			// Token: 0x0400039F RID: 927
			[Argument(0, IsInputFileName = true, HelpText = "Predictor model file used in scoring", ShortName = "in", SortOrder = 2)]
			public string inputModelFile;
		}
	}
}
