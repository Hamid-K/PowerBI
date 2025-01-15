using System;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Split.Text.Semantics
{
	// Token: 0x0200138B RID: 5003
	[Parseable("ParseXML")]
	public struct QuotingConfiguration : IRenderableLiteral
	{
		// Token: 0x06009B5D RID: 39773 RVA: 0x0020C878 File Offset: 0x0020AA78
		public QuotingConfiguration(char? quoteChar = '"', bool doubleQuoteEscape = true, char? escapeChar = '\\', QuotingStyle style = QuotingStyle.Standard)
		{
			this.QuoteChar = quoteChar;
			this.DoubleQuoteEscape = doubleQuoteEscape;
			if (this.DoubleQuoteEscape && this.QuoteChar == null)
			{
				throw new ArgumentException("DoubleQuoteEscape is true, when QuoteChar is null");
			}
			this.EscapeChar = escapeChar;
			if (this.QuoteChar != null)
			{
				char? c = this.QuoteChar;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
				c = this.EscapeChar;
				int? num2 = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
				if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
				{
					throw new ArgumentException("EscapeChar cannot be equal to QuoteChar");
				}
			}
			this.Style = style;
		}

		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x06009B5E RID: 39774 RVA: 0x0020C953 File Offset: 0x0020AB53
		public readonly char? QuoteChar { get; }

		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x06009B5F RID: 39775 RVA: 0x0020C95B File Offset: 0x0020AB5B
		public readonly bool DoubleQuoteEscape { get; }

		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x06009B60 RID: 39776 RVA: 0x0020C963 File Offset: 0x0020AB63
		public readonly char? EscapeChar { get; }

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x06009B61 RID: 39777 RVA: 0x0020C96B File Offset: 0x0020AB6B
		public readonly QuotingStyle Style { get; }

		// Token: 0x06009B62 RID: 39778 RVA: 0x0020C973 File Offset: 0x0020AB73
		public QuotingConfiguration ToStandard()
		{
			return new QuotingConfiguration(this.QuoteChar, this.DoubleQuoteEscape, this.EscapeChar, QuotingStyle.Standard);
		}

		// Token: 0x06009B63 RID: 39779 RVA: 0x0020C990 File Offset: 0x0020AB90
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is QuotingConfiguration)
			{
				QuotingConfiguration quotingConfiguration = (QuotingConfiguration)obj;
				return this.Equals(quotingConfiguration);
			}
			return false;
		}

		// Token: 0x06009B64 RID: 39780 RVA: 0x0020C9BC File Offset: 0x0020ABBC
		public bool Equals(QuotingConfiguration other)
		{
			char? c = this.QuoteChar;
			int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
			c = other.QuoteChar;
			int? num2 = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
			if (((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))) && this.DoubleQuoteEscape == other.DoubleQuoteEscape)
			{
				c = this.EscapeChar;
				num2 = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
				c = other.EscapeChar;
				num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
				if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
				{
					return this.Style == other.Style;
				}
			}
			return false;
		}

		// Token: 0x06009B65 RID: 39781 RVA: 0x0020CAD8 File Offset: 0x0020ACD8
		public override int GetHashCode()
		{
			return (int)((((((this.Style * (QuotingStyle)307) ^ (QuotingStyle)((this.QuoteChar == null) ? 0 : this.QuoteChar.GetHashCode())) * (QuotingStyle)307) ^ (QuotingStyle)this.DoubleQuoteEscape.GetHashCode()) * (QuotingStyle)307) ^ (QuotingStyle)((this.EscapeChar == null) ? 0 : this.EscapeChar.GetHashCode()));
		}

		// Token: 0x06009B66 RID: 39782 RVA: 0x0020CB5C File Offset: 0x0020AD5C
		public override string ToString()
		{
			return string.Format("<{0}, {1}, {2}, {3}>", new object[]
			{
				this.QuoteChar.ToLiteral(null),
				this.DoubleQuoteEscape,
				this.EscapeChar.ToLiteral(null),
				this.Style.ToLiteral(null)
			});
		}

		// Token: 0x06009B67 RID: 39783 RVA: 0x0020CBC3 File Offset: 0x0020ADC3
		public string RenderHumanReadable()
		{
			return this.ToString();
		}

		// Token: 0x06009B68 RID: 39784 RVA: 0x0020CBD4 File Offset: 0x0020ADD4
		public XElement RenderXML()
		{
			return new XElement("QuotingConfiguration").WithAttribute("QuoteChar", ((int)this.QuoteChar) ?? (-1)).WithAttribute("DoubleQuoteEscape", this.DoubleQuoteEscape).WithAttribute("EscapeChar", ((int)this.EscapeChar) ?? (-1))
				.WithAttribute("Style", this.Style);
		}

		// Token: 0x06009B69 RID: 39785 RVA: 0x0020CC6C File Offset: 0x0020AE6C
		public static QuotingConfiguration? ParseXML(XElement node)
		{
			if (node.Name != "QuotingConfiguration")
			{
				return null;
			}
			XAttribute xattribute = node.Attribute("QuoteChar");
			int num;
			if (!int.TryParse((xattribute != null) ? xattribute.Value : null, out num))
			{
				return null;
			}
			char? c = ((num >= 0 && num <= 65535) ? new char?((char)num) : null);
			XAttribute xattribute2 = node.Attribute("DoubleQuoteEscape");
			bool flag;
			if (!bool.TryParse((xattribute2 != null) ? xattribute2.Value : null, out flag))
			{
				return null;
			}
			XAttribute xattribute3 = node.Attribute("EscapeChar");
			int num2;
			if (!int.TryParse((xattribute3 != null) ? xattribute3.Value : null, out num2))
			{
				return null;
			}
			char? c2 = ((num2 >= 0 && num2 <= 65535) ? new char?((char)num2) : null);
			XAttribute xattribute4 = node.Attribute("Style");
			QuotingStyle quotingStyle;
			if (!Enum.TryParse<QuotingStyle>((xattribute4 != null) ? xattribute4.Value : null, out quotingStyle))
			{
				return null;
			}
			return new QuotingConfiguration?(new QuotingConfiguration(c, flag, c2, quotingStyle));
		}

		// Token: 0x04003E06 RID: 15878
		public const char Quote = '"';

		// Token: 0x04003E07 RID: 15879
		public const char Pipe = '|';

		// Token: 0x04003E08 RID: 15880
		public const char Backslash = '\\';
	}
}
