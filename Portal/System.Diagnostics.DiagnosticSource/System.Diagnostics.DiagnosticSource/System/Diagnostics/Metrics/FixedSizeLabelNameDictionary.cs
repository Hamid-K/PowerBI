using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200003E RID: 62
	internal class FixedSizeLabelNameDictionary<TStringSequence, TObjectSequence, TAggregator> : ConcurrentDictionary<TStringSequence, ConcurrentDictionary<TObjectSequence, TAggregator>> where TStringSequence : IStringSequence, IEquatable<TStringSequence> where TObjectSequence : IObjectSequence, IEquatable<TObjectSequence> where TAggregator : Aggregator
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00008CA0 File Offset: 0x00006EA0
		public void Collect(Action<LabeledAggregationStatistics> visitFunc)
		{
			foreach (KeyValuePair<TStringSequence, ConcurrentDictionary<TObjectSequence, TAggregator>> keyValuePair in this)
			{
				TStringSequence key = keyValuePair.Key;
				foreach (KeyValuePair<TObjectSequence, TAggregator> keyValuePair2 in keyValuePair.Value)
				{
					TObjectSequence key2 = keyValuePair2.Key;
					KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[key.Length];
					for (int i = 0; i < array.Length; i++)
					{
						KeyValuePair<string, string>[] array2 = array;
						int num = i;
						string text = key[i];
						object obj = key2[i];
						array2[num] = new KeyValuePair<string, string>(text, ((obj != null) ? obj.ToString() : null) ?? "");
					}
					IAggregationStatistics aggregationStatistics = keyValuePair2.Value.Collect();
					visitFunc(new LabeledAggregationStatistics(aggregationStatistics, array));
				}
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008DC0 File Offset: 0x00006FC0
		public ConcurrentDictionary<TObjectSequence, TAggregator> GetValuesDictionary(in TStringSequence names)
		{
			return base.GetOrAdd(names, (TStringSequence _) => new ConcurrentDictionary<TObjectSequence, TAggregator>());
		}
	}
}
