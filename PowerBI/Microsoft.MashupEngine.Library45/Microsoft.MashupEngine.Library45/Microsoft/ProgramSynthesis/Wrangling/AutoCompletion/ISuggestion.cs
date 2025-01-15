using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion
{
	// Token: 0x02000248 RID: 584
	public interface ISuggestion : IEquatable<ISuggestion>
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000C6D RID: 3181
		string SuggestEventId { get; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000C6E RID: 3182
		IAutoCompleter Source { get; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C6F RID: 3183
		string PrefixString { get; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C70 RID: 3184
		uint MatchOffset { get; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000C71 RID: 3185
		string CompletionSuffix { get; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000C72 RID: 3186
		IReadOnlyDictionary<string, object> Metadata { get; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000C73 RID: 3187
		string CompleteValue { get; }
	}
}
