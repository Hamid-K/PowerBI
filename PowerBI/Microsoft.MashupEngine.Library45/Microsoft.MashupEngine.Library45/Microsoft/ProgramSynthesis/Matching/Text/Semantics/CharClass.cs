using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x02001225 RID: 4645
	internal class CharClass : IEquatable<CharClass>
	{
		// Token: 0x06008BE3 RID: 35811 RVA: 0x001D4B28 File Offset: 0x001D2D28
		private static string Escape(char c)
		{
			if (!CharClass.SpecialChars.Contains(c))
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { c }));
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("\\{0}", new object[] { c }));
		}

		// Token: 0x06008BE4 RID: 35812 RVA: 0x001D4B80 File Offset: 0x001D2D80
		private CharClass(IEnumerable<string> classes, bool isAny = false)
		{
			this._classes = classes.ToImmutableHashSet<string>();
			this._isAny = isAny;
			string text;
			if (!this._isAny)
			{
				text = "[" + string.Join(string.Empty, this._classes.OrderBy((string s) => s)) + "]";
			}
			else
			{
				text = "[\\s\\S]";
			}
			this.RegexString = text;
		}

		// Token: 0x06008BE5 RID: 35813 RVA: 0x001D4BFE File Offset: 0x001D2DFE
		private CharClass(string classString)
			: this(classString.Yield<string>(), false)
		{
		}

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x06008BE6 RID: 35814 RVA: 0x001D4C0D File Offset: 0x001D2E0D
		internal string RegexString { get; }

		// Token: 0x06008BE7 RID: 35815 RVA: 0x001D4C18 File Offset: 0x001D2E18
		internal static CharClass FromUnionOf(IReadOnlyList<CharClass> charClasses)
		{
			if (charClasses.Any((CharClass c) => c._isAny))
			{
				return CharClass.Any();
			}
			return new CharClass(charClasses.SelectMany((CharClass c) => c._classes), false);
		}

		// Token: 0x06008BE8 RID: 35816 RVA: 0x001D4C7D File Offset: 0x001D2E7D
		internal static CharClass UnionOf(params CharClass[] charClasses)
		{
			return CharClass.FromUnionOf(charClasses);
		}

		// Token: 0x06008BE9 RID: 35817 RVA: 0x001D4C85 File Offset: 0x001D2E85
		internal static CharClass Any()
		{
			return new CharClass(Enumerable.Empty<string>(), true);
		}

		// Token: 0x06008BEA RID: 35818 RVA: 0x001D4C92 File Offset: 0x001D2E92
		internal static CharClass Character(char character)
		{
			return new CharClass(CharClass.Escape(character));
		}

		// Token: 0x06008BEB RID: 35819 RVA: 0x001D4C9F File Offset: 0x001D2E9F
		internal static CharClass From(params char[] chars)
		{
			Func<char, CharClass> func;
			if ((func = CharClass.<>O.<0>__Character) == null)
			{
				func = (CharClass.<>O.<0>__Character = new Func<char, CharClass>(CharClass.Character));
			}
			return CharClass.FromUnionOf(chars.Select(func).ToList<CharClass>());
		}

		// Token: 0x06008BEC RID: 35820 RVA: 0x001D4CCC File Offset: 0x001D2ECC
		internal static CharClass Range(char begin, char end)
		{
			if (begin >= end)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Character class range should have begin {0} less than end {1}.", new object[] { begin, end })));
			}
			if (begin == '-' || end == '-')
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Ranges beginning or ending with {0} are not implemented.", new object[] { '-' })));
			}
			return new CharClass(FormattableString.Invariant(FormattableStringFactory.Create("{0}-{1}", new object[] { begin, end })));
		}

		// Token: 0x06008BED RID: 35821 RVA: 0x001D4D65 File Offset: 0x001D2F65
		internal static CharClass SpecialClass(string classString)
		{
			if (!classString.StartsWith("\\"))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Special classes should begin with backslash.", Array.Empty<object>())));
			}
			return new CharClass(classString);
		}

		// Token: 0x06008BEE RID: 35822 RVA: 0x001D4D94 File Offset: 0x001D2F94
		public bool Equals(CharClass other)
		{
			return other != null && (this == other || this.RegexString == other.RegexString);
		}

		// Token: 0x06008BEF RID: 35823 RVA: 0x001D4DB2 File Offset: 0x001D2FB2
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((CharClass)obj)));
		}

		// Token: 0x06008BF0 RID: 35824 RVA: 0x001D4DE0 File Offset: 0x001D2FE0
		public override int GetHashCode()
		{
			return (275609723 * this.RegexString.GetHashCode()) ^ 1982451631;
		}

		// Token: 0x06008BF1 RID: 35825 RVA: 0x001D4DF9 File Offset: 0x001D2FF9
		public override string ToString()
		{
			return this.RegexString;
		}

		// Token: 0x06008BF2 RID: 35826 RVA: 0x001D4E04 File Offset: 0x001D3004
		internal XElement RenderXML()
		{
			XElement xelement = new XElement("CharClass").WithAttribute("isAny", this._isAny);
			foreach (string text in this._classes)
			{
				xelement.Add(new XElement("class", text));
			}
			return xelement;
		}

		// Token: 0x06008BF3 RID: 35827 RVA: 0x001D4E8C File Offset: 0x001D308C
		internal static CharClass Parse(XElement node)
		{
			if (node.Name != "CharClass")
			{
				return null;
			}
			XAttribute xattribute = node.Attribute("isAny");
			string text = ((xattribute != null) ? xattribute.Value : null);
			bool flag;
			if (text == null || !bool.TryParse(text, out flag))
			{
				return null;
			}
			return new CharClass(from c in node.Elements("class")
				select c.Value, flag);
		}

		// Token: 0x0400392E RID: 14638
		private static readonly char[] SpecialChars = new char[] { '{', '}', '\\', '-', '^', '[', ']' };

		// Token: 0x0400392F RID: 14639
		private const char Hyphen = '-';

		// Token: 0x04003930 RID: 14640
		private readonly ImmutableHashSet<string> _classes;

		// Token: 0x04003931 RID: 14641
		private readonly bool _isAny;

		// Token: 0x02001226 RID: 4646
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04003933 RID: 14643
			public static Func<char, CharClass> <0>__Character;
		}
	}
}
