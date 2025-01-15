using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200008F RID: 143
	public sealed class ClusteringMamlEvaluator : MamlEvaluatorBase
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000FFE5 File Offset: 0x0000E1E5
		protected override IEvaluator Evaluator
		{
			get
			{
				return this._evaluator;
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000FFF0 File Offset: 0x0000E1F0
		public ClusteringMamlEvaluator(ClusteringMamlEvaluator.Arguments args, IHostEnvironment env)
			: base(args, env, "Clustering", "ClusteringMamlEvaluator")
		{
			Contracts.CheckValue<ClusteringMamlEvaluator.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, 1 <= args.numTopClustersToOutput, "numTopClustersToOutput");
			this._numTopClusters = args.numTopClustersToOutput;
			this._featureCol = args.featureColumn;
			this._evaluator = new ClusteringEvaluator(new ClusteringEvaluator.Arguments
			{
				calculateDbi = args.calculateDbi
			}, this._host);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00010348 File Offset: 0x0000E548
		protected override IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> GetInputColumnRolesCore(RoleMappedSchema schema)
		{
			foreach (KeyValuePair<RoleMappedSchema.ColumnRole, string> col in base.GetInputColumnRolesCore(schema))
			{
				KeyValuePair<RoleMappedSchema.ColumnRole, string> keyValuePair = col;
				if (!keyValuePair.Key.Equals(RoleMappedSchema.ColumnRole.Label))
				{
					yield return col;
				}
				else
				{
					ISchema schema2 = schema.Schema;
					KeyValuePair<RoleMappedSchema.ColumnRole, string> keyValuePair2 = col;
					int labelIndex;
					if (schema2.TryGetColumnIndex(keyValuePair2.Value, ref labelIndex))
					{
						yield return col;
					}
				}
			}
			string feat = EvaluateUtils.GetColName(this._featureCol, schema.Feature, "Features");
			int featCol;
			if (!schema.Schema.TryGetColumnIndex(feat, ref featCol))
			{
				throw Contracts.ExceptUserArg(this._host, "featureColumn", "Features column '{0}' not found", new object[] { feat });
			}
			yield return RoleMappedSchema.CreatePair(RoleMappedSchema.ColumnRole.Feature, feat);
			yield break;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000104D4 File Offset: 0x0000E6D4
		protected override IEnumerable<string> GetPerInstanceColumnsToSave(RoleMappedSchema schema)
		{
			Contracts.CheckValue<RoleMappedSchema>(this._host, schema, "schema");
			if (schema.Label != null)
			{
				yield return schema.Label.Name;
			}
			yield return "ClusterId";
			yield return "SortedClusters";
			yield return "SortedScores";
			yield break;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000104F8 File Offset: 0x0000E6F8
		protected override IDataView GetPerInstanceMetricsCore(IDataView perInst)
		{
			int num;
			if (perInst.Schema.TryGetColumnIndex("SortedClusters", ref num))
			{
				ColumnType columnType = perInst.Schema.GetColumnType(num);
				if (this._numTopClusters < columnType.VectorSize)
				{
					perInst = new DropSlotsTransform(new DropSlotsTransform.Arguments
					{
						column = new DropSlotsTransform.Column[]
						{
							new DropSlotsTransform.Column
							{
								name = "SortedClusters",
								slots = new DropSlotsTransform.Range[]
								{
									new DropSlotsTransform.Range
									{
										min = this._numTopClusters
									}
								}
							}
						}
					}, this._host, perInst);
				}
			}
			if (perInst.Schema.TryGetColumnIndex("SortedScores", ref num))
			{
				ColumnType columnType2 = perInst.Schema.GetColumnType(num);
				if (this._numTopClusters < columnType2.VectorSize)
				{
					perInst = new DropSlotsTransform(new DropSlotsTransform.Arguments
					{
						column = new DropSlotsTransform.Column[]
						{
							new DropSlotsTransform.Column
							{
								name = "SortedScores",
								slots = new DropSlotsTransform.Range[]
								{
									new DropSlotsTransform.Range
									{
										min = this._numTopClusters
									}
								}
							}
						}
					}, this._host, perInst);
				}
			}
			return perInst;
		}

		// Token: 0x04000117 RID: 279
		private readonly ClusteringEvaluator _evaluator;

		// Token: 0x04000118 RID: 280
		private readonly int _numTopClusters;

		// Token: 0x04000119 RID: 281
		private readonly string _featureCol;

		// Token: 0x02000090 RID: 144
		public class Arguments : MamlEvaluatorBase.ArgumentsBase
		{
			// Token: 0x0400011A RID: 282
			[Argument(0, HelpText = "Features column name", ShortName = "feat")]
			public string featureColumn;

			// Token: 0x0400011B RID: 283
			[Argument(0, HelpText = "Calculate DBI? (time-consuming unsupervised metric)", ShortName = "dbi")]
			public bool calculateDbi;

			// Token: 0x0400011C RID: 284
			[Argument(0, HelpText = "Output top K clusters", ShortName = "topk")]
			public int numTopClustersToOutput = 3;
		}
	}
}
