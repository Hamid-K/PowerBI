using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001ED RID: 493
	public sealed class RankerMamlEvaluator : MamlEvaluatorBase
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0003AB19 File Offset: 0x00038D19
		protected override IEvaluator Evaluator
		{
			get
			{
				return this._evaluator;
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0003AB24 File Offset: 0x00038D24
		public RankerMamlEvaluator(RankerMamlEvaluator.Arguments args, IHostEnvironment env)
			: base(args, env, "Ranking", "RankerMamlEvaluator")
		{
			Contracts.CheckValue<RankerMamlEvaluator.Arguments>(this._host, args, "args");
			Utils.CheckOptionalUserDirectory(args.groupSummaryFilename, "groupSummaryFilename");
			this._evaluator = new RankerEvaluator(new RankerEvaluator.Arguments
			{
				dcgTruncationLevel = args.dcgTruncationLevel,
				labelGains = args.labelGains,
				outputGroupSummary = !string.IsNullOrEmpty(args.groupSummaryFilename)
			}, this._host);
			this._groupSummaryFilename = args.groupSummaryFilename;
			this._groupIdCol = args.groupIdColumn;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0003ABC0 File Offset: 0x00038DC0
		protected override IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> GetInputColumnRolesCore(RoleMappedSchema schema)
		{
			IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> inputColumnRolesCore = base.GetInputColumnRolesCore(schema);
			string colName = EvaluateUtils.GetColName(this._groupIdCol, schema.Group, "GroupId");
			return MetadataUtils.Prepend<KeyValuePair<RoleMappedSchema.ColumnRole, string>>(inputColumnRolesCore, new KeyValuePair<RoleMappedSchema.ColumnRole, string>[] { RoleMappedSchema.CreatePair(RoleMappedSchema.ColumnRole.Group, colName) });
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0003AC14 File Offset: 0x00038E14
		protected override void PrintOverallResultsCore(IChannel ch, string filename, Dictionary<string, IDataView>[] metrics)
		{
			base.PrintOverallResultsCore(ch, filename, metrics);
			if (!string.IsNullOrEmpty(this._groupSummaryFilename))
			{
				IDataView nonStratifiedMetrics;
				if (!this.TryGetGroupSummaryMetrics(metrics, out nonStratifiedMetrics))
				{
					throw Contracts.Except(this._host, "Did not find group summary metrics");
				}
				ch.Trace("Saving group-summary results");
				nonStratifiedMetrics = MetricWriter.GetNonStratifiedMetrics(this._host, nonStratifiedMetrics);
				MetricWriter.SavePerInstance(this._host, ch, this._groupSummaryFilename, nonStratifiedMetrics, true, true);
				ch.Done();
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0003AC88 File Offset: 0x00038E88
		private bool TryGetGroupSummaryMetrics(Dictionary<string, IDataView>[] metrics, out IDataView gs)
		{
			if (metrics.Length == 1)
			{
				return metrics[0].TryGetValue("GroupSummary", out gs);
			}
			gs = null;
			List<IDataView> list = new List<IDataView>();
			for (int i = 0; i < metrics.Length; i++)
			{
				IDataView dataView;
				if (!metrics[i].TryGetValue("GroupSummary", out dataView))
				{
					return false;
				}
				string columnName = dataView.Schema.GetColumnName(0);
				ColumnType columnType = dataView.Schema.GetColumnType(0);
				dataView = Utils.MarshalInvoke<IHost, IDataView, string, string, ColumnType, int, int, string, ValueGetter<VBuffer<DvText>>, IDataView>(new Func<IHost, IDataView, string, string, ColumnType, int, int, string, ValueGetter<VBuffer<DvText>>, IDataView>(EvaluateUtils.AddKeyColumn<int>), columnType.RawType, this._host, dataView, columnName, "Fold Index", columnType, metrics.Length, i + 1, "FoldIndex", null);
				list.Add(dataView);
			}
			gs = AppendRowsDataView.Create(this._host, list[0].Schema, list.ToArray());
			return true;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0003AF7C File Offset: 0x0003917C
		protected override IEnumerable<string> GetPerInstanceColumnsToSave(RoleMappedSchema schema)
		{
			Contracts.CheckValue<RoleMappedSchema>(this._host, schema, "schema");
			Contracts.CheckValue<ColumnInfo>(this._host, schema.Label, "schema", "Data must contain a label column");
			Contracts.CheckValue<ColumnInfo>(this._host, schema.Group, "schema", "Data must contain a group column");
			yield return schema.Group.Name;
			yield return schema.Label.Name;
			ColumnInfo scoreInfo = EvaluateUtils.GetScoreColumnInfo(this._host, schema.Schema, this._scoreCol, "scoreColumn", "Ranking", "Score", null);
			yield return scoreInfo.Name;
			yield return "NDCG";
			yield return "DCG";
			yield return "MaxDCG";
			yield break;
		}

		// Token: 0x040005CD RID: 1485
		private readonly RankerEvaluator _evaluator;

		// Token: 0x040005CE RID: 1486
		private readonly string _groupIdCol;

		// Token: 0x040005CF RID: 1487
		private readonly string _groupSummaryFilename;

		// Token: 0x020001EE RID: 494
		public sealed class Arguments : MamlEvaluatorBase.ArgumentsBase
		{
			// Token: 0x040005D0 RID: 1488
			[Argument(0, HelpText = "Column to use for the group ID", ShortName = "group")]
			public string groupIdColumn;

			// Token: 0x040005D1 RID: 1489
			[Argument(0, HelpText = "Maximum truncation level for computing (N)DCG", ShortName = "t")]
			public int dcgTruncationLevel = 3;

			// Token: 0x040005D2 RID: 1490
			[Argument(0, HelpText = "Label relevance gains", ShortName = "gains")]
			public string labelGains = "0,3,7,15,31";

			// Token: 0x040005D3 RID: 1491
			[Argument(0, HelpText = "Group summary filename", ShortName = "gsf")]
			public string groupSummaryFilename;
		}
	}
}
