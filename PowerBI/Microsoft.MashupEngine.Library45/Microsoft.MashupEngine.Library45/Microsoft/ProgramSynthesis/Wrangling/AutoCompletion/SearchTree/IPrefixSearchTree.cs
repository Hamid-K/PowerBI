using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x02000250 RID: 592
	public interface IPrefixSearchTree<TSequenceable, TSequence, TSubSequence, TValue> where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence>
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000CA6 RID: 3238
		IEnumerable<KeyValuePair<TSequence, TValue>> Items { get; }

		// Token: 0x06000CA7 RID: 3239
		void Add(TSequence key, TValue value);

		// Token: 0x06000CA8 RID: 3240
		bool TryGetValue(TSequence key, out TValue result);

		// Token: 0x06000CA9 RID: 3241
		TValue GetOrCreate(TSequence key, Func<TValue> factory);

		// Token: 0x06000CAA RID: 3242
		IEnumerable<KeyValuePair<TSequence, TValue>> PrefixLookup(TSequence prefix);

		// Token: 0x06000CAB RID: 3243
		void Clear();
	}
}
