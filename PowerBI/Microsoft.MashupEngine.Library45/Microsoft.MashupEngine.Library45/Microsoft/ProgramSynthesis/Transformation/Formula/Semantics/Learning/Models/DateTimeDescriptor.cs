using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016C7 RID: 5831
	public class DateTimeDescriptor : ProgramDescriptor, IEquatable<DateTimeDescriptor>
	{
		// Token: 0x17002103 RID: 8451
		// (get) Token: 0x0600C263 RID: 49763 RVA: 0x0029DF56 File Offset: 0x0029C156
		// (set) Token: 0x0600C264 RID: 49764 RVA: 0x0029DF5E File Offset: 0x0029C15E
		public bool AllowFormat { get; set; }

		// Token: 0x17002104 RID: 8452
		// (get) Token: 0x0600C265 RID: 49765 RVA: 0x0029DF67 File Offset: 0x0029C167
		// (set) Token: 0x0600C266 RID: 49766 RVA: 0x0029DF6F File Offset: 0x0029C16F
		public bool AllowParse { get; set; }

		// Token: 0x17002105 RID: 8453
		// (get) Token: 0x0600C267 RID: 49767 RVA: 0x0029DF78 File Offset: 0x0029C178
		// (set) Token: 0x0600C268 RID: 49768 RVA: 0x0029DF80 File Offset: 0x0029C180
		public bool AllowParsePartial { get; set; }

		// Token: 0x17002106 RID: 8454
		// (get) Token: 0x0600C269 RID: 49769 RVA: 0x0029DF89 File Offset: 0x0029C189
		// (set) Token: 0x0600C26A RID: 49770 RVA: 0x0029DF91 File Offset: 0x0029C191
		public CultureInfo Culture { get; set; }

		// Token: 0x17002107 RID: 8455
		// (get) Token: 0x0600C26B RID: 49771 RVA: 0x0029DF9C File Offset: 0x0029C19C
		public HashSet<char> FormattedLowerFirstChars
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._formattedLowerFirstChars) == null)
				{
					IEnumerable<char> enumerable = DateTimeDescriptor.LoadFormattedLowerFirstChars(this.Mask, this.Culture);
					Func<char, char> func;
					if ((func = DateTimeDescriptor.<>O.<0>__ToLower) == null)
					{
						func = (DateTimeDescriptor.<>O.<0>__ToLower = new Func<char, char>(char.ToLower));
					}
					hashSet = (this._formattedLowerFirstChars = enumerable.Select(func).ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x17002108 RID: 8456
		// (get) Token: 0x0600C26C RID: 49772 RVA: 0x0029DFF4 File Offset: 0x0029C1F4
		public HashSet<char> FormattedLowerLastChars
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._formattedLowerLastChars) == null)
				{
					IEnumerable<char> enumerable = DateTimeDescriptor.LoadFormattedLowerLastChars(this.Mask, this.Culture);
					Func<char, char> func;
					if ((func = DateTimeDescriptor.<>O.<0>__ToLower) == null)
					{
						func = (DateTimeDescriptor.<>O.<0>__ToLower = new Func<char, char>(char.ToLower));
					}
					hashSet = (this._formattedLowerLastChars = enumerable.Select(func).ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x17002109 RID: 8457
		// (get) Token: 0x0600C26D RID: 49773 RVA: 0x0029E04C File Offset: 0x0029C24C
		public int FormattedMaxLength
		{
			get
			{
				int num = this._formattedMaxLength.GetValueOrDefault();
				if (this._formattedMaxLength == null)
				{
					num = this.LoadFormattedMaxLength(this.Mask, this.Culture);
					this._formattedMaxLength = new int?(num);
					return num;
				}
				return num;
			}
		}

		// Token: 0x1700210A RID: 8458
		// (get) Token: 0x0600C26E RID: 49774 RVA: 0x0029E094 File Offset: 0x0029C294
		public int FormattedMinLength
		{
			get
			{
				int num = this._formattedMinLength.GetValueOrDefault();
				if (this._formattedMinLength == null)
				{
					num = this.LoadFormattedMinLength(this.Mask, this.Culture);
					this._formattedMinLength = new int?(num);
					return num;
				}
				return num;
			}
		}

		// Token: 0x1700210B RID: 8459
		// (get) Token: 0x0600C26F RID: 49775 RVA: 0x0029E0DC File Offset: 0x0029C2DC
		// (set) Token: 0x0600C270 RID: 49776 RVA: 0x0029E0E4 File Offset: 0x0029C2E4
		public bool IsPartial { get; set; }

		// Token: 0x1700210C RID: 8460
		// (get) Token: 0x0600C271 RID: 49777 RVA: 0x0029E0ED File Offset: 0x0029C2ED
		public string Locale
		{
			get
			{
				CultureInfo culture = this.Culture;
				if (culture == null)
				{
					return null;
				}
				return culture.Name;
			}
		}

		// Token: 0x1700210D RID: 8461
		// (get) Token: 0x0600C272 RID: 49778 RVA: 0x0029E100 File Offset: 0x0029C300
		// (set) Token: 0x0600C273 RID: 49779 RVA: 0x0029E108 File Offset: 0x0029C308
		public string Mask { get; set; }

		// Token: 0x1700210E RID: 8462
		// (get) Token: 0x0600C274 RID: 49780 RVA: 0x0029E114 File Offset: 0x0029C314
		public HashSet<char> MaskDelimiterHashSet
		{
			get
			{
				HashSet<char> hashSet;
				if ((hashSet = this._maskDelimiterHashSet) == null)
				{
					hashSet = (this._maskDelimiterHashSet = this.MaskDelimiters.ConvertToHashSet<char>());
				}
				return hashSet;
			}
		}

		// Token: 0x1700210F RID: 8463
		// (get) Token: 0x0600C275 RID: 49781 RVA: 0x0029E140 File Offset: 0x0029C340
		public string MaskDelimiters
		{
			get
			{
				string text;
				if ((text = this._maskDelimiters) == null)
				{
					text = (this._maskDelimiters = this.LoadMaskDelimiters());
				}
				return text;
			}
		}

		// Token: 0x17002110 RID: 8464
		// (get) Token: 0x0600C276 RID: 49782 RVA: 0x0029E168 File Offset: 0x0029C368
		public bool OutputAllNumbers
		{
			get
			{
				bool flag = this._outputAllNumbers.GetValueOrDefault();
				bool flag2;
				if (this._outputAllNumbers != null)
				{
					flag2 = flag;
				}
				else
				{
					string mask = this.Mask;
					bool flag3 = mask == "%d" || mask == "dd" || mask == "%M" || mask == "MM" || mask == "yy" || mask == "yyyy";
					flag = flag3;
					this._outputAllNumbers = new bool?(flag);
					flag2 = flag;
				}
				return flag2;
			}
		}

		// Token: 0x17002111 RID: 8465
		// (get) Token: 0x0600C277 RID: 49783 RVA: 0x0029E200 File Offset: 0x0029C400
		public string Pattern
		{
			get
			{
				string text;
				if ((text = this._pattern) == null)
				{
					text = (this._pattern = DateTimeDescriptor.LoadPattern(this.Mask, this.Culture));
				}
				return text;
			}
		}

		// Token: 0x17002112 RID: 8466
		// (get) Token: 0x0600C278 RID: 49784 RVA: 0x0029E234 File Offset: 0x0029C434
		public Regex Regex
		{
			get
			{
				Regex regex;
				if ((regex = this._regex) == null)
				{
					regex = (this._regex = this.Pattern.ToRegex(true));
				}
				return regex;
			}
		}

		// Token: 0x0600C279 RID: 49785 RVA: 0x0029E260 File Offset: 0x0029C460
		public bool Equals(DateTimeDescriptor other)
		{
			return other != null && this.ToEqualString() == other.ToEqualString();
		}

		// Token: 0x0600C27A RID: 49786 RVA: 0x0029E27E File Offset: 0x0029C47E
		public override bool Equals(object other)
		{
			return this.Equals(other as DateTimeDescriptor);
		}

		// Token: 0x0600C27B RID: 49787 RVA: 0x0029E28C File Offset: 0x0029C48C
		public override int GetHashCode()
		{
			return this.ToEqualString().GetHashCode();
		}

		// Token: 0x0600C27C RID: 49788 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool IsCompatible(ProgramDescriptor other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600C27D RID: 49789 RVA: 0x0029E299 File Offset: 0x0029C499
		public static bool operator ==(DateTimeDescriptor left, DateTimeDescriptor right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C27E RID: 49790 RVA: 0x0029E2AF File Offset: 0x0029C4AF
		public static bool operator !=(DateTimeDescriptor left, DateTimeDescriptor right)
		{
			return !(left == right);
		}

		// Token: 0x0600C27F RID: 49791 RVA: 0x0029E2BC File Offset: 0x0029C4BC
		public string ToEqualString()
		{
			string text;
			if ((text = this._toEqualString) == null)
			{
				text = (this._toEqualString = string.Concat(new string[]
				{
					"Mask=",
					this.Mask,
					"; Locale=",
					this.Locale,
					"; ",
					string.Format("AllowFormat={0}; ", this.AllowFormat),
					string.Format("AllowParse={0}; ", this.AllowParse),
					string.Format("AllowParsePartial={0}; ", this.AllowParsePartial),
					string.Format("IsPartial={0}; ", this.IsPartial),
					string.Format("Regex={0}; ", this.Regex)
				}));
			}
			return text;
		}

		// Token: 0x0600C280 RID: 49792 RVA: 0x0029E38C File Offset: 0x0029C58C
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Concat(new string[] { "{[", this.Locale, "]", this.Mask, "}" }));
			}
			return text;
		}

		// Token: 0x0600C281 RID: 49793 RVA: 0x0029E3E4 File Offset: 0x0029C5E4
		private static IEnumerable<char> LoadFormattedLowerFirstChars(string mask, CultureInfo culture)
		{
			if (mask == "dd")
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			bool flag = mask == "%d" || mask == "d";
			if (flag)
			{
				return DateTimeDescriptor._digitLookup["9"];
			}
			if (mask == "MM")
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			flag = mask == "%M" || mask == "M";
			if (flag)
			{
				return DateTimeDescriptor._digitLookup["9"];
			}
			if (mask.StartsWith("yyyy"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.StartsWith("yy"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.StartsWith("MMMM"))
			{
				return from n in culture.DateTimeFormat.MonthNames
					where !string.IsNullOrEmpty(n)
					select n into d
					select d[0];
			}
			if (mask.StartsWith("MMM"))
			{
				return from n in culture.DateTimeFormat.AbbreviatedMonthNames
					where !string.IsNullOrEmpty(n)
					select n into d
					select d[0];
			}
			if (mask.StartsWith("MM"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.StartsWith("M"))
			{
				return DateTimeDescriptor._digitLookup["9"];
			}
			if (mask.StartsWith("dddd"))
			{
				return from n in culture.DateTimeFormat.DayNames
					where !string.IsNullOrEmpty(n)
					select n into d
					select d[0];
			}
			if (mask.StartsWith("ddd"))
			{
				return from n in culture.DateTimeFormat.AbbreviatedDayNames
					where !string.IsNullOrEmpty(n)
					select n into d
					select d[0];
			}
			if (mask.StartsWith("dd"))
			{
				return DateTimeDescriptor._digitLookup["03"];
			}
			if (mask.StartsWith("d"))
			{
				return DateTimeDescriptor._digitLookup["9"];
			}
			if (mask.StartsWith("HH"))
			{
				return DateTimeDescriptor._digitLookup["02"];
			}
			if (mask.StartsWith("H"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.StartsWith("hh"))
			{
				return DateTimeDescriptor._digitLookup["05"];
			}
			if (mask.StartsWith("h"))
			{
				return DateTimeDescriptor._digitLookup["9"];
			}
			if (mask.StartsWith("mm"))
			{
				return DateTimeDescriptor._digitLookup["05"];
			}
			if (mask.StartsWith("m"))
			{
				return DateTimeDescriptor._digitLookup["9"];
			}
			if (mask.StartsWith("ss"))
			{
				return DateTimeDescriptor._digitLookup["05"];
			}
			if (mask.StartsWith("s"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.StartsWith("tt"))
			{
				string amdesignator = culture.DateTimeFormat.AMDesignator;
				string pmdesignator = culture.DateTimeFormat.PMDesignator;
				return ((!string.IsNullOrEmpty(amdesignator)) ? new char?(amdesignator[0]) : null).Yield<char?>().Concat(((!string.IsNullOrEmpty(pmdesignator)) ? new char?(pmdesignator[0]) : null).Yield<char?>()).Collect<char>();
			}
			return new char[0];
		}

		// Token: 0x0600C282 RID: 49794 RVA: 0x0029E82C File Offset: 0x0029CA2C
		private static IEnumerable<char> LoadFormattedLowerLastChars(string mask, CultureInfo culture)
		{
			if (mask == "dd")
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			bool flag = mask == "%d" || mask == "d";
			if (flag)
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask == "MM")
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			flag = mask == "%M" || mask == "M";
			if (flag)
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("yyyy"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("yy"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("MMMM"))
			{
				return from n in culture.DateTimeFormat.MonthNames
					where !string.IsNullOrEmpty(n)
					select n into d
					select d[d.Length - 1];
			}
			if (mask.EndsWith("MMM"))
			{
				return from n in culture.DateTimeFormat.AbbreviatedMonthNames
					where !string.IsNullOrEmpty(n)
					select n into d
					select d[d.Length - 1];
			}
			if (mask.EndsWith("MM"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("M"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("dddd"))
			{
				return from n in culture.DateTimeFormat.DayNames
					where !string.IsNullOrEmpty(n)
					select n into d
					select d[d.Length - 1];
			}
			if (mask.EndsWith("ddd"))
			{
				return from n in culture.DateTimeFormat.AbbreviatedDayNames
					where !string.IsNullOrEmpty(n)
					select n into d
					select d[d.Length - 1];
			}
			if (mask.EndsWith("dd"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("d"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("HH"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("H"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("hh"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("h"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("mm"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("m"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("ss"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("s"))
			{
				return DateTimeDescriptor._digitLookup["10"];
			}
			if (mask.EndsWith("tt"))
			{
				string amdesignator = culture.DateTimeFormat.AMDesignator;
				string pmdesignator = culture.DateTimeFormat.PMDesignator;
				return ((!string.IsNullOrEmpty(amdesignator)) ? new char?(amdesignator[amdesignator.Length - 1]) : null).Yield<char?>().Concat(((!string.IsNullOrEmpty(pmdesignator)) ? new char?(pmdesignator[pmdesignator.Length - 1]) : null).Yield<char?>()).Collect<char>();
			}
			if (mask.EndsWith("Z'") || mask.EndsWith("Z"))
			{
				return 'z'.Yield<char>();
			}
			return new char[0];
		}

		// Token: 0x0600C283 RID: 49795 RVA: 0x0029ECA4 File Offset: 0x0029CEA4
		private int LoadFormattedMaxLength(string mask, CultureInfo culture)
		{
			int num = 0;
			foreach (Match match in DateTimeDescriptor._maskLiteralRegex.NonCachingMatches(mask))
			{
				num += match.Value.Count((char c) => c != '\'');
				mask = mask.Replace(match.Value, string.Empty);
			}
			if (mask.Contains("yyyy"))
			{
				num += 4;
			}
			else if (mask.Contains("yy"))
			{
				num += 2;
			}
			if (mask.Contains("fffffff"))
			{
				num += 7;
			}
			else if (mask.Contains("fff"))
			{
				num += 3;
			}
			if (mask.Contains("MMMM"))
			{
				num += culture.DateTimeFormat.MonthNames.Where((string n) => !string.IsNullOrEmpty(n)).Max((string n) => n.Length);
			}
			else if (mask.Contains("MMM"))
			{
				num += culture.DateTimeFormat.AbbreviatedMonthNames.Where((string n) => !string.IsNullOrEmpty(n)).Max((string n) => n.Length);
			}
			else if (mask.Contains("MM") || mask.Contains("M") || mask == "%M")
			{
				num += 2;
			}
			if (mask.Contains("dddd"))
			{
				num += culture.DateTimeFormat.DayNames.Where((string n) => !string.IsNullOrEmpty(n)).Max((string n) => n.Length);
			}
			else if (mask.Contains("ddd"))
			{
				num += culture.DateTimeFormat.AbbreviatedDayNames.Where((string n) => !string.IsNullOrEmpty(n)).Max((string n) => n.Length);
			}
			else if (mask.Contains("dd") || mask.Contains("d") || mask == "%d")
			{
				num += 2;
			}
			if (mask.Contains("H") || mask.Contains("HH"))
			{
				num += 2;
			}
			if (mask.Contains("h") || mask.Contains("hh"))
			{
				num += 2;
			}
			if (mask.Contains("m") || mask.Contains("mm"))
			{
				num += 2;
			}
			if (mask.Contains("s") || mask.Contains("ss"))
			{
				num += 2;
			}
			if (mask.Contains("tt"))
			{
				num += Math.Max(culture.DateTimeFormat.AMDesignator.Length, culture.DateTimeFormat.PMDesignator.Length);
			}
			num += mask.Count((char c) => !this._maskChars.Contains(c));
			return num;
		}

		// Token: 0x0600C284 RID: 49796 RVA: 0x0029F02C File Offset: 0x0029D22C
		private int LoadFormattedMinLength(string mask, CultureInfo culture)
		{
			int num = 0;
			foreach (Match match in DateTimeDescriptor._maskLiteralRegex.NonCachingMatches(mask))
			{
				num += match.Value.Count((char c) => c != '\'');
				mask = mask.Replace(match.Value, string.Empty);
			}
			if (mask.Contains("yyyy"))
			{
				num += 4;
			}
			else if (mask.Contains("yy"))
			{
				num += 2;
			}
			if (mask.Contains("fffffff"))
			{
				num += 7;
			}
			else if (mask.Contains("fff"))
			{
				num += 3;
			}
			if (mask.Contains("MMMM"))
			{
				num += culture.DateTimeFormat.MonthNames.Where((string n) => !string.IsNullOrEmpty(n)).Min((string n) => n.Length);
			}
			else if (mask.Contains("MMM"))
			{
				num += culture.DateTimeFormat.AbbreviatedMonthNames.Where((string n) => !string.IsNullOrEmpty(n)).Min((string n) => n.Length);
			}
			else if (mask.Contains("MM") || mask.Contains("M") || mask == "%M")
			{
				num++;
			}
			if (mask.Contains("dddd"))
			{
				num += culture.DateTimeFormat.DayNames.Where((string n) => !string.IsNullOrEmpty(n)).Min((string n) => n.Length);
			}
			else if (mask.Contains("ddd"))
			{
				num += culture.DateTimeFormat.AbbreviatedDayNames.Where((string n) => !string.IsNullOrEmpty(n)).Min((string n) => n.Length);
			}
			else if (mask.Contains("dd") || mask.Contains("d") || mask == "%d")
			{
				num++;
			}
			if (mask.Contains("H") || mask.Contains("HH"))
			{
				num++;
			}
			if (mask.Contains("h") || mask.Contains("hh"))
			{
				num++;
			}
			if (mask.Contains("m") || mask.Contains("mm"))
			{
				num++;
			}
			if (mask.Contains("s") || mask.Contains("ss"))
			{
				num++;
			}
			if (mask.Contains("tt"))
			{
				num += Math.Min(culture.DateTimeFormat.AMDesignator.Length, culture.DateTimeFormat.PMDesignator.Length);
			}
			num += mask.Count((char c) => !this._maskChars.Contains(c));
			return num;
		}

		// Token: 0x0600C285 RID: 49797 RVA: 0x0029F3B4 File Offset: 0x0029D5B4
		private string LoadMaskDelimiters()
		{
			if (this.OutputAllNumbers)
			{
				return string.Empty;
			}
			return (from c in this.Mask.Replace("'", string.Empty)
				where c.IsDelimiter()
				select c).ToJoinString();
		}

		// Token: 0x0600C286 RID: 49798 RVA: 0x0029F410 File Offset: 0x0029D610
		private static string LoadPattern(string mask, CultureInfo culture)
		{
			DateTimeDescriptor.<>c__DisplayClass71_0 CS$<>8__locals1 = new DateTimeDescriptor.<>c__DisplayClass71_0();
			CS$<>8__locals1.culture = culture;
			CS$<>8__locals1.literalReplacement = new Dictionary<string, string>();
			int num = 1;
			foreach (Match match in DateTimeDescriptor._maskLiteralRegex.NonCachingMatches(mask))
			{
				string text = string.Format("#{0}", num++);
				string text2 = match.Value.Replace("'", string.Empty);
				CS$<>8__locals1.literalReplacement.Add(text, text2);
				mask = mask.Replace(match.Value, text);
			}
			string text3 = mask.Replace("dddd", "$1").Replace("ddd", "$2").Replace("MMMM", "$3")
				.Replace("MMM", "$4");
			text3 = text3.Replace("dd", "(?:0[1-9]|[1-2][0-9]|3[0-1])").Replace("%d", "(?<!\\p{N})(?:[1-2][0-9]|3[0-1]|[0-9])(?!\\p{N})").Replace("d", "(?:[1-2][0-9]|3[0-1]|[0-9])")
				.Replace("MM", "(?:0[1-9]|1[0-2])")
				.Replace("%M", "(?<!\\p{N})(?:1[0-2]|[1-9])(?!\\p{N})")
				.Replace("M", "(?:1[0-2]|[1-9])")
				.Replace("yyyy", "(?:[0-9]{4})")
				.Replace("yy", "(?:[0-9]{2})(?![0-9])")
				.Replace("hh", "(?:0[1-9]|1[0-2])")
				.Replace("h", "(?:1[0-2]|[1-9])")
				.Replace("HH", "(?:[0-1][0-9]|2[0-3])")
				.Replace("H", "(?:1[0-9]|2[0-3]|[0-9])")
				.Replace("mm", "(?:[0-5][0-9])")
				.Replace("m", "(?:[1-5][0-9]|[0-9])")
				.Replace("ss", "(?:[0-5][0-9])")
				.Replace("tt", CS$<>8__locals1.<LoadPattern>g__AmPm|1());
			text3 = text3.Replace("$1", CS$<>8__locals1.<LoadPattern>g__Days|2()).Replace("$2", CS$<>8__locals1.<LoadPattern>g__AbbreviatedDays|3()).Replace("$3", CS$<>8__locals1.<LoadPattern>g__Months|4())
				.Replace("$4", CS$<>8__locals1.<LoadPattern>g__AbbreviatedMonths|5());
			if (mask.EndsWith("yy"))
			{
				text3 += "(?!\\p{Zs}[0-9])";
			}
			if (mask.EndsWith("h:mm") || mask.EndsWith("H:mm"))
			{
				text3 = text3 + "(?!\\p{Zs}" + CS$<>8__locals1.<LoadPattern>g__AmPm|1() + ")";
			}
			return CS$<>8__locals1.literalReplacement.Keys.Aggregate(text3, (string current, string key) => current.Replace(key, Regex.Escape(CS$<>8__locals1.literalReplacement[key])));
		}

		// Token: 0x0600C288 RID: 49800 RVA: 0x0029F6C8 File Offset: 0x0029D8C8
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeDescriptor()
		{
			Dictionary<string, char[]> dictionary = new Dictionary<string, char[]>();
			dictionary["02"] = new char[] { '0', '1', '2' };
			dictionary["03"] = new char[] { '0', '1', '2', '3' };
			dictionary["05"] = new char[] { '0', '1', '2', '3', '4', '5' };
			dictionary["9"] = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
			dictionary["10"] = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
			DateTimeDescriptor._digitLookup = dictionary;
			DateTimeDescriptor._maskLiteralRegex = "'(.)*?'".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
		}

		// Token: 0x04004B69 RID: 19305
		private static readonly Dictionary<string, char[]> _digitLookup;

		// Token: 0x04004B6A RID: 19306
		private HashSet<char> _formattedLowerFirstChars;

		// Token: 0x04004B6B RID: 19307
		private HashSet<char> _formattedLowerLastChars;

		// Token: 0x04004B6C RID: 19308
		private int? _formattedMaxLength;

		// Token: 0x04004B6D RID: 19309
		private int? _formattedMinLength;

		// Token: 0x04004B6E RID: 19310
		private readonly HashSet<char> _maskChars = new char[] { 'y', 'M', 'd', 'H', 'h', 'm', 's', 'f', 't', '%' }.ConvertToHashSet<char>();

		// Token: 0x04004B6F RID: 19311
		private HashSet<char> _maskDelimiterHashSet;

		// Token: 0x04004B70 RID: 19312
		private string _maskDelimiters;

		// Token: 0x04004B71 RID: 19313
		private static readonly Regex _maskLiteralRegex;

		// Token: 0x04004B72 RID: 19314
		private bool? _outputAllNumbers;

		// Token: 0x04004B73 RID: 19315
		private string _pattern;

		// Token: 0x04004B74 RID: 19316
		private Regex _regex;

		// Token: 0x04004B75 RID: 19317
		private string _toEqualString;

		// Token: 0x04004B76 RID: 19318
		private string _toString;

		// Token: 0x020016C8 RID: 5832
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04004B7D RID: 19325
			public static Func<char, char> <0>__ToLower;
		}
	}
}
