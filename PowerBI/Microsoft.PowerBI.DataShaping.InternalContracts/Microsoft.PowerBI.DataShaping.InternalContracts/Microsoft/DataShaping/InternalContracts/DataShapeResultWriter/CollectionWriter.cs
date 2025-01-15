using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200003E RID: 62
	internal sealed class CollectionWriter<T> : StreamingDsrWriterWrapperBase where T : StreamingDsrWriterWrapperBase, new()
	{
		// Token: 0x06000157 RID: 343 RVA: 0x000046ED File Offset: 0x000028ED
		internal CollectionWriter(StreamingDsrWriterWrapperBase parentWriter, bool inlineInParentWriter)
			: base(parentWriter.Writer, parentWriter.DsrNames)
		{
			this._inlineInParentWriter = inlineInParentWriter;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004708 File Offset: 0x00002908
		internal override void Begin()
		{
			if (!this._inlineInParentWriter)
			{
				base.Writer.BeginArray();
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000471D File Offset: 0x0000291D
		internal T BeginItem()
		{
			base.CreateAndBeginChild<T>(ref this._childWriter);
			return this._childWriter;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004732 File Offset: 0x00002932
		internal override void End()
		{
			if (!this._inlineInParentWriter)
			{
				base.Writer.EndArray();
			}
		}

		// Token: 0x0400009D RID: 157
		private readonly bool _inlineInParentWriter;

		// Token: 0x0400009E RID: 158
		private T _childWriter;
	}
}
