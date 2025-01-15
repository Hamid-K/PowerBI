using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000039 RID: 57
	internal class MultiSizeLabelNameDictionary<TAggregator> where TAggregator : Aggregator
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x000086BC File Offset: 0x000068BC
		public MultiSizeLabelNameDictionary(object initialLabelNameDict)
		{
			this.NoLabelAggregator = default(TAggregator);
			this.Label1 = null;
			this.Label2 = null;
			this.Label3 = null;
			this.LabelMany = null;
			TAggregator taggregator = initialLabelNameDict as TAggregator;
			if (taggregator != null)
			{
				this.NoLabelAggregator = taggregator;
				return;
			}
			FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator> fixedSizeLabelNameDictionary = initialLabelNameDict as FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator>;
			if (fixedSizeLabelNameDictionary != null)
			{
				this.Label1 = fixedSizeLabelNameDictionary;
				return;
			}
			FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator> fixedSizeLabelNameDictionary2 = initialLabelNameDict as FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator>;
			if (fixedSizeLabelNameDictionary2 != null)
			{
				this.Label2 = fixedSizeLabelNameDictionary2;
				return;
			}
			FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator> fixedSizeLabelNameDictionary3 = initialLabelNameDict as FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator>;
			if (fixedSizeLabelNameDictionary3 != null)
			{
				this.Label3 = fixedSizeLabelNameDictionary3;
				return;
			}
			FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator> fixedSizeLabelNameDictionary4 = initialLabelNameDict as FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator>;
			if (fixedSizeLabelNameDictionary4 == null)
			{
				return;
			}
			this.LabelMany = fixedSizeLabelNameDictionary4;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008760 File Offset: 0x00006960
		public TAggregator GetNoLabelAggregator(Func<TAggregator> createFunc)
		{
			if (this.NoLabelAggregator == null)
			{
				TAggregator taggregator = createFunc();
				if (taggregator != null)
				{
					Interlocked.CompareExchange<TAggregator>(ref this.NoLabelAggregator, taggregator, default(TAggregator));
				}
			}
			return this.NoLabelAggregator;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000087A8 File Offset: 0x000069A8
		public FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator> GetFixedSizeLabelNameDictionary<TStringSequence, TObjectSequence>() where TStringSequence : IStringSequence, IEquatable<TStringSequence> where TObjectSequence : IObjectSequence, IEquatable<TObjectSequence>
		{
			TStringSequence tstringSequence = default(TStringSequence);
			if (tstringSequence is StringSequence1)
			{
				if (this.Label1 == null)
				{
					Interlocked.CompareExchange<FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator>>(ref this.Label1, new FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator>(), null);
				}
				return (FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator>)this.Label1;
			}
			if (tstringSequence is StringSequence2)
			{
				if (this.Label2 == null)
				{
					Interlocked.CompareExchange<FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator>>(ref this.Label2, new FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator>(), null);
				}
				return (FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator>)this.Label2;
			}
			if (tstringSequence is StringSequence3)
			{
				if (this.Label3 == null)
				{
					Interlocked.CompareExchange<FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator>>(ref this.Label3, new FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator>(), null);
				}
				return (FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator>)this.Label3;
			}
			if (!(tstringSequence is StringSequenceMany))
			{
				return null;
			}
			if (this.LabelMany == null)
			{
				Interlocked.CompareExchange<FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator>>(ref this.LabelMany, new FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator>(), null);
			}
			return (FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator>)this.LabelMany;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008890 File Offset: 0x00006A90
		public void Collect(Action<LabeledAggregationStatistics> visitFunc)
		{
			if (this.NoLabelAggregator != null)
			{
				IAggregationStatistics aggregationStatistics = this.NoLabelAggregator.Collect();
				visitFunc(new LabeledAggregationStatistics(aggregationStatistics, Array.Empty<KeyValuePair<string, string>>()));
			}
			FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator> label = this.Label1;
			if (label != null)
			{
				label.Collect(visitFunc);
			}
			FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator> label2 = this.Label2;
			if (label2 != null)
			{
				label2.Collect(visitFunc);
			}
			FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator> label3 = this.Label3;
			if (label3 != null)
			{
				label3.Collect(visitFunc);
			}
			FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator> labelMany = this.LabelMany;
			if (labelMany == null)
			{
				return;
			}
			labelMany.Collect(visitFunc);
		}

		// Token: 0x040000DE RID: 222
		private TAggregator NoLabelAggregator;

		// Token: 0x040000DF RID: 223
		private FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator> Label1;

		// Token: 0x040000E0 RID: 224
		private FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator> Label2;

		// Token: 0x040000E1 RID: 225
		private FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator> Label3;

		// Token: 0x040000E2 RID: 226
		private FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator> LabelMany;
	}
}
