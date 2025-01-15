using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200003D RID: 61
	[SecurityCritical]
	internal class LabelInstructionInterpretter<TObjectSequence, TAggregator> where TObjectSequence : struct, IObjectSequence, IEquatable<TObjectSequence> where TAggregator : Aggregator
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00008B40 File Offset: 0x00006D40
		public LabelInstructionInterpretter(int expectedLabelCount, LabelInstruction[] instructions, ConcurrentDictionary<TObjectSequence, TAggregator> valuesDict, Func<TAggregator> createAggregator)
		{
			this._expectedLabelCount = expectedLabelCount;
			this._instructions = instructions;
			this._valuesDict = valuesDict;
			this._createAggregator = (TObjectSequence _) => createAggregator();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008B88 File Offset: 0x00006D88
		public unsafe bool GetAggregator(ReadOnlySpan<KeyValuePair<string, object>> labels, out TAggregator aggregator)
		{
			aggregator = default(TAggregator);
			if (labels.Length != this._expectedLabelCount)
			{
				return false;
			}
			TObjectSequence tobjectSequence = default(TObjectSequence);
			if (tobjectSequence is ObjectSequenceMany)
			{
				tobjectSequence = (TObjectSequence)((object)new ObjectSequenceMany(new object[this._expectedLabelCount]));
			}
			ref TObjectSequence ptr = ref tobjectSequence;
			for (int i = 0; i < this._instructions.Length; i++)
			{
				LabelInstruction labelInstruction = this._instructions[i];
				string labelName = labelInstruction.LabelName;
				KeyValuePair<string, object> keyValuePair = *labels[labelInstruction.SourceIndex];
				if (labelName != keyValuePair.Key)
				{
					return false;
				}
				ref TObjectSequence ptr2 = ref ptr;
				int num = i;
				keyValuePair = *labels[labelInstruction.SourceIndex];
				ptr2[num] = keyValuePair.Value;
			}
			if (!this._valuesDict.TryGetValue(tobjectSequence, out aggregator))
			{
				aggregator = this._createAggregator(tobjectSequence);
				if (aggregator == null)
				{
					return true;
				}
				aggregator = this._valuesDict.GetOrAdd(tobjectSequence, aggregator);
			}
			return true;
		}

		// Token: 0x040000E5 RID: 229
		private int _expectedLabelCount;

		// Token: 0x040000E6 RID: 230
		private LabelInstruction[] _instructions;

		// Token: 0x040000E7 RID: 231
		private ConcurrentDictionary<TObjectSequence, TAggregator> _valuesDict;

		// Token: 0x040000E8 RID: 232
		private Func<TObjectSequence, TAggregator> _createAggregator;
	}
}
