using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering
{
	// Token: 0x02000A6B RID: 2667
	[DebuggerDisplay("{Input}")]
	internal class DataPoint
	{
		// Token: 0x06004222 RID: 16930 RVA: 0x000CEE48 File Offset: 0x000CD048
		public DataPoint(LearningCacheSubstring input, ICollection<PredicateFeature> features, IReadOnlyList<PredicateFeature> allFeatures)
		{
			this.Input = ((input != null) ? input.Value : null);
			this.FeatureVector = new BitArray(allFeatures.Count);
			for (int i = 0; i < allFeatures.Count; i++)
			{
				if (features.Contains(allFeatures[i]))
				{
					this.FeatureVector.Set(i, true);
				}
			}
		}

		// Token: 0x17000B77 RID: 2935
		public bool this[int key]
		{
			get
			{
				return this.FeatureVector[key];
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x000CEEB9 File Offset: 0x000CD0B9
		public BitArray FeatureVector { get; }

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x000CEEC1 File Offset: 0x000CD0C1
		public string Input { get; }
	}
}
