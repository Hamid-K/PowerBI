using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001E5 RID: 485
	public sealed class RankerEvaluator : EvaluatorBase
	{
		// Token: 0x06000AD6 RID: 2774 RVA: 0x00039504 File Offset: 0x00037704
		public RankerEvaluator(RankerEvaluator.Arguments args, IHostEnvironment env)
			: base(env, "RankingEvaluator")
		{
			if (args.dcgTruncationLevel <= 0 || args.dcgTruncationLevel > 10)
			{
				throw Contracts.ExceptUserArg(this._host, "dcgTruncationLevel", "DCG Truncation Level must be between 1 and {0}", new object[] { 10 });
			}
			Contracts.CheckUserArg(this._host, args.labelGains != null, "labelGains", "Label gains cannot be null");
			this._truncationLevel = args.dcgTruncationLevel;
			this._groupSummary = args.outputGroupSummary;
			List<double> list = new List<double>();
			string[] array = args.labelGains.Split(new char[] { ',' });
			for (int i = 0; i < array.Length; i++)
			{
				double num;
				if (!double.TryParse(array[i], out num))
				{
					throw Contracts.ExceptUserArg(this._host, "labelGains", "Label Gains must be of floating or integral type", new object[] { 10 });
				}
				list.Add(num);
			}
			this._labelGains = list.ToArray();
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00039610 File Offset: 0x00037810
		protected override void CheckScoreAndLabelTypes(RoleMappedSchema schema)
		{
			ColumnType type = schema.Label.Type;
			if (type != NumberType.Float && !type.IsKey)
			{
				throw Contracts.ExceptUserArg(this._host, "labelColumn", "Label column '{0}' has type '{1}' but must be R4 or a key", new object[]
				{
					schema.Label.Name,
					type
				});
			}
			ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
			if (uniqueColumn.Type != NumberType.Float)
			{
				throw Contracts.ExceptUserArg(this._host, "scoreColumn", "Score column '{0}' has type '{1}' but must be R4", new object[] { uniqueColumn.Name, type });
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000396B4 File Offset: 0x000378B4
		protected override void CheckCustomColumnTypesCore(RoleMappedSchema schema)
		{
			ColumnType type = schema.Group.Type;
			if (!type.IsKey)
			{
				throw Contracts.ExceptUserArg(this._host, "groupIdColumn", "Group column '{0}' has type '{1}' but must be a key", new object[]
				{
					schema.Group.Name,
					type
				});
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00039730 File Offset: 0x00037930
		protected override Func<int, bool> GetActiveColsCore(RoleMappedSchema schema)
		{
			Func<int, bool> pred = base.GetActiveColsCore(schema);
			return (int i) => i == schema.Group.Index || pred(i);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00039768 File Offset: 0x00037968
		protected override EvaluatorBase.AggregatorBase GetAggregatorCore(RoleMappedSchema schema, string stratName)
		{
			return new RankerEvaluator.Aggregator(this._host, this._labelGains, this._truncationLevel, this._groupSummary, schema.Weight != null, stratName);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00039794 File Offset: 0x00037994
		public override IDataTransform GetPerInstanceMetrics(RoleMappedData data)
		{
			Contracts.CheckValue<ColumnInfo>(this._host, data.Schema.Label, "Schema must contain a label column");
			ColumnInfo uniqueColumn = data.Schema.GetUniqueColumn("Score");
			Contracts.CheckValue<ColumnInfo>(this._host, data.Schema.Group, "Schema must contain a group column");
			return new RankerPerInstanceTransform(this._host, data.Data, data.Schema.Label.Name, uniqueColumn.Name, data.Schema.Group.Name, this._truncationLevel, this._labelGains);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000399DC File Offset: 0x00037BDC
		public override IEnumerable<MetricColumn> GetOverallMetricColumns()
		{
			yield return new MetricColumn("NDCG@<number>", "NDCG", MetricColumn.Objective.Maximize, true, true, new Regex(string.Format("^{0}@(?<at>\\d+)", "NDCG"), RegexOptions.IgnoreCase), "at", string.Format("{0} @{{0}}", "NDCG"));
			yield return new MetricColumn("DCG@<number>", "DCG", MetricColumn.Objective.Maximize, true, true, new Regex(string.Format("^{0}@(?<at>\\d+)", "DCG"), RegexOptions.IgnoreCase), "at", string.Format("{0} @{{0}}", "DCG"));
			yield return new MetricColumn("MaxDcg@<number>", "MaxDCG", MetricColumn.Objective.Maximize, true, true, new Regex(string.Format("^{0}@(?<at>\\d+)", "MaxDCG"), RegexOptions.IgnoreCase), "at", string.Format("{0} @{{0}}", "MaxDCG"));
			yield break;
		}

		// Token: 0x04000592 RID: 1426
		public const string LoadName = "RankingEvaluator";

		// Token: 0x04000593 RID: 1427
		private const string NDCG = "NDCG";

		// Token: 0x04000594 RID: 1428
		private const string DCG = "DCG";

		// Token: 0x04000595 RID: 1429
		private const string MaxDcg = "MaxDCG";

		// Token: 0x04000596 RID: 1430
		public const string GroupSummary = "GroupSummary";

		// Token: 0x04000597 RID: 1431
		private const string GroupId = "GroupId";

		// Token: 0x04000598 RID: 1432
		private readonly int _truncationLevel;

		// Token: 0x04000599 RID: 1433
		private readonly bool _groupSummary;

		// Token: 0x0400059A RID: 1434
		private readonly double[] _labelGains;

		// Token: 0x020001E6 RID: 486
		public sealed class Arguments
		{
			// Token: 0x0400059B RID: 1435
			[Argument(0, HelpText = "Maximum truncation level for computing (N)DCG", ShortName = "t")]
			public int dcgTruncationLevel = 3;

			// Token: 0x0400059C RID: 1436
			[Argument(0, HelpText = "Label relevance gains", ShortName = "gains")]
			public string labelGains = "0,3,7,15,31";

			// Token: 0x0400059D RID: 1437
			[Argument(0, HelpText = "Generate per-group (N)DCG", ShortName = "ogs")]
			public bool outputGroupSummary;
		}

		// Token: 0x020001E7 RID: 487
		private sealed class Aggregator : EvaluatorBase.AggregatorBase
		{
			// Token: 0x06000ADE RID: 2782 RVA: 0x00039A14 File Offset: 0x00037C14
			public Aggregator(IHostEnvironment env, double[] labelGains, int truncationLevel, bool groupSummary, bool weighted, string stratName)
				: base(env, stratName)
			{
				this._counters = new RankerEvaluator.Aggregator.Counters(labelGains, truncationLevel, groupSummary);
				this._weightedCounters = (weighted ? new RankerEvaluator.Aggregator.Counters(labelGains, truncationLevel, false) : null);
				this._currentQueryWeight = float.NaN;
				if (groupSummary)
				{
					this._groupId = new List<DvText>();
				}
			}

			// Token: 0x06000ADF RID: 2783 RVA: 0x00039A8C File Offset: 0x00037C8C
			public override void InitializeNextPass(IRow row, RoleMappedSchema schema)
			{
				ColumnInfo uniqueColumn = schema.GetUniqueColumn("Score");
				this._labelGetter = RowCursorUtils.GetLabelGetter(row, schema.Label.Index);
				this._scoreGetter = row.GetGetter<float>(uniqueColumn.Index);
				this._newGroupDel = RowCursorUtils.GetIsNewGroupDelegate(row, schema.Group.Index);
				if (schema.Weight != null)
				{
					this._weightGetter = row.GetGetter<float>(schema.Weight.Index);
				}
				if (this._counters.GroupSummary)
				{
					ValueGetter<StringBuilder> groupIdBuilder = RowCursorUtils.GetGetterAsStringBuilder(row, schema.Group.Index);
					this._groupSbUpdate = delegate
					{
						groupIdBuilder.Invoke(ref this._groupSb);
					};
					return;
				}
				this._groupSbUpdate = delegate
				{
				};
			}

			// Token: 0x06000AE0 RID: 2784 RVA: 0x00039B70 File Offset: 0x00037D70
			public override void ProcessRow()
			{
				if (this._newGroupDel())
				{
					if (this._groupSize > 0)
					{
						this.ProcessGroup();
						this._groupSize = 0;
					}
					this._groupSbUpdate();
				}
				float num = 0f;
				float num2 = 0f;
				this._labelGetter.Invoke(ref num);
				this._scoreGetter.Invoke(ref num2);
				if (float.IsNaN(num2))
				{
					this.NumBadScores += 1L;
					return;
				}
				this._counters.Update((short)num, num2);
				if (this._weightedCounters != null)
				{
					this._weightedCounters.Update((short)num, num2);
				}
				this._groupSize++;
				float num3 = 1f;
				if (this._weightGetter != null)
				{
					this._weightGetter.Invoke(ref num3);
					if (float.IsNaN(this._currentQueryWeight))
					{
						this._currentQueryWeight = num3;
						return;
					}
					Contracts.Check(num3 == this._currentQueryWeight, "Weights within query differ");
				}
			}

			// Token: 0x06000AE1 RID: 2785 RVA: 0x00039C60 File Offset: 0x00037E60
			private void ProcessGroup()
			{
				this._counters.UpdateGroup(1f);
				if (this._weightedCounters != null)
				{
					this._weightedCounters.UpdateGroup(this._currentQueryWeight);
				}
				if (this._groupId != null)
				{
					this._groupId.Add(new DvText(this._groupSb.ToString()));
				}
				this._currentQueryWeight = float.NaN;
			}

			// Token: 0x06000AE2 RID: 2786 RVA: 0x00039CC4 File Offset: 0x00037EC4
			protected override void FinishPassCore()
			{
				base.FinishPassCore();
				if (this._groupSize > 0)
				{
					this.ProcessGroup();
				}
			}

			// Token: 0x06000AE3 RID: 2787 RVA: 0x00039CF0 File Offset: 0x00037EF0
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
					arrayDataViewBuilder.AddColumn<double>("NDCG", ref vbuffer, NumberType.R8, new double[][]
					{
						this._counters.Ndcg,
						this._weightedCounters.Ndcg
					});
					arrayDataViewBuilder.AddColumn<double>("DCG", ref vbuffer, NumberType.R8, new double[][]
					{
						this._counters.Dcg,
						this._weightedCounters.Dcg
					});
				}
				else
				{
					arrayDataViewBuilder.AddColumn<double>("NDCG", ref vbuffer, NumberType.R8, new double[][] { this._counters.Ndcg });
					arrayDataViewBuilder.AddColumn<double>("DCG", ref vbuffer, NumberType.R8, new double[][] { this._counters.Dcg });
				}
				Dictionary<string, IDataView> dictionary = new Dictionary<string, IDataView>();
				dictionary.Add("OverallMetrics", arrayDataViewBuilder.GetDataView(null));
				if (this._counters.GroupSummary)
				{
					ArrayDataViewBuilder arrayDataViewBuilder2 = new ArrayDataViewBuilder(this._host);
					arrayDataViewBuilder2.AddColumn<DvText>("GroupId", TextType.Instance, this._groupId.Select((DvText sb) => new DvText(sb.ToString())).ToArray<DvText>());
					VBuffer<DvText> vbuffer2 = default(VBuffer<DvText>);
					this.GetGroupSummarySlotNames(ref vbuffer2, "NDCG");
					arrayDataViewBuilder2.AddColumn<double>("NDCG", ref vbuffer2, NumberType.R8, this._counters.GroupNdcg);
					VBuffer<DvText> vbuffer3 = default(VBuffer<DvText>);
					this.GetGroupSummarySlotNames(ref vbuffer3, "DCG");
					arrayDataViewBuilder2.AddColumn<double>("DCG", ref vbuffer3, NumberType.R8, this._counters.GroupDcg);
					VBuffer<DvText> vbuffer4 = default(VBuffer<DvText>);
					this.GetGroupSummarySlotNames(ref vbuffer4, "MaxDCG");
					arrayDataViewBuilder2.AddColumn<double>("MaxDCG", ref vbuffer4, NumberType.R8, this._counters.GroupMaxDcg);
					dictionary.Add("GroupSummary", arrayDataViewBuilder2.GetDataView(null));
				}
				return dictionary;
			}

			// Token: 0x06000AE4 RID: 2788 RVA: 0x00039F5C File Offset: 0x0003815C
			private void GetGroupSummarySlotNames(ref VBuffer<DvText> slotNames, string prefix)
			{
				DvText[] array = slotNames.Values;
				if (Utils.Size<DvText>(array) < this._counters.TruncationLevel)
				{
					array = new DvText[this._counters.TruncationLevel];
				}
				for (int i = 0; i < this._counters.TruncationLevel; i++)
				{
					array[i] = new DvText(string.Format("{0}@{1}", prefix, i + 1));
				}
				slotNames = new VBuffer<DvText>(this._counters.TruncationLevel, array, null);
			}

			// Token: 0x06000AE5 RID: 2789 RVA: 0x00039FE8 File Offset: 0x000381E8
			private void GetSlotNames(ref VBuffer<DvText> slotNames)
			{
				DvText[] array = slotNames.Values;
				if (Utils.Size<DvText>(array) < this._counters.TruncationLevel)
				{
					array = new DvText[this._counters.TruncationLevel];
				}
				for (int i = 0; i < this._counters.TruncationLevel; i++)
				{
					array[i] = new DvText(string.Format("@{0}", i + 1));
				}
				slotNames = new VBuffer<DvText>(this._counters.TruncationLevel, array, null);
			}

			// Token: 0x0400059E RID: 1438
			private float _currentQueryWeight;

			// Token: 0x0400059F RID: 1439
			private ValueGetter<float> _labelGetter;

			// Token: 0x040005A0 RID: 1440
			private ValueGetter<float> _scoreGetter;

			// Token: 0x040005A1 RID: 1441
			private ValueGetter<float> _weightGetter;

			// Token: 0x040005A2 RID: 1442
			private Func<bool> _newGroupDel;

			// Token: 0x040005A3 RID: 1443
			private Action _groupSbUpdate;

			// Token: 0x040005A4 RID: 1444
			private StringBuilder _groupSb;

			// Token: 0x040005A5 RID: 1445
			private readonly RankerEvaluator.Aggregator.Counters _counters;

			// Token: 0x040005A6 RID: 1446
			private readonly RankerEvaluator.Aggregator.Counters _weightedCounters;

			// Token: 0x040005A7 RID: 1447
			private readonly List<DvText> _groupId;

			// Token: 0x040005A8 RID: 1448
			private int _groupSize;

			// Token: 0x020001E8 RID: 488
			public sealed class Counters
			{
				// Token: 0x1700013E RID: 318
				// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0003A071 File Offset: 0x00038271
				public bool GroupSummary
				{
					get
					{
						return this._groupNdcg != null;
					}
				}

				// Token: 0x1700013F RID: 319
				// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0003A080 File Offset: 0x00038280
				public double[] Ndcg
				{
					get
					{
						double[] array = new double[this.TruncationLevel];
						for (int i = 0; i < this.TruncationLevel; i++)
						{
							array[i] = this._sumNdcgAtN[i] / this._sumWeights;
						}
						return array;
					}
				}

				// Token: 0x17000140 RID: 320
				// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0003A0C0 File Offset: 0x000382C0
				public double[] Dcg
				{
					get
					{
						double[] array = new double[this.TruncationLevel];
						for (int i = 0; i < this.TruncationLevel; i++)
						{
							array[i] = this._sumDcgAtN[i] / this._sumWeights;
						}
						return array;
					}
				}

				// Token: 0x17000141 RID: 321
				// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0003A0FD File Offset: 0x000382FD
				public double[][] GroupDcg
				{
					get
					{
						if (this._groupDcg == null)
						{
							return null;
						}
						return this._groupDcg.ToArray();
					}
				}

				// Token: 0x17000142 RID: 322
				// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0003A114 File Offset: 0x00038314
				public double[][] GroupNdcg
				{
					get
					{
						if (this._groupNdcg == null)
						{
							return null;
						}
						return this._groupNdcg.ToArray();
					}
				}

				// Token: 0x17000143 RID: 323
				// (get) Token: 0x06000AED RID: 2797 RVA: 0x0003A12B File Offset: 0x0003832B
				public double[][] GroupMaxDcg
				{
					get
					{
						if (this._groupMaxDcg == null)
						{
							return null;
						}
						return this._groupMaxDcg.ToArray();
					}
				}

				// Token: 0x06000AEE RID: 2798 RVA: 0x0003A144 File Offset: 0x00038344
				public Counters(double[] labelGains, int truncationLevel, bool groupSummary)
				{
					this.TruncationLevel = truncationLevel;
					this._sumDcgAtN = new double[this.TruncationLevel];
					this._sumNdcgAtN = new double[this.TruncationLevel];
					this._groupDcgCur = new double[this.TruncationLevel];
					this._groupMaxDcgCur = new double[this.TruncationLevel];
					if (groupSummary)
					{
						this._groupNdcg = new List<double[]>();
						this._groupDcg = new List<double[]>();
						this._groupMaxDcg = new List<double[]>();
					}
					this._queryLabels = new List<short>();
					this._queryOutputs = new List<float>();
					this._labelGains = labelGains;
				}

				// Token: 0x06000AEF RID: 2799 RVA: 0x0003A1E3 File Offset: 0x000383E3
				public void Update(short label, float output)
				{
					this._queryLabels.Add(label);
					this._queryOutputs.Add(output);
				}

				// Token: 0x06000AF0 RID: 2800 RVA: 0x0003A200 File Offset: 0x00038400
				public void UpdateGroup(float weight)
				{
					RankerUtils.QueryMaxDCG(this._labelGains, this.TruncationLevel, this._queryLabels, this._queryOutputs, this._groupMaxDcgCur);
					if (this._groupMaxDcg != null)
					{
						double[] array = new double[this.TruncationLevel];
						Array.Copy(this._groupMaxDcgCur, array, this.TruncationLevel);
						this._groupMaxDcg.Add(array);
					}
					RankerUtils.QueryDCG(this._labelGains, this.TruncationLevel, this._queryLabels, this._queryOutputs, this._groupDcgCur);
					if (this._groupDcg != null)
					{
						double[] array2 = new double[this.TruncationLevel];
						Array.Copy(this._groupDcgCur, array2, this.TruncationLevel);
						this._groupDcg.Add(array2);
					}
					double[] array3 = new double[this.TruncationLevel];
					for (int i = 0; i < this.TruncationLevel; i++)
					{
						double num = ((this._groupMaxDcgCur[i] > 0.0) ? (this._groupDcgCur[i] / this._groupMaxDcgCur[i] * 100.0) : 0.0);
						this._sumNdcgAtN[i] += num * (double)weight;
						this._sumDcgAtN[i] += this._groupDcgCur[i] * (double)weight;
						array3[i] = num;
					}
					this._sumWeights += (double)weight;
					if (this._groupNdcg != null)
					{
						this._groupNdcg.Add(array3);
					}
					this._queryLabels.Clear();
					this._queryOutputs.Clear();
				}

				// Token: 0x040005AB RID: 1451
				public const int MaxTruncationLevel = 10;

				// Token: 0x040005AC RID: 1452
				public readonly int TruncationLevel;

				// Token: 0x040005AD RID: 1453
				private readonly List<double[]> _groupNdcg;

				// Token: 0x040005AE RID: 1454
				private readonly List<double[]> _groupDcg;

				// Token: 0x040005AF RID: 1455
				private readonly List<double[]> _groupMaxDcg;

				// Token: 0x040005B0 RID: 1456
				private readonly double[] _groupDcgCur;

				// Token: 0x040005B1 RID: 1457
				private readonly double[] _groupMaxDcgCur;

				// Token: 0x040005B2 RID: 1458
				private readonly double[] _sumNdcgAtN;

				// Token: 0x040005B3 RID: 1459
				private readonly double[] _sumDcgAtN;

				// Token: 0x040005B4 RID: 1460
				private double _sumWeights;

				// Token: 0x040005B5 RID: 1461
				private readonly List<short> _queryLabels;

				// Token: 0x040005B6 RID: 1462
				private readonly List<float> _queryOutputs;

				// Token: 0x040005B7 RID: 1463
				private readonly double[] _labelGains;
			}
		}
	}
}
