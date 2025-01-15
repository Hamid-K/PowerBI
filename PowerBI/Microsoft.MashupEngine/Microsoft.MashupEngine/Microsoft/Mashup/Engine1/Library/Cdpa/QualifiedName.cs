using System;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E51 RID: 3665
	internal class QualifiedName : IEquatable<QualifiedName>
	{
		// Token: 0x06006277 RID: 25207 RVA: 0x001522A5 File Offset: 0x001504A5
		public static QualifiedName New(string unescapedRoot)
		{
			return new QualifiedName(unescapedRoot, false);
		}

		// Token: 0x06006278 RID: 25208 RVA: 0x001522AE File Offset: 0x001504AE
		public static QualifiedName From(IdentifierCubeExpression expression)
		{
			return new QualifiedName(expression.Identifier, true);
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x001522BC File Offset: 0x001504BC
		private QualifiedName(string name, bool nameIsEscaped)
		{
			this.name = name;
			this.nameIsEscaped = nameIsEscaped;
		}

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x0600627A RID: 25210 RVA: 0x001522D2 File Offset: 0x001504D2
		private string EscapedName
		{
			get
			{
				if (!this.nameIsEscaped)
				{
					return QualifiedName.EscapePart(this.name);
				}
				return this.name;
			}
		}

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x0600627B RID: 25211 RVA: 0x001522EE File Offset: 0x001504EE
		public string AsString
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x001522F6 File Offset: 0x001504F6
		public QualifiedName Qualify(string unescapedPart)
		{
			return new QualifiedName(this.EscapedName + "." + QualifiedName.EscapePart(unescapedPart), true);
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x00152314 File Offset: 0x00150514
		public QualifiedName Qualify(QualifiedName qualifiedPart)
		{
			return new QualifiedName(this.EscapedName + "." + qualifiedPart.EscapedName, true);
		}

		// Token: 0x0600627E RID: 25214 RVA: 0x00152332 File Offset: 0x00150532
		public IdentifierCubeExpression ToExpression()
		{
			return new IdentifierCubeExpression(this.EscapedName);
		}

		// Token: 0x0600627F RID: 25215 RVA: 0x00152340 File Offset: 0x00150540
		public override int GetHashCode()
		{
			return this.name.GetHashCode() + this.nameIsEscaped.GetHashCode();
		}

		// Token: 0x06006280 RID: 25216 RVA: 0x00152367 File Offset: 0x00150567
		public override bool Equals(object other)
		{
			return this.Equals(other as QualifiedName);
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x00152375 File Offset: 0x00150575
		public bool Equals(QualifiedName other)
		{
			return other != null && this.name == other.name && this.nameIsEscaped == other.nameIsEscaped;
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x001522EE File Offset: 0x001504EE
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x0015239D File Offset: 0x0015059D
		private static string EscapePart(string unescaped)
		{
			if (unescaped.IndexOfAny(QualifiedName.charsToEscape) != -1)
			{
				return "[" + unescaped.Replace("]", "]]") + "]";
			}
			return unescaped;
		}

		// Token: 0x040035AA RID: 13738
		private readonly string name;

		// Token: 0x040035AB RID: 13739
		private readonly bool nameIsEscaped;

		// Token: 0x040035AC RID: 13740
		private static readonly char[] charsToEscape = new char[] { ']', '.' };
	}
}
