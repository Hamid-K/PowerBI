using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200003F RID: 63
	internal sealed class DataIntersectionWriter : DsrCalculationContainerWriterBase
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00004747 File Offset: 0x00002947
		internal void WriteId(string value)
		{
			base.Writer.WriteProperty(base.DsrNames.Id, value);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004760 File Offset: 0x00002960
		internal void WriteIndex(int index)
		{
			base.Writer.WriteProperty(base.DsrNames.Index, index);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004779 File Offset: 0x00002979
		internal CollectionWriter<DataShapeWriter> BeginDataShapes()
		{
			base.Writer.BeginProperty(base.DsrNames.DataShapes);
			base.CreateAndBeginChild<DataShapeWriter>(ref this._dataShapesWriter);
			return this._dataShapesWriter;
		}

		// Token: 0x0400009F RID: 159
		private CollectionWriter<DataShapeWriter> _dataShapesWriter;
	}
}
