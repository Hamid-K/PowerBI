using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000854 RID: 2132
	[Parseable("TryParseXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public class DateTimeRoundingSpec : Tuple<PartialDateTime, uint, DateTimePart, RoundingMode, DateTimePart?, uint>, IRenderableLiteral
	{
		// Token: 0x06002E4F RID: 11855 RVA: 0x000848BB File Offset: 0x00082ABB
		public DateTimeRoundingSpec(PartialDateTime zero, uint delta, DateTimePart unit, RoundingMode mode, DateTimePart? upperExcludePart = null, uint upperExcludeAmount = 0U)
			: base(zero, delta, unit, mode, upperExcludePart, upperExcludeAmount)
		{
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x000848CC File Offset: 0x00082ACC
		public PartialDateTime Zero
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x000848D4 File Offset: 0x00082AD4
		public uint Delta
		{
			get
			{
				return base.Item2;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002E52 RID: 11858 RVA: 0x000848DC File Offset: 0x00082ADC
		public DateTimePart Unit
		{
			get
			{
				return base.Item3;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x000848E4 File Offset: 0x00082AE4
		public RoundingMode Mode
		{
			get
			{
				return base.Item4;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x000848EC File Offset: 0x00082AEC
		public DateTimePart? UpperExcludePart
		{
			get
			{
				return base.Item5;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000848F4 File Offset: 0x00082AF4
		public uint UpperExcludeAmount
		{
			get
			{
				return base.Item6;
			}
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000848FC File Offset: 0x00082AFC
		public string RenderHumanReadable()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("\"({0}, {1}, {2}, {3}, {4}, {5})\"", new object[]
			{
				this.Zero.RenderHumanReadable(),
				this.Delta,
				this.Unit,
				this.Mode,
				this.UpperExcludePart,
				this.UpperExcludeAmount
			}));
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x00084974 File Offset: 0x00082B74
		public XElement RenderXML()
		{
			XElement xelement = new XElement("DateTimeRoundingSpec", new object[]
			{
				this.Zero.RenderXML(),
				new XAttribute("Delta", this.Delta),
				new XAttribute("Unit", this.Unit),
				new XAttribute("Mode", this.Mode)
			});
			if (this.UpperExcludePart != null)
			{
				xelement.Add(new XAttribute("UpperExcludePart", this.UpperExcludePart.Value));
				xelement.Add(new XAttribute("UpperExcludeAmount", this.UpperExcludeAmount));
			}
			return xelement;
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x00084A58 File Offset: 0x00082C58
		public static Optional<DateTimeRoundingSpec> TryParseXML(XElement literal)
		{
			Optional<DateTimeRoundingSpec> optional;
			try
			{
				if (literal.Name != "DateTimeRoundingSpec")
				{
					optional = Optional<DateTimeRoundingSpec>.Nothing;
				}
				else
				{
					XElement xelement = literal.Element("PartialDateTime");
					if (xelement == null)
					{
						optional = Optional<DateTimeRoundingSpec>.Nothing;
					}
					else
					{
						if (literal.Elements().Count<XElement>() > 1)
						{
							throw new NotImplementedException("Unsupported NumberFormat XML: " + ((literal != null) ? literal.ToString() : null));
						}
						Optional<PartialDateTime> optional2 = PartialDateTime.TryParseXML(xelement);
						if (!optional2.HasValue)
						{
							optional = Optional<DateTimeRoundingSpec>.Nothing;
						}
						else
						{
							PartialDateTime value = optional2.Value;
							uint num = uint.Parse(literal.Attribute("Delta").Value, CultureInfo.InvariantCulture);
							DateTimePart dateTimePart;
							RoundingMode roundingMode;
							if (!Enum.TryParse<DateTimePart>(literal.Attribute("Unit").Value, out dateTimePart))
							{
								optional = Optional<DateTimeRoundingSpec>.Nothing;
							}
							else if (!Enum.TryParse<RoundingMode>(literal.Attribute("Mode").Value, out roundingMode))
							{
								optional = Optional<DateTimeRoundingSpec>.Nothing;
							}
							else
							{
								DateTimePart? dateTimePart2 = null;
								uint num2 = 0U;
								XAttribute xattribute = literal.Attribute("UpperExcludePart");
								if (xattribute != null)
								{
									DateTimePart dateTimePart3;
									if (!Enum.TryParse<DateTimePart>(xattribute.Value, out dateTimePart3))
									{
										return Optional<DateTimeRoundingSpec>.Nothing;
									}
									dateTimePart2 = new DateTimePart?(dateTimePart3);
									num2 = uint.Parse(literal.Attribute("UpperExcludeAmount").Value, CultureInfo.InvariantCulture);
								}
								optional = new DateTimeRoundingSpec(value, num, dateTimePart, roundingMode, dateTimePart2, num2).Some<DateTimeRoundingSpec>();
							}
						}
					}
				}
			}
			catch
			{
				optional = Optional<DateTimeRoundingSpec>.Nothing;
			}
			return optional;
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x00084C10 File Offset: 0x00082E10
		internal static Optional<DateTimeRoundingSpec> TryParseHumanReadable(string literal)
		{
			if (!literal.StartsWith("\"({", StringComparison.Ordinal) || !literal.EndsWith(")\"", StringComparison.Ordinal))
			{
				throw new NotImplementedException("Unsupported DateTimeRoundingSpec literal: " + literal);
			}
			int num = literal.IndexOf('}');
			if (num == -1)
			{
				throw new NotImplementedException("Didn't find } in DateTimeRoundingSpec literal: " + literal);
			}
			Optional<PartialDateTime> optional = PartialDateTime.TryParseHumanReadable(literal.Substring(2, num - 1));
			if (!optional.HasValue)
			{
				return Optional<DateTimeRoundingSpec>.Nothing;
			}
			string[] array = literal.Substring(num + 1, literal.Length - num - 3).Split(new char[] { ',' });
			if (array.Length != 6)
			{
				throw new NotImplementedException("Unsupported DateTimeRoundingSpec literal: " + literal);
			}
			uint num2 = uint.Parse(array[1], CultureInfo.InvariantCulture);
			DateTimePart dateTimePart;
			if (!Enum.TryParse<DateTimePart>(array[2], out dateTimePart))
			{
				return Optional<DateTimeRoundingSpec>.Nothing;
			}
			RoundingMode roundingMode;
			if (!Enum.TryParse<RoundingMode>(array[3], out roundingMode))
			{
				return Optional<DateTimeRoundingSpec>.Nothing;
			}
			DateTimePart? dateTimePart2 = null;
			uint num3 = 0U;
			if (!string.IsNullOrWhiteSpace(array[4]))
			{
				DateTimePart dateTimePart3;
				if (!Enum.TryParse<DateTimePart>(array[4], out dateTimePart3))
				{
					return Optional<DateTimeRoundingSpec>.Nothing;
				}
				dateTimePart2 = new DateTimePart?(dateTimePart3);
				num3 = uint.Parse(array[5], CultureInfo.InvariantCulture);
			}
			return new DateTimeRoundingSpec(optional.Value, num2, dateTimePart, roundingMode, dateTimePart2, num3).Some<DateTimeRoundingSpec>();
		}
	}
}
