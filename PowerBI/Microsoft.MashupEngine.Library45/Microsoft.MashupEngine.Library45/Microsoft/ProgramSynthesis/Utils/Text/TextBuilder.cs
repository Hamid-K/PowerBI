using System;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000524 RID: 1316
	public class TextBuilder
	{
		// Token: 0x06001D4C RID: 7500 RVA: 0x00057298 File Offset: 0x00055498
		private TextBuilder(int indentSize = 4)
		{
			this._indentSize = indentSize;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x000572B2 File Offset: 0x000554B2
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x000572BA File Offset: 0x000554BA
		public int IndentLevel { get; private set; }

		// Token: 0x06001D4F RID: 7503 RVA: 0x000572C4 File Offset: 0x000554C4
		public TextBuilder Add(string text)
		{
			if (this.IndentLevel > 0 && text.Contains(Environment.NewLine))
			{
				string text2 = new string(' ', this.IndentLevel * this._indentSize);
				text = text2 + text.Replace(Environment.NewLine, Environment.NewLine + text2);
			}
			this._output.Append(text);
			return this;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x00057328 File Offset: 0x00055528
		public TextBuilder AddBlankLine()
		{
			this.AddLine(string.Empty);
			return this;
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00057338 File Offset: 0x00055538
		public TextBuilder AddBlankLine(int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.AddBlankLine();
			}
			return this;
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x00057359 File Offset: 0x00055559
		public TextBuilder AddHeading(string text)
		{
			this.AddLine(text);
			this.AddLine(new string('‾', text.Length));
			return this;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0005737C File Offset: 0x0005557C
		public TextBuilder AddLine(string text)
		{
			if (this.IndentLevel > 0)
			{
				string text2 = new string(' ', this.IndentLevel * this._indentSize);
				text = text2 + text.Replace(Environment.NewLine, Environment.NewLine + text2);
			}
			if (text.EndsWith(Environment.NewLine))
			{
				this._output.Append(text);
			}
			else
			{
				this._output.AppendLine(text);
			}
			return this;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x000573EF File Offset: 0x000555EF
		public TextBuilder AddSection(string heading, string body, int trailingBlankLines = 2)
		{
			if (string.IsNullOrEmpty(body))
			{
				body = "<empty>";
			}
			this.AddHeading(heading);
			this.AddLine(body.TrimEnd(Array.Empty<char>()));
			this.AddBlankLine(trailingBlankLines);
			return this;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x00057423 File Offset: 0x00055623
		public TextBuilder AddTitle(string text)
		{
			this.AddLine(text);
			this.AddLine(new string('═', text.Length));
			return this;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x00057445 File Offset: 0x00055645
		public static TextBuilder Create(int indentSize = 4)
		{
			return new TextBuilder(indentSize);
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x00057450 File Offset: 0x00055650
		public TextBuilder PopIndent()
		{
			if (this.IndentLevel == 0)
			{
				throw new Exception("Indent is at zero.");
			}
			int indentLevel = this.IndentLevel;
			this.IndentLevel = indentLevel - 1;
			return this;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x00057484 File Offset: 0x00055684
		public TextBuilder PushIndent()
		{
			int indentLevel = this.IndentLevel;
			this.IndentLevel = indentLevel + 1;
			return this;
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x000574A2 File Offset: 0x000556A2
		public string Render()
		{
			return this._output.ToString();
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x000574AF File Offset: 0x000556AF
		public override string ToString()
		{
			return this.Render();
		}

		// Token: 0x04000E39 RID: 3641
		private readonly int _indentSize;

		// Token: 0x04000E3A RID: 3642
		private readonly StringBuilder _output = new StringBuilder();
	}
}
