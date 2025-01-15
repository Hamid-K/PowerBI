using System;
using System.Globalization;
using System.Text;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004B2 RID: 1202
	internal sealed class SapBwIdentifier
	{
		// Token: 0x06002788 RID: 10120 RVA: 0x0007415C File Offset: 0x0007235C
		public static MdxIdentifier Parse(string input)
		{
			MdxIdentifier mdxIdentifier;
			if (!SapBwIdentifier.TryParse(input, out mdxIdentifier))
			{
				throw new FormatException();
			}
			return mdxIdentifier;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0007417C File Offset: 0x0007237C
		public static bool TryParse(string input, out MdxIdentifier identifier)
		{
			Scanner scanner = new Scanner(input);
			try
			{
				bool flag;
				string text = SapBwIdentifier.ParseDimension(scanner, out flag);
				string text2 = (flag ? SapBwIdentifier.ParseHierarchy(scanner) : null);
				string text3 = null;
				string text4 = null;
				string text5;
				MdxIdentifierType mdxIdentifierType;
				while (SapBwIdentifier.TryParsePart(scanner, out text5, out mdxIdentifierType))
				{
					if (mdxIdentifierType == MdxIdentifierType.Level)
					{
						text3 = text5;
					}
					else
					{
						text4 = text5;
					}
				}
				mdxIdentifierType = ((text2 == null) ? MdxIdentifierType.Dimension : MdxIdentifierType.Hierarchy);
				identifier = new MdxIdentifier(text, text2, text3, text4, input, mdxIdentifierType);
				return true;
			}
			catch (FormatException)
			{
			}
			identifier = null;
			return false;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x00074204 File Offset: 0x00072404
		public static bool TryExtractDimensionAndLevel(string identifier, out string levelIdentifier)
		{
			MdxIdentifier mdxIdentifier;
			if (SapBwIdentifier.TryParse(identifier, out mdxIdentifier) && mdxIdentifier.LevelName != null)
			{
				levelIdentifier = SapBwIdentifier.ExtractDimensionAndLevel(identifier);
				return true;
			}
			levelIdentifier = null;
			return false;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x00074231 File Offset: 0x00072431
		public static IdentifierCubeExpression ExtractDimensionAndLevel(IdentifierCubeExpression identifier)
		{
			return new IdentifierCubeExpression(SapBwIdentifier.ExtractDimensionAndLevel(identifier.Identifier));
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x00074244 File Offset: 0x00072444
		public static string ExtractDimensionAndLevel(string identifier)
		{
			int num = identifier.LastIndexOf('.');
			return identifier.Substring(0, num);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00074264 File Offset: 0x00072464
		private static string ParseDimension(Scanner scanner, out bool hasAnotherPart)
		{
			hasAnotherPart = false;
			scanner.Pop('[');
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			bool flag = false;
			while (num != 30)
			{
				char c = scanner.Pop();
				if (c == ']')
				{
					string text = stringBuilder.ToString();
					if (text.Length == 0)
					{
						throw new FormatException();
					}
					return text;
				}
				else
				{
					if (c == ' ')
					{
						flag = true;
					}
					else
					{
						if (flag)
						{
							throw new FormatException();
						}
						stringBuilder.Append(c);
					}
					num++;
				}
			}
			hasAnotherPart = scanner.Peek() != ']';
			string text2 = stringBuilder.ToString();
			if (text2.Length == 0)
			{
				throw new FormatException();
			}
			return text2;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000742F0 File Offset: 0x000724F0
		private static string ParseHierarchy(Scanner scanner)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				char c = scanner.Pop();
				if (c == ']')
				{
					break;
				}
				stringBuilder.Append(c);
			}
			string text = stringBuilder.ToString();
			if (text.Length == 0)
			{
				throw new FormatException();
			}
			return text.TrimEnd(Array.Empty<char>());
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x00074337 File Offset: 0x00072537
		private static bool TryParsePart(Scanner scanner, out string part, out MdxIdentifierType type)
		{
			if (scanner.HasMore)
			{
				part = MdxIdentifier.ParsePart(scanner, true);
				type = (part.StartsWith("LEVEL", StringComparison.Ordinal) ? MdxIdentifierType.Level : MdxIdentifierType.Member);
				return true;
			}
			part = null;
			type = MdxIdentifierType.Member;
			return false;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00074368 File Offset: 0x00072568
		public static string TrimAndQuotePart(string identifier, int? minLength = null)
		{
			string text = ((minLength != null) ? SapBwIdentifier.TrimStart(identifier, '0', minLength.Value) : identifier.TrimStart(new char[] { '0' }));
			if (text.Length == 0 && identifier.Length > 0)
			{
				text = "0";
			}
			return MdxIdentifier.QuotePart(text);
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000743C0 File Offset: 0x000725C0
		public static string ExtractValue(string value)
		{
			int num = value.LastIndexOf('.');
			if (num != -1)
			{
				value = value.Substring(num + 1);
			}
			return value.Trim(new char[] { '[', ']' });
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000743FC File Offset: 0x000725FC
		public static object ExtractDateOrNull(object value)
		{
			string text = value as string;
			DateTime dateTime;
			if (text != null && DateTime.TryParseExact(SapBwIdentifier.ExtractValue(text), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
			{
				return new Date(dateTime);
			}
			return null;
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x0007443C File Offset: 0x0007263C
		private static string TrimStart(string value, char charToTrim, int minLength)
		{
			if (value == null || value.Length <= minLength)
			{
				return value;
			}
			int num = value.Length - 1;
			int num2 = value.Length - minLength;
			int num3 = 0;
			while (num3 < num2 && value[num3] == charToTrim)
			{
				num3++;
			}
			int num4 = num - num3 + 1;
			if (num4 == value.Length)
			{
				return value;
			}
			if (num4 != 0)
			{
				return value.Substring(num3, num4);
			}
			return string.Empty;
		}

		// Token: 0x040010A0 RID: 4256
		public const string SapDateFormat = "yyyyMMdd";

		// Token: 0x040010A1 RID: 4257
		private const int SapBwDimensionLength = 30;
	}
}
