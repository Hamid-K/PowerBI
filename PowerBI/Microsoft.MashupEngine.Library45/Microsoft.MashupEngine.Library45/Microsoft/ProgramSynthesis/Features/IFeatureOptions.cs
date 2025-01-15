using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007DA RID: 2010
	public interface IFeatureOptions : IEquatable<IFeatureOptions>
	{
		// Token: 0x06002AD4 RID: 10964
		Symbol GetSubstitutionFor(Symbol symbol);

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002AD5 RID: 10965
		ImmutableDictionary<Symbol, Symbol> Substitutions { get; }

		// Token: 0x06002AD6 RID: 10966
		IFeatureOptions CloneWithNewSubstitutions(IReadOnlyDictionary<Symbol, Symbol> newSubstitutions);

		// Token: 0x06002AD7 RID: 10967
		IFeatureOptions WithAddedSubstitutions(IReadOnlyDictionary<Symbol, Symbol> additionalSubstitutions);
	}
}
