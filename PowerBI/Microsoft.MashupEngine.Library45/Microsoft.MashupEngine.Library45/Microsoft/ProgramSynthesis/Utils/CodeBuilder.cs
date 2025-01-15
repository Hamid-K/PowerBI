using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000405 RID: 1029
	[DebuggerDisplay("Code = {_code}")]
	public class CodeBuilder
	{
		// Token: 0x06001754 RID: 5972 RVA: 0x0004729C File Offset: 0x0004549C
		public CodeBuilder(uint indentSize = 4U)
		{
			this._indentLevels = new Stack<int>();
			this._indentSize = Convert.ToInt32(indentSize);
			this._currentIndentString = "";
			this._code = new StringBuilder();
			this._indentLevels.Push(0);
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x000472E8 File Offset: 0x000454E8
		public bool IsEmpty
		{
			get
			{
				return this._code.Length == 0;
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000472F8 File Offset: 0x000454F8
		public static CodeBuilder Create(string initialCode)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.Append(initialCode);
			return codeBuilder;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00047308 File Offset: 0x00045508
		public void PushIndent(uint additionalIndent = 1U)
		{
			int num = (int)((long)this._indentLevels.Peek() + (long)((ulong)additionalIndent));
			this._indentLevels.Push(num);
			this._currentIndentString = new string(' ', num * this._indentSize);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00047347 File Offset: 0x00045547
		public void PopIndent()
		{
			this._indentLevels.Pop();
			this._currentIndentString = new string(' ', this._indentLevels.Peek() * this._indentSize);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00047374 File Offset: 0x00045574
		private bool AtStartOfLine()
		{
			return this._code.Length == 0 || this._code[this._code.Length - 1] == '\n';
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000473A4 File Offset: 0x000455A4
		private void Append(string code, bool allowLeadingWhiteSpace)
		{
			if (string.IsNullOrEmpty(code))
			{
				return;
			}
			string[] array = code.Split(new char[] { '\n' });
			if (!allowLeadingWhiteSpace)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if ((i > 0 || this.AtStartOfLine()) && array[i].Length > 0 && char.IsWhiteSpace(array[i][0]))
					{
						throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Whitespace (0x{0:x2}) at start of line {1} in input '{2}'", new object[]
						{
							(int)array[i][0],
							i,
							code
						})));
					}
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (j != 0)
				{
					this.AppendLine();
				}
				string text = array[j];
				if (this.AtStartOfLine() && !string.IsNullOrWhiteSpace(text))
				{
					this._code.Append(this._currentIndentString);
				}
				this._code.Append(text);
			}
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00047487 File Offset: 0x00045687
		public void Append(string code)
		{
			this.Append(code, false);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00047491 File Offset: 0x00045691
		public void AppendIndented(string code)
		{
			this.Append(code, true);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0004749B File Offset: 0x0004569B
		public void Prepend(CodeBuilder code)
		{
			if (!code.AtStartOfLine())
			{
				code.AppendLine();
			}
			this._code.Insert(0, code.GetCode());
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x000474BE File Offset: 0x000456BE
		public void Append(CodeBuilder code)
		{
			this.AppendIndented(code.GetCode());
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x000474CC File Offset: 0x000456CC
		public void AppendLine(string codeLine)
		{
			this.Append(codeLine);
			this.AppendLine();
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000474DC File Offset: 0x000456DC
		public void AppendLine()
		{
			int num = this._code.Length - 1;
			while (num >= 0 && this._code[num] != '\n' && char.IsWhiteSpace(this._code[num]))
			{
				num--;
			}
			this._code.Length = num + 1;
			this._code.AppendLine();
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00047540 File Offset: 0x00045740
		public void AppendBlock(string block)
		{
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			using (StringReader stringReader = new StringReader(block))
			{
				string text = stringReader.ReadLine();
				if (text != null)
				{
					this.AppendIndented(text);
					while ((text = stringReader.ReadLine()) != null)
					{
						this.AppendLine();
						this.AppendIndented(text);
					}
				}
			}
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000475AC File Offset: 0x000457AC
		public void AppendDelimitedList(string prefix, IReadOnlyList<string> elements, string sep = ",", string open = "(", string close = ")", IReadOnlyList<string> elementComments = null, int maxLineLength = 80)
		{
			if (prefix != null)
			{
				if (!elements.Any((string el) => el == null))
				{
					if (elements == null)
					{
						throw new ArgumentNullException("elements");
					}
					if (open == null)
					{
						throw new ArgumentNullException("open");
					}
					if (close == null)
					{
						throw new ArgumentNullException("close");
					}
					this.Append(prefix);
					this.Append(open);
					int num;
					if (elementComments != null)
					{
						num = int.MaxValue;
					}
					else
					{
						num = this._indentLevels.Peek() + prefix.Length + open.Length + close.Length + elements.Sum((string el) => el.Length) + (elements.Count - 1) * (sep.Length + 1);
					}
					int num2 = num;
					if (elementComments == null && num2 <= maxLineLength)
					{
						string text = (string.IsNullOrWhiteSpace(sep) ? sep : (sep + " "));
						this.Append(string.Join(text, elements));
					}
					else
					{
						this.AppendLine();
						using (this.NewScope(null, 1U))
						{
							for (int i = 0; i < elements.Count; i++)
							{
								this.Append(elements[i]);
								this.Append(sep);
								if (elementComments != null && elementComments[i] != null)
								{
									this.Append(elementComments[i]);
								}
								this.AppendLine();
							}
						}
					}
					this.AppendLine(close);
					return;
				}
			}
			throw new ArgumentNullException("prefix");
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00047738 File Offset: 0x00045938
		public IDisposable NewScope(string introduction = null, uint additionalIndent = 1U)
		{
			if (!string.IsNullOrEmpty(introduction))
			{
				this.AppendLine(introduction);
			}
			return new CodeBuilder.IndentationContext(this, additionalIndent, null);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00047751 File Offset: 0x00045951
		public IDisposable NewBracesScope(string introduction = null, uint additionalIndent = 1U)
		{
			this.Append(introduction);
			this.AppendLine(string.IsNullOrEmpty(introduction) ? "{" : " {");
			return new CodeBuilder.IndentationContext(this, additionalIndent, "}");
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00047780 File Offset: 0x00045980
		public string GetCode()
		{
			return this._code.ToString();
		}

		// Token: 0x04000B2E RID: 2862
		private const int MaxLineLength = 80;

		// Token: 0x04000B2F RID: 2863
		private readonly Stack<int> _indentLevels;

		// Token: 0x04000B30 RID: 2864
		private string _currentIndentString;

		// Token: 0x04000B31 RID: 2865
		private readonly int _indentSize;

		// Token: 0x04000B32 RID: 2866
		private readonly StringBuilder _code;

		// Token: 0x04000B33 RID: 2867
		private const int DefaultAdditionalIndent = 1;

		// Token: 0x02000406 RID: 1030
		private sealed class IndentationContext : IDisposable
		{
			// Token: 0x17000453 RID: 1107
			// (get) Token: 0x06001766 RID: 5990 RVA: 0x0004778D File Offset: 0x0004598D
			private CodeBuilder Builder { get; }

			// Token: 0x17000454 RID: 1108
			// (get) Token: 0x06001767 RID: 5991 RVA: 0x00047795 File Offset: 0x00045995
			private string EndLine { get; }

			// Token: 0x06001768 RID: 5992 RVA: 0x0004779D File Offset: 0x0004599D
			public IndentationContext(CodeBuilder builder, uint additionalIndent = 1U, string endLine = null)
			{
				this.Builder = builder;
				this._disposed = false;
				this.EndLine = endLine;
				this.Builder.PushIndent(additionalIndent);
			}

			// Token: 0x06001769 RID: 5993 RVA: 0x000477C6 File Offset: 0x000459C6
			public void Dispose()
			{
				if (this._disposed)
				{
					return;
				}
				this.Builder.PopIndent();
				if (this.EndLine != null)
				{
					this.Builder.AppendLine(this.EndLine);
				}
				this._disposed = true;
			}

			// Token: 0x04000B35 RID: 2869
			private bool _disposed;
		}
	}
}
