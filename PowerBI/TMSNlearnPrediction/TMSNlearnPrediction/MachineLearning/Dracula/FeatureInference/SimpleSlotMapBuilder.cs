using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Dracula.FeatureInference
{
	// Token: 0x0200041B RID: 1051
	public sealed class SimpleSlotMapBuilder : DraculaSlotMapBuilderBase
	{
		// Token: 0x060015E9 RID: 5609 RVA: 0x0007FBF0 File Offset: 0x0007DDF0
		public SimpleSlotMapBuilder(SimpleSlotMapBuilder.Arguments args, IHostEnvironment env, IDataView input)
			: base(args, env, input)
		{
			IHost host = env.Register("SlotMapBuilder");
			Contracts.CheckUserArg(host, args.significanceThreshold > 0, "significanceThreshold", "Significance threshold must be positive");
			using (IChannel channel = host.Start("Feature inference"))
			{
				channel.Info("Initializing count tables");
				HashJoinTransform.Arguments arguments = new HashJoinTransform.Arguments
				{
					column = new HashJoinTransform.Column[]
					{
						new HashJoinTransform.Column
						{
							name = args.column,
							source = args.column
						}
					},
					join = false
				};
				CountTableTransform.Arguments arguments2 = new CountTableTransform.Arguments
				{
					column = new CountTableTransform.Column[]
					{
						new CountTableTransform.Column
						{
							name = args.column,
							source = args.column
						}
					},
					labelColumn = args.labelColumn,
					countTable = new SubComponent<ICountTableBuilder, SignatureCountTableBuilder>("CMSketch", new string[] { "d=1 w=1048576" })
				};
				channel.Info("Counting");
				HashJoinTransform hashJoinTransform = new HashJoinTransform(arguments, env, input);
				CountTableTransform countTableTransform = new CountTableTransform(arguments2, env, hashJoinTransform);
				channel.Info("Inspecting count tables");
				FeatureInferenceSettings featureInferenceSettings = new FeatureInferenceSettings(countTableTransform.GetLabelCardinality(), (float)args.significanceThreshold);
				List<SlotSetStats> list = new List<SlotSetStats>();
				for (int i = 0; i < this._srcVectorSize; i++)
				{
					CMCountTable cmcountTable = countTableTransform.GetCountTable(0, i) as CMCountTable;
					SlotSetStats slotSetStats = SlotSetStats.CreateFromSingleColumn(featureInferenceSettings, i, cmcountTable);
					list.Add(slotSetStats);
				}
				channel.Info("Computing pairs");
				List<SlotSetStats> list2 = new List<SlotSetStats>();
				for (int j = 0; j < this._srcVectorSize - 1; j++)
				{
					for (int k = j + 1; k < this._srcVectorSize; k++)
					{
						list2.Add(SlotSetStats.CreateCombined(list[j], list[k]));
					}
				}
				if (list2.Count > args.featureCount - this._srcVectorSize)
				{
					channel.Info("Filtering out extra columns");
					list2 = list2.OrderByDescending((SlotSetStats s) => s.GetEntropyGain()).Take(args.featureCount - this._srcVectorSize).ToList<SlotSetStats>();
				}
				list.AddRange(list2);
				this._slotMap = this.GenerateSlotMap(list);
				channel.Done();
			}
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0007FE91 File Offset: 0x0007E091
		private int[][] GenerateSlotMap(IEnumerable<SlotSetStats> stats)
		{
			return stats.Select((SlotSetStats s) => s.IncludedColumns.ToArray<int>()).ToArray<int[]>();
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0007FEBB File Offset: 0x0007E0BB
		public override int[][] CreateSlotMap()
		{
			return this._slotMap;
		}

		// Token: 0x04000D71 RID: 3441
		private readonly int[][] _slotMap;

		// Token: 0x0200041C RID: 1052
		public class Arguments : DraculaSlotMapBuilderBase.ArgumentsBase
		{
			// Token: 0x04000D74 RID: 3444
			[Argument(0, HelpText = "Minimum number of examples to keep feature value for entropy calculation", ShortName = "sth")]
			public int significanceThreshold = 5;
		}
	}
}
