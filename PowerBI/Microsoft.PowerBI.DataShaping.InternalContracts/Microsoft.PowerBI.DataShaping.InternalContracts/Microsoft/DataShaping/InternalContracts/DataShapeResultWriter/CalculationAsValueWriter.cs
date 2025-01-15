using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200003D RID: 61
	internal sealed class CalculationAsValueWriter : InlinedDsrStructureWriterBase, ICalculationWriter
	{
		// Token: 0x06000152 RID: 338 RVA: 0x000046AD File Offset: 0x000028AD
		public void WriteIdAndVariantValue(string id, object value)
		{
			base.Writer.WriteTypeEncodedValue(value);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000046BB File Offset: 0x000028BB
		public void WriteIdAndSimpleValue(string id, object value)
		{
			base.Writer.WriteSimpleEncodedValue(value);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000046C9 File Offset: 0x000028C9
		public void WriteIdAndJsonEncodedValue(string id, string value)
		{
			base.Writer.WriteJsonEncodedValue(value);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000046D7 File Offset: 0x000028D7
		public void WriteIdAndJsonEncodedString(string id, string value)
		{
			base.Writer.WriteJsonEncodedString(value);
		}
	}
}
