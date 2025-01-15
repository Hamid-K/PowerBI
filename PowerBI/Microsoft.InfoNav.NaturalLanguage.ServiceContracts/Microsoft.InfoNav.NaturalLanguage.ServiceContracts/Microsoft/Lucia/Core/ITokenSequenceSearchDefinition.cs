using System;
using System.Collections.ObjectModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000AC RID: 172
	public interface ITokenSequenceSearchDefinition
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600037C RID: 892
		IUtteranceText UtteranceText { get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600037D RID: 893
		ReadOnlyCollection<int> ExactSearchTokenIndexes { get; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600037E RID: 894
		TokenSequenceSearchType SearchType { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600037F RID: 895
		bool AllowPrefixMatch { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000380 RID: 896
		bool IncludeHiddenEntities { get; }

		// Token: 0x06000381 RID: 897
		ITokenSequenceBuilder CreateTokenSequenceBuilder(TokenSequenceBuilderBehavior behavior);

		// Token: 0x06000382 RID: 898
		ITokenSequenceBuilder CreatePrefixTokenSequenceBuilder(TokenSequenceBuilderBehavior behavior);
	}
}
