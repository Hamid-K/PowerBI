using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200005A RID: 90
	internal sealed class ScopeValueWriter : DsrObjectWriterBase
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00005510 File Offset: 0x00003710
		internal void WriteValue(object value)
		{
			base.Writer.WriteTypeEncodedProperty(base.DsrNames.ValueUpper, value);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005529 File Offset: 0x00003729
		internal void WriteKey(string value)
		{
			base.Writer.WriteProperty(base.DsrNames.Key, value);
		}
	}
}
