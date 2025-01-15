using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	internal struct AccessibleKeyValuePair<TKey, TValue>
	{
		// Token: 0x06000366 RID: 870 RVA: 0x00018F2F File Offset: 0x0001712F
		public AccessibleKeyValuePair(TKey key, TValue value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x04000089 RID: 137
		public TKey Key;

		// Token: 0x0400008A RID: 138
		public TValue Value;
	}
}
