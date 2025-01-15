using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200001C RID: 28
	public interface IDataReaderFieldProperties
	{
		// Token: 0x06000041 RID: 65
		int GetPropertyCount(int fieldIndex);

		// Token: 0x06000042 RID: 66
		string GetPropertyName(int fieldIndex, int propertyIndex);

		// Token: 0x06000043 RID: 67
		object GetPropertyValue(int fieldIndex, int propertyIndex);
	}
}
