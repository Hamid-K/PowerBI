using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000247 RID: 583
	public sealed class BinaryClassifierEvaluator : RowToRowEvaluatorBase
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x00046CD0 File Offset: 0x00044ED0
		public BinaryClassifierEvaluator(BinaryClassifierEvaluator.Arguments args, IHostEnvironment env)
			: base(env, "BinaryClassifierEvaluator")
		{
			Contracts.CheckValue<BinaryClassifierEvaluator.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, args.maxAucExamples >= -1, "maxAucExamples", "Must be at least -1");
			Contracts.CheckUserArg(this._host, args.numRocExamples >= 0, "numRocExamples", "Must be non-negative");
			Contracts.CheckUserArg(this._host, args.numAuPrcExamples >= 0, "numAuPrcExamples", "Must be non-negative");
			this._useRaw = args.useRawScoreThreshold;
			this._threshold = args.threshold;
			this._prCount = args.numRocExamples;
			this._aucCount = args.maxAucExamples;
			this._auPrcCount = args.numAuPrcExamples;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00046D9C File Offset: 0x00044F9C
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
			if (columnType != NumberType.R4 && columnType != NumberType.R8 && columnType != BoolType.Instance && columnType.KeyCount != 2)
			{
				throw Contracts.Except(this._host, "Label column '{0}' has type '{1}' but must be R4, R8, BL or a 2-value key", new object[]
				{
					schema.Label.Name,
					columnType
				});
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00046E6C File Offset: 0x0004506C
		protected override void CheckCustomColumnTypesCore(RoleMappedSchema schema)
		{
			IReadOnlyList<ColumnInfo> columns = schema.GetColumns("Probability");
			if (columns != null)
			{
				Contracts.Check(this._host, columns.Count == 1, "Cannot have multiple probability columns");
				ColumnType type = columns[0].Type;
				if (type != NumberType.Float)
				{
					throw Contracts.Except(this._host, "Probability column '{0}' has type '{1}' but must be R4", new object[]
					{
						columns[0].Name,
						type
					});
				}
			}
			else if (!this._useRaw)
			{
				throw Contracts.Except(this._host, "Cannot compute the predicted label from the probability column because it does not exist");
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00046F3C File Offset: 0x0004513C
		protected override Func<int, bool> GetActiveColsCore(RoleMappedSchema schema)
		{
			Func<int, bool> pred = base.GetActiveColsCore(schema);
			IReadOnlyList<ColumnInfo> prob = schema.GetColumns("Probability");
			return (int i) => (Utils.Size<ColumnInfo>(prob) > 0 && i == prob[0].Index) || pred(i);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00046F80 File Offset: 0x00045180
		protected override EvaluatorBase.AggregatorBase GetAggregatorCore(RoleMappedSchema schema, string stratName)
		{
			DvText[] classNames = this.GetClassNames(schema);
			return new BinaryClassifierEvaluator.Aggregator(this._host, classNames, schema.Weight != null, this._aucCount, this._auPrcCount, this._threshold, this._useRaw, this._prCount, stratName);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00046FCC File Offset: 0x000451CC
		private DvText[] GetClassNames(RoleMappedSchema schema)
		{
			VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
			ColumnType metadataTypeOrNull;
			if (schema.Label.Type.IsKey && (metadataTypeOrNull = schema.Schema.GetMetadataTypeOrNull("KeyValues", schema.Label.Index)) != null && metadataTypeOrNull.ItemType.IsKnownSizeVector && metadataTypeOrNull.ItemType.IsText)
			{
				schema.Schema.GetMetadata<VBuffer<DvText>>("KeyValues", schema.Label.Index, ref vbuffer);
			}
			else
			{
				vbuffer = new VBuffer<DvText>(2, new DvText[]
				{
					new DvText("positive"),
					new DvText("negative")
				}, null);
			}
			DvText[] array = new DvText[2];
			vbuffer.CopyTo(array);
			return array;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000470A0 File Offset: 0x000452A0
		protected override IRowMapper CreatePerInstanceRowMapper(RoleMappedSchema schema)
		{
			Contracts.CheckValue<ColumnInfo>(schema.Label, "label", "Could not find the label column");
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			IReadOnlyList<ColumnInfo> columns = schema.GetColumns("Probability");
			string text = ((Utils.Size<ColumnInfo>(columns) > 0) ? columns[0].Name : null);
			return new BinaryPerInstanceEvaluator(this._host, schema.Schema, uniqueColumn.Name, text, schema.Label.Name, this._threshold, this._useRaw);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000473E4 File Offset: 0x000455E4
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("Accuracy", "Accuracy", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("PosPrec", "Positive precision", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("PosRecall", "Positive recall", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("NegPrec", "Negative precision", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("NegRecall", "Negative recall", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("AUC", "AUC", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("LogLoss", "Log-loss", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("LogLossReduction", "Log-loss reduction", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("Entropy", "Test-set entropy (prior Log-Loss/instance)", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("F1", "F1 Score", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("AUPRC", "AUPRC", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield break;
		}

		// Token: 0x0400071A RID: 1818
		public const string LoadName = "BinaryClassifierEvaluator";

		// Token: 0x0400071B RID: 1819
		public const string Accuracy = "Accuracy";

		// Token: 0x0400071C RID: 1820
		public const string PosPrecName = "Positive precision";

		// Token: 0x0400071D RID: 1821
		public const string PosRecallName = "Positive recall";

		// Token: 0x0400071E RID: 1822
		public const string NegPrecName = "Negative precision";

		// Token: 0x0400071F RID: 1823
		public const string NegRecallName = "Negative recall";

		// Token: 0x04000720 RID: 1824
		public const string Auc = "AUC";

		// Token: 0x04000721 RID: 1825
		public const string LogLoss = "Log-loss";

		// Token: 0x04000722 RID: 1826
		public const string LogLossReduction = "Log-loss reduction";

		// Token: 0x04000723 RID: 1827
		public const string Entropy = "Test-set entropy (prior Log-Loss/instance)";

		// Token: 0x04000724 RID: 1828
		public const string F1 = "F1 Score";

		// Token: 0x04000725 RID: 1829
		public const string AuPrc = "AUPRC";

		// Token: 0x04000726 RID: 1830
		public const string PrCurve = "PrCurve";

		// Token: 0x04000727 RID: 1831
		public const string Precision = "Precision";

		// Token: 0x04000728 RID: 1832
		public const string Recall = "Recall";

		// Token: 0x04000729 RID: 1833
		public const string FPR = "FPR";

		// Token: 0x0400072A RID: 1834
		public const string Threshold = "Threshold";

		// Token: 0x0400072B RID: 1835
		private readonly float _threshold;

		// Token: 0x0400072C RID: 1836
		private readonly bool _useRaw;

		// Token: 0x0400072D RID: 1837
		private readonly int _prCount;

		// Token: 0x0400072E RID: 1838
		private readonly int _aucCount;

		// Token: 0x0400072F RID: 1839
		private readonly int _auPrcCount;

		// Token: 0x02000248 RID: 584
		public sealed class Arguments
		{
			// Token: 0x04000730 RID: 1840
			[Argument(0, HelpText = "Probability value for classification thresholding")]
			public float threshold;

			// Token: 0x04000731 RID: 1841
			[Argument(0, HelpText = "Use raw score value instead of probability for classification thresholding", ShortName = "useRawScore")]
			public bool useRawScoreThreshold = true;

			// Token: 0x04000732 RID: 1842
			[Argument(0, HelpText = "The number of samples to use for p/r curve generation. Specify 0 for no p/r curve generation", ShortName = "numpr")]
			public int numRocExamples;

			// Token: 0x04000733 RID: 1843
			[Argument(0, HelpText = "The number of samples to use for AUC calculation. If 0, AUC is not computed. If -1, the whole dataset is used", ShortName = "numauc")]
			public int maxAucExamples = -1;

			// Token: 0x04000734 RID: 1844
			[Argument(0, HelpText = "The number of samples to use for AUPRC calculation. Specify 0 for no AUPRC calculation", ShortName = "numauprc")]
			public int numAuPrcExamples;
		}

		// Token: 0x02000249 RID: 585
		public enum Metrics
		{
			// Token: 0x04000736 RID: 1846
			[EnumValueDisplay("Accuracy")]
			Accuracy,
			// Token: 0x04000737 RID: 1847
			[EnumValueDisplay("Positive precision")]
			PosPrecName,
			// Token: 0x04000738 RID: 1848
			[EnumValueDisplay("Positive recall")]
			PosRecallName,
			// Token: 0x04000739 RID: 1849
			[EnumValueDisplay("Negative precision")]
			NegPrecName,
			// Token: 0x0400073A RID: 1850
			[EnumValueDisplay("Negative recall")]
			NegRecallName,
			// Token: 0x0400073B RID: 1851
			[EnumValueDisplay("AUC")]
			Auc,
			// Token: 0x0400073C RID: 1852
			[EnumValueDisplay("Log-loss")]
			LogLoss,
			// Token: 0x0400073D RID: 1853
			[EnumValueDisplay("Log-loss reduction")]
			LogLossReduction,
			// Token: 0x0400073E RID: 1854
			[EnumValueDisplay("F1 Score")]
			F1,
			// Token: 0x0400073F RID: 1855
			[EnumValueDisplay("AUPRC")]
			AuPrc
		}

		// Token: 0x0200024A RID: 586
		private sealed class Aggregator : EvaluatorBase.AggregatorBase
		{
			// Token: 0x06000D1A RID: 3354 RVA: 0x00047440 File Offset: 0x00045640
			public Aggregator(IHostEnvironment env, DvText[] classNames, bool weighted, int aucReservoirSize, int auPrcReservoirSize, float threshold, bool useRaw, int prCount, string stratName)
				: base(env, stratName)
			{
				this._classNames = classNames;
				this._counters = new BinaryClassifierEvaluator.Aggregator.Counters(useRaw, threshold);
				this._weightedCounters = (weighted ? new BinaryClassifierEvaluator.Aggregator.Counters(useRaw, threshold) : null);
				this._weighted = weighted;
				if (weighted)
				{
					this._aucAggregator = new EvaluatorBase.WeightedAucAggregator(this._host.Rand, aucReservoirSize);
					if (auPrcReservoirSize > 0)
					{
						this._auPrcAggregator = new EvaluatorBase.WeightedAuPrcAggregator(this._host.Rand, auPrcReservoirSize);
					}
				}
				else
				{
					this._aucAggregator = new EvaluatorBase.UnweightedAucAggregator(this._host.Rand, aucReservoirSize);
					if (auPrcReservoirSize > 0)
					{
						this._auPrcAggregator = new EvaluatorBase.UnweightedAuPrcAggregator(this._host.Rand, auPrcReservoirSize);
					}
				}
				if (prCount > 0)
				{
					ValueGetter<BinaryClassifierEvaluator.Aggregator.RocInfo> valueGetter = delegate(ref BinaryClassifierEvaluator.Aggregator.RocInfo dst)
					{
						dst.Label = this._label;
						dst.Score = this._score;
						dst.Weight = this._weight;
					};
					this._prCurveReservoir = new ReservoirSamplerWithoutReplacement<BinaryClassifierEvaluator.Aggregator.RocInfo>(this._host.Rand, prCount, valueGetter);
				}
			}

			// Token: 0x06000D1B RID: 3355 RVA: 0x00047534 File Offset: 0x00045734
			public override void InitializeNextPass(IRow row, RoleMappedSchema schema)
			{
				ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
				this._labelGetter = RowCursorUtils.GetLabelGetter(row, schema.Label.Index);
				this._scoreGetter = row.GetGetter<float>(uniqueColumn.Index);
				IReadOnlyList<ColumnInfo> columns = schema.GetColumns(new RoleMappedSchema.ColumnRole("Probability"));
				if (columns != null)
				{
					this._probGetter = row.GetGetter<float>(columns[0].Index);
				}
				else
				{
					this._probGetter = delegate(ref float value)
					{
						value = float.NaN;
					};
				}
				if (this._weighted)
				{
					this._weightGetter = row.GetGetter<float>(schema.Weight.Index);
				}
			}

			// Token: 0x06000D1C RID: 3356 RVA: 0x000475EC File Offset: 0x000457EC
			public override void ProcessRow()
			{
				this._labelGetter.Invoke(ref this._label);
				this._scoreGetter.Invoke(ref this._score);
				if (!FloatUtils.IsFinite(this._score))
				{
					this.NumBadScores += 1L;
					return;
				}
				if (float.IsNaN(this._label))
				{
					Console.WriteLine("NaN");
					this.NumUnlabeledInstances += 1L;
					return;
				}
				float num = 0f;
				this._probGetter.Invoke(ref num);
				double num2;
				if (!float.IsNaN(num))
				{
					if (this._label > 0f)
					{
						num2 = -Math.Log((double)num, 2.0);
					}
					else
					{
						num2 = -Math.Log(1.0 - (double)num, 2.0);
					}
				}
				else
				{
					num2 = double.NaN;
				}
				this._counters.Update(this._score, num, this._label, num2, 1f);
				if (this._weightGetter != null)
				{
					this._weightGetter.Invoke(ref this._weight);
					if (!FloatUtils.IsFinite(this._weight))
					{
						this.NumBadWeights += 1L;
						this._weight = 1f;
					}
					this._aucAggregator.ProcessRow(this._label, this._score, this._weight);
					this._weightedCounters.Update(this._score, num, this._label, num2, this._weight);
				}
				else
				{
					this._aucAggregator.ProcessRow(this._label, this._score, 1f);
				}
				if (this._prCurveReservoir != null)
				{
					this._prCurveReservoir.Sample();
				}
				if (this._auPrcAggregator != null)
				{
					this._auPrcAggregator.ProcessRow(this._label, this._score, this._weight);
				}
			}

			// Token: 0x06000D1D RID: 3357 RVA: 0x000477B4 File Offset: 0x000459B4
			public override Dictionary<string, IDataView> Finish()
			{
				this._aucAggregator.Finish();
				double num2;
				double num = this._aucAggregator.ComputeWeightedAuc(out num2);
				double num3 = 0.0;
				double num4 = 0.0;
				if (this._auPrcAggregator != null)
				{
					num4 = this._auPrcAggregator.ComputeWeightedAuPrc(out num3);
				}
				ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(this._host);
				if (this._weighted)
				{
					arrayDataViewBuilder.AddColumn<DvBool>("IsWeighted", BoolType.Instance, new DvBool[]
					{
						DvBool.False,
						DvBool.True
					});
					arrayDataViewBuilder.AddColumn<double>("AUC", NumberType.R8, new double[] { num2, num });
					arrayDataViewBuilder.AddColumn<double>("Accuracy", NumberType.R8, new double[]
					{
						this._counters.Acc,
						this._weightedCounters.Acc
					});
					arrayDataViewBuilder.AddColumn<double>("Positive precision", NumberType.R8, new double[]
					{
						this._counters.PrecisionPos,
						this._weightedCounters.PrecisionPos
					});
					arrayDataViewBuilder.AddColumn<double>("Positive recall", NumberType.R8, new double[]
					{
						this._counters.RecallPos,
						this._weightedCounters.RecallPos
					});
					arrayDataViewBuilder.AddColumn<double>("Negative precision", NumberType.R8, new double[]
					{
						this._counters.PrecisionNeg,
						this._weightedCounters.PrecisionNeg
					});
					arrayDataViewBuilder.AddColumn<double>("Negative recall", NumberType.R8, new double[]
					{
						this._counters.RecallNeg,
						this._weightedCounters.RecallNeg
					});
					arrayDataViewBuilder.AddColumn<double>("Log-loss", NumberType.R8, new double[]
					{
						this._counters.LogLoss,
						this._weightedCounters.LogLoss
					});
					arrayDataViewBuilder.AddColumn<double>("Log-loss reduction", NumberType.R8, new double[]
					{
						this._counters.LogLossReduction,
						this._weightedCounters.LogLossReduction
					});
					arrayDataViewBuilder.AddColumn<double>("Test-set entropy (prior Log-Loss/instance)", NumberType.R8, new double[]
					{
						this._counters.Entropy,
						this._weightedCounters.Entropy
					});
					arrayDataViewBuilder.AddColumn<double>("F1 Score", NumberType.R8, new double[]
					{
						this._counters.F1,
						this._weightedCounters.F1
					});
					if (this._auPrcAggregator != null)
					{
						arrayDataViewBuilder.AddColumn<double>("AUPRC", NumberType.R8, new double[] { num3, num4 });
					}
				}
				else
				{
					arrayDataViewBuilder.AddColumn<double>("AUC", NumberType.R8, new double[] { num2 });
					arrayDataViewBuilder.AddColumn<double>("Accuracy", NumberType.R8, new double[] { this._counters.Acc });
					arrayDataViewBuilder.AddColumn<double>("Positive precision", NumberType.R8, new double[] { this._counters.PrecisionPos });
					arrayDataViewBuilder.AddColumn<double>("Positive recall", NumberType.R8, new double[] { this._counters.RecallPos });
					arrayDataViewBuilder.AddColumn<double>("Negative precision", NumberType.R8, new double[] { this._counters.PrecisionNeg });
					arrayDataViewBuilder.AddColumn<double>("Negative recall", NumberType.R8, new double[] { this._counters.RecallNeg });
					arrayDataViewBuilder.AddColumn<double>("Log-loss", NumberType.R8, new double[] { this._counters.LogLoss });
					arrayDataViewBuilder.AddColumn<double>("Log-loss reduction", NumberType.R8, new double[] { this._counters.LogLossReduction });
					arrayDataViewBuilder.AddColumn<double>("Test-set entropy (prior Log-Loss/instance)", NumberType.R8, new double[] { this._counters.Entropy });
					arrayDataViewBuilder.AddColumn<double>("F1 Score", NumberType.R8, new double[] { this._counters.F1 });
					if (this._auPrcAggregator != null)
					{
						arrayDataViewBuilder.AddColumn<double>("AUPRC", NumberType.R8, new double[] { num3 });
					}
				}
				Dictionary<string, IDataView> dictionary = new Dictionary<string, IDataView>();
				dictionary.Add("OverallMetrics", arrayDataViewBuilder.GetDataView(null));
				this.AddOtherMetrics(dictionary);
				return dictionary;
			}

			// Token: 0x06000D1E RID: 3358 RVA: 0x00047CA8 File Offset: 0x00045EA8
			private void AddOtherMetrics(Dictionary<string, IDataView> metrics)
			{
				ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(this._host);
				VBuffer<DvText> vbuffer = new VBuffer<DvText>(this._classNames.Length, this._classNames, null);
				arrayDataViewBuilder.AddColumn<double>("Count", ref vbuffer, NumberType.R8, new double[][]
				{
					new double[]
					{
						this._counters.NumTruePos,
						this._counters.NumFalseNeg
					},
					new double[]
					{
						this._counters.NumFalsePos,
						this._counters.NumTrueNeg
					}
				});
				if (this._weightedCounters != null)
				{
					arrayDataViewBuilder.AddColumn<double>("Weight", ref vbuffer, NumberType.R8, new double[][]
					{
						new double[]
						{
							this._weightedCounters.NumTruePos,
							this._weightedCounters.NumFalseNeg
						},
						new double[]
						{
							this._weightedCounters.NumFalsePos,
							this._weightedCounters.NumTrueNeg
						}
					});
				}
				metrics.Add("ConfusionMatrix", arrayDataViewBuilder.GetDataView(null));
				if (this._prCurveReservoir != null)
				{
					metrics.Add("PrCurve", this.ComputePrCurves());
				}
			}

			// Token: 0x06000D1F RID: 3359 RVA: 0x00047E04 File Offset: 0x00046004
			private IDataView ComputePrCurves()
			{
				this._prCurveReservoir.Lock();
				IEnumerable<BinaryClassifierEvaluator.Aggregator.RocInfo> sample = this._prCurveReservoir.GetSample();
				List<double> list = new List<double>();
				List<double> list2 = new List<double>();
				List<double> list3 = new List<double>();
				List<float> list4 = new List<float>();
				List<double> list5 = ((this._weightedCounters != null) ? new List<double>() : null);
				List<double> list6 = ((this._weightedCounters != null) ? new List<double>() : null);
				List<double> list7 = ((this._weightedCounters != null) ? new List<double>() : null);
				double num = 0.0;
				double num2 = 0.0;
				double num3 = 0.0;
				double num4 = 0.0;
				foreach (BinaryClassifierEvaluator.Aggregator.RocInfo rocInfo in sample.OrderByDescending((BinaryClassifierEvaluator.Aggregator.RocInfo x) => x.Score))
				{
					if (rocInfo.Label > 0f)
					{
						num += 1.0;
					}
					else
					{
						num2 += 1.0;
					}
					list.Add(num / (num + num2));
					list2.Add(num);
					list3.Add(num2);
					if (list5 != null)
					{
						if (rocInfo.Label > 0f)
						{
							num3 += (double)rocInfo.Weight;
						}
						else
						{
							num4 += (double)rocInfo.Weight;
						}
						list5.Add(num3 / (num3 + num4));
						list6.Add(num3);
						list7.Add(num4);
					}
					list4.Add(rocInfo.Score);
				}
				for (int i = 0; i < list2.Count; i++)
				{
					List<double> list8;
					int num5;
					(list8 = list2)[num5 = i] = list8[num5] / num;
					List<double> list9;
					int num6;
					(list9 = list3)[num6 = i] = list9[num6] / num2;
				}
				if (list6 != null)
				{
					for (int j = 0; j < list6.Count; j++)
					{
						List<double> list10;
						int num7;
						(list10 = list6)[num7 = j] = list10[num7] / num3;
						List<double> list11;
						int num8;
						(list11 = list7)[num8 = j] = list11[num8] / num4;
					}
				}
				ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(this._host);
				arrayDataViewBuilder.AddColumn<float>("Threshold", NumberType.R4, list4.ToArray());
				arrayDataViewBuilder.AddColumn<double>("Precision", NumberType.R8, list.ToArray());
				arrayDataViewBuilder.AddColumn<double>("Recall", NumberType.R8, list2.ToArray());
				arrayDataViewBuilder.AddColumn<double>("FPR", NumberType.R8, list3.ToArray());
				if (list5 != null)
				{
					arrayDataViewBuilder.AddColumn<double>("Weighted Precision", NumberType.R8, list5.ToArray());
					arrayDataViewBuilder.AddColumn<double>("Weighted Recall", NumberType.R8, list6.ToArray());
					arrayDataViewBuilder.AddColumn<double>("Weighted FPR", NumberType.R8, list7.ToArray());
				}
				return arrayDataViewBuilder.GetDataView(null);
			}

			// Token: 0x04000740 RID: 1856
			private readonly ReservoirSamplerWithoutReplacement<BinaryClassifierEvaluator.Aggregator.RocInfo> _prCurveReservoir;

			// Token: 0x04000741 RID: 1857
			private readonly EvaluatorBase.AuPrcAggregatorBase _auPrcAggregator;

			// Token: 0x04000742 RID: 1858
			private readonly EvaluatorBase.AucAggregatorBase _aucAggregator;

			// Token: 0x04000743 RID: 1859
			private readonly BinaryClassifierEvaluator.Aggregator.Counters _counters;

			// Token: 0x04000744 RID: 1860
			private readonly BinaryClassifierEvaluator.Aggregator.Counters _weightedCounters;

			// Token: 0x04000745 RID: 1861
			private readonly bool _weighted;

			// Token: 0x04000746 RID: 1862
			private ValueGetter<float> _labelGetter;

			// Token: 0x04000747 RID: 1863
			private ValueGetter<float> _scoreGetter;

			// Token: 0x04000748 RID: 1864
			private ValueGetter<float> _weightGetter;

			// Token: 0x04000749 RID: 1865
			private ValueGetter<float> _probGetter;

			// Token: 0x0400074A RID: 1866
			private float _score;

			// Token: 0x0400074B RID: 1867
			private float _label;

			// Token: 0x0400074C RID: 1868
			private float _weight;

			// Token: 0x0400074D RID: 1869
			private readonly DvText[] _classNames;

			// Token: 0x0200024B RID: 587
			private sealed class Counters
			{
				// Token: 0x1700017E RID: 382
				// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0004810C File Offset: 0x0004630C
				public double Acc
				{
					get
					{
						return (this.NumTrueNeg + this.NumTruePos) / (this.NumTruePos + this.NumTrueNeg + this.NumFalseNeg + this.NumFalsePos);
					}
				}

				// Token: 0x1700017F RID: 383
				// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00048137 File Offset: 0x00046337
				public double RecallPos
				{
					get
					{
						if (this.NumTruePos + this.NumFalseNeg <= 0.0)
						{
							return 0.0;
						}
						return this.NumTruePos / (this.NumTruePos + this.NumFalseNeg);
					}
				}

				// Token: 0x17000180 RID: 384
				// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0004816F File Offset: 0x0004636F
				public double PrecisionPos
				{
					get
					{
						if (this.NumTruePos + this.NumFalsePos <= 0.0)
						{
							return 0.0;
						}
						return this.NumTruePos / (this.NumTruePos + this.NumFalsePos);
					}
				}

				// Token: 0x17000181 RID: 385
				// (get) Token: 0x06000D26 RID: 3366 RVA: 0x000481A7 File Offset: 0x000463A7
				public double RecallNeg
				{
					get
					{
						if (this.NumTrueNeg + this.NumFalsePos <= 0.0)
						{
							return 0.0;
						}
						return this.NumTrueNeg / (this.NumTrueNeg + this.NumFalsePos);
					}
				}

				// Token: 0x17000182 RID: 386
				// (get) Token: 0x06000D27 RID: 3367 RVA: 0x000481DF File Offset: 0x000463DF
				public double PrecisionNeg
				{
					get
					{
						if (this.NumTrueNeg + this.NumFalseNeg <= 0.0)
						{
							return 0.0;
						}
						return this.NumTrueNeg / (this.NumTrueNeg + this.NumFalseNeg);
					}
				}

				// Token: 0x17000183 RID: 387
				// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00048217 File Offset: 0x00046417
				public double Entropy
				{
					get
					{
						return MathUtils.Entropy((this.NumTruePos + this.NumFalseNeg) / (this.NumTruePos + this.NumTrueNeg + this.NumFalseNeg + this.NumFalsePos), false);
					}
				}

				// Token: 0x17000184 RID: 388
				// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00048248 File Offset: 0x00046448
				public double LogLoss
				{
					get
					{
						if (double.IsNaN(this._logLoss))
						{
							return double.NaN;
						}
						if (this._numLogLossPositives + this._numLogLossNegatives <= 0.0)
						{
							return 0.0;
						}
						return this._logLoss / (this._numLogLossPositives + this._numLogLossNegatives);
					}
				}

				// Token: 0x17000185 RID: 389
				// (get) Token: 0x06000D2A RID: 3370 RVA: 0x000482A4 File Offset: 0x000464A4
				public double LogLossReduction
				{
					get
					{
						if (this._numLogLossPositives + this._numLogLossNegatives == 0.0)
						{
							return 0.0;
						}
						double num = this._logLoss / (this._numLogLossPositives + this._numLogLossNegatives);
						double num2 = this._numLogLossPositives / (this._numLogLossPositives + this._numLogLossNegatives);
						double num3 = MathUtils.Entropy(num2, false);
						return 100.0 * (num3 - num) / num3;
					}
				}

				// Token: 0x17000186 RID: 390
				// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00048314 File Offset: 0x00046514
				public double F1
				{
					get
					{
						return 2.0 * this.PrecisionPos * this.RecallPos / (this.PrecisionPos + this.RecallPos);
					}
				}

				// Token: 0x06000D2C RID: 3372 RVA: 0x0004833B File Offset: 0x0004653B
				public Counters(bool useRaw, float threshold)
				{
					this._useRaw = useRaw;
					this._threshold = threshold;
				}

				// Token: 0x06000D2D RID: 3373 RVA: 0x00048354 File Offset: 0x00046554
				public void Update(float score, float prob, float label, double logloss, float weight)
				{
					bool flag = (this._useRaw ? (score > this._threshold) : (prob > this._threshold));
					if (label > 0f)
					{
						if (flag)
						{
							this.NumTruePos += (double)weight;
						}
						else
						{
							this.NumFalseNeg += (double)weight;
						}
					}
					else if (flag)
					{
						this.NumFalsePos += (double)weight;
					}
					else
					{
						this.NumTrueNeg += (double)weight;
					}
					if (!float.IsNaN(prob))
					{
						if (label > 0f)
						{
							this._numLogLossPositives += (double)weight;
						}
						else
						{
							this._numLogLossNegatives += (double)weight;
						}
					}
					this._logLoss += logloss * (double)weight;
				}

				// Token: 0x04000750 RID: 1872
				private readonly bool _useRaw;

				// Token: 0x04000751 RID: 1873
				private readonly float _threshold;

				// Token: 0x04000752 RID: 1874
				public double NumTruePos;

				// Token: 0x04000753 RID: 1875
				public double NumTrueNeg;

				// Token: 0x04000754 RID: 1876
				public double NumFalsePos;

				// Token: 0x04000755 RID: 1877
				public double NumFalseNeg;

				// Token: 0x04000756 RID: 1878
				private double _numLogLossPositives;

				// Token: 0x04000757 RID: 1879
				private double _numLogLossNegatives;

				// Token: 0x04000758 RID: 1880
				private double _logLoss;
			}

			// Token: 0x0200024C RID: 588
			private struct RocInfo
			{
				// Token: 0x04000759 RID: 1881
				public float Score;

				// Token: 0x0400075A RID: 1882
				public float Label;

				// Token: 0x0400075B RID: 1883
				public float Weight;
			}
		}
	}
}
