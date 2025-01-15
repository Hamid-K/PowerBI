using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers
{
	// Token: 0x02001D67 RID: 7527
	[Parseable("TryParseXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public class NumberFormat : IRenderableLiteral, IEquatable<NumberFormat>
	{
		// Token: 0x0600FD16 RID: 64790 RVA: 0x003606C8 File Offset: 0x0035E8C8
		[JsonConstructor]
		public NumberFormat(Optional<uint> minTrailingZeros = default(Optional<uint>), Optional<uint> maxTrailingZeros = default(Optional<uint>), Optional<uint> minTrailingZerosAndWhitespace = default(Optional<uint>), Optional<uint> minLeadingZeros = default(Optional<uint>), Optional<uint> minLeadingZerosAndWhitespace = default(Optional<uint>), NumberFormatDetails details = null)
		{
			this.MinTrailingZeros = minTrailingZeros;
			this.MaxTrailingZeros = maxTrailingZeros;
			this.MinTrailingZerosAndWhitespace = (minTrailingZerosAndWhitespace.HasValue ? minTrailingZerosAndWhitespace : minTrailingZeros);
			this.MinLeadingZeros = minLeadingZeros;
			this.MinLeadingZerosAndWhitespace = (minLeadingZerosAndWhitespace.HasValue ? minLeadingZerosAndWhitespace : minLeadingZeros);
			this.Details = details ?? NumberFormatDetails.Default;
		}

		// Token: 0x0600FD17 RID: 64791 RVA: 0x0036072C File Offset: 0x0035E92C
		public NumberFormat With(Optional<uint>? minTrailingZeros = null, Optional<uint>? maxTrailingZeros = null, Optional<uint>? minTrailingZerosAndWhitespace = null, Optional<uint>? minLeadingZeros = null, Optional<uint>? minLeadingZerosAndWhitespace = null, NumberFormatDetails details = null)
		{
			return new NumberFormat(minTrailingZeros ?? this.MinTrailingZeros, maxTrailingZeros ?? this.MaxTrailingZeros, minTrailingZerosAndWhitespace ?? this.MinTrailingZerosAndWhitespace, minLeadingZeros ?? this.MinLeadingZeros, minLeadingZerosAndWhitespace ?? this.MinLeadingZerosAndWhitespace, details ?? this.Details);
		}

		// Token: 0x17002A2D RID: 10797
		// (get) Token: 0x0600FD18 RID: 64792 RVA: 0x003607CE File Offset: 0x0035E9CE
		public Optional<uint> MinTrailingZeros { get; }

		// Token: 0x17002A2E RID: 10798
		// (get) Token: 0x0600FD19 RID: 64793 RVA: 0x003607D6 File Offset: 0x0035E9D6
		public Optional<uint> MaxTrailingZeros { get; }

		// Token: 0x17002A2F RID: 10799
		// (get) Token: 0x0600FD1A RID: 64794 RVA: 0x003607DE File Offset: 0x0035E9DE
		public Optional<uint> MinTrailingZerosAndWhitespace { get; }

		// Token: 0x17002A30 RID: 10800
		// (get) Token: 0x0600FD1B RID: 64795 RVA: 0x003607E6 File Offset: 0x0035E9E6
		public Optional<uint> MinLeadingZeros { get; }

		// Token: 0x17002A31 RID: 10801
		// (get) Token: 0x0600FD1C RID: 64796 RVA: 0x003607EE File Offset: 0x0035E9EE
		public Optional<uint> MinLeadingZerosAndWhitespace { get; }

		// Token: 0x17002A32 RID: 10802
		// (get) Token: 0x0600FD1D RID: 64797 RVA: 0x003607F6 File Offset: 0x0035E9F6
		public NumberFormatDetails Details { get; }

		// Token: 0x17002A33 RID: 10803
		// (get) Token: 0x0600FD1E RID: 64798 RVA: 0x003607FE File Offset: 0x0035E9FE
		public Optional<char> SeparatorChar
		{
			get
			{
				return this.Details.SeparatorChar;
			}
		}

		// Token: 0x17002A34 RID: 10804
		// (get) Token: 0x0600FD1F RID: 64799 RVA: 0x0036080B File Offset: 0x0035EA0B
		public Optional<uint[]> SeparatedSectionSizes
		{
			get
			{
				return this.Details.SeparatedSectionSizes;
			}
		}

		// Token: 0x17002A35 RID: 10805
		// (get) Token: 0x0600FD20 RID: 64800 RVA: 0x00360818 File Offset: 0x0035EA18
		public Optional<decimal> Scale
		{
			get
			{
				return this.Details.Scale;
			}
		}

		// Token: 0x0600FD21 RID: 64801 RVA: 0x00360828 File Offset: 0x0035EA28
		public bool Equals(NumberFormat other)
		{
			return other != null && (this == other || (this.MinTrailingZeros == other.MinTrailingZeros && this.MaxTrailingZeros == other.MaxTrailingZeros && this.MinTrailingZerosAndWhitespace == other.MinTrailingZerosAndWhitespace && this.MinLeadingZeros == other.MinLeadingZeros && this.MinLeadingZerosAndWhitespace == other.MinLeadingZerosAndWhitespace && this.Details.Equals(other.Details)));
		}

		// Token: 0x0600FD22 RID: 64802 RVA: 0x003608B4 File Offset: 0x0035EAB4
		public string RenderHumanReadable()
		{
			string text = this.Details.RenderHumanReadable();
			string text2 = text.Substring(1, text.Length - 2);
			return string.Concat(new string[]
			{
				"\"(",
				NumberFormat.ToStringOrEmpty<uint>(this.MinTrailingZeros),
				", ",
				NumberFormat.ToStringOrEmpty<uint>(this.MaxTrailingZeros),
				", ",
				NumberFormat.ToStringOrEmpty<uint>(this.MinTrailingZerosAndWhitespace),
				", ",
				NumberFormat.ToStringOrEmpty<uint>(this.MinLeadingZeros),
				", ",
				NumberFormat.ToStringOrEmpty<uint>(this.MinLeadingZerosAndWhitespace),
				", ",
				text2,
				")\""
			});
		}

		// Token: 0x0600FD23 RID: 64803 RVA: 0x00360970 File Offset: 0x0035EB70
		public XElement RenderXML()
		{
			return new XElement("NumberFormat", new object[]
			{
				new XAttribute("MinTrailingZeros", NumberFormat.ToStringOrEmpty<uint>(this.MinTrailingZeros)),
				new XAttribute("MaxTrailingZeros", NumberFormat.ToStringOrEmpty<uint>(this.MaxTrailingZeros)),
				new XAttribute("MinTrailingZerosAndWhitespace", NumberFormat.ToStringOrEmpty<uint>(this.MinTrailingZerosAndWhitespace)),
				new XAttribute("MinLeadingZeros", NumberFormat.ToStringOrEmpty<uint>(this.MinLeadingZeros)),
				new XAttribute("MinLeadingZerosAndWhitespace", NumberFormat.ToStringOrEmpty<uint>(this.MinLeadingZerosAndWhitespace)),
				this.Details.RenderXML()
			});
		}

		// Token: 0x0600FD24 RID: 64804 RVA: 0x00360A34 File Offset: 0x0035EC34
		private static string ToStringOrEmpty<T>(Optional<T> optional)
		{
			if (!optional.HasValue)
			{
				return string.Empty;
			}
			T value = optional.Value;
			return value.ToString();
		}

		// Token: 0x0600FD25 RID: 64805 RVA: 0x00360A65 File Offset: 0x0035EC65
		private static Optional<decimal> ReadOptionalDecimalAttribute(XAttribute attribute)
		{
			if (attribute.Value.Length != 0)
			{
				return decimal.Parse(attribute.Value, CultureInfo.InvariantCulture).Some<decimal>();
			}
			return Optional<decimal>.Nothing;
		}

		// Token: 0x0600FD26 RID: 64806 RVA: 0x00360A8F File Offset: 0x0035EC8F
		private static Optional<uint> ReadOptionalUintAttribute(XAttribute attribute)
		{
			if (attribute.Value.Length != 0)
			{
				return uint.Parse(attribute.Value, CultureInfo.InvariantCulture).Some<uint>();
			}
			return Optional<uint>.Nothing;
		}

		// Token: 0x0600FD27 RID: 64807 RVA: 0x00360AB9 File Offset: 0x0035ECB9
		private static Optional<char> ReadOptionalCharAttribute(XAttribute attribute)
		{
			if (attribute.Value.Length != 0)
			{
				return char.Parse(attribute.Value).Some<char>();
			}
			return Optional<char>.Nothing;
		}

		// Token: 0x0600FD28 RID: 64808 RVA: 0x00360AE0 File Offset: 0x0035ECE0
		internal static Optional<NumberFormat> TryParseXML(XElement literal)
		{
			Optional<NumberFormat> optional;
			try
			{
				if (literal.Name != "NumberFormat")
				{
					optional = Optional<NumberFormat>.Nothing;
				}
				else
				{
					XElement xelement = literal.Element("NumberFormatDetails");
					if (xelement == null)
					{
						optional = Optional<NumberFormat>.Nothing;
					}
					else
					{
						if (literal.Elements().Count<XElement>() > 1)
						{
							throw new NotImplementedException("Unsupported NumberFormat XML: " + ((literal != null) ? literal.ToString() : null));
						}
						Optional<NumberFormatDetails> optional2 = NumberFormatDetails.TryParseXML(xelement);
						if (!optional2.HasValue)
						{
							optional = Optional<NumberFormat>.Nothing;
						}
						else
						{
							optional = new NumberFormat(NumberFormat.ReadOptionalUintAttribute(literal.Attribute("MinTrailingZeros")), NumberFormat.ReadOptionalUintAttribute(literal.Attribute("MaxTrailingZeros")), NumberFormat.ReadOptionalUintAttribute(literal.Attribute("MinTrailingZerosAndWhitespace")), NumberFormat.ReadOptionalUintAttribute(literal.Attribute("MinLeadingZeros")), NumberFormat.ReadOptionalUintAttribute(literal.Attribute("MinLeadingZerosAndWhitespace")), optional2.Value).Some<NumberFormat>();
						}
					}
				}
			}
			catch
			{
				optional = Optional<NumberFormat>.Nothing;
			}
			return optional;
		}

		// Token: 0x0600FD29 RID: 64809 RVA: 0x00360C18 File Offset: 0x0035EE18
		internal static NumberFormat TryParseHumanReadable(string literal)
		{
			if (literal == "default")
			{
				return new NumberFormat(Optional<uint>.Nothing, Optional<uint>.Nothing, Optional<uint>.Nothing, Optional<uint>.Nothing, Optional<uint>.Nothing, NumberFormatDetails.Default);
			}
			if (!literal.StartsWith("\"("))
			{
				throw new NotImplementedException("Unsupported NumberFormat literal: " + literal);
			}
			literal = literal.Substring(2, literal.Length - 4);
			Optional<uint>[] array = new Optional<uint>[5];
			for (int i = 0; i < array.Length; i++)
			{
				int num = literal.IndexOf(", ", StringComparison.Ordinal);
				string text = literal.Substring(0, num);
				literal = literal.Substring(num + 2);
				array[i] = ((text.Length == 0) ? Optional<uint>.Nothing : uint.Parse(text).Some<uint>());
			}
			Optional<NumberFormatDetails> optional = NumberFormatDetails.TryParseHumanReadable("\"" + literal + "\"");
			return new NumberFormat(array[0], array[1], array[2], array[3], array[4], optional.Value);
		}

		// Token: 0x0600FD2A RID: 64810 RVA: 0x00360D24 File Offset: 0x0035EF24
		private static decimal Rescale(decimal number, decimal scale)
		{
			if (scale < 1m)
			{
				return number * scale;
			}
			return number / (1m / scale);
		}

		// Token: 0x0600FD2B RID: 64811 RVA: 0x00360D4C File Offset: 0x0035EF4C
		public string ToString(decimal value)
		{
			if (this.Scale.HasValue)
			{
				value = NumberFormat.Rescale(value, this.Scale.Value);
			}
			if (this.MaxTrailingZeros.HasValue)
			{
				value = Math.Round(value, (int)this.MaxTrailingZeros.Value, MidpointRounding.AwayFromZero);
			}
			bool flag = value < 0m;
			decimal num = Math.Abs(value);
			decimal num2 = Math.Abs(Math.Truncate(num));
			decimal num3 = Math.Abs(num - num2);
			string text = num2.ToString(CultureInfo.InvariantCulture);
			if (this.SeparatorChar.HasValue && this.SeparatedSectionSizes.HasValue && (long)text.Length > (long)((ulong)this.SeparatedSectionSizes.Value[0]))
			{
				LinkedList<string> linkedList = new LinkedList<string>();
				int num4 = text.Length;
				foreach (int num5 in this.SeparatedSectionSizes.Value.Select((uint s) => (int)s).Concat(EnumerableEx.Repeat<int>((int)this.SeparatedSectionSizes.Value.Last<uint>())))
				{
					if (num5 == 0 || num4 < num5)
					{
						if (num4 > 0)
						{
							linkedList.AddFirst(text.Substring(0, num4));
							break;
						}
						break;
					}
					else
					{
						linkedList.AddFirst(text.Substring(num4 - num5, num5));
						num4 -= num5;
					}
				}
				text = string.Join(this.SeparatorChar.Value.ToString(), linkedList);
			}
			text = text.PadLeft((int)this.MinLeadingZeros.OrElse(0U), '0');
			if (flag)
			{
				text = "-" + text;
			}
			text = text.PadLeft((int)this.MinLeadingZerosAndWhitespace.OrElse(0U), ' ');
			string text2;
			if (!this.MinTrailingZerosAndWhitespace.HasValue && num3 == 0m && !num3.ToString(CultureInfo.InvariantCulture).Contains('.'))
			{
				text2 = "";
			}
			else
			{
				text2 = this.Details.DecimalMarkChar.ToString() + num3.ToString(CultureInfo.InvariantCulture).TrimStart(new char[] { '0' }).TrimStart(new char[] { '.' })
					.PadRight((int)this.MinTrailingZeros.OrElse(0U), '0')
					.PadRight((int)this.MinTrailingZerosAndWhitespace.OrElse(0U), ' ');
			}
			return text + text2;
		}

		// Token: 0x0600FD2C RID: 64812 RVA: 0x00361008 File Offset: 0x0035F208
		private static ProgramSetBuilder<TNode> SingletonLiteralProgramSet<TNode, TValue>(GrammarBuilders build, Func<TValue?, TNode> func, Optional<TValue> literalValue) where TNode : IProgramNodeBuilder where TValue : struct
		{
			return ProgramSetBuilder.List<TNode>(new TNode[] { func(literalValue.HasValue ? new TValue?(literalValue.Value) : null) });
		}

		// Token: 0x0600FD2D RID: 64813 RVA: 0x0036104D File Offset: 0x0035F24D
		private static ProgramSetBuilder<TNode> LiteralProgramSet<TNode, TValue>(GrammarBuilders build, Func<TValue?, TNode> func, IEnumerable<TValue?> literalValues) where TNode : IProgramNodeBuilder where TValue : struct
		{
			return ProgramSetBuilder.List<TNode>(literalValues.Select(func).ToArray<TNode>());
		}

		// Token: 0x0600FD2E RID: 64814 RVA: 0x00361060 File Offset: 0x0035F260
		private static string GetDecimalPartString(decimal number)
		{
			number = Math.Abs(number);
			string[] array = (number - Math.Truncate(number)).ToString(CultureInfo.InvariantCulture).Split(new char[] { '.' });
			if (array.Length != 2)
			{
				return null;
			}
			return array[1];
		}

		// Token: 0x0600FD2F RID: 64815 RVA: 0x003610AC File Offset: 0x0035F2AC
		internal static bool IsValidNumberContext(ValueSubstring numberString)
		{
			return (numberString.Start <= 0U || !char.IsDigit(numberString.Source[(int)numberString.Start]) || !char.IsDigit(numberString.Source[(int)(numberString.Start - 1U)])) && ((ulong)numberString.End >= (ulong)((long)numberString.Source.Length) || !char.IsDigit(numberString.Source[(int)(numberString.End - 1U)]) || !char.IsDigit(numberString.Source[(int)numberString.End]));
		}

		// Token: 0x0600FD30 RID: 64816 RVA: 0x00361148 File Offset: 0x0035F348
		internal static decimal? ParseNumber(ValueSubstring numberString, bool alternateDecimalMarkMode)
		{
			if (NumberFormat.IsValidNumberContext(numberString))
			{
				return NumberFormat.ParseNumber(numberString.Value, alternateDecimalMarkMode);
			}
			return null;
		}

		// Token: 0x0600FD31 RID: 64817 RVA: 0x00361174 File Offset: 0x0035F374
		private static decimal? ParseNumber(string numberString, bool alternateDecimalMarkMode)
		{
			if (NumberFormat.NonNumberChar.IsMatch(numberString))
			{
				return null;
			}
			return Semantics.ParseNumber(ValueSubstring.Create(NumberFormat.NonNumberCharOrSpace.Replace(numberString, ""), null, null, null, null), alternateDecimalMarkMode ? NumberFormatDetails.Alternate : NumberFormatDetails.Default);
		}

		// Token: 0x0600FD32 RID: 64818 RVA: 0x003611D8 File Offset: 0x0035F3D8
		private static decimal? ParseNumber(string numberString, char? separatorChar, char decimalMark)
		{
			string text = numberString;
			if (separatorChar != null)
			{
				text = text.Replace(separatorChar.Value.ToString(), "");
			}
			if (NumberFormat.NonNumberChar.IsMatch(text))
			{
				return null;
			}
			return Semantics.ParseNumber(ValueSubstring.Create(NumberFormat.NonNumberCharOrSpace.Replace(text, ""), null, null, null, null), new NumberFormatDetails(decimalMark, default(Optional<char>), default(Optional<uint[]>), default(Optional<decimal>), default(Optional<string>), true));
		}

		// Token: 0x0600FD33 RID: 64819 RVA: 0x0036127F File Offset: 0x0035F47F
		public static bool IsValidNumberString(ValueSubstring numberString)
		{
			return NumberFormat.IsValidNumberContext(numberString) && NumberFormat.IsValidNumberString(numberString.Value);
		}

		// Token: 0x0600FD34 RID: 64820 RVA: 0x00361296 File Offset: 0x0035F496
		public static bool IsValidNumberString(string numberString)
		{
			return Semantics.GetStaticTokenByName("GeneralNumber").MatchesEntireString(numberString.Trim()) && NumberFormatDetails.Learn(new string[] { numberString }, true) != null;
		}

		// Token: 0x0600FD35 RID: 64821 RVA: 0x003612C4 File Offset: 0x0035F4C4
		internal static bool IsAllowedScalePower(int power)
		{
			return power == -2 || power == 2 || (power % 3 == 0 && power != 0);
		}

		// Token: 0x0600FD36 RID: 64822 RVA: 0x003612DC File Offset: 0x0035F4DC
		public static ProgramSetBuilder<numberFormat> Learn(GrammarBuilders build, IEnumerable<Record<ValueSubstring, decimal>> toFormatPairs, bool maxDecimalDigitsRequired)
		{
			IReadOnlyList<Record<ValueSubstring, decimal>> toFormatPairsList = (toFormatPairs as IReadOnlyList<Record<ValueSubstring, decimal>>) ?? toFormatPairs.ToList<Record<ValueSubstring, decimal>>();
			if (toFormatPairsList.Any((Record<ValueSubstring, decimal> toParse) => !NumberFormat.IsValidNumberContext(toParse.Item1)))
			{
				return null;
			}
			return new bool[]
			{
				default(bool),
				true
			}.Select((bool alternateDecimalMarkMode) => NumberFormat._Learn(build, toFormatPairsList, maxDecimalDigitsRequired, alternateDecimalMarkMode)).NormalizedUnion<numberFormat>();
		}

		// Token: 0x0600FD37 RID: 64823 RVA: 0x00361364 File Offset: 0x0035F564
		private static ProgramSetBuilder<numberFormat> _Learn(GrammarBuilders build, IReadOnlyList<Record<ValueSubstring, decimal>> toFormatPairs, bool maxDecimalDigitsRequired, bool alternateDecimalMarkMode)
		{
			if (!toFormatPairs.Any<Record<ValueSubstring, decimal>>())
			{
				return null;
			}
			if (toFormatPairs.Any((Record<ValueSubstring, decimal> t) => t.Item1.Length == 0U))
			{
				return null;
			}
			List<NumberFormatDetails.NumberParts> list;
			char c;
			bool flag;
			char? c2;
			uint[] array;
			if (!NumberFormatDetails.LearnOutputFormatDetails(toFormatPairs.Select((Record<ValueSubstring, decimal> p) => p.Item1.Value), toFormatPairs.Count, alternateDecimalMarkMode, false, false, false, out list, out c, out flag, out c2, out array))
			{
				return null;
			}
			Record<decimal?, uint?>? record = NumberFormat.HandleMismatches(toFormatPairs, c2, c);
			if (record == null)
			{
				return null;
			}
			decimal? item = record.Value.Item1;
			uint? item2 = record.Value.Item2;
			IReadOnlyList<string> readOnlyList = list.Select((NumberFormatDetails.NumberParts s) => NumberFormat.NotDigitMinusOrSpace.Replace(s.IntegerPart, "")).ToList<string>();
			IEnumerable<string> enumerable = readOnlyList;
			Func<string, bool> func;
			if ((func = NumberFormat.<>O.<0>__IsNullOrWhiteSpace) == null)
			{
				func = (NumberFormat.<>O.<0>__IsNullOrWhiteSpace = new Func<string, bool>(string.IsNullOrWhiteSpace));
			}
			if (!enumerable.Any(func))
			{
				if (!readOnlyList.Any((string intStr) => !char.IsDigit(intStr.Last<char>())))
				{
					List<int> whitespacePaddedLengths = (from intStr in readOnlyList
						where intStr[0] == ' '
						select intStr.Length).Distinct<int>().ToList<int>();
					if (whitespacePaddedLengths.Count > 1 || (whitespacePaddedLengths.Count == 1 && readOnlyList.Any((string intStr) => intStr.Length < whitespacePaddedLengths.Single<int>())))
					{
						return null;
					}
					uint? num = whitespacePaddedLengths.Select((int l) => new uint?((uint)l)).SingleOrDefault<uint?>();
					List<string> list2 = readOnlyList.Select((string intStr) => intStr.TrimStart(new char[] { ' ' }).TrimStart(new char[] { '-' })).ToList<string>();
					List<int> zeroPaddedLengths = (from intStr in list2
						where intStr[0] == '0'
						select intStr.Length).Distinct<int>().ToList<int>();
					if (zeroPaddedLengths.Count > 1 || (zeroPaddedLengths.Count == 1 && list2.Any((string intStr) => intStr.Length < zeroPaddedLengths.Single<int>())))
					{
						return null;
					}
					uint? num2 = zeroPaddedLengths.Select((int l) => new uint?((uint)l)).SingleOrDefault<uint?>();
					IEnumerable<uint?> enumerable2;
					if (num == null)
					{
						enumerable2 = Seq.Of<uint?>(new uint?[1]).Concat(from l in Enumerable.Range(1, readOnlyList.Min((string intStr) => intStr.Length))
							select new uint?((uint)l));
					}
					else
					{
						enumerable2 = Seq.Of<uint?>(new uint?[] { num });
					}
					IEnumerable<uint?> enumerable3 = enumerable2;
					IEnumerable<uint?> enumerable4;
					if (num2 == null)
					{
						enumerable4 = Seq.Of<uint?>(new uint?[1]).Concat(from l in Enumerable.Range(1, list2.Min((string intStr) => intStr.Length))
							select new uint?((uint)l));
					}
					else
					{
						enumerable4 = Seq.Of<uint?>(new uint?[] { num2 });
					}
					IEnumerable<uint?> enumerable5 = enumerable4;
					NumberFormatDetails numberFormatDetails = new NumberFormatDetails(c, c2, array, item, flag);
					if (list.Any((NumberFormatDetails.NumberParts s) => s.DecimalPart == null))
					{
						if (maxDecimalDigitsRequired && item2 == null)
						{
							item2 = new uint?(0U);
						}
						return build.Set.Join.BuildNumberFormat(ProgramSetBuilder.List<minTrailingZeros>(new minTrailingZeros[] { build.Node.Rule.minTrailingZeros(null) }), ProgramSetBuilder.List<maxTrailingZeros>(new maxTrailingZeros[] { build.Node.Rule.maxTrailingZeros(item2) }), ProgramSetBuilder.List<minTrailingZerosAndWhitespace>(new minTrailingZerosAndWhitespace[] { build.Node.Rule.minTrailingZerosAndWhitespace(null) }), NumberFormat.LiteralProgramSet<minLeadingZeros, uint>(build, new Func<uint?, minLeadingZeros>(build.Node.Rule.minLeadingZeros), enumerable5), NumberFormat.LiteralProgramSet<minLeadingZerosAndWhitespace, uint>(build, new Func<uint?, minLeadingZerosAndWhitespace>(build.Node.Rule.minLeadingZerosAndWhitespace), enumerable3), ProgramSetBuilder.List<numberFormatDetails>(new numberFormatDetails[] { build.Node.Rule.numberFormatDetails(numberFormatDetails) }));
					}
					if (list.Any((NumberFormatDetails.NumberParts s) => NumberFormat.NotDigitOrSpace.IsMatch(s.DecimalPart)))
					{
						return null;
					}
					List<string> list3 = list.Select((NumberFormatDetails.NumberParts s) => NumberFormat.NotDigitOrSpace.Replace(s.DecimalPart, "")).ToList<string>();
					int num3 = list3.Max((string decStr) => decStr.TrimEnd(new char[] { ' ' }).Length);
					if (item2 != null && (long)num3 != (long)((ulong)item2.Value))
					{
						return null;
					}
					List<int> whitespacePaddedDecLengths = (from decStr in list3
						where decStr.Length > 0 && decStr.Last<char>() == ' '
						select decStr.Length).Distinct<int>().ToList<int>();
					if (whitespacePaddedDecLengths.Count > 1 || (whitespacePaddedDecLengths.Count == 1 && list3.Any((string decStr) => decStr.Length < whitespacePaddedDecLengths.Single<int>())))
					{
						return null;
					}
					uint? num4 = whitespacePaddedDecLengths.Select((int l) => new uint?((uint)l)).SingleOrDefault<uint?>();
					List<string> list4;
					if (num4 == null)
					{
						list4 = list3;
					}
					else
					{
						list4 = list3.Select((string decStr) => decStr.TrimEnd(new char[] { ' ' })).ToList<string>();
					}
					List<string> list5 = list4;
					List<int> zeroPaddedDecLengths = (from decStr in list5
						where decStr.Length > 0 && decStr.Last<char>() == '0'
						select decStr.Length).Distinct<int>().ToList<int>();
					if (zeroPaddedDecLengths.Count > 1 || (zeroPaddedDecLengths.Count == 1 && list5.Any((string decStr) => decStr.Length < zeroPaddedDecLengths.Single<int>())))
					{
						return null;
					}
					uint? num5 = zeroPaddedDecLengths.Select((int l) => new uint?((uint)l)).SingleOrDefault<uint?>();
					uint? num6;
					if (num5 == null)
					{
						num6 = (list5.Any((string decStr) => decStr.Length == 0) ? new uint?(0U) : null);
					}
					else
					{
						num6 = num5;
					}
					uint? num7 = num6;
					IEnumerable<uint?> enumerable6;
					if (num4 == null)
					{
						enumerable6 = Seq.Of<uint?>(new uint?[1]).Concat(from l in Enumerable.Range(0, 1 + list3.Min((string decStr) => decStr.Length))
							select new uint?((uint)l));
					}
					else
					{
						enumerable6 = Seq.Of<uint?>(new uint?[] { num4 });
					}
					IEnumerable<uint?> enumerable7 = enumerable6;
					IEnumerable<uint?> enumerable8;
					if (num7 == null)
					{
						enumerable8 = Seq.Of<uint?>(new uint?[1]).Concat(from l in Enumerable.Range(0, 1 + list5.Min((string decStr) => decStr.Length))
							select new uint?((uint)l));
					}
					else
					{
						enumerable8 = Seq.Of<uint?>(new uint?[] { num7 });
					}
					IEnumerable<uint?> enumerable9 = enumerable8;
					uint?[] array2;
					if (item2 == null)
					{
						(array2 = new uint?[2])[1] = new uint?((uint)num3);
					}
					else
					{
						(array2 = new uint?[1])[0] = new uint?((uint)num3);
					}
					uint?[] array3 = array2;
					if (maxDecimalDigitsRequired)
					{
						array3 = array3.Where((uint? z) => z != null).ToArray<uint?>();
					}
					return build.Set.Join.BuildNumberFormat(NumberFormat.LiteralProgramSet<minTrailingZeros, uint>(build, new Func<uint?, minTrailingZeros>(build.Node.Rule.minTrailingZeros), enumerable9), NumberFormat.LiteralProgramSet<maxTrailingZeros, uint>(build, new Func<uint?, maxTrailingZeros>(build.Node.Rule.maxTrailingZeros), array3), NumberFormat.LiteralProgramSet<minTrailingZerosAndWhitespace, uint>(build, new Func<uint?, minTrailingZerosAndWhitespace>(build.Node.Rule.minTrailingZerosAndWhitespace), enumerable7), NumberFormat.LiteralProgramSet<minLeadingZeros, uint>(build, new Func<uint?, minLeadingZeros>(build.Node.Rule.minLeadingZeros), enumerable5), NumberFormat.LiteralProgramSet<minLeadingZerosAndWhitespace, uint>(build, new Func<uint?, minLeadingZerosAndWhitespace>(build.Node.Rule.minLeadingZerosAndWhitespace), enumerable3), ProgramSetBuilder.List<numberFormatDetails>(new numberFormatDetails[] { build.Node.Rule.numberFormatDetails(numberFormatDetails) }));
				}
			}
			return null;
		}

		// Token: 0x0600FD38 RID: 64824 RVA: 0x00361D48 File Offset: 0x0035FF48
		private static Record<decimal?, uint?>? HandleMismatches(IReadOnlyList<Record<ValueSubstring, decimal>> toFormatPairs, char? separatorChar, char decimalMark)
		{
			IList<decimal> list = new List<decimal>(toFormatPairs.Count);
			foreach (Record<ValueSubstring, decimal> record in toFormatPairs)
			{
				decimal? num = NumberFormat.ParseNumber(record.Item1.Value, separatorChar, decimalMark);
				if (num == null)
				{
					return null;
				}
				list.Add(num.Value);
			}
			var list2 = toFormatPairs.Zip(list, (Record<ValueSubstring, decimal> t, decimal parsedFormatted) => new
			{
				toFormat = t.Item2,
				parsedFormatted = parsedFormatted
			}).ToList();
			bool flag = false;
			decimal? scaleUsed = null;
			uint? roundingPlaces = null;
			foreach (var <>f__AnonymousType in list2.Where(t => t.toFormat != t.parsedFormatted))
			{
				flag = true;
				decimal num2 = <>f__AnonymousType.toFormat;
				if (scaleUsed == null && num2 != 0m && <>f__AnonymousType.parsedFormatted != num2)
				{
					int num3 = (int)Math.Round(Math.Log10((double)(<>f__AnonymousType.parsedFormatted / num2)));
					if (NumberFormat.IsAllowedScalePower(num3))
					{
						decimal num4 = (decimal)Math.Pow(10.0, (double)num3);
						scaleUsed = new decimal?(num4);
					}
				}
				if (scaleUsed != null)
				{
					num2 = NumberFormat.Rescale(num2, scaleUsed.Value);
				}
				if (roundingPlaces == null)
				{
					decimal num5 = Math.Abs(num2 - <>f__AnonymousType.parsedFormatted);
					if (num5 != 0m && num5 <= Math.Min(Math.Abs(num2), Math.Abs(<>f__AnonymousType.parsedFormatted)))
					{
						int num6 = (int)Math.Truncate(-Math.Log10((double)num5));
						if (num6 < 0)
						{
							return null;
						}
						roundingPlaces = new uint?((uint)num6);
					}
				}
				if (roundingPlaces != null)
				{
					num2 = Math.Round(num2, (int)roundingPlaces.Value, MidpointRounding.AwayFromZero);
				}
				if (num2 != <>f__AnonymousType.parsedFormatted)
				{
					return null;
				}
			}
			if (roundingPlaces == null)
			{
				roundingPlaces = (from t in list2.Select(delegate(t)
					{
						string decimalPartString = NumberFormat.GetDecimalPartString((scaleUsed != null) ? NumberFormat.Rescale(t.toFormat, scaleUsed.Value) : t.toFormat);
						int num8 = ((decimalPartString != null) ? decimalPartString.Length : 0);
						string decimalPartString2 = NumberFormat.GetDecimalPartString(t.parsedFormatted);
						return new
						{
							toFormatDecLength = num8,
							formattedDecLength = ((decimalPartString2 != null) ? decimalPartString2.Length : 0)
						};
					})
					where t.toFormatDecLength > t.formattedDecLength
					select new uint?((uint)t.formattedDecLength)).FirstOrDefault<uint?>();
				if (roundingPlaces != null)
				{
					uint? roundingPlaces2 = roundingPlaces;
					uint num7 = 28U;
					if ((roundingPlaces2.GetValueOrDefault() > num7) & (roundingPlaces2 != null))
					{
						roundingPlaces = null;
					}
				}
			}
			if (flag)
			{
				if (roundingPlaces == null && scaleUsed == null)
				{
					return null;
				}
				if (!list2.All(delegate(pair)
				{
					decimal num9 = pair.toFormat;
					if (scaleUsed != null)
					{
						num9 = NumberFormat.Rescale(num9, scaleUsed.Value);
					}
					if (roundingPlaces != null)
					{
						num9 = Math.Round(num9, (int)roundingPlaces.Value, MidpointRounding.AwayFromZero);
					}
					return num9 == pair.parsedFormatted;
				}))
				{
					return null;
				}
			}
			return new Record<decimal?, uint?>?(Record.Create<decimal?, uint?>(scaleUsed, roundingPlaces));
		}

		// Token: 0x0600FD39 RID: 64825 RVA: 0x0036211C File Offset: 0x0036031C
		internal ProgramSetBuilder<numberFormat> AsProgramSet(GrammarBuilders build)
		{
			return build.Set.Join.BuildNumberFormat(NumberFormat.SingletonLiteralProgramSet<minTrailingZeros, uint>(build, new Func<uint?, minTrailingZeros>(build.Node.Rule.minTrailingZeros), this.MinTrailingZeros), NumberFormat.SingletonLiteralProgramSet<maxTrailingZeros, uint>(build, new Func<uint?, maxTrailingZeros>(build.Node.Rule.maxTrailingZeros), this.MaxTrailingZeros), NumberFormat.SingletonLiteralProgramSet<minTrailingZerosAndWhitespace, uint>(build, new Func<uint?, minTrailingZerosAndWhitespace>(build.Node.Rule.minTrailingZerosAndWhitespace), this.MinTrailingZerosAndWhitespace), NumberFormat.SingletonLiteralProgramSet<minLeadingZeros, uint>(build, new Func<uint?, minLeadingZeros>(build.Node.Rule.minLeadingZeros), this.MinLeadingZeros), NumberFormat.SingletonLiteralProgramSet<minLeadingZerosAndWhitespace, uint>(build, new Func<uint?, minLeadingZerosAndWhitespace>(build.Node.Rule.minLeadingZerosAndWhitespace), this.MinLeadingZerosAndWhitespace), ProgramSetBuilder.List<numberFormatDetails>(new numberFormatDetails[] { build.Node.Rule.numberFormatDetails(this.Details) }));
		}

		// Token: 0x0600FD3A RID: 64826 RVA: 0x0036220B File Offset: 0x0036040B
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((NumberFormat)obj)));
		}

		// Token: 0x0600FD3B RID: 64827 RVA: 0x0036223C File Offset: 0x0036043C
		private int ComputeHashCode()
		{
			return (((((((((this.MinTrailingZeros.GetHashCode() * 397) ^ this.MaxTrailingZeros.GetHashCode()) * 397) ^ this.MinTrailingZerosAndWhitespace.GetHashCode()) * 397) ^ this.MinLeadingZeros.GetHashCode()) * 397) ^ this.MinLeadingZerosAndWhitespace.GetHashCode()) * 397) ^ this.Details.GetHashCode();
		}

		// Token: 0x0600FD3C RID: 64828 RVA: 0x003622DB File Offset: 0x003604DB
		public override int GetHashCode()
		{
			if (this._hashCode == null)
			{
				this._hashCode = new int?(this.ComputeHashCode());
			}
			return this._hashCode.Value;
		}

		// Token: 0x0600FD3D RID: 64829 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(NumberFormat left, NumberFormat right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600FD3E RID: 64830 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(NumberFormat left, NumberFormat right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x0600FD3F RID: 64831 RVA: 0x00362308 File Offset: 0x00360508
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("MinTrailingZeros: {0}, MaxTrailingZeros: {1}, MinTrailingZerosAndWhitespace: {2}, MinLeadingZeros: {3}, MinLeadingZerosAndWhitespace: {4}, SeparatorChar: {5}, SeparatedSectionSizes: {6}", new object[] { this.MinTrailingZeros, this.MaxTrailingZeros, this.MinTrailingZerosAndWhitespace, this.MinLeadingZeros, this.MinLeadingZerosAndWhitespace, this.SeparatorChar, this.SeparatedSectionSizes }));
		}

		// Token: 0x04005E7D RID: 24189
		private static readonly Regex NotDigitMinusOrSpace = new Regex("[^- \\p{Nd}]", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04005E7E RID: 24190
		private static readonly Regex NonNumberChar = new Regex("[^-+\\p{Zs},.eE\\p{Nd}()·’٫⎖\\p{Pd}\\p{Pc}\\p{Po}]", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04005E7F RID: 24191
		private static readonly Regex NonNumberCharOrSpace = new Regex("[^-+,.eE\\p{Nd}()·’٫⎖]", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04005E80 RID: 24192
		private static readonly Regex NotDigitOrSpace = new Regex("[^ \\p{Nd}]", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04005E81 RID: 24193
		private int? _hashCode;

		// Token: 0x02001D68 RID: 7528
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005E88 RID: 24200
			public static Func<string, bool> <0>__IsNullOrWhiteSpace;
		}
	}
}
