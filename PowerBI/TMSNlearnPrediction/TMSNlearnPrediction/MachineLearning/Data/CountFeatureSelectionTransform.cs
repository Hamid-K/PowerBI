using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200009D RID: 157
	public static class CountFeatureSelectionTransform
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00011E04 File Offset: 0x00010004
		public static IDataTransform Create(CountFeatureSelectionTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register(CountFeatureSelectionTransform.RegistrationName);
			Contracts.CheckValue<CountFeatureSelectionTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, Utils.Size<string>(args.column) > 0, "column");
			Contracts.CheckUserArg(host, args.count > 0L, "count");
			int[] array2;
			long[][] array = CountFeatureSelectionUtils.Train(host, input, args.column, out array2);
			int num = args.column.Length;
			IDataTransform dataTransform;
			using (IChannel channel = host.Start("Dropping Slots"))
			{
				int[] array3;
				List<DropSlotsTransform.Column> list = CountFeatureSelectionTransform.CreateDropSlotsColumns(args, num, array, out array3);
				if (list.Count <= 0)
				{
					channel.Info("No features are being dropped.");
					dataTransform = NopTransform.CreateIfNeeded(host, input);
				}
				else
				{
					for (int i = 0; i < array3.Length; i++)
					{
						channel.Info("Selected {0} slots out of {1} in column '{2}'", new object[]
						{
							array3[i],
							array2[i],
							args.column[i]
						});
					}
					channel.Info("Total number of slots selected: {0}", new object[] { array3.Sum() });
					DropSlotsTransform.Arguments arguments = new DropSlotsTransform.Arguments();
					arguments.column = list.ToArray();
					channel.Done();
					dataTransform = new DropSlotsTransform(arguments, host, input);
				}
			}
			return dataTransform;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00011F84 File Offset: 0x00010184
		private static List<DropSlotsTransform.Column> CreateDropSlotsColumns(CountFeatureSelectionTransform.Arguments args, int size, long[][] scores, out int[] selectedCount)
		{
			selectedCount = new int[scores.Length];
			List<DropSlotsTransform.Column> list = new List<DropSlotsTransform.Column>();
			for (int i = 0; i < size; i++)
			{
				DropSlotsTransform.Column column = new DropSlotsTransform.Column();
				column.source = args.column[i];
				List<DropSlotsTransform.Range> list2 = new List<DropSlotsTransform.Range>();
				long[] array = scores[i];
				selectedCount[i] = 0;
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] < args.count)
					{
						DropSlotsTransform.Range range = new DropSlotsTransform.Range();
						range.min = j;
						while (j < array.Length && array[j] < args.count)
						{
							j++;
						}
						range.max = new int?(j - 1);
						list2.Add(range);
						if (j < array.Length)
						{
							selectedCount[i]++;
						}
					}
					else
					{
						selectedCount[i]++;
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

		// Token: 0x0400015C RID: 348
		internal const string Summary = "Selects the slots for which the count of non-default values is greater than or equal to a threshold.";

		// Token: 0x0400015D RID: 349
		internal static string RegistrationName = "CountFeatureSelectionTransform";

		// Token: 0x0200009E RID: 158
		public sealed class Arguments
		{
			// Token: 0x0400015E RID: 350
			[Argument(4, HelpText = "Columns to use for feature selection", ShortName = "col", SortOrder = 1)]
			public string[] column;

			// Token: 0x0400015F RID: 351
			[Argument(1, HelpText = "If the count of non-default values for a slot is greater than or equal to this threshold, the slot is preserved", ShortName = "c", SortOrder = 1)]
			public long count = 1L;
		}
	}
}
