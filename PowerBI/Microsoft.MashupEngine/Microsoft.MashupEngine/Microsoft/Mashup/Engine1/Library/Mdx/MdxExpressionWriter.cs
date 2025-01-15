using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009AD RID: 2477
	internal sealed class MdxExpressionWriter
	{
		// Token: 0x060046C0 RID: 18112 RVA: 0x000ED854 File Offset: 0x000EBA54
		private MdxExpressionWriter()
		{
			this.sb = new StringBuilder();
			this.state = MdxExpressionWriter.State.StartOfLine;
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x000ED86E File Offset: 0x000EBA6E
		public MdxExpressionWriter.Scope NewScope()
		{
			return new MdxExpressionWriter.Scope(this);
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x000ED878 File Offset: 0x000EBA78
		public void WriteExpressionList(IList<MdxExpression> expressions, string start, string separator, string end)
		{
			this.Write(start);
			for (int i = 0; i < expressions.Count; i++)
			{
				using (this.NewScope())
				{
					expressions[i].Write(this);
					if (i < expressions.Count - 1)
					{
						this.Write(separator);
					}
				}
			}
			this.Write(end);
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x000ED8EC File Offset: 0x000EBAEC
		public void Write(string s)
		{
			if (s.Length > 0)
			{
				MdxExpressionWriter.State state = this.state;
				if (state != MdxExpressionWriter.State.StartOfLine)
				{
					if (state != MdxExpressionWriter.State.Terminal && !MdxExpressionWriter.terminals.Contains(s[0]))
					{
						this.sb.Append(' ');
					}
				}
				else
				{
					this.WriteIndent();
				}
				this.sb.Append(s);
				if (MdxExpressionWriter.terminals.Contains(s[s.Length - 1]))
				{
					this.state = MdxExpressionWriter.State.Terminal;
					return;
				}
				this.state = MdxExpressionWriter.State.NonTerminal;
			}
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x000ED971 File Offset: 0x000EBB71
		public void WriteLine()
		{
			this.WriteLine(string.Empty);
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x000ED97E File Offset: 0x000EBB7E
		public void WriteLine(string s)
		{
			this.Write(s);
			this.sb.AppendLine();
			this.state = MdxExpressionWriter.State.StartOfLine;
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x000ED99C File Offset: 0x000EBB9C
		public static string ToString(MdxExpression expression)
		{
			MdxExpressionWriter mdxExpressionWriter = new MdxExpressionWriter();
			expression.Write(mdxExpressionWriter);
			return mdxExpressionWriter.ToString();
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x000ED9BC File Offset: 0x000EBBBC
		public override string ToString()
		{
			return this.sb.ToString();
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x000ED9CC File Offset: 0x000EBBCC
		private void WriteIndent()
		{
			for (int i = 0; i < this.depth; i++)
			{
				this.sb.Append("    ");
			}
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x000ED9FB File Offset: 0x000EBBFB
		private void IncreaseDepth()
		{
			this.depth++;
			if (this.state != MdxExpressionWriter.State.StartOfLine)
			{
				this.WriteLine();
			}
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x000EDA19 File Offset: 0x000EBC19
		private void DecreaseDepth()
		{
			this.depth--;
			if (this.state != MdxExpressionWriter.State.StartOfLine)
			{
				this.WriteLine();
			}
		}

		// Token: 0x0400258F RID: 9615
		private const string indent = "    ";

		// Token: 0x04002590 RID: 9616
		private static readonly HashSet<char> terminals = new HashSet<char> { ' ', ',', '(', ')', '{', '}', '.' };

		// Token: 0x04002591 RID: 9617
		private readonly StringBuilder sb;

		// Token: 0x04002592 RID: 9618
		private MdxExpressionWriter.State state;

		// Token: 0x04002593 RID: 9619
		private int depth;

		// Token: 0x020009AE RID: 2478
		public struct Scope : IDisposable
		{
			// Token: 0x060046CC RID: 18124 RVA: 0x000EDA8E File Offset: 0x000EBC8E
			public Scope(MdxExpressionWriter writer)
			{
				this.writer = writer;
				this.writer.IncreaseDepth();
			}

			// Token: 0x060046CD RID: 18125 RVA: 0x000EDAA2 File Offset: 0x000EBCA2
			public void Dispose()
			{
				this.writer.DecreaseDepth();
			}

			// Token: 0x04002594 RID: 9620
			private readonly MdxExpressionWriter writer;
		}

		// Token: 0x020009AF RID: 2479
		private enum State
		{
			// Token: 0x04002596 RID: 9622
			StartOfLine,
			// Token: 0x04002597 RID: 9623
			NonTerminal,
			// Token: 0x04002598 RID: 9624
			Terminal
		}
	}
}
