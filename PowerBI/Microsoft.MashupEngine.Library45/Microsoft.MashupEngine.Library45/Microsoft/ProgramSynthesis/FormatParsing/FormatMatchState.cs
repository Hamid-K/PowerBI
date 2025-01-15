using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.FormatParsing
{
	// Token: 0x02000762 RID: 1890
	public abstract class FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> : IEquatable<FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> where TPartialParse : IPartialParse<TFullParse, TFormatPart, TSubstring, TPartialParse>, IEquatable<TPartialParse> where TFormatPart : IFormatPart<TPartialParse, TFullParse, TSubstring, TFormatPart> where TSubstring : ISubstring<TSubstring>, IEquatable<TSubstring>
	{
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002872 RID: 10354 RVA: 0x00072AD0 File Offset: 0x00070CD0
		public Optional<TPartialParse> CumulativeParse { get; }

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x00072AD8 File Offset: 0x00070CD8
		public TSubstring UnparsedSuffix { get; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x00072AE0 File Offset: 0x00070CE0
		public ImmutableDictionary<int, TPartialParse> ParsedValues { get; }

		// Token: 0x06002875 RID: 10357 RVA: 0x00072AE8 File Offset: 0x00070CE8
		protected FormatMatchState(TSubstring unparsedSuffix)
		{
			this.UnparsedSuffix = unparsedSuffix;
			this.CumulativeParse = Optional<TPartialParse>.Nothing;
			this.ParsedValues = ImmutableDictionary<int, TPartialParse>.Empty;
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x00072B0D File Offset: 0x00070D0D
		protected FormatMatchState(TSubstring unparsedSuffix, TPartialParse cumulativeParse, ImmutableDictionary<int, TPartialParse> parsedValues)
		{
			this.UnparsedSuffix = unparsedSuffix;
			this.CumulativeParse = cumulativeParse.Some<TPartialParse>();
			this.ParsedValues = parsedValues;
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x00072B30 File Offset: 0x00070D30
		public bool Equals(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> other)
		{
			return other != null && (other == this || (EqualityComparer<TSubstring>.Default.Equals(this.UnparsedSuffix, other.UnparsedSuffix) && this.CumulativeParse.Equals(other.CumulativeParse) && this.ParsedValues.DictionaryEquals(other.ParsedValues, EqualityComparer<TPartialParse>.Default)));
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x00072B90 File Offset: 0x00070D90
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> formatMatchState = other as FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>;
			return formatMatchState != null && this.Equals(formatMatchState);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x00072BBC File Offset: 0x00070DBC
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			this._hashCode = new int?(0);
			this._hashCode ^= EqualityComparer<TSubstring>.Default.GetHashCode(this.UnparsedSuffix) * 3361;
			this._hashCode ^= this.CumulativeParse.GetHashCode() * 3361;
			this._hashCode ^= this.ParsedValues.OrderIndependentHashCode(KeyValueComparer<int, TPartialParse>.DefaultEqualityInstance) * 3361;
			return this._hashCode.Value;
		}

		// Token: 0x0600287A RID: 10362
		public abstract FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> ToFullState();

		// Token: 0x0600287B RID: 10363 RVA: 0x00072CCF File Offset: 0x00070ECF
		public static FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> CreateFullState(TSubstring unparsedSuffix)
		{
			return FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>.Create(unparsedSuffix);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x00072CD7 File Offset: 0x00070ED7
		public static FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> CreateFullState(TSubstring unparsedSuffix, TPartialParse cumulativeParse, ImmutableDictionary<int, TPartialParse> parsedValues)
		{
			return FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>.Create(unparsedSuffix, cumulativeParse, parsedValues);
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x00072CE1 File Offset: 0x00070EE1
		public DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> Sequence(TPartialParse partialParse, ImmutableDictionary<int, TPartialParse> parsedValues, TSubstring unparsedSuffix)
		{
			return DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>.Create(this, partialParse, parsedValues, unparsedSuffix);
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x00072CEC File Offset: 0x00070EEC
		public DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> Sequence(Func<TSubstring, TPartialParse> emptyParseFactory)
		{
			return DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>.Create(this, emptyParseFactory(this.UnparsedSuffix), this.ParsedValues, this.UnparsedSuffix);
		}

		// Token: 0x040013AF RID: 5039
		private int? _hashCode;
	}
}
