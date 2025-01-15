using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AngleSharp.Extensions;

namespace AngleSharp.Css
{
	// Token: 0x02000108 RID: 264
	public class PrettyStyleFormatter : IStyleFormatter
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x0003BA92 File Offset: 0x00039C92
		public PrettyStyleFormatter()
		{
			this._intendString = "\t";
			this._newLineString = "\n";
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0003BAB0 File Offset: 0x00039CB0
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0003BAB8 File Offset: 0x00039CB8
		public string Indentation
		{
			get
			{
				return this._intendString;
			}
			set
			{
				this._intendString = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0003BAC1 File Offset: 0x00039CC1
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x0003BAC9 File Offset: 0x00039CC9
		public string NewLine
		{
			get
			{
				return this._newLineString;
			}
			set
			{
				this._newLineString = value;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0003BAD4 File Offset: 0x00039CD4
		string IStyleFormatter.Sheet(IEnumerable<IStyleFormattable> rules)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			bool flag = true;
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				foreach (IStyleFormattable styleFormattable in rules)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringWriter.Write(this._newLineString);
						stringWriter.Write(this._newLineString);
					}
					styleFormattable.ToCss(stringWriter, this);
				}
			}
			return stringBuilder.ToPool();
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0003BB68 File Offset: 0x00039D68
		string IStyleFormatter.Block(IEnumerable<IStyleFormattable> rules)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder().Append('{').Append(' ');
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				foreach (IStyleFormattable styleFormattable in rules)
				{
					stringWriter.Write(this._newLineString);
					stringWriter.Write(this.Intend(styleFormattable.ToCss(this)));
					stringWriter.Write(this._newLineString);
				}
			}
			return stringBuilder.Append('}').ToPool();
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0003BC14 File Offset: 0x00039E14
		string IStyleFormatter.Declaration(string name, string value, bool important)
		{
			return CssStyleFormatter.Instance.Declaration(name, value, important);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0003BC23 File Offset: 0x00039E23
		string IStyleFormatter.Declarations(IEnumerable<string> declarations)
		{
			return string.Join(this._newLineString, declarations.Select((string m) => m + ";"));
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0003BC55 File Offset: 0x00039E55
		string IStyleFormatter.Medium(bool exclusive, bool inverse, string type, IEnumerable<IStyleFormattable> constraints)
		{
			return CssStyleFormatter.Instance.Medium(exclusive, inverse, type, constraints);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0003BC66 File Offset: 0x00039E66
		string IStyleFormatter.Constraint(string name, string value)
		{
			return CssStyleFormatter.Instance.Constraint(name, value);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0003BC74 File Offset: 0x00039E74
		string IStyleFormatter.Rule(string name, string value)
		{
			return CssStyleFormatter.Instance.Rule(name, value);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0003BC82 File Offset: 0x00039E82
		string IStyleFormatter.Rule(string name, string prelude, string rules)
		{
			return CssStyleFormatter.Instance.Rule(name, prelude, rules);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0003BC94 File Offset: 0x00039E94
		string IStyleFormatter.Style(string selector, IStyleFormattable rules)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder().Append(selector).Append(" {");
			string text = rules.ToCss(this);
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.Append(this._newLineString);
				stringBuilder.Append(this.Intend(text));
				stringBuilder.Append(this._newLineString);
			}
			else
			{
				stringBuilder.Append(' ');
			}
			return stringBuilder.Append('}').ToPool();
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0003BD07 File Offset: 0x00039F07
		string IStyleFormatter.Comment(string data)
		{
			return CssStyleFormatter.Instance.Comment(data);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0003BD14 File Offset: 0x00039F14
		private string Intend(string content)
		{
			return this._intendString + content.Replace(this._newLineString, this._newLineString + this._intendString);
		}

		// Token: 0x04000720 RID: 1824
		private string _intendString;

		// Token: 0x04000721 RID: 1825
		private string _newLineString;
	}
}
