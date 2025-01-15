using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200028F RID: 655
	public static class EvaluateTransform
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x000534EC File Offset: 0x000516EC
		public static IDataTransform Create(EvaluateTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IDataTransform perInstanceMetrics;
			using (IChannel channel = env.Register("EvaluateTransform").Start("Create Transform"))
			{
				channel.Trace("Binding columns");
				ISchema schema = input.Schema;
				string text = TrainUtils.MatchNameOrDefaultOrNull(channel, schema, "labelColumn", args.labelColumn, "Label");
				string text2 = TrainUtils.MatchNameOrDefaultOrNull(channel, schema, "groupColumn", args.groupColumn, "GroupId");
				string text3 = TrainUtils.MatchNameOrDefaultOrNull(channel, schema, "weightColumn", args.weightColumn, "Weight");
				IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = TrainUtils.CheckAndGenerateCustomColumns(channel, args.customColumn);
				channel.Trace("Creating evaluator");
				SubComponent<IMamlEvaluator, SignatureMamlEvaluator> subComponent = args.evaluator;
				if (!SubComponentExtensions.IsGood(subComponent))
				{
					subComponent = EvaluateUtils.GetEvaluatorType(channel, input.Schema);
				}
				IMamlEvaluator mamlEvaluator = ComponentCatalog.CreateInstance<IMamlEvaluator, SignatureMamlEvaluator>(subComponent, new object[] { env });
				RoleMappedData roleMappedData = TrainUtils.CreateExamples(input, text, null, text2, text3, null, enumerable);
				perInstanceMetrics = mamlEvaluator.GetPerInstanceMetrics(roleMappedData);
			}
			return perInstanceMetrics;
		}

		// Token: 0x04000841 RID: 2113
		internal const string Summary = "Runs a previously trained predictor on the data.";

		// Token: 0x02000290 RID: 656
		public sealed class Arguments
		{
			// Token: 0x04000842 RID: 2114
			[Argument(4, HelpText = "Column to use for labels", ShortName = "lab", SortOrder = 3)]
			public string labelColumn = "Label";

			// Token: 0x04000843 RID: 2115
			[Argument(4, HelpText = "Column to use for example weight", ShortName = "weight", SortOrder = 4)]
			public string weightColumn = "Weight";

			// Token: 0x04000844 RID: 2116
			[Argument(4, HelpText = "Column to use for grouping", ShortName = "group", SortOrder = 5)]
			public string groupColumn = "GroupId";

			// Token: 0x04000845 RID: 2117
			[Argument(4, HelpText = "Columns with custom kinds declared through key assignments, e.g., col[Kind]=Name to assign column named 'Name' kind 'Kind'", ShortName = "col", SortOrder = 10)]
			public KeyValuePair<string, string>[] customColumn;

			// Token: 0x04000846 RID: 2118
			[Argument(4, HelpText = "Evaluator to use", ShortName = "eval")]
			public SubComponent<IMamlEvaluator, SignatureMamlEvaluator> evaluator;
		}
	}
}
