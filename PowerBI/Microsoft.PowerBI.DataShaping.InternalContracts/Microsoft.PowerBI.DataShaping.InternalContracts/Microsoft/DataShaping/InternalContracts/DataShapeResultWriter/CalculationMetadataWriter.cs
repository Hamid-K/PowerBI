using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000039 RID: 57
	internal sealed class CalculationMetadataWriter : DsrObjectWriterBase
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00004544 File Offset: 0x00002744
		public void WriteCalculationMetadata(string id, int conceptualTypeCode, string dictId)
		{
			base.Writer.WriteProperty(base.DsrNames.Id, id);
			base.Writer.WriteProperty(base.DsrNames.DataType, conceptualTypeCode);
			if (dictId != null)
			{
				base.Writer.WriteProperty(base.DsrNames.DictionaryId, dictId);
			}
		}
	}
}
