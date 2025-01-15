using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200003C RID: 60
	[SecurityCritical]
	internal static class LabelInstructionCompiler
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00008934 File Offset: 0x00006B34
		public static AggregatorLookupFunc<TAggregator> Create<TAggregator>(ref AggregatorStore<TAggregator> aggregatorStore, Func<TAggregator> createAggregatorFunc, ReadOnlySpan<KeyValuePair<string, object>> labels) where TAggregator : Aggregator
		{
			LabelInstruction[] array = LabelInstructionCompiler.Compile(labels);
			Array.Sort<LabelInstruction>(array, (LabelInstruction a, LabelInstruction b) => string.CompareOrdinal(a.LabelName, b.LabelName));
			int expectedLabels = labels.Length;
			switch (array.Length)
			{
			case 0:
			{
				TAggregator defaultAggregator = aggregatorStore.GetAggregator();
				return delegate(ReadOnlySpan<KeyValuePair<string, object>> l, out TAggregator aggregator)
				{
					if (l.Length != expectedLabels)
					{
						aggregator = default(TAggregator);
						return false;
					}
					aggregator = defaultAggregator;
					return true;
				};
			}
			case 1:
			{
				StringSequence1 stringSequence = new StringSequence1(array[0].LabelName);
				ConcurrentDictionary<ObjectSequence1, TAggregator> labelValuesDictionary = aggregatorStore.GetLabelValuesDictionary<StringSequence1, ObjectSequence1>(in stringSequence);
				LabelInstructionInterpretter<ObjectSequence1, TAggregator> labelInstructionInterpretter = new LabelInstructionInterpretter<ObjectSequence1, TAggregator>(expectedLabels, array, labelValuesDictionary, createAggregatorFunc);
				return new AggregatorLookupFunc<TAggregator>(labelInstructionInterpretter.GetAggregator);
			}
			case 2:
			{
				StringSequence2 stringSequence2 = new StringSequence2(array[0].LabelName, array[1].LabelName);
				ConcurrentDictionary<ObjectSequence2, TAggregator> labelValuesDictionary2 = aggregatorStore.GetLabelValuesDictionary<StringSequence2, ObjectSequence2>(in stringSequence2);
				LabelInstructionInterpretter<ObjectSequence2, TAggregator> labelInstructionInterpretter2 = new LabelInstructionInterpretter<ObjectSequence2, TAggregator>(expectedLabels, array, labelValuesDictionary2, createAggregatorFunc);
				return new AggregatorLookupFunc<TAggregator>(labelInstructionInterpretter2.GetAggregator);
			}
			case 3:
			{
				StringSequence3 stringSequence3 = new StringSequence3(array[0].LabelName, array[1].LabelName, array[2].LabelName);
				ConcurrentDictionary<ObjectSequence3, TAggregator> labelValuesDictionary3 = aggregatorStore.GetLabelValuesDictionary<StringSequence3, ObjectSequence3>(in stringSequence3);
				LabelInstructionInterpretter<ObjectSequence3, TAggregator> labelInstructionInterpretter3 = new LabelInstructionInterpretter<ObjectSequence3, TAggregator>(expectedLabels, array, labelValuesDictionary3, createAggregatorFunc);
				return new AggregatorLookupFunc<TAggregator>(labelInstructionInterpretter3.GetAggregator);
			}
			default:
			{
				string[] array2 = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array[i].LabelName;
				}
				StringSequenceMany stringSequenceMany = new StringSequenceMany(array2);
				ConcurrentDictionary<ObjectSequenceMany, TAggregator> labelValuesDictionary4 = aggregatorStore.GetLabelValuesDictionary<StringSequenceMany, ObjectSequenceMany>(in stringSequenceMany);
				LabelInstructionInterpretter<ObjectSequenceMany, TAggregator> labelInstructionInterpretter4 = new LabelInstructionInterpretter<ObjectSequenceMany, TAggregator>(expectedLabels, array, labelValuesDictionary4, createAggregatorFunc);
				return new AggregatorLookupFunc<TAggregator>(labelInstructionInterpretter4.GetAggregator);
			}
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008AF0 File Offset: 0x00006CF0
		private unsafe static LabelInstruction[] Compile(ReadOnlySpan<KeyValuePair<string, object>> labels)
		{
			LabelInstruction[] array = new LabelInstruction[labels.Length];
			for (int i = 0; i < labels.Length; i++)
			{
				LabelInstruction[] array2 = array;
				int num = i;
				int num2 = i;
				KeyValuePair<string, object> keyValuePair = *labels[i];
				array2[num] = new LabelInstruction(num2, keyValuePair.Key);
			}
			return array;
		}
	}
}
