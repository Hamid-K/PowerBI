using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x0200122A RID: 4650
	public class ConcatToken : Token, IEquatable<ConcatToken>
	{
		// Token: 0x06008C15 RID: 35861 RVA: 0x001D5752 File Offset: 0x001D3952
		public ConcatToken(string name, double score, params CharClassToken[] subTokens)
			: base(name, score, null)
		{
			this.SubTokens = subTokens;
		}

		// Token: 0x06008C16 RID: 35862 RVA: 0x001D5764 File Offset: 0x001D3964
		public bool Equals(ConcatToken other)
		{
			return other != null && (this == other || (base.Equals(other) && this.SubTokens.SequenceEqual(other.SubTokens)));
		}

		// Token: 0x06008C17 RID: 35863 RVA: 0x001D578D File Offset: 0x001D398D
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((ConcatToken)obj)));
		}

		// Token: 0x06008C18 RID: 35864 RVA: 0x001D57BC File Offset: 0x001D39BC
		public override int GetHashCode()
		{
			if (this._hashCode == null)
			{
				this._hashCode = new int?((base.GetHashCode() * 32452867) ^ ((this.SubTokens != null) ? this.SubTokens.OrderDependentHashCode<CharClassToken>() : 0));
			}
			return this._hashCode.Value;
		}

		// Token: 0x06008C19 RID: 35865 RVA: 0x001D5810 File Offset: 0x001D3A10
		public override XElement RenderXML()
		{
			XElement xelement = new XElement("ConcatToken").WithAttribute("name", base.Name).WithAttribute("score", base.Score);
			foreach (CharClassToken charClassToken in this.SubTokens)
			{
				xelement.Add(charClassToken.RenderXML());
			}
			return xelement;
		}

		// Token: 0x06008C1A RID: 35866 RVA: 0x001D5898 File Offset: 0x001D3A98
		internal static ConcatToken Parse(XElement node)
		{
			if (node.Name != "ConcatToken")
			{
				return null;
			}
			XAttribute xattribute = node.Attribute("name");
			string text = ((xattribute != null) ? xattribute.Value : null);
			if (text == null)
			{
				return null;
			}
			XAttribute xattribute2 = node.Attribute("score");
			double num;
			if (!double.TryParse((xattribute2 != null) ? xattribute2.Value : null, out num))
			{
				return null;
			}
			IEnumerable<XElement> enumerable = node.Elements();
			Func<XElement, CharClassToken> func;
			if ((func = ConcatToken.<>O.<0>__Parse) == null)
			{
				func = (ConcatToken.<>O.<0>__Parse = new Func<XElement, CharClassToken>(CharClassToken.Parse));
			}
			CharClassToken[] array = enumerable.Select(func).ToArray<CharClassToken>();
			if (array.Any<CharClassToken>())
			{
				if (!array.Any((CharClassToken c) => c == null))
				{
					return new ConcatToken(text, num, array);
				}
			}
			return null;
		}

		// Token: 0x06008C1B RID: 35867 RVA: 0x001D596C File Offset: 0x001D3B6C
		public override uint PrefixMatchLength(string target)
		{
			string text = target;
			foreach (CharClassToken charClassToken in this.SubTokens)
			{
				uint num = charClassToken.PrefixMatchLength(text);
				if (num == 0U)
				{
					return 0U;
				}
				text = text.Substring((int)num);
			}
			return (uint)(target.Length - text.Length);
		}

		// Token: 0x06008C1C RID: 35868 RVA: 0x001D59D8 File Offset: 0x001D3BD8
		public override string TryGetRegexPattern()
		{
			string text = TokenUtils.BaseRegexDescriptionFor(this.SubTokens);
			return text.Substring(1, text.Length - 2);
		}

		// Token: 0x04003943 RID: 14659
		public readonly IReadOnlyList<CharClassToken> SubTokens;

		// Token: 0x04003944 RID: 14660
		private int? _hashCode;

		// Token: 0x0200122B RID: 4651
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04003945 RID: 14661
			public static Func<XElement, CharClassToken> <0>__Parse;
		}
	}
}
