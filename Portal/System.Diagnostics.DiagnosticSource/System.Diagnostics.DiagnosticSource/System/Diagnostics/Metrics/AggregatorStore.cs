using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security;
using System.Threading;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000038 RID: 56
	[SecuritySafeCritical]
	internal struct AggregatorStore<TAggregator> where TAggregator : Aggregator
	{
		// Token: 0x060001DC RID: 476 RVA: 0x00008461 File Offset: 0x00006661
		public AggregatorStore(Func<TAggregator> createAggregator)
		{
			this._stateUnion = null;
			this._cachedLookupFunc = null;
			this._createAggregatorFunc = createAggregator;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000847C File Offset: 0x0000667C
		public TAggregator GetAggregator(ReadOnlySpan<KeyValuePair<string, object>> labels)
		{
			AggregatorLookupFunc<TAggregator> cachedLookupFunc = this._cachedLookupFunc;
			TAggregator taggregator;
			if (cachedLookupFunc != null && cachedLookupFunc(labels, out taggregator))
			{
				return taggregator;
			}
			return this.GetAggregatorSlow(labels);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000084AC File Offset: 0x000066AC
		private TAggregator GetAggregatorSlow(ReadOnlySpan<KeyValuePair<string, object>> labels)
		{
			AggregatorLookupFunc<TAggregator> aggregatorLookupFunc = LabelInstructionCompiler.Create<TAggregator>(ref this, this._createAggregatorFunc, labels);
			this._cachedLookupFunc = aggregatorLookupFunc;
			TAggregator taggregator;
			bool flag = aggregatorLookupFunc(labels, out taggregator);
			return taggregator;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000084DC File Offset: 0x000066DC
		public void Collect(Action<LabeledAggregationStatistics> visitFunc)
		{
			object stateUnion = this._stateUnion;
			object stateUnion2 = this._stateUnion;
			TAggregator taggregator = stateUnion2 as TAggregator;
			if (taggregator != null)
			{
				IAggregationStatistics aggregationStatistics = taggregator.Collect();
				visitFunc(new LabeledAggregationStatistics(aggregationStatistics, Array.Empty<KeyValuePair<string, string>>()));
				return;
			}
			FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator> fixedSizeLabelNameDictionary = stateUnion2 as FixedSizeLabelNameDictionary<StringSequence1, ObjectSequence1, TAggregator>;
			if (fixedSizeLabelNameDictionary != null)
			{
				fixedSizeLabelNameDictionary.Collect(visitFunc);
				return;
			}
			FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator> fixedSizeLabelNameDictionary2 = stateUnion2 as FixedSizeLabelNameDictionary<StringSequence2, ObjectSequence2, TAggregator>;
			if (fixedSizeLabelNameDictionary2 != null)
			{
				fixedSizeLabelNameDictionary2.Collect(visitFunc);
				return;
			}
			FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator> fixedSizeLabelNameDictionary3 = stateUnion2 as FixedSizeLabelNameDictionary<StringSequence3, ObjectSequence3, TAggregator>;
			if (fixedSizeLabelNameDictionary3 != null)
			{
				fixedSizeLabelNameDictionary3.Collect(visitFunc);
				return;
			}
			FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator> fixedSizeLabelNameDictionary4 = stateUnion2 as FixedSizeLabelNameDictionary<StringSequenceMany, ObjectSequenceMany, TAggregator>;
			if (fixedSizeLabelNameDictionary4 != null)
			{
				fixedSizeLabelNameDictionary4.Collect(visitFunc);
				return;
			}
			MultiSizeLabelNameDictionary<TAggregator> multiSizeLabelNameDictionary = stateUnion2 as MultiSizeLabelNameDictionary<TAggregator>;
			if (multiSizeLabelNameDictionary == null)
			{
				return;
			}
			multiSizeLabelNameDictionary.Collect(visitFunc);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000859C File Offset: 0x0000679C
		public TAggregator GetAggregator()
		{
			TAggregator taggregator;
			MultiSizeLabelNameDictionary<TAggregator> multiSizeLabelNameDictionary;
			MultiSizeLabelNameDictionary<TAggregator> multiSizeLabelNameDictionary2;
			for (;;)
			{
				object stateUnion = this._stateUnion;
				if (stateUnion == null)
				{
					taggregator = this._createAggregatorFunc();
					if (taggregator == null)
					{
						break;
					}
					if (Interlocked.CompareExchange(ref this._stateUnion, taggregator, null) == null)
					{
						return taggregator;
					}
				}
				else
				{
					TAggregator taggregator2 = stateUnion as TAggregator;
					if (taggregator2 != null)
					{
						return taggregator2;
					}
					multiSizeLabelNameDictionary = stateUnion as MultiSizeLabelNameDictionary<TAggregator>;
					if (multiSizeLabelNameDictionary != null)
					{
						goto Block_4;
					}
					multiSizeLabelNameDictionary2 = new MultiSizeLabelNameDictionary<TAggregator>(stateUnion);
					if (Interlocked.CompareExchange(ref this._stateUnion, multiSizeLabelNameDictionary2, stateUnion) == stateUnion)
					{
						goto Block_5;
					}
				}
			}
			return taggregator;
			Block_4:
			return multiSizeLabelNameDictionary.GetNoLabelAggregator(this._createAggregatorFunc);
			Block_5:
			return multiSizeLabelNameDictionary2.GetNoLabelAggregator(this._createAggregatorFunc);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008634 File Offset: 0x00006834
		public ConcurrentDictionary<TObjectSequence, TAggregator> GetLabelValuesDictionary<TStringSequence, TObjectSequence>(in TStringSequence names) where TStringSequence : IStringSequence, IEquatable<TStringSequence> where TObjectSequence : IObjectSequence, IEquatable<TObjectSequence>
		{
			FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator> fixedSizeLabelNameDictionary;
			FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator> fixedSizeLabelNameDictionary2;
			MultiSizeLabelNameDictionary<TAggregator> multiSizeLabelNameDictionary;
			MultiSizeLabelNameDictionary<TAggregator> multiSizeLabelNameDictionary2;
			for (;;)
			{
				object stateUnion = this._stateUnion;
				if (stateUnion == null)
				{
					fixedSizeLabelNameDictionary = new FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator>();
					if (Interlocked.CompareExchange(ref this._stateUnion, fixedSizeLabelNameDictionary, null) == null)
					{
						break;
					}
				}
				else
				{
					fixedSizeLabelNameDictionary2 = stateUnion as FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator>;
					if (fixedSizeLabelNameDictionary2 != null)
					{
						goto Block_2;
					}
					multiSizeLabelNameDictionary = stateUnion as MultiSizeLabelNameDictionary<TAggregator>;
					if (multiSizeLabelNameDictionary != null)
					{
						goto Block_3;
					}
					multiSizeLabelNameDictionary2 = new MultiSizeLabelNameDictionary<TAggregator>(stateUnion);
					if (Interlocked.CompareExchange(ref this._stateUnion, multiSizeLabelNameDictionary2, stateUnion) == stateUnion)
					{
						goto Block_4;
					}
				}
			}
			return fixedSizeLabelNameDictionary.GetValuesDictionary(in names);
			Block_2:
			return fixedSizeLabelNameDictionary2.GetValuesDictionary(in names);
			Block_3:
			return multiSizeLabelNameDictionary.GetFixedSizeLabelNameDictionary<TStringSequence, TObjectSequence>().GetValuesDictionary(in names);
			Block_4:
			return multiSizeLabelNameDictionary2.GetFixedSizeLabelNameDictionary<TStringSequence, TObjectSequence>().GetValuesDictionary(in names);
		}

		// Token: 0x040000DB RID: 219
		private volatile object _stateUnion;

		// Token: 0x040000DC RID: 220
		private volatile AggregatorLookupFunc<TAggregator> _cachedLookupFunc;

		// Token: 0x040000DD RID: 221
		private readonly Func<TAggregator> _createAggregatorFunc;
	}
}
