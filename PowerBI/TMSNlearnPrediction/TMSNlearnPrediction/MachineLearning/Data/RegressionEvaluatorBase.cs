using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001DA RID: 474
	public abstract class RegressionEvaluatorBase : RowToRowEvaluatorBase
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x00037A20 File Offset: 0x00035C20
		protected RegressionEvaluatorBase(RegressionEvaluatorBase.ArgumentsBase args, IHostEnvironment env, string registrationName)
			: base(env, registrationName)
		{
			Contracts.CheckUserArg(this._host, SubComponentExtensions.IsGood(args.lossFunction), "loss", "Loss function must be specified.");
			this._lossFunction = ComponentCatalog.CreateInstance<IRegressionLoss, SignatureRegressionLoss>(args.lossFunction);
		}

		// Token: 0x0400056B RID: 1387
		public const string L1 = "L1(avg)";

		// Token: 0x0400056C RID: 1388
		public const string L2 = "L2(avg)";

		// Token: 0x0400056D RID: 1389
		public const string Rms = "RMS(avg)";

		// Token: 0x0400056E RID: 1390
		public const string Loss = "Loss-fn(avg)";

		// Token: 0x0400056F RID: 1391
		public const string RSquared = "R Squared";

		// Token: 0x04000570 RID: 1392
		protected readonly IRegressionLoss _lossFunction;

		// Token: 0x020001DB RID: 475
		public abstract class ArgumentsBase
		{
			// Token: 0x04000571 RID: 1393
			[Argument(4, HelpText = "Loss function", ShortName = "loss")]
			public SubComponent<IRegressionLoss, SignatureRegressionLoss> lossFunction = new SubComponent<IRegressionLoss, SignatureRegressionLoss>("SquaredLoss");
		}

		// Token: 0x020001DC RID: 476
		protected abstract class RegressionAggregatorBase<TScore, TMetrics> : EvaluatorBase.AggregatorBase
		{
			// Token: 0x17000132 RID: 306
			// (get) Token: 0x06000A9A RID: 2714
			protected abstract RegressionEvaluatorBase.RegressionAggregatorBase<TScore, TMetrics>.CountersBase UnweightedCounters { get; }

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06000A9B RID: 2715
			protected abstract RegressionEvaluatorBase.RegressionAggregatorBase<TScore, TMetrics>.CountersBase WeightedCounters { get; }

			// Token: 0x06000A9C RID: 2716 RVA: 0x00037A73 File Offset: 0x00035C73
			protected RegressionAggregatorBase(IHostEnvironment env, IRegressionLoss lossFunction, string stratName)
				: base(env, stratName)
			{
				this._lossFunction = lossFunction;
			}

			// Token: 0x06000A9D RID: 2717 RVA: 0x00037A84 File Offset: 0x00035C84
			public override void InitializeNextPass(IRow row, RoleMappedSchema schema)
			{
				ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
				this._labelGetter = RowCursorUtils.GetLabelGetter(row, schema.Label.Index);
				this._scoreGetter = row.GetGetter<TScore>(uniqueColumn.Index);
				if (schema.Weight != null)
				{
					this._weightGetter = row.GetGetter<float>(schema.Weight.Index);
				}
			}

			// Token: 0x06000A9E RID: 2718 RVA: 0x00037AEC File Offset: 0x00035CEC
			public override void ProcessRow()
			{
				float num = 0f;
				this._labelGetter.Invoke(ref num);
				this._scoreGetter.Invoke(ref this._score);
				if (float.IsNaN(num))
				{
					this.NumUnlabeledInstances += 1L;
					return;
				}
				if (this.IsNaN(ref this._score))
				{
					this.NumBadScores += 1L;
				}
				float num2 = 1f;
				if (this._weightGetter != null)
				{
					this._weightGetter.Invoke(ref num2);
					if (!FloatUtils.IsFinite(num2))
					{
						this.NumBadWeights += 1L;
						num2 = 1f;
					}
				}
				this.ApplyLossFunction(ref this._score, num, ref this._loss);
				this.UnweightedCounters.Update(ref this._score, num, 1f, ref this._loss);
				if (this.WeightedCounters != null)
				{
					this.WeightedCounters.Update(ref this._score, num, num2, ref this._loss);
				}
			}

			// Token: 0x06000A9F RID: 2719
			protected abstract void ApplyLossFunction(ref TScore score, float label, ref TMetrics loss);

			// Token: 0x06000AA0 RID: 2720
			protected abstract bool IsNaN(ref TScore score);

			// Token: 0x06000AA1 RID: 2721 RVA: 0x00037BDC File Offset: 0x00035DDC
			public override Dictionary<string, IDataView> Finish()
			{
				ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(this._host);
				if (this.WeightedCounters != null)
				{
					arrayDataViewBuilder.AddColumn<DvBool>("IsWeighted", BoolType.Instance, new DvBool[]
					{
						DvBool.False,
						DvBool.True
					});
					this.AddColumn(arrayDataViewBuilder, "L1(avg)", new TMetrics[]
					{
						this.UnweightedCounters.L1,
						this.WeightedCounters.L1
					});
					this.AddColumn(arrayDataViewBuilder, "L2(avg)", new TMetrics[]
					{
						this.UnweightedCounters.L2,
						this.WeightedCounters.L2
					});
					this.AddColumn(arrayDataViewBuilder, "RMS(avg)", new TMetrics[]
					{
						this.UnweightedCounters.Rms,
						this.WeightedCounters.Rms
					});
					this.AddColumn(arrayDataViewBuilder, "Loss-fn(avg)", new TMetrics[]
					{
						this.UnweightedCounters.Loss,
						this.WeightedCounters.Loss
					});
					this.AddColumn(arrayDataViewBuilder, "R Squared", new TMetrics[]
					{
						this.UnweightedCounters.RSquared,
						this.WeightedCounters.RSquared
					});
				}
				else
				{
					this.AddColumn(arrayDataViewBuilder, "L1(avg)", new TMetrics[] { this.UnweightedCounters.L1 });
					this.AddColumn(arrayDataViewBuilder, "L2(avg)", new TMetrics[] { this.UnweightedCounters.L2 });
					this.AddColumn(arrayDataViewBuilder, "RMS(avg)", new TMetrics[] { this.UnweightedCounters.Rms });
					this.AddColumn(arrayDataViewBuilder, "Loss-fn(avg)", new TMetrics[] { this.UnweightedCounters.Loss });
					this.AddColumn(arrayDataViewBuilder, "R Squared", new TMetrics[] { this.UnweightedCounters.RSquared });
				}
				return new Dictionary<string, IDataView> { 
				{
					"OverallMetrics",
					arrayDataViewBuilder.GetDataView(null)
				} };
			}

			// Token: 0x06000AA2 RID: 2722
			protected abstract void AddColumn(ArrayDataViewBuilder dvBldr, string metricName, params TMetrics[] metric);

			// Token: 0x04000572 RID: 1394
			private ValueGetter<float> _labelGetter;

			// Token: 0x04000573 RID: 1395
			private ValueGetter<TScore> _scoreGetter;

			// Token: 0x04000574 RID: 1396
			private ValueGetter<float> _weightGetter;

			// Token: 0x04000575 RID: 1397
			protected TScore _score;

			// Token: 0x04000576 RID: 1398
			protected TMetrics _loss;

			// Token: 0x04000577 RID: 1399
			protected readonly IRegressionLoss _lossFunction;

			// Token: 0x020001DD RID: 477
			protected abstract class CountersBase
			{
				// Token: 0x17000134 RID: 308
				// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00037E58 File Offset: 0x00036058
				public TMetrics L1
				{
					get
					{
						TMetrics tmetrics = this.Zero();
						if (this._sumWeights > 0.0)
						{
							this.Normalize(ref this._totalL1Loss, ref tmetrics);
						}
						return tmetrics;
					}
				}

				// Token: 0x17000135 RID: 309
				// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00037E8C File Offset: 0x0003608C
				public TMetrics L2
				{
					get
					{
						TMetrics tmetrics = this.Zero();
						if (this._sumWeights > 0.0)
						{
							this.Normalize(ref this._totalL2Loss, ref tmetrics);
						}
						return tmetrics;
					}
				}

				// Token: 0x17000136 RID: 310
				// (get) Token: 0x06000AA5 RID: 2725
				public abstract TMetrics Rms { get; }

				// Token: 0x17000137 RID: 311
				// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00037EC0 File Offset: 0x000360C0
				public TMetrics Loss
				{
					get
					{
						TMetrics tmetrics = this.Zero();
						if (this._sumWeights > 0.0)
						{
							this.Normalize(ref this._totalLoss, ref tmetrics);
						}
						return tmetrics;
					}
				}

				// Token: 0x17000138 RID: 312
				// (get) Token: 0x06000AA7 RID: 2727
				public abstract TMetrics RSquared { get; }

				// Token: 0x06000AA8 RID: 2728 RVA: 0x00037EF4 File Offset: 0x000360F4
				public void Update(ref TScore score, float label, float weight, ref TMetrics loss)
				{
					this._sumWeights += (double)weight;
					this._totalLabelW += (double)(label * weight);
					this._totalLabelSquaredW += (double)(label * label * weight);
					this.UpdateCore(label, ref score, ref loss, weight);
				}

				// Token: 0x06000AA9 RID: 2729
				protected abstract void UpdateCore(float label, ref TScore score, ref TMetrics loss, float weight);

				// Token: 0x06000AAA RID: 2730
				protected abstract void Normalize(ref TMetrics src, ref TMetrics dst);

				// Token: 0x06000AAB RID: 2731
				protected abstract TMetrics Zero();

				// Token: 0x04000578 RID: 1400
				protected double _sumWeights;

				// Token: 0x04000579 RID: 1401
				protected TMetrics _totalL1Loss;

				// Token: 0x0400057A RID: 1402
				protected TMetrics _totalL2Loss;

				// Token: 0x0400057B RID: 1403
				protected TMetrics _totalLoss;

				// Token: 0x0400057C RID: 1404
				protected double _totalLabelW;

				// Token: 0x0400057D RID: 1405
				protected double _totalLabelSquaredW;
			}
		}
	}
}
