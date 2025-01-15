using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002CE RID: 718
	public sealed class MultiClassClassifierEvaluator : RowToRowEvaluatorBase
	{
		// Token: 0x06001074 RID: 4212 RVA: 0x0005ABEC File Offset: 0x00058DEC
		public MultiClassClassifierEvaluator(MultiClassClassifierEvaluator.Arguments args, IHostEnvironment env)
			: base(env, "MultiClassClassifierEvaluator")
		{
			Contracts.CheckUserArg(this._host, args.outputTopKAcc == null || args.outputTopKAcc > 0, "outputTopKAcc");
			this._outputTopKAcc = args.outputTopKAcc;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0005AC4C File Offset: 0x00058E4C
		protected override void CheckScoreAndLabelTypes(RoleMappedSchema schema)
		{
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			ColumnType columnType = uniqueColumn.Type;
			if (columnType.VectorSize < 2 || columnType.ItemType != NumberType.Float)
			{
				throw Contracts.Except(this._host, "Score column '{0}' has type {1} but must be a vector of two or more items of type R4", new object[] { uniqueColumn.Name, columnType });
			}
			Contracts.Check(this._host, schema.Label != null, "Could not find the label column");
			columnType = schema.Label.Type;
			if (columnType != NumberType.Float && columnType.KeyCount <= 0)
			{
				throw Contracts.Except(this._host, "Label column '{0}' has type {1} but must be a float or a known-cardinality key", new object[]
				{
					schema.Label.Name,
					columnType
				});
			}
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0005AD14 File Offset: 0x00058F14
		protected override EvaluatorBase.AggregatorBase GetAggregatorCore(RoleMappedSchema schema, string stratName)
		{
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			int vectorSize = uniqueColumn.Type.VectorSize;
			DvText[] classNames = this.GetClassNames(schema);
			return new MultiClassClassifierEvaluator.Aggregator(this._host, classNames, vectorSize, schema.Weight != null, this._outputTopKAcc, stratName);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0005AD74 File Offset: 0x00058F74
		private DvText[] GetClassNames(RoleMappedSchema schema)
		{
			VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
			ColumnType metadataTypeOrNull;
			DvText[] array;
			if (schema.Label.Type.IsKey && (metadataTypeOrNull = schema.Schema.GetMetadataTypeOrNull("KeyValues", schema.Label.Index)) != null && metadataTypeOrNull.ItemType.IsKnownSizeVector && metadataTypeOrNull.ItemType.IsText)
			{
				schema.Schema.GetMetadata<VBuffer<DvText>>("KeyValues", schema.Label.Index, ref vbuffer);
				array = new DvText[vbuffer.Length];
				vbuffer.CopyTo(array);
			}
			else
			{
				IReadOnlyList<ColumnInfo> columns = schema.GetColumns("Score");
				int vectorSize = columns[0].Type.VectorSize;
				array = (from i in Enumerable.Range(0, vectorSize)
					select new DvText(i.ToString())).ToArray<DvText>();
			}
			return array;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0005AE60 File Offset: 0x00059060
		protected override IRowMapper CreatePerInstanceRowMapper(RoleMappedSchema schema)
		{
			Contracts.CheckValue<ColumnInfo>(this._host, schema.Label, "Schema must contain a label column");
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			int vectorSize = uniqueColumn.Type.VectorSize;
			return new MultiClassPerInstanceEvaluator(this._host, schema.Schema, uniqueColumn.Name, schema.Label.Name, vectorSize);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0005B0B0 File Offset: 0x000592B0
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("AccuracyMicro", "Accuracy(micro-avg)", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("AccuracyMacro", "Accuracy(macro-avg)", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("TopKAccuracy", "Top K accuracy", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield return new MetricColumn("LogLoss<class name>", "Per class log-loss", MetricColumn.Objective.Minimize, true, true, new Regex(string.Format("^{0}(?<class>.+)", "Log-loss"), RegexOptions.IgnoreCase), "class", string.Format("{0} (class {{0}})", "Per class log-loss"));
			yield return new MetricColumn("LogLoss", "Log-loss", MetricColumn.Objective.Minimize, true, false, null, null, null);
			yield return new MetricColumn("LogLossReduction", "Log-loss-reduction", MetricColumn.Objective.Maximize, true, false, null, null, null);
			yield break;
		}

		// Token: 0x04000921 RID: 2337
		public const string AccuracyMicro = "Accuracy(micro-avg)";

		// Token: 0x04000922 RID: 2338
		public const string AccuracyMacro = "Accuracy(macro-avg)";

		// Token: 0x04000923 RID: 2339
		public const string TopKAccuracy = "Top K accuracy";

		// Token: 0x04000924 RID: 2340
		public const string PerClassLogLoss = "Per class log-loss";

		// Token: 0x04000925 RID: 2341
		public const string LogLoss = "Log-loss";

		// Token: 0x04000926 RID: 2342
		public const string LogLossReduction = "Log-loss-reduction";

		// Token: 0x04000927 RID: 2343
		public const string LoadName = "MultiClassClassifierEvaluator";

		// Token: 0x04000928 RID: 2344
		private readonly int? _outputTopKAcc;

		// Token: 0x020002CF RID: 719
		public sealed class Arguments
		{
			// Token: 0x0400092A RID: 2346
			[Argument(0, HelpText = "Output top K accuracy", ShortName = "topkacc")]
			public int? outputTopKAcc;
		}

		// Token: 0x020002D0 RID: 720
		public enum Metrics
		{
			// Token: 0x0400092C RID: 2348
			[EnumValueDisplay("Accuracy(micro-avg)")]
			AccuracyMicro,
			// Token: 0x0400092D RID: 2349
			[EnumValueDisplay("Accuracy(macro-avg)")]
			AccuracyMacro,
			// Token: 0x0400092E RID: 2350
			[EnumValueDisplay("Log-loss")]
			LogLoss,
			// Token: 0x0400092F RID: 2351
			[EnumValueDisplay("Log-loss-reduction")]
			LogLossReduction
		}

		// Token: 0x020002D1 RID: 721
		private sealed class Aggregator : EvaluatorBase.AggregatorBase
		{
			// Token: 0x0600107C RID: 4220 RVA: 0x0005B0D8 File Offset: 0x000592D8
			public Aggregator(IHostEnvironment env, DvText[] classNames, int scoreVectorSize, bool weighted, int? outputTopKAcc, string stratName)
				: base(env, stratName)
			{
				this._scoresArr = new float[scoreVectorSize];
				this._counters = new MultiClassClassifierEvaluator.Aggregator.Counters(scoreVectorSize, outputTopKAcc);
				this._weightedCounters = (weighted ? new MultiClassClassifierEvaluator.Aggregator.Counters(scoreVectorSize, outputTopKAcc) : null);
				this._classNames = classNames;
			}

			// Token: 0x0600107D RID: 4221 RVA: 0x0005B124 File Offset: 0x00059324
			public override void InitializeNextPass(IRow row, RoleMappedSchema schema)
			{
				ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
				this._labelGetter = RowCursorUtils.GetLabelGetter(row, schema.Label.Index);
				this._scoreGetter = row.GetGetter<VBuffer<float>>(uniqueColumn.Index);
				if (schema.Weight != null)
				{
					this._weightGetter = row.GetGetter<float>(schema.Weight.Index);
				}
			}

			// Token: 0x0600107E RID: 4222 RVA: 0x0005B194 File Offset: 0x00059394
			public override void ProcessRow()
			{
				float num = 0f;
				this._labelGetter.Invoke(ref num);
				if (float.IsNaN(num))
				{
					this.NumUnlabeledInstances += 1L;
					return;
				}
				if (num < 0f || num != (float)((int)num))
				{
					this._numNegOrNonIntegerLabels += 1L;
					return;
				}
				this._scoreGetter.Invoke(ref this._scores);
				Contracts.Check(this._host, this._scores.Length == this._scoresArr.Length);
				if (VBufferUtils.HasNaNs(ref this._scores) || VBufferUtils.HasNonFinite(ref this._scores))
				{
					this.NumBadScores += 1L;
					return;
				}
				this._scores.CopyTo(this._scoresArr);
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
				if (Utils.Size<int>(this._indicesArr) < this._scoresArr.Length)
				{
					this._indicesArr = new int[this._scoresArr.Length];
				}
				int num3 = 0;
				foreach (int num4 in from i in Enumerable.Range(0, this._scoresArr.Length)
					orderby this._scoresArr[i] descending
					select i)
				{
					this._indicesArr[num3++] = num4;
				}
				int num5 = (int)num;
				double num7;
				if (num5 < this._scoresArr.Length)
				{
					float num6 = Math.Min(1f, Math.Max(1E-15f, this._scoresArr[num5]));
					num7 = -Math.Log((double)num6);
				}
				else
				{
					num7 = -Math.Log(1.0000000036274937E-15);
					this._numUnknownClassInstances += 1L;
				}
				this._counters.Update(this._indicesArr, num7, num5, 1f);
				if (this._weightedCounters != null)
				{
					this._weightedCounters.Update(this._indicesArr, num7, num5, num2);
				}
			}

			// Token: 0x0600107F RID: 4223 RVA: 0x0005B3B0 File Offset: 0x000595B0
			public override Dictionary<string, IDataView> Finish()
			{
				return new Dictionary<string, IDataView>
				{
					{
						"OverallMetrics",
						this.GetOverallMetrics()
					},
					{
						"ConfusionMatrix",
						this.GetConfusionMatrix()
					}
				};
			}

			// Token: 0x06001080 RID: 4224 RVA: 0x0005B3E8 File Offset: 0x000595E8
			private IDataView GetOverallMetrics()
			{
				ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(this._host);
				VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
				this.GetSlotNames(ref vbuffer);
				if (this._weightedCounters == null)
				{
					arrayDataViewBuilder.AddColumn<double>("Accuracy(micro-avg)", NumberType.R8, new double[] { this._counters.MicroAvgAccuracy });
					arrayDataViewBuilder.AddColumn<double>("Accuracy(macro-avg)", NumberType.R8, new double[] { this._counters.MacroAvgAccuracy });
					arrayDataViewBuilder.AddColumn<double>("Log-loss", NumberType.R8, new double[] { this._counters.LogLoss });
					arrayDataViewBuilder.AddColumn<double>("Log-loss-reduction", NumberType.R8, new double[] { this._counters.Reduction });
					if (this._counters.OutputTopKAcc > 0)
					{
						arrayDataViewBuilder.AddColumn<double>("Top K accuracy", NumberType.R8, new double[] { this._counters.TopKAccuracy });
					}
					arrayDataViewBuilder.AddColumn<double>("Per class log-loss", ref vbuffer, NumberType.R8, new double[][] { this._counters.PerClassLogLoss });
				}
				else
				{
					arrayDataViewBuilder.AddColumn<DvBool>("IsWeighted", BoolType.Instance, new DvBool[]
					{
						DvBool.False,
						DvBool.True
					});
					arrayDataViewBuilder.AddColumn<double>("Accuracy(micro-avg)", NumberType.R8, new double[]
					{
						this._counters.MicroAvgAccuracy,
						this._weightedCounters.MicroAvgAccuracy
					});
					arrayDataViewBuilder.AddColumn<double>("Accuracy(macro-avg)", NumberType.R8, new double[]
					{
						this._counters.MacroAvgAccuracy,
						this._weightedCounters.MacroAvgAccuracy
					});
					arrayDataViewBuilder.AddColumn<double>("Log-loss", NumberType.R8, new double[]
					{
						this._counters.LogLoss,
						this._weightedCounters.LogLoss
					});
					arrayDataViewBuilder.AddColumn<double>("Log-loss-reduction", NumberType.R8, new double[]
					{
						this._counters.Reduction,
						this._weightedCounters.Reduction
					});
					if (this._counters.OutputTopKAcc > 0)
					{
						arrayDataViewBuilder.AddColumn<double>("Top K accuracy", NumberType.R8, new double[]
						{
							this._counters.TopKAccuracy,
							this._weightedCounters.TopKAccuracy
						});
					}
					arrayDataViewBuilder.AddColumn<double>("Per class log-loss", ref vbuffer, NumberType.R8, new double[][]
					{
						this._counters.PerClassLogLoss,
						this._weightedCounters.PerClassLogLoss
					});
				}
				return arrayDataViewBuilder.GetDataView(null);
			}

			// Token: 0x06001081 RID: 4225 RVA: 0x0005B6FC File Offset: 0x000598FC
			private IDataView GetConfusionMatrix()
			{
				ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(this._host);
				VBuffer<DvText> vbuffer = new VBuffer<DvText>(this._classNames.Length, this._classNames, null);
				arrayDataViewBuilder.AddColumn<double>("Count", ref vbuffer, NumberType.R8, this._counters.ConfusionTable);
				if (this._weightedCounters != null)
				{
					arrayDataViewBuilder.AddColumn<double>("Weight", ref vbuffer, NumberType.R8, this._weightedCounters.ConfusionTable);
				}
				return arrayDataViewBuilder.GetDataView(null);
			}

			// Token: 0x06001082 RID: 4226 RVA: 0x0005B784 File Offset: 0x00059984
			protected override List<string> GetWarningsCore()
			{
				List<string> warningsCore = base.GetWarningsCore();
				if (this._numUnknownClassInstances > 0L)
				{
					warningsCore.Add(string.Format("Found {0} test instances with class values not seen in the training set. LogLoss is reported higher than usual because of these instances.", this._numUnknownClassInstances));
				}
				if (this._numNegOrNonIntegerLabels > 0L)
				{
					warningsCore.Add(string.Format("Found {0} test instances with labels that are either negative or non integers. These instances were ignored", this._numNegOrNonIntegerLabels));
				}
				return warningsCore;
			}

			// Token: 0x06001083 RID: 4227 RVA: 0x0005B7E4 File Offset: 0x000599E4
			private void GetSlotNames(ref VBuffer<DvText> slotNames)
			{
				DvText[] array = slotNames.Values;
				if (Utils.Size<DvText>(array) < this._classNames.Length)
				{
					array = new DvText[this._classNames.Length];
				}
				for (int i = 0; i < this._classNames.Length; i++)
				{
					array[i] = new DvText(string.Format("(class {0})", this._classNames[i]));
				}
				slotNames = new VBuffer<DvText>(this._classNames.Length, array, null);
			}

			// Token: 0x04000930 RID: 2352
			private const float Epsilon = 1E-15f;

			// Token: 0x04000931 RID: 2353
			private ValueGetter<float> _labelGetter;

			// Token: 0x04000932 RID: 2354
			private ValueGetter<VBuffer<float>> _scoreGetter;

			// Token: 0x04000933 RID: 2355
			private ValueGetter<float> _weightGetter;

			// Token: 0x04000934 RID: 2356
			private VBuffer<float> _scores;

			// Token: 0x04000935 RID: 2357
			private readonly float[] _scoresArr;

			// Token: 0x04000936 RID: 2358
			private int[] _indicesArr;

			// Token: 0x04000937 RID: 2359
			private readonly MultiClassClassifierEvaluator.Aggregator.Counters _counters;

			// Token: 0x04000938 RID: 2360
			private readonly MultiClassClassifierEvaluator.Aggregator.Counters _weightedCounters;

			// Token: 0x04000939 RID: 2361
			private long _numUnknownClassInstances;

			// Token: 0x0400093A RID: 2362
			private long _numNegOrNonIntegerLabels;

			// Token: 0x0400093B RID: 2363
			private readonly DvText[] _classNames;

			// Token: 0x020002D2 RID: 722
			private sealed class Counters
			{
				// Token: 0x1700019D RID: 413
				// (get) Token: 0x06001085 RID: 4229 RVA: 0x0005B86F File Offset: 0x00059A6F
				public double MicroAvgAccuracy
				{
					get
					{
						if (this._numInstances <= 0.0)
						{
							return 0.0;
						}
						return this._numCorrect / this._numInstances;
					}
				}

				// Token: 0x1700019E RID: 414
				// (get) Token: 0x06001086 RID: 4230 RVA: 0x0005B89C File Offset: 0x00059A9C
				public double MacroAvgAccuracy
				{
					get
					{
						double num = 0.0;
						int num2 = 0;
						for (int i = 0; i < this._numClasses; i++)
						{
							if (this._sumWeightsOfClass[i] > 0.0)
							{
								num2++;
								num += this.ConfusionTable[i][i] / this._sumWeightsOfClass[i];
							}
						}
						return num / (double)num2;
					}
				}

				// Token: 0x1700019F RID: 415
				// (get) Token: 0x06001087 RID: 4231 RVA: 0x0005B8F7 File Offset: 0x00059AF7
				public double LogLoss
				{
					get
					{
						if (this._numInstances <= 0.0)
						{
							return 0.0;
						}
						return this._totalLogLoss / this._numInstances;
					}
				}

				// Token: 0x170001A0 RID: 416
				// (get) Token: 0x06001088 RID: 4232 RVA: 0x0005B924 File Offset: 0x00059B24
				public double Reduction
				{
					get
					{
						double num = 0.0;
						for (int i = 0; i < this._numClasses; i++)
						{
							if (this._sumWeightsOfClass[i] != 0.0)
							{
								num += this._sumWeightsOfClass[i] * Math.Log(this._sumWeightsOfClass[i] / this._numInstances);
							}
						}
						num /= -this._numInstances;
						return 100.0 * (num - this.LogLoss) / num;
					}
				}

				// Token: 0x170001A1 RID: 417
				// (get) Token: 0x06001089 RID: 4233 RVA: 0x0005B99D File Offset: 0x00059B9D
				public double TopKAccuracy
				{
					get
					{
						if (this._numInstances <= 0.0)
						{
							return 0.0;
						}
						return this._numCorrectTopK / this._numInstances;
					}
				}

				// Token: 0x170001A2 RID: 418
				// (get) Token: 0x0600108A RID: 4234 RVA: 0x0005B9C8 File Offset: 0x00059BC8
				public double[] PerClassLogLoss
				{
					get
					{
						double[] array = new double[this._totalPerClassLogLoss.Length];
						for (int i = 0; i < this._totalPerClassLogLoss.Length; i++)
						{
							array[i] = this._totalPerClassLogLoss[i] / this._sumWeightsOfClass[i];
						}
						return array;
					}
				}

				// Token: 0x0600108B RID: 4235 RVA: 0x0005BA0C File Offset: 0x00059C0C
				public Counters(int numClasses, int? outputTopKAcc)
				{
					this._numClasses = numClasses;
					this.OutputTopKAcc = outputTopKAcc;
					this._sumWeightsOfClass = new double[numClasses];
					this._totalPerClassLogLoss = new double[numClasses];
					this.ConfusionTable = new double[numClasses][];
					for (int i = 0; i < this.ConfusionTable.Length; i++)
					{
						this.ConfusionTable[i] = new double[numClasses];
					}
				}

				// Token: 0x0600108C RID: 4236 RVA: 0x0005BA74 File Offset: 0x00059C74
				public void Update(int[] indices, double loglossCurr, int label, float weight)
				{
					int num = indices[0];
					this._numInstances += (double)weight;
					if (label < this._numClasses)
					{
						this._sumWeightsOfClass[label] += (double)weight;
					}
					this._totalLogLoss += loglossCurr * (double)weight;
					if (label < this._numClasses)
					{
						this._totalPerClassLogLoss[label] += loglossCurr * (double)weight;
					}
					if (num == label)
					{
						this._numCorrect += (double)weight;
						this.ConfusionTable[label][label] += (double)weight;
						this._numCorrectTopK += (double)weight;
						return;
					}
					if (label < this._numClasses)
					{
						if (this.OutputTopKAcc > 0)
						{
							int num2 = Array.IndexOf<int>(indices, label);
							if (0 <= num2 && num2 < this.OutputTopKAcc)
							{
								this._numCorrectTopK += (double)weight;
							}
						}
						this.ConfusionTable[label][num] += (double)weight;
					}
				}

				// Token: 0x0400093C RID: 2364
				private readonly int _numClasses;

				// Token: 0x0400093D RID: 2365
				public readonly int? OutputTopKAcc;

				// Token: 0x0400093E RID: 2366
				private double _totalLogLoss;

				// Token: 0x0400093F RID: 2367
				private double _numInstances;

				// Token: 0x04000940 RID: 2368
				private double _numCorrect;

				// Token: 0x04000941 RID: 2369
				private double _numCorrectTopK;

				// Token: 0x04000942 RID: 2370
				private readonly double[] _sumWeightsOfClass;

				// Token: 0x04000943 RID: 2371
				private readonly double[] _totalPerClassLogLoss;

				// Token: 0x04000944 RID: 2372
				public readonly double[][] ConfusionTable;
			}
		}
	}
}
