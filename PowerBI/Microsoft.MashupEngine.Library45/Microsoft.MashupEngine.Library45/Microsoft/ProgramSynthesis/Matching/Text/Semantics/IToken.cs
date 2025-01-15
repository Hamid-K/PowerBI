using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x0200122F RID: 4655
	[Parseable("ParseXML", DeclaringType = typeof(Token))]
	public interface IToken : IEquatable<IToken>
	{
		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x06008C40 RID: 35904
		string Description { get; }

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06008C41 RID: 35905
		string Name { get; }

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x06008C42 RID: 35906
		double Score { get; }

		// Token: 0x06008C43 RID: 35907
		uint PrefixMatchLength(string target);

		// Token: 0x06008C44 RID: 35908
		string TryGetRegexPattern();
	}
}
