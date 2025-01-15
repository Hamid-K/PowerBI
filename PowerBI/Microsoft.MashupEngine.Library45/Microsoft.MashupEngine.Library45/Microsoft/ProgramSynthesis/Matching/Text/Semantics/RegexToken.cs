using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x02001230 RID: 4656
	public class RegexToken : Token
	{
		// Token: 0x06008C45 RID: 35909 RVA: 0x001D6AE0 File Offset: 0x001D4CE0
		public RegexToken(string name, string regex, double score)
			: base(name, score, null)
		{
			this.Regex = new Regex(FormattableString.Invariant(FormattableStringFactory.Create("^{0}", new object[] { regex })), RegexOptions.ExplicitCapture | RegexOptions.Compiled);
			if (this.Regex.Match(string.Empty).Success)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Regex tokens cannot match the empty string: {0}", new object[] { regex })));
			}
		}

		// Token: 0x06008C46 RID: 35910 RVA: 0x001D6B52 File Offset: 0x001D4D52
		protected RegexToken(string name, Regex regex, double score)
			: base(name, score, null)
		{
			this.Regex = regex;
		}

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x06008C47 RID: 35911 RVA: 0x001D6B64 File Offset: 0x001D4D64
		public Regex Regex { get; }

		// Token: 0x06008C48 RID: 35912 RVA: 0x001D6B6C File Offset: 0x001D4D6C
		public override uint PrefixMatchLength(string target)
		{
			if (target == null)
			{
				return 0U;
			}
			Match match = this.Regex.Match(target);
			if (!match.Success)
			{
				return 0U;
			}
			return Convert.ToUInt32(match.Length);
		}

		// Token: 0x06008C49 RID: 35913 RVA: 0x001D6BA0 File Offset: 0x001D4DA0
		public override string TryGetRegexPattern()
		{
			return this.Regex.ToString().Substring(1);
		}

		// Token: 0x06008C4A RID: 35914 RVA: 0x001D6BB4 File Offset: 0x001D4DB4
		public override XElement RenderXML()
		{
			return new XElement("RegexToken", this.Regex.ToString()).WithAttribute("name", base.Name).WithAttribute("score", base.Score);
		}

		// Token: 0x06008C4B RID: 35915 RVA: 0x001D6C00 File Offset: 0x001D4E00
		protected internal static RegexToken Parse(XElement node)
		{
			if (node.Name != "RegexToken")
			{
				return null;
			}
			XAttribute xattribute = node.Attribute("name");
			string text = ((xattribute != null) ? xattribute.Value : null);
			XAttribute xattribute2 = node.Attribute("score");
			string text2 = ((xattribute2 != null) ? xattribute2.Value : null);
			string value = node.Value;
			if (text == null || text2 == null || string.IsNullOrEmpty(value))
			{
				return null;
			}
			return new RegexToken(text, value, double.Parse(text2, CultureInfo.InvariantCulture));
		}
	}
}
