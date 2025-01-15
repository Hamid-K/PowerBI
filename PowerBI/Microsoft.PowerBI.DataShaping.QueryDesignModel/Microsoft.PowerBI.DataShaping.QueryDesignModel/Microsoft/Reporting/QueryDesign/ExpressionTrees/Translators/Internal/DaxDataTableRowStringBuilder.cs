using System;
using System.Text;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000127 RID: 295
	public sealed class DaxDataTableRowStringBuilder
	{
		// Token: 0x06001053 RID: 4179 RVA: 0x0002CB50 File Offset: 0x0002AD50
		public DaxDataTableRowStringBuilder(int columnCount)
			: this(new StringBuilder(), columnCount)
		{
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0002CB5E File Offset: 0x0002AD5E
		public DaxDataTableRowStringBuilder(StringBuilder stringBuilder, int columnCount)
		{
			this._colCounter = 0;
			this._stringBuilder = stringBuilder;
			this._columnCount = columnCount;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0002CB7B File Offset: 0x0002AD7B
		public string ToDax()
		{
			return this._stringBuilder.ToString();
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0002CB88 File Offset: 0x0002AD88
		public void Begin()
		{
			this._colCounter = 0;
			if (this._columnCount > 1)
			{
				this._stringBuilder.Append("(");
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0002CBAB File Offset: 0x0002ADAB
		public void AppendColumn(string argument)
		{
			if (this._colCounter > 0)
			{
				this._stringBuilder.Append(", ");
			}
			this._stringBuilder.Append(argument);
			this._colCounter++;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0002CBE2 File Offset: 0x0002ADE2
		public void End()
		{
			if (this._columnCount > 1)
			{
				this._stringBuilder.Append(")");
			}
		}

		// Token: 0x04000A7D RID: 2685
		private readonly StringBuilder _stringBuilder;

		// Token: 0x04000A7E RID: 2686
		private int _colCounter;

		// Token: 0x04000A7F RID: 2687
		private int _columnCount;
	}
}
