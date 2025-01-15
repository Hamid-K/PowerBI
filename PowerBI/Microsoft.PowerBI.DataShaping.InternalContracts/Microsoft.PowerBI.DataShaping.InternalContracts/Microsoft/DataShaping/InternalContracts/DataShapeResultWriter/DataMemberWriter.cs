using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000042 RID: 66
	internal sealed class DataMemberWriter : DsrObjectWriterBase
	{
		// Token: 0x06000168 RID: 360 RVA: 0x000048B9 File Offset: 0x00002AB9
		internal CollectionWriter<DataMemberInstanceWriter> BeginInstances()
		{
			return this.BeginInstances(base.DsrNames.Instances);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000048CC File Offset: 0x00002ACC
		internal CollectionWriter<DataMemberInstanceWriter> BeginInstances(string dataMemberId)
		{
			base.Writer.BeginProperty(dataMemberId);
			base.CreateAndBeginChild<DataMemberInstanceWriter>(ref this._instancesWriter);
			return this._instancesWriter;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000048ED File Offset: 0x00002AED
		internal void WriteId(string value)
		{
			base.Writer.WriteProperty(base.DsrNames.Id, value);
		}

		// Token: 0x040000A4 RID: 164
		private CollectionWriter<DataMemberInstanceWriter> _instancesWriter;
	}
}
