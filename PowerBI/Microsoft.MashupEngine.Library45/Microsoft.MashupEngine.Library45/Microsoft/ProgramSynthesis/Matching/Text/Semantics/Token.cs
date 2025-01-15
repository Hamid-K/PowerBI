using System;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x02001231 RID: 4657
	[DebuggerDisplay("{ToString()} ({Score})")]
	public abstract class Token : IToken, IEquatable<IToken>, IRenderableLiteral
	{
		// Token: 0x06008C4C RID: 35916 RVA: 0x001D6C89 File Offset: 0x001D4E89
		internal Token(string name, double score, string desc = null)
		{
			this.Name = name;
			this.Score = score;
			this.Description = desc ?? this.Name;
		}

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x06008C4D RID: 35917 RVA: 0x001D6CB0 File Offset: 0x001D4EB0
		// (set) Token: 0x06008C4E RID: 35918 RVA: 0x001D6CB8 File Offset: 0x001D4EB8
		public string Description { get; protected set; }

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x06008C4F RID: 35919 RVA: 0x001D6CC1 File Offset: 0x001D4EC1
		public string Name { get; }

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x06008C50 RID: 35920 RVA: 0x001D6CC9 File Offset: 0x001D4EC9
		public double Score { get; }

		// Token: 0x06008C51 RID: 35921 RVA: 0x001D6CD4 File Offset: 0x001D4ED4
		public bool Equals(IToken other)
		{
			return other != null && (this == other || (string.Equals(this.Name, other.Name) && this.Score.Equals(other.Score)));
		}

		// Token: 0x06008C52 RID: 35922
		public abstract uint PrefixMatchLength(string target);

		// Token: 0x06008C53 RID: 35923
		public abstract string TryGetRegexPattern();

		// Token: 0x06008C54 RID: 35924 RVA: 0x001D6D15 File Offset: 0x001D4F15
		public override string ToString()
		{
			return this.Description;
		}

		// Token: 0x06008C55 RID: 35925 RVA: 0x001D6D1D File Offset: 0x001D4F1D
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(base.GetType() != obj.GetType()) && this.Equals((IToken)obj)));
		}

		// Token: 0x06008C56 RID: 35926 RVA: 0x001D6D4C File Offset: 0x001D4F4C
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			string name = this.Name;
			this._hashCode = new int?((((name != null) ? name.GetHashCode() : 0) * 6725377) ^ this.Score.GetHashCode());
			return this._hashCode.Value;
		}

		// Token: 0x06008C57 RID: 35927 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(Token left, Token right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06008C58 RID: 35928 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(Token left, Token right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06008C59 RID: 35929 RVA: 0x001D6DB0 File Offset: 0x001D4FB0
		public static IToken ParseXML(XElement xml)
		{
			string localName = xml.Name.LocalName;
			if (localName == "ConstantToken")
			{
				return ConstantToken.Parse(xml);
			}
			if (localName == "CharClassToken")
			{
				return CharClassToken.Parse(xml);
			}
			if (localName == "ConcatToken")
			{
				return ConcatToken.Parse(xml);
			}
			if (!(localName == "RegexToken"))
			{
				return null;
			}
			return RegexToken.Parse(xml);
		}

		// Token: 0x06008C5A RID: 35930 RVA: 0x001D6D15 File Offset: 0x001D4F15
		public string RenderHumanReadable()
		{
			return this.Description;
		}

		// Token: 0x06008C5B RID: 35931
		public abstract XElement RenderXML();

		// Token: 0x04003960 RID: 14688
		private int? _hashCode;
	}
}
