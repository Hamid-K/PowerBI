using System;
using System.Globalization;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000336 RID: 822
	internal class ExtendedStringBuilder
	{
		// Token: 0x06001835 RID: 6197 RVA: 0x00058BB1 File Offset: 0x00056DB1
		public ExtendedStringBuilder()
		{
			this.m_stringBuilder = new StringBuilder();
			this.m_indentation = 0;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00058BCB File Offset: 0x00056DCB
		public void Append(string str)
		{
			this.m_stringBuilder.Append(str);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00058BDA File Offset: 0x00056DDA
		[StringFormatMethod("str")]
		public void Append([NotNull] string str, params object[] args)
		{
			this.m_stringBuilder.AppendFormat(CultureInfo.InvariantCulture, str, args);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00058BEF File Offset: 0x00056DEF
		public void AppendAndBeginNewLine(string str)
		{
			this.m_stringBuilder.AppendLine(str);
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00058BFE File Offset: 0x00056DFE
		[StringFormatMethod("str")]
		public void AppendAndBeginNewLine([NotNull] string str, params object[] args)
		{
			this.AppendAndBeginNewLine(string.Format(str, args));
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00058C0D File Offset: 0x00056E0D
		[StringFormatMethod("format")]
		public void AppendLine([NotNull] string format, params object[] args)
		{
			this.AppendLine(string.Format(format, args));
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00058C1C File Offset: 0x00056E1C
		public void AppendLine(string str)
		{
			this.m_stringBuilder.AppendLine("{0}{1}".FormatWithCurrentCulture(new object[]
			{
				new string(' ', this.m_indentation * 4),
				str
			}));
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00058C50 File Offset: 0x00056E50
		[StringFormatMethod("format")]
		public void AppendWithPrefix([NotNull] string format, params object[] args)
		{
			this.AppendWithPrefix(string.Format(format, args));
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00058C5F File Offset: 0x00056E5F
		public void AppendWithPrefix(string str)
		{
			this.m_stringBuilder.Append("{0}{1}".FormatWithCurrentCulture(new object[]
			{
				new string(' ', this.m_indentation * 4),
				str
			}));
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00058C93 File Offset: 0x00056E93
		public void AppendLine()
		{
			this.m_stringBuilder.AppendLine();
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00058CA1 File Offset: 0x00056EA1
		public override string ToString()
		{
			return this.m_stringBuilder.ToString();
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00058CAE File Offset: 0x00056EAE
		public void AppendTabToPrefix(bool withChar)
		{
			if (withChar)
			{
				this.AppendLine("{");
			}
			this.m_indentation++;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00058CCC File Offset: 0x00056ECC
		public void RemoveTabFromPrefix(bool withChar)
		{
			this.m_indentation--;
			if (withChar)
			{
				this.AppendLine("}");
			}
		}

		// Token: 0x04000874 RID: 2164
		public const int c_tabSize = 4;

		// Token: 0x04000875 RID: 2165
		private StringBuilder m_stringBuilder;

		// Token: 0x04000876 RID: 2166
		private int m_indentation;
	}
}
