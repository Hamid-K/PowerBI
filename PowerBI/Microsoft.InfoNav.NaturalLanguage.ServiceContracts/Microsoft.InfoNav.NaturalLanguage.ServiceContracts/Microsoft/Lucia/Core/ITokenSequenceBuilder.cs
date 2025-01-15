using System;
using System.Collections.ObjectModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000AB RID: 171
	public interface ITokenSequenceBuilder
	{
		// Token: 0x06000374 RID: 884
		void Append(int utteranceTokenIndex);

		// Token: 0x06000375 RID: 885
		void Prepend(int utteranceTokenIndex);

		// Token: 0x06000376 RID: 886
		bool IsValid();

		// Token: 0x06000377 RID: 887
		string GetTokenMatchValue(int tokenOffset);

		// Token: 0x06000378 RID: 888
		ReadOnlyCollection<TokenMatch> GenerateSequence();

		// Token: 0x06000379 RID: 889
		string GenerateSequenceText();

		// Token: 0x0600037A RID: 890
		void RemoveFirst();

		// Token: 0x0600037B RID: 891
		void RemoveLast();
	}
}
