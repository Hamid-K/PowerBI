using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200003C RID: 60
	internal sealed class CalculationAsPropertyWriter : InlinedDsrStructureWriterBase, ICalculationWriter
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00004669 File Offset: 0x00002869
		public void WriteIdAndVariantValue(string id, object value)
		{
			base.Writer.WriteTypeEncodedProperty(id, value);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004678 File Offset: 0x00002878
		public void WriteIdAndSimpleValue(string id, object value)
		{
			base.Writer.WriteSimpleEncodedProperty(id, value);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004687 File Offset: 0x00002887
		public void WriteIdAndJsonEncodedValue(string id, string value)
		{
			base.Writer.WriteJsonEncodedProperty(id, value);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004696 File Offset: 0x00002896
		public void WriteIdAndJsonEncodedString(string id, string value)
		{
			base.Writer.WriteJsonEncodedStringProperty(id, value);
		}
	}
}
