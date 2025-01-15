using System;
using System.Text;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000123 RID: 291
	public sealed class DaxArgumentStringBuilder
	{
		// Token: 0x0600103A RID: 4154 RVA: 0x0002C80A File Offset: 0x0002AA0A
		public DaxArgumentStringBuilder(int argsCount, bool multiLine)
		{
			this._argsCount = argsCount;
			this._multiLine = multiLine;
			this._argsCounter = 0;
			this._stringBuilder = new StringBuilder();
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0002C832 File Offset: 0x0002AA32
		public string ToDax()
		{
			return this._stringBuilder.ToString();
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0002C840 File Offset: 0x0002AA40
		public void AppendArgument(string argument)
		{
			this._argsCounter++;
			if (this._multiLine)
			{
				this._stringBuilder.Append(DaxFormat.IncreaseIndent(argument));
				if (this._argsCounter != this._argsCount)
				{
					this._stringBuilder.Append(",");
					this._stringBuilder.Append(DaxFormat.NewLine);
					this._stringBuilder.Append('\t');
					return;
				}
			}
			else
			{
				this._stringBuilder.Append(argument);
				if (this._argsCounter != this._argsCount)
				{
					this._stringBuilder.Append(", ");
				}
			}
		}

		// Token: 0x04000A72 RID: 2674
		private readonly StringBuilder _stringBuilder;

		// Token: 0x04000A73 RID: 2675
		private readonly int _argsCount;

		// Token: 0x04000A74 RID: 2676
		private readonly bool _multiLine;

		// Token: 0x04000A75 RID: 2677
		private int _argsCounter;
	}
}
