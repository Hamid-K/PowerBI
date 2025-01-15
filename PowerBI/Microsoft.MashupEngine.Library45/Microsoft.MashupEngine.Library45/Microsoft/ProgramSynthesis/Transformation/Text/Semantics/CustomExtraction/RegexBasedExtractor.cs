using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction
{
	// Token: 0x02001D8B RID: 7563
	[Parseable("TryParseXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public class RegexBasedExtractor : CustomExtractor
	{
		// Token: 0x17002A58 RID: 10840
		// (get) Token: 0x0600FE36 RID: 65078 RVA: 0x00364B0A File Offset: 0x00362D0A
		public Regex Regex { get; }

		// Token: 0x0600FE37 RID: 65079 RVA: 0x00364B12 File Offset: 0x00362D12
		public override IReadOnlyList<Record<uint, uint>> Extract(string s)
		{
			return (from m in this.Regex.NonCachingMatches(s)
				select Record.Create<uint, uint>((uint)m.Index, (uint)(m.Index + m.Length))).ToList<Record<uint, uint>>();
		}

		// Token: 0x0600FE38 RID: 65080 RVA: 0x00364B49 File Offset: 0x00362D49
		public override void BindTranslation(Module module, string name, TargetLanguage translationTarget, string headerModuleName)
		{
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("{0} should never have been called on a {1} object.", new object[]
			{
				"BindTranslation",
				base.GetType()
			})));
		}

		// Token: 0x17002A59 RID: 10841
		// (get) Token: 0x0600FE39 RID: 65081 RVA: 0x00364B76 File Offset: 0x00362D76
		public override double Score { get; }

		// Token: 0x0600FE3A RID: 65082 RVA: 0x00364B80 File Offset: 0x00362D80
		protected override XElement RenderXMLImpl()
		{
			XElement xelement = new XElement("RegexBasedExtractor");
			xelement.Add(new XElement("Regex", this.Regex.ToString()));
			xelement.Add(new XElement("Score", this.Score));
			return xelement;
		}

		// Token: 0x0600FE3B RID: 65083 RVA: 0x00364BDC File Offset: 0x00362DDC
		protected override string RenderHumanReadableImpl()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("RegexBasedExtractor({0}, {1})", new object[] { this.Regex, this.Score }));
		}

		// Token: 0x0600FE3C RID: 65084 RVA: 0x00364C0A File Offset: 0x00362E0A
		public override bool Equals(CustomExtractor other)
		{
			return other != null && (other == this || (!(base.GetType() != other.GetType()) && this.Equals(other as RegexBasedExtractor)));
		}

		// Token: 0x0600FE3D RID: 65085 RVA: 0x00364C38 File Offset: 0x00362E38
		public override int GetHashCode()
		{
			return this.Regex.GetHashCode() * 31;
		}

		// Token: 0x0600FE3E RID: 65086 RVA: 0x00364C48 File Offset: 0x00362E48
		public bool Equals(RegexBasedExtractor other)
		{
			return other.Regex.ToString() == this.Regex.ToString();
		}

		// Token: 0x0600FE3F RID: 65087 RVA: 0x00364C65 File Offset: 0x00362E65
		public RegexBasedExtractor(string regexString, double score)
			: this(new Regex(regexString, RegexOptions.Compiled), score)
		{
		}

		// Token: 0x0600FE40 RID: 65088 RVA: 0x00364C78 File Offset: 0x00362E78
		public RegexBasedExtractor(Regex regex, double score)
		{
			this.Regex = regex;
			this.Score = score;
			if (score < 0.0 || score > 1.0)
			{
				throw new ArgumentOutOfRangeException(FormattableString.Invariant(FormattableStringFactory.Create("{0} must be between 0.0 and 1.0", new object[] { "score" })));
			}
		}

		// Token: 0x0600FE41 RID: 65089 RVA: 0x00364CD4 File Offset: 0x00362ED4
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("RegexBasedExtractor({0})", new object[] { this.Regex.ToString() }));
		}

		// Token: 0x0600FE42 RID: 65090 RVA: 0x00364CFC File Offset: 0x00362EFC
		public new static RegexBasedExtractor TryParseXML(XElement literal, DeserializationContext context)
		{
			XElement xelement = literal.Element("Regex");
			string text = ((xelement != null) ? xelement.Value : null);
			XElement xelement2 = literal.Element("Score");
			string text2 = ((xelement2 != null) ? xelement2.Value : null);
			if (text == null || text2 == null)
			{
				return null;
			}
			double num;
			if (!double.TryParse(text2, out num))
			{
				return null;
			}
			return new RegexBasedExtractor(text, num);
		}

		// Token: 0x0600FE43 RID: 65091 RVA: 0x00364D60 File Offset: 0x00362F60
		public new static RegexBasedExtractor TryParseHumanReadable(string literal, DeserializationContext context)
		{
			string text = "RegexBasedExtractor(";
			if (!literal.StartsWith(text, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			List<string> list = (from s in literal.Substring(text.Length, literal.Length - text.Length - 1).Split(new char[] { ',' })
				select s.Trim()).ToList<string>();
			if (list.Count != 2)
			{
				return null;
			}
			double num;
			if (!double.TryParse(list[1], out num))
			{
				return null;
			}
			return new RegexBasedExtractor(list[0], num);
		}
	}
}
