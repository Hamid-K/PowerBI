using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012C6 RID: 4806
	internal static class DateTimeFormat
	{
		// Token: 0x06007E67 RID: 32359 RVA: 0x001B1024 File Offset: 0x001AF224
		private static string SourceName(this ValueKind dataType)
		{
			switch (dataType)
			{
			case ValueKind.Time:
				return "time";
			case ValueKind.Date:
				return "date";
			case ValueKind.DateTime:
				return "datetime";
			case ValueKind.DateTimeZone:
				return "datetimezone";
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x06007E68 RID: 32360 RVA: 0x001B1074 File Offset: 0x001AF274
		public static DateTimeFormat.FormatComponentType FromValueKind(TypeValue mType)
		{
			switch (mType.TypeKind)
			{
			case ValueKind.Time:
				return DateTimeFormat.FormatComponentType.Time;
			case ValueKind.Date:
				return DateTimeFormat.FormatComponentType.Date;
			case ValueKind.DateTime:
				return DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time;
			case ValueKind.DateTimeZone:
				return DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.Zone;
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x06007E69 RID: 32361 RVA: 0x001B10BC File Offset: 0x001AF2BC
		public static void UnpackFormatOptions(RecordValue options, out Value format, out Value culture)
		{
			format = Value.Null;
			culture = Value.Null;
			for (int i = 0; i < options.Keys.Length; i++)
			{
				string text = options.Keys[i];
				if (!(text == "Format"))
				{
					if (!(text == "Culture"))
					{
						throw ValueException.NewParameterError<Message1>(Strings.OptionsRecord_UnsupportedKey(options.Keys[i]), options);
					}
					culture = (options[i].IsNull ? Value.Null : options[i].AsText);
				}
				else
				{
					format = (options[i].IsNull ? Value.Null : options[i].AsText);
				}
			}
		}

		// Token: 0x06007E6A RID: 32362 RVA: 0x001B1180 File Offset: 0x001AF380
		public static void ValidateFormat(TypeValue instantType, string format)
		{
			DateTimeFormat.ValidationResults validationResults = null;
			if (format == null)
			{
				return;
			}
			if (DateTimeFormat.TryParseDateTimeFormat(format, out validationResults))
			{
				using (IEnumerator<DateTimeFormat.FormatComponent> enumerator = validationResults.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DateTimeFormat.FormatComponent formatComponent = enumerator.Current;
						string text;
						if (instantType.TypeKind == ValueKind.Time)
						{
							if ((formatComponent.ComponentType & DateTimeFormat.FormatComponentType.Date) != DateTimeFormat.FormatComponentType.None)
							{
								text = Strings.DataComponent_Date;
							}
							else
							{
								if ((formatComponent.ComponentType & DateTimeFormat.FormatComponentType.Zone) == DateTimeFormat.FormatComponentType.None)
								{
									continue;
								}
								text = Strings.DataComponent_Zone;
							}
						}
						else if (instantType.TypeKind == ValueKind.Date)
						{
							if ((formatComponent.ComponentType & DateTimeFormat.FormatComponentType.Time) != DateTimeFormat.FormatComponentType.None)
							{
								text = Strings.DataComponent_Time;
							}
							else
							{
								if ((formatComponent.ComponentType & DateTimeFormat.FormatComponentType.Zone) == DateTimeFormat.FormatComponentType.None)
								{
									continue;
								}
								text = Strings.DataComponent_Zone;
							}
						}
						else if (instantType.TypeKind == ValueKind.DateTime)
						{
							if ((formatComponent.ComponentType & DateTimeFormat.FormatComponentType.Zone) == DateTimeFormat.FormatComponentType.None)
							{
								continue;
							}
							text = Strings.DataComponent_Zone;
						}
						else
						{
							if (instantType.TypeKind != ValueKind.DateTimeZone)
							{
								throw new InvalidOperationException(Strings.UnreachableCodePath);
							}
							continue;
						}
						throw ValueException.NewParameterError<Message2>(Strings.FormatValueDoesNotHaveComponent(instantType.TypeKind.SourceName(), text), RecordValue.New(new NamedValue[]
						{
							new NamedValue("Format", TextValue.New(format))
						}));
					}
					return;
				}
			}
			throw ValueException.NewParameterError<Message0>(Strings.MalformedFormat_Truncation, RecordValue.New(new NamedValue[]
			{
				new NamedValue("Format", TextValue.New(format))
			}));
		}

		// Token: 0x06007E6B RID: 32363 RVA: 0x001B1304 File Offset: 0x001AF504
		static DateTimeFormat()
		{
			Dictionary<string, DateTimeFormat.FormatComponentType> dictionary = new Dictionary<string, DateTimeFormat.FormatComponentType>();
			dictionary["d"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["D"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["f"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["F"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["g"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["G"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["m"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["M"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["o"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.Zone | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["O"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.Zone | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["t"] = DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["T"] = DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["y"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["Y"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary["r"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time;
			dictionary["R"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time;
			dictionary["s"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time;
			dictionary["u"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.Utc;
			dictionary["U"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.Zone;
			DateTimeFormat.standardFormats = dictionary;
			Dictionary<string, DateTimeFormat.FormatComponentType> dictionary2 = new Dictionary<string, DateTimeFormat.FormatComponentType>();
			dictionary2["d"] = DateTimeFormat.FormatComponentType.Date;
			dictionary2["dd"] = DateTimeFormat.FormatComponentType.Date;
			dictionary2["ddd"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary2["dddd"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary2["f"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["ff"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["fff"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["ffff"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["fffff"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["ffffff"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["fffffff"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["F"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["FF"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["FFF"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["FFFF"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["FFFFF"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["FFFFFF"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["FFFFFFF"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["g"] = DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary2["gg"] = DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary2["h"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["hh"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["H"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["HH"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["K"] = DateTimeFormat.FormatComponentType.None;
			dictionary2["m"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["mm"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["M"] = DateTimeFormat.FormatComponentType.Date;
			dictionary2["MM"] = DateTimeFormat.FormatComponentType.Date;
			dictionary2["MMM"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary2["MMMM"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary2["s"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["ss"] = DateTimeFormat.FormatComponentType.Time;
			dictionary2["t"] = DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary2["tt"] = DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary2["y"] = DateTimeFormat.FormatComponentType.Date;
			dictionary2["yy"] = DateTimeFormat.FormatComponentType.Date;
			dictionary2["yyy"] = DateTimeFormat.FormatComponentType.Date;
			dictionary2["yyyy"] = DateTimeFormat.FormatComponentType.Date;
			dictionary2["yyyyy"] = DateTimeFormat.FormatComponentType.Date;
			dictionary2["z"] = DateTimeFormat.FormatComponentType.Zone;
			dictionary2["zz"] = DateTimeFormat.FormatComponentType.Zone;
			dictionary2["zzz"] = DateTimeFormat.FormatComponentType.Zone;
			dictionary2[":"] = DateTimeFormat.FormatComponentType.Time | DateTimeFormat.FormatComponentType.CultureDependent;
			dictionary2["/"] = DateTimeFormat.FormatComponentType.Date | DateTimeFormat.FormatComponentType.CultureDependent;
			DateTimeFormat.customFormats = dictionary2;
			IEnumerable<DateTimeFormat.CondensedCustomFormatSpecifier> enumerable = from key in DateTimeFormat.customFormats.Keys
				group key by key[0] into @group
				select @group.OrderByDescending((string fmt) => fmt.Length).First<string>() into fmt
				select new DateTimeFormat.CondensedCustomFormatSpecifier(fmt.Count<char>(), fmt[0]);
			string text = "^" + DateTimeFormat.NamedGroupRegex("standard", DateTimeFormat.OrElseRegex(DateTimeFormat.standardFormats.Keys.ToArray<string>())) + "$";
			string text2 = DateTimeFormat.OrElseRegex(enumerable.Select((DateTimeFormat.CondensedCustomFormatSpecifier fmt) => "^%" + DateTimeFormat.NamedGroupRegex("custom", (fmt.Count > 1) ? (fmt.BaseFormat.ToString() + "{1," + fmt.Count.ToString() + "}") : fmt.BaseFormat.ToString())).ToArray<string>()) + "$";
			string text3 = DateTimeFormat.OrElseRegex(enumerable.Select(delegate(DateTimeFormat.CondensedCustomFormatSpecifier fmt)
			{
				string text9;
				if (fmt.Count > 1)
				{
					text9 = fmt.BaseFormat.ToString() + "{1," + fmt.Count.ToString() + "}";
				}
				else
				{
					text9 = fmt.BaseFormat.ToString();
				}
				return DateTimeFormat.NamedGroupRegex("custom", text9);
			}).ToArray<string>());
			string text4 = "\\\\" + DateTimeFormat.NamedGroupRegex("literal", ".");
			string text5 = "(\\\"" + DateTimeFormat.NamedGroupRegex("literal", "[^\\\"]*") + "\\\"";
			string text6 = "(\\\"" + DateTimeFormat.NamedGroupRegex("unmatchedQuoted", "[^\\\"]*") + ")$)";
			string text7 = DateTimeFormat.NamedGroupRegex("escapeAtEndToken", "\\\\$");
			string text8 = DateTimeFormat.NamedGroupRegex("literal", ".");
			DateTimeFormat.formatMatcher = new Regex(DateTimeFormat.OrElseRegex(new string[] { text, text2, text3, text4, text5, text6, text7, text8 }), RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
		}

		// Token: 0x06007E6C RID: 32364 RVA: 0x001B17BA File Offset: 0x001AF9BA
		private static string NamedGroupRegex(string name, string pattern)
		{
			return string.Concat(new string[] { "(?<", name, ">", pattern, ")" });
		}

		// Token: 0x06007E6D RID: 32365 RVA: 0x001B17E7 File Offset: 0x001AF9E7
		private static string OrElseRegex(params string[] patterns)
		{
			return "(" + string.Join("|", patterns) + ")";
		}

		// Token: 0x06007E6E RID: 32366 RVA: 0x001B1804 File Offset: 0x001AFA04
		public static bool TryParseDateTimeFormat(IEngineHost engine, RecordValue userFormatOptions, string functionName, out DateTimeFormat.ValidationResults results)
		{
			IEnumerable<string> enumerable = userFormatOptions.Keys.Except(new string[] { "Culture", "Format" });
			using (IEnumerator<string> enumerator = enumerable.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					Keys keys = Keys.New("Name", "Value");
					int num = userFormatOptions.IndexOf(text);
					RecordValue recordValue = RecordValue.New(keys, new IValueReference[]
					{
						TextValue.New(userFormatOptions.Keys[num]),
						userFormatOptions.GetReference(num)
					});
					throw ValueException.NewParameterError<Message2>(Strings.OptionsRecord_Unsupported(functionName, enumerable.First<string>()), recordValue);
				}
			}
			Value @null = Value.Null;
			string text2 = null;
			for (int i = 0; i < userFormatOptions.Count; i++)
			{
				if (userFormatOptions.Keys[i] == "Culture")
				{
					Value value = userFormatOptions[i];
				}
				if (userFormatOptions.Keys[i] == "Format")
				{
					Value value2 = userFormatOptions[i];
					if (!value2.IsNull)
					{
						text2 = value2.AsText.String;
					}
				}
			}
			return DateTimeFormat.TryParseDateTimeFormat(text2, out results);
		}

		// Token: 0x06007E6F RID: 32367 RVA: 0x001B1940 File Offset: 0x001AFB40
		public static bool TryParseDateTimeFormat(string format, out DateTimeFormat.ValidationResults results)
		{
			bool isValid = true;
			if (format.Length == 0)
			{
				results = new DateTimeFormat.ValidationResults(new DateTimeFormat.FormatComponent[0], format);
				return false;
			}
			IList<DateTimeFormat.FormatComponent> tokens;
			if (format.Length == 1)
			{
				DateTimeFormat.FormatComponentType formatComponentType;
				DateTimeFormat.standardFormats.TryGetValue(format, out formatComponentType);
				tokens = new DateTimeFormat.FormatComponent[]
				{
					new DateTimeFormat.FormatComponent(formatComponentType, format)
				};
			}
			else
			{
				tokens = new List<DateTimeFormat.FormatComponent>();
				DateTimeFormat.formatMatcher.Replace(format, delegate(Match match)
				{
					string value = match.Value;
					DateTimeFormat.FormatComponentType formatComponentType2;
					if (value[0] == '"' || value[0] == '\\')
					{
						isValid = false;
						tokens.Add(new DateTimeFormat.FormatComponent(DateTimeFormat.FormatComponentType.Unknown, match.Value));
					}
					else if (DateTimeFormat.customFormats.TryGetValue(value, out formatComponentType2))
					{
						tokens.Add(new DateTimeFormat.FormatComponent(DateTimeFormat.customFormats[value], value));
					}
					else
					{
						tokens.Add(new DateTimeFormat.FormatComponent(DateTimeFormat.FormatComponentType.None, value));
					}
					return "";
				});
			}
			results = new DateTimeFormat.ValidationResults(tokens, format);
			return isValid;
		}

		// Token: 0x0400456D RID: 17773
		private const string STANDARD_TOKEN_NAME = "standard";

		// Token: 0x0400456E RID: 17774
		private const string CUSTOM_TOKEN_NAME = "custom";

		// Token: 0x0400456F RID: 17775
		private const string LITERAL_TOKEN_NAME = "literal";

		// Token: 0x04004570 RID: 17776
		private const string UNMATCHED_QUOTED_TOKEN = "unmatchedQuoted";

		// Token: 0x04004571 RID: 17777
		private const string ESCAPE_AT_END_TOKEN = "escapeAtEndToken";

		// Token: 0x04004572 RID: 17778
		private static readonly Regex formatMatcher;

		// Token: 0x04004573 RID: 17779
		private static readonly Dictionary<string, DateTimeFormat.FormatComponentType> standardFormats;

		// Token: 0x04004574 RID: 17780
		private static readonly Dictionary<string, DateTimeFormat.FormatComponentType> customFormats;

		// Token: 0x020012C7 RID: 4807
		public class ValidationResults : IEnumerable<DateTimeFormat.FormatComponent>, IEnumerable
		{
			// Token: 0x06007E70 RID: 32368 RVA: 0x001B19DC File Offset: 0x001AFBDC
			public ValidationResults(IList<DateTimeFormat.FormatComponent> tokens, string format)
			{
				this.tokens = tokens;
				this.format = format;
			}

			// Token: 0x17002241 RID: 8769
			// (get) Token: 0x06007E71 RID: 32369 RVA: 0x001B19F4 File Offset: 0x001AFBF4
			public DateTimeFormat.FormatComponentType ComponentsUsed
			{
				get
				{
					if (this.tokens == null)
					{
						return DateTimeFormat.FormatComponentType.None;
					}
					return this.tokens.Select((DateTimeFormat.FormatComponent token) => token.ComponentType).Aggregate((DateTimeFormat.FormatComponentType cur, DateTimeFormat.FormatComponentType next) => cur | next);
				}
			}

			// Token: 0x17002242 RID: 8770
			// (get) Token: 0x06007E72 RID: 32370 RVA: 0x001B1A59 File Offset: 0x001AFC59
			public int TokenCount
			{
				get
				{
					return this.tokens.Count;
				}
			}

			// Token: 0x06007E73 RID: 32371 RVA: 0x001B1A66 File Offset: 0x001AFC66
			public IEnumerator<DateTimeFormat.FormatComponent> GetEnumerator()
			{
				return this.tokens.AsEnumerable<DateTimeFormat.FormatComponent>().GetEnumerator();
			}

			// Token: 0x06007E74 RID: 32372 RVA: 0x001B1A78 File Offset: 0x001AFC78
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04004575 RID: 17781
			private readonly IList<DateTimeFormat.FormatComponent> tokens;

			// Token: 0x04004576 RID: 17782
			private readonly string format;
		}

		// Token: 0x020012C9 RID: 4809
		public struct FormatComponent
		{
			// Token: 0x17002243 RID: 8771
			// (get) Token: 0x06007E79 RID: 32377 RVA: 0x001B1A9A File Offset: 0x001AFC9A
			public DateTimeFormat.FormatComponentType ComponentType
			{
				get
				{
					return this.componentType;
				}
			}

			// Token: 0x17002244 RID: 8772
			// (get) Token: 0x06007E7A RID: 32378 RVA: 0x001B1AA2 File Offset: 0x001AFCA2
			public string Token
			{
				get
				{
					return this.token;
				}
			}

			// Token: 0x06007E7B RID: 32379 RVA: 0x001B1AAA File Offset: 0x001AFCAA
			public FormatComponent(DateTimeFormat.FormatComponentType componentType, string token)
			{
				this.componentType = componentType;
				this.token = token;
			}

			// Token: 0x0400457A RID: 17786
			private readonly DateTimeFormat.FormatComponentType componentType;

			// Token: 0x0400457B RID: 17787
			private readonly string token;
		}

		// Token: 0x020012CA RID: 4810
		private struct CondensedCustomFormatSpecifier
		{
			// Token: 0x06007E7C RID: 32380 RVA: 0x001B1ABA File Offset: 0x001AFCBA
			public CondensedCustomFormatSpecifier(int count, char baseFormat)
			{
				this.count = count;
				this.baseFormat = baseFormat;
			}

			// Token: 0x17002245 RID: 8773
			// (get) Token: 0x06007E7D RID: 32381 RVA: 0x001B1ACA File Offset: 0x001AFCCA
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x17002246 RID: 8774
			// (get) Token: 0x06007E7E RID: 32382 RVA: 0x001B1AD2 File Offset: 0x001AFCD2
			public char BaseFormat
			{
				get
				{
					return this.baseFormat;
				}
			}

			// Token: 0x0400457C RID: 17788
			private readonly int count;

			// Token: 0x0400457D RID: 17789
			private readonly char baseFormat;
		}

		// Token: 0x020012CB RID: 4811
		[Flags]
		public enum FormatComponentType
		{
			// Token: 0x0400457F RID: 17791
			None = 0,
			// Token: 0x04004580 RID: 17792
			Date = 1,
			// Token: 0x04004581 RID: 17793
			Time = 2,
			// Token: 0x04004582 RID: 17794
			Zone = 4,
			// Token: 0x04004583 RID: 17795
			Utc = 8,
			// Token: 0x04004584 RID: 17796
			CultureDependent = 16,
			// Token: 0x04004585 RID: 17797
			Unknown = -1,
			// Token: 0x04004586 RID: 17798
			First = 1,
			// Token: 0x04004587 RID: 17799
			Last = 16
		}

		// Token: 0x020012CC RID: 4812
		private struct FormatCulture
		{
			// Token: 0x06007E7F RID: 32383 RVA: 0x001B1ADA File Offset: 0x001AFCDA
			public FormatCulture(string format, string culture)
			{
				this.format = format;
				this.culture = culture;
			}

			// Token: 0x17002247 RID: 8775
			// (get) Token: 0x06007E80 RID: 32384 RVA: 0x001B1AEA File Offset: 0x001AFCEA
			public string Format
			{
				get
				{
					return this.format;
				}
			}

			// Token: 0x17002248 RID: 8776
			// (get) Token: 0x06007E81 RID: 32385 RVA: 0x001B1AF2 File Offset: 0x001AFCF2
			public string Culture
			{
				get
				{
					return this.culture;
				}
			}

			// Token: 0x04004588 RID: 17800
			private readonly string format;

			// Token: 0x04004589 RID: 17801
			private readonly string culture;
		}
	}
}
