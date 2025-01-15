using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000F2 RID: 242
	public static class LearnerFeatureSelectionTransform
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x0001AE34 File Offset: 0x00019034
		public static IDataTransform Create(LearnerFeatureSelectionTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register(LearnerFeatureSelectionTransform.RegistrationName);
			Contracts.CheckValue<LearnerFeatureSelectionTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			IExceptionContext exceptionContext = host;
			float? threshold = args.threshold;
			Contracts.CheckUserArg(exceptionContext, (threshold.GetValueOrDefault() >= 0f && threshold != null && args.numSlotsToKeep == null) || (args.threshold == null && args.numSlotsToKeep > 0), "Threshold or number of features to keep must be specified (but not both).");
			VBuffer<float> vbuffer = default(VBuffer<float>);
			LearnerFeatureSelectionTransform.TrainCore(host, input, args, ref vbuffer);
			IDataTransform dataTransform;
			using (IChannel channel = host.Start("Dropping Slots"))
			{
				int num;
				DropSlotsTransform.Column column = LearnerFeatureSelectionTransform.CreateDropSlotsColumn(args, ref vbuffer, out num);
				if (column == null)
				{
					channel.Info("No features are being dropped.");
					dataTransform = NopTransform.CreateIfNeeded(host, input);
				}
				else
				{
					channel.Info("Selected {0} slots out of {1} in column '{2}'", new object[] { num, vbuffer.Length, args.featureColumn });
					DropSlotsTransform.Arguments arguments = new DropSlotsTransform.Arguments();
					arguments.column = new DropSlotsTransform.Column[] { column };
					channel.Done();
					dataTransform = new DropSlotsTransform(arguments, host, input);
				}
			}
			return dataTransform;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001AFA4 File Offset: 0x000191A4
		private static DropSlotsTransform.Column CreateDropSlotsColumn(LearnerFeatureSelectionTransform.Arguments args, ref VBuffer<float> scores, out int selectedCount)
		{
			DropSlotsTransform.Column column = new DropSlotsTransform.Column();
			column.source = args.featureColumn;
			selectedCount = 0;
			if (scores.Count == 0)
			{
				DropSlotsTransform.Range range = new DropSlotsTransform.Range();
				column.slots = new DropSlotsTransform.Range[] { range };
				return column;
			}
			float num;
			int num2;
			if (args.threshold != null)
			{
				num = args.threshold.Value;
				num2 = ((num > 0f) ? int.MaxValue : 0);
			}
			else
			{
				num = LearnerFeatureSelectionTransform.ComputeThreshold(scores.Values, scores.Count, args.numSlotsToKeep.Value, out num2);
			}
			List<DropSlotsTransform.Range> list = new List<DropSlotsTransform.Range>();
			for (int i = 0; i < scores.Count; i++)
			{
				float num3 = Math.Abs(scores.Values[i]);
				if (num3 > num)
				{
					selectedCount++;
				}
				else if (num3 == num && num2 > 0)
				{
					num2--;
					selectedCount++;
				}
				else
				{
					DropSlotsTransform.Range range2 = new DropSlotsTransform.Range();
					range2.min = i;
					while (++i < scores.Count)
					{
						num3 = Math.Abs(scores.Values[i]);
						if (num3 > num)
						{
							selectedCount++;
							break;
						}
						if (num3 == num && num2 > 0)
						{
							num2--;
							selectedCount++;
							break;
						}
					}
					range2.max = new int?(i - 1);
					list.Add(range2);
				}
			}
			if (!scores.IsDense)
			{
				int j = 0;
				int count = list.Count;
				for (int k = 0; k < count; k++)
				{
					DropSlotsTransform.Range range3 = list[k];
					int min = range3.min;
					int value = range3.max.Value;
					range3.min = ((min == 0) ? 0 : (scores.Indices[min - 1] + 1));
					range3.max = new int?((value == scores.Count - 1) ? (scores.Length - 1) : (scores.Indices[value + 1] - 1));
					while (j < min)
					{
						int num4 = ((j == 0) ? 0 : (scores.Indices[j - 1] + 1));
						int num5 = scores.Indices[j] - 1;
						if (num4 <= num5)
						{
							list.Add(new DropSlotsTransform.Range
							{
								min = num4,
								max = new int?(num5)
							});
						}
						j++;
					}
					j = value;
				}
				while (j <= scores.Count)
				{
					int num6 = ((j == 0) ? 0 : (scores.Indices[j - 1] + 1));
					int num7 = ((j == scores.Count) ? (scores.Length - 1) : (scores.Indices[j] - 1));
					if (num6 <= num7)
					{
						list.Add(new DropSlotsTransform.Range
						{
							min = num6,
							max = new int?(num7)
						});
					}
					j++;
				}
				list.Add(new DropSlotsTransform.Range
				{
					min = scores.Length
				});
			}
			if (list.Count > 0)
			{
				column.slots = list.ToArray();
				return column;
			}
			return null;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001B2A8 File Offset: 0x000194A8
		private static float ComputeThreshold(float[] scores, int count, int topk, out int tiedScoresToKeep)
		{
			Heap<float> heap = new Heap<float>((float f1, float f2) => f1 > f2, topk);
			for (int i = 0; i < count; i++)
			{
				float num = Math.Abs(scores[i]);
				if (!float.IsNaN(num))
				{
					if (heap.Count < topk)
					{
						heap.Add(num);
					}
					else if (heap.Top < num)
					{
						heap.Pop();
						heap.Add(num);
					}
				}
			}
			float top = heap.Top;
			tiedScoresToKeep = 0;
			if (top == 0f)
			{
				return top;
			}
			while (heap.Count > 0)
			{
				float num2 = heap.Pop();
				if (num2 > top)
				{
					break;
				}
				tiedScoresToKeep++;
			}
			return top;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001B350 File Offset: 0x00019550
		private static void TrainCore(IHost host, IDataView input, LearnerFeatureSelectionTransform.Arguments args, ref VBuffer<float> scores)
		{
			using (IChannel channel = host.Start("Train"))
			{
				channel.Trace("Constructing trainer");
				ITrainer trainer = ComponentCatalog.CreateInstance<ITrainer<RoleMappedData, IPredictorWithFeatureWeights<float>>, SignatureFeatureScorerTrainer>(args.filter, new object[] { host });
				IDataView dataView = input;
				ISchema schema = dataView.Schema;
				string text = TrainUtils.MatchNameOrDefaultOrNull(channel, schema, "labelColumn", args.labelColumn, "Label");
				string text2 = TrainUtils.MatchNameOrDefaultOrNull(channel, schema, "featureColumn", args.featureColumn, "Features");
				string text3 = TrainUtils.MatchNameOrDefaultOrNull(channel, schema, "groupColumn", args.groupColumn, "GroupId");
				string text4 = TrainUtils.MatchNameOrDefaultOrNull(channel, schema, "weightColumn", args.weightColumn, "Weight");
				string text5 = TrainUtils.MatchNameOrDefaultOrNull(channel, schema, "nameColumn", args.nameColumn, "Name");
				TrainUtils.AddNormalizerIfNeeded(host, channel, trainer, ref dataView, text2, args.normalizeFeatures);
				channel.Trace("Binding columns");
				IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = TrainUtils.CheckAndGenerateCustomColumns(channel, args.customColumn);
				RoleMappedData roleMappedData = TrainUtils.CreateExamples(dataView, text, text2, text3, text4, text5, enumerable);
				IPredictor predictor = TrainUtils.Train(host, channel, roleMappedData, trainer, args.filter.Kind, null, null, 0, args.cacheData, null);
				IPredictorWithFeatureWeights<float> predictorWithFeatureWeights = predictor as IPredictorWithFeatureWeights<float>;
				predictorWithFeatureWeights.GetFeatureWeights(ref scores);
				channel.Done();
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001B4B8 File Offset: 0x000196B8
		public static void Train(IHostEnvironment env, IDataView input, LearnerFeatureSelectionTransform.Arguments args, ref VBuffer<float> scores)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register(LearnerFeatureSelectionTransform.RegistrationName);
			Contracts.CheckValue<LearnerFeatureSelectionTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			IExceptionContext exceptionContext = host;
			float? threshold = args.threshold;
			Contracts.CheckUserArg(exceptionContext, (threshold.GetValueOrDefault() >= 0f && threshold != null && args.numSlotsToKeep == null) || (args.threshold == null && args.numSlotsToKeep > 0), "Threshold or number of features to keep must be specified (but not both).");
			LearnerFeatureSelectionTransform.TrainCore(host, input, args, ref scores);
		}

		// Token: 0x04000254 RID: 596
		internal const string Summary = "Selects the slots for which the absolute value of the corresponding weight in a linear learner is greater than a threshold.";

		// Token: 0x04000255 RID: 597
		internal static string RegistrationName = "LearnerFeatureSelectionTransform";

		// Token: 0x020000F3 RID: 243
		public sealed class Arguments
		{
			// Token: 0x04000257 RID: 599
			[Argument(4, HelpText = "If the corresponding absolute value of the weight for a slot is greater than this threshold, the slot is preserved", ShortName = "ft", SortOrder = 2)]
			public float? threshold;

			// Token: 0x04000258 RID: 600
			[Argument(0, HelpText = "The number of slots to preserve", ShortName = "topk", SortOrder = 1)]
			public int? numSlotsToKeep;

			// Token: 0x04000259 RID: 601
			[Argument(4, HelpText = "Filter", ShortName = "f", SortOrder = 1)]
			public SubComponent<ITrainer<RoleMappedData, IPredictorWithFeatureWeights<float>>, SignatureFeatureScorerTrainer> filter = new SubComponent<ITrainer<RoleMappedData, IPredictorWithFeatureWeights<float>>, SignatureFeatureScorerTrainer>("SDCA");

			// Token: 0x0400025A RID: 602
			[Argument(4, HelpText = "Column to use for features", ShortName = "feat,col", SortOrder = 3, Purpose = "ColumnName")]
			public string featureColumn = "Features";

			// Token: 0x0400025B RID: 603
			[Argument(4, HelpText = "Column to use for labels", ShortName = "lab", SortOrder = 4, Purpose = "ColumnName")]
			public string labelColumn = "Label";

			// Token: 0x0400025C RID: 604
			[Argument(4, HelpText = "Column to use for example weight", ShortName = "weight", SortOrder = 5, Purpose = "ColumnName")]
			public string weightColumn = "Weight";

			// Token: 0x0400025D RID: 605
			[Argument(4, HelpText = "Column to use for grouping", ShortName = "group", Purpose = "ColumnName")]
			public string groupColumn = "GroupId";

			// Token: 0x0400025E RID: 606
			[Argument(0, HelpText = "Name column name", ShortName = "name", Purpose = "ColumnName")]
			public string nameColumn = "Name";

			// Token: 0x0400025F RID: 607
			[Argument(4, HelpText = "Columns with custom kinds declared through key assignments, e.g., col[Kind]=Name to assign column named 'Name' kind 'Kind'")]
			public KeyValuePair<string, string>[] customColumn;

			// Token: 0x04000260 RID: 608
			[Argument(4, HelpText = "Normalize option for the feature column", ShortName = "norm")]
			public NormalizeOption normalizeFeatures = NormalizeOption.Auto;

			// Token: 0x04000261 RID: 609
			[Argument(4, HelpText = "Whether we should cache input training data", ShortName = "cache")]
			public bool? cacheData;
		}
	}
}
