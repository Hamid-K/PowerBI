using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000091 RID: 145
	public sealed class AnomalyDetectionEvaluator : EvaluatorBase
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00010658 File Offset: 0x0000E858
		public AnomalyDetectionEvaluator(AnomalyDetectionEvaluator.Arguments args, IHostEnvironment env)
			: base(env, "AnomalyDetectionEvaluator")
		{
			Contracts.CheckUserArg(this._host, args.k > 0, "k", "k must be positive");
			Contracts.CheckUserArg(this._host, 0.0 <= args.p && args.p <= 1.0, "p", "p must be in [0,1]");
			Contracts.CheckUserArg(this._host, args.numTopResults >= 0, "numTopResults", "Must be non-negative");
			Contracts.CheckUserArg(this._host, args.maxAucExamples >= -1, "maxAucExamples", "Must be at least -1");
			this._k = args.k;
			this._p = args.p;
			this._numTopResults = args.numTopResults;
			this._streaming = args.stream;
			this._aucCount = args.maxAucExamples;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0001074C File Offset: 0x0000E94C
		protected override void CheckScoreAndLabelTypes(RoleMappedSchema schema)
		{
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			ColumnType columnType = uniqueColumn.Type;
			if (columnType != NumberType.Float)
			{
				throw Contracts.Except(this._host, "Score column '{0}' has type '{1}' but must be R4", new object[] { uniqueColumn, columnType });
			}
			Contracts.Check(this._host, schema.Label != null, "Could not find the label column");
			columnType = schema.Label.Type;
			if (columnType != NumberType.Float && columnType.KeyCount != 2)
			{
				throw Contracts.Except(this._host, "Label column '{0}' has type '{1}' but must be R4 or a 2-value key", new object[]
				{
					schema.Label.Name,
					columnType
				});
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00010800 File Offset: 0x0000EA00
		protected override EvaluatorBase.AggregatorBase GetAggregatorCore(RoleMappedSchema schema, string stratName)
		{
			return new AnomalyDetectionEvaluator.Aggregator(this._host, this._aucCount, this._numTopResults, this._k, this._p, this._streaming, (schema.Name == null) ? (-1) : schema.Name.Index, stratName);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0001084D File Offset: 0x0000EA4D
		public override IDataTransform GetPerInstanceMetrics(RoleMappedData data)
		{
			return NopTransform.CreateIfNeeded(this._host, data.Data);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00010A54 File Offset: 0x0000EC54
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("DrAtK", "DR @K FP", MetricColumn.Objective.Maximize, false, false, null, null, null);
			yield return new MetricColumn("DrAtPFpr", "DR @P FPR", MetricColumn.Objective.Maximize, false, false, null, null, null);
			yield return new MetricColumn("DrAtNumPos", "DR @NumPos", MetricColumn.Objective.Maximize, false, false, null, null, null);
			yield return new MetricColumn("NumAnomalies", "NumAnomalies", MetricColumn.Objective.Info, false, false, null, null, null);
			yield return new MetricColumn("ThreshAtK", "Threshold @K FP", MetricColumn.Objective.Info, false, false, null, null, null);
			yield return new MetricColumn("ThreshAtP", "Threshold @P FPR", MetricColumn.Objective.Info, false, false, null, null, null);
			yield return new MetricColumn("ThreshAtNumPos", "Threshold @NumPos", MetricColumn.Objective.Info, false, false, null, null, null);
			yield break;
		}

		// Token: 0x0400011D RID: 285
		public const string LoadName = "AnomalyDetectionEvaluator";

		// Token: 0x0400011E RID: 286
		public const string TopKResults = "TopKResults";

		// Token: 0x0400011F RID: 287
		private readonly int _k;

		// Token: 0x04000120 RID: 288
		private readonly double _p;

		// Token: 0x04000121 RID: 289
		private readonly int _numTopResults;

		// Token: 0x04000122 RID: 290
		private readonly bool _streaming;

		// Token: 0x04000123 RID: 291
		private readonly int _aucCount;

		// Token: 0x02000092 RID: 146
		public sealed class Arguments
		{
			// Token: 0x04000124 RID: 292
			[Argument(0, HelpText = "Expected number of false positives")]
			public int k = 10;

			// Token: 0x04000125 RID: 293
			[Argument(0, HelpText = "Expected false positive rate")]
			public double p = 0.01;

			// Token: 0x04000126 RID: 294
			[Argument(0, HelpText = "Number of top-scored predictions to display", ShortName = "n")]
			public int numTopResults = 50;

			// Token: 0x04000127 RID: 295
			[Argument(0, HelpText = "Whether to calculate metrics in a streaming fashion")]
			public bool stream;

			// Token: 0x04000128 RID: 296
			[Argument(0, HelpText = "The number of samples to use for AUC calculation. If 0, AUC is not computed. If -1, the whole dataset is used", ShortName = "numauc")]
			public int maxAucExamples = -1;
		}

		// Token: 0x02000093 RID: 147
		public static class OverallMetrics
		{
			// Token: 0x04000129 RID: 297
			public const string DrAtK = "DR @K FP";

			// Token: 0x0400012A RID: 298
			public const string DrAtPFpr = "DR @P FPR";

			// Token: 0x0400012B RID: 299
			public const string DrAtNumPos = "DR @NumPos";

			// Token: 0x0400012C RID: 300
			public const string NumAnomalies = "NumAnomalies";

			// Token: 0x0400012D RID: 301
			public const string ThreshAtK = "Threshold @K FP";

			// Token: 0x0400012E RID: 302
			public const string ThreshAtP = "Threshold @P FPR";

			// Token: 0x0400012F RID: 303
			public const string ThreshAtNumPos = "Threshold @NumPos";
		}

		// Token: 0x02000094 RID: 148
		public static class TopKResultsColumns
		{
			// Token: 0x04000130 RID: 304
			public const string Instance = "Instance";

			// Token: 0x04000131 RID: 305
			public const string AnomalyScore = "Anomaly Score";

			// Token: 0x04000132 RID: 306
			public const string Label = "Label";
		}

		// Token: 0x02000095 RID: 149
		private sealed class Aggregator : EvaluatorBase.AggregatorBase
		{
			// Token: 0x060002B4 RID: 692 RVA: 0x00010AB4 File Offset: 0x0000ECB4
			public Aggregator(IHostEnvironment env, int reservoirSize, int topK, int k, double p, bool streaming, int nameIndex, string stratName)
				: base(env, stratName)
			{
				this._nameIndex = nameIndex;
				this._topExamples = new Heap<AnomalyDetectionEvaluator.Aggregator.TopExamplesInfo>((AnomalyDetectionEvaluator.Aggregator.TopExamplesInfo exampleA, AnomalyDetectionEvaluator.Aggregator.TopExamplesInfo exampleB) => exampleA.Score > exampleB.Score, topK);
				this._topK = topK;
				this._k = k;
				this._p = p;
				this._streaming = streaming;
				if (this._streaming)
				{
					this._counters = new AnomalyDetectionEvaluator.Aggregator.StreamingCounters(this._k, this._p);
				}
				else
				{
					this._counters = new AnomalyDetectionEvaluator.Aggregator.Counters(this._k, this._p);
				}
				this._aucAggregator = new EvaluatorBase.UnweightedAucAggregator(this._host.Rand, reservoirSize);
			}

			// Token: 0x060002B5 RID: 693 RVA: 0x00010B69 File Offset: 0x0000ED69
			private bool IsMainPass()
			{
				if (!this._streaming)
				{
					return this._passNum == 0;
				}
				return this._passNum == 1;
			}

			// Token: 0x060002B6 RID: 694 RVA: 0x00010B86 File Offset: 0x0000ED86
			protected override void FinishPassCore()
			{
				if (this._streaming && this._passNum == 0)
				{
					this._counters.FinishFirstPass();
				}
			}

			// Token: 0x060002B7 RID: 695 RVA: 0x00010BA3 File Offset: 0x0000EDA3
			public override bool IsActive()
			{
				return (this._streaming && this._passNum < 2) || this._passNum < 1;
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x00010BC4 File Offset: 0x0000EDC4
			private void AddOtherMetrics(Dictionary<string, IDataView> metrics)
			{
				ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(this._host);
				DvText[] array = new DvText[this._topExamples.Count];
				float[] array2 = new float[this._topExamples.Count];
				float[] array3 = new float[this._topExamples.Count];
				while (this._topExamples.Count > 0)
				{
					array[this._topExamples.Count - 1] = new DvText(this._topExamples.Top.Name);
					array2[this._topExamples.Count - 1] = this._topExamples.Top.Score;
					array3[this._topExamples.Count - 1] = this._topExamples.Top.Label;
					this._topExamples.Pop();
				}
				arrayDataViewBuilder.AddColumn<DvText>("Instance", TextType.Instance, array);
				arrayDataViewBuilder.AddColumn<float>("Anomaly Score", NumberType.R4, array2);
				arrayDataViewBuilder.AddColumn<float>("Label", NumberType.R4, array3);
				metrics.Add("TopKResults", arrayDataViewBuilder.GetDataView(null));
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x00010D24 File Offset: 0x0000EF24
			public override void InitializeNextPass(IRow row, RoleMappedSchema schema)
			{
				ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
				this._labelGetter = RowCursorUtils.GetLabelGetter(row, schema.Label.Index);
				this._scoreGetter = row.GetGetter<float>(uniqueColumn.Index);
				if (this.IsMainPass())
				{
					if (this._nameIndex < 0)
					{
						int rowCounter = 0;
						this._nameGetter = delegate(ref DvText dst)
						{
							dst = new DvText(rowCounter++.ToString());
						};
						return;
					}
					this._nameGetter = row.GetGetter<DvText>(this._nameIndex);
				}
			}

			// Token: 0x060002BA RID: 698 RVA: 0x00010DB0 File Offset: 0x0000EFB0
			public override void ProcessRow()
			{
				float num = 0f;
				this._labelGetter.Invoke(ref num);
				if (float.IsNaN(num))
				{
					if (this._passNum == 0)
					{
						this.NumUnlabeledInstances += 1L;
					}
					return;
				}
				float num2 = 0f;
				this._scoreGetter.Invoke(ref num2);
				if (!FloatUtils.IsFinite(num2))
				{
					if (this._passNum == 0)
					{
						this.NumBadScores += 1L;
					}
					return;
				}
				if (this._passNum == 0)
				{
					this._counters.UpdateCounts(num);
				}
				if (!this.IsMainPass())
				{
					return;
				}
				this._aucAggregator.ProcessRow(num, num2, 1f);
				this._counters.Update(num, num2);
				DvText dvText = default(DvText);
				this._nameGetter.Invoke(ref dvText);
				if (this._topExamples.Count >= this._topK)
				{
					AnomalyDetectionEvaluator.Aggregator.TopExamplesInfo top = this._topExamples.Top;
					if (num2 < top.Score)
					{
						return;
					}
					this._topExamples.Pop();
				}
				this._topExamples.Add(new AnomalyDetectionEvaluator.Aggregator.TopExamplesInfo
				{
					Score = num2,
					Label = num,
					Name = dvText.ToString()
				});
			}

			// Token: 0x060002BB RID: 699 RVA: 0x00010EE4 File Offset: 0x0000F0E4
			public override Dictionary<string, IDataView> Finish()
			{
				ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(this._host);
				this._aucAggregator.Finish();
				double num2;
				double num = this._aucAggregator.ComputeWeightedAuc(out num2);
				arrayDataViewBuilder.AddColumn<double>("AUC", NumberType.R8, new double[] { num });
				double num3;
				double num4;
				float num5;
				float num6;
				float num7;
				double metrics = this._counters.GetMetrics(this._k, this._p, out num3, out num4, out num5, out num6, out num7);
				arrayDataViewBuilder.AddColumn<double>("DR @K FP", NumberType.R8, new double[] { metrics });
				arrayDataViewBuilder.AddColumn<double>("DR @P FPR", NumberType.R8, new double[] { num3 });
				arrayDataViewBuilder.AddColumn<double>("DR @NumPos", NumberType.R8, new double[] { num4 });
				arrayDataViewBuilder.AddColumn<float>("Threshold @K FP", NumberType.R4, new float[] { num5 });
				arrayDataViewBuilder.AddColumn<float>("Threshold @P FPR", NumberType.R4, new float[] { num6 });
				arrayDataViewBuilder.AddColumn<float>("Threshold @NumPos", NumberType.R4, new float[] { num7 });
				DvInt8 dvInt = this._counters.NumAnomalies;
				arrayDataViewBuilder.AddColumn<DvInt8>("NumAnomalies", NumberType.I8, new DvInt8[] { dvInt });
				Dictionary<string, IDataView> dictionary = new Dictionary<string, IDataView>();
				dictionary.Add("OverallMetrics", arrayDataViewBuilder.GetDataView(null));
				this.AddOtherMetrics(dictionary);
				return dictionary;
			}

			// Token: 0x04000133 RID: 307
			private readonly Heap<AnomalyDetectionEvaluator.Aggregator.TopExamplesInfo> _topExamples;

			// Token: 0x04000134 RID: 308
			private readonly int _nameIndex;

			// Token: 0x04000135 RID: 309
			private readonly int _topK;

			// Token: 0x04000136 RID: 310
			private readonly int _k;

			// Token: 0x04000137 RID: 311
			private readonly double _p;

			// Token: 0x04000138 RID: 312
			private readonly AnomalyDetectionEvaluator.Aggregator.CountersBase _counters;

			// Token: 0x04000139 RID: 313
			private readonly bool _streaming;

			// Token: 0x0400013A RID: 314
			private readonly EvaluatorBase.UnweightedAucAggregator _aucAggregator;

			// Token: 0x0400013B RID: 315
			private ValueGetter<float> _labelGetter;

			// Token: 0x0400013C RID: 316
			private ValueGetter<float> _scoreGetter;

			// Token: 0x0400013D RID: 317
			private ValueGetter<DvText> _nameGetter;

			// Token: 0x02000096 RID: 150
			private abstract class CountersBase
			{
				// Token: 0x17000023 RID: 35
				// (get) Token: 0x060002BD RID: 701
				protected abstract long NumExamples { get; }

				// Token: 0x060002BE RID: 702 RVA: 0x0001107F File Offset: 0x0000F27F
				protected CountersBase(int k, double p)
				{
					this._k = k;
					this._p = p;
				}

				// Token: 0x060002BF RID: 703 RVA: 0x00011095 File Offset: 0x0000F295
				public void Update(float label, float score)
				{
					this.UpdateCore(label, score);
				}

				// Token: 0x060002C0 RID: 704
				protected abstract void UpdateCore(float label, float score);

				// Token: 0x060002C1 RID: 705 RVA: 0x000110B0 File Offset: 0x0000F2B0
				public double GetMetrics(int k, double p, out double drAtP, out double drAtNumPos, out float thresholdAtK, out float thresholdAtP, out float thresholdAtNumPos)
				{
					if (this.NumAnomalies == 0L || this.NumAnomalies == this.NumExamples)
					{
						thresholdAtK = (thresholdAtP = (thresholdAtNumPos = float.NaN));
						return drAtP = (drAtNumPos = double.NaN);
					}
					IEnumerable<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info> sortedExamples = this.GetSortedExamples();
					drAtP = this.DetectionRate(sortedExamples, (int)(p * (double)(this.NumExamples - this.NumAnomalies)), out thresholdAtP);
					drAtNumPos = (double)sortedExamples.Take((int)this.NumAnomalies).Count((AnomalyDetectionEvaluator.Aggregator.CountersBase.Info result) => result.Label > 0f) / (double)this.NumAnomalies;
					thresholdAtNumPos = sortedExamples.Take((int)this.NumAnomalies).Last<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info>().Score;
					return this.DetectionRate(sortedExamples, k, out thresholdAtK);
				}

				// Token: 0x060002C2 RID: 706
				protected abstract IEnumerable<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info> GetSortedExamples();

				// Token: 0x060002C3 RID: 707 RVA: 0x00011184 File Offset: 0x0000F384
				protected double DetectionRate(IEnumerable<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info> sortedExamples, int maxFalsePositives, out float threshold)
				{
					int num = 0;
					int num2 = 0;
					threshold = float.PositiveInfinity;
					foreach (AnomalyDetectionEvaluator.Aggregator.CountersBase.Info info in sortedExamples)
					{
						threshold = info.Score;
						if (info.Label > 0f)
						{
							num++;
						}
						else if (++num2 > maxFalsePositives)
						{
							break;
						}
					}
					return (double)num / (double)this.NumAnomalies;
				}

				// Token: 0x060002C4 RID: 708 RVA: 0x00011200 File Offset: 0x0000F400
				public void UpdateCounts(float label)
				{
					this._numExamples += 1L;
					if (label > 0f)
					{
						this.NumAnomalies += 1L;
					}
				}

				// Token: 0x060002C5 RID: 709 RVA: 0x00011228 File Offset: 0x0000F428
				public virtual void FinishFirstPass()
				{
				}

				// Token: 0x060002C6 RID: 710 RVA: 0x0001122C File Offset: 0x0000F42C
				protected IEnumerable<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info> ReverseHeap(Heap<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info> heap)
				{
					AnomalyDetectionEvaluator.Aggregator.CountersBase.Info[] array = new AnomalyDetectionEvaluator.Aggregator.CountersBase.Info[heap.Count];
					while (heap.Count > 0)
					{
						array[heap.Count - 1] = heap.Pop();
					}
					return array;
				}

				// Token: 0x0400013F RID: 319
				public long NumAnomalies;

				// Token: 0x04000140 RID: 320
				protected long _numExamples;

				// Token: 0x04000141 RID: 321
				protected readonly int _k;

				// Token: 0x04000142 RID: 322
				protected readonly double _p;

				// Token: 0x02000097 RID: 151
				protected struct Info
				{
					// Token: 0x060002C8 RID: 712 RVA: 0x0001126A File Offset: 0x0000F46A
					public Info(float label, float score)
					{
						this.Label = label;
						this.Score = score;
					}

					// Token: 0x04000144 RID: 324
					public readonly float Label;

					// Token: 0x04000145 RID: 325
					public readonly float Score;
				}
			}

			// Token: 0x02000098 RID: 152
			private sealed class Counters : AnomalyDetectionEvaluator.Aggregator.CountersBase
			{
				// Token: 0x17000024 RID: 36
				// (get) Token: 0x060002C9 RID: 713 RVA: 0x0001127A File Offset: 0x0000F47A
				protected override long NumExamples
				{
					get
					{
						return (long)this._examples.Count;
					}
				}

				// Token: 0x060002CA RID: 714 RVA: 0x00011288 File Offset: 0x0000F488
				public Counters(int k, double p)
					: base(k, p)
				{
					this._examples = new List<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info>();
				}

				// Token: 0x060002CB RID: 715 RVA: 0x0001129D File Offset: 0x0000F49D
				protected override void UpdateCore(float label, float score)
				{
					this._examples.Add(new AnomalyDetectionEvaluator.Aggregator.CountersBase.Info(label, score));
				}

				// Token: 0x060002CC RID: 716 RVA: 0x000112C4 File Offset: 0x0000F4C4
				protected override IEnumerable<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info> GetSortedExamples()
				{
					double num = this._p * (double)(this._numExamples - this.NumAnomalies);
					if (num < (double)this._k)
					{
						num = (double)this._k;
					}
					if (num < (double)this.NumAnomalies)
					{
						num = (double)this.NumAnomalies;
					}
					int num2 = (int)Math.Ceiling(num);
					Heap<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info> heap = new Heap<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info>((AnomalyDetectionEvaluator.Aggregator.CountersBase.Info info1, AnomalyDetectionEvaluator.Aggregator.CountersBase.Info info2) => info1.Score > info2.Score);
					int num3 = 0;
					foreach (AnomalyDetectionEvaluator.Aggregator.CountersBase.Info info in this._examples)
					{
						if (num3 < num2)
						{
							heap.Add(info);
							if (info.Label <= 0f)
							{
								num3++;
							}
						}
						else if (info.Score >= heap.Top.Score)
						{
							heap.Add(info);
							if (info.Label <= 0f)
							{
								while (heap.Pop().Label > 0f)
								{
								}
							}
						}
					}
					return base.ReverseHeap(heap);
				}

				// Token: 0x04000146 RID: 326
				private readonly List<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info> _examples;
			}

			// Token: 0x02000099 RID: 153
			private sealed class StreamingCounters : AnomalyDetectionEvaluator.Aggregator.CountersBase
			{
				// Token: 0x17000025 RID: 37
				// (get) Token: 0x060002CE RID: 718 RVA: 0x000113DC File Offset: 0x0000F5DC
				protected override long NumExamples
				{
					get
					{
						return this._numExamples;
					}
				}

				// Token: 0x060002CF RID: 719 RVA: 0x000113F6 File Offset: 0x0000F5F6
				public StreamingCounters(int k, double p)
					: base(k, p)
				{
					this._examples = new Heap<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info>((AnomalyDetectionEvaluator.Aggregator.CountersBase.Info info1, AnomalyDetectionEvaluator.Aggregator.CountersBase.Info info2) => info1.Score > info2.Score);
				}

				// Token: 0x060002D0 RID: 720 RVA: 0x00011428 File Offset: 0x0000F628
				protected override void UpdateCore(float label, float score)
				{
					if (this._numFalsePos < this._maxNumFalsePos)
					{
						this._examples.Add(new AnomalyDetectionEvaluator.Aggregator.CountersBase.Info(label, score));
						if (label <= 0f)
						{
							this._numFalsePos++;
						}
						return;
					}
					if (score < this._examples.Top.Score)
					{
						return;
					}
					this._examples.Add(new AnomalyDetectionEvaluator.Aggregator.CountersBase.Info(label, score));
					if (label <= 0f)
					{
						while (this._examples.Pop().Label > 0f)
						{
						}
					}
				}

				// Token: 0x060002D1 RID: 721 RVA: 0x000114B4 File Offset: 0x0000F6B4
				public override void FinishFirstPass()
				{
					double num = this._p * (double)(this._numExamples - this.NumAnomalies);
					if (num < (double)this._k)
					{
						num = (double)this._k;
					}
					if (num < (double)this.NumAnomalies)
					{
						num = (double)this.NumAnomalies;
					}
					this._maxNumFalsePos = (int)Math.Ceiling(num);
				}

				// Token: 0x060002D2 RID: 722 RVA: 0x00011508 File Offset: 0x0000F708
				protected override IEnumerable<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info> GetSortedExamples()
				{
					return base.ReverseHeap(this._examples);
				}

				// Token: 0x04000148 RID: 328
				private readonly Heap<AnomalyDetectionEvaluator.Aggregator.CountersBase.Info> _examples;

				// Token: 0x04000149 RID: 329
				private int _numFalsePos;

				// Token: 0x0400014A RID: 330
				private int _maxNumFalsePos;
			}

			// Token: 0x0200009A RID: 154
			private struct TopExamplesInfo
			{
				// Token: 0x0400014C RID: 332
				public float Score;

				// Token: 0x0400014D RID: 333
				public float Label;

				// Token: 0x0400014E RID: 334
				public string Name;
			}
		}
	}
}
