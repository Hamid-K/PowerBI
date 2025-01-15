using System;

namespace Microsoft.DataShaping.InternalContracts.DaxQueryResultWriter
{
	// Token: 0x02000030 RID: 48
	internal sealed class DaxQueryResultCollectionWriter<T> : DaxQueryResultWriterBase where T : DaxQueryResultWriterBase, new()
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00004083 File Offset: 0x00002283
		internal DaxQueryResultCollectionWriter(DaxQueryResultWriterBase parentWriter)
			: base(parentWriter.Writer, parentWriter.Settings)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004097 File Offset: 0x00002297
		internal override void Begin()
		{
			base.Writer.BeginArray();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000040A4 File Offset: 0x000022A4
		internal T BeginItem()
		{
			base.CreateAndBeginChild<T>(ref this._childWriter);
			return this._childWriter;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000040B9 File Offset: 0x000022B9
		internal override void End()
		{
			base.Writer.EndArray();
		}

		// Token: 0x04000086 RID: 134
		private T _childWriter;
	}
}
