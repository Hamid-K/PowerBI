using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016CC RID: 5836
	public class FormatNumberDescriptor : ProgramDescriptor, IEquatable<FormatNumberDescriptor>
	{
		// Token: 0x17002113 RID: 8467
		// (get) Token: 0x0600C2BF RID: 49855 RVA: 0x0029FA3C File Offset: 0x0029DC3C
		public CultureInfo Culture
		{
			get
			{
				CultureInfo cultureInfo;
				if ((cultureInfo = this._culture) == null)
				{
					cultureInfo = (this._culture = new CultureInfo(this.Locale));
				}
				return cultureInfo;
			}
		}

		// Token: 0x17002114 RID: 8468
		// (get) Token: 0x0600C2C0 RID: 49856 RVA: 0x0029FA67 File Offset: 0x0029DC67
		public string DecimalSeparator
		{
			get
			{
				return this.Culture.NumberFormat.NumberDecimalSeparator;
			}
		}

		// Token: 0x17002115 RID: 8469
		// (get) Token: 0x0600C2C1 RID: 49857 RVA: 0x0029FA79 File Offset: 0x0029DC79
		public string GroupSeparator
		{
			get
			{
				return this.Culture.NumberFormat.NumberGroupSeparator;
			}
		}

		// Token: 0x17002116 RID: 8470
		// (get) Token: 0x0600C2C2 RID: 49858 RVA: 0x0029FA8B File Offset: 0x0029DC8B
		public bool IncludeDecimalSeparator
		{
			get
			{
				return this.TrailingDigits > 0;
			}
		}

		// Token: 0x17002117 RID: 8471
		// (get) Token: 0x0600C2C3 RID: 49859 RVA: 0x0029FA96 File Offset: 0x0029DC96
		// (set) Token: 0x0600C2C4 RID: 49860 RVA: 0x0029FA9E File Offset: 0x0029DC9E
		public bool IncludeGroupSeparator { get; set; }

		// Token: 0x17002118 RID: 8472
		// (get) Token: 0x0600C2C5 RID: 49861 RVA: 0x0029FAA7 File Offset: 0x0029DCA7
		public bool IncludeSymbol
		{
			get
			{
				return this.Symbol != null;
			}
		}

		// Token: 0x17002119 RID: 8473
		// (get) Token: 0x0600C2C6 RID: 49862 RVA: 0x0029FAB2 File Offset: 0x0029DCB2
		public bool IncludeCurrencySymbol
		{
			get
			{
				FormatNumberSymbolDescriptor symbol = this.Symbol;
				return symbol != null && symbol.IsCurrency;
			}
		}

		// Token: 0x1700211A RID: 8474
		// (get) Token: 0x0600C2C7 RID: 49863 RVA: 0x0029FAC5 File Offset: 0x0029DCC5
		public bool IncludePercentSymbol
		{
			get
			{
				FormatNumberSymbolDescriptor symbol = this.Symbol;
				return symbol != null && symbol.IsPercent;
			}
		}

		// Token: 0x1700211B RID: 8475
		// (get) Token: 0x0600C2C8 RID: 49864 RVA: 0x0029FAD8 File Offset: 0x0029DCD8
		// (set) Token: 0x0600C2C9 RID: 49865 RVA: 0x0029FAE0 File Offset: 0x0029DCE0
		public int LeadingDigits { get; set; } = 1;

		// Token: 0x1700211C RID: 8476
		// (get) Token: 0x0600C2CA RID: 49866 RVA: 0x0029FAEC File Offset: 0x0029DCEC
		public string Mask
		{
			get
			{
				string text;
				if ((text = this._mask) == null)
				{
					text = (this._mask = this.ToSimplifiedFormatString());
				}
				return text;
			}
		}

		// Token: 0x1700211D RID: 8477
		// (get) Token: 0x0600C2CB RID: 49867 RVA: 0x0029FB12 File Offset: 0x0029DD12
		// (set) Token: 0x0600C2CC RID: 49868 RVA: 0x0029FB1A File Offset: 0x0029DD1A
		public string Locale { get; set; }

		// Token: 0x1700211E RID: 8478
		// (get) Token: 0x0600C2CD RID: 49869 RVA: 0x0029FB23 File Offset: 0x0029DD23
		// (set) Token: 0x0600C2CE RID: 49870 RVA: 0x0029FB2B File Offset: 0x0029DD2B
		public FormatNumberSymbolDescriptor Symbol { get; set; }

		// Token: 0x1700211F RID: 8479
		// (get) Token: 0x0600C2CF RID: 49871 RVA: 0x0029FB34 File Offset: 0x0029DD34
		// (set) Token: 0x0600C2D0 RID: 49872 RVA: 0x0029FB3C File Offset: 0x0029DD3C
		public int TrailingDigits { get; set; }

		// Token: 0x17002120 RID: 8480
		// (get) Token: 0x0600C2D1 RID: 49873 RVA: 0x0029FB48 File Offset: 0x0029DD48
		public string Pattern
		{
			get
			{
				string text;
				if ((text = this._pattern) == null)
				{
					text = (this._pattern = this.LoadPattern());
				}
				return text;
			}
		}

		// Token: 0x17002121 RID: 8481
		// (get) Token: 0x0600C2D2 RID: 49874 RVA: 0x0029FB70 File Offset: 0x0029DD70
		public Regex Regex
		{
			get
			{
				Regex regex;
				if ((regex = this._regex) == null)
				{
					regex = (this._regex = this.Pattern.ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant));
				}
				return regex;
			}
		}

		// Token: 0x0600C2D3 RID: 49875 RVA: 0x0029FBA0 File Offset: 0x0029DDA0
		private string LoadPattern()
		{
			string text = Regex.Escape(this.Culture.NumberFormat.NumberGroupSeparator);
			string text2 = Regex.Escape(this.Culture.NumberFormat.NumberDecimalSeparator);
			int num = this.Culture.NumberFormat.NumberGroupSizes.FirstOrDefault<int>();
			if (num == 0)
			{
				num = 3;
			}
			string text3 = null;
			if (this.IncludeGroupSeparator)
			{
				double num2 = (double)this.LeadingDigits;
				if (num2 > (double)num)
				{
					num2 += 1.0;
				}
				string text4 = string.Format("(?=[{0}{1}]{{{2},}})", text, "\\p{N}", num2);
				text3 = string.Format("{0}(?:{1}{{1,{2}}}(?:{3}{4}{{{5}}})*)", new object[] { text4, "\\p{N}", num, text, "\\p{N}", num });
			}
			if (text3 == null)
			{
				text3 = string.Format("{0}{{{1},}}", "\\p{N}", this.LeadingDigits);
			}
			string text5 = (this.IncludeDecimalSeparator ? string.Format("[{0}]{1}{{{2}}}", text2, "\\p{N}", this.TrailingDigits) : string.Empty);
			string text6 = string.Empty;
			string text7 = string.Empty;
			string text8 = this.Culture.NumberFormat.PositiveSign + this.Culture.NumberFormat.NegativeSign;
			if (this.IncludeSymbol && this.Symbol != null && !string.IsNullOrEmpty(this.Symbol.Text))
			{
				string text9 = Regex.Escape(this.Symbol.Text);
				if (this.Symbol.Prefix)
				{
					text6 = text9;
				}
				if (!this.Symbol.Prefix)
				{
					text7 = text9;
				}
			}
			return string.Concat(new string[]
			{
				"(?<![", text, text2, "\\p{N}])", text6, "[", text8, "]?", text3, text5,
				text7, "(?!\\p{N})"
			});
		}

		// Token: 0x0600C2D4 RID: 49876 RVA: 0x0029FD9C File Offset: 0x0029DF9C
		public bool Equals(FormatNumberDescriptor other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C2D5 RID: 49877 RVA: 0x0029FDBA File Offset: 0x0029DFBA
		public override bool Equals(object other)
		{
			return this.Equals(other as FormatNumberDescriptor);
		}

		// Token: 0x0600C2D6 RID: 49878 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C2D7 RID: 49879 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool IsCompatible(ProgramDescriptor other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600C2D8 RID: 49880 RVA: 0x0029FDC8 File Offset: 0x0029DFC8
		public static bool operator ==(FormatNumberDescriptor left, FormatNumberDescriptor right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C2D9 RID: 49881 RVA: 0x0029FDDE File Offset: 0x0029DFDE
		public static bool operator !=(FormatNumberDescriptor left, FormatNumberDescriptor right)
		{
			return !(left == right);
		}

		// Token: 0x0600C2DA RID: 49882 RVA: 0x0029FDEC File Offset: 0x0029DFEC
		public string ToFormatString()
		{
			if (this._toFormatString != null)
			{
				return this._toFormatString;
			}
			string text = this.ToSimplifiedFormatString();
			string text2;
			if ((text2 = this._toFormatString) == null)
			{
				text2 = (this._toFormatString = text + ";-" + text);
			}
			return text2;
		}

		// Token: 0x0600C2DB RID: 49883 RVA: 0x0029FE30 File Offset: 0x0029E030
		public string ToSimplifiedFormatString()
		{
			if (this._toSimplifiedFormatString != null)
			{
				return this._toSimplifiedFormatString;
			}
			if (this.LeadingDigits < 1)
			{
				throw new ApplicationException(string.Format("Invalid LeadingDigits ({0})", this.LeadingDigits));
			}
			if (this.TrailingDigits < 0)
			{
				throw new ApplicationException(string.Format("Invalid TrailingDigits ({0})", this.TrailingDigits));
			}
			string text = ((this.IncludeSymbol && this.Symbol.Prefix) ? this.Symbol.Text : string.Empty);
			int num = this.Culture.NumberFormat.CurrencyGroupSizes.FirstOrDefault<int>();
			if (num == 0)
			{
				num = 3;
			}
			if (this.IncludeGroupSeparator)
			{
				if (this.LeadingDigits == 3)
				{
					Console.Write("");
				}
				string text2 = ((this.LeadingDigits > num) ? new string('0', this.LeadingDigits) : (new string('#', num + 1 - this.LeadingDigits) + new string('0', this.LeadingDigits)));
				text2 = text2.Insert(text2.Length - num, ",");
				text += text2;
			}
			else
			{
				text += new string('0', this.LeadingDigits);
			}
			if (this.IncludeDecimalSeparator && this.TrailingDigits > 0)
			{
				text = text + "." + new string('0', this.TrailingDigits);
			}
			if (this.IncludeSymbol && !this.Symbol.Prefix)
			{
				text += this.Symbol.Text;
			}
			string text3;
			if ((text3 = this._toSimplifiedFormatString) == null)
			{
				text3 = (this._toSimplifiedFormatString = text);
			}
			return text3;
		}

		// Token: 0x0600C2DC RID: 49884 RVA: 0x0029FFC8 File Offset: 0x0029E1C8
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Concat(new string[]
				{
					"{[",
					this.Culture.Name,
					"]",
					this.ToSimplifiedFormatString(),
					"}"
				}));
			}
			return text;
		}

		// Token: 0x04004BBD RID: 19389
		private CultureInfo _culture;

		// Token: 0x04004BBE RID: 19390
		private string _toFormatString;

		// Token: 0x04004BBF RID: 19391
		private string _toSimplifiedFormatString;

		// Token: 0x04004BC0 RID: 19392
		private string _toString;

		// Token: 0x04004BC1 RID: 19393
		private string _pattern;

		// Token: 0x04004BC2 RID: 19394
		private Regex _regex;

		// Token: 0x04004BC3 RID: 19395
		private string _mask;
	}
}
