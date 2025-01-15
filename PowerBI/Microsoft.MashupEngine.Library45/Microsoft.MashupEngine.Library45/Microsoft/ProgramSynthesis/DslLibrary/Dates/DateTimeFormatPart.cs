using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000832 RID: 2098
	[Parseable("TryParseFromXML")]
	public abstract class DateTimeFormatPart : IRenderableLiteral
	{
		// Token: 0x06002D50 RID: 11600 RVA: 0x00080734 File Offset: 0x0007E934
		protected DateTimeFormatPart(string formatString, Optional<DateTimePart> matchedPart, Optional<Token> token, int minimumLength, int maximumLength, string posixOutputFormatString = null, string posixParsingFormatString = null, FormatAttributes attributes = null)
		{
			this.MinimumLength = minimumLength;
			this.MaximumLength = maximumLength;
			this.PosixOutputFormatString = posixOutputFormatString;
			this.PosixParsingFormatString = posixParsingFormatString;
			this.Attributes = attributes;
			this.BaseFormatString = formatString;
			this.FullFormatString = ((this.Attributes == null) ? this.BaseFormatString : (this.BaseFormatString + this.Attributes.FormatSuffix));
			this.MatchedPart = matchedPart;
			this.Token = token;
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002D51 RID: 11601 RVA: 0x000807B0 File Offset: 0x0007E9B0
		public int MinimumLength { get; }

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000807B8 File Offset: 0x0007E9B8
		public int MaximumLength { get; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x000807C0 File Offset: 0x0007E9C0
		public string PosixOutputFormatString { get; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002D54 RID: 11604 RVA: 0x000807C8 File Offset: 0x0007E9C8
		public string PosixParsingFormatString { get; }

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002D55 RID: 11605 RVA: 0x000807D0 File Offset: 0x0007E9D0
		public FormatAttributes Attributes { get; }

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x000807D8 File Offset: 0x0007E9D8
		public string BaseFormatString { get; }

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x000807E0 File Offset: 0x0007E9E0
		public string FullFormatString { get; }

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x000807E8 File Offset: 0x0007E9E8
		public Optional<Token> Token { get; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x000807F0 File Offset: 0x0007E9F0
		public Optional<DateTimePart> MatchedPart { get; }

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public virtual bool IsNumeric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public virtual bool IsNumericAtEnd
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x0000A5FD File Offset: 0x000087FD
		public virtual bool UniqueParse
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D5D RID: 11613
		public abstract string FormatStringFor(DateTimeFormat.Target target, bool strict = true);

		// Token: 0x06002D5E RID: 11614 RVA: 0x000807F8 File Offset: 0x0007E9F8
		internal IEnumerable<Record<StringRegion, int>> ParseAll(StringRegion sr)
		{
			return Enumerable.Range(0, (int)((ulong)(sr.End - sr.Start) - (ulong)((long)this.MinimumLength))).SelectMany((int offset) => this.ParseAllNext(sr.Slice((uint)((ulong)sr.Start + (ulong)((long)offset)), sr.End)));
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x00080854 File Offset: 0x0007EA54
		internal virtual Optional<int> ParseFullString(string str)
		{
			if (str.Length > this.MaximumLength || str.Length < this.MinimumLength)
			{
				return Optional<int>.Nothing;
			}
			Optional<Record<StringRegion, int>> optional = this.ParseNext(new StringRegion(str, Microsoft.ProgramSynthesis.DslLibrary.Token.DateTimeTokens));
			if (!optional.HasValue || (ulong)optional.Value.Item1.Length != (ulong)((long)str.Length))
			{
				return Optional<int>.Nothing;
			}
			return optional.Value.Item2.Some<int>();
		}

		// Token: 0x06002D60 RID: 11616
		protected internal abstract Optional<Record<StringRegion, int>> ParseNext(StringRegion sr);

		// Token: 0x06002D61 RID: 11617 RVA: 0x000808D0 File Offset: 0x0007EAD0
		internal virtual IEnumerable<Record<StringRegion, int>> ParseAllNext(StringRegion sr)
		{
			return this.ParseNext(sr).AsEnumerable<Record<StringRegion, int>>();
		}

		// Token: 0x06002D62 RID: 11618
		protected abstract string ToString(int value);

		// Token: 0x06002D63 RID: 11619 RVA: 0x000808E0 File Offset: 0x0007EAE0
		public string ToString(PartialDateTime dt)
		{
			if (!this.MatchedPart.HasValue)
			{
				return this.ToString(0);
			}
			if (dt == null || !dt.Contains(this.MatchedPart.Value))
			{
				return null;
			}
			return dt.Get(this.MatchedPart.Value).Select(new Func<int, string>(this.ToString)).OrElseDefault<string>();
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x00080954 File Offset: 0x0007EB54
		public static DateTimeFormatPart Create(string format, FormatAttributes attributes)
		{
			if (DateTimeFormatPart.Cache == null)
			{
				return DateTimeFormatPart._Create(format, attributes);
			}
			return DateTimeFormatPart.Cache.MaybeGet(Record.Create<string, FormatAttributes>(format, attributes)).OrElseCompute(delegate
			{
				if (attributes != null)
				{
					throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Format={0} with attributes={1} is unsupported.", new object[] { format, attributes })));
				}
				return ConstantDateTimeFormatPart.FromConstantFormat(format);
			});
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000809BC File Offset: 0x0007EBBC
		private static DateTimeFormatPart _Create(string format, FormatAttributes attributes)
		{
			if (format != null)
			{
				switch (format.Length)
				{
				case 1:
				{
					char c = format[0];
					if (c <= 'V')
					{
						if (c == 'H')
						{
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Hour, format.Length, 2, 0, (attributes != null && attributes.Attributes.ContainsKey("disallow24Hour")) ? 23 : 24, null, null, null, DateTimeFormatPart.AllowLeadingZerosAndDisallow24HourFormatAttributes.Equals(attributes) ? "%H" : null, attributes);
						}
						if (c == 'M')
						{
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Month, format.Length, 2, 1, 12, null, null, null, DateTimeFormatPart.AllowLeadingZerosFormatAttributes.Equals(attributes) ? "%m" : null, attributes);
						}
						if (c != 'V')
						{
							goto IL_0AFC;
						}
						return new NumericDateTimeFormatPart(format[0], DateTimePart.WeekOfYear, format.Length, 2, 1, 53, null, null, null, null, attributes);
					}
					else
					{
						if (c == 'Z')
						{
							return TimeZoneOffsetFormatPart.CreateZ(attributes);
						}
						switch (c)
						{
						case 'd':
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Day, format.Length, 2, 1, 31, null, null, null, DateTimeFormatPart.AllowLeadingZerosFormatAttributes.Equals(attributes) ? "%d" : null, attributes);
						case 'e':
						case 'g':
						case 'k':
						case 'l':
						case 'n':
						case 'p':
						case 'r':
							goto IL_0AFC;
						case 'f':
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Millisecond, 1, 1, 0, 9, (int f) => f * 100, (int f) => f / 100, null, null, attributes);
						case 'h':
							return new NumericDateTimeFormatPart(format[0], DateTimePart.HourInPeriod, format.Length, 2, 1, 12, null, null, null, DateTimeFormatPart.AllowLeadingZerosFormatAttributes.Equals(attributes) ? "%I" : null, attributes);
						case 'i':
							return new NumericDateTimeFormatPart(format[0], DateTimePart.DayOfWeekInMonth, 1, 1, 1, 5, null, null, null, null, attributes);
						case 'j':
							return new NumericDateTimeFormatPart(format[0], DateTimePart.DayOfYear, 1, 3, 1, 366, null, null, null, DateTimeFormatPart.AllowLeadingZerosFormatAttributes.Equals(attributes) ? "%j" : null, attributes);
						case 'm':
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Minute, format.Length, 2, 0, 59, null, null, null, DateTimeFormatPart.AllowLeadingZerosFormatAttributes.Equals(attributes) ? "%M" : null, attributes);
						case 'o':
							return StringDateTimeFormatPart.CreateOrdinalDay(format, attributes);
						case 'q':
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Quarter, 1, 1, 1, 4, null, null, null, null, attributes);
						case 's':
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Second, format.Length, 2, 0, 61, null, null, null, DateTimeFormatPart.AllowLeadingZerosFormatAttributes.Equals(attributes) ? "%S" : null, attributes);
						case 't':
							break;
						default:
							if (c != 'y')
							{
								goto IL_0AFC;
							}
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Year, format.Length, 2, 0, 99, (int y) => CultureInfo.InvariantCulture.Calendar.ToFourDigitYear(y), (int y) => y % 100, null, null, attributes);
						}
					}
					break;
				}
				case 2:
				{
					char c = format[0];
					if (c <= 'h')
					{
						if (c <= 'M')
						{
							if (c != 'H')
							{
								if (c != 'M')
								{
									goto IL_0AFC;
								}
								if (!(format == "MM"))
								{
									goto IL_0AFC;
								}
								return new NumericDateTimeFormatPart(format[0], DateTimePart.Month, format.Length, 2, 1, 12, null, null, "%m", null, attributes);
							}
							else
							{
								if (!(format == "HH"))
								{
									goto IL_0AFC;
								}
								return new NumericDateTimeFormatPart(format[0], DateTimePart.Hour, format.Length, 2, 0, (attributes != null && attributes.Attributes.ContainsKey("disallow24Hour")) ? 23 : 24, null, null, "%H", null, attributes);
							}
						}
						else
						{
							switch (c)
							{
							case 'V':
								if (!(format == "VV"))
								{
									goto IL_0AFC;
								}
								return new NumericDateTimeFormatPart(format[0], DateTimePart.WeekOfYear, format.Length, 2, 1, 53, null, null, null, null, attributes);
							case 'W':
							case 'X':
								goto IL_0AFC;
							case 'Y':
								if (!(format == "YY"))
								{
									goto IL_0AFC;
								}
								return new NumericDateTimeFormatPart(format[0], DateTimePart.WeekYear, format.Length, 2, 0, 99, (int y) => CultureInfo.InvariantCulture.Calendar.ToFourDigitYear(y), (int y) => y % 100, null, null, attributes);
							case 'Z':
								if (!(format == "ZZ"))
								{
									goto IL_0AFC;
								}
								return TimeZoneOffsetFormatPart.CreateZZ(attributes);
							default:
								switch (c)
								{
								case 'd':
									if (!(format == "dd"))
									{
										goto IL_0AFC;
									}
									return new NumericDateTimeFormatPart(format[0], DateTimePart.Day, format.Length, 2, 1, 31, null, null, "%d", null, attributes);
								case 'e':
								case 'g':
									goto IL_0AFC;
								case 'f':
									if (!(format == "ff"))
									{
										goto IL_0AFC;
									}
									return new NumericDateTimeFormatPart(format[0], DateTimePart.Millisecond, 2, 2, 0, 99, (int f) => f * 10, (int f) => f / 10, null, null, attributes);
								case 'h':
									if (!(format == "hh"))
									{
										goto IL_0AFC;
									}
									return new NumericDateTimeFormatPart(format[0], DateTimePart.HourInPeriod, format.Length, 2, 1, 12, null, null, "%I", null, attributes);
								default:
									goto IL_0AFC;
								}
								break;
							}
						}
					}
					else if (c <= 's')
					{
						if (c != 'm')
						{
							if (c != 's')
							{
								goto IL_0AFC;
							}
							if (!(format == "ss"))
							{
								goto IL_0AFC;
							}
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Second, format.Length, 2, 0, 61, null, null, "%S", null, attributes);
						}
						else
						{
							if (!(format == "mm"))
							{
								goto IL_0AFC;
							}
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Minute, format.Length, 2, 0, 59, null, null, "%M", null, attributes);
						}
					}
					else if (c != 't')
					{
						if (c != 'y')
						{
							goto IL_0AFC;
						}
						if (!(format == "yy"))
						{
							goto IL_0AFC;
						}
						return new NumericDateTimeFormatPart(format[0], DateTimePart.Year, format.Length, 2, 0, 99, (int y) => CultureInfo.InvariantCulture.Calendar.ToFourDigitYear(y), (int y) => y % 100, "%y", null, attributes);
					}
					else if (!(format == "tt"))
					{
						goto IL_0AFC;
					}
					break;
				}
				case 3:
				{
					char c = format[0];
					if (c <= 'd')
					{
						if (c != 'M')
						{
							if (c != 'd')
							{
								goto IL_0AFC;
							}
							if (!(format == "ddd"))
							{
								goto IL_0AFC;
							}
							return StringDateTimeFormatPart.CreateDayOfWeek(format, "%a", attributes);
						}
						else
						{
							if (!(format == "MMM"))
							{
								goto IL_0AFC;
							}
							return StringDateTimeFormatPart.CreateMonth(format, "%b", attributes);
						}
					}
					else if (c != 'f')
					{
						if (c != 'j')
						{
							if (c != 'y')
							{
								goto IL_0AFC;
							}
							if (!(format == "yyy"))
							{
								goto IL_0AFC;
							}
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Year, format.Length, 5, 0, 99999, null, null, null, null, attributes);
						}
						else
						{
							if (!(format == "jjj"))
							{
								goto IL_0AFC;
							}
							return new NumericDateTimeFormatPart(format[0], DateTimePart.DayOfYear, 3, 3, 1, 366, null, null, "%j", null, attributes);
						}
					}
					else
					{
						if (!(format == "fff"))
						{
							goto IL_0AFC;
						}
						return new NumericDateTimeFormatPart(format[0], DateTimePart.Millisecond, 3, 3, 0, 999, null, null, null, null, attributes);
					}
					break;
				}
				case 4:
				{
					char c = format[0];
					if (c <= 'Y')
					{
						if (c != 'M')
						{
							if (c != 'Y')
							{
								goto IL_0AFC;
							}
							if (!(format == "YYYY"))
							{
								goto IL_0AFC;
							}
							return new NumericDateTimeFormatPart(format[0], DateTimePart.WeekYear, format.Length, 4, 0, 9999, null, null, null, null, attributes);
						}
						else
						{
							if (!(format == "MMMM"))
							{
								goto IL_0AFC;
							}
							return StringDateTimeFormatPart.CreateMonth(format, "%B", attributes);
						}
					}
					else if (c != 'd')
					{
						if (c != 'f')
						{
							if (c != 'y')
							{
								goto IL_0AFC;
							}
							if (!(format == "yyyy"))
							{
								goto IL_0AFC;
							}
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Year, format.Length, 4, 0, 9999, null, null, "%Y", "%Y", attributes);
						}
						else
						{
							if (!(format == "ffff"))
							{
								goto IL_0AFC;
							}
							return new NumericDateTimeFormatPart(format[0], DateTimePart.Millisecond, 4, 4, 0, 9999, (int f) => f / 10, (int f) => f * 10, null, null, attributes);
						}
					}
					else
					{
						if (!(format == "dddd"))
						{
							goto IL_0AFC;
						}
						return StringDateTimeFormatPart.CreateDayOfWeek(format, "%A", attributes);
					}
					break;
				}
				case 5:
				{
					char c = format[0];
					if (c != 'f')
					{
						if (c != 'y')
						{
							goto IL_0AFC;
						}
						if (!(format == "yyyyy"))
						{
							goto IL_0AFC;
						}
						return new NumericDateTimeFormatPart(format[0], DateTimePart.Year, format.Length, 5, 0, 99999, null, null, null, null, attributes);
					}
					else
					{
						if (!(format == "fffff"))
						{
							goto IL_0AFC;
						}
						return new NumericDateTimeFormatPart(format[0], DateTimePart.Millisecond, 5, 5, 0, 99999, (int f) => f / 100, (int f) => f * 100, null, null, attributes);
					}
					break;
				}
				case 6:
					if (!(format == "ffffff"))
					{
						goto IL_0AFC;
					}
					return new NumericDateTimeFormatPart(format[0], DateTimePart.Millisecond, 6, 6, 0, 999999, (int f) => f / 1000, (int f) => f * 1000, "%f", null, attributes);
				case 7:
					if (!(format == "fffffff"))
					{
						goto IL_0AFC;
					}
					return new NumericDateTimeFormatPart(format[0], DateTimePart.Millisecond, 7, 7, 0, 9999999, (int f) => f / 10000, (int f) => f * 10000, null, null, attributes);
				default:
					goto IL_0AFC;
				}
				return StringDateTimeFormatPart.CreatePeriod(format, attributes);
			}
			IL_0AFC:
			if (attributes != null)
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Format={0} with attributes={1} is unsupported.", new object[] { format, attributes })));
			}
			return ConstantDateTimeFormatPart.FromConstantFormat(format);
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000814F1 File Offset: 0x0007F6F1
		private static IEnumerable<DateTimeFormatPart> _ParseFormatString(string format)
		{
			StringBuilder current = new StringBuilder(format.Length);
			Dictionary<string, string> attributes = null;
			string baseFormat = null;
			string attributeKey = null;
			bool inDoubleQuote = false;
			bool inSingleQuote = false;
			bool lastWasBackslash = false;
			bool inAttributeList = false;
			foreach (char c in format)
			{
				if (lastWasBackslash)
				{
					current.Append(c);
					lastWasBackslash = false;
				}
				else if (inDoubleQuote)
				{
					current.Append(c);
					inDoubleQuote = c != '"';
					lastWasBackslash = c == '\\';
				}
				else if (inSingleQuote)
				{
					current.Append(c);
					inSingleQuote = c != '\'';
					lastWasBackslash = c == '\\';
				}
				else
				{
					if (inAttributeList)
					{
						char c2 = c;
						if (c2 == ';')
						{
							goto IL_018E;
						}
						if (c2 != '=')
						{
							if (c2 == '}')
							{
								goto IL_018E;
							}
							current.Append(c);
						}
						else
						{
							attributeKey = current.ToString();
							current.Clear();
						}
						IL_0227:
						if (c == '}')
						{
							yield return DateTimeFormatPart.Create(baseFormat, new FormatAttributes(attributes));
							inAttributeList = false;
							attributes = null;
							goto IL_036A;
						}
						goto IL_036A;
						IL_018E:
						if (attributeKey == null)
						{
							attributes.Add(current.ToString(), null);
						}
						else
						{
							Optional<string> optional = StdLiteralParsing.TryParse<string>(current.ToString(), default(DeserializationContext));
							if (!optional.HasValue)
							{
								throw new Exception("Unable to parse attribute in format: " + format);
							}
							attributes.Add(attributeKey, optional.Value);
							attributeKey = null;
						}
						current.Clear();
						goto IL_0227;
					}
					if (c == '{')
					{
						inAttributeList = true;
						baseFormat = current.ToString();
						attributes = new Dictionary<string, string>();
						current.Clear();
					}
					else
					{
						if (current.Length == 0 || c == current[current.Length - 1])
						{
							current.Append(c);
						}
						else
						{
							yield return DateTimeFormatPart.Create(current.ToString(), null);
							current.Clear();
							current.Append(c);
						}
						if (c == '\\')
						{
							lastWasBackslash = true;
						}
						if (c == '\'')
						{
							inSingleQuote = true;
						}
						if (c == '"')
						{
							inDoubleQuote = true;
						}
					}
				}
				IL_036A:;
			}
			string text = null;
			yield return DateTimeFormatPart.Create(current.ToString(), null);
			yield break;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x00081501 File Offset: 0x0007F701
		public static IEnumerable<DateTimeFormatPart> ParseFormatString(string format)
		{
			ConstantDateTimeFormatPart lastConstant = null;
			foreach (DateTimeFormatPart part in DateTimeFormatPart._ParseFormatString(format))
			{
				ConstantDateTimeFormatPart constantDateTimeFormatPart = part as ConstantDateTimeFormatPart;
				if (constantDateTimeFormatPart != null)
				{
					if (lastConstant != null)
					{
						lastConstant = new ConstantDateTimeFormatPart(lastConstant.ConstantString + constantDateTimeFormatPart.ConstantString);
					}
					else
					{
						lastConstant = constantDateTimeFormatPart;
					}
				}
				else
				{
					if (lastConstant != null)
					{
						yield return lastConstant;
						lastConstant = null;
					}
					yield return part;
				}
				part = null;
			}
			IEnumerator<DateTimeFormatPart> enumerator = null;
			if (lastConstant != null)
			{
				yield return lastConstant;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x00081511 File Offset: 0x0007F711
		public override string ToString()
		{
			return this.RenderHumanReadable();
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x00081519 File Offset: 0x0007F719
		public override int GetHashCode()
		{
			int hashCode = this.BaseFormatString.GetHashCode();
			int num = 14159;
			FormatAttributes attributes = this.Attributes;
			return hashCode ^ (num * ((attributes != null) ? attributes.GetHashCode() : 0));
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x0008153F File Offset: 0x0007F73F
		private bool Equals(DateTimeFormatPart other)
		{
			return string.Equals(this.BaseFormatString, other.BaseFormatString) && object.Equals(this.Attributes, other.Attributes);
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x00081567 File Offset: 0x0007F767
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((DateTimeFormatPart)obj)));
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x00081595 File Offset: 0x0007F795
		public string RenderHumanReadable()
		{
			return this.FullFormatString;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000815A0 File Offset: 0x0007F7A0
		internal XElement RenderXML(string name)
		{
			XElement xelement = new XElement(name, new object[]
			{
				new XAttribute("BaseFormatString", this.BaseFormatString),
				new XAttribute("FullFormatString", this.FullFormatString),
				this.MatchedPart.HasValue ? new XAttribute("DateTimePart", this.MatchedPart.Value) : null
			});
			if (this.Attributes != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.Attributes.Attributes.OrderBy((KeyValuePair<string, string> kv) => kv.Key))
				{
					xelement.Add(new XElement("Attribute", new object[]
					{
						new XAttribute("key", keyValuePair.Key),
						(keyValuePair.Value == null) ? null : new XAttribute("value", keyValuePair.Value)
					}));
				}
			}
			return xelement;
		}

		// Token: 0x06002D6E RID: 11630
		public abstract XElement RenderXML();

		// Token: 0x06002D6F RID: 11631 RVA: 0x000816F0 File Offset: 0x0007F8F0
		public static DateTimeFormatPart TryParseFromXML(XElement literal)
		{
			string localName = literal.Name.LocalName;
			if (localName == "FormatPart")
			{
				return DateTimeFormatPart.Create(literal.Value, null);
			}
			if (localName == "ConstantFormatPart")
			{
				return ConstantDateTimeFormatPart.TryParseFromXML(literal);
			}
			if (localName == "NumericFormatPart")
			{
				return NumericDateTimeFormatPart.TryParseFromXML(literal);
			}
			if (localName == "StringFormatPart")
			{
				return StringDateTimeFormatPart.TryParseFromXML(literal);
			}
			if (!(localName == "TimeZoneOffsetFormatPart"))
			{
				return null;
			}
			return TimeZoneOffsetFormatPart.TryParseFromXML(literal);
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x00081778 File Offset: 0x0007F978
		internal static Optional<FormatAttributes> ParseAttributesFromXML(XElement literal)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (XElement xelement in literal.Elements("Attribute"))
			{
				XAttribute xattribute = xelement.Attribute("key");
				if (xattribute == null)
				{
					return Optional<FormatAttributes>.Nothing;
				}
				Dictionary<string, string> dictionary2 = dictionary;
				string value = xattribute.Value;
				XAttribute xattribute2 = xelement.Attribute("value");
				dictionary2.Add(value, (xattribute2 != null) ? xattribute2.Value : null);
			}
			return (dictionary.Any<KeyValuePair<string, string>>() ? new FormatAttributes(dictionary, false) : null).Some<FormatAttributes>();
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x00081830 File Offset: 0x0007FA30
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeFormatPart()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["disallow24Hour"] = null;
			DateTimeFormatPart.Disallow24HourFormatAttributes = new FormatAttributes(dictionary);
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			dictionary2["allowLeadingZeros"] = null;
			dictionary2["disallow24Hour"] = null;
			DateTimeFormatPart.AllowLeadingZerosAndDisallow24HourFormatAttributes = new FormatAttributes(dictionary2);
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
			dictionary3["allowLeadingZeros"] = null;
			DateTimeFormatPart.AllowLeadingZerosFormatAttributes = new FormatAttributes(dictionary3);
			Dictionary<string, string> dictionary4 = new Dictionary<string, string>();
			dictionary4["allowNumericZero"] = null;
			DateTimeFormatPart.AllowNumericZeroFormatAttributes = new FormatAttributes(dictionary4);
			Dictionary<string, string> dictionary5 = new Dictionary<string, string>();
			dictionary5["numericZero"] = null;
			DateTimeFormatPart.NumericZeroFormatAttributes = new FormatAttributes(dictionary5);
			Dictionary<string, string> dictionary6 = new Dictionary<string, string>();
			dictionary6["casing"] = "lower";
			DateTimeFormatPart.LowerCaseFormatAttributes = new FormatAttributes(dictionary6);
			DateTimeFormatPart.AllowLeadingZerosPartChars = new char[] { 'M', 'd', 'H', 'h', 'm', 's', 'j', 'V' };
			DateTimeFormatPart.Cache = new string[]
			{
				"y", "yy", "yyy", "yyyy", "yyyyy", "M", "MM", "MMM", "MMMM", "d",
				"dd", "ddd", "dddd", "o", "f", "ff", "fff", "ffff", "fffff", "ffffff",
				"fffffff", "H", "HH", "h", "hh", "t", "tt", "m", "mm", "s",
				"ss", "q", "j", "jjj", "i", "V", "VV", "YY", "YYYY", "Z",
				"ZZ"
			}.Select((string f) => Record.Create<string, FormatAttributes>(f, null)).Concat(DateTimeFormatPart.AllowLeadingZerosPartChars.Select((char f) => Record.Create<string, FormatAttributes>(f.ToString(), DateTimeFormatPart.AllowLeadingZerosFormatAttributes))).Concat(new Record<string, FormatAttributes>[]
			{
				Record.Create<string, FormatAttributes>("t", DateTimeFormatPart.LowerCaseFormatAttributes),
				Record.Create<string, FormatAttributes>("tt", DateTimeFormatPart.LowerCaseFormatAttributes)
			})
				.Concat(new Record<string, FormatAttributes>[]
				{
					Record.Create<string, FormatAttributes>("H", DateTimeFormatPart.Disallow24HourFormatAttributes),
					Record.Create<string, FormatAttributes>("HH", DateTimeFormatPart.Disallow24HourFormatAttributes),
					Record.Create<string, FormatAttributes>("H", DateTimeFormatPart.AllowLeadingZerosAndDisallow24HourFormatAttributes),
					Record.Create<string, FormatAttributes>("HH", DateTimeFormatPart.AllowLeadingZerosAndDisallow24HourFormatAttributes),
					Record.Create<string, FormatAttributes>("Z", DateTimeFormatPart.AllowNumericZeroFormatAttributes),
					Record.Create<string, FormatAttributes>("ZZ", DateTimeFormatPart.AllowNumericZeroFormatAttributes),
					Record.Create<string, FormatAttributes>("Z", DateTimeFormatPart.NumericZeroFormatAttributes),
					Record.Create<string, FormatAttributes>("ZZ", DateTimeFormatPart.NumericZeroFormatAttributes)
				})
				.ToDictionary((Record<string, FormatAttributes> t) => t, delegate(Record<string, FormatAttributes> t)
				{
					string item = t.Item1;
					FormatAttributes item2 = t.Item2;
					return DateTimeFormatPart._Create(item, (item2 != null) ? item2.CloneWithAllAttributesUnhandled() : null);
				});
			DateTimeFormatPart.AllFormats = DateTimeFormatPart.Cache.Values;
		}

		// Token: 0x040015C7 RID: 5575
		public static readonly FormatAttributes Disallow24HourFormatAttributes;

		// Token: 0x040015C8 RID: 5576
		public static readonly FormatAttributes AllowLeadingZerosAndDisallow24HourFormatAttributes;

		// Token: 0x040015C9 RID: 5577
		public static readonly FormatAttributes AllowLeadingZerosFormatAttributes;

		// Token: 0x040015CA RID: 5578
		public static readonly FormatAttributes AllowNumericZeroFormatAttributes;

		// Token: 0x040015CB RID: 5579
		public static readonly FormatAttributes NumericZeroFormatAttributes;

		// Token: 0x040015CC RID: 5580
		public static readonly FormatAttributes LowerCaseFormatAttributes;

		// Token: 0x040015CD RID: 5581
		internal static readonly char[] AllowLeadingZerosPartChars;

		// Token: 0x040015CE RID: 5582
		private static readonly IReadOnlyDictionary<Record<string, FormatAttributes>, DateTimeFormatPart> Cache;

		// Token: 0x040015CF RID: 5583
		public static readonly IEnumerable<DateTimeFormatPart> AllFormats;

		// Token: 0x040015D0 RID: 5584
		private const string LegacyXMLName = "FormatPart";

		// Token: 0x040015D1 RID: 5585
		private const string AttributeXMLName = "Attribute";
	}
}
