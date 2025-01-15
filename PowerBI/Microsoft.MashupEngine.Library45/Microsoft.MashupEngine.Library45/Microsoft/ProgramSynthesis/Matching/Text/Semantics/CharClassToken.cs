using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x02001228 RID: 4648
	public class CharClassToken : RegexToken, IEquatable<CharClassToken>
	{
		// Token: 0x06008BFB RID: 35835 RVA: 0x001D4F54 File Offset: 0x001D3154
		internal CharClassToken(string name, CharClass charClass, uint size, double? score = null)
			: base(name, FormattableString.Invariant(FormattableStringFactory.Create("{0}+", new object[] { charClass.RegexString })), score ?? (-5.0 - Math.Sqrt(size)))
		{
			this.CharClass = charClass;
			this.ClassSize = size;
			this.UnrestrictedToken = this;
			this._hashCode = null;
			base.Description = FormattableString.Invariant(FormattableStringFactory.Create("[{0}]+", new object[] { base.Name }));
		}

		// Token: 0x06008BFC RID: 35836 RVA: 0x001D4FF4 File Offset: 0x001D31F4
		private CharClassToken(CharClassToken source, uint length, double score)
			: base(source.Name, source.Regex, score)
		{
			this.CharClass = source.CharClass;
			this.ClassSize = source.ClassSize;
			this.RequiredLength = new uint?(length);
			this.UnrestrictedToken = source;
			this._hashCode = null;
			base.Description = FormattableString.Invariant(FormattableStringFactory.Create("[{0}]{{{1}}}", new object[]
			{
				base.Name,
				this.RequiredLength.Value
			}));
		}

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x06008BFD RID: 35837 RVA: 0x001D5085 File Offset: 0x001D3285
		[JsonIgnore]
		public CharClassToken UnrestrictedToken { get; }

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x06008BFE RID: 35838 RVA: 0x001D508D File Offset: 0x001D328D
		public uint? RequiredLength { get; }

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x06008BFF RID: 35839 RVA: 0x001D5095 File Offset: 0x001D3295
		internal CharClass CharClass { get; }

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x06008C00 RID: 35840 RVA: 0x001D509D File Offset: 0x001D329D
		public string ClassRegex
		{
			get
			{
				return this.CharClass.RegexString;
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x06008C01 RID: 35841 RVA: 0x001D50AA File Offset: 0x001D32AA
		public uint ClassSize { get; }

		// Token: 0x06008C02 RID: 35842 RVA: 0x001D50B4 File Offset: 0x001D32B4
		public bool Equals(CharClassToken other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (base.Equals(other))
			{
				uint? requiredLength = this.RequiredLength;
				uint? requiredLength2 = other.RequiredLength;
				if ((requiredLength.GetValueOrDefault() == requiredLength2.GetValueOrDefault()) & (requiredLength != null == (requiredLength2 != null)))
				{
					return this.CharClass.Equals(other.CharClass);
				}
			}
			return false;
		}

		// Token: 0x06008C03 RID: 35843 RVA: 0x001D511C File Offset: 0x001D331C
		public override uint PrefixMatchLength(string target)
		{
			if (this.RequiredLength == null)
			{
				return base.PrefixMatchLength(target);
			}
			uint num = base.PrefixMatchLength(target);
			uint? requiredLength = this.RequiredLength;
			if (!((num == requiredLength.GetValueOrDefault()) & (requiredLength != null)))
			{
				return 0U;
			}
			return this.RequiredLength.Value;
		}

		// Token: 0x06008C04 RID: 35844 RVA: 0x001D5174 File Offset: 0x001D3374
		public override string TryGetRegexPattern()
		{
			if (this.RequiredLength == null)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}+(?!{1})", new object[] { this.ClassRegex, this.ClassRegex }));
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}{{{1}}}(?!{2})", new object[]
			{
				this.ClassRegex,
				this.RequiredLength.Value,
				this.ClassRegex
			}));
		}

		// Token: 0x06008C05 RID: 35845 RVA: 0x001D51F8 File Offset: 0x001D33F8
		public string GetNonGreedyRegex()
		{
			if (this.RequiredLength == null)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}+", new object[] { this.ClassRegex }));
			}
			uint? requiredLength = this.RequiredLength;
			uint num = 1U;
			if (!((requiredLength.GetValueOrDefault() == num) & (requiredLength != null)))
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}{{{1}}}", new object[]
				{
					this.ClassRegex,
					this.RequiredLength.Value
				}));
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { this.ClassRegex }));
		}

		// Token: 0x06008C06 RID: 35846 RVA: 0x001D52A4 File Offset: 0x001D34A4
		public static CharClassToken FromUnionOf(IEnumerable<CharClassToken> tokens, double? penalty = null, string newName = null)
		{
			IList<CharClassToken> list = (tokens as IList<CharClassToken>) ?? tokens.ToList<CharClassToken>();
			CharClass charClass = CharClass.FromUnionOf(list.Select((CharClassToken t) => t.CharClass).ToList<CharClass>());
			string text = newName;
			if (newName == null)
			{
				text = string.Join("|", list.Select((CharClassToken token) => token.Name));
			}
			return new CharClassToken(text, charClass, Convert.ToUInt32(list.Sum((CharClassToken token) => (long)((ulong)token.ClassSize))), new double?((penalty ?? 12.0) * list.Sum((CharClassToken token) => token.Score)));
		}

		// Token: 0x06008C07 RID: 35847 RVA: 0x001D539E File Offset: 0x001D359E
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((CharClassToken)obj)));
		}

		// Token: 0x06008C08 RID: 35848 RVA: 0x001D53CC File Offset: 0x001D35CC
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			this._hashCode = new int?(base.GetHashCode());
			this._hashCode = (this._hashCode * 1684171) ^ this.RequiredLength.GetHashCode();
			int? num = this._hashCode * 1684171;
			CharClass charClass = this.CharClass;
			this._hashCode = num ^ ((charClass != null) ? charClass.GetHashCode() : 0);
			return this._hashCode.Value;
		}

		// Token: 0x06008C09 RID: 35849 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(CharClassToken left, CharClassToken right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06008C0A RID: 35850 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(CharClassToken left, CharClassToken right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06008C0B RID: 35851 RVA: 0x001D54E3 File Offset: 0x001D36E3
		public CharClassToken GetTokenForLength(uint length)
		{
			return new CharClassToken(this, length, 0.75 * base.Score * (1.0 + 0.4 / (length + 1U)));
		}

		// Token: 0x06008C0C RID: 35852 RVA: 0x001D5518 File Offset: 0x001D3718
		public override XElement RenderXML()
		{
			if (this.RequiredLength == null)
			{
				XElement xelement = new XElement("CharClassToken").WithAttribute("name", base.Name).WithAttribute("classSize", this.ClassSize).WithAttribute("score", base.Score);
				xelement.Add(this.CharClass.RenderXML());
				return xelement;
			}
			CharClassToken charClassToken = new CharClassToken(base.Name, this.CharClass, this.ClassSize, new double?(base.Score));
			return new XElement("CharClassToken", charClassToken.RenderXML()).WithAttribute("length", this.RequiredLength.Value);
		}

		// Token: 0x06008C0D RID: 35853 RVA: 0x001D55E8 File Offset: 0x001D37E8
		protected internal new static CharClassToken Parse(XElement node)
		{
			if (node.Name != "CharClassToken")
			{
				return null;
			}
			XAttribute xattribute = node.Attribute("length");
			if (xattribute == null)
			{
				XAttribute xattribute2 = node.Attribute("name");
				string text = ((xattribute2 != null) ? xattribute2.Value : null);
				XAttribute xattribute3 = node.Attribute("classSize");
				string text2 = ((xattribute3 != null) ? xattribute3.Value : null);
				XAttribute xattribute4 = node.Attribute("score");
				string text3 = ((xattribute4 != null) ? xattribute4.Value : null);
				XElement xelement = node.Element("CharClass");
				CharClass charClass = ((xelement != null) ? CharClass.Parse(xelement) : null);
				if (text == null || text2 == null || text3 == null || charClass == null)
				{
					return null;
				}
				return new CharClassToken(text, charClass, uint.Parse(text2, CultureInfo.InvariantCulture), new double?(double.Parse(text3, CultureInfo.InvariantCulture)));
			}
			else
			{
				uint num = uint.Parse(xattribute.Value, CultureInfo.InvariantCulture);
				CharClassToken charClassToken = CharClassToken.Parse(node.Elements().First<XElement>());
				if (charClassToken == null)
				{
					return null;
				}
				return new CharClassToken(charClassToken, num, charClassToken.Score);
			}
		}

		// Token: 0x06008C0E RID: 35854 RVA: 0x001D5711 File Offset: 0x001D3911
		internal bool Contains(char character)
		{
			return base.Regex.IsMatch(character.ToString());
		}

		// Token: 0x04003939 RID: 14649
		private int? _hashCode;
	}
}
