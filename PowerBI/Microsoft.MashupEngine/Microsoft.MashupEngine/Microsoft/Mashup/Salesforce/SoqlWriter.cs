using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001FD RID: 509
	internal class SoqlWriter
	{
		// Token: 0x06000A26 RID: 2598 RVA: 0x00016AD5 File Offset: 0x00014CD5
		public SoqlWriter(HashSet<string> identifiers)
		{
			this.writer = new StringWriter(CultureInfo.InvariantCulture);
			this.identifiers = identifiers;
			this.dictionary = new Dictionary<string, string>();
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00016AFF File Offset: 0x00014CFF
		public string ToText()
		{
			return this.writer.ToString();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00016B0C File Offset: 0x00014D0C
		public void WriteIdentifier(string identifier)
		{
			this.writer.Write(identifier);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00016B1A File Offset: 0x00014D1A
		public void WriteInt32(int value)
		{
			this.writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00016B33 File Offset: 0x00014D33
		public void WriteInt64(long value)
		{
			this.writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00016B4C File Offset: 0x00014D4C
		public void WriteDouble(double value)
		{
			this.writer.Write(value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00016B6A File Offset: 0x00014D6A
		public void WriteDecimal(decimal value)
		{
			this.writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00016B83 File Offset: 0x00014D83
		public void WriteDate(DateTime value)
		{
			this.writer.Write(value.ToString("yyyy\\-MM\\-dd", CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00016BA1 File Offset: 0x00014DA1
		public void WriteDateTime(DateTime value)
		{
			this.writer.Write(value.ToString("yyyy\\-MM\\-dd\\THH\\:mm\\:sszzz", CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public string Escape(string value, string charsToEscape)
		{
			StringBuilder stringBuilder = new StringBuilder(value);
			for (int i = 0; i < stringBuilder.Length; i++)
			{
				int num = charsToEscape.IndexOf(stringBuilder[i]);
				if (num >= 0 && num % 2 == 0)
				{
					stringBuilder[i] = charsToEscape[num + 1];
					stringBuilder.Insert(i, '\\');
					i++;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00016C20 File Offset: 0x00014E20
		public void WriteString(string value)
		{
			this.writer.Write("'");
			this.writer.Write(this.Escape(value, "\nn\rr\tt\bb\ff\"\"''\\\\"));
			this.writer.Write("'");
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00016C5C File Offset: 0x00014E5C
		public void WriteLikeString(string value, bool wildAtStart, bool wildAtEnd)
		{
			this.writer.Write("'");
			if (wildAtStart)
			{
				this.writer.Write("%");
			}
			this.writer.Write(this.Escape(value, "\nn\rr\tt\bb\ff\"\"''\\\\__%%"));
			if (wildAtEnd)
			{
				this.writer.Write("%");
			}
			this.writer.Write("'");
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00016CC6 File Offset: 0x00014EC6
		public void WriteBool(bool value)
		{
			this.writer.Write(value ? "TRUE" : "FALSE");
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00016CE2 File Offset: 0x00014EE2
		public void Write(Token token)
		{
			this.writer.Write(token.Value);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00016B33 File Offset: 0x00014D33
		public void Write(long value)
		{
			this.writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00016CF6 File Offset: 0x00014EF6
		public void WriteSpace()
		{
			this.writer.Write(" ");
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00016D08 File Offset: 0x00014F08
		public void WriteLine()
		{
			this.writer.WriteLine();
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00016D18 File Offset: 0x00014F18
		private string CreateIdentifier(string identifier)
		{
			string text = SoqlWriter.CreateBaseIdentifier(identifier);
			string text2 = text;
			int num = this.dictionary.Count;
			while (this.identifiers.Contains(text2) || this.dictionary.ContainsKey(text2))
			{
				text2 = text + num.ToString(CultureInfo.InvariantCulture);
				num++;
			}
			return text2;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00016D70 File Offset: 0x00014F70
		private static string CreateBaseIdentifier(string name)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (char.IsLetter(name[0]))
			{
				stringBuilder.Append(name[0]);
			}
			else
			{
				stringBuilder.Append("_");
			}
			for (int i = 1; i < name.Length; i++)
			{
				if (char.IsLetterOrDigit(name[i]))
				{
					stringBuilder.Append(name[i]);
				}
				else
				{
					stringBuilder.Append("_");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000622 RID: 1570
		private const string charsToEscape = "\nn\rr\tt\bb\ff\"\"''\\\\";

		// Token: 0x04000623 RID: 1571
		private const string charsToEscapeForLike = "\nn\rr\tt\bb\ff\"\"''\\\\__%%";

		// Token: 0x04000624 RID: 1572
		private readonly StringWriter writer;

		// Token: 0x04000625 RID: 1573
		private readonly HashSet<string> identifiers;

		// Token: 0x04000626 RID: 1574
		private readonly Dictionary<string, string> dictionary;
	}
}
