using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000883 RID: 2179
	public class StringDateTimeFormatPart : DateTimeFormatPart
	{
		// Token: 0x06002FA0 RID: 12192 RVA: 0x0008C1D0 File Offset: 0x0008A3D0
		private StringDateTimeFormatPart(string formatString, DateTimePart matchedPart, IImmutableDictionary<int, string> stringLookup, StringDateTimeFormatPart abbreviationOf, string posixOutputFormatString = null, string posixParsingFormatString = null, FormatAttributes attributes = null)
			: base(formatString, matchedPart.Some<DateTimePart>(), StringDateTimeFormatPart.LetterTokenNames.Select((string name) => Microsoft.ProgramSynthesis.DslLibrary.Token.GetStaticTokenByName(name).OrElseDefault<Token>()).MaybeFirst((Token token) => stringLookup.Values.All(new Func<string, bool>(token.MatchesEntireString))), stringLookup.Values.Min((string str) => str.Length), stringLookup.Values.Max((string str) => str.Length), posixOutputFormatString, posixParsingFormatString, attributes)
		{
			StringDateTimeFormatPart <>4__this = this;
			this.StringLookup = stringLookup;
			this.AbbreviationOf = abbreviationOf;
			this._prefixLength = base.MinimumLength;
			Func<KeyValuePair<int, string>, string> <>9__7;
			this._parsePrefixLookup = (from kv in stringLookup
				group kv by kv.Value.Substring(0, <>4__this._prefixLength)).ToImmutableDictionary((IGrouping<string, KeyValuePair<int, string>> g) => g.Key, delegate(IGrouping<string, KeyValuePair<int, string>> g)
			{
				Func<KeyValuePair<int, string>, string> func;
				if ((func = <>9__7) == null)
				{
					func = (<>9__7 = (KeyValuePair<int, string> kv) => kv.Value.Substring(<>4__this._prefixLength));
				}
				return g.ToImmutableDictionary(func, (KeyValuePair<int, string> kv) => kv.Key);
			});
			if (attributes != null && attributes.HasUnhandledAttributes)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported attributes for format string '{0}': {1}", new object[] { formatString, attributes })), "attributes");
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x0008C33F File Offset: 0x0008A53F
		public IImmutableDictionary<int, string> StringLookup { get; }

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x0008C347 File Offset: 0x0008A547
		public IEnumerable<string> AllValues
		{
			get
			{
				return this.StringLookup.Values;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x0008C354 File Offset: 0x0008A554
		public StringDateTimeFormatPart AbbreviationOf { get; }

		// Token: 0x06002FA4 RID: 12196 RVA: 0x0008C35C File Offset: 0x0008A55C
		public override string FormatStringFor(DateTimeFormat.Target target, bool strict = true)
		{
			string baseFormatString = base.BaseFormatString;
			if (baseFormatString != null)
			{
				switch (baseFormatString.Length)
				{
				case 1:
				{
					char c = baseFormatString[0];
					if (c != 'o')
					{
						if (c == 't')
						{
							if (base.Attributes == null)
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%p");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = (strict ? null : "%p");
									break;
								case DateTimeFormat.Target.MomentJS:
									text = (strict ? null : "A");
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = (strict ? null : "A");
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = null;
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							if (base.Attributes.Equals(DateTimeFormatPart.LowerCaseFormatAttributes))
							{
								string text;
								switch (target)
								{
								case DateTimeFormat.Target.PosixFormatting:
									text = (strict ? null : "%p");
									break;
								case DateTimeFormat.Target.PosixParsing:
									text = (strict ? null : "%p");
									break;
								case DateTimeFormat.Target.MomentJS:
									text = (strict ? null : "a");
									break;
								case DateTimeFormat.Target.DayJSFormatting:
									text = (strict ? null : "a");
									break;
								case DateTimeFormat.Target.PowerAppsFormatting:
									text = "a/p";
									break;
								default:
									throw new NotImplementedException("unsupported target: " + target.ToString());
								}
								return text;
							}
							throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
						}
					}
					else
					{
						if (base.Attributes == null)
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = null;
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = null;
								break;
							case DateTimeFormat.Target.MomentJS:
								text = "Do";
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = null;
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = null;
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
					}
					break;
				}
				case 2:
					if (baseFormatString == "tt")
					{
						if (base.Attributes == null)
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = (strict ? null : "%p");
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = (strict ? null : "%p");
								break;
							case DateTimeFormat.Target.MomentJS:
								text = "A";
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = "A";
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = "AM/PM";
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						if (base.Attributes.Equals(DateTimeFormatPart.LowerCaseFormatAttributes))
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = (strict ? null : "%p");
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = (strict ? null : "%p");
								break;
							case DateTimeFormat.Target.MomentJS:
								text = "a";
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = "a";
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = null;
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
					}
					break;
				case 3:
				{
					char c = baseFormatString[0];
					if (c != 'M')
					{
						if (c == 'd')
						{
							if (baseFormatString == "ddd")
							{
								if (base.Attributes == null)
								{
									string text;
									switch (target)
									{
									case DateTimeFormat.Target.PosixFormatting:
										text = "%a";
										break;
									case DateTimeFormat.Target.PosixParsing:
										text = (strict ? null : "%a");
										break;
									case DateTimeFormat.Target.MomentJS:
										text = "ddd";
										break;
									case DateTimeFormat.Target.DayJSFormatting:
										text = "ddd";
										break;
									case DateTimeFormat.Target.PowerAppsFormatting:
										text = "ddd";
										break;
									default:
										throw new NotImplementedException("unsupported target: " + target.ToString());
									}
									return text;
								}
								throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
							}
						}
					}
					else if (baseFormatString == "MMM")
					{
						if (base.Attributes == null)
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = "%b";
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = (strict ? null : "%b");
								break;
							case DateTimeFormat.Target.MomentJS:
								text = "MMM";
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = "MMM";
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = "mmm";
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
					}
					break;
				}
				case 4:
				{
					char c = baseFormatString[0];
					if (c != 'M')
					{
						if (c == 'd')
						{
							if (baseFormatString == "dddd")
							{
								if (base.Attributes == null)
								{
									string text;
									switch (target)
									{
									case DateTimeFormat.Target.PosixFormatting:
										text = "%A";
										break;
									case DateTimeFormat.Target.PosixParsing:
										text = (strict ? null : "%A");
										break;
									case DateTimeFormat.Target.MomentJS:
										text = "dddd";
										break;
									case DateTimeFormat.Target.DayJSFormatting:
										text = "dddd";
										break;
									case DateTimeFormat.Target.PowerAppsFormatting:
										text = "dddd";
										break;
									default:
										throw new NotImplementedException("unsupported target: " + target.ToString());
									}
									return text;
								}
								throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
							}
						}
					}
					else if (baseFormatString == "MMMM")
					{
						if (base.Attributes == null)
						{
							string text;
							switch (target)
							{
							case DateTimeFormat.Target.PosixFormatting:
								text = "%B";
								break;
							case DateTimeFormat.Target.PosixParsing:
								text = (strict ? null : "%B");
								break;
							case DateTimeFormat.Target.MomentJS:
								text = "MMMM";
								break;
							case DateTimeFormat.Target.DayJSFormatting:
								text = "MMMM";
								break;
							case DateTimeFormat.Target.PowerAppsFormatting:
								text = "mmmm";
								break;
							default:
								throw new NotImplementedException("unsupported target: " + target.ToString());
							}
							return text;
						}
						throw new NotImplementedException("unsupported format: " + ((this != null) ? this.ToString() : null));
					}
					break;
				}
				}
			}
			throw new NotImplementedException("unsupported format part: " + base.BaseFormatString);
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x0008C94C File Offset: 0x0008AB4C
		protected internal override Optional<Record<StringRegion, int>> ParseNext(StringRegion sr)
		{
			if ((ulong)sr.Length < (ulong)((long)base.MinimumLength))
			{
				return Optional<Record<StringRegion, int>>.Nothing;
			}
			StringRegion stringRegion = sr.Slice(sr.Start, (uint)((ulong)sr.Start + (ulong)((long)this._prefixLength)));
			string value = stringRegion.Value;
			ImmutableDictionary<string, int> immutableDictionary;
			if (!this._parsePrefixLookup.TryGetValue(value, out immutableDictionary))
			{
				return Optional<Record<StringRegion, int>>.Nothing;
			}
			int num;
			if (immutableDictionary.TryGetValue(string.Empty, out num))
			{
				return Record.Create<StringRegion, int>(stringRegion, num).Some<Record<StringRegion, int>>();
			}
			string srAfterPrefix = sr.Slice((uint)((ulong)sr.Start + (ulong)((long)this._prefixLength)), sr.End).Value;
			return immutableDictionary.Where(delegate(KeyValuePair<string, int> kv)
			{
				string srAfterPrefix2 = srAfterPrefix;
				KeyValuePair<string, int> keyValuePair = kv;
				return srAfterPrefix2.StartsWith(keyValuePair.Key, StringComparison.Ordinal);
			}).Select(delegate(KeyValuePair<string, int> kv)
			{
				StringRegion sr2 = sr;
				uint start = sr.Start;
				long num2 = (long)((ulong)sr.Start + (ulong)((long)this._prefixLength));
				KeyValuePair<string, int> keyValuePair2 = kv;
				StringRegion stringRegion2 = sr2.Slice(start, (uint)(num2 + (long)keyValuePair2.Key.Length));
				keyValuePair2 = kv;
				return Record.Create<StringRegion, int>(stringRegion2, keyValuePair2.Value);
			}).MaybeFirst<Record<StringRegion, int>>();
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x0008CA4C File Offset: 0x0008AC4C
		protected override string ToString(int value)
		{
			return this.StringLookup.GetValueOrDefault(value);
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x0008CA5C File Offset: 0x0008AC5C
		public override XElement RenderXML()
		{
			XElement xelement = base.RenderXML("StringFormatPart");
			if (base.PosixOutputFormatString != null)
			{
				xelement.Add(new XAttribute("PosixOutputFormatString", base.PosixOutputFormatString));
			}
			if (base.PosixParsingFormatString != null)
			{
				xelement.Add(new XAttribute("PosixParsingFormatString", base.PosixParsingFormatString));
			}
			if (this.AbbreviationOf != null)
			{
				xelement.Add(new XElement("AbbreviationOf", this.AbbreviationOf.RenderXML()));
			}
			foreach (KeyValuePair<int, string> keyValuePair in this.StringLookup.OrderBy((KeyValuePair<int, string> kv) => kv.Key))
			{
				xelement.Add(new XElement("Lookup", new object[]
				{
					new XAttribute("string", keyValuePair.Value),
					new XAttribute("value", keyValuePair.Key)
				}));
			}
			return xelement;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x0008CB94 File Offset: 0x0008AD94
		public new static StringDateTimeFormatPart TryParseFromXML(XElement literal)
		{
			if (((literal != null) ? literal.Name : null) != "StringFormatPart")
			{
				return null;
			}
			Optional<FormatAttributes> optional = DateTimeFormatPart.ParseAttributesFromXML(literal);
			XAttribute xattribute = literal.Attribute("BaseFormatString");
			if (!optional.HasValue || xattribute == null)
			{
				return null;
			}
			XAttribute xattribute2 = literal.Attribute("DateTimePart");
			DateTimePart dateTimePart;
			if (xattribute2 == null || !Enum.TryParse<DateTimePart>(xattribute2.Value, out dateTimePart))
			{
				return null;
			}
			XAttribute xattribute3 = literal.Attribute("PosixOutputFormatString");
			XAttribute xattribute4 = literal.Attribute("PosixParsingFormatString");
			ImmutableDictionary<int, string>.Builder builder = ImmutableDictionary<int, string>.Empty.ToBuilder();
			foreach (XElement xelement in literal.Elements("Lookup"))
			{
				XAttribute xattribute5 = xelement.Attribute("string");
				if (xattribute5 == null)
				{
					return null;
				}
				XAttribute xattribute6 = xelement.Attribute("value");
				if (xattribute6 == null)
				{
					return null;
				}
				int num;
				if (!int.TryParse(xattribute6.Value, out num))
				{
					return null;
				}
				builder.Add(num, xattribute5.Value);
			}
			XElement xelement2 = literal.Element("AbbreviationOf");
			StringDateTimeFormatPart stringDateTimeFormatPart;
			if (xelement2 == null)
			{
				stringDateTimeFormatPart = null;
			}
			else
			{
				stringDateTimeFormatPart = StringDateTimeFormatPart.TryParseFromXML(xelement2.Elements().OnlyOrDefault<XElement>());
				if (stringDateTimeFormatPart == null)
				{
					return null;
				}
			}
			return new StringDateTimeFormatPart(xattribute.Value, dateTimePart, builder.ToImmutable(), stringDateTimeFormatPart, (xattribute3 != null) ? xattribute3.Value : null, (xattribute4 != null) ? xattribute4.Value : null, optional.Value);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x0008CD5C File Offset: 0x0008AF5C
		private static Func<DateTime, string> BuildStringGenerationFunction(string formatString, FormatAttributes attributes)
		{
			if (formatString.Length == 1)
			{
				formatString = "%" + formatString;
			}
			if (attributes == null || !attributes.Attributes.ContainsKey("casing"))
			{
				return (DateTime dt) => dt.ToString(formatString, CultureInfo.InvariantCulture);
			}
			attributes.MarkAttributeAsHandled("casing");
			if (attributes.Attributes["casing"] == "lower")
			{
				return (DateTime dt) => dt.ToString(formatString, CultureInfo.InvariantCulture).ToLowerInvariant();
			}
			throw new ArgumentException("Unsupported casing option in attributes: " + ((attributes != null) ? attributes.ToString() : null), "attributes");
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x0008CE14 File Offset: 0x0008B014
		private static ImmutableDictionary<int, string> CreateDayOfWeekStrings(string formatString, FormatAttributes attributes)
		{
			Func<DateTime, string> strGenFunc = StringDateTimeFormatPart.BuildStringGenerationFunction(formatString, attributes);
			return (from day in Enumerable.Range(1, 7)
				let dt = new DateTime(2000, 1, day)
				select new
				{
					day = dt.DayOfWeek,
					str = strGenFunc(dt)
				}).ToImmutableDictionary(o => (int)o.day, o => o.str);
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x0008CEB4 File Offset: 0x0008B0B4
		internal static StringDateTimeFormatPart CreateDayOfWeek(string formatString, string posixFormat = null, FormatAttributes attributes = null)
		{
			ImmutableDictionary<int, string> immutableDictionary = StringDateTimeFormatPart.CreateDayOfWeekStrings(formatString, attributes);
			return new StringDateTimeFormatPart(formatString, DateTimePart.DayOfWeek, immutableDictionary, (formatString == "ddd") ? ((StringDateTimeFormatPart)DateTimeFormatPart.Create("dddd", attributes)) : null, posixFormat, posixFormat, attributes);
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x0008CEF8 File Offset: 0x0008B0F8
		private static ImmutableDictionary<int, string> CreateMonthStrings(string formatString, FormatAttributes attributes)
		{
			Func<DateTime, string> strGenFunc = StringDateTimeFormatPart.BuildStringGenerationFunction(formatString, attributes);
			return (from month in Enumerable.Range(1, 12)
				let dt = new DateTime(2000, month, 1)
				select new
				{
					month = month,
					str = strGenFunc(dt)
				}).ToImmutableDictionary(o => o.month, o => o.str);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x0008CF98 File Offset: 0x0008B198
		internal static StringDateTimeFormatPart CreateMonth(string formatString, string posixFormat = null, FormatAttributes attributes = null)
		{
			ImmutableDictionary<int, string> immutableDictionary = StringDateTimeFormatPart.CreateMonthStrings(formatString, attributes);
			return new StringDateTimeFormatPart(formatString, DateTimePart.Month, immutableDictionary, (formatString == "MMM") ? ((StringDateTimeFormatPart)DateTimeFormatPart.Create("MMMM", attributes)) : null, posixFormat, posixFormat, attributes);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x0008CFD8 File Offset: 0x0008B1D8
		private static ImmutableDictionary<int, string> CreatePeriodStrings(string formatString, FormatAttributes attributes)
		{
			Func<DateTime, string> strGenFunc = StringDateTimeFormatPart.BuildStringGenerationFunction(formatString, attributes);
			return (from period in Enumerable.Range(0, 2)
				let dt = new DateTime(2000, 1, 1, 12 * period, 1, 1)
				select new
				{
					period = period,
					str = strGenFunc(dt)
				}).ToImmutableDictionary(o => o.period, o => o.str);
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x0008D078 File Offset: 0x0008B278
		internal static StringDateTimeFormatPart CreatePeriod(string formatString, FormatAttributes attributes)
		{
			ImmutableDictionary<int, string> immutableDictionary = StringDateTimeFormatPart.CreatePeriodStrings(formatString, attributes);
			return new StringDateTimeFormatPart(formatString, DateTimePart.Period, immutableDictionary, (formatString == "t") ? ((StringDateTimeFormatPart)DateTimeFormatPart.Create("tt", attributes)) : null, null, null, attributes);
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x0008D0B8 File Offset: 0x0008B2B8
		internal static StringDateTimeFormatPart CreateOrdinalDay(string formatString, FormatAttributes attributes)
		{
			if (formatString != "o")
			{
				throw new ArgumentException("\"o\" is the only format string supported for ordinal day.", "formatString");
			}
			if (attributes != null && attributes.HasUnhandledAttributes)
			{
				throw new NotImplementedException("Attributes are unsupported for ordinal day: " + ((attributes != null) ? attributes.ToString() : null));
			}
			ImmutableDictionary<int, string> immutableDictionary = Enumerable.Range(1, 31).ToImmutableDictionary((int n) => n, delegate(int n)
			{
				string text = n.ToString();
				string text2;
				switch (n % 100)
				{
				case 11:
					text2 = "th";
					break;
				case 12:
					text2 = "th";
					break;
				case 13:
					text2 = "th";
					break;
				default:
				{
					string text3;
					switch (n % 10)
					{
					case 1:
						text3 = "st";
						break;
					case 2:
						text3 = "nd";
						break;
					case 3:
						text3 = "rd";
						break;
					default:
						text3 = "th";
						break;
					}
					text2 = text3;
					break;
				}
				}
				return text + text2;
			});
			return new StringDateTimeFormatPart(formatString, DateTimePart.Day, immutableDictionary, null, null, null, attributes);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x0008D164 File Offset: 0x0008B364
		// Note: this type is marked as 'beforefieldinit'.
		static StringDateTimeFormatPart()
		{
			FormatAttributes[] array = new FormatAttributes[2];
			int num = 1;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["casing"] = "lower";
			array[num] = new FormatAttributes(dictionary);
			StringDateTimeFormatPart.DateValueStrings = (from attr in array
				from strings in new ImmutableDictionary<int, string>[]
				{
					StringDateTimeFormatPart.CreateMonthStrings("MMM", attr),
					StringDateTimeFormatPart.CreateDayOfWeekStrings("ddd", attr),
					StringDateTimeFormatPart.CreatePeriodStrings("tt", attr)
				}
				from value in strings.Values
				select value).ToList<string>();
		}

		// Token: 0x04001748 RID: 5960
		private static readonly string[] LetterTokenNames = new string[] { "ALL CAPS", "Lowercase word", "Camel Case", "Alphanum" };

		// Token: 0x04001749 RID: 5961
		private readonly int _prefixLength;

		// Token: 0x0400174A RID: 5962
		private readonly ImmutableDictionary<string, ImmutableDictionary<string, int>> _parsePrefixLookup;

		// Token: 0x0400174D RID: 5965
		internal const string XMLName = "StringFormatPart";

		// Token: 0x0400174E RID: 5966
		private const string LookupXMLName = "Lookup";

		// Token: 0x0400174F RID: 5967
		private const string LookupStringXMLName = "string";

		// Token: 0x04001750 RID: 5968
		private const string LookupValueXMLName = "value";

		// Token: 0x04001751 RID: 5969
		public static readonly IReadOnlyCollection<string> DateValueStrings;
	}
}
