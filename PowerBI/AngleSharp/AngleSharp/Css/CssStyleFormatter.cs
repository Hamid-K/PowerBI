using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AngleSharp.Css
{
	// Token: 0x02000101 RID: 257
	public sealed class CssStyleFormatter : IStyleFormatter
	{
		// Token: 0x0600084B RID: 2123 RVA: 0x0003A218 File Offset: 0x00038418
		string IStyleFormatter.Sheet(IEnumerable<IStyleFormattable> rules)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			this.WriteJoined(stringBuilder, rules, Environment.NewLine, true);
			return stringBuilder.ToPool();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0003A240 File Offset: 0x00038440
		string IStyleFormatter.Block(IEnumerable<IStyleFormattable> rules)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder().Append('{');
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				foreach (IStyleFormattable styleFormattable in rules)
				{
					stringWriter.Write(' ');
					styleFormattable.ToCss(stringWriter, this);
				}
			}
			return stringBuilder.Append(' ').Append('}').ToPool();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0003A2D0 File Offset: 0x000384D0
		string IStyleFormatter.Declaration(string name, string value, bool important)
		{
			string text = value + (important ? " !important" : string.Empty);
			return name + ": " + text;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0003A2FF File Offset: 0x000384FF
		string IStyleFormatter.Declarations(IEnumerable<string> declarations)
		{
			return string.Join("; ", declarations);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0003A30C File Offset: 0x0003850C
		string IStyleFormatter.Medium(bool exclusive, bool inverse, string type, IEnumerable<IStyleFormattable> constraints)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			bool flag = true;
			if (exclusive)
			{
				stringBuilder.Append("only ");
			}
			else if (inverse)
			{
				stringBuilder.Append("not ");
			}
			if (!string.IsNullOrEmpty(type))
			{
				stringBuilder.Append(type);
				flag = false;
			}
			this.WriteJoined(stringBuilder, constraints, " and ", flag);
			return stringBuilder.ToPool();
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0003A368 File Offset: 0x00038568
		string IStyleFormatter.Constraint(string name, string value)
		{
			string text = ((value != null) ? (": " + value) : string.Empty);
			return "(" + name + text + ")";
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0003A39C File Offset: 0x0003859C
		string IStyleFormatter.Rule(string name, string value)
		{
			return name + " " + value + ";";
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0003A3B0 File Offset: 0x000385B0
		string IStyleFormatter.Rule(string name, string prelude, string rules)
		{
			string text = (string.IsNullOrEmpty(prelude) ? string.Empty : (prelude + " "));
			return name + " " + text + rules;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0003A3E8 File Offset: 0x000385E8
		string IStyleFormatter.Style(string selector, IStyleFormattable rules)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder().Append(selector).Append(" { ");
			int length = stringBuilder.Length;
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				rules.ToCss(stringWriter, this);
			}
			if (stringBuilder.Length > length)
			{
				stringBuilder.Append(' ');
			}
			return stringBuilder.Append('}').ToPool();
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0003A45C File Offset: 0x0003865C
		string IStyleFormatter.Comment(string data)
		{
			return string.Join("/* ", new string[] { data, " */" });
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0003A47C File Offset: 0x0003867C
		private void WriteJoined(StringBuilder sb, IEnumerable<IStyleFormattable> elements, string separator, bool first = true)
		{
			using (StringWriter stringWriter = new StringWriter(sb))
			{
				foreach (IStyleFormattable styleFormattable in elements)
				{
					if (first)
					{
						first = false;
					}
					else
					{
						stringWriter.Write(separator);
					}
					styleFormattable.ToCss(stringWriter, this);
				}
			}
		}

		// Token: 0x04000697 RID: 1687
		public static readonly IStyleFormatter Instance = new CssStyleFormatter();
	}
}
