using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000A98 RID: 2712
	public class RichNumericType : RichDataType<SyntacticNumericType>
	{
		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060043BB RID: 17339 RVA: 0x000D4478 File Offset: 0x000D2678
		public Optional<BigInteger> MaxValue
		{
			get
			{
				if (!this.ContainsIntegerSubtype || this.ContainsRealSubtype)
				{
					return Optional<BigInteger>.Nothing;
				}
				return this._minMaxRange.Select((Record<BigInteger, BigInteger> t) => t.Item2);
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060043BC RID: 17340 RVA: 0x000D44C8 File Offset: 0x000D26C8
		public Optional<BigInteger> MinValue
		{
			get
			{
				if (!this.ContainsIntegerSubtype || this.ContainsRealSubtype)
				{
					return Optional<BigInteger>.Nothing;
				}
				return this._minMaxRange.Select((Record<BigInteger, BigInteger> t) => t.Item1);
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060043BD RID: 17341 RVA: 0x000D4518 File Offset: 0x000D2718
		public Optional<int> Precision
		{
			get
			{
				if (!this.ContainsRealSubtype || this.ContainsIntegerSubtype)
				{
					return Optional<int>.Nothing;
				}
				return this._precisionAndScale.Select((Record<int, int> t) => t.Item1);
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060043BE RID: 17342 RVA: 0x000D4568 File Offset: 0x000D2768
		public Optional<int> Scale
		{
			get
			{
				if (!this.ContainsRealSubtype || this.ContainsIntegerSubtype)
				{
					return Optional<int>.Nothing;
				}
				return this._precisionAndScale.Select((Record<int, int> t) => t.Item2);
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x000D45B8 File Offset: 0x000D27B8
		public Optional<int> MaxPrecision
		{
			get
			{
				if (this.ContainsRealSubtype || !this.ContainsIntegerSubtype)
				{
					return this._maxPrecisionAndScale.Select((Record<int, int> t) => t.Item1);
				}
				return Optional<int>.Nothing;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060043C0 RID: 17344 RVA: 0x000D4608 File Offset: 0x000D2808
		public Optional<int> MaxScale
		{
			get
			{
				if (this.ContainsRealSubtype || !this.ContainsIntegerSubtype)
				{
					return this._maxPrecisionAndScale.Select((Record<int, int> t) => t.Item2);
				}
				return Optional<int>.Nothing;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x000D4655 File Offset: 0x000D2855
		private IEnumerable<SyntacticNumericType> SyntacticSubTypes
		{
			get
			{
				return base.TypeClusters.SelectMany((SyntacticTypeOptionSet<SyntacticNumericType> t) => t);
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060043C2 RID: 17346 RVA: 0x000D4681 File Offset: 0x000D2881
		public bool ContainsIntegerSubtype
		{
			get
			{
				return this.SyntacticSubTypes.Any((SyntacticNumericType st) => st.IsInteger);
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060043C3 RID: 17347 RVA: 0x000D46AD File Offset: 0x000D28AD
		public bool ContainsRealSubtype
		{
			get
			{
				return this.SyntacticSubTypes.Any((SyntacticNumericType st) => st.IsReal);
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060043C4 RID: 17348 RVA: 0x000D46D9 File Offset: 0x000D28D9
		internal bool IsNaturalNumber
		{
			get
			{
				if (this.SyntacticSubTypes.Count<SyntacticNumericType>() == 1)
				{
					return this.SyntacticSubTypes.All((SyntacticNumericType st) => st.IsInteger && !st.DecimalSeparator.HasValue && !st.GroupSeparator.HasValue && !st.IsNegated);
				}
				return false;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060043C5 RID: 17349 RVA: 0x000D4715 File Offset: 0x000D2915
		public bool HasNaNOrNaValueOnly
		{
			get
			{
				return this.SyntacticSubTypes.All((SyntacticNumericType st) => st.IsNaN || st.IsNaValue);
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x060043C6 RID: 17350 RVA: 0x000D4741 File Offset: 0x000D2941
		// (set) Token: 0x060043C7 RID: 17351 RVA: 0x000D4749 File Offset: 0x000D2949
		public bool ContainsNonUniformPrecisionAndScale { get; private set; }

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x060043C8 RID: 17352 RVA: 0x000D4754 File Offset: 0x000D2954
		public NativeNumericType NativeType
		{
			get
			{
				NativeNumericType? nativeType = this._nativeType;
				return ((nativeType != null) ? nativeType : (this._nativeType = new NativeNumericType?(this.ComputeNativeType()))).Value;
			}
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x000D4790 File Offset: 0x000D2990
		private NativeNumericType ComputeNativeType()
		{
			if (this.SyntacticSubTypes.All((SyntacticNumericType t) => !t.DecimalSeparator.HasValue))
			{
				return this.ComputeIntegerRange();
			}
			return NativeNumericType.Real;
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x000D47C8 File Offset: 0x000D29C8
		private NativeNumericType ComputeIntegerRange()
		{
			BigInteger value = this.MinValue.Value;
			BigInteger value2 = this.MaxValue.Value;
			if (value > 0L && value2 > 0L)
			{
				NativeNumericType nativeNumericType = NativeNumericType.Unsigned;
				if (value2 < 256L)
				{
					return nativeNumericType | NativeNumericType.Integer8;
				}
				if (value2 < 65535L)
				{
					return nativeNumericType | NativeNumericType.Integer16;
				}
				if (value2 < (long)((ulong)(-1)))
				{
					return nativeNumericType | NativeNumericType.Integer32;
				}
				if (value2 < 18446744073709551615UL)
				{
					return nativeNumericType | NativeNumericType.Integer64;
				}
				return nativeNumericType | NativeNumericType.BigInteger;
			}
			else
			{
				if (value >= -128L && value2 <= 127L)
				{
					return NativeNumericType.Integer8;
				}
				if (value >= -32768L && value2 <= 32767L)
				{
					return NativeNumericType.Integer16;
				}
				if (value >= -2147483648L && value2 <= 2147483647L)
				{
					return NativeNumericType.Integer32;
				}
				if (value >= -9223372036854775808L && value2 <= 9223372036854775807L)
				{
					return NativeNumericType.Integer64;
				}
				return NativeNumericType.BigInteger;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x060043CB RID: 17355 RVA: 0x000D48CC File Offset: 0x000D2ACC
		public IEnumerable<string> CurrencySymbols
		{
			get
			{
				return (from t in base.TypeClusters.SelectMany((SyntacticTypeOptionSet<SyntacticNumericType> t) => t)
					where t.CurrencySymbol.HasValue
					select t.CurrencySymbol.Value).Distinct<string>();
			}
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x000D4950 File Offset: 0x000D2B50
		public RichNumericType()
			: base(DataKind.Numeric)
		{
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x000D495C File Offset: 0x000D2B5C
		public override Optional<object> MaybeCastAsType(string value)
		{
			value = base.Canonicalize(value).OrElseDefault<string>();
			if (value == null)
			{
				return Optional<object>.Nothing;
			}
			double num;
			if (this.ContainsRealSubtype && double.TryParse(value, out num))
			{
				return num.Some<object>();
			}
			int num2;
			if (this.ContainsIntegerSubtype && int.TryParse(value, out num2))
			{
				return num2.Some<object>();
			}
			return Optional<object>.Nothing;
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x000D49C4 File Offset: 0x000D2BC4
		public override bool Equals(RichDataType<SyntacticNumericType> other)
		{
			if (this == other)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			RichNumericType richNumericType = other as RichNumericType;
			if (richNumericType == null)
			{
				return false;
			}
			if (richNumericType.MaxValue == this.MaxValue && richNumericType.MinValue == this.MinValue && richNumericType.NaValueSet.SetEquals(base.NaValueSet))
			{
				return richNumericType.TypeClusters.SelectMany((SyntacticTypeOptionSet<SyntacticNumericType> t) => t).ConvertToHashSet<SyntacticNumericType>().Equals(base.TypeClusters.SelectMany((SyntacticTypeOptionSet<SyntacticNumericType> t) => t));
			}
			return false;
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x000D4A84 File Offset: 0x000D2C84
		protected override void FinishImpl(long numSamples)
		{
			this._nativeType = null;
			base.SuccessOnFinish = !base.EarlyFailure && base.RejectionCount <= 0 && base.AcceptanceCount > 0 && base.NaValueCount < (int)Math.Floor((double)numSamples * Math.Max(Math.Pow(0.5, (double)base.NaValueSet.Count), 0.005));
			if (base.SuccessOnFinish)
			{
				using (List<SyntacticTypeOptionSet<SyntacticNumericType>>.Enumerator enumerator = base.TypeClusters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SyntacticTypeOptionSet<SyntacticNumericType> syntacticTypeOptionSet = enumerator.Current;
						syntacticTypeOptionSet.DropOptionsWithNoExamples();
					}
					return;
				}
			}
			base.TypeClusters.Clear();
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x000D4B50 File Offset: 0x000D2D50
		private SyntacticTypeOptionSet<SyntacticNumericType> UpdateMetadataAndReturnResults(SyntacticTypeOptionSet<SyntacticNumericType> typeOptions, string value)
		{
			if (typeOptions == null)
			{
				return null;
			}
			Optional<string> optional = typeOptions.First<SyntacticNumericType>().Canonicalize(value);
			if (optional.HasValue)
			{
				this.UpdateMetadata(optional.Value);
			}
			return typeOptions;
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x000D4B88 File Offset: 0x000D2D88
		private void UpdateMetadata(string canon)
		{
			BigInteger bigInteger;
			if (BigInteger.TryParse(canon, NumberStyles.Integer, CultureInfo.InvariantCulture, out bigInteger))
			{
				if (!this.ContainsRealSubtype)
				{
					BigInteger bigInteger2 = (this.MinValue.HasValue ? BigInteger.Min(this.MinValue.Value, bigInteger) : bigInteger);
					BigInteger bigInteger3 = (this.MaxValue.HasValue ? BigInteger.Max(this.MaxValue.Value, bigInteger) : bigInteger);
					this._minMaxRange = Record.Create<BigInteger, BigInteger>(bigInteger2, bigInteger3).Some<Record<BigInteger, BigInteger>>();
				}
				this.UpdateScaleAndPrecision(canon.Length, 0, true);
				return;
			}
			if (RichNumericType.CanonicalRealRegex.IsMatch(canon))
			{
				this._minMaxRange = Optional<Record<BigInteger, BigInteger>>.Nothing;
				string[] array = canon.Split(new char[] { '.' });
				int length = array[0].Length;
				int length2 = array[1].Length;
				this.UpdateScaleAndPrecision(length, length2, this.ContainsIntegerSubtype);
			}
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x000D4C6C File Offset: 0x000D2E6C
		private void UpdateScaleAndPrecision(int leftDigits, int rightDigits, bool hasInt)
		{
			int num = leftDigits + rightDigits;
			if (this._maxPrecisionAndScale.HasValue)
			{
				int num2 = Math.Max(this._maxPrecisionAndScale.Value.Item1 - this._maxPrecisionAndScale.Value.Item2, leftDigits);
				int num3 = Math.Max(this._maxPrecisionAndScale.Value.Item2, rightDigits);
				int num4 = num2 + num3;
				this._maxPrecisionAndScale = Record.Create<int, int>(num4, num3).Some<Record<int, int>>();
			}
			else
			{
				this._maxPrecisionAndScale = Record.Create<int, int>(num, rightDigits).Some<Record<int, int>>();
			}
			if (!this.ContainsNonUniformPrecisionAndScale)
			{
				this.ContainsNonUniformPrecisionAndScale = (this.Precision.HasValue && this.Precision.Value != num) || (this.Scale.HasValue && this.Scale.Value != rightDigits);
				if (hasInt || this.ContainsNonUniformPrecisionAndScale)
				{
					this._precisionAndScale = Optional<Record<int, int>>.Nothing;
					return;
				}
				if (!this._precisionAndScale.HasValue)
				{
					this._precisionAndScale = Record.Create<int, int>(num, rightDigits).Some<Record<int, int>>();
				}
			}
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x000D4D89 File Offset: 0x000D2F89
		private static SyntacticTypeOptionSet<SyntacticNumericType> WrapOptions(IEnumerable<SyntacticNumericType> options)
		{
			return SyntacticTypeOptionSet.From<SyntacticNumericType>(options);
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x000D4D91 File Offset: 0x000D2F91
		private static SyntacticTypeOptionSet<SyntacticNumericType> WrapOption(SyntacticNumericType option)
		{
			return SyntacticTypeOptionSet.From<SyntacticNumericType>(option);
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x000D4D9C File Offset: 0x000D2F9C
		protected override SyntacticTypeOptionSet<SyntacticNumericType> ProcessSample(string sample)
		{
			string text = sample;
			this._nativeType = null;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string text2;
			string text3;
			sample = sample.UnquoteStringIfQuoted(out text2, out text3);
			this.AddSubstitutionIfNonEmpty(text2, string.Empty, dictionary);
			this.AddSubstitutionIfNonEmpty(text3, string.Empty, dictionary);
			List<SyntacticNumericType> list = this.DetectCultureNeutralNumericTypes(sample, text2, text3, dictionary).ToList<SyntacticNumericType>();
			if (list.Any<SyntacticNumericType>())
			{
				return this.UpdateMetadataAndReturnResults(RichNumericType.WrapOptions(list), sample);
			}
			List<SyntacticNumericType> list2 = this.DetectNumericTypes(sample, text2, text3, dictionary).ToList<SyntacticNumericType>();
			if (list2.Any<SyntacticNumericType>())
			{
				return this.UpdateMetadataAndReturnResults(RichNumericType.WrapOptions(list2), sample);
			}
			List<SyntacticNumericType> list3 = this.DetectInfinityOrNanTypes(sample, text2, text3, dictionary).ToList<SyntacticNumericType>();
			if (list3.Any<SyntacticNumericType>())
			{
				return RichNumericType.WrapOptions(list3);
			}
			List<SyntacticNumericType> list4 = this.DetectScientificNotationTypes(sample, text2, text3, dictionary).ToList<SyntacticNumericType>();
			if (list4.Any<SyntacticNumericType>())
			{
				return RichNumericType.WrapOptions(list4);
			}
			string text4;
			string text5;
			sample = sample.UnparenthesizeStringIfParenthesized(out text4, out text5);
			this.AddSubstitutionIfNonEmpty(text4, string.Empty, dictionary);
			this.AddSubstitutionIfNonEmpty(text5, string.Empty, dictionary);
			StringRegion stringRegion = new StringRegion(sample, RichNumericType.NumberParsingTokens);
			RichNumericType.ParseContext parseContext = new RichNumericType.ParseContext(dictionary);
			if (this.Parse(stringRegion, parseContext) && !parseContext.Failed)
			{
				return this.UpdateMetadataAndReturnResults(this.InferTypes(parseContext, text4, text5, text2, text3), sample);
			}
			SyntacticNumericType syntacticNumericType = this.ProcessNaValue(text);
			if (syntacticNumericType != null)
			{
				return RichNumericType.WrapOptions(syntacticNumericType.Yield<SyntacticNumericType>());
			}
			return null;
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x000D4EFC File Offset: 0x000D30FC
		protected override void NotifySample(string sample)
		{
			Optional<string> optional = base.Canonicalize(sample);
			if (optional.HasValue)
			{
				this.UpdateMetadata(optional.Value);
			}
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x000D4F27 File Offset: 0x000D3127
		private SyntacticNumericType ProcessNaValue(string sample)
		{
			if (!RichNumericType.DigitsRegex.IsMatch(sample))
			{
				return new SyntacticNumericType(sample);
			}
			return null;
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x060043D8 RID: 17368 RVA: 0x000D4F3E File Offset: 0x000D313E
		private static Dictionary<string, Token> NumberParsingTokens
		{
			get
			{
				return RichNumericType.NumberParsingTokensLazy.Value;
			}
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x000D4F4C File Offset: 0x000D314C
		private static Dictionary<string, Token> BuildNumberParsingTokens()
		{
			Dictionary<string, Token> dictionary = new Dictionary<string, Token>();
			dictionary["Digits"] = new RegexToken("\\d+", "Digits", 0, 0.0, null, true, true, null);
			dictionary["CurrencyToken"] = new RegexToken(RichNumericType.ExtendedCurrencyRegex, "CurrencyToken", 0, 0.0, null, true, true, null);
			dictionary["PlusToken"] = new RegexToken(UnicodeUtils.PlusRegex, "PlusToken", 0, 0.0, null, true, true, null);
			dictionary["MinusToken"] = new RegexToken(UnicodeUtils.MinusRegex, "MinusToken", 0, 0.0, null, true, true, null);
			dictionary["WhiteSpaceToken"] = new RegexToken("\\s+", "WhiteSpaceToken", 0, 0.0, null, true, true, null);
			dictionary["NonNumericToken"] = new RegexToken("[^\\d]", "NonNumericToken", 0, 0.0, null, true, true, null);
			string text = "NumericSeparator";
			string text2 = "|";
			IEnumerable<string> allNumericDelimiters = RichNumericType.AllNumericDelimiters;
			Func<string, string> func;
			if ((func = RichNumericType.<>O.<0>__Escape) == null)
			{
				func = (RichNumericType.<>O.<0>__Escape = new Func<string, string>(Regex.Escape));
			}
			dictionary[text] = new RegexToken(string.Join(text2, allNumericDelimiters.Select(func)), "NumericSeparator", 0, 0.0, null, true, true, null);
			return dictionary;
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x000D50A0 File Offset: 0x000D32A0
		private bool StartsWithToken(StringRegion region, Token token, out string matchString, out StringRegion suffix)
		{
			PositionMatch positionMatch;
			if (region.Cache.TryGetTokenMatchStartingAt(region.Start, token, out positionMatch))
			{
				matchString = region.Slice(region.Start, region.Start + positionMatch.Length).Value;
				suffix = region.Slice(region.Start + positionMatch.Length, region.End);
				return true;
			}
			matchString = null;
			suffix = null;
			return false;
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x000D5109 File Offset: 0x000D3309
		private bool StartsWithCurrency(StringRegion region, out string currencyString, out StringRegion suffix)
		{
			return this.StartsWithToken(region, RichNumericType.NumberParsingTokens["CurrencyToken"], out currencyString, out suffix);
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x000D5123 File Offset: 0x000D3323
		private bool StartsWithNumeric(StringRegion region, out string numericString, out StringRegion suffix)
		{
			return this.StartsWithToken(region, RichNumericType.NumberParsingTokens["Digits"], out numericString, out suffix);
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x000D513D File Offset: 0x000D333D
		private bool StartsWithWhiteSpace(StringRegion region, RichNumericType.ParseContext context, out string whiteSpaceString, out StringRegion suffix)
		{
			return this.StartsWithToken(region, RichNumericType.NumberParsingTokens["WhiteSpaceToken"], out whiteSpaceString, out suffix);
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x000D5158 File Offset: 0x000D3358
		private bool StartsWithSign(StringRegion region, out string signString, out StringRegion suffix)
		{
			return this.StartsWithToken(region, RichNumericType.NumberParsingTokens["PlusToken"], out signString, out suffix) || this.StartsWithToken(region, RichNumericType.NumberParsingTokens["MinusToken"], out signString, out suffix);
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x000D5190 File Offset: 0x000D3390
		private bool StartsWithNumericSeparator(StringRegion region, out string numericSeparatorString, out StringRegion suffix)
		{
			string text;
			StringRegion stringRegion;
			return this.StartsWithToken(region, RichNumericType.NumberParsingTokens["NumericSeparator"], out numericSeparatorString, out suffix) && this.StartsWithNumeric(suffix, out text, out stringRegion);
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x000D51C8 File Offset: 0x000D33C8
		private bool Parse(StringRegion region, RichNumericType.ParseContext context)
		{
			if (region.Length == 0U)
			{
				if (!context.FormatParts.Any<RichNumericType.NumberFormatPart>())
				{
					context.Fail();
					return false;
				}
				if (context.FormatParts.First<RichNumericType.NumberFormatPart>().Kind == RichNumericType.NumberFormatPartKind.NumericSeparator && context.Separators.Count<string>() > 1)
				{
					context.Fail();
					return false;
				}
				return context.FormatParts.Any((RichNumericType.NumberFormatPart fp) => fp.Kind == RichNumericType.NumberFormatPartKind.NumericSection);
			}
			else
			{
				string text;
				StringRegion stringRegion;
				if (this.StartsWithNumeric(region, out text, out stringRegion))
				{
					return this.ParseNumeric(text, stringRegion, context);
				}
				string text2;
				if (this.StartsWithCurrency(region, out text2, out stringRegion))
				{
					return this.ParseCurrency(text2, stringRegion, context);
				}
				string text3;
				if (this.StartsWithSign(region, out text3, out stringRegion))
				{
					return this.ParseSign(text3, stringRegion, context);
				}
				string text4;
				if (this.StartsWithWhiteSpace(region, context, out text4, out stringRegion))
				{
					return this.ParseWhiteSpace(text4, stringRegion, context);
				}
				string text5;
				if (this.StartsWithNumericSeparator(region, out text5, out stringRegion) && !context.FormatParts.Any<RichNumericType.NumberFormatPart>())
				{
					return this.ParseSeparator(text5, stringRegion, context);
				}
				context.Fail();
				return false;
			}
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x000D52D0 File Offset: 0x000D34D0
		private bool ParseSeparator(string separator, StringRegion suffix, RichNumericType.ParseContext context)
		{
			if (context.Separators.Distinct<string>().Count<string>() >= 2)
			{
				context.Fail();
				return false;
			}
			context.FormatParts.Add(new RichNumericType.NumberFormatPart(RichNumericType.NumberFormatPartKind.NumericSeparator, separator));
			string text;
			StringRegion stringRegion;
			if (this.StartsWithNumeric(suffix, out text, out stringRegion))
			{
				return this.ParseNumeric(text, stringRegion, context);
			}
			context.Fail();
			return false;
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x000D5328 File Offset: 0x000D3528
		private bool ParseCurrency(string currency, StringRegion suffix, RichNumericType.ParseContext context)
		{
			if (context.CurrencyString.HasValue)
			{
				context.Fail();
				return false;
			}
			context.CurrencyString = currency.Some<string>();
			context.FormatParts.Add(new RichNumericType.NumberFormatPart(RichNumericType.NumberFormatPartKind.CurrencySymbol, currency));
			return this.Parse(suffix, context);
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x000D5373 File Offset: 0x000D3573
		private bool ParseWhiteSpace(string whiteSpaceString, StringRegion suffixAfterWhiteSpace, RichNumericType.ParseContext context)
		{
			if (whiteSpaceString.Length > 1)
			{
				context.Fail();
				return false;
			}
			context.FormatParts.Add(new RichNumericType.NumberFormatPart(RichNumericType.NumberFormatPartKind.WhiteSpaceString, whiteSpaceString));
			return this.Parse(suffixAfterWhiteSpace, context);
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x000D53A0 File Offset: 0x000D35A0
		private bool ParseSign(string sign, StringRegion suffixAfterSign, RichNumericType.ParseContext context)
		{
			if (sign.Length > 1 || !UnicodeUtils.SignChars.Contains(sign[0]) || context.SignChar.HasValue)
			{
				context.Fail();
				return false;
			}
			context.SignChar = sign[0].Some<char>();
			context.FormatParts.Add(new RichNumericType.NumberFormatPart(RichNumericType.NumberFormatPartKind.SignString, sign));
			return this.Parse(suffixAfterSign, context);
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x000D5410 File Offset: 0x000D3610
		private bool ParseNumeric(string numeric, StringRegion suffix, RichNumericType.ParseContext context)
		{
			if (context.NumericParseComplete)
			{
				context.Fail();
				return false;
			}
			if (!RichNumericType.CultureInvariantNumericRegex.IsMatch(numeric))
			{
				foreach (KeyValuePair<string, string> keyValuePair in RichNumericType.GetSubstitutions(numeric))
				{
					string text;
					if (context.Substitutions.TryGetValue(keyValuePair.Key, out text) && text != keyValuePair.Value)
					{
						context.Fail();
						return false;
					}
					context.Substitutions[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			context.FormatParts.Add(new RichNumericType.NumberFormatPart(RichNumericType.NumberFormatPartKind.NumericSection, numeric));
			string text2;
			StringRegion stringRegion;
			if (!this.StartsWithNumericSeparator(suffix, out text2, out stringRegion))
			{
				context.CompleteNumericParse();
				return this.Parse(suffix, context);
			}
			if (context.FormatParts.Count > 0 && context.FormatParts.First<RichNumericType.NumberFormatPart>().Kind == RichNumericType.NumberFormatPartKind.NumericSeparator)
			{
				context.Fail();
				return false;
			}
			return this.ParseSeparator(text2, stringRegion, context);
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x000D552C File Offset: 0x000D372C
		private void AddSubstitutionIfNonEmpty(string before, string after, Dictionary<string, string> substitutions)
		{
			if (before.Length > 0)
			{
				substitutions[before] = after;
			}
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x000D553F File Offset: 0x000D373F
		private static SyntacticTypeOptionSet<SyntacticNumericType> OptionsForNoSeparators(string regexTemplate, Dictionary<string, string> substitutions, bool isParenthesized, RichNumericType.ParseContext ctx)
		{
			return RichNumericType.WrapOption(new SyntacticNumericType(new Regex(string.Format(regexTemplate, "\\d+"), RegexOptions.Compiled), substitutions, null, null, ctx.CurrencyString.OrElseDefault<string>(), false, isParenthesized || ctx.IsSignNegative, ctx.HasLeadingNegativeSign));
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x000D5580 File Offset: 0x000D3780
		private static SyntacticTypeOptionSet<SyntacticNumericType> OptionsForOneLeadingSeparator(string regexTemplate, Dictionary<string, string> substitutions, bool isParenthesized, RichNumericType.ParseContext ctx)
		{
			string value = ctx.FormatParts.First<RichNumericType.NumberFormatPart>().Value;
			if (value != ".")
			{
				substitutions[value] = ".";
			}
			return RichNumericType.WrapOption(new SyntacticNumericType(new Regex(string.Format(regexTemplate, RichNumericType.CreateDelimitedNumberRegexPattern(null, value, false, true)), RegexOptions.Compiled), substitutions, null, value, ctx.CurrencyString.OrElseDefault<string>(), false, isParenthesized || ctx.IsSignNegative, ctx.HasLeadingNegativeSign));
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x000D55F8 File Offset: 0x000D37F8
		private static SyntacticTypeOptionSet<SyntacticNumericType> OptionsForDistinctRepeatingSeparator(string regexTemplate, Dictionary<string, string> substitutions, bool isParenthesized, RichNumericType.ParseContext ctx)
		{
			List<string> list = ctx.Separators.ToList<string>();
			List<RichNumericType.NumberFormatPart> list2 = ctx.FormatParts.Where((RichNumericType.NumberFormatPart fp) => fp.Kind == RichNumericType.NumberFormatPartKind.NumericSection).ToList<RichNumericType.NumberFormatPart>();
			if (!list2.Skip(1).All((RichNumericType.NumberFormatPart section) => section.Value.Length >= 2 && section.Value.Length <= 3) || list2.Last<RichNumericType.NumberFormatPart>().Value.Length != 3)
			{
				return null;
			}
			string text = string.Format(regexTemplate, RichNumericType.CreateDelimitedNumberRegexPattern(list.First<string>(), null, true, false));
			substitutions[list.First<string>()] = string.Empty;
			return RichNumericType.WrapOption(new SyntacticNumericType(new Regex(text, RegexOptions.Compiled), substitutions, list.First<string>(), null, ctx.CurrencyString.OrElseDefault<string>(), false, isParenthesized || ctx.IsSignNegative, ctx.HasLeadingNegativeSign));
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x000D56DE File Offset: 0x000D38DE
		private static IEnumerable<SyntacticNumericType> GetOptionsForSingleSeparatorJustDecimalPoint(string regexTemplate, string dSeparator, Dictionary<string, string> substitutions, bool isParenthesized, RichNumericType.ParseContext ctx)
		{
			string text = string.Format(regexTemplate, RichNumericType.CreateDelimitedNumberRegexPattern(null, dSeparator, false, true));
			Dictionary<string, string> dictionary = substitutions.ToDictionary<string, string>();
			if (dSeparator != ".")
			{
				dictionary[dSeparator] = ".";
			}
			yield return new SyntacticNumericType(new Regex(text, RegexOptions.Compiled), dictionary, null, dSeparator, ctx.CurrencyString.OrElseDefault<string>(), false, isParenthesized || ctx.IsSignNegative, ctx.HasLeadingNegativeSign);
			yield break;
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x000D570B File Offset: 0x000D390B
		private static IEnumerable<SyntacticNumericType> GetOptionsForSingleSeparatorJustGroupSeparator(string regexTemplate, Dictionary<string, string> substitutions, bool isParenthesized, RichNumericType.ParseContext ctx)
		{
			List<string> list = ctx.Separators.ToList<string>();
			if (ctx.FormatParts.Where((RichNumericType.NumberFormatPart fp) => fp.Kind == RichNumericType.NumberFormatPartKind.NumericSection).Last<RichNumericType.NumberFormatPart>().Value.Length == 3)
			{
				string text = list.First<string>();
				string text2 = string.Format(regexTemplate, RichNumericType.CreateDelimitedNumberRegexPattern(text, null, true, false));
				Dictionary<string, string> dictionary = substitutions.ToDictionary<string, string>();
				dictionary[text] = string.Empty;
				yield return new SyntacticNumericType(new Regex(text2, RegexOptions.Compiled), dictionary, text, null, ctx.CurrencyString.OrElseDefault<string>(), false, isParenthesized || ctx.IsSignNegative, ctx.HasLeadingNegativeSign);
			}
			yield break;
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x000D5730 File Offset: 0x000D3930
		private static IEnumerable<SyntacticNumericType> GetOptionsForSingleSeparatorUnknownGroupSeparator(string regexTemplate, Dictionary<string, string> substitutions, bool isParenthesized, RichNumericType.ParseContext ctx)
		{
			string dSeparator = ctx.Separators.First<string>();
			IEnumerable<string> allNumericDelimiters = RichNumericType.AllNumericDelimiters;
			Func<string, bool> <>9__0;
			Func<string, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (string d) => d != dSeparator);
			}
			foreach (string text in allNumericDelimiters.Where(func))
			{
				string dSeparator2 = dSeparator;
				string text2 = text;
				string text3 = string.Format(regexTemplate, RichNumericType.CreateDelimitedNumberRegexPattern(text2, dSeparator2, false, true));
				Dictionary<string, string> dictionary = substitutions.ToDictionary<string, string>();
				dictionary[dSeparator2] = ".";
				dictionary[text2] = string.Empty;
				yield return new SyntacticNumericType(new Regex(text3, RegexOptions.Compiled), dictionary, text2, dSeparator2, ctx.CurrencyString.OrElseDefault<string>(), false, isParenthesized || ctx.IsSignNegative, ctx.HasLeadingNegativeSign);
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x000D5755 File Offset: 0x000D3955
		private static IEnumerable<SyntacticNumericType> GetOptionsForSingleSeparatorUnknownDecimalPoint(string regexTemplate, Dictionary<string, string> substitutions, bool isParenthesized, RichNumericType.ParseContext ctx)
		{
			List<string> list = ctx.Separators.ToList<string>();
			if (ctx.FormatParts.Where((RichNumericType.NumberFormatPart fp) => fp.Kind == RichNumericType.NumberFormatPartKind.NumericSection).Last<RichNumericType.NumberFormatPart>().Value.Length == 3)
			{
				RichNumericType.<>c__DisplayClass76_0 CS$<>8__locals1 = new RichNumericType.<>c__DisplayClass76_0();
				CS$<>8__locals1.gSeparator = list.First<string>();
				IEnumerable<string> allNumericDelimiters = RichNumericType.AllNumericDelimiters;
				Func<string, bool> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = (string d) => d != CS$<>8__locals1.gSeparator);
				}
				foreach (string text in allNumericDelimiters.Where(func))
				{
					string gSeparator = CS$<>8__locals1.gSeparator;
					string text2 = string.Format(regexTemplate, RichNumericType.CreateDelimitedNumberRegexPattern(gSeparator, text, true, false));
					Dictionary<string, string> dictionary = substitutions.ToDictionary<string, string>();
					if (text != ".")
					{
						dictionary[text] = ".";
					}
					dictionary[gSeparator] = string.Empty;
					yield return new SyntacticNumericType(new Regex(text2, RegexOptions.Compiled), dictionary, gSeparator, text, ctx.CurrencyString.OrElseDefault<string>(), false, isParenthesized || ctx.IsSignNegative, ctx.HasLeadingNegativeSign);
				}
				IEnumerator<string> enumerator = null;
				CS$<>8__locals1 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x000D577C File Offset: 0x000D397C
		private static SyntacticTypeOptionSet<SyntacticNumericType> OptionsForTwoSeparators(string regexTemplate, Dictionary<string, string> substitutions, bool isParenthesized, RichNumericType.ParseContext ctx)
		{
			List<string> list = ctx.Separators.ToList<string>();
			string text = list.First<string>();
			string text2 = list.Last<string>();
			List<RichNumericType.NumberFormatPart> list2 = ctx.FormatParts.Where((RichNumericType.NumberFormatPart fp) => fp.Kind == RichNumericType.NumberFormatPartKind.NumericSection).ToList<RichNumericType.NumberFormatPart>();
			int count = list2.Count;
			if (!list2.Skip(1).Take(count - 2).All((RichNumericType.NumberFormatPart section) => section.Value.Length >= 2 && section.Value.Length <= 3))
			{
				return null;
			}
			string text3 = string.Format(regexTemplate, RichNumericType.CreateDelimitedNumberRegexPattern(text, text2, false, false));
			substitutions[text] = string.Empty;
			if (text2 != ".")
			{
				substitutions[text2] = ".";
			}
			return RichNumericType.WrapOption(new SyntacticNumericType(new Regex(text3, RegexOptions.Compiled), substitutions, text, text2, ctx.CurrencyString.OrElseDefault<string>(), false, isParenthesized || ctx.IsSignNegative, ctx.HasLeadingNegativeSign));
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x000D5874 File Offset: 0x000D3A74
		private static SyntacticTypeOptionSet<SyntacticNumericType> OptionsForDistinctNonRepeatingSeparator(string regexTemplate, Dictionary<string, string> substitutions, bool isParenthesized, RichNumericType.ParseContext ctx)
		{
			string text = ctx.Separators.ToList<string>().First<string>();
			List<SyntacticNumericType> list = new List<SyntacticNumericType>();
			IEnumerable<SyntacticNumericType> optionsForSingleSeparatorJustDecimalPoint = RichNumericType.GetOptionsForSingleSeparatorJustDecimalPoint(regexTemplate, text, substitutions, isParenthesized, ctx);
			IEnumerable<SyntacticNumericType> optionsForSingleSeparatorJustGroupSeparator = RichNumericType.GetOptionsForSingleSeparatorJustGroupSeparator(regexTemplate, substitutions, isParenthesized, ctx);
			int length = ctx.FormatParts.Where((RichNumericType.NumberFormatPart fp) => fp.Kind == RichNumericType.NumberFormatPartKind.NumericSection).Last<RichNumericType.NumberFormatPart>().Value.Length;
			if (text == "." || length == 2)
			{
				list.AddRange(optionsForSingleSeparatorJustDecimalPoint);
				list.AddRange(optionsForSingleSeparatorJustGroupSeparator);
			}
			else
			{
				list.AddRange(optionsForSingleSeparatorJustGroupSeparator);
				list.AddRange(optionsForSingleSeparatorJustDecimalPoint);
			}
			list.AddRange(RichNumericType.GetOptionsForSingleSeparatorUnknownGroupSeparator(regexTemplate, substitutions, isParenthesized, ctx));
			list.AddRange(RichNumericType.GetOptionsForSingleSeparatorUnknownDecimalPoint(regexTemplate, substitutions, isParenthesized, ctx));
			return SyntacticTypeOptionSet.From<SyntacticNumericType>(list);
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x000D593C File Offset: 0x000D3B3C
		private SyntacticTypeOptionSet<SyntacticNumericType> InferTypes(RichNumericType.ParseContext ctx, string leftParen, string rightParen, string leftQuote, string rightQuote)
		{
			bool flag = !string.IsNullOrEmpty(leftParen);
			Dictionary<string, string> dictionary = ctx.Substitutions.ToDictionary<string, string>();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("^");
			stringBuilder.Append(Regex.Escape(leftQuote));
			stringBuilder.Append(Regex.Escape(leftParen));
			bool flag2 = false;
			foreach (RichNumericType.NumberFormatPart numberFormatPart in ctx.FormatParts)
			{
				switch (numberFormatPart.Kind)
				{
				case RichNumericType.NumberFormatPartKind.NumericSection:
					if (!flag2)
					{
						flag2 = true;
						stringBuilder.Append("{0}");
					}
					break;
				case RichNumericType.NumberFormatPartKind.CurrencySymbol:
				case RichNumericType.NumberFormatPartKind.SignString:
					stringBuilder.Append(Regex.Escape(numberFormatPart.Value));
					dictionary[numberFormatPart.Value] = string.Empty;
					break;
				case RichNumericType.NumberFormatPartKind.WhiteSpaceString:
					stringBuilder.Append("\\s");
					dictionary[numberFormatPart.Value] = string.Empty;
					break;
				}
			}
			stringBuilder.Append(Regex.Escape(rightParen));
			stringBuilder.Append(Regex.Escape(rightQuote));
			stringBuilder.Append("$");
			string text = stringBuilder.ToString();
			List<string> list = ctx.Separators.ToList<string>();
			List<string> list2 = (from s in list
				group s by s into g
				select g.Key).ToList<string>();
			if (list2.Count == 0)
			{
				return RichNumericType.OptionsForNoSeparators(text, dictionary, flag, ctx);
			}
			if (list2.Count != 1)
			{
				return RichNumericType.OptionsForTwoSeparators(text, dictionary, flag, ctx);
			}
			if (ctx.FormatParts.First<RichNumericType.NumberFormatPart>().Kind == RichNumericType.NumberFormatPartKind.NumericSeparator)
			{
				if (list.Count > 1)
				{
					throw new InvalidOperationException("Shouldn't have encountered more than one separator, given a leading separator.");
				}
				return RichNumericType.OptionsForOneLeadingSeparator(text, dictionary, flag, ctx);
			}
			else
			{
				if (list.Count > 1)
				{
					return RichNumericType.OptionsForDistinctRepeatingSeparator(text, dictionary, flag, ctx);
				}
				return RichNumericType.OptionsForDistinctNonRepeatingSeparator(text, dictionary, flag, ctx);
			}
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x000D5B5C File Offset: 0x000D3D5C
		private static string WrapInNonCaptureGroupIfNeeded(string pattern, string suffix)
		{
			if (!string.IsNullOrEmpty(suffix))
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("(?:{0}){1}", new object[] { pattern, suffix }));
			}
			return pattern;
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x000D5B88 File Offset: 0x000D3D88
		private static string CreateDelimitedNumberRegexPattern(string groupSeparator, string decimalSeparator, bool mustHaveGroupSeparator, bool mustHaveDecimalSeparator)
		{
			if (string.IsNullOrEmpty(groupSeparator) && string.IsNullOrEmpty(decimalSeparator))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("{0} and {1} cannot both be null or empty.", new object[] { "groupSeparator", "decimalSeparator" })));
			}
			string text = (mustHaveGroupSeparator ? string.Empty : "?");
			string text2 = (mustHaveDecimalSeparator ? string.Empty : "?");
			if (!string.IsNullOrEmpty(groupSeparator) && !string.IsNullOrEmpty(decimalSeparator))
			{
				string text3 = RichNumericType.WrapInNonCaptureGroupIfNeeded(FormattableString.Invariant(FormattableStringFactory.Create("{0}\\d+", new object[] { Regex.Escape(decimalSeparator) })), text2);
				return FormattableString.Invariant(FormattableStringFactory.Create("\\d+(?:{0}\\d{{2,3}})*(?:{1}\\d{{3}}){2}{3}", new object[]
				{
					Regex.Escape(groupSeparator),
					Regex.Escape(groupSeparator),
					text,
					text3
				}));
			}
			if (!string.IsNullOrEmpty(decimalSeparator))
			{
				string text4 = RichNumericType.WrapInNonCaptureGroupIfNeeded(FormattableString.Invariant(FormattableStringFactory.Create("{0}\\d+", new object[] { Regex.Escape(decimalSeparator) })), text2);
				return FormattableString.Invariant(FormattableStringFactory.Create("\\d*{0}", new object[] { text4 }));
			}
			if (!string.IsNullOrEmpty(groupSeparator))
			{
				string text5 = RichNumericType.WrapInNonCaptureGroupIfNeeded(FormattableString.Invariant(FormattableStringFactory.Create("{0}\\d{{3}}", new object[] { Regex.Escape(groupSeparator) })), text);
				return FormattableString.Invariant(FormattableStringFactory.Create("\\d+(?:{0}\\d{{2,3}})*{1}", new object[]
				{
					Regex.Escape(groupSeparator),
					text5
				}));
			}
			return null;
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x000D5CEF File Offset: 0x000D3EEF
		private IEnumerable<SyntacticNumericType> DetectCultureNeutralNumericTypes(string value, string leftQuote, string rightQuote, Dictionary<string, string> substitutions)
		{
			if (RichNumericType.CultureInvariantNumericRegex.IsMatch(value))
			{
				Regex regex = new Regex(string.Concat(new string[] { "^", leftQuote, "[0-9]+", rightQuote, "$" }), RegexOptions.Compiled);
				yield return new SyntacticNumericType(regex, substitutions, null, null, null, false, false, false);
			}
			yield break;
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x000D5D18 File Offset: 0x000D3F18
		private static Dictionary<string, string> GetSubstitutions(string value)
		{
			Dictionary<string, string> candidateSubstMap;
			if (!RichNumericType.NativeDigitSubstitutions.TryGetValue(value.Substring(0, 1), out candidateSubstMap))
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unhandled numeric string: {0}", new object[] { value })));
			}
			if (!value.All((char c) => candidateSubstMap.ContainsKey(new string(c, 1))))
			{
				return null;
			}
			return candidateSubstMap;
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x000D5D80 File Offset: 0x000D3F80
		private IEnumerable<SyntacticNumericType> DetectNumericTypes(string value, string leftQuote, string rightQuote, Dictionary<string, string> substitutions)
		{
			if (!RichNumericType.NumericRegex.IsMatch(value))
			{
				yield break;
			}
			Dictionary<string, string> substitutions2 = RichNumericType.GetSubstitutions(value);
			if (substitutions2 == null)
			{
				yield break;
			}
			foreach (KeyValuePair<string, string> keyValuePair in substitutions2)
			{
				substitutions[keyValuePair.Key] = keyValuePair.Value;
			}
			yield return new SyntacticNumericType(new Regex(string.Concat(new string[] { "^", leftQuote, "\\d+", rightQuote, "$" }), RegexOptions.Compiled), substitutions, null, null, null, false, false, false);
			yield break;
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x000D5DA6 File Offset: 0x000D3FA6
		private IEnumerable<SyntacticNumericType> DetectInfinityOrNanTypes(string value, string leftQuote, string rightQuote, Dictionary<string, string> substitutionMap)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (RichNumericType.PositiveInfinitySymbols.Contains(value))
			{
				substitutionMap[value] = CultureInfo.InvariantCulture.NumberFormat.PositiveInfinitySymbol;
				flag = true;
				flag3 = true;
			}
			else if (RichNumericType.NegativeInfinitySymbols.Contains(value))
			{
				substitutionMap[value] = CultureInfo.InvariantCulture.NumberFormat.NegativeInfinitySymbol;
				flag = true;
				flag4 = true;
			}
			else if (RichNumericType.NaNSymbols.Contains(value))
			{
				substitutionMap[value] = CultureInfo.InvariantCulture.NumberFormat.NaNSymbol;
				flag = true;
				flag2 = true;
			}
			if (flag)
			{
				Regex regex = new Regex(string.Concat(new string[]
				{
					"^",
					leftQuote,
					Regex.Escape(value),
					rightQuote,
					"$"
				}), RegexOptions.Compiled);
				yield return new SyntacticNumericType(regex, substitutionMap, flag2, flag3, flag4);
			}
			yield break;
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x000D5DCC File Offset: 0x000D3FCC
		private IEnumerable<SyntacticNumericType> DetectScientificNotationTypes(string sample, string leftQuote, string rightQuote, Dictionary<string, string> substitutions)
		{
			Match match = RichNumericType.AnchoredScientificNotationRegex.Match(sample);
			if (!match.Success || sample.StartsWith("E") || sample.StartsWith("e"))
			{
				yield break;
			}
			string text = (match.Groups["DecimalPoint"].Success ? match.Groups["DecimalPoint"].Value : ".");
			Regex regex = new Regex(FormattableString.Invariant(FormattableStringFactory.Create("^{0}[+-]?\\d*(?:{1}\\d+)?[eE][+-]?\\d+{2}$", new object[]
			{
				leftQuote,
				Regex.Escape(text),
				rightQuote
			})), RegexOptions.Compiled);
			if (text != ".")
			{
				substitutions[text] = ".";
			}
			yield return new SyntacticNumericType(regex, substitutions, null, text, null, true, false, false);
			yield break;
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x060043F8 RID: 17400 RVA: 0x000D5DF2 File Offset: 0x000D3FF2
		private static string ScientificNotationPattern
		{
			get
			{
				return RichNumericType.ScientificNotationPatternLazy.Value;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x060043F9 RID: 17401 RVA: 0x000D5DFE File Offset: 0x000D3FFE
		private static Regex AnchoredScientificNotationRegex
		{
			get
			{
				return RichNumericType.AnchoredScientificNotationRegexLazy.Value;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x060043FA RID: 17402 RVA: 0x000D5E0A File Offset: 0x000D400A
		private static Regex CultureInvariantNumericRegex
		{
			get
			{
				return RichNumericType.CultureInvariantNumericRegexLazy.Value;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x060043FB RID: 17403 RVA: 0x000D5E16 File Offset: 0x000D4016
		private static Regex NumericRegex
		{
			get
			{
				return RichNumericType.NumericRegexLazy.Value;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x060043FC RID: 17404 RVA: 0x000D5E22 File Offset: 0x000D4022
		private static string ExtendedCurrencyRegexPattern
		{
			get
			{
				return RichNumericType.ExtendedCurrencyRegexPatternLazy.Value;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x000D5E2E File Offset: 0x000D402E
		private static Regex ExtendedCurrencyRegex
		{
			get
			{
				return RichNumericType.ExtendedCurrencyRegexLazy.Value;
			}
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x000D5E3C File Offset: 0x000D403C
		private static string CreateExtendedCurrencyRegexPattern()
		{
			Regex currencyRegex = new Regex("^\\p{Sc}$", RegexOptions.Compiled);
			return string.Join("|", (from cs in CultureDetails.CurrencySymbols
				where !currencyRegex.IsMatch(cs)
				select FormattableString.Invariant(FormattableStringFactory.Create("({0})", new object[] { Regex.Escape(cs) })) into cs
				orderby cs.Length descending
				select cs).AppendItem("\\p{Sc}"));
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x000D5ED2 File Offset: 0x000D40D2
		internal static HashSet<string> AllNumericDelimiters
		{
			get
			{
				return RichNumericType.AllNumericDelimitersLazy.Value;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x000D5EDE File Offset: 0x000D40DE
		private static HashSet<string> NaNSymbols
		{
			get
			{
				return RichNumericType.NaNSymbolsLazy.Value;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06004401 RID: 17409 RVA: 0x000D5EEA File Offset: 0x000D40EA
		private static HashSet<string> PositiveInfinitySymbols
		{
			get
			{
				return RichNumericType.PositiveInfinitySymbolsLazy.Value;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x000D5EF6 File Offset: 0x000D40F6
		private static HashSet<string> NegativeInfinitySymbols
		{
			get
			{
				return RichNumericType.NegativeInfinitySymbolsLazy.Value;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06004403 RID: 17411 RVA: 0x000D5F02 File Offset: 0x000D4102
		private static Dictionary<string, Dictionary<string, string>> NativeDigitSubstitutions
		{
			get
			{
				return RichNumericType.NativeDigitSubstitutionsLazy.Value;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06004404 RID: 17412 RVA: 0x000D5F0E File Offset: 0x000D410E
		private static Regex CanonicalRealRegex
		{
			get
			{
				return RichNumericType.CanonicalRealRegexLazy.Value;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06004405 RID: 17413 RVA: 0x000D5F1A File Offset: 0x000D411A
		public override long MinRequiredSamplesForSuccess
		{
			get
			{
				return (long)((double)base.NaValueCount / Math.Max(Math.Pow(0.5, (double)base.NaValueSet.Count), 0.005));
			}
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000D5F4D File Offset: 0x000D414D
		private static HashSet<string> ConstructNaNSymbols()
		{
			return CultureDetails.NaNSymbols.ConvertToHashSet(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x000D5F5E File Offset: 0x000D415E
		private static HashSet<string> ConstructPositiveInfinitySymbols()
		{
			return CultureDetails.PositiveInfinitySymbols.ConvertToHashSet(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x000D5F6F File Offset: 0x000D416F
		private static HashSet<string> ConstructNegativeInfinitySymbols()
		{
			return CultureDetails.NegativeInfinitySymbols.ConvertToHashSet(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x000D5F80 File Offset: 0x000D4180
		private static Dictionary<string, Dictionary<string, string>> ConstructNativeDigitSubstitutions()
		{
			Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();
			foreach (string[] array in CultureDetails.NativeDigits)
			{
				if (array.Length != 10)
				{
					throw new NotImplementedException("Encountered more than 10 native digits in culture. This isn't supported.");
				}
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
				for (int j = 0; j < 10; j++)
				{
					dictionary2[array[j]] = j.ToString(CultureInfo.InvariantCulture);
				}
				foreach (string text in array)
				{
					dictionary[text] = dictionary2;
				}
			}
			return dictionary;
		}

		// Token: 0x04001EAA RID: 7850
		private Optional<Record<BigInteger, BigInteger>> _minMaxRange;

		// Token: 0x04001EAB RID: 7851
		private Optional<Record<int, int>> _precisionAndScale;

		// Token: 0x04001EAC RID: 7852
		private Optional<Record<int, int>> _maxPrecisionAndScale;

		// Token: 0x04001EAD RID: 7853
		private const double BaseAllowedNaFraction = 0.5;

		// Token: 0x04001EAF RID: 7855
		private NativeNumericType? _nativeType;

		// Token: 0x04001EB0 RID: 7856
		private static readonly Regex DigitsRegex = new Regex("\\d", RegexOptions.Compiled);

		// Token: 0x04001EB1 RID: 7857
		private static readonly Lazy<Dictionary<string, Token>> NumberParsingTokensLazy = new Lazy<Dictionary<string, Token>>(new Func<Dictionary<string, Token>>(RichNumericType.BuildNumberParsingTokens));

		// Token: 0x04001EB2 RID: 7858
		private const string DecimalPointGroupName = "DecimalPoint";

		// Token: 0x04001EB3 RID: 7859
		private const string ZeroOrMoreDigits = "\\d*";

		// Token: 0x04001EB4 RID: 7860
		private const string OneOrMoreDigits = "\\d+";

		// Token: 0x04001EB5 RID: 7861
		private static readonly Lazy<string> ScientificNotationPatternLazy = new Lazy<string>(() => FormattableString.Invariant(FormattableStringFactory.Create("[+-]?{0}(?:(?<{1}>{2}){3})?[eE][+-]?{4}", new object[]
		{
			"\\d*",
			"DecimalPoint",
			string.Join("|", RichNumericType.AllNumericDelimiters.Select(new Func<string, string>(Regex.Escape))),
			"\\d+",
			"\\d+"
		})));

		// Token: 0x04001EB6 RID: 7862
		private static readonly Lazy<Regex> AnchoredScientificNotationRegexLazy = new Lazy<Regex>(() => new Regex(FormattableString.Invariant(FormattableStringFactory.Create("^{0}$", new object[] { RichNumericType.ScientificNotationPattern })), RegexOptions.Compiled));

		// Token: 0x04001EB7 RID: 7863
		private static readonly Lazy<Regex> CultureInvariantNumericRegexLazy = new Lazy<Regex>(() => new Regex("^[0-9]+$", RegexOptions.Compiled));

		// Token: 0x04001EB8 RID: 7864
		private static readonly Lazy<Regex> NumericRegexLazy = new Lazy<Regex>(() => new Regex("^\\d+$", RegexOptions.Compiled));

		// Token: 0x04001EB9 RID: 7865
		private static readonly Lazy<string> ExtendedCurrencyRegexPatternLazy = new Lazy<string>(new Func<string>(RichNumericType.CreateExtendedCurrencyRegexPattern), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04001EBA RID: 7866
		private static readonly Lazy<Regex> ExtendedCurrencyRegexLazy = new Lazy<Regex>(() => new Regex(RichNumericType.ExtendedCurrencyRegexPattern, RegexOptions.ExplicitCapture | RegexOptions.Compiled));

		// Token: 0x04001EBB RID: 7867
		private static readonly Lazy<HashSet<string>> AllNumericDelimitersLazy = new Lazy<HashSet<string>>(delegate
		{
			if (!(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == "/"))
			{
				return CultureDetails.NumberSeparators.Where((string d) => d != "/").ConvertToHashSet<string>();
			}
			return CultureDetails.NumberSeparators.ConvertToHashSet<string>();
		});

		// Token: 0x04001EBC RID: 7868
		private static readonly Lazy<HashSet<string>> NaNSymbolsLazy = new Lazy<HashSet<string>>(new Func<HashSet<string>>(RichNumericType.ConstructNaNSymbols));

		// Token: 0x04001EBD RID: 7869
		private static readonly Lazy<HashSet<string>> PositiveInfinitySymbolsLazy = new Lazy<HashSet<string>>(new Func<HashSet<string>>(RichNumericType.ConstructPositiveInfinitySymbols));

		// Token: 0x04001EBE RID: 7870
		private static readonly Lazy<HashSet<string>> NegativeInfinitySymbolsLazy = new Lazy<HashSet<string>>(new Func<HashSet<string>>(RichNumericType.ConstructNegativeInfinitySymbols));

		// Token: 0x04001EBF RID: 7871
		private static readonly Lazy<Dictionary<string, Dictionary<string, string>>> NativeDigitSubstitutionsLazy = new Lazy<Dictionary<string, Dictionary<string, string>>>(new Func<Dictionary<string, Dictionary<string, string>>>(RichNumericType.ConstructNativeDigitSubstitutions));

		// Token: 0x04001EC0 RID: 7872
		private static readonly Lazy<Regex> CanonicalRealRegexLazy = new Lazy<Regex>(() => new Regex("^[0-9]+(:?\\.[0-9]+)?$", RegexOptions.Compiled));

		// Token: 0x02000A99 RID: 2713
		private enum NumberFormatPartKind
		{
			// Token: 0x04001EC2 RID: 7874
			NumericSection,
			// Token: 0x04001EC3 RID: 7875
			CurrencySymbol,
			// Token: 0x04001EC4 RID: 7876
			SignString,
			// Token: 0x04001EC5 RID: 7877
			NumericSeparator,
			// Token: 0x04001EC6 RID: 7878
			WhiteSpaceString
		}

		// Token: 0x02000A9A RID: 2714
		private class NumberFormatPart
		{
			// Token: 0x17000C0F RID: 3087
			// (get) Token: 0x0600440B RID: 17419 RVA: 0x000D616C File Offset: 0x000D436C
			public RichNumericType.NumberFormatPartKind Kind { get; }

			// Token: 0x17000C10 RID: 3088
			// (get) Token: 0x0600440C RID: 17420 RVA: 0x000D6174 File Offset: 0x000D4374
			public string Value { get; }

			// Token: 0x0600440D RID: 17421 RVA: 0x000D617C File Offset: 0x000D437C
			public NumberFormatPart(RichNumericType.NumberFormatPartKind kind, string value)
			{
				this.Kind = kind;
				this.Value = value;
			}
		}

		// Token: 0x02000A9B RID: 2715
		private class ParseContext
		{
			// Token: 0x17000C11 RID: 3089
			// (get) Token: 0x0600440E RID: 17422 RVA: 0x000D6194 File Offset: 0x000D4394
			public IEnumerable<string> Separators
			{
				get
				{
					return from fp in this.FormatParts
						where fp.Kind == RichNumericType.NumberFormatPartKind.NumericSeparator
						select fp.Value;
				}
			}

			// Token: 0x17000C12 RID: 3090
			// (get) Token: 0x0600440F RID: 17423 RVA: 0x000D61EF File Offset: 0x000D43EF
			// (set) Token: 0x06004410 RID: 17424 RVA: 0x000D61F7 File Offset: 0x000D43F7
			public Optional<string> CurrencyString { get; set; }

			// Token: 0x17000C13 RID: 3091
			// (get) Token: 0x06004411 RID: 17425 RVA: 0x000D6200 File Offset: 0x000D4400
			// (set) Token: 0x06004412 RID: 17426 RVA: 0x000D6208 File Offset: 0x000D4408
			public Optional<char> SignChar { get; set; }

			// Token: 0x17000C14 RID: 3092
			// (get) Token: 0x06004413 RID: 17427 RVA: 0x000D6211 File Offset: 0x000D4411
			// (set) Token: 0x06004414 RID: 17428 RVA: 0x000D6219 File Offset: 0x000D4419
			public bool NumericParseComplete { get; private set; }

			// Token: 0x17000C15 RID: 3093
			// (get) Token: 0x06004415 RID: 17429 RVA: 0x000D6224 File Offset: 0x000D4424
			public bool IsSignNegative
			{
				get
				{
					return this.SignChar.HasValue && UnicodeUtils.MinusChars.Contains(this.SignChar.Value);
				}
			}

			// Token: 0x17000C16 RID: 3094
			// (get) Token: 0x06004416 RID: 17430 RVA: 0x000D625B File Offset: 0x000D445B
			public bool HasLeadingNegativeSign
			{
				get
				{
					return this.IsSignNegative && this.FormatParts.First<RichNumericType.NumberFormatPart>().Kind == RichNumericType.NumberFormatPartKind.SignString;
				}
			}

			// Token: 0x17000C17 RID: 3095
			// (get) Token: 0x06004417 RID: 17431 RVA: 0x000D627A File Offset: 0x000D447A
			public Dictionary<string, string> Substitutions { get; }

			// Token: 0x17000C18 RID: 3096
			// (get) Token: 0x06004418 RID: 17432 RVA: 0x000D6282 File Offset: 0x000D4482
			public List<RichNumericType.NumberFormatPart> FormatParts { get; }

			// Token: 0x17000C19 RID: 3097
			// (get) Token: 0x06004419 RID: 17433 RVA: 0x000D628A File Offset: 0x000D448A
			// (set) Token: 0x0600441A RID: 17434 RVA: 0x000D6292 File Offset: 0x000D4492
			public bool Failed { get; private set; }

			// Token: 0x0600441B RID: 17435 RVA: 0x000D629B File Offset: 0x000D449B
			public ParseContext(IEnumerable<KeyValuePair<string, string>> substitutions)
			{
				this.Substitutions = substitutions.ToDictionary<string, string>();
				this.FormatParts = new List<RichNumericType.NumberFormatPart>();
			}

			// Token: 0x0600441C RID: 17436 RVA: 0x000D62BA File Offset: 0x000D44BA
			public void Fail()
			{
				this.Failed = true;
			}

			// Token: 0x0600441D RID: 17437 RVA: 0x000D62C3 File Offset: 0x000D44C3
			public void CompleteNumericParse()
			{
				this.NumericParseComplete = true;
			}
		}

		// Token: 0x02000A9D RID: 2717
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001ED2 RID: 7890
			public static Func<string, string> <0>__Escape;
		}
	}
}
