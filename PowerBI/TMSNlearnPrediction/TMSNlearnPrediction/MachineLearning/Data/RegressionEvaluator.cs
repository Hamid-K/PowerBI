using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000294 RID: 660
	public sealed class RegressionEvaluator : RegressionEvaluatorBase
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x000538BD File Offset: 0x00051ABD
		public RegressionEvaluator(RegressionEvaluator.Arguments args, IHostEnvironment env)
			: base(args, env, "RegressionEvaluator")
		{
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000538CC File Offset: 0x00051ACC
		protected override void CheckScoreAndLabelTypes(RoleMappedSchema schema)
		{
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			ColumnType columnType = uniqueColumn.Type;
			if (columnType.IsVector || columnType.ItemType != NumberType.Float)
			{
				throw Contracts.Except(this._host, "Score column '{0}' has type '{1}' but must be R4", new object[] { uniqueColumn, columnType });
			}
			Contracts.Check(this._host, schema.Label != null, "Could not find the label column");
			columnType = schema.Label.Type;
			if (columnType != NumberType.R4)
			{
				throw Contracts.Except(this._host, "Label column '{0}' has type '{1}' but must be R4", new object[]
				{
					schema.Label.Name,
					columnType
				});
			}
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00053982 File Offset: 0x00051B82
		protected override EvaluatorBase.AggregatorBase GetAggregatorCore(RoleMappedSchema schema, string stratName)
		{
			return new RegressionEvaluator.Aggregator(this._host, this._lossFunction, schema.Weight != null, stratName);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000539A4 File Offset: 0x00051BA4
		protected override IRowMapper CreatePerInstanceRowMapper(RoleMappedSchema schema)
		{
			Contracts.CheckValue<ColumnInfo>(schema.Label, "label", "Could not find the label column");
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			return new RegressionPerInstanceEvaluator(this._host, schema.Schema, uniqueColumn.Name, schema.Label.Name);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00053B94 File Offset: 0x00051D94
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("L1", "L1(avg)", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("L2", "L2(avg)", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("Rms", "RMS(avg)", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("Loss", "Loss-fn(avg)", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("RSquared", "R Squared", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield break;
		}

		// Token: 0x04000851 RID: 2129
		public const string LoadName = "RegressionEvaluator";

		// Token: 0x02000295 RID: 661
		public sealed class Arguments : RegressionEvaluatorBase.ArgumentsBase
		{
		}

		// Token: 0x02000296 RID: 662
		public enum Metrics
		{
			// Token: 0x04000853 RID: 2131
			[EnumValueDisplay("L1(avg)")]
			L1,
			// Token: 0x04000854 RID: 2132
			[EnumValueDisplay("L2(avg)")]
			L2,
			// Token: 0x04000855 RID: 2133
			[EnumValueDisplay("RMS(avg)")]
			Rms,
			// Token: 0x04000856 RID: 2134
			[EnumValueDisplay("Loss-fn(avg)")]
			Loss,
			// Token: 0x04000857 RID: 2135
			[EnumValueDisplay("R Squared")]
			RSquared
		}

		// Token: 0x02000297 RID: 663
		private sealed class Aggregator : RegressionEvaluatorBase.RegressionAggregatorBase<float, double>
		{
			// Token: 0x17000196 RID: 406
			// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00053BB9 File Offset: 0x00051DB9
			protected override RegressionEvaluatorBase.RegressionAggregatorBase<float, double>.CountersBase UnweightedCounters
			{
				get
				{
					return this._counters;
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00053BC1 File Offset: 0x00051DC1
			protected override RegressionEvaluatorBase.RegressionAggregatorBase<float, double>.CountersBase WeightedCounters
			{
				get
				{
					return this._weightedCounters;
				}
			}

			// Token: 0x06000F47 RID: 3911 RVA: 0x00053BC9 File Offset: 0x00051DC9
			public Aggregator(IHostEnvironment env, IRegressionLoss lossFunction, bool weighted, string stratName)
				: base(env, lossFunction, stratName)
			{
				this._counters = new RegressionEvaluator.Aggregator.Counters();
				this._weightedCounters = (weighted ? new RegressionEvaluator.Aggregator.Counters() : null);
			}

			// Token: 0x06000F48 RID: 3912 RVA: 0x00053BF1 File Offset: 0x00051DF1
			protected override void ApplyLossFunction(ref float score, float label, ref double loss)
			{
				loss = this._lossFunction.Loss(score, label);
			}

			// Token: 0x06000F49 RID: 3913 RVA: 0x00053C03 File Offset: 0x00051E03
			protected override bool IsNaN(ref float score)
			{
				return float.IsNaN(score);
			}

			// Token: 0x06000F4A RID: 3914 RVA: 0x00053C0C File Offset: 0x00051E0C
			protected override void AddColumn(ArrayDataViewBuilder dvBldr, string metricName, params double[] metric)
			{
				dvBldr.AddColumn<double>(metricName, NumberType.R8, metric);
			}

			// Token: 0x04000858 RID: 2136
			private readonly RegressionEvaluator.Aggregator.Counters _counters;

			// Token: 0x04000859 RID: 2137
			private readonly RegressionEvaluator.Aggregator.Counters _weightedCounters;

			// Token: 0x02000298 RID: 664
			private sealed class Counters : RegressionEvaluatorBase.RegressionAggregatorBase<float, double>.CountersBase
			{
				// Token: 0x17000198 RID: 408
				// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00053C1B File Offset: 0x00051E1B
				public override double Rms
				{
					get
					{
						if (this._sumWeights <= 0.0)
						{
							return 0.0;
						}
						return Math.Sqrt(this._totalL2Loss / this._sumWeights);
					}
				}

				// Token: 0x17000199 RID: 409
				// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00053C4C File Offset: 0x00051E4C
				public override double RSquared
				{
					get
					{
						if (this._sumWeights <= 0.0)
						{
							return 0.0;
						}
						return 1.0 - this._totalL2Loss / (this._totalLabelSquaredW - this._totalLabelW) / this._sumWeights;
					}
				}

				// Token: 0x06000F4D RID: 3917 RVA: 0x00053C9C File Offset: 0x00051E9C
				protected override void UpdateCore(float label, ref float score, ref double loss, float weight)
				{
					double num = Math.Abs((double)label - (double)score);
					this._totalL1Loss += num * (double)weight;
					this._totalL2Loss += num * num * (double)weight;
					this._totalLoss += loss * (double)weight;
				}

				// Token: 0x06000F4E RID: 3918 RVA: 0x00053CEE File Offset: 0x00051EEE
				protected override void Normalize(ref double src, ref double dst)
				{
					dst = src / this._sumWeights;
				}

				// Token: 0x06000F4F RID: 3919 RVA: 0x00053CFB File Offset: 0x00051EFB
				protected override double Zero()
				{
					return 0.0;
				}
			}
		}
	}
}
