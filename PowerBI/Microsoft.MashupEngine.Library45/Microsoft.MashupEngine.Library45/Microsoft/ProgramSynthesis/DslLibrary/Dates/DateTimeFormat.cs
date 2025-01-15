using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.FormatParsing;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000827 RID: 2087
	[Parseable("TryParseFromXML", ParseHumanReadableString = "TryParseHumanReadable")]
	[JsonObject(MemberSerialization.OptIn)]
	public class DateTimeFormat : IRenderableLiteral, IEquatable<DateTimeFormat>, IFormatPart<DateTimeFormatMatch, PartialDateTime, StringRegion, DateTimeFormat>
	{
		// Token: 0x06002CF6 RID: 11510 RVA: 0x0007F498 File Offset: 0x0007D698
		public DateTimeFormat(params DateTimeFormatPart[] formatParts)
			: this(formatParts)
		{
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x0007F4A4 File Offset: 0x0007D6A4
		public DateTimeFormat(IEnumerable<DateTimeFormatPart> formatParts)
		{
			this.AllFormatParts = formatParts.ToList<DateTimeFormatPart>();
			this.FormatParts = this.AllFormatParts.Where((DateTimeFormatPart fp) => fp.MaximumLength > 0).ToList<DateTimeFormatPart>();
			if (this.AllFormatParts.Count == this.FormatParts.Count)
			{
				this.FormatParts = this.AllFormatParts;
			}
			this.FormatString = string.Concat(this.FormatParts.Select((DateTimeFormatPart fp) => fp.RenderHumanReadable()));
			this.MatchedParts = new DateTimePartSet(this.FormatParts.SelectMany((DateTimeFormatPart fp) => fp.MatchedPart.AsEnumerable<DateTimePart>()));
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x0007F586 File Offset: 0x0007D786
		[JsonConstructor]
		public DateTimeFormat(string format)
			: this(DateTimeFormatPart.ParseFormatString(format))
		{
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x0007F594 File Offset: 0x0007D794
		public IReadOnlyList<DateTimeFormatPart> AllFormatParts { get; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002CFA RID: 11514 RVA: 0x0007F59C File Offset: 0x0007D79C
		public IReadOnlyList<DateTimeFormatPart> FormatParts { get; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x0007F5A4 File Offset: 0x0007D7A4
		[JsonProperty(PropertyName = "Format")]
		public string FormatString { get; }

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x0007F5AC File Offset: 0x0007D7AC
		public string PosixOutputFormatString
		{
			get
			{
				IReadOnlyList<string> readOnlyList = this.FormatParts.Select((DateTimeFormatPart fp) => fp.PosixOutputFormatString).ToList<string>();
				if (!readOnlyList.Any((string p) => p == null))
				{
					return string.Concat(readOnlyList);
				}
				return null;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x0007F618 File Offset: 0x0007D818
		public string PosixParsingFormatString
		{
			get
			{
				IReadOnlyList<string> readOnlyList = this.FormatParts.Select((DateTimeFormatPart fp) => fp.PosixParsingFormatString).ToList<string>();
				if (!readOnlyList.Any((string p) => p == null))
				{
					return string.Concat(readOnlyList);
				}
				return null;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x0007F684 File Offset: 0x0007D884
		public DateTimePartSet MatchedParts { get; }

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x0007F68C File Offset: 0x0007D88C
		internal Token Token
		{
			get
			{
				if (this.FormatParts.Count == 1)
				{
					return this.FormatParts.Single<DateTimeFormatPart>().Token.OrElseDefault<Token>();
				}
				if (!this.FormatParts.All((DateTimeFormatPart fp) => fp.Token.HasValue && fp.Token.Value.Name == "Digits"))
				{
					return null;
				}
				return this.FormatParts.First<DateTimeFormatPart>().Token.Value;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x0007F703 File Offset: 0x0007D903
		public bool IsNumeric
		{
			get
			{
				return this.FormatParts.All((DateTimeFormatPart fp) => fp.IsNumeric);
			}
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x0007F72F File Offset: 0x0007D92F
		public string FormatStringFor(DateTimeFormat.Target target, bool strict = true)
		{
			return this.FormatWith(DateTimeFormatTranslator.For(target), strict);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x0007F73E File Offset: 0x0007D93E
		internal string FormatWith(DateTimeFormatTranslator translator, bool strict = true)
		{
			return translator.Translate(this, strict);
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0007F748 File Offset: 0x0007D948
		public bool Equals(DateTimeFormat other)
		{
			return other != null && (this == other || string.Equals(this.FormatString, other.FormatString));
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x0007F766 File Offset: 0x0007D966
		public string RenderHumanReadable()
		{
			return this.FormatString.ToLiteral(null);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x0007F774 File Offset: 0x0007D974
		public XElement RenderXML()
		{
			XElement xelement = new XElement("DateTimeFormat", new XAttribute("format", this.FormatString));
			foreach (DateTimeFormatPart dateTimeFormatPart in this.FormatParts)
			{
				xelement.Add(dateTimeFormatPart.RenderXML());
			}
			return xelement;
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x0007F7EC File Offset: 0x0007D9EC
		private IEnumerable<DateTimeFormatMatch> ParseAllNext(StringRegion sr, int index = 0, uint? start = null, PartialDateTime partialDateTime = null)
		{
			if (index == this.FormatParts.Count)
			{
				return Seq.Of<DateTimeFormatMatch>(new DateTimeFormatMatch[]
				{
					new DateTimeFormatMatch(sr.Slice(sr.Start, start ?? sr.Start), this, partialDateTime)
				});
			}
			StringRegion stringRegion = sr.Slice(start ?? sr.Start, sr.End);
			if ((ulong)stringRegion.Length < (ulong)((long)this.FormatParts.Skip(index).Sum((DateTimeFormatPart fp) => fp.MinimumLength)))
			{
				return Enumerable.Empty<DateTimeFormatMatch>();
			}
			partialDateTime = partialDateTime ?? PartialDateTime.Empty;
			DateTimeFormatPart dateTimeFormatPart = this.FormatParts[index];
			uint? num = start;
			uint end = sr.End;
			if ((num.GetValueOrDefault() >= end) & (num != null))
			{
				return Enumerable.Empty<DateTimeFormatMatch>();
			}
			IEnumerable<DateTimeFormatMatch> enumerable = Enumerable.Empty<DateTimeFormatMatch>();
			foreach (Record<StringRegion, int> record in dateTimeFormatPart.ParseAllNext(stringRegion))
			{
				uint end2 = record.Item1.End;
				Optional<PartialDateTime> optional = partialDateTime.Some<PartialDateTime>();
				if (dateTimeFormatPart.MatchedPart.HasValue)
				{
					optional = partialDateTime.With(dateTimeFormatPart.MatchedPart.Value, record.Item2);
					if (!optional.HasValue)
					{
						return Enumerable.Empty<DateTimeFormatMatch>();
					}
				}
				enumerable = enumerable.Concat(this.ParseAllNext(sr, index + 1, new uint?(end2), optional.Value));
			}
			return enumerable;
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x0007F9B8 File Offset: 0x0007DBB8
		private Optional<DateTimeFormatMatch> MaybeParseFull(StringRegion sr)
		{
			if (this.FormatParts.Count > 2)
			{
				DateTimeFormatPart dateTimeFormatPart = this.FormatParts.LastOrDefault<DateTimeFormatPart>();
				if (dateTimeFormatPart != null && dateTimeFormatPart.IsNumericAtEnd && this.FormatParts.IsNumericIncludingAtEnd())
				{
					DateTimeFormatPart numericAtEndPart = this.FormatParts.Last<DateTimeFormatPart>();
					Func<Record<StringRegion, int>, bool> <>9__5;
					Func<PartialDateTime, DateTimeFormatMatch> <>9__7;
					for (int i = Math.Min(numericAtEndPart.MaximumLength, (int)sr.Length); i >= numericAtEndPart.MinimumLength; i--)
					{
						Optional<Record<StringRegion, int>> parseOpt = numericAtEndPart.ParseNext(sr.Slice((uint)((ulong)sr.End - (ulong)((long)i)), sr.End));
						Optional<Record<StringRegion, int>> parseOpt2 = parseOpt;
						Func<Record<StringRegion, int>, bool> func;
						if ((func = <>9__5) == null)
						{
							func = (<>9__5 = (Record<StringRegion, int> r) => r.Item1.End != sr.End);
						}
						if (!parseOpt2.Select(func).OrElse(true))
						{
							Optional<PartialDateTime> optional = new DateTimeFormat(this.FormatParts.SkipLast(1)).MaybeParseFull(sr.RelativeSlice(0U, parseOpt.Value.Item1.Start)).SelectMany((DateTimeFormatMatch restParse) => restParse.PartialDateTime.With(numericAtEndPart.MatchedPart.Value, parseOpt.Value.Item2));
							Func<PartialDateTime, DateTimeFormatMatch> func2;
							if ((func2 = <>9__7) == null)
							{
								func2 = (<>9__7 = (PartialDateTime combinedPdt) => new DateTimeFormatMatch(sr, this, combinedPdt));
							}
							return optional.Select(func2);
						}
					}
					return default(Optional<DateTimeFormatMatch>);
				}
			}
			PartialDateTimeData empty = PartialDateTimeData.Empty;
			int num = this.FormatParts.Sum((DateTimeFormatPart fp) => fp.MinimumLength);
			if ((ulong)sr.Length < (ulong)((long)num))
			{
				return Optional<DateTimeFormatMatch>.Nothing;
			}
			int num2 = this.FormatParts.Sum((DateTimeFormatPart fp) => fp.MaximumLength);
			if ((ulong)sr.Length > (ulong)((long)num2))
			{
				return Optional<DateTimeFormatMatch>.Nothing;
			}
			int num3 = num;
			int num4 = num2;
			IEnumerable<DateTimeFormatPart> enumerable = this.FormatParts.Where((DateTimeFormatPart p) => p.MinimumLength != p.MaximumLength && !p.AllowsLeadingZeros());
			bool flag = this.FormatParts.Where((DateTimeFormatPart fp) => fp.AllowsLeadingZeros()).HasAtLeast(2);
			bool flag2 = this.IsNumeric && num != num2;
			if (flag2 && enumerable.HasAtLeast(2))
			{
				return Optional<DateTimeFormatMatch>.Nothing;
			}
			StringRegion stringRegion = sr;
			foreach (DateTimeFormatPart dateTimeFormatPart2 in this.FormatParts)
			{
				if (flag2 && dateTimeFormatPart2.MinimumLength != dateTimeFormatPart2.MaximumLength)
				{
					uint num5;
					if (dateTimeFormatPart2.AllowsLeadingZeros() && flag)
					{
						num5 = (uint)dateTimeFormatPart2.MaximumLength;
						if (stringRegion.Length < num5)
						{
							return Optional<DateTimeFormatMatch>.Nothing;
						}
					}
					else
					{
						num5 = sr.Length - (uint)(num - dateTimeFormatPart2.MinimumLength);
					}
					StringRegion stringRegion2 = stringRegion.Slice(stringRegion.Start, stringRegion.Start + num5);
					Optional<int> optional2 = dateTimeFormatPart2.ParseFullString(stringRegion2.Value);
					if (!optional2.HasValue)
					{
						return Optional<DateTimeFormatMatch>.Nothing;
					}
					empty.TryAdd(dateTimeFormatPart2.MatchedPart.Value, optional2.Value);
					stringRegion = stringRegion.Slice(stringRegion.Start + num5, stringRegion.End);
				}
				else
				{
					Optional<Record<StringRegion, int>> optional3 = dateTimeFormatPart2.ParseNext(stringRegion);
					if (!optional3.HasValue)
					{
						return Optional<DateTimeFormatMatch>.Nothing;
					}
					Record<StringRegion, int> value = optional3.Value;
					stringRegion = stringRegion.Slice(value.Item1.End, stringRegion.End);
					if (dateTimeFormatPart2.MatchedPart.HasValue)
					{
						empty.TryAdd(dateTimeFormatPart2.MatchedPart.Value, value.Item2);
					}
				}
				num3 -= dateTimeFormatPart2.MinimumLength;
				num4 -= dateTimeFormatPart2.MaximumLength;
				if ((ulong)stringRegion.Length < (ulong)((long)num3))
				{
					return Optional<DateTimeFormatMatch>.Nothing;
				}
				if ((ulong)stringRegion.Length > (ulong)((long)num4))
				{
					return Optional<DateTimeFormatMatch>.Nothing;
				}
			}
			return from pdt in PartialDateTime.Create(empty)
				select new DateTimeFormatMatch(sr, this, pdt);
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x0007FE84 File Offset: 0x0007E084
		public string ToString(DateTime dt)
		{
			return this.ToString(PartialDateTime.Create(dt));
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0007FE92 File Offset: 0x0007E092
		public string ToString(DateTimeOffset dt)
		{
			return this.ToString(PartialDateTime.Create(dt));
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x0007FEA0 File Offset: 0x0007E0A0
		public string ToString(PartialDateTime dt)
		{
			IReadOnlyList<string> readOnlyList = this.FormatParts.Select((DateTimeFormatPart fp) => fp.ToString(dt)).ToList<string>();
			if (readOnlyList.Any((string p) => p == null))
			{
				return null;
			}
			return string.Concat(readOnlyList);
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x0007FF08 File Offset: 0x0007E108
		public static DateTimeFormat TryParseFromXML(XElement literal)
		{
			if (literal.Name != "DateTimeFormat")
			{
				return null;
			}
			IEnumerable<XElement> enumerable = literal.Elements();
			Func<XElement, DateTimeFormatPart> func;
			if ((func = DateTimeFormat.<>O.<0>__TryParseFromXML) == null)
			{
				func = (DateTimeFormat.<>O.<0>__TryParseFromXML = new Func<XElement, DateTimeFormatPart>(DateTimeFormatPart.TryParseFromXML));
			}
			IEnumerable<DateTimeFormatPart> enumerable2 = enumerable.Select(func).ToList<DateTimeFormatPart>();
			if (!enumerable2.Any((DateTimeFormatPart p) => p == null))
			{
				return new DateTimeFormat(enumerable2);
			}
			return null;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x0007FF8C File Offset: 0x0007E18C
		internal static DateTimeFormat TryParseHumanReadable(string literal)
		{
			return (from format in StdLiteralParsing.TryParse<string>(literal, default(DeserializationContext))
				select new DateTimeFormat(format)).OrElseDefault<DateTimeFormat>();
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x0007FFD1 File Offset: 0x0007E1D1
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((DateTimeFormat)obj)));
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x0007FFFF File Offset: 0x0007E1FF
		public override int GetHashCode()
		{
			if (this.FormatString == null)
			{
				return 0;
			}
			return this.FormatString.GetHashCode();
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x00080016 File Offset: 0x0007E216
		public override string ToString()
		{
			return this.FormatString;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x0008001E File Offset: 0x0007E21E
		internal IEnumerable<DateTimeFormatMatch> AllParses(LearningCacheSubstring ss)
		{
			StringRegion stringRegion = (ss as StringRegion) ?? new StringRegion(ss.Cache).Slice(ss.Start, ss.End);
			uint num;
			for (uint start = stringRegion.Start; start < stringRegion.End; start = num + 1U)
			{
				foreach (DateTimeFormatMatch dateTimeFormatMatch in this.ParseAllNext(stringRegion.Slice(start, stringRegion.End), 0, null, null))
				{
					yield return dateTimeFormatMatch;
				}
				IEnumerator<DateTimeFormatMatch> enumerator = null;
				num = start;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x00080038 File Offset: 0x0007E238
		internal Optional<DateTimeFormatMatch> MaybeParse(LearningCacheSubstring ss)
		{
			StringRegion stringRegion = (ss as StringRegion) ?? new StringRegion(ss.Cache).Slice(ss.Start, ss.End);
			return this.MaybeParseFull(stringRegion);
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x00080073 File Offset: 0x0007E273
		public Optional<int> GetNextPossibleMatchPosition(StringRegion unparsedSuffix)
		{
			return this.GetAllPossibleMatchPositions(unparsedSuffix).MaybeFirst<int>();
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x00080084 File Offset: 0x0007E284
		public IEnumerable<int> GetAllPossibleMatchPositions(StringRegion unparsedSuffix)
		{
			IReadOnlyCollection<DateTimeFormatMatch> matchesFor = DateFormatCache.For(unparsedSuffix, HeuristicsMode.AllowMostFormats).GetMatchesFor(this);
			IEnumerable<int> enumerable;
			if (matchesFor == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = from m in matchesFor
					select (int)m.Region.Start into idx
					where (long)idx >= (long)((ulong)unparsedSuffix.Start)
					select idx;
			}
			return enumerable ?? Enumerable.Empty<int>();
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000800F8 File Offset: 0x0007E2F8
		public IEnumerable<DateTimeFormatMatch> Parse(StringRegion unparsedSuffix)
		{
			IEnumerable<DateTimeFormatMatch> formatMatchesStartingAt = DateFormatCache.For(unparsedSuffix, HeuristicsMode.AllowMostFormats).GetFormatMatchesStartingAt(this, unparsedSuffix.Start);
			return formatMatchesStartingAt ?? Enumerable.Empty<DateTimeFormatMatch>();
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x00080123 File Offset: 0x0007E323
		public static DateTimeFormat Empty { get; } = new DateTimeFormat(new DateTimeFormatPart[] { EmptyDateTimeFormatPart.Instance });

		// Token: 0x02000828 RID: 2088
		public enum Target
		{
			// Token: 0x04001590 RID: 5520
			PosixFormatting,
			// Token: 0x04001591 RID: 5521
			PosixParsing,
			// Token: 0x04001592 RID: 5522
			MomentJS,
			// Token: 0x04001593 RID: 5523
			DayJSFormatting,
			// Token: 0x04001594 RID: 5524
			PowerAppsFormatting
		}

		// Token: 0x02000829 RID: 2089
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001595 RID: 5525
			public static Func<XElement, DateTimeFormatPart> <0>__TryParseFromXML;
		}
	}
}
