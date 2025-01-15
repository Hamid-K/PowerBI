using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001DE RID: 478
	public sealed class QuantileRegressionEvaluator : RegressionEvaluatorBase
	{
		// Token: 0x06000AAD RID: 2733 RVA: 0x00037F3C File Offset: 0x0003613C
		public QuantileRegressionEvaluator(QuantileRegressionEvaluator.Arguments args, IHostEnvironment env)
			: base(args, env, "QuantileRegressionEvaluator")
		{
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00037F4C File Offset: 0x0003614C
		protected override IRowMapper CreatePerInstanceRowMapper(RoleMappedSchema schema)
		{
			Contracts.CheckValue<ColumnInfo>(this._host, schema.Label, "Schema must contain a label column");
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			int vectorSize = uniqueColumn.Type.VectorSize;
			ColumnType metadataTypeOrNull = schema.Schema.GetMetadataTypeOrNull("SlotNames", uniqueColumn.Index);
			Contracts.Check(this._host, metadataTypeOrNull != null && metadataTypeOrNull.IsKnownSizeVector && metadataTypeOrNull.ItemType.IsText, "Quantile regression score column must have slot names");
			VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
			schema.Schema.GetMetadata<VBuffer<DvText>>("SlotNames", uniqueColumn.Index, ref vbuffer);
			return new QuantileRegressionPerInstanceEvaluator(this._host, schema.Schema, uniqueColumn.Name, schema.Label.Name, vectorSize, vbuffer.Values);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00038018 File Offset: 0x00036218
		protected override void CheckScoreAndLabelTypes(RoleMappedSchema schema)
		{
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			ColumnType columnType = uniqueColumn.Type;
			if (columnType.VectorSize == 0 || (columnType.ItemType != NumberType.R4 && columnType.ItemType != NumberType.R8))
			{
				throw Contracts.Except(this._host, "Score column '{0}' has type '{1}' but must be a known length vector of type R4 or R8", new object[] { uniqueColumn.Name, columnType });
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

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000380E0 File Offset: 0x000362E0
		protected override EvaluatorBase.AggregatorBase GetAggregatorCore(RoleMappedSchema schema, string stratName)
		{
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			ColumnType columnType = uniqueColumn.Type;
			VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
			columnType = schema.Schema.GetMetadataTypeOrNull("SlotNames", uniqueColumn.Index);
			if (columnType != null && columnType.VectorSize == uniqueColumn.Type.VectorSize && columnType.ItemType.IsText)
			{
				schema.Schema.GetMetadata<VBuffer<DvText>>("SlotNames", uniqueColumn.Index, ref vbuffer);
			}
			return new QuantileRegressionEvaluator.Aggregator(this._host, this._lossFunction, schema.Weight != null, uniqueColumn.Type.VectorSize, ref vbuffer, stratName);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00038324 File Offset: 0x00036524
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("L1", "L1(avg)", MetricColumn.Objective.Minimize, true, true, null, null, null);
			yield return new MetricColumn("L2", "L2(avg)", MetricColumn.Objective.Minimize, true, true, null, null, null);
			yield return new MetricColumn("Rms", "RMS(avg)", MetricColumn.Objective.Minimize, true, true, null, null, null);
			yield return new MetricColumn("Loss", "Loss-fn(avg)", MetricColumn.Objective.Minimize, true, true, null, null, null);
			yield return new MetricColumn("RSquared", "R Squared", MetricColumn.Objective.Maximize, true, true, null, null, null);
			yield break;
		}

		// Token: 0x0400057E RID: 1406
		public const string LoadName = "QuantileRegressionEvaluator";

		// Token: 0x020001DF RID: 479
		public sealed class Arguments : RegressionEvaluatorBase.ArgumentsBase
		{
		}

		// Token: 0x020001E0 RID: 480
		private sealed class Aggregator : RegressionEvaluatorBase.RegressionAggregatorBase<VBuffer<float>, VBuffer<double>>
		{
			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00038349 File Offset: 0x00036549
			protected override RegressionEvaluatorBase.RegressionAggregatorBase<VBuffer<float>, VBuffer<double>>.CountersBase UnweightedCounters
			{
				get
				{
					return this._counters;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00038351 File Offset: 0x00036551
			protected override RegressionEvaluatorBase.RegressionAggregatorBase<VBuffer<float>, VBuffer<double>>.CountersBase WeightedCounters
			{
				get
				{
					return this._weightedCounters;
				}
			}

			// Token: 0x06000AB5 RID: 2741 RVA: 0x0003835C File Offset: 0x0003655C
			public Aggregator(IHostEnvironment env, IRegressionLoss lossFunction, bool weighted, int size, ref VBuffer<DvText> slotNames, string stratName)
				: base(env, lossFunction, stratName)
			{
				this._score = new VBuffer<float>(size, 0, null, null);
				this._loss = new VBuffer<double>(size, 0, null, null);
				this._counters = new QuantileRegressionEvaluator.Aggregator.Counters(size);
				if (weighted)
				{
					this._weightedCounters = new QuantileRegressionEvaluator.Aggregator.Counters(size);
				}
				this._slotNames = slotNames;
			}

			// Token: 0x06000AB6 RID: 2742 RVA: 0x000383E0 File Offset: 0x000365E0
			protected override void ApplyLossFunction(ref VBuffer<float> score, float label, ref VBuffer<double> loss)
			{
				VBufferUtils.PairManipulator<float, double> pairManipulator = delegate(int slot, float src, ref double dst)
				{
					dst = this._lossFunction.Loss(src, label);
				};
				VBufferUtils.ApplyWith<float, double>(ref score, ref loss, pairManipulator);
			}

			// Token: 0x06000AB7 RID: 2743 RVA: 0x00038416 File Offset: 0x00036616
			protected override bool IsNaN(ref VBuffer<float> score)
			{
				return VBufferUtils.HasNaNs(ref score);
			}

			// Token: 0x06000AB8 RID: 2744 RVA: 0x0003841E File Offset: 0x0003661E
			protected override void AddColumn(ArrayDataViewBuilder dvBldr, string metricName, params VBuffer<double>[] metric)
			{
				if (this._slotNames.Length > 0)
				{
					dvBldr.AddColumn<double>(metricName, ref this._slotNames, NumberType.R8, metric);
					return;
				}
				dvBldr.AddColumn<double>(metricName, NumberType.R8, metric);
			}

			// Token: 0x0400057F RID: 1407
			private readonly QuantileRegressionEvaluator.Aggregator.Counters _counters;

			// Token: 0x04000580 RID: 1408
			private readonly QuantileRegressionEvaluator.Aggregator.Counters _weightedCounters;

			// Token: 0x04000581 RID: 1409
			private VBuffer<DvText> _slotNames;

			// Token: 0x020001E1 RID: 481
			private sealed class Counters : RegressionEvaluatorBase.RegressionAggregatorBase<VBuffer<float>, VBuffer<double>>.CountersBase
			{
				// Token: 0x1700013B RID: 315
				// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00038450 File Offset: 0x00036650
				public override VBuffer<double> Rms
				{
					get
					{
						double[] array = new double[this._size];
						if (this._sumWeights != 0.0)
						{
							foreach (KeyValuePair<int, double> keyValuePair in this._totalL2Loss.Items(false))
							{
								array[keyValuePair.Key] = Math.Sqrt(keyValuePair.Value / this._sumWeights);
							}
						}
						return new VBuffer<double>(this._size, array, null);
					}
				}

				// Token: 0x1700013C RID: 316
				// (get) Token: 0x06000ABA RID: 2746 RVA: 0x000384E4 File Offset: 0x000366E4
				public override VBuffer<double> RSquared
				{
					get
					{
						double[] array = new double[this._size];
						if (this._sumWeights != 0.0)
						{
							foreach (KeyValuePair<int, double> keyValuePair in this._totalL2Loss.Items(false))
							{
								array[keyValuePair.Key] = 1.0 - keyValuePair.Value / (this._totalLabelSquaredW - this._totalLabelW) / this._sumWeights;
							}
						}
						return new VBuffer<double>(this._size, array, null);
					}
				}

				// Token: 0x06000ABB RID: 2747 RVA: 0x0003858C File Offset: 0x0003678C
				public Counters(int size)
				{
					this._size = size;
					this._totalL1Loss = VBufferUtils.CreateDense<double>(size);
					this._totalL2Loss = VBufferUtils.CreateDense<double>(size);
					this._totalLoss = VBufferUtils.CreateDense<double>(size);
				}

				// Token: 0x06000ABC RID: 2748 RVA: 0x000385BF File Offset: 0x000367BF
				protected override void UpdateCore(float label, ref VBuffer<float> score, ref VBuffer<double> loss, float weight)
				{
					this.AddL1AndL2Loss(label, ref score, weight);
					this.AddCustomLoss(weight, ref loss);
				}

				// Token: 0x06000ABD RID: 2749 RVA: 0x000385D4 File Offset: 0x000367D4
				private void AddL1AndL2Loss(float label, ref VBuffer<float> score, float weight)
				{
					Contracts.Check(score.Length == this._totalL1Loss.Length, "Vectors must have the same dimensionality.");
					if (score.IsDense)
					{
						for (int i = 0; i < score.Length; i++)
						{
							double num = Math.Abs((double)label - (double)score.Values[i]);
							double num2 = num * (double)weight;
							this._totalL1Loss.Values[i] += num2;
							this._totalL2Loss.Values[i] += num * num2;
						}
						return;
					}
					for (int j = 0; j < score.Count; j++)
					{
						double num3 = Math.Abs((double)label - (double)score.Values[j]);
						double num4 = num3 * (double)weight;
						this._totalL1Loss.Values[score.Indices[j]] += num4;
						this._totalL2Loss.Values[score.Indices[j]] += num3 * num4;
					}
				}

				// Token: 0x06000ABE RID: 2750 RVA: 0x000386E8 File Offset: 0x000368E8
				private void AddCustomLoss(float weight, ref VBuffer<double> loss)
				{
					Contracts.Check(loss.Length == this._totalL1Loss.Length, "Vectors must have the same dimensionality.");
					if (loss.IsDense)
					{
						for (int i = 0; i < loss.Length; i++)
						{
							this._totalLoss.Values[i] += loss.Values[i] * (double)weight;
						}
						return;
					}
					for (int j = 0; j < loss.Count; j++)
					{
						this._totalLoss.Values[loss.Indices[j]] += loss.Values[j] * (double)weight;
					}
				}

				// Token: 0x06000ABF RID: 2751 RVA: 0x00038794 File Offset: 0x00036994
				protected override void Normalize(ref VBuffer<double> src, ref VBuffer<double> dst)
				{
					double[] array = dst.Values;
					if (Utils.Size<double>(array) < src.Length)
					{
						array = new double[src.Length];
					}
					double num = 1.0 / this._sumWeights;
					for (int i = 0; i < src.Length; i++)
					{
						array[i] = src.Values[i] * num;
					}
					dst = new VBuffer<double>(src.Length, array, null);
				}

				// Token: 0x06000AC0 RID: 2752 RVA: 0x00038804 File Offset: 0x00036A04
				protected override VBuffer<double> Zero()
				{
					return VBufferUtils.CreateDense<double>(this._size);
				}

				// Token: 0x04000582 RID: 1410
				private readonly int _size;
			}
		}
	}
}
