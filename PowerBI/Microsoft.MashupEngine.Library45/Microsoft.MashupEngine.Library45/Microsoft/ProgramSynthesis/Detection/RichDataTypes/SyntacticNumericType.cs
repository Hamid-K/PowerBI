using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000A95 RID: 2709
	public class SyntacticNumericType : SyntacticType
	{
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060043A3 RID: 17315 RVA: 0x000D4063 File Offset: 0x000D2263
		public Optional<string> GroupSeparator { get; }

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060043A4 RID: 17316 RVA: 0x000D406B File Offset: 0x000D226B
		public Optional<string> DecimalSeparator { get; }

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060043A5 RID: 17317 RVA: 0x000D4073 File Offset: 0x000D2273
		public Optional<string> CurrencySymbol { get; }

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060043A6 RID: 17318 RVA: 0x000D407B File Offset: 0x000D227B
		public bool IsInScientificNotation { get; }

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x000D4083 File Offset: 0x000D2283
		public bool IsNegated { get; }

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060043A8 RID: 17320 RVA: 0x000D408B File Offset: 0x000D228B
		public bool HasLeadingSign { get; }

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x000D4094 File Offset: 0x000D2294
		public bool IsInteger
		{
			get
			{
				return !this.DecimalSeparator.HasValue && !this.IsInScientificNotation && !this.IsNaN && !this.IsPositiveInfinity && !this.IsNegativeInfinity;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060043AA RID: 17322 RVA: 0x000D40D4 File Offset: 0x000D22D4
		public bool IsReal
		{
			get
			{
				return !this.IsInteger;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x000D40DF File Offset: 0x000D22DF
		public bool IsNaN { get; }

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060043AC RID: 17324 RVA: 0x000D40E7 File Offset: 0x000D22E7
		public bool IsPositiveInfinity { get; }

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x000D40EF File Offset: 0x000D22EF
		public bool IsNegativeInfinity { get; }

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060043AE RID: 17326 RVA: 0x000D40F7 File Offset: 0x000D22F7
		public Regex MembershipRegex { get; }

		// Token: 0x060043AF RID: 17327 RVA: 0x000D40FF File Offset: 0x000D22FF
		public SyntacticNumericType(string naValue)
			: base(DataKind.Numeric, null, naValue)
		{
			this.MembershipRegex = new Regex(Regex.Escape(naValue), RegexOptions.Compiled);
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x000D411C File Offset: 0x000D231C
		public SyntacticNumericType(Regex membershipRegex, Dictionary<string, string> substitutions, bool isNaN, bool isPositiveInfinity, bool isNegativeInfinity)
			: base(DataKind.Numeric, substitutions, null)
		{
			this.IsNaN = isNaN;
			this.IsPositiveInfinity = isPositiveInfinity;
			this.IsNegativeInfinity = isNegativeInfinity;
			this.MembershipRegex = membershipRegex;
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x000D4148 File Offset: 0x000D2348
		public SyntacticNumericType(Regex membershipRegex, Dictionary<string, string> substitutions = null, string groupSeparator = null, string decimalSeparator = null, string currencySymbol = null, bool isInScientificNotation = false, bool isNegated = false, bool hasLeadingSign = false)
			: base(DataKind.Numeric, substitutions, null)
		{
			this.MembershipRegex = membershipRegex;
			this.DecimalSeparator = decimalSeparator.SomeIfNotNull<string>();
			this.CurrencySymbol = currencySymbol.SomeIfNotNull<string>();
			this.GroupSeparator = groupSeparator.SomeIfNotNull<string>();
			this.IsInScientificNotation = isInScientificNotation;
			this.IsNegated = isNegated;
			this.HasLeadingSign = hasLeadingSign;
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x000D41A4 File Offset: 0x000D23A4
		public override Optional<string> Canonicalize(string value)
		{
			if (!this.MembershipRegex.IsMatch(value) || base.IsNaValue)
			{
				return Optional<string>.Nothing;
			}
			string text = value.NormalizeAndTrim(NormalizationForm.FormC);
			if (text == null)
			{
				return Optional<string>.Nothing;
			}
			string text2 = base.EmptySubstitutions.Concat(base.NonEmptySubstitutions).Aggregate(text, (string acc, KeyValuePair<string, string> kvp) => acc.Replace(kvp.Key, kvp.Value));
			text = (this.IsNegated ? ("-" + text2) : text2);
			return text.Some<string>();
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x000D4234 File Offset: 0x000D2434
		public override bool IsValid(string value)
		{
			if (!base.IsNaValue)
			{
				return this.MembershipRegex.IsMatch(value);
			}
			return value == base.NaValue.Value;
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x000D426C File Offset: 0x000D246C
		public override bool Equals(SyntacticType other)
		{
			if (this == other)
			{
				return true;
			}
			SyntacticNumericType syntacticNumericType = other as SyntacticNumericType;
			return syntacticNumericType != null && (syntacticNumericType.NaValue.Equals(base.NaValue) && syntacticNumericType.GroupSeparator.Equals(this.GroupSeparator) && syntacticNumericType.DecimalSeparator.Equals(this.DecimalSeparator) && syntacticNumericType.CurrencySymbol.Equals(this.CurrencySymbol) && syntacticNumericType.IsInScientificNotation.Equals(this.IsInScientificNotation) && syntacticNumericType.IsNegated.Equals(this.IsNegated) && base.Substitutions.ReadOnlyDictionaryEquals(syntacticNumericType.Substitutions, null)) && this.MembershipRegex.Equals(syntacticNumericType.MembershipRegex);
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x000D4339 File Offset: 0x000D2539
		public override bool Equals(object other)
		{
			return this.Equals(other as SyntacticNumericType);
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x000D4348 File Offset: 0x000D2548
		public override int GetHashCode()
		{
			if (this._hashCode == null)
			{
				int num = 0;
				num = (num * 27409) ^ base.NaValue.GetHashCode();
				num = (num * 27409) ^ this.GroupSeparator.GetHashCode();
				num = (num * 27409) ^ this.DecimalSeparator.GetHashCode();
				num = (num * 27409) ^ this.CurrencySymbol.GetHashCode();
				num = (num * 27409) ^ this.IsInScientificNotation.GetHashCode();
				num = (num * 27409) ^ this.IsNegated.GetHashCode();
				num = (num * 27409) ^ base.Substitutions.OrderIndependentHashCode(KeyValueComparer<string, string>.DefaultEqualityInstance);
				this._hashCode = new int?(num);
			}
			return this._hashCode.Value;
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x000D443C File Offset: 0x000D263C
		public override string ToString()
		{
			if (!base.IsNaValue)
			{
				return this.MembershipRegex.ToString();
			}
			return base.NaValue.Value;
		}

		// Token: 0x04001E97 RID: 7831
		private int? _hashCode;
	}
}
