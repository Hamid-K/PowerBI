using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200002E RID: 46
	public interface IDataReaderFieldProperties
	{
		// Token: 0x060000B7 RID: 183
		int GetPropertyCount(int fieldIndex);

		// Token: 0x060000B8 RID: 184
		string GetPropertyName(int fieldIndex, int propertyIndex);

		// Token: 0x060000B9 RID: 185
		object GetPropertyValue(int fieldIndex, int propertyIndex);
	}
}
