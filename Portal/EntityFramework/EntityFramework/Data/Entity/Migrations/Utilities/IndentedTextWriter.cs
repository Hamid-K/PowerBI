using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x020000A5 RID: 165
	public class IndentedTextWriter : TextWriter
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0001F3C6 File Offset: 0x0001D5C6
		public override Encoding Encoding
		{
			get
			{
				return this._writer.Encoding;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0001F3D3 File Offset: 0x0001D5D3
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x0001F3E0 File Offset: 0x0001D5E0
		public override string NewLine
		{
			get
			{
				return this._writer.NewLine;
			}
			set
			{
				this._writer.NewLine = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0001F3EE File Offset: 0x0001D5EE
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x0001F3F6 File Offset: 0x0001D5F6
		public int Indent
		{
			get
			{
				return this._indentLevel;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this._indentLevel = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0001F406 File Offset: 0x0001D606
		public TextWriter InnerWriter
		{
			get
			{
				return this._writer;
			}
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0001F40E File Offset: 0x0001D60E
		public IndentedTextWriter(TextWriter writer)
			: this(writer, "    ")
		{
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0001F41C File Offset: 0x0001D61C
		public IndentedTextWriter(TextWriter writer, string tabString)
			: base(IndentedTextWriter.Culture)
		{
			this._writer = writer;
			this._tabString = tabString;
			this._indentLevel = 0;
			this._tabsPending = false;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0001F450 File Offset: 0x0001D650
		public override void Close()
		{
			this._writer.Close();
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0001F45D File Offset: 0x0001D65D
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0001F46A File Offset: 0x0001D66A
		protected virtual void OutputTabs()
		{
			if (!this._tabsPending)
			{
				return;
			}
			this._writer.Write(this.CurrentIndentation());
			this._tabsPending = false;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0001F490 File Offset: 0x0001D690
		public virtual string CurrentIndentation()
		{
			if (this._indentLevel <= 0 || string.IsNullOrEmpty(this._tabString))
			{
				return string.Empty;
			}
			if (this._indentLevel == 1)
			{
				return this._tabString;
			}
			int num = this._indentLevel - 2;
			string text = ((num < this._cachedIndents.Count) ? this._cachedIndents[num] : null);
			if (text == null)
			{
				text = this.BuildIndent(this._indentLevel);
				if (num == this._cachedIndents.Count)
				{
					this._cachedIndents.Add(text);
				}
				else
				{
					for (int i = this._cachedIndents.Count; i <= num; i++)
					{
						this._cachedIndents.Add(null);
					}
					this._cachedIndents[num] = text;
				}
			}
			return text;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0001F54C File Offset: 0x0001D74C
		private string BuildIndent(int numberOfIndents)
		{
			StringBuilder stringBuilder = new StringBuilder(numberOfIndents * this._tabString.Length);
			for (int i = 0; i < numberOfIndents; i++)
			{
				stringBuilder.Append(this._tabString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0001F58B File Offset: 0x0001D78B
		public override void Write(string value)
		{
			this.OutputTabs();
			this._writer.Write(value);
			if (value != null && (value.Equals("\r\n", StringComparison.Ordinal) || value.Equals("\n", StringComparison.Ordinal)))
			{
				this._tabsPending = true;
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0001F5C5 File Offset: 0x0001D7C5
		public override void Write(bool value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0001F5D9 File Offset: 0x0001D7D9
		public override void Write(char value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0001F5ED File Offset: 0x0001D7ED
		public override void Write(char[] buffer)
		{
			this.OutputTabs();
			this._writer.Write(buffer);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0001F601 File Offset: 0x0001D801
		public override void Write(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.Write(buffer, index, count);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0001F617 File Offset: 0x0001D817
		public override void Write(double value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0001F62B File Offset: 0x0001D82B
		public override void Write(float value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0001F63F File Offset: 0x0001D83F
		public override void Write(int value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0001F653 File Offset: 0x0001D853
		public override void Write(long value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0001F667 File Offset: 0x0001D867
		public override void Write(object value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0001F67B File Offset: 0x0001D87B
		public override void Write(string format, object arg0)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0001F690 File Offset: 0x0001D890
		public override void Write(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0, arg1);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0001F6A6 File Offset: 0x0001D8A6
		public override void Write(string format, params object[] arg)
		{
			this.OutputTabs();
			this._writer.Write(format, arg);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0001F6BB File Offset: 0x0001D8BB
		public void WriteLineNoTabs(string value)
		{
			this._writer.WriteLine(value);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0001F6C9 File Offset: 0x0001D8C9
		public override void WriteLine(string value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0001F6E4 File Offset: 0x0001D8E4
		public override void WriteLine()
		{
			this.OutputTabs();
			this._writer.WriteLine();
			this._tabsPending = true;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0001F6FE File Offset: 0x0001D8FE
		public override void WriteLine(bool value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0001F719 File Offset: 0x0001D919
		public override void WriteLine(char value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0001F734 File Offset: 0x0001D934
		public override void WriteLine(char[] buffer)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer);
			this._tabsPending = true;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0001F74F File Offset: 0x0001D94F
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer, index, count);
			this._tabsPending = true;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0001F76C File Offset: 0x0001D96C
		public override void WriteLine(double value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0001F787 File Offset: 0x0001D987
		public override void WriteLine(float value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0001F7A2 File Offset: 0x0001D9A2
		public override void WriteLine(int value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0001F7BD File Offset: 0x0001D9BD
		public override void WriteLine(long value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0001F7D8 File Offset: 0x0001D9D8
		public override void WriteLine(object value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0001F7F3 File Offset: 0x0001D9F3
		public override void WriteLine(string format, object arg0)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0);
			this._tabsPending = true;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0001F80F File Offset: 0x0001DA0F
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0, arg1);
			this._tabsPending = true;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0001F82C File Offset: 0x0001DA2C
		public override void WriteLine(string format, params object[] arg)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg);
			this._tabsPending = true;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0001F848 File Offset: 0x0001DA48
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x04000834 RID: 2100
		public const string DefaultTabString = "    ";

		// Token: 0x04000835 RID: 2101
		public static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

		// Token: 0x04000836 RID: 2102
		private readonly TextWriter _writer;

		// Token: 0x04000837 RID: 2103
		private int _indentLevel;

		// Token: 0x04000838 RID: 2104
		private bool _tabsPending;

		// Token: 0x04000839 RID: 2105
		private readonly string _tabString;

		// Token: 0x0400083A RID: 2106
		private readonly List<string> _cachedIndents = new List<string>();
	}
}
