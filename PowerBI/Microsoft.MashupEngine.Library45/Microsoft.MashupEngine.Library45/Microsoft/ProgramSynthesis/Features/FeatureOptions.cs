using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007D8 RID: 2008
	public class FeatureOptions : IFeatureOptions, IEquatable<IFeatureOptions>
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x0007806E File Offset: 0x0007626E
		// (set) Token: 0x06002AC4 RID: 10948 RVA: 0x00078076 File Offset: 0x00076276
		public ImmutableDictionary<Symbol, Symbol> Substitutions { get; private set; }

		// Token: 0x06002AC5 RID: 10949 RVA: 0x0007807F File Offset: 0x0007627F
		public virtual bool Equals(IFeatureOptions other)
		{
			return this == other || (other != null && !(base.GetType() != other.GetType()) && this.Equals((FeatureOptions)other));
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000780B0 File Offset: 0x000762B0
		public bool Equals(FeatureOptions other)
		{
			return other != null && (other == this || this.Substitutions == other.Substitutions || (this.Substitutions != null && other.Substitutions != null && this.Substitutions.DictionaryEquals(other.Substitutions, null)));
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000780FC File Offset: 0x000762FC
		public override bool Equals(object obj)
		{
			return obj != null && (obj == this || (!(obj.GetType() != base.GetType()) && this.Equals((FeatureOptions)obj)));
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x0007812A File Offset: 0x0007632A
		public override int GetHashCode()
		{
			return (this.Substitutions.OrderIndependentKeyValueHashCode<Symbol, Symbol>() * 19483) ^ base.GetType().GetHashCode();
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x00078149 File Offset: 0x00076349
		public override string ToString()
		{
			return string.Join(", ", this.Substitutions.Select((KeyValuePair<Symbol, Symbol> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0} -> {1}", new object[] { kvp.Key, kvp.Value }))));
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x0007817F File Offset: 0x0007637F
		public Symbol GetSubstitutionFor(Symbol symbol)
		{
			ImmutableDictionary<Symbol, Symbol> substitutions = this.Substitutions;
			return ((substitutions != null) ? substitutions.MaybeGet(symbol).OrElseDefault<Symbol>() : null) ?? symbol;
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x0007819E File Offset: 0x0007639E
		public virtual IFeatureOptions CloneWithNewSubstitutions(IReadOnlyDictionary<Symbol, Symbol> newSubstitutions)
		{
			FeatureOptions featureOptions = this.Clone();
			featureOptions.Substitutions = newSubstitutions.ToImmutableDictionary<Symbol, Symbol>();
			return featureOptions;
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000781B4 File Offset: 0x000763B4
		public virtual IFeatureOptions WithAddedSubstitutions(IReadOnlyDictionary<Symbol, Symbol> additionalSubstitutions)
		{
			IImmutableDictionary<Symbol, Symbol> immutableDictionary = this.Substitutions;
			immutableDictionary = immutableDictionary.AddRange(additionalSubstitutions);
			return this.CloneWithNewSubstitutions(immutableDictionary);
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000781D7 File Offset: 0x000763D7
		public FeatureOptions(IReadOnlyDictionary<Symbol, Symbol> substitutions = null)
		{
			this.Substitutions = ((substitutions != null) ? substitutions.ToImmutableDictionary<Symbol, Symbol>() : null) ?? ImmutableDictionary<Symbol, Symbol>.Empty;
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000781FA File Offset: 0x000763FA
		protected virtual FeatureOptions Clone()
		{
			return new FeatureOptions(this.Substitutions);
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002ACF RID: 10959 RVA: 0x00078207 File Offset: 0x00076407
		public static FeatureOptions Default { get; } = new FeatureOptions(null);
	}
}
