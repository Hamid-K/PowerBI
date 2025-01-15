using System;
using System.Linq;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000195 RID: 405
	public abstract class ManyToOneColumn
	{
		// Token: 0x060008A9 RID: 2217 RVA: 0x0002FDA8 File Offset: 0x0002DFA8
		protected virtual bool TryParse(string str)
		{
			int num = str.IndexOf(':');
			if (num < 0 && str.IndexOf(',') < 0)
			{
				this.name = str;
				this.source = new string[] { str };
				return true;
			}
			if (0 >= num || num >= str.Length - 1)
			{
				return false;
			}
			this.name = str.Substring(0, num);
			string text = str.Substring(num + 1);
			if (text.Contains(":"))
			{
				return false;
			}
			this.source = text.Split(new char[] { ',' });
			return this.source.All((string s) => !string.IsNullOrEmpty(s));
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002FE6C File Offset: 0x0002E06C
		protected bool TryParse(string str, out string extra)
		{
			extra = null;
			int num = str.IndexOf(':');
			if (num < 0)
			{
				this.name = str;
				this.source = new string[] { str };
				return true;
			}
			if (num == 0 || num >= str.Length - 1)
			{
				return false;
			}
			this.name = str.Substring(0, num);
			int num2 = num + 1;
			num = str.IndexOf(':', num2);
			string text;
			if (num < 0)
			{
				text = str.Substring(num2);
			}
			else
			{
				if (num == num2 || num >= str.Length - 1)
				{
					return false;
				}
				extra = str.Substring(num2, num - num2);
				text = str.Substring(num + 1);
				if (text.Contains(':'))
				{
					return false;
				}
			}
			this.source = text.Split(new char[] { ',' });
			return this.source.All((string s) => !string.IsNullOrEmpty(s));
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0002FF6C File Offset: 0x0002E16C
		protected virtual bool TryUnparseCore(StringBuilder sb)
		{
			if (string.IsNullOrWhiteSpace(this.name) || Utils.Size<string>(this.source) == 0)
			{
				return false;
			}
			if (CmdQuoter.NeedsQuoting(this.name))
			{
				return false;
			}
			if (this.source.Any((string x) => CmdQuoter.NeedsQuoting(x) || x.Contains(",")))
			{
				return false;
			}
			if (this.source.Length == 1 && this.source[0] == this.name)
			{
				sb.Append(this.name);
				return true;
			}
			sb.Append(this.name).Append(':');
			string text = "";
			foreach (string text2 in this.source)
			{
				sb.Append(text).Append(text2);
				text = ",";
			}
			return true;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00030060 File Offset: 0x0002E260
		protected virtual bool TryUnparseCore(StringBuilder sb, string extra)
		{
			if (string.IsNullOrWhiteSpace(this.name) || Utils.Size<string>(this.source) == 0)
			{
				return false;
			}
			if (CmdQuoter.NeedsQuoting(this.name))
			{
				return false;
			}
			if (this.source.Any((string x) => CmdQuoter.NeedsQuoting(x) || x.Contains(",")))
			{
				return false;
			}
			sb.Append(this.name).Append(':').Append(extra)
				.Append(':');
			string text = "";
			foreach (string text2 in this.source)
			{
				sb.Append(text).Append(text2);
				text = ",";
			}
			return true;
		}

		// Token: 0x04000477 RID: 1143
		[Argument(0, HelpText = "Name of the new column", ShortName = "name")]
		public string name;

		// Token: 0x04000478 RID: 1144
		[Argument(4, HelpText = "Name of the source column", ShortName = "src")]
		public string[] source;
	}
}
