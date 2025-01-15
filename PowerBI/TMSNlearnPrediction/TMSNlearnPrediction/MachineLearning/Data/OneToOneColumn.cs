using System;
using System.Text;
using Microsoft.MachineLearning.CommandLine;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200002C RID: 44
	public abstract class OneToOneColumn
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00008085 File Offset: 0x00006285
		protected virtual bool TryParse(string str)
		{
			return ColumnParsingUtils.TryParse(str, out this.name, out this.source);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008099 File Offset: 0x00006299
		protected bool TryParse(string str, out string extra)
		{
			return ColumnParsingUtils.TryParse(str, out this.name, out this.source, out extra);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000080B0 File Offset: 0x000062B0
		protected virtual bool TryUnparseCore(StringBuilder sb)
		{
			if (!this.TrySanitize())
			{
				return false;
			}
			if (CmdQuoter.NeedsQuoting(this.name) || CmdQuoter.NeedsQuoting(this.source))
			{
				return false;
			}
			sb.Append(this.name);
			if (this.source != this.name)
			{
				sb.Append(':').Append(this.source);
			}
			return true;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008118 File Offset: 0x00006318
		protected virtual bool TryUnparseCore(StringBuilder sb, string extra)
		{
			if (!this.TrySanitize())
			{
				return false;
			}
			if (CmdQuoter.NeedsQuoting(this.name) || CmdQuoter.NeedsQuoting(this.source))
			{
				return false;
			}
			sb.Append(this.name).Append(':').Append(extra)
				.Append(':')
				.Append(this.source);
			return true;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00008178 File Offset: 0x00006378
		public bool TrySanitize()
		{
			if (string.IsNullOrWhiteSpace(this.name))
			{
				this.name = this.source;
			}
			else if (string.IsNullOrWhiteSpace(this.source))
			{
				this.source = this.name;
			}
			return !string.IsNullOrWhiteSpace(this.name);
		}

		// Token: 0x04000073 RID: 115
		[Argument(0, HelpText = "Name of the new column", ShortName = "name")]
		public string name;

		// Token: 0x04000074 RID: 116
		[Argument(0, HelpText = "Name of the source column", ShortName = "src")]
		public string source;
	}
}
