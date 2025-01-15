using System;

namespace Microsoft.DataShaping.InternalContracts.DaxQueryResultWriter
{
	// Token: 0x02000034 RID: 52
	internal sealed class DaxQueryResultTableWriter : DaxQueryResultObjectWriterBase
	{
		// Token: 0x0600012C RID: 300 RVA: 0x000043BB File Offset: 0x000025BB
		internal DaxQueryResultCollectionWriter<DaxQueryResultRowWriter> BeginRows()
		{
			base.Writer.BeginProperty("rows");
			base.CreateAndBeginChild<DaxQueryResultRowWriter>(ref this._rowsWriter);
			return this._rowsWriter;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000043E0 File Offset: 0x000025E0
		internal void WriteException(HandledExceptionWrapper ex)
		{
			base.CreateChild<DaxQueryResultErrorWriter>(ref this._errorWriter);
			this._errorWriter.WriteException(ex);
		}

		// Token: 0x04000088 RID: 136
		private DaxQueryResultCollectionWriter<DaxQueryResultRowWriter> _rowsWriter;

		// Token: 0x04000089 RID: 137
		private DaxQueryResultErrorWriter _errorWriter;
	}
}
