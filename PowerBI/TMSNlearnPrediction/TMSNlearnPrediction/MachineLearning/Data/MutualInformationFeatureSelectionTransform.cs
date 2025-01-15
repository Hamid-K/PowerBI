using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000119 RID: 281
	public static class MutualInformationFeatureSelectionTransform
	{
		// Token: 0x060005A8 RID: 1448 RVA: 0x0001EBDC File Offset: 0x0001CDDC
		public static IDataTransform Create(MutualInformationFeatureSelectionTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register(MutualInformationFeatureSelectionTransform.RegistrationName);
			Contracts.CheckValue<MutualInformationFeatureSelectionTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, Utils.Size<string>(args.column) > 0, "column");
			Contracts.CheckUserArg(host, args.numSlotsToKeep > 0, "count");
			Contracts.CheckNonWhiteSpace(host, args.labelColumn, "labelColumn");
			Contracts.Check(host, args.numBins > 1, "numBins must be greater than 1.");
			IDataTransform dataTransform;
			using (IChannel channel = host.Start("Selecting Slots"))
			{
				channel.Info("Computing mutual information");
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				HashSet<string> hashSet = new HashSet<string>();
				foreach (string text in args.column)
				{
					if (!hashSet.Add(text))
					{
						channel.Warning("Column '{0}' specified multiple time.", new object[] { text });
					}
				}
				string[] array = hashSet.ToArray<string>();
				int[] array2 = new int[array.Length];
				float[][] array3 = MutualInformationFeatureSelectionUtils.TrainCore(host, input, args.labelColumn, array, args.numBins, array2);
				stopwatch.Stop();
				channel.Info("Finished mutual information computation in {0}", new object[] { stopwatch.Elapsed });
				channel.Info("Selecting features to drop");
				int num2;
				float num = MutualInformationFeatureSelectionTransform.ComputeThreshold(array3, args.numSlotsToKeep, out num2);
				int[] array4;
				List<DropSlotsTransform.Column> list = MutualInformationFeatureSelectionTransform.CreateDropSlotsColumns(array, array.Length, array3, num, num2, out array4);
				if (list.Count <= 0)
				{
					channel.Info("No features are being dropped.");
					dataTransform = NopTransform.CreateIfNeeded(host, input);
				}
				else
				{
					for (int j = 0; j < array4.Length; j++)
					{
						channel.Info("Selected {0} slots out of {1} in column '{2}'", new object[]
						{
							array4[j],
							array2[j],
							array[j]
						});
					}
					channel.Info("Total number of slots selected: {0}", new object[] { array4.Sum() });
					DropSlotsTransform dropSlotsTransform = new DropSlotsTransform(new DropSlotsTransform.Arguments
					{
						column = list.ToArray()
					}, host, input);
					channel.Done();
					dataTransform = dropSlotsTransform;
				}
			}
			return dataTransform;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001EE54 File Offset: 0x0001D054
		private static float ComputeThreshold(float[][] scores, int topk, out int tiedScoresToKeep)
		{
			Heap<float> heap = new Heap<float>((float f1, float f2) => f1 > f2, topk);
			for (int i = 0; i < scores.Length; i++)
			{
				for (int j = 0; j < scores[i].Length; j++)
				{
					float num = scores[i][j];
					if (heap.Count < topk)
					{
						if (num > 0f)
						{
							heap.Add(num);
						}
					}
					else if (heap.Top < num)
					{
						heap.Pop();
						heap.Add(num);
					}
				}
			}
			float num2 = ((heap.Count < topk) ? 0f : heap.Top);
			tiedScoresToKeep = 0;
			if (num2 == 0f)
			{
				return num2;
			}
			while (heap.Count > 0)
			{
				float num3 = heap.Pop();
				if (num3 > num2)
				{
					break;
				}
				tiedScoresToKeep++;
			}
			return num2;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001EF20 File Offset: 0x0001D120
		private static List<DropSlotsTransform.Column> CreateDropSlotsColumns(string[] cols, int size, float[][] scores, float threshold, int tiedScoresToKeep, out int[] selectedCount)
		{
			List<DropSlotsTransform.Column> list = new List<DropSlotsTransform.Column>();
			selectedCount = new int[scores.Length];
			for (int i = 0; i < size; i++)
			{
				DropSlotsTransform.Column column = new DropSlotsTransform.Column();
				column.source = cols[i];
				List<DropSlotsTransform.Range> list2 = new List<DropSlotsTransform.Range>();
				float[] array = scores[i];
				selectedCount[i] = 0;
				for (int j = 0; j < array.Length; j++)
				{
					float num = array[j];
					if (num > threshold)
					{
						selectedCount[i]++;
					}
					else if (num == threshold && tiedScoresToKeep > 0)
					{
						tiedScoresToKeep--;
						selectedCount[i]++;
					}
					else
					{
						DropSlotsTransform.Range range = new DropSlotsTransform.Range();
						range.min = j;
						while (++j < array.Length)
						{
							num = array[j];
							if (num > threshold)
							{
								selectedCount[i]++;
								break;
							}
							if (num == threshold && tiedScoresToKeep > 0)
							{
								tiedScoresToKeep--;
								selectedCount[i]++;
								break;
							}
						}
						range.max = new int?(j - 1);
						list2.Add(range);
					}
				}
				if (list2.Count > 0)
				{
					column.slots = list2.ToArray();
					list.Add(column);
				}
			}
			return list;
		}

		// Token: 0x040002D4 RID: 724
		internal const string Summary = "Selects the top k slots ordered by their mutual information with the label column.";

		// Token: 0x040002D5 RID: 725
		internal static string RegistrationName = "MutualInformationFeatureSelectionTransform";

		// Token: 0x0200011A RID: 282
		public sealed class Arguments
		{
			// Token: 0x040002D7 RID: 727
			[Argument(4, HelpText = "Columns to use for feature selection", ShortName = "col", SortOrder = 1)]
			public string[] column;

			// Token: 0x040002D8 RID: 728
			[Argument(4, HelpText = "Column to use for labels", ShortName = "lab", SortOrder = 4, Purpose = "ColumnName")]
			public string labelColumn = "Label";

			// Token: 0x040002D9 RID: 729
			[Argument(0, HelpText = "The number of slots to preserve", ShortName = "topk", SortOrder = 1)]
			public int numSlotsToKeep = 1000;

			// Token: 0x040002DA RID: 730
			[Argument(0, HelpText = "Max number of bins for R4/R8 columns, power of 2 recommended", ShortName = "bins")]
			public int numBins = 256;
		}
	}
}
