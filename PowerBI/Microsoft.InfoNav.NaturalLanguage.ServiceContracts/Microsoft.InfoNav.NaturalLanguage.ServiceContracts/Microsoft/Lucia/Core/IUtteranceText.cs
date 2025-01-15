using System;
using System.Collections.ObjectModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000AD RID: 173
	public interface IUtteranceText
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000383 RID: 899
		string Utterance { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000384 RID: 900
		ReadOnlyCollection<IUtteranceToken> Tokens { get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000385 RID: 901
		LanguageIdentifier Language { get; }
	}
}
