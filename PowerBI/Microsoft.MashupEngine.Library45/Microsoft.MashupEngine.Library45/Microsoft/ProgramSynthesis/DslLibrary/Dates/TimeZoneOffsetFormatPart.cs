using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x0200088B RID: 2187
	public class TimeZoneOffsetFormatPart : DateTimeFormatPart
	{
		// Token: 0x06002FDA RID: 12250 RVA: 0x0008D548 File Offset: 0x0008B748
		private TimeZoneOffsetFormatPart(string formatString, int minimumLength, int maximumLength, FormatAttributes attributes)
			: base(formatString, DateTimePart.TimeZoneOffset.Some<DateTimePart>(), default(Optional<Token>), minimumLength, maximumLength, null, null, attributes)
		{
			string text;
			if (!(formatString == "Z"))
			{
				if (!(formatString == "ZZ"))
				{
					throw new NotImplementedException("Unsupported TimeZoneOffset format: " + formatString);
				}
				text = string.Empty;
			}
			else
			{
				text = ":";
			}
			this.Separator = text;
			if (attributes != null && attributes.Attributes.ContainsKey("numericZero"))
			{
				this.ZeroIsZ = false;
				this.AllowNumericZero = true;
				attributes.MarkAttributeAsHandled("numericZero");
			}
			else
			{
				this.ZeroIsZ = true;
			}
			if (attributes != null && attributes.Attributes.ContainsKey("allowNumericZero"))
			{
				this.AllowNumericZero = true;
				attributes.MarkAttributeAsHandled("allowNumericZero");
			}
			if (attributes != null && attributes.HasUnhandledAttributes)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported attributes for format string '{0}': {1}", new object[] { formatString, attributes })), "attributes");
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x0008D650 File Offset: 0x0008B850
		public override bool IsNumericAtEnd
		{
			get
			{
				return this.Separator.Length == 0;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x0008D660 File Offset: 0x0008B860
		public bool AllowNumericZero { get; }

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x0008D668 File Offset: 0x0008B868
		public bool ZeroIsZ { get; }

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x0008D670 File Offset: 0x0008B870
		public string Separator { get; }

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x0008D678 File Offset: 0x0008B878
		public override bool UniqueParse
		{
			get
			{
				return !this.AllowNumericZero || !this.ZeroIsZ;
			}
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x0008D690 File Offset: 0x0008B890
		public static TimeZoneOffsetFormatPart CreateZ(FormatAttributes attributes)
		{
			bool flag = attributes == null || !attributes.Attributes.ContainsKey("numericZero");
			return new TimeZoneOffsetFormatPart("Z", flag ? 1 : 6, 6, attributes);
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x0008D6CC File Offset: 0x0008B8CC
		public static TimeZoneOffsetFormatPart CreateZZ(FormatAttributes attributes)
		{
			bool flag = attributes == null || !attributes.Attributes.ContainsKey("numericZero");
			return new TimeZoneOffsetFormatPart("ZZ", flag ? 1 : 5, 5, attributes);
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x0008D708 File Offset: 0x0008B908
		public override string FormatStringFor(DateTimeFormat.Target target, bool strict = true)
		{
			string baseFormatString = base.BaseFormatString;
			if (!(baseFormatString == "Z"))
			{
				if (!(baseFormatString == "ZZ"))
				{
					throw new NotImplementedException("unsupported format part: " + ((this != null) ? this.ToString() : null));
				}
				if (base.Attributes == null)
				{
					string text;
					switch (target)
					{
					case DateTimeFormat.Target.PosixFormatting:
						text = (strict ? null : "%z");
						break;
					case DateTimeFormat.Target.PosixParsing:
						text = (strict ? null : "%z");
						break;
					case DateTimeFormat.Target.MomentJS:
						text = "ZZ";
						break;
					case DateTimeFormat.Target.DayJSFormatting:
						text = "ZZ";
						break;
					case DateTimeFormat.Target.PowerAppsFormatting:
						text = null;
						break;
					default:
						throw new NotImplementedException("unsupported target: " + target.ToString());
					}
					return text;
				}
				if (base.Attributes.Equals(DateTimeFormatPart.AllowNumericZeroFormatAttributes) || base.Attributes.Equals(DateTimeFormatPart.NumericZeroFormatAttributes))
				{
					string text;
					switch (target)
					{
					case DateTimeFormat.Target.PosixFormatting:
						text = (strict ? null : "%z");
						break;
					case DateTimeFormat.Target.PosixParsing:
						text = (strict ? null : "%z");
						break;
					case DateTimeFormat.Target.MomentJS:
						text = (strict ? null : "ZZ");
						break;
					case DateTimeFormat.Target.DayJSFormatting:
						text = (strict ? null : "ZZ");
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
						text = "Z";
						break;
					case DateTimeFormat.Target.DayJSFormatting:
						text = "Z";
						break;
					case DateTimeFormat.Target.PowerAppsFormatting:
						text = null;
						break;
					default:
						throw new NotImplementedException("unsupported target: " + target.ToString());
					}
					return text;
				}
				if (base.Attributes.Equals(DateTimeFormatPart.AllowNumericZeroFormatAttributes) || base.Attributes.Equals(DateTimeFormatPart.NumericZeroFormatAttributes))
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
						text = (strict ? null : "Z");
						break;
					case DateTimeFormat.Target.DayJSFormatting:
						text = (strict ? null : "Z");
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
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x0008D988 File Offset: 0x0008BB88
		public override XElement RenderXML()
		{
			return base.RenderXML("TimeZoneOffsetFormatPart");
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x0008D998 File Offset: 0x0008BB98
		public new static TimeZoneOffsetFormatPart TryParseFromXML(XElement literal)
		{
			if (literal.Name != "TimeZoneOffsetFormatPart")
			{
				return null;
			}
			Optional<FormatAttributes> optional = DateTimeFormatPart.ParseAttributesFromXML(literal);
			XAttribute xattribute = literal.Attribute("BaseFormatString");
			if (!optional.HasValue || xattribute == null)
			{
				return null;
			}
			return DateTimeFormatPart.Create(xattribute.Value, optional.Value) as TimeZoneOffsetFormatPart;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x0008D9FC File Offset: 0x0008BBFC
		protected override string ToString(int value)
		{
			if (value == 0 && this.ZeroIsZ)
			{
				return "Z";
			}
			int num = Math.Abs(value);
			int num2 = num / 60;
			int num3 = num % 60;
			StringBuilder stringBuilder = new StringBuilder(base.MaximumLength);
			stringBuilder.Append((value < 0) ? '-' : '+');
			stringBuilder.Append(num2.ToString("00"));
			stringBuilder.Append(this.Separator);
			stringBuilder.Append(num3.ToString("00"));
			return stringBuilder.ToString();
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x0008DA80 File Offset: 0x0008BC80
		protected internal override Optional<Record<StringRegion, int>> ParseNext(StringRegion sr)
		{
			Optional<char> optional = sr.MaybeFirstChar();
			if (!optional.HasValue)
			{
				return default(Optional<Record<StringRegion, int>>);
			}
			char value = optional.Value;
			if (value == 'Z' && this.ZeroIsZ)
			{
				return Record.Create<StringRegion, int>(sr.Slice(sr.Start, sr.Start + 1U), 0).Some<Record<StringRegion, int>>();
			}
			if ((ulong)sr.Length < (ulong)((long)base.MaximumLength) || (value != '+' && value != '-') || (this.Separator.Length > 0 && sr.RelativeSlice(3U, (uint)(3 + this.Separator.Length)).Value != this.Separator))
			{
				return default(Optional<Record<StringRegion, int>>);
			}
			Optional<int> optional2 = NumericDateTimeFormatPart.TryParse(sr.Source, (int)(sr.Start + 1U), (int)(sr.Start + 3U));
			if (optional2.Select((int hour) => hour > 24).OrElse(true))
			{
				return default(Optional<Record<StringRegion, int>>);
			}
			Optional<int> optional3 = NumericDateTimeFormatPart.TryParse(sr.Source, (int)(sr.Start + 3U + (uint)this.Separator.Length), (int)(sr.Start + 5U + (uint)this.Separator.Length));
			if (optional3.Select((int minute) => minute != 0 && minute != 30 && minute != 45).OrElse(true))
			{
				return default(Optional<Record<StringRegion, int>>);
			}
			int num = optional2.Value * 60 + optional3.Value;
			if (num == 0 && (!this.AllowNumericZero || value == '-'))
			{
				return default(Optional<Record<StringRegion, int>>);
			}
			if (value == '-')
			{
				num = -num;
			}
			return Record.Create<StringRegion, int>(sr.RelativeSlice(0U, (uint)base.MaximumLength), num).Some<Record<StringRegion, int>>();
		}

		// Token: 0x04001771 RID: 6001
		internal const string XMLName = "TimeZoneOffsetFormatPart";
	}
}
