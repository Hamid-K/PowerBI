using System;
using System.Text;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200020F RID: 527
	public class ExtendedStringBuilder
	{
		// Token: 0x06000DE8 RID: 3560 RVA: 0x00030FE8 File Offset: 0x0002F1E8
		public ExtendedStringBuilder()
			: this(4, 0, null)
		{
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00030FF4 File Offset: 0x0002F1F4
		public ExtendedStringBuilder(int tabSize, int initialIndentation, char[] indentors)
		{
			Ensure.ArgIsInRange<int>(tabSize, 0, 16, "tabSize", 0);
			Ensure.ArgIsInRange<int>(initialIndentation, 0, 16, "initialIndentation", 0);
			Ensure.ArgSatisfiesCondition("indentors", indentors == null || indentors.Length == 2, "Indentors array must be either null, or have two characters (one to indent, another to unindent)");
			this.m_tabSize = tabSize;
			this.m_indentation = initialIndentation;
			this.m_indentors = indentors;
			this.m_stringBuilder = new StringBuilder();
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00031060 File Offset: 0x0002F260
		public void AppendLine(string str)
		{
			for (int i = 0; i < this.m_indentation * this.m_tabSize; i++)
			{
				this.m_stringBuilder.Append(' ');
			}
			this.m_stringBuilder.AppendLine(str);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x000310A0 File Offset: 0x0002F2A0
		public void AppendString(string str)
		{
			this.m_stringBuilder.Append(str);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x000310B0 File Offset: 0x0002F2B0
		public void AppendLine(char c)
		{
			for (int i = 0; i < this.m_indentation * this.m_tabSize; i++)
			{
				this.m_stringBuilder.Append(' ');
			}
			this.m_stringBuilder.Append(c);
			this.m_stringBuilder.AppendLine();
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000310FC File Offset: 0x0002F2FC
		public void AppendLine()
		{
			this.m_stringBuilder.AppendLine();
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0003110A File Offset: 0x0002F30A
		public override string ToString()
		{
			return this.m_stringBuilder.ToString();
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00031117 File Offset: 0x0002F317
		public void AppendTabToPrefix(bool withChar)
		{
			if (withChar)
			{
				this.AppendLine("{");
			}
			this.m_indentation++;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00031135 File Offset: 0x0002F335
		public void Indent()
		{
			if (this.m_indentors != null)
			{
				this.AppendLine(this.m_indentors[0]);
			}
			this.m_indentation++;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003115B File Offset: 0x0002F35B
		public void Unindent()
		{
			this.m_indentation--;
			if (this.m_indentors != null)
			{
				this.AppendLine(this.m_indentors[1]);
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00031181 File Offset: 0x0002F381
		public void RemoveTabFromPrefix(bool withChar)
		{
			this.m_indentation--;
			if (withChar)
			{
				this.AppendLine("}");
			}
		}

		// Token: 0x0400056F RID: 1391
		private const int c_tabSize = 4;

		// Token: 0x04000570 RID: 1392
		private int m_tabSize;

		// Token: 0x04000571 RID: 1393
		private int m_indentation;

		// Token: 0x04000572 RID: 1394
		private char[] m_indentors;

		// Token: 0x04000573 RID: 1395
		private StringBuilder m_stringBuilder;
	}
}
