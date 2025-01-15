using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001677 RID: 5751
	internal class SuggestOutput
	{
		// Token: 0x0600C03B RID: 49211 RVA: 0x002965BC File Offset: 0x002947BC
		internal SuggestOutput(IRow inputRow, SuggestOutputOptions options, LearnOptions learnOptions, CancellationToken cancellationToken)
		{
			this._inputRow = inputRow;
			this._options = options;
			this._recognition = new Recognition(null, learnOptions.DataCultures, learnOptions.ColumnNamePriority, learnOptions.EnableMatchNames, learnOptions.EnableMatchUnicode, learnOptions.FromNumbersColumnLimit, learnOptions.EnableNegativePosition, learnOptions.EnableFromNumberStr, learnOptions.EnableRoundNumber, learnOptions.EnableRoundDateTime, learnOptions.EnableDateTimePart, learnOptions.EnableTimePart, learnOptions.EnableArithmetic, learnOptions.ForwardFillMaxScale, NumberSourceKind.Input, DateTimeSourceKind.Input, false, false, learnOptions.NumberFormatMaxLeadingDigits, null, cancellationToken);
		}

		// Token: 0x0600C03C RID: 49212 RVA: 0x00296644 File Offset: 0x00294844
		public IReadOnlyList<OutputSuggestion> Get(string prefix, CancellationToken cancellationToken = default(CancellationToken))
		{
			SuggestOutput.<>c__DisplayClass4_0 CS$<>8__locals1 = new SuggestOutput.<>c__DisplayClass4_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.prefix = prefix;
			if (string.IsNullOrEmpty(CS$<>8__locals1.prefix))
			{
				return this.GetDefault(cancellationToken);
			}
			CS$<>8__locals1.prefixWords = (from match in SuggestOutput._wordRegex.NonCachingMatches(CS$<>8__locals1.prefix)
				select match.Value).ToList<string>();
			char c = CS$<>8__locals1.prefix.Last<char>();
			bool flag = CS$<>8__locals1.<Get>g__NumberChar|0(c);
			CS$<>8__locals1.searchTerm = (flag ? new string(CS$<>8__locals1.prefix.Reverse<char>().TakeWhile(new Func<char, bool>(CS$<>8__locals1.<Get>g__NumberChar|0)).Reverse<char>()
				.ToArray<char>()) : (c.IsDelimiter() ? string.Empty : (CS$<>8__locals1.prefixWords.LastOrDefault<string>() ?? string.Empty)));
			IEnumerable<string> enumerable = this.NumberCompletions(CS$<>8__locals1.searchTerm, false);
			IEnumerable<string> enumerable2 = this.DateCompletions(CS$<>8__locals1.searchTerm, false);
			cancellationToken.ThrowIfCancellationRequested();
			if (!this._recognition.InputStrings(this._inputRow, null).Any<StringInput>())
			{
				return SuggestOutput.Suggestions(enumerable2.Concat(enumerable));
			}
			IEnumerable<string> enumerable3 = this.WordCompletions(CS$<>8__locals1.searchTerm);
			IEnumerable<string> enumerable4 = from completion in flag ? enumerable : enumerable3.Take(this._options.SubstringLimit).Concat(enumerable2).Concat(enumerable)
				where completion != CS$<>8__locals1.prefix && !CS$<>8__locals1.prefixWords.Contains(completion, StringComparer.OrdinalIgnoreCase)
				select CS$<>8__locals1.prefix.Splice(CS$<>8__locals1.prefix.Length - CS$<>8__locals1.searchTerm.Length, completion, CS$<>8__locals1.searchTerm.Length);
			cancellationToken.ThrowIfCancellationRequested();
			return SuggestOutput.Suggestions(enumerable4);
		}

		// Token: 0x0600C03D RID: 49213 RVA: 0x002967D4 File Offset: 0x002949D4
		public IReadOnlyList<OutputSuggestion> GetDefault(CancellationToken cancellationToken = default(CancellationToken))
		{
			IEnumerable<string> enumerable = this.DateCompletions(null, true).ToList<string>();
			if (enumerable.Any<string>())
			{
				return SuggestOutput.Suggestions(enumerable);
			}
			IEnumerable<string> enumerable2 = this.NumberCompletions(null, true).ToList<string>();
			if (enumerable2.Any<string>())
			{
				return SuggestOutput.Suggestions(enumerable2);
			}
			cancellationToken.ThrowIfCancellationRequested();
			return SuggestOutput.Suggestions(this.WordCompletions(null).Take(this._options.SubstringLimitDefault));
		}

		// Token: 0x0600C03E RID: 49214 RVA: 0x00296840 File Offset: 0x00294A40
		public IReadOnlyList<OutputSuggestion> GetTokens(CancellationToken cancellationToken = default(CancellationToken))
		{
			IEnumerable<string> enumerable = this.NumberCompletions(null, false);
			IEnumerable<string> enumerable2 = this.DateCompletions(null, false);
			IEnumerable<string> enumerable3 = this.WordCompletions(null).Take(this._options.SubstringLimit).Concat(enumerable2)
				.Concat(enumerable);
			cancellationToken.ThrowIfCancellationRequested();
			return SuggestOutput.Suggestions(enumerable3);
		}

		// Token: 0x0600C03F RID: 49215 RVA: 0x00296890 File Offset: 0x00294A90
		private IEnumerable<string> DateCompletions(string searchTerm = null, bool useDefaultDescriptors = false)
		{
			IReadOnlyList<DateTime> readOnlyList = this._recognition.DateTimes(this._inputRow, DateTimeSourceKind.InputAndParsed).DistinctValues;
			if (useDefaultDescriptors)
			{
				readOnlyList = readOnlyList.Take(1).ToList<DateTime>();
			}
			return from <>h__TransparentIdentifier2 in (from <>h__TransparentIdentifier0 in readOnlyList.SelectMany(delegate(DateTime date)
					{
						if (!useDefaultDescriptors)
						{
							return SuggestOutput.DateSuggestionDescriptors(this._options.SuggestionCultures);
						}
						return SuggestOutput.DateDefaultSuggestionDescriptors(this._options.SuggestionCultures);
					}, (DateTime date, DateTimeDescriptor desc) => new { date, desc })
					let mask = desc.Mask
					where date != date.Date || (date == date.Date && !mask.Contains("h") && !mask.Contains("H") && mask != "tt")
					select new
					{
						<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
						completion = Operators.FormatDateTime(date, desc)
					}).Where(delegate(<>h__TransparentIdentifier2)
				{
					if (searchTerm == null)
					{
						return true;
					}
					if (this._options.DateMatch != SuggestOutputDateMatch.Contains)
					{
						return <>h__TransparentIdentifier2.completion.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase);
					}
					return <>h__TransparentIdentifier2.completion.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0;
				})
				select <>h__TransparentIdentifier2.completion;
		}

		// Token: 0x0600C040 RID: 49216 RVA: 0x002969B8 File Offset: 0x00294BB8
		private IEnumerable<string> NumberCompletions(string searchTerm = null, bool useDefaultDescriptors = false)
		{
			IEnumerable<decimal> enumerable = this._recognition.Numbers(this._inputRow, false, NumberSourceKind.InputAndParsed).DistinctValues.Union(this._recognition.FindArithmeticNumbers(this._inputRow, null));
			if (useDefaultDescriptors)
			{
				enumerable = enumerable.Take(1).ToList<decimal>();
			}
			return from <>h__TransparentIdentifier0 in enumerable.SelectMany(delegate(decimal number)
				{
					if (!useDefaultDescriptors)
					{
						return this.NumberSuggestionDescriptors();
					}
					return this.NumberDefaultSuggestionDescriptors();
				}, (decimal number, FormatNumberDescriptor desc) => new { number, desc })
				let completion = Operators.FormatNumber(number, desc)
				where searchTerm == null || completion.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase)
				select completion;
		}

		// Token: 0x0600C041 RID: 49217 RVA: 0x00296AB0 File Offset: 0x00294CB0
		private IEnumerable<string> WordCompletions(string searchTerm = null)
		{
			return from word in this.Words()
				where searchTerm == null || word.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase)
				select word;
		}

		// Token: 0x0600C042 RID: 49218 RVA: 0x00296AE4 File Offset: 0x00294CE4
		private IReadOnlyList<string> Words()
		{
			object[] array = new object[] { "Words", this._inputRow };
			IReadOnlyList<string> readOnlyList;
			if (this._recognition.CacheTryGetValue<IReadOnlyList<string>>(array, out readOnlyList))
			{
				return readOnlyList;
			}
			readOnlyList = (from input in this._recognition.InputStrings(this._inputRow, null).DistinctValues
				from match in SuggestOutput._wordRegex.NonCachingMatches(input)
				select match.Value).Distinct<string>().ToList<string>();
			return this._recognition.CacheSet<IReadOnlyList<string>>(array, readOnlyList);
		}

		// Token: 0x0600C043 RID: 49219 RVA: 0x00296B90 File Offset: 0x00294D90
		private static IReadOnlyList<OutputSuggestion> Suggestions(IEnumerable<string> sources)
		{
			sources = sources.Distinct<string>().ToReadOnlyList<string>();
			int count = sources.Count<string>();
			return sources.Select((string source, int idx) => new OutputSuggestion
			{
				Text = source,
				Score = (double)(count - idx)
			}).ToList<OutputSuggestion>();
		}

		// Token: 0x0600C044 RID: 49220 RVA: 0x00296BD4 File Offset: 0x00294DD4
		private static IEnumerable<DateTimeDescriptor> DateDefaultSuggestionDescriptors(IEnumerable<CultureInfo> cultures)
		{
			return from desc in (from mask in new string[] { "{ShortDatePattern}", "{ShortDatePattern} {ShortTimePattern}", "{US_MediumDatePattern}", "{LongDatePattern}", "{LongDatePattern} {ShortTimePattern}" }
					from culture in cultures
					let isUs = culture.Name == "en-US"
					select new DateTimeDescriptor
					{
						Mask = mask.Replace("{ShortDatePattern}", culture.DateTimeFormat.ShortDatePattern).Replace("{ShortTimePattern}", culture.DateTimeFormat.ShortTimePattern).Replace("{LongDatePattern}", culture.DateTimeFormat.LongDatePattern)
							.Replace("{US_MediumDatePattern}", isUs ? "MMMM d, yyyy" : string.Empty),
						Culture = culture
					}).Distinct<DateTimeDescriptor>()
				where !string.IsNullOrEmpty(desc.Mask)
				select desc;
		}

		// Token: 0x0600C045 RID: 49221 RVA: 0x00296CC0 File Offset: 0x00294EC0
		private static IEnumerable<DateTimeDescriptor> DateSuggestionDescriptors(IEnumerable<CultureInfo> cultures)
		{
			return from desc in (from mask in new string[]
					{
						"{ShortDatePattern}", "{ShortDatePattern} {ShortTimePattern}", "MMMM d, yyyy", "MMMM d, yyyy h:mm tt", "MMMM d, yyyy HH:mm", "MMMM d", "MMMM yyyy", "MMMM", "MMM d, yyyy", "MMM d, yyyy h:mm tt",
						"MMM d, yyyy HH:mm", "MMM d", "MMM yyyy", "MMM", "dd-MMM-yyyy", "dd-MMM-yyyy h:mm tt", "dd-MMM-yyyy HH:mm", "{LongDatePattern}", "dddd, MMMM d, yyyy", "dddd, MMMM d, yyyy h:mm tt",
						"dddd, MMMM d, yyyy HH:mm", "dddd h:mm tt", "dddd HH:mm", "dddd", "ddd h:mm tt", "ddd HH:mm", "ddd", "yyyy/MM/dd", "yyyy/MM/dd h:mm tt", "yyyy/MM/dd HH:mm",
						"yyyy'-'MM'-'dd'T'HH':'mm':'ss", "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", "yyyy", "h:mm tt", "HH:mm", "tt"
					}
					from culture in cultures
					select new DateTimeDescriptor
					{
						Mask = mask.Replace("{ShortDatePattern}", culture.DateTimeFormat.ShortDatePattern).Replace("{ShortTimePattern}", culture.DateTimeFormat.ShortTimePattern).Replace("{LongDatePattern}", culture.DateTimeFormat.LongDatePattern),
						Culture = culture
					}).Distinct<DateTimeDescriptor>()
				where !string.IsNullOrEmpty(desc.Mask)
				select desc;
		}

		// Token: 0x0600C046 RID: 49222 RVA: 0x00296E75 File Offset: 0x00295075
		private IEnumerable<FormatNumberDescriptor> NumberDefaultSuggestionDescriptors()
		{
			return this.NumberSuggestionDescriptors();
		}

		// Token: 0x0600C047 RID: 49223 RVA: 0x00296E80 File Offset: 0x00295080
		private IEnumerable<FormatNumberDescriptor> NumberSuggestionDescriptors()
		{
			return from desc in this._recognition.NumberFormatDescriptors.Where(delegate(FormatNumberDescriptor desc)
				{
					bool flag = !desc.IncludePercentSymbol && desc.LeadingDigits == 1;
					if (flag)
					{
						bool flag2 = !desc.IncludeCurrencySymbol && desc.TrailingDigits <= 2;
						if (!flag2)
						{
							bool flag3 = desc.IncludeCurrencySymbol && desc.IncludeGroupSeparator;
							if (flag3)
							{
								int trailingDigits = desc.TrailingDigits;
								bool flag4 = trailingDigits == 0 || trailingDigits == 2;
								flag3 = flag4;
							}
							flag2 = flag3;
						}
						flag = flag2;
					}
					return flag;
				})
				orderby !desc.IncludeSymbol, !desc.IncludeGroupSeparator, desc.ToFormatString().Length
				select desc;
		}

		// Token: 0x04004A38 RID: 19000
		private readonly IRow _inputRow;

		// Token: 0x04004A39 RID: 19001
		private readonly SuggestOutputOptions _options;

		// Token: 0x04004A3A RID: 19002
		private readonly Recognition _recognition;

		// Token: 0x04004A3B RID: 19003
		private static readonly Regex _wordRegex = "(?:(?:\\p{N}+)?(?:[\\p{L}]+)(?:\\p{N}+)?|(?:[\\p{L}]+)(?:\\p{N}+)?(?:\\p{L}+)?)".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
	}
}
