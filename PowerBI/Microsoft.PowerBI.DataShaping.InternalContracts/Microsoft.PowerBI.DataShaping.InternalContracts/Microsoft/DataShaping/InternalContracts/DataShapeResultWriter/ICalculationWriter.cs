using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x0200003A RID: 58
	internal interface ICalculationWriter
	{
		// Token: 0x06000144 RID: 324
		void WriteIdAndVariantValue(string id, object value);

		// Token: 0x06000145 RID: 325
		void WriteIdAndSimpleValue(string id, object value);

		// Token: 0x06000146 RID: 326
		void WriteIdAndJsonEncodedValue(string id, string value);

		// Token: 0x06000147 RID: 327
		void WriteIdAndJsonEncodedString(string id, string value);
	}
}
