using System;
using System.Text;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009B3 RID: 2483
	internal sealed class MdxIdentifier
	{
		// Token: 0x060046D7 RID: 18135 RVA: 0x000EDB39 File Offset: 0x000EBD39
		public MdxIdentifier(string dimension, string hierarchy, string level, string propertyOrMember, string identifier, MdxIdentifierType type)
		{
			this.dimension = dimension;
			this.hierarchy = hierarchy;
			this.level = level;
			this.propertyOrMember = propertyOrMember;
			this.identifier = identifier;
			this.type = type;
		}

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x060046D8 RID: 18136 RVA: 0x000EDB6E File Offset: 0x000EBD6E
		public string DimensionName
		{
			get
			{
				return this.dimension;
			}
		}

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x060046D9 RID: 18137 RVA: 0x000EDB76 File Offset: 0x000EBD76
		public string HierarchyName
		{
			get
			{
				return this.hierarchy;
			}
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x060046DA RID: 18138 RVA: 0x000EDB7E File Offset: 0x000EBD7E
		public string LevelName
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x060046DB RID: 18139 RVA: 0x000EDB86 File Offset: 0x000EBD86
		public string PropertyOrMemberName
		{
			get
			{
				return this.propertyOrMember;
			}
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x060046DC RID: 18140 RVA: 0x000EDB8E File Offset: 0x000EBD8E
		public MdxIdentifierType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x060046DD RID: 18141 RVA: 0x000EDB98 File Offset: 0x000EBD98
		public string LevelUniqueName
		{
			get
			{
				if (this.level == null)
				{
					throw new InvalidOperationException("LevelUniqueName property cannot be accessed if the identifier is not a level unique name.");
				}
				StringBuilder stringBuilder = new StringBuilder();
				MdxIdentifier.QuotePart(this.dimension, stringBuilder);
				stringBuilder.Append('.');
				MdxIdentifier.QuotePart(this.hierarchy, stringBuilder);
				stringBuilder.Append('.');
				MdxIdentifier.QuotePart(this.level, stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x000EDBFA File Offset: 0x000EBDFA
		public override string ToString()
		{
			return this.identifier;
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x000EDC02 File Offset: 0x000EBE02
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MdxIdentifier);
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x000EDC10 File Offset: 0x000EBE10
		public bool Equals(MdxIdentifier other)
		{
			return other != null && other.identifier == this.identifier;
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x000EDC28 File Offset: 0x000EBE28
		public override int GetHashCode()
		{
			return this.identifier.GetHashCode();
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x000EDC38 File Offset: 0x000EBE38
		public static MdxIdentifier Parse(string input)
		{
			MdxIdentifier mdxIdentifier;
			if (!MdxIdentifier.TryParse(input, out mdxIdentifier))
			{
				throw new FormatException();
			}
			return mdxIdentifier;
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x000EDC58 File Offset: 0x000EBE58
		public static bool TryParse(string input, out MdxIdentifier identifier)
		{
			Scanner scanner = new Scanner(input);
			try
			{
				string text = MdxIdentifier.ParsePart(scanner, false);
				string text2 = null;
				string text3 = null;
				string text4 = null;
				MdxIdentifierType mdxIdentifierType = MdxIdentifierType.Dimension;
				if (scanner.HasMore)
				{
					mdxIdentifierType = MdxIdentifierType.Hierarchy;
					text2 = MdxIdentifier.ParsePart(scanner, true);
					if (scanner.HasMore)
					{
						scanner.Pop('.');
						if (scanner.Peek() == '&')
						{
							mdxIdentifierType = MdxIdentifierType.Member;
							text4 = MdxIdentifier.GetMember(scanner);
						}
						else
						{
							mdxIdentifierType = MdxIdentifierType.Level;
							text3 = MdxIdentifier.ParsePart(scanner, false);
							if (scanner.HasMore)
							{
								mdxIdentifierType = MdxIdentifierType.Member;
								scanner.Pop('.');
								if (scanner.Peek() == '&')
								{
									text4 = MdxIdentifier.GetMember(scanner);
								}
								else
								{
									text4 = MdxIdentifier.ParsePart(scanner, false);
								}
							}
						}
					}
				}
				identifier = new MdxIdentifier(text, text2, text3, text4, input, mdxIdentifierType);
				return !scanner.HasMore;
			}
			catch (FormatException)
			{
			}
			identifier = null;
			return false;
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x000EDD2C File Offset: 0x000EBF2C
		public static string QuotePart(string part)
		{
			StringBuilder stringBuilder = new StringBuilder();
			MdxIdentifier.QuotePart(part, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x000EDD4C File Offset: 0x000EBF4C
		private static string GetMember(Scanner scanner)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (scanner.HasMore)
			{
				if (scanner.Peek() == '.')
				{
					scanner.Pop();
					if (scanner.Peek() == '&')
					{
						continue;
					}
				}
				else
				{
					scanner.Pop('&');
				}
				stringBuilder.Append(MdxIdentifier.ParsePart(scanner, false));
				if (scanner.HasMore)
				{
					stringBuilder.Append(".");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x000EDDB8 File Offset: 0x000EBFB8
		public static string ParsePart(Scanner scanner, bool expectPeriod)
		{
			if (expectPeriod)
			{
				scanner.Pop('.');
			}
			scanner.Pop('[');
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				char c = scanner.Pop();
				if (c == ']')
				{
					if (!scanner.HasMore || scanner.Peek() != ']')
					{
						break;
					}
					stringBuilder.Append(scanner.Pop());
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			string text = stringBuilder.ToString();
			if (text.Length == 0)
			{
				throw new FormatException();
			}
			return text;
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x000EDE2C File Offset: 0x000EC02C
		private static void QuotePart(string identifier, StringBuilder sb)
		{
			sb.Append('[');
			foreach (char c in identifier)
			{
				if (c == ']')
				{
					sb.Append(']');
				}
				sb.Append(c);
			}
			sb.Append(']');
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x000EDE7C File Offset: 0x000EC07C
		public static bool TryGetQuotedValue(TextValue textValue, out string value)
		{
			value = textValue.AsString;
			if (!MdxIdentifier.IsValidIdentifier(value))
			{
				return false;
			}
			string text;
			bool flag = MdxIdentifier.TryQuoteValue(value, out text);
			value = text;
			return flag;
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x000EDEA8 File Offset: 0x000EC0A8
		public static bool IsValidIdentifier(string identifier)
		{
			return !string.IsNullOrEmpty(identifier) && identifier.Length <= 500;
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x000EDEC4 File Offset: 0x000EC0C4
		public static bool TryQuoteValue(string value, out string quotedValue)
		{
			value = value.Trim();
			quotedValue = value;
			if (!value.StartsWith("[", StringComparison.Ordinal))
			{
				if (value.EndsWith("]", StringComparison.Ordinal))
				{
					return false;
				}
				quotedValue = MdxIdentifier.QuotePart(value);
			}
			return true;
		}

		// Token: 0x040025A8 RID: 9640
		private const int MaxIdentifierLength = 500;

		// Token: 0x040025A9 RID: 9641
		private readonly string dimension;

		// Token: 0x040025AA RID: 9642
		private readonly string hierarchy;

		// Token: 0x040025AB RID: 9643
		private readonly string level;

		// Token: 0x040025AC RID: 9644
		private readonly string propertyOrMember;

		// Token: 0x040025AD RID: 9645
		private readonly string identifier;

		// Token: 0x040025AE RID: 9646
		private readonly MdxIdentifierType type;
	}
}
