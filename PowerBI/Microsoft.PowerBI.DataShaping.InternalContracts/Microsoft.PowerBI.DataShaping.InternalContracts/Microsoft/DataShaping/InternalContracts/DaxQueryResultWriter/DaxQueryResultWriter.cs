using System;

namespace Microsoft.DataShaping.InternalContracts.DaxQueryResultWriter
{
	// Token: 0x02000035 RID: 53
	internal sealed class DaxQueryResultWriter : DaxQueryResultObjectWriterBase
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00004403 File Offset: 0x00002603
		internal DaxQueryResultCollectionWriter<DaxQueryResultTableWriter> BeginTables()
		{
			base.Writer.BeginProperty("tables");
			base.CreateAndBeginChild<DaxQueryResultTableWriter>(ref this._tablesWriter);
			return this._tablesWriter;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004428 File Offset: 0x00002628
		internal void WriteException(HandledExceptionWrapper ex)
		{
			base.CreateChild<DaxQueryResultErrorWriter>(ref this._errorWriter);
			this._errorWriter.WriteException(ex);
		}

		// Token: 0x0400008A RID: 138
		private DaxQueryResultCollectionWriter<DaxQueryResultTableWriter> _tablesWriter;

		// Token: 0x0400008B RID: 139
		private DaxQueryResultErrorWriter _errorWriter;
	}
}
