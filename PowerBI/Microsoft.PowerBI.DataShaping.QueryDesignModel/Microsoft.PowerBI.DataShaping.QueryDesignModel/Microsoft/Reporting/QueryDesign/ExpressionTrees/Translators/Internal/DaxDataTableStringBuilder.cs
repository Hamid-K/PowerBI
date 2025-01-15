using System;
using System.Text;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000126 RID: 294
	public sealed class DaxDataTableStringBuilder
	{
		// Token: 0x0600104C RID: 4172 RVA: 0x0002CA69 File Offset: 0x0002AC69
		public DaxDataTableStringBuilder(int columnCount)
		{
			this._rowCounter = 0;
			this._stringBuilder = new StringBuilder();
			this._currentRowBuilder = new DaxDataTableRowStringBuilder(this._stringBuilder, columnCount);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0002CA95 File Offset: 0x0002AC95
		public void Begin()
		{
			this._stringBuilder.Append("{");
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0002CAA8 File Offset: 0x0002ACA8
		public void End()
		{
			this._stringBuilder.Append("}");
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0002CABB File Offset: 0x0002ACBB
		public string ToDax()
		{
			return this._stringBuilder.ToString();
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0002CAC8 File Offset: 0x0002ACC8
		public void BeginRow()
		{
			this._isRowActive = true;
			if (this._rowCounter > 0)
			{
				this._stringBuilder.Append(",");
				this._stringBuilder.Append(DaxFormat.NewLine);
				this._stringBuilder.Append('\t');
			}
			this._currentRowBuilder.Begin();
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0002CB20 File Offset: 0x0002AD20
		public void AppendColumn(string argument)
		{
			this._currentRowBuilder.AppendColumn(argument);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0002CB2E File Offset: 0x0002AD2E
		public void EndRow()
		{
			this._currentRowBuilder.End();
			this._isRowActive = false;
			this._rowCounter++;
		}

		// Token: 0x04000A79 RID: 2681
		private readonly StringBuilder _stringBuilder;

		// Token: 0x04000A7A RID: 2682
		private int _rowCounter;

		// Token: 0x04000A7B RID: 2683
		private DaxDataTableRowStringBuilder _currentRowBuilder;

		// Token: 0x04000A7C RID: 2684
		private bool _isRowActive;
	}
}
