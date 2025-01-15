using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers
{
	// Token: 0x02001D62 RID: 7522
	[Parseable("TryParseXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public class NumberFormatDetails : IRenderableLiteral, IEquatable<NumberFormatDetails>
	{
		// Token: 0x0600FCD5 RID: 64725 RVA: 0x0035EE28 File Offset: 0x0035D028
		[JsonConstructor]
		public NumberFormatDetails(char decimalMarkChar, Optional<char> separatorChar = default(Optional<char>), Optional<uint[]> separatedSectionSizes = default(Optional<uint[]>), Optional<decimal> scale = default(Optional<decimal>), Optional<string> currencySymbol = default(Optional<string>), bool trailingSign = true)
		{
			this.SeparatorChar = separatorChar;
			this.SeparatedSectionSizes = separatedSectionSizes;
			this.Scale = scale;
			this.DecimalMarkChar = decimalMarkChar;
			this.CurrencySymbol = currencySymbol;
			this.TrailingSign = trailingSign;
			NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
			numberFormatInfo.NumberGroupSeparator = separatorChar.Select((char c) => c.ToString()).OrElse("");
			numberFormatInfo.NumberDecimalSeparator = this.DecimalMarkChar.ToString();
			this.NumberFormatInfo = numberFormatInfo;
			if (currencySymbol.HasValue)
			{
				this.NumberFormatInfo.CurrencySymbol = currencySymbol.Value;
			}
			if (separatedSectionSizes.HasValue)
			{
				this.NumberFormatInfo.NumberGroupSizes = separatedSectionSizes.Value.Select((uint size) => (int)size).ToArray<int>();
				this.NumberFormatInfo.CurrencyGroupSizes = this.NumberFormatInfo.NumberGroupSizes;
			}
		}

		// Token: 0x0600FCD6 RID: 64726 RVA: 0x0035EF34 File Offset: 0x0035D134
		public NumberFormatDetails(char decimalMarkChar, char? separatorChar, uint[] separatedSectionSizes, decimal? scale, bool trailingSign = true)
		{
			Optional<char> optional = separatorChar.SomeIfNotNull<char>();
			Optional<uint[]> optional2 = separatedSectionSizes.SomeIfNotNull<uint[]>();
			this..ctor(decimalMarkChar, optional, optional2, scale.SomeIfNotNull<decimal>(), default(Optional<string>), trailingSign);
		}

		// Token: 0x17002A20 RID: 10784
		// (get) Token: 0x0600FCD7 RID: 64727 RVA: 0x0035EF6C File Offset: 0x0035D16C
		public char DecimalMarkChar { get; }

		// Token: 0x17002A21 RID: 10785
		// (get) Token: 0x0600FCD8 RID: 64728 RVA: 0x0035EF74 File Offset: 0x0035D174
		public Optional<char> SeparatorChar { get; }

		// Token: 0x17002A22 RID: 10786
		// (get) Token: 0x0600FCD9 RID: 64729 RVA: 0x0035EF7C File Offset: 0x0035D17C
		public Optional<uint[]> SeparatedSectionSizes { get; }

		// Token: 0x17002A23 RID: 10787
		// (get) Token: 0x0600FCDA RID: 64730 RVA: 0x0035EF84 File Offset: 0x0035D184
		public Optional<decimal> Scale { get; }

		// Token: 0x17002A24 RID: 10788
		// (get) Token: 0x0600FCDB RID: 64731 RVA: 0x0035EF8C File Offset: 0x0035D18C
		public Optional<string> CurrencySymbol { get; }

		// Token: 0x17002A25 RID: 10789
		// (get) Token: 0x0600FCDC RID: 64732 RVA: 0x0035EF94 File Offset: 0x0035D194
		public bool TrailingSign { get; }

		// Token: 0x17002A26 RID: 10790
		// (get) Token: 0x0600FCDD RID: 64733 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal bool AllowParseParensAsNegative
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002A27 RID: 10791
		// (get) Token: 0x0600FCDE RID: 64734 RVA: 0x0035EF9C File Offset: 0x0035D19C
		internal NumberFormatInfo NumberFormatInfo { get; }

		// Token: 0x17002A28 RID: 10792
		// (get) Token: 0x0600FCDF RID: 64735 RVA: 0x0035EFA4 File Offset: 0x0035D1A4
		public static NumberFormatDetails Default { get; } = new NumberFormatDetails('.', ','.Some<char>(), default(Optional<uint[]>), default(Optional<decimal>), default(Optional<string>), true);

		// Token: 0x17002A29 RID: 10793
		// (get) Token: 0x0600FCE0 RID: 64736 RVA: 0x0035EFAB File Offset: 0x0035D1AB
		internal static NumberFormatDetails Alternate { get; } = new NumberFormatDetails(',', '.'.Some<char>(), default(Optional<uint[]>), default(Optional<decimal>), default(Optional<string>), true);

		// Token: 0x0600FCE1 RID: 64737 RVA: 0x0035EFB2 File Offset: 0x0035D1B2
		public string RenderHumanReadable()
		{
			return this.ToString();
		}

		// Token: 0x0600FCE2 RID: 64738 RVA: 0x0035EFBC File Offset: 0x0035D1BC
		public XElement RenderXML()
		{
			List<object> list = new List<object>();
			if (this.SeparatorChar.HasValue)
			{
				list.Add(new XAttribute("SeparatorChar", NumberFormatDetails.ToStringOrEmpty<char>(this.SeparatorChar)));
			}
			if (this.SeparatedSectionSizes.HasValue)
			{
				list.Add(this.SeparatedSectionSizes.Select((uint[] sizes) => sizes.CollectionToXML("SeparatedSectionSizes", "Item", ObjectFormatting.Literal, null, Array.Empty<Func<uint, XAttribute>>())).OrElseDefault<XElement>());
			}
			if (this.Scale.HasValue)
			{
				list.Add(new XAttribute("Scale", NumberFormatDetails.ToStringOrEmpty<decimal>(this.Scale)));
			}
			list.Add(new XAttribute("DecimalMarkChar", this.DecimalMarkChar.ToString()));
			if (this.CurrencySymbol.HasValue)
			{
				list.Add(new XAttribute("CurrencySymbol", NumberFormatDetails.ToStringOrEmpty<string>(this.CurrencySymbol)));
			}
			list.Add(new XAttribute("TrailingSign", this.TrailingSign.ToString()));
			return new XElement("NumberFormatDetails", list.Cast<object>().ToArray<object>());
		}

		// Token: 0x0600FCE3 RID: 64739 RVA: 0x0035F107 File Offset: 0x0035D307
		private static Optional<decimal> ReadOptionalDecimalAttribute(string attribute)
		{
			if (attribute != null && attribute.Length != 0)
			{
				return decimal.Parse(attribute, CultureInfo.InvariantCulture).Some<decimal>();
			}
			return Optional<decimal>.Nothing;
		}

		// Token: 0x0600FCE4 RID: 64740 RVA: 0x0035F12A File Offset: 0x0035D32A
		private static Optional<uint> ReadOptionalUintAttribute(string attribute)
		{
			if (attribute != null && attribute.Length != 0)
			{
				return uint.Parse(attribute, CultureInfo.InvariantCulture).Some<uint>();
			}
			return Optional<uint>.Nothing;
		}

		// Token: 0x0600FCE5 RID: 64741 RVA: 0x0035F14D File Offset: 0x0035D34D
		private static Optional<char> ReadOptionalCharAttribute(string attribute)
		{
			if (attribute != null && attribute.Length != 0)
			{
				return char.Parse(attribute).Some<char>();
			}
			return Optional<char>.Nothing;
		}

		// Token: 0x0600FCE6 RID: 64742 RVA: 0x0035F16C File Offset: 0x0035D36C
		private static Optional<char> ReadOptionalCharLiteral(string attribute)
		{
			if (attribute != null && attribute.Length != 0)
			{
				return StdLiteralParsing.TryParse<char>(attribute, default(DeserializationContext));
			}
			return Optional<char>.Nothing;
		}

		// Token: 0x0600FCE7 RID: 64743 RVA: 0x0035F199 File Offset: 0x0035D399
		private static Optional<string> ReadOptionalStringAttribute(string attribute)
		{
			if (attribute != null && attribute.Length != 0)
			{
				return attribute.Some<string>();
			}
			return Optional<string>.Nothing;
		}

		// Token: 0x0600FCE8 RID: 64744 RVA: 0x0035F1B4 File Offset: 0x0035D3B4
		private static Optional<string> ReadOptionalStringLiteral(string attribute)
		{
			if (attribute != null && attribute.Length != 0)
			{
				return StdLiteralParsing.TryParse<string>(attribute, default(DeserializationContext));
			}
			return Optional<string>.Nothing;
		}

		// Token: 0x0600FCE9 RID: 64745 RVA: 0x0035F1E4 File Offset: 0x0035D3E4
		private static Optional<bool> ReadOptionalBooleanAttribute(string attribute)
		{
			if (attribute != null && attribute.Length != 0)
			{
				Optional<bool> optional = StdLiteralParsing.TryParse<bool>(attribute, default(DeserializationContext));
				return (optional.HasValue && optional.Value).Some<bool>();
			}
			return Optional<bool>.Nothing;
		}

		// Token: 0x0600FCEA RID: 64746 RVA: 0x0035F22C File Offset: 0x0035D42C
		internal static Optional<NumberFormatDetails> TryParseXML(XElement literal)
		{
			Optional<NumberFormatDetails> optional;
			try
			{
				if (literal.Name != "NumberFormatDetails")
				{
					optional = Optional<NumberFormatDetails>.Nothing;
				}
				else
				{
					XAttribute xattribute = literal.Attribute("SeparatedSectionSize");
					Optional<uint> optional2 = NumberFormatDetails.ReadOptionalUintAttribute((xattribute != null) ? xattribute.Value : null);
					Optional<uint[]> optional3 = from str in literal.Element("SeparatedSectionSizes").SomeIfNotNull<XElement>()
						select StdLiteralParsing.TryParse<uint[]>(str, default(DeserializationContext));
					XAttribute xattribute2 = literal.Attribute("DecimalMarkChar");
					char c = NumberFormatDetails.ReadOptionalCharAttribute((xattribute2 != null) ? xattribute2.Value : null).OrElse('.');
					XAttribute xattribute3 = literal.Attribute("SeparatorChar");
					Optional<char> optional4 = NumberFormatDetails.ReadOptionalCharAttribute((xattribute3 != null) ? xattribute3.Value : null);
					Optional<uint[]>[] array = new Optional<uint[]>[2];
					array[0] = optional3;
					array[1] = optional2.Select((uint size) => new uint[] { size });
					Optional<uint[]> optional5 = array.FirstValue<uint[]>();
					XAttribute xattribute4 = literal.Attribute("Scale");
					Optional<decimal> optional6 = NumberFormatDetails.ReadOptionalDecimalAttribute((xattribute4 != null) ? xattribute4.Value : null);
					XAttribute xattribute5 = literal.Attribute("CurrencySymbol");
					Optional<string> optional7 = NumberFormatDetails.ReadOptionalStringAttribute((xattribute5 != null) ? xattribute5.Value : null);
					XAttribute xattribute6 = literal.Attribute("TrailingSign");
					optional = new NumberFormatDetails(c, optional4, optional5, optional6, optional7, NumberFormatDetails.ReadOptionalBooleanAttribute((xattribute6 != null) ? xattribute6.Value : null).OrElse(true)).Some<NumberFormatDetails>();
				}
			}
			catch
			{
				optional = Optional<NumberFormatDetails>.Nothing;
			}
			return optional;
		}

		// Token: 0x0600FCEB RID: 64747 RVA: 0x0035F3E0 File Offset: 0x0035D5E0
		internal static Optional<NumberFormatDetails> TryParseHumanReadable(string literal)
		{
			if (!literal.StartsWith("\"(", StringComparison.Ordinal) || !literal.EndsWith(")\"", StringComparison.Ordinal))
			{
				return Optional<NumberFormatDetails>.Nothing;
			}
			literal = literal.Substring(2, literal.Length - 4);
			int num = literal.IndexOf(", ", StringComparison.Ordinal);
			Optional<char> optional = NumberFormatDetails.ReadOptionalCharLiteral(literal.Substring(0, num));
			literal = literal.Substring(num + 2);
			Optional<uint[]> optional2;
			if (literal.StartsWith(",", StringComparison.Ordinal))
			{
				optional2 = Optional<uint[]>.Nothing;
				literal = literal.Substring(2);
			}
			else
			{
				num = literal.IndexOf("]", StringComparison.Ordinal);
				optional2 = from xs in (from str in literal.Substring(1, num - 1).Split(new string[] { ", " }, StringSplitOptions.None)
						select StdLiteralParsing.TryParse<uint>(str, default(DeserializationContext))).WholeSequenceOfValues<uint>()
					select xs.ToArray<uint>();
				literal = literal.Substring(num + 3);
			}
			num = literal.IndexOf(", ", StringComparison.Ordinal);
			Optional<decimal> optional3 = NumberFormatDetails.ReadOptionalDecimalAttribute(literal.Substring(0, num));
			literal = literal.Substring(num + 2);
			num = literal.IndexOf(", ", StringComparison.Ordinal);
			char value = NumberFormatDetails.ReadOptionalCharLiteral(literal.Substring(0, num)).Value;
			literal = literal.Substring(num + 2);
			num = literal.IndexOf(", ", StringComparison.Ordinal);
			Optional<string> optional4 = NumberFormatDetails.ReadOptionalStringLiteral(literal.Substring(0, num));
			literal = literal.Substring(num + 2);
			bool flag = NumberFormatDetails.ReadOptionalBooleanAttribute(literal).OrElse(true);
			return new NumberFormatDetails(value, optional, optional2, optional3, optional4, flag).Some<NumberFormatDetails>();
		}

		// Token: 0x0600FCEC RID: 64748 RVA: 0x0035F586 File Offset: 0x0035D786
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NumberFormatDetails);
		}

		// Token: 0x0600FCED RID: 64749 RVA: 0x0035F594 File Offset: 0x0035D794
		public bool Equals(NumberFormatDetails other)
		{
			return other != null && (other == this || (other.DecimalMarkChar.Equals(this.DecimalMarkChar) && other.SeparatorChar.Equals(this.SeparatorChar) && other.TrailingSign.Equals(this.TrailingSign) && other.CurrencySymbol.Equals(this.CurrencySymbol) && other.Scale.Equals(this.Scale) && other.SeparatedSectionSizes.HasValue == this.SeparatedSectionSizes.HasValue && (!this.SeparatedSectionSizes.HasValue || this.SeparatedSectionSizes.Value.SequenceEqual(other.SeparatedSectionSizes.Value))));
		}

		// Token: 0x0600FCEE RID: 64750 RVA: 0x0035F680 File Offset: 0x0035D880
		public override int GetHashCode()
		{
			return this.SeparatorChar.GetHashCode() ^ (197 * this.DecimalMarkChar.GetHashCode()) ^ this.SeparatedSectionSizes.Select((uint[] sizes) => sizes.Aggregate(0, (int a, uint b) => (int)((long)(a * 31) ^ (long)((ulong)b)))).OrElse(0) ^ this.Scale.GetHashCode() ^ this.TrailingSign.GetHashCode() ^ this.CurrencySymbol.GetHashCode();
		}

		// Token: 0x0600FCEF RID: 64751 RVA: 0x0035F724 File Offset: 0x0035D924
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("\"({0}, {1}, {2}, {3}, {4}, {5})\"", new object[]
			{
				NumberFormatDetails.ToLiteralOrEmpty<char>(this.SeparatorChar),
				NumberFormatDetails.ToLiteralOrEmpty<uint[]>(this.SeparatedSectionSizes),
				NumberFormatDetails.ToStringOrEmpty<decimal>(this.Scale),
				this.DecimalMarkChar.ToLiteral(null),
				NumberFormatDetails.ToLiteralOrEmpty<string>(this.CurrencySymbol),
				this.TrailingSign
			}));
		}

		// Token: 0x0600FCF0 RID: 64752 RVA: 0x0035F7A0 File Offset: 0x0035D9A0
		private static string ToStringOrEmpty<T>(Optional<T> optional)
		{
			if (!optional.HasValue)
			{
				return string.Empty;
			}
			T value = optional.Value;
			return value.ToString();
		}

		// Token: 0x0600FCF1 RID: 64753 RVA: 0x0035F7D1 File Offset: 0x0035D9D1
		private static string ToLiteralOrEmpty<T>(Optional<T> optional)
		{
			if (!optional.HasValue)
			{
				return string.Empty;
			}
			return optional.Value.ToLiteral(null);
		}

		// Token: 0x0600FCF2 RID: 64754 RVA: 0x0035F7F4 File Offset: 0x0035D9F4
		internal static NumberFormatDetails Learn(IReadOnlyList<string> numberStrings, bool forParsing)
		{
			foreach (bool flag in new bool[]
			{
				default(bool),
				true
			})
			{
				List<NumberFormatDetails.NumberParts> list;
				char c;
				bool flag2;
				char? c2;
				uint[] array2;
				if (NumberFormatDetails.LearnOutputFormatDetails(numberStrings, numberStrings.Count, flag, true, true, forParsing, out list, out c, out flag2, out c2, out array2))
				{
					if (c2 == null)
					{
						c2 = new char?((c == ',' || c == '٫') ? '.' : ',');
					}
					Optional<char> optional = c2.SomeIfNotNull<char>();
					Optional<uint[]> optional2 = (forParsing ? Optional<uint[]>.Nothing : array2.SomeIfNotNull<uint[]>());
					Optional<decimal> nothing = Optional<decimal>.Nothing;
					return new NumberFormatDetails(c, optional, optional2, nothing, Optional<string>.Nothing, flag2);
				}
			}
			return null;
		}

		// Token: 0x0600FCF3 RID: 64755 RVA: 0x0035F898 File Offset: 0x0035DA98
		internal static bool LearnOutputFormatDetails(IEnumerable<string> formattedNumbers, int numFormattedNumbers, bool alternateDecimalMarkMode, bool allowExponents, bool allowInitialPlus, bool forParsing, out List<NumberFormatDetails.NumberParts> numberParts, out char decimalMarkVal, out bool trailingSign, out char? separatorChar, out uint[] separatedSectionSizes)
		{
			decimalMarkVal = '\0';
			char? c = null;
			separatorChar = null;
			separatedSectionSizes = null;
			trailingSign = false;
			numberParts = new List<NumberFormatDetails.NumberParts>(numFormattedNumbers);
			foreach (string text in formattedNumbers)
			{
				NumberFormatDetails.NumberParts numberParts2 = default(NumberFormatDetails.NumberParts);
				int num = text.IndexOf("E", StringComparison.OrdinalIgnoreCase);
				string text2;
				if (num < 0)
				{
					text2 = text;
				}
				else
				{
					if (!allowExponents)
					{
						return false;
					}
					text2 = text.Substring(0, num);
					numberParts2.ExponentPart = text.Substring(num + 1);
				}
				string text3 = text2.Trim(new char[] { ' ' });
				if (text3.Length == 0)
				{
					return false;
				}
				string text4;
				if (text3[0] == '-' || (allowInitialPlus && text3[0] == '+'))
				{
					text4 = text3.Substring(1);
				}
				else
				{
					text4 = text3;
				}
				if (forParsing)
				{
					int num2 = text4.Length - 1;
					char c2 = text4[num2];
					if (c2 == '-' || c2 == '+')
					{
						text4 = text4.Substring(0, num2);
						trailingSign = true;
					}
				}
				IReadOnlyList<Match> readOnlyList = NumberFormatDetails.NotDigit.NonCachingMatches(text4).ToList<Match>();
				if (readOnlyList.Count == 0)
				{
					if (!forParsing && !NumberFormatDetails.<LearnOutputFormatDetails>g__IsAsciiSimpleInteger|51_2(text2))
					{
						return false;
					}
					numberParts.Add(new NumberFormatDetails.NumberParts
					{
						IntegerPart = text2
					});
				}
				else
				{
					if (readOnlyList.Any((Match s) => s.Length == 1 && (s.Value == "-" || s.Value == "+")))
					{
						return false;
					}
					if (c != null && separatorChar != null)
					{
						Match match = readOnlyList[readOnlyList.Count - 1];
						char? separatorCharLocal = separatorChar;
						if (readOnlyList.DropLast<Match>().All(delegate(Match s)
						{
							if (s.Length == 1)
							{
								int num9 = (int)s.Value[0];
								char? separatorCharLocal2 = separatorCharLocal;
								int? num10 = ((separatorCharLocal2 != null) ? new int?((int)separatorCharLocal2.GetValueOrDefault()) : null);
								return (num9 == num10.GetValueOrDefault()) & (num10 != null);
							}
							return false;
						}) && match.Length == 1)
						{
							int num3 = (int)match.Value[0];
							char? c3 = separatorCharLocal;
							int? num4 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
							if ((num3 == num4.GetValueOrDefault()) & (num4 != null))
							{
								goto IL_0750;
							}
							int num5 = (int)match.Value[0];
							c3 = c;
							num4 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
							if ((num5 == num4.GetValueOrDefault()) & (num4 != null))
							{
								goto IL_0750;
							}
						}
						return false;
					}
					if ((from s in readOnlyList.DropLast<Match>()
						select s.Value).Distinct<string>().Count<string>() <= 1)
					{
						if (!readOnlyList.Any((Match s) => s.Length != 1))
						{
							bool flag = readOnlyList[0].Value[0] != readOnlyList[readOnlyList.Count - 1].Value[0];
							char? c4 = null;
							char? c5 = null;
							char? c3;
							int? num4;
							if (flag)
							{
								c4 = new char?(readOnlyList[readOnlyList.Count - 1].Value[0]);
								c5 = new char?(readOnlyList[0].Value[0]);
							}
							else if (readOnlyList.Count == 1)
							{
								char c6 = readOnlyList[0].Value[0];
								c3 = separatorChar;
								num4 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
								int num6 = (int)c6;
								if (!((num4.GetValueOrDefault() == num6) & (num4 != null)))
								{
									c3 = c;
									num4 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
									num6 = (int)c6;
									if (!((num4.GetValueOrDefault() == num6) & (num4 != null)))
									{
										if (c6 == ',')
										{
											if (alternateDecimalMarkMode)
											{
												c4 = new char?(c6);
											}
											else
											{
												c5 = new char?(c6);
											}
										}
										else if (NumberOptions.DecimalMarkOptions.Contains(c6))
										{
											c4 = new char?(c6);
										}
										else
										{
											c5 = new char?(c6);
										}
									}
								}
							}
							else
							{
								c5 = new char?(readOnlyList[0].Value[0]);
							}
							int? num7;
							if (c4 != null)
							{
								c3 = c4;
								num4 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
								int num6 = 44;
								if (alternateDecimalMarkMode != ((num4.GetValueOrDefault() == num6) & (num4 != null)))
								{
									return false;
								}
								if (c != null)
								{
									c3 = c4;
									num4 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
									num6 = (int)c.Value;
									if (!((num4.GetValueOrDefault() == num6) & (num4 != null)))
									{
										return false;
									}
								}
								c3 = c4;
								num4 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
								c3 = separatorChar;
								num7 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
								if (!((num4.GetValueOrDefault() == num7.GetValueOrDefault()) & (num4 != null == (num7 != null))))
								{
									c3 = c4;
									num7 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
									c3 = c5;
									num4 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
									if (!((num7.GetValueOrDefault() == num4.GetValueOrDefault()) & (num7 != null == (num4 != null))))
									{
										c = c4;
										goto IL_0622;
									}
								}
								return false;
							}
							IL_0622:
							if (c5 == null)
							{
								goto IL_0750;
							}
							if (alternateDecimalMarkMode)
							{
								c3 = c5;
								num4 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
								int num6 = 44;
								if ((num4.GetValueOrDefault() == num6) & (num4 != null))
								{
									return false;
								}
							}
							if (separatorChar != null)
							{
								c3 = c5;
								num4 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
								int num6 = (int)separatorChar.Value;
								if (!((num4.GetValueOrDefault() == num6) & (num4 != null)))
								{
									return false;
								}
							}
							c3 = c5;
							num4 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
							c3 = c;
							num7 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
							if ((num4.GetValueOrDefault() == num7.GetValueOrDefault()) & (num4 != null == (num7 != null)))
							{
								return false;
							}
							separatorChar = c5;
							goto IL_0750;
						}
					}
					return false;
					IL_0750:
					if (c != null)
					{
						int num8 = text2.IndexOf(c.Value);
						if (num8 < 0)
						{
							numberParts2.IntegerPart = text2;
						}
						else
						{
							numberParts2.IntegerPart = text2.Substring(0, num8);
							numberParts2.DecimalPart = text2.Substring(num8 + 1);
						}
					}
					else
					{
						numberParts2.IntegerPart = text2;
					}
					numberParts.Add(numberParts2);
					if (!forParsing && numberParts2.DecimalPart != null && !NumberFormatDetails.<LearnOutputFormatDetails>g__IsAsciiSimpleDecimal|51_3(numberParts2.DecimalPart))
					{
						return false;
					}
					if (separatorChar != null)
					{
						string[] array = numberParts2.IntegerPart.TrimStart(new char[] { ' ' }).TrimStart(new char[] { '-', '+' }).Split(new char[] { separatorChar.Value }, StringSplitOptions.None);
						if (array[0].Length != 0)
						{
							if (!array.Skip(1).Any((string s) => s.Length < 2))
							{
								if (forParsing)
								{
									continue;
								}
								Optional<uint[]> optional = NumberFormatDetails.UnifySectionSizes(array.Select((string s) => (uint)s.Length).Reverse<uint>().ToArray<uint>(), separatedSectionSizes);
								if (!optional.HasValue)
								{
									return false;
								}
								separatedSectionSizes = optional.Value;
								if (!array.Any(new Func<string, bool>(NumberFormatDetails.<LearnOutputFormatDetails>g__IsAsciiDigitsString|51_1)))
								{
									return false;
								}
								continue;
							}
						}
						return false;
					}
					if (!forParsing && !NumberFormatDetails.<LearnOutputFormatDetails>g__IsAsciiSimpleInteger|51_2(numberParts2.IntegerPart))
					{
						return false;
					}
				}
			}
			if (alternateDecimalMarkMode && c == null)
			{
				char? c3 = separatorChar;
				int? num7 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
				int num6 = 46;
				if (!((num7.GetValueOrDefault() == num6) & (num7 != null)))
				{
					return false;
				}
			}
			if (c == null)
			{
				char c7;
				if (!alternateDecimalMarkMode)
				{
					char? c3 = separatorChar;
					int? num7 = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
					int num6 = 46;
					if (!((num7.GetValueOrDefault() == num6) & (num7 != null)))
					{
						c7 = '.';
						goto IL_09BA;
					}
				}
				c7 = ',';
				IL_09BA:
				c = new char?(c7);
			}
			separatedSectionSizes = NumberFormatDetails.SummarizeSeparatedSectionSizes(separatedSectionSizes);
			decimalMarkVal = c.Value;
			return true;
		}

		// Token: 0x0600FCF4 RID: 64756 RVA: 0x0036029C File Offset: 0x0035E49C
		private static Optional<uint[]> UnifySectionSizes(uint[] candidateSectionSizes, uint[] separatedSectionSizes)
		{
			if (separatedSectionSizes == null)
			{
				return candidateSectionSizes.Some<uint[]>();
			}
			int num = Math.Min(candidateSectionSizes.Length, separatedSectionSizes.Length) - 1;
			if (!candidateSectionSizes.SequencePrefixEqual(separatedSectionSizes, num, null))
			{
				return Optional<uint[]>.Nothing;
			}
			if (candidateSectionSizes.Length > separatedSectionSizes.Length)
			{
				if (candidateSectionSizes[separatedSectionSizes.Length - 1] < separatedSectionSizes[separatedSectionSizes.Length - 1])
				{
					return Optional<uint[]>.Nothing;
				}
				return candidateSectionSizes.Some<uint[]>();
			}
			else if (candidateSectionSizes.Length < separatedSectionSizes.Length)
			{
				if (candidateSectionSizes[candidateSectionSizes.Length - 1] > separatedSectionSizes[candidateSectionSizes.Length - 1])
				{
					return Optional<uint[]>.Nothing;
				}
				return separatedSectionSizes.Some<uint[]>();
			}
			else
			{
				if (candidateSectionSizes.Last<uint>() <= separatedSectionSizes.Last<uint>())
				{
					return separatedSectionSizes.Some<uint[]>();
				}
				return candidateSectionSizes.Some<uint[]>();
			}
		}

		// Token: 0x0600FCF5 RID: 64757 RVA: 0x00360338 File Offset: 0x0035E538
		private static uint[] SummarizeSeparatedSectionSizes(uint[] separatedSectionSizes)
		{
			if (separatedSectionSizes == null || separatedSectionSizes.Length == 1)
			{
				separatedSectionSizes = null;
			}
			else
			{
				uint num = separatedSectionSizes[separatedSectionSizes.Length - 2];
				uint lastSize = separatedSectionSizes[separatedSectionSizes.Length - 1];
				if (num < lastSize)
				{
					lastSize = (separatedSectionSizes[separatedSectionSizes.Length - 1] = 0U);
				}
				else if (num > lastSize)
				{
					lastSize = (separatedSectionSizes[separatedSectionSizes.Length - 1] = num);
				}
				int num2 = separatedSectionSizes.Reverse<uint>().TakeWhile((uint size) => size == lastSize).Count<uint>();
				if (num2 > 1)
				{
					separatedSectionSizes = separatedSectionSizes.DropLast(num2 - 1).ToArray<uint>();
				}
			}
			return separatedSectionSizes;
		}

		// Token: 0x0600FCF6 RID: 64758 RVA: 0x003603D9 File Offset: 0x0035E5D9
		public NumberFormatDetails With(Optional<char> separatorChar)
		{
			return new NumberFormatDetails(this.DecimalMarkChar, separatorChar, this.SeparatedSectionSizes, this.Scale, this.CurrencySymbol, this.TrailingSign);
		}

		// Token: 0x0600FCF8 RID: 64760 RVA: 0x0036047C File Offset: 0x0035E67C
		[CompilerGenerated]
		internal static bool <LearnOutputFormatDetails>g__IsAsciiDigits|51_0(string str, int start, int end)
		{
			for (int i = start; i < end; i++)
			{
				char c = str[i];
				if (c < '0' || c > '9')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600FCF9 RID: 64761 RVA: 0x003604AA File Offset: 0x0035E6AA
		[CompilerGenerated]
		internal static bool <LearnOutputFormatDetails>g__IsAsciiDigitsString|51_1(string str)
		{
			return NumberFormatDetails.<LearnOutputFormatDetails>g__IsAsciiDigits|51_0(str, 0, str.Length);
		}

		// Token: 0x0600FCFA RID: 64762 RVA: 0x003604BC File Offset: 0x0035E6BC
		[CompilerGenerated]
		internal static bool <LearnOutputFormatDetails>g__IsAsciiSimpleInteger|51_2(string str)
		{
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				if (c != ' ')
				{
					if (c == '-' || c == '+')
					{
						i++;
					}
					return NumberFormatDetails.<LearnOutputFormatDetails>g__IsAsciiDigits|51_0(str, i, str.Length);
				}
			}
			return false;
		}

		// Token: 0x0600FCFB RID: 64763 RVA: 0x00360504 File Offset: 0x0035E704
		[CompilerGenerated]
		internal static bool <LearnOutputFormatDetails>g__IsAsciiSimpleDecimal|51_3(string str)
		{
			for (int i = str.Length - 1; i >= 0; i--)
			{
				if (str[i] != ' ')
				{
					return NumberFormatDetails.<LearnOutputFormatDetails>g__IsAsciiDigits|51_0(str, 0, i + 1);
				}
			}
			return false;
		}

		// Token: 0x04005E68 RID: 24168
		private static readonly Regex NotDigit = new Regex("[^\\p{Nd}]", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x02001D63 RID: 7523
		internal struct NumberParts
		{
			// Token: 0x17002A2A RID: 10794
			// (get) Token: 0x0600FCFC RID: 64764 RVA: 0x0036053B File Offset: 0x0035E73B
			// (set) Token: 0x0600FCFD RID: 64765 RVA: 0x00360543 File Offset: 0x0035E743
			public string IntegerPart { readonly get; set; }

			// Token: 0x17002A2B RID: 10795
			// (get) Token: 0x0600FCFE RID: 64766 RVA: 0x0036054C File Offset: 0x0035E74C
			// (set) Token: 0x0600FCFF RID: 64767 RVA: 0x00360554 File Offset: 0x0035E754
			public string DecimalPart { readonly get; set; }

			// Token: 0x17002A2C RID: 10796
			// (get) Token: 0x0600FD00 RID: 64768 RVA: 0x0036055D File Offset: 0x0035E75D
			// (set) Token: 0x0600FD01 RID: 64769 RVA: 0x00360565 File Offset: 0x0035E765
			public string ExponentPart { readonly get; set; }
		}
	}
}
