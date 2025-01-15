using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000023 RID: 35
	public abstract class MamlEvaluatorBase : IMamlEvaluator, IEvaluator
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000AE RID: 174
		protected abstract IEvaluator Evaluator { get; }

		// Token: 0x060000AF RID: 175 RVA: 0x0000698C File Offset: 0x00004B8C
		protected MamlEvaluatorBase(MamlEvaluatorBase.ArgumentsBase args, IHostEnvironment env, string scoreColumnKind, string registrationName)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register(registrationName);
			this._scoreColumnKind = scoreColumnKind;
			this._scoreCol = args.scoreColumn;
			this._labelCol = args.labelColumn;
			this._weightCol = args.weightColumn;
			this._stratCols = args.stratColumn;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000069EF File Offset: 0x00004BEF
		public Dictionary<string, IDataView> Evaluate(RoleMappedData data)
		{
			data = RoleMappedData.Create(data.Data, this.GetInputColumnRoles(data.Schema, true, false));
			return this.Evaluator.Evaluate(data);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006A28 File Offset: 0x00004C28
		protected IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> GetInputColumnRoles(RoleMappedSchema schema, bool needStrat = false, bool needName = false)
		{
			Contracts.CheckValue<RoleMappedSchema>(this._host, schema, "schema");
			IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable;
			if (needStrat && this._stratCols != null)
			{
				enumerable = this._stratCols.Select((string col) => RoleMappedSchema.CreatePair(MamlEvaluatorBase.Strat, col));
			}
			else
			{
				enumerable = Enumerable.Empty<KeyValuePair<RoleMappedSchema.ColumnRole, string>>();
			}
			IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable2 = enumerable;
			if (needName && schema.Name != null)
			{
				enumerable2 = MetadataUtils.Prepend<KeyValuePair<RoleMappedSchema.ColumnRole, string>>(enumerable2, new KeyValuePair<RoleMappedSchema.ColumnRole, string>[] { RoleMappedSchema.CreatePair(RoleMappedSchema.ColumnRole.Name, schema.Name.Name) });
			}
			return enumerable2.Concat(this.GetInputColumnRolesCore(schema));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006CA8 File Offset: 0x00004EA8
		protected virtual IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> GetInputColumnRolesCore(RoleMappedSchema schema)
		{
			ColumnInfo scoreInfo = EvaluateUtils.GetScoreColumnInfo(this._host, schema.Schema, this._scoreCol, "scoreColumn", this._scoreColumnKind, "Score", null);
			yield return RoleMappedSchema.CreatePair("Score", scoreInfo.Name);
			string lab = EvaluateUtils.GetColName(this._labelCol, schema.Label, "Label");
			yield return RoleMappedSchema.CreatePair(RoleMappedSchema.ColumnRole.Label, lab);
			string weight = EvaluateUtils.GetColName(this._weightCol, schema.Weight, null);
			if (!string.IsNullOrEmpty(weight))
			{
				yield return RoleMappedSchema.CreatePair(RoleMappedSchema.ColumnRole.Weight, weight);
			}
			yield break;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006CCC File Offset: 0x00004ECC
		public virtual IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			return this.Evaluator.GetOverallMetricColumns();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006CD9 File Offset: 0x00004ED9
		public void PrintFoldResults(IChannel ch, Dictionary<string, IDataView> metrics)
		{
			Contracts.CheckValue<IChannel>(this._host, ch, "ch");
			Contracts.CheckValue<Dictionary<string, IDataView>>(this._host, metrics, "metrics");
			this.PrintFoldResultsCore(ch, metrics);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006D08 File Offset: 0x00004F08
		protected virtual void PrintFoldResultsCore(IChannel ch, Dictionary<string, IDataView> metrics)
		{
			IDataView dataView;
			if (!metrics.TryGetValue("OverallMetrics", out dataView))
			{
				throw Contracts.Except(this._host, "No overall metrics found");
			}
			string text;
			string perFoldResults = MetricWriter.GetPerFoldResults(this._host, dataView, out text);
			if (!string.IsNullOrEmpty(text))
			{
				ch.Info(text);
			}
			ch.Info(perFoldResults);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006D5A File Offset: 0x00004F5A
		public void PrintOverallResults(IChannel ch, string filename, params Dictionary<string, IDataView>[] metrics)
		{
			Contracts.CheckValue<IChannel>(this._host, ch, "ch");
			Contracts.CheckNonEmpty<Dictionary<string, IDataView>>(this._host, metrics, "metrics");
			this.PrintOverallResultsCore(ch, filename, metrics);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006D88 File Offset: 0x00004F88
		protected virtual void PrintOverallResultsCore(IChannel ch, string filename, Dictionary<string, IDataView>[] metrics)
		{
			IDataView dataView;
			if (!this.TryGetOverallMetrics(metrics, out dataView))
			{
				throw Contracts.Except(this._host, "No overall metrics found");
			}
			MetricWriter.PrintOverallMetrics(this._host, ch, filename, dataView);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00006DC0 File Offset: 0x00004FC0
		protected bool TryGetOverallMetrics(Dictionary<string, IDataView>[] metrics, out IDataView overall)
		{
			if (metrics.Length == 1)
			{
				return metrics[0].TryGetValue("OverallMetrics", out overall);
			}
			overall = null;
			List<IDataView> list = new List<IDataView>();
			for (int i = 0; i < metrics.Length; i++)
			{
				Dictionary<string, IDataView> dictionary = metrics[i];
				IDataView dataView;
				if (!dictionary.TryGetValue("OverallMetrics", out dataView))
				{
					return false;
				}
				string columnName = dataView.Schema.GetColumnName(0);
				ColumnType columnType = dataView.Schema.GetColumnType(0);
				dataView = Utils.MarshalInvoke<IHost, IDataView, string, string, ColumnType, string, string, IDataView>(new Func<IHost, IDataView, string, string, ColumnType, string, string, IDataView>(EvaluateUtils.AddTextColumn<int>), columnType.RawType, this._host, dataView, columnName, "Fold Index", columnType, string.Format("Fold {0}", i), "FoldName");
				list.Add(dataView);
			}
			overall = AppendRowsDataView.Create(this._host, list[0].Schema, list.ToArray());
			return true;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00006E90 File Offset: 0x00005090
		public IDataTransform GetPerInstanceMetrics(RoleMappedData scoredData)
		{
			RoleMappedSchema schema = scoredData.Schema;
			RoleMappedData roleMappedData = RoleMappedData.Create(scoredData.Data, this.GetInputColumnRoles(schema, false, false));
			return this.Evaluator.GetPerInstanceMetrics(roleMappedData);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00006EC8 File Offset: 0x000050C8
		private IDataView WrapPerInstance(RoleMappedData perInst)
		{
			IDataView dataView = perInst.Data;
			List<ChooseColumnsTransform.Column> list = new List<ChooseColumnsTransform.Column>();
			int num;
			if (perInst.Schema.Schema.TryGetColumnIndex("Fold Index", ref num))
			{
				list.Add(new ChooseColumnsTransform.Column
				{
					source = "Fold Index"
				});
			}
			if (perInst.Schema.Name == null)
			{
				dataView = new GenerateNumberTransform(new GenerateNumberTransform.Arguments
				{
					column = new GenerateNumberTransform.Column[]
					{
						new GenerateNumberTransform.Column
						{
							name = "Instance"
						}
					},
					useCounter = true
				}, this._host, dataView);
				list.Add(new ChooseColumnsTransform.Column
				{
					name = "Instance"
				});
			}
			else
			{
				list.Add(new ChooseColumnsTransform.Column
				{
					source = perInst.Schema.Name.Name,
					name = "Instance"
				});
			}
			if (perInst.Schema.Weight != null)
			{
				list.Add(new ChooseColumnsTransform.Column
				{
					name = perInst.Schema.Weight.Name
				});
			}
			foreach (string text in this.GetPerInstanceColumnsToSave(perInst.Schema))
			{
				list.Add(new ChooseColumnsTransform.Column
				{
					name = text
				});
			}
			dataView = new ChooseColumnsTransform(new ChooseColumnsTransform.Arguments
			{
				column = list.ToArray()
			}, this._host, dataView);
			return this.GetPerInstanceMetricsCore(dataView);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00007070 File Offset: 0x00005270
		protected virtual IDataView GetPerInstanceMetricsCore(IDataView perInst)
		{
			return perInst;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007074 File Offset: 0x00005274
		public IDataView GetPerInstanceDataViewToSave(RoleMappedData perInstance)
		{
			Contracts.CheckValue<RoleMappedData>(this._host, perInstance, "perInstance");
			RoleMappedData roleMappedData = RoleMappedData.Create(perInstance.Data, this.GetInputColumnRoles(perInstance.Schema, false, true));
			return this.WrapPerInstance(roleMappedData);
		}

		// Token: 0x060000BD RID: 189
		protected abstract IEnumerable<string> GetPerInstanceColumnsToSave(RoleMappedSchema schema);

		// Token: 0x04000056 RID: 86
		public static RoleMappedSchema.ColumnRole Strat = "Strat";

		// Token: 0x04000057 RID: 87
		protected readonly IHost _host;

		// Token: 0x04000058 RID: 88
		protected readonly string _scoreColumnKind;

		// Token: 0x04000059 RID: 89
		protected readonly string _scoreCol;

		// Token: 0x0400005A RID: 90
		protected readonly string _labelCol;

		// Token: 0x0400005B RID: 91
		protected readonly string _weightCol;

		// Token: 0x0400005C RID: 92
		protected readonly string[] _stratCols;

		// Token: 0x02000024 RID: 36
		public abstract class ArgumentsBase
		{
			// Token: 0x0400005E RID: 94
			[Argument(0, HelpText = "Column to use for labels", ShortName = "lab")]
			public string labelColumn;

			// Token: 0x0400005F RID: 95
			[Argument(0, HelpText = "Weight column name", ShortName = "weight")]
			public string weightColumn;

			// Token: 0x04000060 RID: 96
			[Argument(0, HelpText = "Score column name", ShortName = "score")]
			public string scoreColumn;

			// Token: 0x04000061 RID: 97
			[Argument(4, HelpText = "Stratification column name", ShortName = "strat")]
			public string[] stratColumn;
		}
	}
}
