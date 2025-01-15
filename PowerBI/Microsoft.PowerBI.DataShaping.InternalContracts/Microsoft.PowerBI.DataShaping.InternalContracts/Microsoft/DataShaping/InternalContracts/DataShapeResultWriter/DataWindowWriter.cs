using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000045 RID: 69
	internal sealed class DataWindowWriter : DsrObjectWriterBase
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00004B49 File Offset: 0x00002D49
		internal void WriteId(string value)
		{
			base.Writer.WriteProperty(base.DsrNames.Id, value);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004B62 File Offset: 0x00002D62
		internal void WriteIsComplete(bool isComplete)
		{
			base.Writer.WriteProperty(base.DsrNames.IsComplete, isComplete);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004B7B File Offset: 0x00002D7B
		internal RestartTokenCollectionWriter BeginRestartTokens()
		{
			base.Writer.BeginProperty(base.DsrNames.RestartTokens);
			base.CreateAndBeginChild<RestartTokenCollectionWriter>(ref this._restartTokensWriter);
			return this._restartTokensWriter;
		}

		// Token: 0x040000AE RID: 174
		private RestartTokenCollectionWriter _restartTokensWriter;
	}
}
