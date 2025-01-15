using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200003B RID: 59
	internal sealed class CalculationAsObjectWriter : DsrObjectWriterBase, ICalculationWriter
	{
		// Token: 0x06000148 RID: 328 RVA: 0x000045A1 File Offset: 0x000027A1
		public void WriteIdAndVariantValue(string id, object value)
		{
			base.Writer.WriteProperty(base.DsrNames.Id, id);
			base.Writer.WriteTypeEncodedProperty(base.DsrNames.ValueUpper, value);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000045D1 File Offset: 0x000027D1
		public void WriteIdAndSimpleValue(string id, object value)
		{
			base.Writer.WriteProperty(base.DsrNames.Id, id);
			base.Writer.WriteSimpleEncodedProperty(base.DsrNames.ValueUpper, value);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004601 File Offset: 0x00002801
		public void WriteIdAndJsonEncodedValue(string id, string value)
		{
			base.Writer.WriteProperty(base.DsrNames.Id, id);
			base.Writer.WriteJsonEncodedProperty(base.DsrNames.ValueUpper, value);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004631 File Offset: 0x00002831
		public void WriteIdAndJsonEncodedString(string id, string value)
		{
			base.Writer.WriteProperty(base.DsrNames.Id, id);
			base.Writer.WriteJsonEncodedStringProperty(base.DsrNames.ValueUpper, value);
		}
	}
}
