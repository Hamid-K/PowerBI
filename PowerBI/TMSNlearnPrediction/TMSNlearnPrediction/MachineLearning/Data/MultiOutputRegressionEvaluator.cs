using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001FD RID: 509
	public sealed class MultiOutputRegressionEvaluator : RegressionEvaluatorBase
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x0003CC08 File Offset: 0x0003AE08
		public MultiOutputRegressionEvaluator(MultiOutputRegressionEvaluator.Arguments args, IHostEnvironment env)
			: base(args, env, "MultiRegressionEvaluator")
		{
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0003CC18 File Offset: 0x0003AE18
		protected override IRowMapper CreatePerInstanceRowMapper(RoleMappedSchema schema)
		{
			Contracts.CheckValue<ColumnInfo>(this._host, schema.Label, "label", "Could not find the label column");
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			return new MultiOutputRegressionPerInstanceEvaluator(this._host, schema.Schema, uniqueColumn.Name, schema.Label.Name);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0003CC74 File Offset: 0x0003AE74
		protected override void CheckScoreAndLabelTypes(RoleMappedSchema schema)
		{
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			ColumnType columnType = uniqueColumn.Type;
			if (columnType.VectorSize == 0 || columnType.ItemType != NumberType.Float)
			{
				throw Contracts.Except(this._host, "Score column '{0}' has type '{1}' but must be a known length vector of type R4", new object[] { uniqueColumn.Name, columnType });
			}
			Contracts.Check(this._host, schema.Label != null, "Could not find the label column");
			columnType = schema.Label.Type;
			if (!columnType.IsKnownSizeVector || (columnType.ItemType != NumberType.R4 && columnType.ItemType != NumberType.R8))
			{
				throw Contracts.Except(this._host, "Label column '{0}' has type '{1}' but must be a known-size vector of R4 or R8", new object[]
				{
					schema.Label.Name,
					columnType
				});
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0003CD4C File Offset: 0x0003AF4C
		protected override EvaluatorBase.AggregatorBase GetAggregatorCore(RoleMappedSchema schema, string stratName)
		{
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			return new MultiOutputRegressionEvaluator.Aggregator(this._host, this._lossFunction, uniqueColumn.Type.VectorSize, schema.Weight != null, stratName);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0003CFC4 File Offset: 0x0003B1C4
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("Dist", "Euclidean-Dist(avg)", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("L1_<label number>", "Per label L1(avg)", MetricColumn.Objective.Minimize, true, true, new Regex(string.Format("{0}_(?<label>\\d+)\\)", "L1(avg)"), RegexOptions.IgnoreCase), "label", string.Format("{0} (Label_{{0}}", "Per label L1(avg)"));
			yield return new MetricColumn("L2_<label number>", "Per label L2(avg)", MetricColumn.Objective.Minimize, true, true, new Regex(string.Format("{0}_(?<label>\\d+)\\)", "L2(avg)"), RegexOptions.IgnoreCase), "label", string.Format("{0} (Label_{{0}}", "Per label L2(avg)"));
			yield return new MetricColumn("Rms_<label number>", "Per label RMS(avg)", MetricColumn.Objective.Minimize, true, true, new Regex(string.Format("{0}_(?<label>\\d+)\\)", "RMS(avg)"), RegexOptions.IgnoreCase), "label", string.Format("{0} (Label_{{0}}", "Per label RMS(avg)"));
			yield return new MetricColumn("Loss_<label number>", "Per label LOSS-FN(avg)", MetricColumn.Objective.Minimize, true, true, new Regex(string.Format("{0}_(?<label>\\d+)\\)", "Loss-fn(avg)"), RegexOptions.IgnoreCase), "label", string.Format("{0} (Label_{{0}}", "Per label LOSS-FN(avg)"));
			yield break;
		}

		// Token: 0x0400061A RID: 1562
		private const string Dist = "Euclidean-Dist(avg)";

		// Token: 0x0400061B RID: 1563
		private const string PerLabelL1 = "Per label L1(avg)";

		// Token: 0x0400061C RID: 1564
		private const string PerLabelL2 = "Per label L2(avg)";

		// Token: 0x0400061D RID: 1565
		private const string PerLabelRms = "Per label RMS(avg)";

		// Token: 0x0400061E RID: 1566
		private const string PerLabelLoss = "Per label LOSS-FN(avg)";

		// Token: 0x0400061F RID: 1567
		public const string LoadName = "MultiRegressionEvaluator";

		// Token: 0x020001FE RID: 510
		public sealed class Arguments : RegressionEvaluatorBase.ArgumentsBase
		{
		}

		// Token: 0x020001FF RID: 511
		private sealed class Aggregator : EvaluatorBase.AggregatorBase
		{
			// Token: 0x06000B56 RID: 2902 RVA: 0x0003CFEC File Offset: 0x0003B1EC
			public Aggregator(IHostEnvironment env, IRegressionLoss lossFunction, int size, bool weighted, string stratName)
				: base(env, stratName)
			{
				this._size = size;
				this._labelArr = new float[this._size];
				this._scoreArr = new float[this._size];
				this._counters = new MultiOutputRegressionEvaluator.Aggregator.Counters(lossFunction, this._size);
				this._weightedCounters = (weighted ? new MultiOutputRegressionEvaluator.Aggregator.Counters(lossFunction, this._size) : null);
			}

			// Token: 0x06000B57 RID: 2903 RVA: 0x0003D058 File Offset: 0x0003B258
			public override void InitializeNextPass(IRow row, RoleMappedSchema schema)
			{
				ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
				this._labelGetter = RowCursorUtils.GetVecGetterAs<float>(NumberType.Float, row, schema.Label.Index);
				this._scoreGetter = row.GetGetter<VBuffer<float>>(uniqueColumn.Index);
				if (schema.Weight != null)
				{
					this._weightGetter = row.GetGetter<float>(schema.Weight.Index);
				}
			}

			// Token: 0x06000B58 RID: 2904 RVA: 0x0003D0C4 File Offset: 0x0003B2C4
			public override void ProcessRow()
			{
				this._labelGetter.Invoke(ref this._label);
				Contracts.Check(this._label.Length == this._size);
				this._scoreGetter.Invoke(ref this._score);
				Contracts.Check(this._score.Length == this._size);
				if (VBufferUtils.HasNaNs(ref this._score))
				{
					this.NumBadScores += 1L;
					return;
				}
				float num = 1f;
				if (this._weightGetter != null)
				{
					this._weightGetter.Invoke(ref num);
					if (!FloatUtils.IsFinite(num))
					{
						this.NumBadWeights += 1L;
						num = 1f;
					}
				}
				float[] array;
				if (!this._label.IsDense)
				{
					this._label.CopyTo(this._labelArr);
					array = this._labelArr;
				}
				else
				{
					array = this._label.Values;
				}
				float[] array2;
				if (!this._score.IsDense)
				{
					this._score.CopyTo(this._scoreArr);
					array2 = this._scoreArr;
				}
				else
				{
					array2 = this._score.Values;
				}
				this._counters.Update(array2, array, this._size, 1f);
				if (this._weightedCounters != null)
				{
					this._weightedCounters.Update(array2, array, this._size, num);
				}
			}

			// Token: 0x06000B59 RID: 2905 RVA: 0x0003D214 File Offset: 0x0003B414
			public override Dictionary<string, IDataView> Finish()
			{
				ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(this._host);
				VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
				this.GetSlotNames(ref vbuffer);
				if (this._weightedCounters != null)
				{
					arrayDataViewBuilder.AddColumn<DvBool>("IsWeighted", BoolType.Instance, new DvBool[]
					{
						DvBool.False,
						DvBool.True
					});
					arrayDataViewBuilder.AddColumn<double>("Per label L1(avg)", ref vbuffer, NumberType.R8, new double[][]
					{
						this._counters.PerLabelL1,
						this._weightedCounters.PerLabelL1
					});
					arrayDataViewBuilder.AddColumn<double>("Per label L2(avg)", ref vbuffer, NumberType.R8, new double[][]
					{
						this._counters.PerLabelL2,
						this._weightedCounters.PerLabelL2
					});
					arrayDataViewBuilder.AddColumn<double>("Per label RMS(avg)", ref vbuffer, NumberType.R8, new double[][]
					{
						this._counters.PerLabelRms,
						this._weightedCounters.PerLabelRms
					});
					arrayDataViewBuilder.AddColumn<double>("Per label LOSS-FN(avg)", ref vbuffer, NumberType.R8, new double[][]
					{
						this._counters.PerLabelLoss,
						this._weightedCounters.PerLabelLoss
					});
					arrayDataViewBuilder.AddColumn<double>("L1(avg)", NumberType.R8, new double[]
					{
						this._counters.L1,
						this._weightedCounters.L1
					});
					arrayDataViewBuilder.AddColumn<double>("L2(avg)", NumberType.R8, new double[]
					{
						this._counters.L2,
						this._weightedCounters.L2
					});
					arrayDataViewBuilder.AddColumn<double>("Euclidean-Dist(avg)", NumberType.R8, new double[]
					{
						this._counters.Dist,
						this._weightedCounters.Dist
					});
				}
				else
				{
					arrayDataViewBuilder.AddColumn<double>("Per label L1(avg)", ref vbuffer, NumberType.R8, new double[][] { this._counters.PerLabelL1 });
					arrayDataViewBuilder.AddColumn<double>("Per label L2(avg)", ref vbuffer, NumberType.R8, new double[][] { this._counters.PerLabelL2 });
					arrayDataViewBuilder.AddColumn<double>("Per label RMS(avg)", ref vbuffer, NumberType.R8, new double[][] { this._counters.PerLabelRms });
					arrayDataViewBuilder.AddColumn<double>("Per label LOSS-FN(avg)", ref vbuffer, NumberType.R8, new double[][] { this._counters.PerLabelLoss });
					arrayDataViewBuilder.AddColumn<double>("L1(avg)", NumberType.R8, new double[] { this._counters.L1 });
					arrayDataViewBuilder.AddColumn<double>("L2(avg)", NumberType.R8, new double[] { this._counters.L2 });
					arrayDataViewBuilder.AddColumn<double>("Euclidean-Dist(avg)", NumberType.R8, new double[] { this._counters.Dist });
				}
				return new Dictionary<string, IDataView> { 
				{
					"OverallMetrics",
					arrayDataViewBuilder.GetDataView(null)
				} };
			}

			// Token: 0x06000B5A RID: 2906 RVA: 0x0003D560 File Offset: 0x0003B760
			private void GetSlotNames(ref VBuffer<DvText> slotNames)
			{
				DvText[] array = slotNames.Values;
				if (Utils.Size<DvText>(array) < this._size)
				{
					array = new DvText[this._size];
				}
				for (int i = 0; i < this._size; i++)
				{
					array[i] = new DvText(string.Format("(Label_{0})", i));
				}
				slotNames = new VBuffer<DvText>(this._size, array, null);
			}

			// Token: 0x04000620 RID: 1568
			private ValueGetter<VBuffer<float>> _labelGetter;

			// Token: 0x04000621 RID: 1569
			private ValueGetter<VBuffer<float>> _scoreGetter;

			// Token: 0x04000622 RID: 1570
			private ValueGetter<float> _weightGetter;

			// Token: 0x04000623 RID: 1571
			private readonly int _size;

			// Token: 0x04000624 RID: 1572
			private VBuffer<float> _label;

			// Token: 0x04000625 RID: 1573
			private VBuffer<float> _score;

			// Token: 0x04000626 RID: 1574
			private float[] _labelArr;

			// Token: 0x04000627 RID: 1575
			private float[] _scoreArr;

			// Token: 0x04000628 RID: 1576
			private readonly MultiOutputRegressionEvaluator.Aggregator.Counters _counters;

			// Token: 0x04000629 RID: 1577
			private readonly MultiOutputRegressionEvaluator.Aggregator.Counters _weightedCounters;

			// Token: 0x02000200 RID: 512
			private sealed class Counters
			{
				// Token: 0x1700014F RID: 335
				// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0003D5D3 File Offset: 0x0003B7D3
				public double L1
				{
					get
					{
						if (this._sumWeights <= 0.0)
						{
							return 0.0;
						}
						return this._sumL1 / this._sumWeights;
					}
				}

				// Token: 0x17000150 RID: 336
				// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0003D5FD File Offset: 0x0003B7FD
				public double L2
				{
					get
					{
						if (this._sumWeights <= 0.0)
						{
							return 0.0;
						}
						return this._sumL2 / this._sumWeights;
					}
				}

				// Token: 0x17000151 RID: 337
				// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0003D627 File Offset: 0x0003B827
				public double Dist
				{
					get
					{
						if (this._sumWeights <= 0.0)
						{
							return 0.0;
						}
						return this._sumEuclidean / this._sumWeights;
					}
				}

				// Token: 0x17000152 RID: 338
				// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0003D654 File Offset: 0x0003B854
				public double[] PerLabelL1
				{
					get
					{
						double[] array = new double[this._l1Loss.Length];
						if (this._sumWeights == 0.0)
						{
							return array;
						}
						for (int i = 0; i < this._l1Loss.Length; i++)
						{
							array[i] = this._l1Loss[i] / this._sumWeights;
						}
						return array;
					}
				}

				// Token: 0x17000153 RID: 339
				// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0003D6A8 File Offset: 0x0003B8A8
				public double[] PerLabelL2
				{
					get
					{
						double[] array = new double[this._l2Loss.Length];
						if (this._sumWeights == 0.0)
						{
							return array;
						}
						for (int i = 0; i < this._l2Loss.Length; i++)
						{
							array[i] = this._l2Loss[i] / this._sumWeights;
						}
						return array;
					}
				}

				// Token: 0x17000154 RID: 340
				// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0003D6FC File Offset: 0x0003B8FC
				public double[] PerLabelRms
				{
					get
					{
						double[] array = new double[this._l2Loss.Length];
						if (this._sumWeights == 0.0)
						{
							return array;
						}
						for (int i = 0; i < this._l2Loss.Length; i++)
						{
							array[i] = Math.Sqrt(this._l2Loss[i] / this._sumWeights);
						}
						return array;
					}
				}

				// Token: 0x17000155 RID: 341
				// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0003D758 File Offset: 0x0003B958
				public double[] PerLabelLoss
				{
					get
					{
						double[] array = new double[this._fnLoss.Length];
						if (this._sumWeights == 0.0)
						{
							return array;
						}
						for (int i = 0; i < this._fnLoss.Length; i++)
						{
							array[i] = this._fnLoss[i] / this._sumWeights;
						}
						return array;
					}
				}

				// Token: 0x06000B62 RID: 2914 RVA: 0x0003D7AC File Offset: 0x0003B9AC
				public Counters(IRegressionLoss lossFunction, int size)
				{
					this._lossFunction = lossFunction;
					this._l1Loss = new double[size];
					this._l2Loss = new double[size];
					this._fnLoss = new double[size];
				}

				// Token: 0x06000B63 RID: 2915 RVA: 0x0003D7E0 File Offset: 0x0003B9E0
				public void Update(float[] score, float[] label, int length, float weight)
				{
					double num = (double)weight;
					double num2 = 0.0;
					double num3 = 0.0;
					for (int i = 0; i < length; i++)
					{
						double num4 = Math.Abs((double)label[i] - (double)score[i]);
						this._l1Loss[i] += num4 * num;
						this._l2Loss[i] += num4 * num4 * num;
						this._fnLoss[i] += this._lossFunction.Loss(score[i], label[i]) * num;
						num2 += num4;
						num3 += num4 * num4;
					}
					this._sumL1 += num2 * (double)weight;
					this._sumL2 += num3 * (double)weight;
					this._sumEuclidean += Math.Sqrt(num3) * (double)weight;
					this._sumWeights += (double)weight;
				}

				// Token: 0x0400062A RID: 1578
				private readonly double[] _l1Loss;

				// Token: 0x0400062B RID: 1579
				private readonly double[] _l2Loss;

				// Token: 0x0400062C RID: 1580
				private readonly double[] _fnLoss;

				// Token: 0x0400062D RID: 1581
				private double _sumWeights;

				// Token: 0x0400062E RID: 1582
				private double _sumL1;

				// Token: 0x0400062F RID: 1583
				private double _sumL2;

				// Token: 0x04000630 RID: 1584
				private double _sumEuclidean;

				// Token: 0x04000631 RID: 1585
				private readonly IRegressionLoss _lossFunction;
			}
		}
	}
}
